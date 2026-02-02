using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Presenter;

public sealed class MailStatementsPagePresenter
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMailStatementsPageView _view;
    private List<AlarmDefinition> _allAlarms = new();

    public MailStatementsPagePresenter(IServiceScopeFactory scopeFactory, IMailStatementsPageView view)
    {
        _scopeFactory = scopeFactory;
        _view = view;

        _view.Load += async (s, e) => await LoadAlarmsAsync();
        _view.SearchTextChanged += (s, e) => ApplyFilter(e);
        _view.AddNewRequested += (s, e) => HandleAdd();
        _view.EditRequested += (s, e) => HandleEdit(e);
        _view.DeleteRequested += async (s, e) => await HandleDeleteAsync(e);
        _view.ManageUsersRequested += (s, e) => HandleManageUsers(e);
    }

    private async Task LoadAlarmsAsync()
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            _allAlarms = await dbContext.AlarmDefinitions
                .OrderBy(a => a.Name)
                .ToListAsync();
            
            // Get user names for each alarm
            var subscriptions = await dbContext.AlarmUserSubscriptions
                .Include(s => s.AlarmUser)
                .ToListAsync();
            
            var userNamesByAlarm = subscriptions
                .GroupBy(s => s.AlarmDefinitionId)
                .ToDictionary(
                    g => g.Key, 
                    g => string.Join(", ", g.Select(s => s.AlarmUser.FullName).Take(3)) 
                         + (g.Count() > 3 ? $" +{g.Count() - 3}" : ""));
            
            ApplyFilter("", userNamesByAlarm);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Alarmlar yüklenirken hata: {ex.Message}");
        }
    }

    private Dictionary<Guid, string> _userNamesByAlarm = new();

    private void ApplyFilter(string searchText, Dictionary<Guid, string>? userNames = null)
    {
        if (userNames != null) _userNamesByAlarm = userNames;
        
        var text = searchText.Trim().ToLower();
        var filtered = string.IsNullOrEmpty(text)
            ? _allAlarms
            : _allAlarms.Where(a =>
                a.Name.ToLower().Contains(text) ||
                (a.Description?.ToLower().Contains(text) ?? false)).ToList();

        var displayModels = filtered.Select(a => new AlarmDisplayModel
        {
            Id = a.Id,
            Name = a.Name,
            AlarmUsers = _userNamesByAlarm.TryGetValue(a.Id, out var names) ? names : "",
            Description = a.Description ?? "",
            Type = GetTypeTurkish(a.Type),
            Priority = GetPriorityTurkish(a.Priority),
            Status = a.IsActive ? "Aktif" : "Pasif"
        }).ToList();

        _view.DisplayAlarms(displayModels);
    }

    private async void HandleAdd()
    {
        if (_view.ShowEditDialog(null))
        {
            await LoadAlarmsAsync();
        }
    }

    private async void HandleEdit(Guid id)
    {
        if (_view.ShowEditDialog(id))
        {
            await LoadAlarmsAsync();
        }
    }

    private async void HandleManageUsers(Guid id)
    {
        var alarm = _allAlarms.FirstOrDefault(a => a.Id == id);
        if (alarm == null) return;
        
        if (_view.ShowUsersEditDialog(id, alarm.Name))
        {
            await LoadAlarmsAsync();
        }
    }

    private async Task HandleDeleteAsync(Guid id)
    {
        if (!_view.ConfirmDelete()) return;

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            var alarm = await dbContext.AlarmDefinitions.FindAsync(id);
            if (alarm != null)
            {
                dbContext.AlarmDefinitions.Remove(alarm);
                await dbContext.SaveChangesAsync();
                await LoadAlarmsAsync();
                _view.ShowInfo("Alarm silindi.");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Silme hatası: {ex.Message}");
        }
    }

    private static string GetTypeTurkish(AlarmType type)
    {
        return type switch
        {
            AlarmType.Digital => "Dijital",
            AlarmType.Threshold => "Eşik Değer",
            _ => type.ToString()
        };
    }

    private static string GetPriorityTurkish(AlarmPriority priority)
    {
        return priority switch
        {
            AlarmPriority.Low => "Düşük",
            AlarmPriority.Medium => "Orta",
            AlarmPriority.High => "Yüksek",
            AlarmPriority.Critical => "Kritik",
            _ => priority.ToString()
        };
    }
}
