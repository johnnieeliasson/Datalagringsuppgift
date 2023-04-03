

using ConsoleApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace ConsoleApp1.Services;

internal class MenuService
{
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();
    private readonly StatusService _statusService = new();

    public async Task<UserEntity> CreateUserAsync()
    {
        var _entity = new UserEntity();
        Console.Clear();
        Console.WriteLine("############# Ny Användare ############");
        Console.Write(" Ange förnamn: ");
        _entity.FirstName = Console.ReadLine() ?? "";
        Console.Write(" Ange efternamnn: ");
        _entity.LastName = Console.ReadLine() ?? "";
        Console.Write(" Ange epostadress: ");
        _entity.Email = Console.ReadLine() ?? "";

        return await _userService.CreateAsync(_entity);

    }

    public async Task MainMenu(int userId)
    {
        Console.Clear();
        Console.WriteLine("############# Huvudmenu ############");
        Console.WriteLine("1. Visa alla aktiva ärenden");
        Console.WriteLine("2. Visa alla ärenden");
        Console.WriteLine("3. Visa specifikt ärende");
        Console.WriteLine("4. Ändra ärendestatus");
        Console.WriteLine("5. Skapa nytt ärende");
        Console.WriteLine("6. Se alla användare");
        Console.Write("Välj ett av ovanstående alternativ: ");
        var option = Console.ReadLine();

        switch(option)
        {
            case "1":
                await ActiveCasesAsync();
                break;

            case "2":
                await AllCasesAsync();
                break;

            case "3":
                await SpecificCaseAsync();
                break;

            case "4":
                await UpdateStatusAsync();
                break;

            case "5":
                await NewCaseAsync(userId);
                break;

            case "6":
                await HandlersAsync(); 
                break;

            default:
                break;

        }
    }

    private async Task ActiveCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("########### Aktiva Ärenden ##########");
        foreach(var _case in await _caseService.GetAllActiveAsync())
        {
            Console.WriteLine($"Ärende ID: {_case.CaseId}");
            Console.WriteLine($"Skapad: {_case.Created}");
            Console.WriteLine($"Uppdaterad: {_case.Updated}");
            Console.WriteLine($"Status: {_case.Status.StatusName}");
            Console.WriteLine($"Beskrivning: {_case.Description}");
            Console.WriteLine("");
        }
    }

    private async Task AllCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("########### Alla Ärenden ##########");
        foreach (var _case in await _caseService.GetAllAsync())
        {
            Console.WriteLine($"Ärende ID: {_case.CaseId}");
            Console.WriteLine($"Skapad: {_case.Created}");
            Console.WriteLine($"Uppdaterad: {_case.Updated}");
            Console.WriteLine($"Status: {_case.Status.StatusName}");
            Console.WriteLine($"Beskrivning: {_case.Description}");
            Console.WriteLine("");
        }
    }

    public async Task SpecificCaseAsync()
    {
        Console.Write("Ange ärendenummer: ");
        var caseId = int.Parse(Console.ReadLine());
        var _case = await _caseService.GetAsync(caseId);
        if (_case != null)

        {

            Console.Clear();
            Console.WriteLine("########### Valt ärende ##########");
            Console.WriteLine($"Ärende ID: {_case.CaseId}");
            Console.WriteLine($"Skapad: {_case.Created}");
            Console.WriteLine($"Uppdaterad: {_case.Updated}");
            Console.WriteLine($"Status: {_case.Status.StatusName}");
            Console.WriteLine($"Beskrivning: {_case.Description}");
            Console.WriteLine("");
 
        }
              
              
    }

    public async Task UpdateStatusAsync()
    {
        Console.Write("Ange ärendenummer:  ");
        var caseId = int.Parse(Console.ReadLine());
        var _case = await _caseService.GetAsync(caseId);
        if (_case != null)
        {
            Console.WriteLine("Ange status: ");
            Console.WriteLine("1 - Ej påbörjad");
            Console.WriteLine("2 - Påbörjad");
            Console.WriteLine("3 - Avslutad");
            _case.StatusId = int.Parse(Console.ReadLine());

            await _caseService.UpdateStatusAsync(_case);
            Console.WriteLine("Status uppdaterad");

        }
    }

        



    private async Task HandlersAsync()
    {
        Console.Clear();
        Console.WriteLine("########### Användare ##########");
        foreach (var _user in await _userService.GetAllAsync())
        {
            Console.WriteLine($"Användar-ID: {_user.Id}");
            Console.WriteLine($"Namn: {_user.FirstName} {_user.LastName}");

            Console.WriteLine($"E-postadress: {_user.Email}");
            Console.WriteLine("");
        }
    }

    private async Task NewCaseAsync(int userId)
    {
        var _entity = new CaseEntity { UserId = userId };
        Console.Clear();
        Console.WriteLine("############# Nytt Ärende ############");
        Console.Write(" Ange kundens namn: ");
        _entity.UserName = Console.ReadLine() ?? "";
        Console.Write(" Ange kundens epostadress: ");
        _entity.UserEmail = Console.ReadLine() ?? "";
        Console.Write(" Ange kundens telefonnummer: ");
        _entity.UserPhoneNumber = Console.ReadLine() ?? "";
        Console.Write(" Beskriv ärendet: ");
        _entity.Description = Console.ReadLine() ?? "";

        await _caseService.CreateAsync(_entity);
        await ActiveCasesAsync();
    }


}
