namespace Budget.Web.Models;

/// <summary>
/// Represents the model used to display error information in the Presentation layer.
/// </summary>
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
