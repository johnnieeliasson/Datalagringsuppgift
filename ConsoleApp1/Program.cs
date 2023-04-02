using ConsoleApp1.Services;

StatusService statusService = new();
MenuService menuService = new();
UserService userService = new();

await statusService.InitializeAsync();
var currentUser = await userService.GetAsync(x => x.Email == "sarah@gmail.com");
if (currentUser == null)
    currentUser = await menuService.CreateUserAsync();

while (true)
{
    
    await menuService.MainMenu(currentUser.Id);
    Console.ReadKey();
}