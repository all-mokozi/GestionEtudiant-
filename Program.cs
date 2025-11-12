using GestionEtudiant.Repository;
using GestionEtudiant.Repository.Impl;
using GestionEtudiant.Services;
using GestionEtudiant.Services.Impl;
using GestionEtudiant.Views;
using Microsoft.Extensions.DependencyInjection;

var ioc = new ServiceCollection();
ioc.AddSingleton<IEtudiantRepository, EtudiantRepository>();
ioc.AddSingleton<INoteRepository, NoteRepository>();
ioc.AddSingleton<IEtudiantService, EtudiantService>();
ioc.AddSingleton<INoteService, NoteService>();
ioc.AddTransient<ConsoleView>();
var services = ioc.BuildServiceProvider();
var view = services.GetRequiredService<ConsoleView>();
view.Run();

