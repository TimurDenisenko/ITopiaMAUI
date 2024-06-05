using ITopiaMAUI.Models;
using ITopiaMAUI.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Plugin.Maui.Audio;

namespace ITopiaMAUI.Views;

public class GameView : ContentPage
{
    private Frame menuFrame;
    private readonly Image character;
    private readonly Label dialog, title;
    private readonly StackLayout st;
    private readonly Editor codeEditor;
    private readonly Button forward, back;
    private Script script;
    public GameView(SaveViewModel save = null)
    {
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
        character = new Image();
        dialog = new Label
        {
            HeightRequest = 80,
            WidthRequest = 760,
            FontSize = 15,
            TextColor = Colors.Black,
        };
        codeEditor = new Editor
        {
            HeightRequest = 270,
            WidthRequest = 500,
            FontSize = 15,
            TextColor = Colors.Black,
            IsVisible = false,
            BackgroundColor = Colors.White,
            VerticalTextAlignment = TextAlignment.Start,
        };
        codeEditor.Completed += CodeEditor_Completed;
        st = new StackLayout
        {
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
        back = new Button
        {
            WidthRequest = 430,
            BackgroundColor = Colors.Transparent,
            CornerRadius = 25,
        };
        back.Clicked += (s, e) => GoBack();
        forward = new Button
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
        AddRange(mainLayout, menu, character, st, dialog, back, forward, title, codeEditor);
        mainLayout.SetLayoutBounds(menu, new Rect(-360, -150, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(dialog, new Rect(10, 135, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(st, new Rect(0, 260, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(back, new Rect(-215, 250, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(forward, new Rect(215, 250, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(character, new Rect(0, 20, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(title, new Rect(mainLayout.WidthRequest / 2 - 100, mainLayout.HeightRequest / 2 - 40, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(codeEditor, new Rect(0, -50, mainLayout.WidthRequest, mainLayout.HeightRequest));
        Content = mainLayout;
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
            Setting("Back", save.CurrentBackground);
            Setting("Pers", save.CurrentPers);
        }

        GeneratePage();
    }

    private async void GeneratePage()
    {
        if (new string[] {"Back", "Pers"}.Any(x => NovellaScenario.Scenario[NovellaScenario.PageNum].Split(":")[0]==x))
        {
            string[] changes = NovellaScenario.Scenario[NovellaScenario.PageNum].Replace(" ", "").Trim().Split('|');
            for (int i = 0; i < changes.Length; i++)
            {
                RevSetting(changes[i].Split(":")[0]);
            }
            NovellaScenario.PageNum -= 2;
        }
        Enum.TryParse(NovellaScenario.Scenario[NovellaScenario.PageNum].Trim(), out LineType currentLine);
        title.IsVisible = currentLine == LineType.Empty;
        codeEditor.IsVisible = currentLine == LineType.Code;
        forward.IsEnabled = !(currentLine == LineType.Code);
        Array.ForEach(new VisualElement[] { dialog, st, character }, x => x.IsVisible = !(currentLine == LineType.Empty));
        if (currentLine == LineType.Setting)
        {
            ++NovellaScenario.PageNum;
            string[] changes = NovellaScenario.Scenario[NovellaScenario.PageNum].Replace(" ", "").Trim().Split('|');
            for (int i = 0; i < changes.Length; i++)
            {
                Setting(changes[i].Split(":")[0], changes[i].Split(":")[1]);
            }
            GoForward();
        }
        else if (currentLine == LineType.Empty)
        {
            ++NovellaScenario.PageNum;
            BackgroundColor = Colors.Black;
            BackgroundImageSource = null;
            NovellaScenario.CurrentBackground = "none";
            title.Text = NovellaScenario.Scenario[NovellaScenario.PageNum];
        }
        else if (currentLine == LineType.Code)
        {
            NovellaScenario.PageNum++;
            codeEditor.Text = NovellaScenario.Scenario[NovellaScenario.PageNum];
        }
        else if (NovellaScenario.Scenario[NovellaScenario.PageNum].Split("-").Length != 2 && NovellaScenario.PageNum != 0)
        {
            GoBack();
        }
        else if (NovellaScenario.Scenario[NovellaScenario.PageNum].Split('|')[0].Split(':')[0].Trim() == "Test")
        {
            NovellaScenario.PageNum = NovellaScenario.TestNum - 3;
            GoBack();
        }
        else
        {
            dialog.Text = string.Empty;
            if (NovellaScenario.Scenario[NovellaScenario.PageNum].Contains("[player]"))
                NovellaScenario.Scenario[NovellaScenario.PageNum] = NovellaScenario.Scenario[NovellaScenario.PageNum].Replace("[player]", NovellaScenario.Name);
            forward.IsEnabled = false;
            back.IsEnabled = false;
            foreach (char item in NovellaScenario.Scenario[NovellaScenario.PageNum])
            {
                dialog.Text += item;
                await Task.Delay(NovellaScenario.TextSpeed);
            }
            forward.IsEnabled = true;
            back.IsEnabled = true;
        }
    }
    private async void CodeEditor_Completed(object sender, EventArgs e)
    {
        script = CSharpScript.Create(codeEditor.Text);
        try
        {
            ScriptState result = await script.RunAsync();
            await DisplayAlert("Edu", "Kood koostatud. Tagastusväärtus: " + result?.ReturnValue ?? "null", "Tühista");
            NovellaScenario.TestNum = NovellaScenario.PageNum + 1;
            Test();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", ex.Message, "Tühista");
        }
    }
    private async void Test(string test = null, string wait = null)
    {
        if (test==null || wait==null)
        {
            NovellaScenario.PageNum++;
            if (NovellaScenario.Scenario[NovellaScenario.PageNum].Split('|')[0].Split(':')[0].Trim() == "Test")
                Test(NovellaScenario.Scenario[NovellaScenario.PageNum].Split('|')[0].Split(':')[1].Trim(),
                    NovellaScenario.Scenario[NovellaScenario.PageNum].Split('|')[1].Split(':')[1].Trim());
            else
            {
                await DisplayAlert("Edu", "Kõik testid läbisid edukalt", "Tühista");
                GeneratePage();
            }
            return;
        }
        try
        {
            ScriptState result = await script.ContinueWith(test).RunAsync();
            if (result.ReturnValue.ToString() != wait)
            {
                await DisplayAlert("Ebaõnnestumine", "Eeldatav: " + wait + " Tagastatud: " + result.ReturnValue, "Tühista");
                NovellaScenario.PageNum = NovellaScenario.TestNum - 1;
                return;
            }
            Test();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", ex.Message, "Tühista");
        }
    }
    private void Setting(string setting, string value)
    {
        switch (setting)
        {
            case "Back":
                NovellaScenario.RevBackground = NovellaScenario.CurrentBackground ??= "None";
                NovellaScenario.CurrentBackground = value;
                BackgroundImageSource = FileManage.ConvertToImageSource(value);
                return;
            case "Pers":
                NovellaScenario.RevPers = NovellaScenario.CurrentPers ??= "None";
                NovellaScenario.CurrentPers = value;
                character.Source = FileManage.ConvertToImageSource(value);
                return;
            default:
                return;
        }
    }
    private void RevSetting(string setting)
    {
        switch (setting)
        {
            case "Back":
                NovellaScenario.CurrentBackground = NovellaScenario.RevBackground ??= "None";
                BackgroundImageSource = FileManage.ConvertToImageSource(NovellaScenario.RevBackground);
                return;
            case "Pers":
                NovellaScenario.CurrentPers = NovellaScenario.RevPers ??= "None";
                character.Source = FileManage.ConvertToImageSource(NovellaScenario.RevPers);
                return;
            default:
                return;
        }
    }
    private void GoBack()
    {
        if (NovellaScenario.Scenario[NovellaScenario.PageNum+1].Split('|')[0].Split(':')[0].Trim() == "Test")
            NovellaScenario.PageNum-=2;
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