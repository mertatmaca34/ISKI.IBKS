using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Login;

public sealed class LoginResponse
{
    [JsonPropertyName("TicketId")]
    public string TicketId { get; init; } = string.Empty;

    [JsonPropertyName("DeviceId")]
    public string? DeviceId { get; init; }

    [JsonPropertyName("User")]
    public SaisUserResponse? User { get; init; }
}

public sealed record SaisUserResponse
{
    [JsonPropertyName("idcode")]
    public object? IdCode { get; init; }

    [JsonPropertyName("type")]
    public int Type { get; init; }

    [JsonPropertyName("loginname")]
    public string? LoginName { get; init; }

    [JsonPropertyName("firstname")]
    public string? FirstName { get; init; }

    [JsonPropertyName("lastname")]
    public string? LastName { get; init; }

    [JsonPropertyName("birthday")]
    public DateTime? Birthday { get; init; }

    [JsonPropertyName("password")]
    public string? Password { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("cellphone")]
    public string? CellPhone { get; init; }

    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("status")]
    public bool Status { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("town")]
    public string? Town { get; init; }

    [JsonPropertyName("tckimlikno")]
    public string? TcKimlikNo { get; init; }

    [JsonPropertyName("DutyCity")]
    public object? DutyCity { get; init; }

    [JsonPropertyName("ExpireDate")]
    public DateTime? ExpireDate { get; init; }

    [JsonPropertyName("City_Title")]
    public string? CityTitle { get; init; }

    [JsonPropertyName("Town_Title")]
    public string? TownTitle { get; init; }

    [JsonPropertyName("DutyCity_Title")]
    public object? DutyCityTitle { get; init; }

    [JsonPropertyName("createdby_Title")]
    public string? CreatedByTitle { get; init; }

    [JsonPropertyName("changedby_Title")]
    public string? ChangedByTitle { get; init; }

    [JsonPropertyName("CompanyId")]
    public object? CompanyId { get; init; }

    [JsonPropertyName("Company_Title")]
    public object? CompanyTitle { get; init; }

    [JsonPropertyName("FullName")]
    public string? FullName { get; init; }

    [JsonPropertyName("Status_Title")]
    public string? StatusTitle { get; init; }

    [JsonPropertyName("Type_Title")]
    public string? TypeTitle { get; init; }

    [JsonPropertyName("PositionName_Title")]
    public object? PositionNameTitle { get; init; }

    [JsonPropertyName("Roles_Title")]
    public string? RolesTitle { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("created")]
    public DateTime? Created { get; init; }

    [JsonPropertyName("changed")]
    public DateTime? Changed { get; init; }

    [JsonPropertyName("changedby")]
    public string? ChangedBy { get; init; }

    [JsonPropertyName("createdby")]
    public string? CreatedBy { get; init; }
}
