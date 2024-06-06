using ITopiaMAUI.Models;

namespace ITopiaMAUI.Views;
public partial class DBNovellaScenarioPage : ContentPage
{
    public DBNovellaScenarioPage()
    {
        InitializeComponent();
    }

    private void IsLoad_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DBNovellaScenario DBNovellaScenario = (DBNovellaScenario)BindingContext;
        if (IsLoad.IsChecked)
            NovellaScenario.ScenarioID = DBNovellaScenario.ID;
    }

    private void SaveDBNovellaScenario(object sender, EventArgs e)
    {
        DBNovellaScenario DBNovellaScenario = (DBNovellaScenario)BindingContext;
        DBNovellaScenario.Scenario = FileManage.SerializeToFile(DBNovellaScenario.Scenario.Split('\n'));
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
    protected override void OnAppearing()
    {
        DBNovellaScenario DBNovellaScenario = (DBNovellaScenario)BindingContext;
        base.OnAppearing();
        Scenario.Text = string.Join('\n', FileManage.DeserializeFile<string[]>(Scenario.Text ?? "[]"));
        IsLoad.IsChecked = NovellaScenario.ScenarioID == (DBNovellaScenario?.ID ?? -1);
        IsLoad.CheckedChanged += IsLoad_CheckedChanged;
    }
}
    