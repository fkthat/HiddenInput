using FkThat.Console;
using FkThat.HiddenInput;
using FkThat.HiddenInput.Samples;

using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

// configure services
services.AddTransient<Application>();
services.AddTransient<IConsoleText, SystemConsole>();
services.AddTransient<IConsoleKeyboard, SystemConsole>();
services.AddTransient<IConsoleHiddenInput, ConsoleHiddenInput>();

// run app
using var sp = services.BuildServiceProvider();
using var scope = sp.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<Application>();
app.Run();
