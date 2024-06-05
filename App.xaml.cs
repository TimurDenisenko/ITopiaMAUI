﻿using ITopiaMAUI.Models;
using ITopiaMAUI.Views;

namespace ITopiaMAUI;

public partial class App : Application
{
    public const string DATABASE_NAME = "itopia.db";
    public static Repository database;
    public static Repository Database
    {
        get
        {
            database ??= new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            return database;
        }
    }
    public App()
    {
        Database.SaveNovellaScenario(new DBNovellaScenario
        {
            Author = "Timur Denisenko",
            Name = "ITopia",
            Scenario = FileManage.SerializeToFile(ITopiaMAUI.Properties.Resources.scenario.Split('\n'))
        });
        MainPage = new Shell { CurrentItem = new MainFormView()};
	}
}
