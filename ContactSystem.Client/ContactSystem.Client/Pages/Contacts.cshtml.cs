using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactSystem.Client.Pages;

public class ContactsModel : PageModel
{
    private readonly ILogger<ContactsModel> _logger;
    public string OfficeId { get; set; }
    public string OfficeName { get; set; }

    public ContactsModel(ILogger<ContactsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(string officeId, string officeName)
    {
        _logger.LogDebug("Moving to find contacts in Office: {officeName} with Id: {officeId}", officeName, officeId);

        OfficeId = officeId;
        OfficeName = officeName;
    }
}