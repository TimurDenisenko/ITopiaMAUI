using ITopiaMAUI.Models;

namespace ITopiaMAUI.Views;

public partial class MainFormView : ContentPage
{
    AbsoluteLayout layout;
    Frame settingLayout;
    public MainFormView()
    {
        BackgroundImageSource = FileManage.ConvertToImageSource(Properties.Resources.University);
        layout = new AbsoluteLayout();
        Label title1 = new Label { Text = "IT", TextColor = Colors.Black, FontSize = 60 };
        Label title2 = new Label { Text = "opia", TextColor = Colors.Orange, FontSize = 60 };
        AddRange(title1, title2);
        Button new_game = new Button { Text = "Uus mäng" };
        new_game.Clicked += (s, e) => Application.Current.MainPage = new SettingView(); 
        Button continue_game = new Button { Text = "Jätka" };
        Button setting = new Button { Text = "Sätted" };
        Button exit = new Button { Text = "Välju" };
        exit.Clicked += (s, e) => Application.Current.Quit();
        setting.Clicked += (s, e) =>
        {
            settingLayout = SettingView.SettingFrame();
            layout.Children.Add(settingLayout);
            layout.SetLayoutBounds(settingLayout, new Rect(15, 4, layout.Width, layout.Height));
        };
        continue_game.Clicked += (s, e) =>
        {
            settingLayout = SettingView.SavingFrame();
            layout.Children.Add(settingLayout);
            layout.SetLayoutBounds(settingLayout, new Rect(-17, 50, layout.Width, layout.Height));
        };
        foreach (Button btn in new Button[] { new_game, continue_game, setting, exit })
        {
            btn.Opacity = 0.9;
            btn.BackgroundColor = Color.FromRgb(124, 32, 58);
            btn.TextColor = Color.FromRgb(255, 159, 104);
            btn.MinimumWidthRequest = 150;
            btn.MinimumHeightRequest = 50;
            btn.FontSize = 25;
            btn.CornerRadius = 20;
            layout.Children.Add(btn);
        }
        layout.SetLayoutBounds(title1, new Rect(330, 40, layout.Width, layout.Height));
        layout.SetLayoutBounds(title2, new Rect(370, 40, layout.Width, layout.Height));
        layout.SetLayoutBounds(new_game, new Rect(230, 160, layout.Width, layout.Height));
        layout.SetLayoutBounds(continue_game, new Rect(430, 160, layout.Width, layout.Height));
        layout.SetLayoutBounds(setting, new Rect(230, 260, layout.Width, layout.Height));
        layout.SetLayoutBounds(exit, new Rect(430, 260, layout.Width, layout.Height));
        Content = layout;
    }
    private void AddRange(params IView[] views) => Array.ForEach(views, layout.Add);
}