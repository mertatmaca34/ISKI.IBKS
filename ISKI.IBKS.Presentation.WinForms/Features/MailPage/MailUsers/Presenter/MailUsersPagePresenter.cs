using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Presenter;

public sealed class MailUsersPagePresenter
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMailUsersPageView _view;
    private List<AlarmUser> _allUsers = new();

    public MailUsersPagePresenter(IServiceScopeFactory scopeFactory, IMailUsersPageView view)
    {
        _scopeFactory = scopeFactory;
        _view = view;

        _view.Load += async (s, e) => await LoadUsersAsync();
        _view.SearchTextChanged += (s, e) => ApplyFilter(e);
        _view.AddNewRequested += (s, e) => HandleAdd();
        _view.EditRequested += (s, e) => HandleEdit(e);
        _view.DeleteRequested += async (s, e) => await HandleDeleteAsync(e);
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            _allUsers = await dbContext.AlarmUsers.OrderBy(u => u.FullName).ToListAsync();
            ApplyFilter("");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Kullanıcılar yüklenirken hata: {ex.Message}");
        }
    }

    private void ApplyFilter(string searchText)
    {
        var text = searchText.Trim().ToLower();
        var filtered = string.IsNullOrEmpty(text)
            ? _allUsers
            : _allUsers.Where(u =>
                u.FullName.ToLower().Contains(text) ||
                u.Email.ToLower().Contains(text) ||
                (u.Department?.ToLower().Contains(text) ?? false) ||
                (u.Title?.ToLower().Contains(text) ?? false)).ToList();

        var displayModels = filtered.Select(u => new UserDisplayModel
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber ?? "",
            Department = u.Department ?? "",
            Title = u.Title ?? "",
            Status = u.IsActive ? "Aktif" : "Pasif",
            EmailNotifications = u.ReceiveEmailNotifications ? "Evet" : "Hayır",
            MinPriorityLevel = GetPriorityTurkish(u.MinimumPriorityLevel)
        }).ToList();

        _view.DisplayUsers(displayModels);
    }

    private async void HandleAdd()
    {
        if (_view.ShowEditDialog(null))
        {
            await LoadUsersAsync();
        }
    }

    private async void HandleEdit(Guid id)
    {
        if (_view.ShowEditDialog(id))
        {
            await LoadUsersAsync();
        }
    }

    private async Task HandleDeleteAsync(Guid id)
    {
        if (!_view.ConfirmDelete()) return;

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            var user = await dbContext.AlarmUsers.FindAsync(id);
            if (user != null)
            {
                dbContext.AlarmUsers.Remove(user);
                await dbContext.SaveChangesAsync();
                await LoadUsersAsync();
                _view.ShowInfo("Kullanıcı silindi.");
            }
        }
        catch (Exception ex)
        {
            _view.ShowError($"Silme hatası: {ex.Message}");
        }
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
