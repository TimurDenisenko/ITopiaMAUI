using ITopiaMAUI.Models;

namespace ITopiaMAUI.Views;
public partial class DBNovellaScenarioPage : ContentPage
{
    public DBNovellaScenarioPage()
    {
        InitializeComponent();
    }

    private void SaveDBNovellaScenario(object sender, EventArgs e)
    {
        DBNovellaScenario DBNovellaScenario = (DBNovellaScenario)BindingContext;
        if (new string[] { DBNovellaScenario.Name, DBNovellaScenario.Author, DBNovellaScenario.Scenario }.All(x => !string.IsNullOrEmpty(x)))
            App.Database.SaveNovellaScenario(DBNovellaScenario);
        Application.Current.MainPage = new DBNovellaScenarioListPage();
    }

    private void DeleteDBNovellaScenario(object sender, EventArgs e)
    {
        DBNovellaScenario DBNovellaScenario = (DBNovellaScenario)BindingContext;
        App.Database.DeleteNovellaScenario(DBNovellaScenario.ID);
        Application.Current.MainPage = new DBNovellaScenarioListPage();
    }

    private void Cancel(object sender, EventArgs e) =>
        Application.Current.MainPage = new DBNovellaScenarioListPage();
}