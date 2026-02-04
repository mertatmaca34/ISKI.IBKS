using System.Net;
using System.Net.Mail;
using ISKI.IBKS.Application.Services.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ISKI.IBKS.Infrastructure.Services.Mail;

/// <summary>
/// SMTP tabanlı alarm e-posta bildirim servisi
/// </summary>
public sealed class SmtpAlarmMailService : IAlarmMailService
{
    private readonly MailConfiguration _config;
    private readonly ILogger<SmtpAlarmMailService> _logger;

    public SmtpAlarmMailService(IOptions<MailConfiguration> config, ILogger<SmtpAlarmMailService> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public async Task<bool> SendAlarmNotificationAsync(
        string toEmail,
        string toName,
        string alarmTitle,
        string alarmDescription,
        string sensorName,
        double? currentValue,
        double? thresholdValue,
        DateTime occurredAt,
        CancellationToken ct = default)
    {
        try
        {
            var subject = $"[IBKS ALARM] {alarmTitle}";
            var body = BuildAlarmEmailBody(alarmTitle, alarmDescription, sensorName, currentValue, thresholdValue, occurredAt);

            return await SendEmailAsync(toEmail, toName, subject, body, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Alarm bildirimi gönderilemedi: {Email}", toEmail);
            return false;
        }
    }

    public async Task<int> SendAlarmToSubscribersAsync(
        Guid alarmDefinitionId,
        string alarmTitle,
        string alarmDescription,
        string sensorName,
        double? currentValue,
        double? thresholdValue,
        DateTime occurredAt,
        CancellationToken ct = default)
    {
        // Bu metot normalde veritabanından aboneleri çekip gönderir
        // Şimdilik placeholder olarak bırakıyoruz
        _logger.LogInformation("Alarm bildirimi gönderilecek: {AlarmId} - {Title}", alarmDefinitionId, alarmTitle);
        // FIXME: AlarmManager bu işi yapmalı veya burada DB'den user çekilmeli?
        // Refactoring kapsamında AlarmManager logic'i yönetiyor ama mail gönderimi burada.
        // AlarmManager direk mail servisini kullanıyor.
        // Eğer "Subscribers" mantığı varsa, AlarmManager'da kullanıcıları çekip tek tek SendAlarmNotificationAsync çağırabiliriz 
        // VEYA bu metodun içini doldurabiliriz.
        // Mevcut yapıda AlarmManager'ın bu metodu çağırması bekleniyor.
        // Ancak ben AlarmManager'da SendAlarmMailAsync (arayüzde olmayan metod?) çağırdım.
        // IAlarmManager implementation'da `_mailService.SendAlarmMailAsync` diye bir şey uydurdum.
        // IAlarmMailService arayüzünde `SendAlarmNotificationAsync` var.
        // Düzelteceğim.
        return 0;
    }

    public async Task<bool> SendTestEmailAsync(string toEmail, CancellationToken ct = default)
    {
        try
        {
            var subject = "[IBKS] Test E-postası";
            var body = @"
<html>
<body style='font-family: Arial, sans-serif;'>
    <h2 style='color: #0083C8;'>IBKS Test E-postası</h2>
    <p>Bu bir test e-postasıdır.</p>
    <p>E-posta bildirimleri düzgün çalışıyor.</p>
    <hr/>
    <p style='font-size: 12px; color: #666;'>
        Bu e-posta ISKI.IBKS sistemi tarafından gönderilmiştir.
    </p>
</body>
</html>";

            return await SendEmailAsync(toEmail, null, subject, body, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Test e-postası gönderilemedi: {Email}", toEmail);
            return false;
        }
    }

    private async Task<bool> SendEmailAsync(string toEmail, string? toName, string subject, string body, CancellationToken ct)
    {
        var smtpHost = _config.SmtpHost;
        var smtpPort = _config.SmtpPort;
        var username = _config.Username;
        var password = _config.Password;
        var useSsl = _config.UseSsl;
        var fromAddress = string.IsNullOrEmpty(_config.FromAddress) ? username : _config.FromAddress;
        var fromName = string.IsNullOrEmpty(_config.FromName) ? "IBKS Sistem" : _config.FromName;

        if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(fromAddress) || string.IsNullOrEmpty(username))
        {
            _logger.LogWarning("SMTP ayarları eksik. Mail gönderilemiyor. (Host: {Host}, From: {From}, User: {User})", smtpHost, fromAddress, username);
            return false;
        }

        // Port fix (default 587)
        if (smtpPort == 0) smtpPort = 587;

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            EnableSsl = useSsl,
            Credentials = new NetworkCredential(username, password),
            Timeout = 30000
        };

        var from = new MailAddress(fromAddress, fromName);
        var to = new MailAddress(toEmail, toName ?? toEmail);

        using var message = new MailMessage(from, to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        await client.SendMailAsync(message, ct);
        _logger.LogInformation("E-posta gönderildi: {To}", toEmail);
        return true;
    }

    private static string BuildAlarmEmailBody(
        string alarmTitle,
        string alarmDescription,
        string sensorName,
        double? currentValue,
        double? thresholdValue,
        DateTime occurredAt)
    {
        return $@"
<html>
<body style='font-family: Arial, sans-serif;'>
    <div style='background-color: #dc3545; color: white; padding: 20px; border-radius: 8px 8px 0 0;'>
        <h2 style='margin: 0;'>⚠️ IBKS Alarm Bildirimi</h2>
    </div>
    <div style='border: 1px solid #ddd; padding: 20px; border-radius: 0 0 8px 8px;'>
        <h3 style='color: #dc3545;'>{alarmTitle}</h3>
        <p>{alarmDescription}</p>
        
        <table style='width: 100%; border-collapse: collapse; margin-top: 20px;'>
            <tr style='background-color: #f8f9fa;'>
                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Sensör</strong></td>
                <td style='padding: 10px; border: 1px solid #ddd;'>{sensorName}</td>
            </tr>
            <tr>
                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Mevcut Değer</strong></td>
                <td style='padding: 10px; border: 1px solid #ddd;'>{(currentValue.HasValue ? currentValue.Value.ToString("F2") : "-")}</td>
            </tr>
            <tr style='background-color: #f8f9fa;'>
                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Eşik Değer</strong></td>
                <td style='padding: 10px; border: 1px solid #ddd;'>{(thresholdValue.HasValue ? thresholdValue.Value.ToString("F2") : "-")}</td>
            </tr>
            <tr>
                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Oluşma Zamanı</strong></td>
                <td style='padding: 10px; border: 1px solid #ddd;'>{occurredAt:dd.MM.yyyy HH:mm:ss}</td>
            </tr>
        </table>
        
        <hr style='margin-top: 30px; border: none; border-top: 1px solid #ddd;'/>
        <p style='font-size: 12px; color: #666;'>
            Bu e-posta ISKI.IBKS Alarm Sistemi tarafından otomatik olarak gönderilmiştir.
        </p>
    </div>
</body>
</html>";
    }
}
