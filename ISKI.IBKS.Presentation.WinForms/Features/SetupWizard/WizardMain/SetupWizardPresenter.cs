using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Steps;
using ISKI.IBKS.Shared.Localization;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using ISKI.IBKS.Infrastructure.Persistence.Seeders;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;

public sealed class SetupWizardPresenter
{
    private readonly ISetupWizardView _view;
    private readonly IbksDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IStationConfiguration _stationConfig;
    private readonly SetupState _state;
    private readonly List<ISetupWizardStep> _steps;
    private int _currentStepIndex = 0;

    public SetupWizardPresenter(
        ISetupWizardView view,
        IbksDbContext dbContext,
        IConfiguration configuration,
        IStationConfiguration stationConfig,
        IEnumerable<ISetupWizardStep> steps,
        SetupState state)
    {
        _view = view;
        _dbContext = dbContext;
        _configuration = configuration;
        _stationConfig = stationConfig;
        _steps = steps.ToList();
        _state = state;

        _state.UpdateFromConfiguration(_configuration);

        _view.NextRequested += async (s, e) => await HandleNextAsync();
        _view.BackRequested += (s, e) => HandleBack();

        ShowStep(0);
    }

    private void ShowStep(int index)
    {
        if (index < 0 || index >= _steps.Count) return;

        var step = _steps[index];
        _currentStepIndex = index;

        _view.SetStepTitle(step.Title);
        _view.SetStepDescription(step.Description);
        _view.SetStepIndicator(string.Format(Strings.Wizard_StepIndicator, index + 1, _steps.Count));
        _view.ShowStepControl(step.GetControl());

        _ = step.LoadAsync();

        _view.SetBackButtonVisible(index > 0 && index < _steps.Count - 1);
        _view.SetNextButtonText(index == _steps.Count - 1 ? Strings.Common_Finish : Strings.Common_Next);
        
        // Final step logic: Disable finish button until tests complete
        if (index == _steps.Count - 1 && step is IFinalSummaryStepView summaryView)
        {
            _view.SetNextButtonEnabled(false);
            summaryView.TestsCompleted += (s, errorCount) => 
            {
                // Always enable Finish button after tests complete
                _view.SetNextButtonEnabled(true);
                
                // Update header color based on results
                bool hasErrors = errorCount > 0;
                string summaryText = hasErrors ? Strings.Wizard_SetupWithWarnings : Strings.Wizard_SetupComplete;
                _view.UpdateTheme(hasErrors, summaryText);
            };
        }
        else
        {
            _view.SetNextButtonEnabled(true);
        }
    }

    private async Task HandleNextAsync()
    {
        var step = _steps[_currentStepIndex];
        var validation = step.Validate();

        if (!validation.IsValid)
        {
            _view.ShowError(validation.ErrorMessage ?? Strings.Setup_InvalidInput);
            return;
        }

        await step.SaveAsync();

        if (_currentStepIndex < _steps.Count - 1)
        {
            ShowStep(_currentStepIndex + 1);
        }
        else
        {
            // If it's the last step (which is now FinalSummaryStep), we finish
            await FinalizeSetupAsync();
        }
    }

    private void HandleBack()
    {
        if (_currentStepIndex > 0) ShowStep(_currentStepIndex - 1);
    }

    private async Task FinalizeSetupAsync()
    {
        try
        {
            // Disable button and show wait message
            _view.SetNextButtonEnabled(false);
            _view.SetNextButtonText(Strings.Common_Wait);
            
            // Ensure Database is created and seeded before finishing
            await _dbContext.Database.EnsureCreatedAsync();
            await AlarmSeeder.SeedAsync(_dbContext);

            // Sync with JSON files
            await _stationConfig.SaveStationIdAndNameAsync(_state.StationId, _state.StationName);
            await _stationConfig.SavePlcSettingsAsync(_state.PlcIpAddress, _state.PlcRack, _state.PlcSlot, _state.PlcSelectedSensors);
            await _stationConfig.SaveSaisSettingsAsync(new SaisSettings
            {
                BaseUrl = _state.SaisApiUrl,
                UserName = _state.SaisUserName,
                Password = _state.SaisPassword
            });
            await _stationConfig.SaveMailSettingsAsync(_state.SmtpHost, _state.SmtpPort, _state.SmtpUser, _state.SmtpPass, _state.UseSsl);
            await _stationConfig.SaveCalibrationSettingsAsync(new ISKI.IBKS.Application.Common.Configuration.CalibrationSettings
            {
                PhZeroReference = _state.PhZeroRef,
                PhSpanReference = _state.PhSpanRef,
                PhZeroDuration = _state.PhCalibrationDuration,
                PhSpanDuration = _state.PhCalibrationDuration,
                ConductivityZeroReference = _state.CondZeroRef,
                ConductivitySpanReference = _state.CondSpanRef,
                ConductivityZeroDuration = _state.CondCalibrationDuration,
                ConductivitySpanDuration = _state.CondCalibrationDuration
            });

            _view.CloseWizard();
        }
        catch (Exception ex)
        {
            var fullMessage = ex.InnerException != null 
                ? $"{ex.Message}\n\nDetay: {ex.InnerException.Message}" 
                : ex.Message;
            _view.ShowError(string.Format(Strings.Setup_SaveError, fullMessage));
        }
    }
}
