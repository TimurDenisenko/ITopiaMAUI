using ITopiaMAUI.Models;
using ITopiaMAUI.ViewModels;

namespace ITopiaMAUI.Views;

public class GameView : ContentPage
{
    Frame menuFrame;
    Image character;
    Label dialog, title;
    StackLayout st;
    public GameView(SaveViewModel save = null)
    {
        if (save == null)
        {
            DBNovellaScenario ns = App.Database.GetNovellaScenarios().ToArray()[0];
            NovellaScenario.PageNum = 0;
            NovellaScenario.Scenario = FileManage.DeserializeFile<string[]>(ns.Scenario);
        }
        else
        {
            NovellaScenario.PageNum = save.PageNum;
            NovellaScenario.Scenario = FileManage.DeserializeFile<string[]>(save.Scenario);
        }
        AbsoluteLayout mainLayout = new AbsoluteLayout
        {
            WidthRequest = 860,
            HeightRequest = 350,
        };
        ImageButton menu = new ImageButton
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.menu),
            WidthRequest = 40, 
            HeightRequest = 40,
        };
        menu.Clicked += (s, e) =>
        {
            if (menuFrame == null || !menuFrame.IsVisible)
            {
                menuFrame = SettingView.MenuFrame();
                mainLayout.Children.Add(menuFrame);
                mainLayout.SetLayoutBounds(menuFrame, new Rect(15, 4, mainLayout.WidthRequest, mainLayout.HeightRequest));
                return;
            }
            menuFrame.IsVisible = false;
        };
        character = new Image
        {
        };
        dialog = new Label
        {
            HeightRequest = 80,
            WidthRequest = 760,
            FontSize = 15,
            TextColor = Colors.Black,
        };
        st = new StackLayout { 
            Children = 
            {
                new Frame
                {
                CornerRadius = 25,
                BorderColor = Colors.Black,
                BackgroundColor = Colors.LightGray,
                WidthRequest = 780,
                HeightRequest = 80,
                }
            },
            Opacity = 0.7
        };
        Button back = new Button
        {
            WidthRequest = 430,
            BackgroundColor = Colors.Transparent,
            CornerRadius = 25,
        };
        back.Clicked += (s, e) => GoBack();
        Button forward = new Button
        {
            WidthRequest = 400,
            BackgroundColor = Colors.Transparent,
            CornerRadius = 25,
        };
        forward.Clicked += (s, e) => GoForward();
        title = new Label
        {
            FontSize = 60,
            TextColor = Colors.White,
        };
        AddRange(mainLayout,menu, character, st,dialog,back,forward, title);
        mainLayout.SetLayoutBounds(menu,new Rect(-360, -150, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(dialog, new Rect(10, 135, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(st, new Rect(0, 260, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(back, new Rect(-215, 250, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(forward, new Rect(215, 250, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(character, new Rect(0, 20, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(title, new Rect(mainLayout.WidthRequest/2-100, mainLayout.HeightRequest / 2 - 40, mainLayout.WidthRequest, mainLayout.HeightRequest));
        Content = mainLayout;
        GeneratePage();
    }
    private async void GeneratePage()
    {
        Enum.TryParse(NovellaScenario.Scenario[NovellaScenario.PageNum].Trim(), out LineType currentLine);
        title.IsVisible = currentLine == LineType.Empty;
        Array.ForEach(new VisualElement[] { dialog, st, character }, x => x.IsVisible = !(currentLine == LineType.Empty));
        if (currentLine == LineType.Setting)
        {
            ++NovellaScenario.PageNum;
            string[] changes = NovellaScenario.Scenario[NovellaScenario.PageNum].Replace(" ", "").Trim().Split('|');
            for (int i = 0; i < changes.Length; i++)
            {
                Setting(changes[i].Split(":")[0], changes[i].Split(":")[1]);
            }
            ++NovellaScenario.PageNum;
            GoForward();
        }
        else if (currentLine == LineType.Empty)
        {
            ++NovellaScenario.PageNum;
            BackgroundColor = Colors.Black;
            BackgroundImageSource = null;
            NovellaScenario.CurrentLocation = "none";updat
            title.Text = NovellaScenario.Scenario[NovellaScenario.PageNum];
        }
        else
        {
            dialog.Text = string.Empty;
            if (NovellaScenario.Scenario[NovellaScenario.PageNum].Contains("[player]"))
                NovellaScenario.Scenario[NovellaScenario.PageNum] = NovellaScenario.Scenario[NovellaScenario.PageNum].Replace("[player]", NovellaScenario.Name);
            foreach (char item in NovellaScenario.Scenario[NovellaScenario.PageNum])
            {
                dialog.Text += item;
                await Task.Delay(10);
            }
        }
    }
    private void Setting(string setting, string value)
    {
        switch (setting)
        {
            case "Back":
                BackgroundImageSource = FileManage.ConvertToImageSource(Properties.Resources.ResourceManager.GetObject(value) as byte[]);
                NovellaScenario.CurrentLocation = value;
                break;
            case "Pers":
                character.Source = FileManage.ConvertToImageSource(Properties.Resources.ResourceManager.GetObject(value) as byte[]);
                break;
            default:
                break;
        }
    }
    private void GoBack()
    {
        --NovellaScenario.PageNum;
        GeneratePage();
    }
    private void GoForward()
    {
        ++NovellaScenario.PageNum;
        GeneratePage();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }
    private void AddRange(AbsoluteLayout layout, params IView[] views) => Array.ForEach(views, layout.Children.Add);
}