using ITopiaMAUI.Models;

namespace ITopiaMAUI.Views;

public partial class DBNovellaScenarioListPage : ContentPage
{
    public DBNovellaScenarioListPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        scenarioList.ItemsSource = App.Database.GetNovellaScenarios().Where(x => x.ID != 0);
        base.OnAppearing();
    }
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        DBNovellaScenario selectedNovellaScenario = e.SelectedItem as DBNovellaScenario;
        DBNovellaScenarioPage NovellaScenarioPage = new DBNovellaScenarioPage();
        NovellaScenarioPage.BindingContext= selectedNovellaScenario;
        Application.Current.MainPage = NovellaScenarioPage;
    }

    private void CreateScenario(object sender, EventArgs e)
    {
        DBNovellaScenario NovellaScenario = new DBNovellaScenario();
        DBNovellaScenarioPage NovellaScenarioPage = new DBNovellaScenarioPage();
        NovellaScenarioPage.BindingContext= NovellaScenario;
        Application.Current.MainPage = NovellaScenarioPage;
    }

    private void Button_Clicked(object sender, EventArgs e) =>
        Application.Current.MainPage = new MainFormView();
}