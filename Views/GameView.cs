using ITopiaMAUI.ViewModels;

namespace ITopiaMAUI.Views;

public class GameView : ContentPage
{
    Frame menuFrame;
	public GameView(SaveViewModel save = null)
    {
        BackgroundImageSource = FileManage.ConvertToImageSource(Properties.Resources.University);
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
        Image character = new Image
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.anna),
        };
        Label dialog = new Label
        {
            HeightRequest = 80,
            WidthRequest = 760,
            FontSize = 15,
            TextColor = Colors.Black,
        };
        if (save!=null)
        {
            dialog.Text = save.Name;
        }
        StackLayout st = new StackLayout { 
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
        Button forward = new Button
        {
            WidthRequest = 400,
            BackgroundColor = Colors.Transparent,
            CornerRadius = 25,
        };
        AddRange(mainLayout,menu, character, st,dialog,back,forward);
        mainLayout.SetLayoutBounds(menu,new Rect(-360, -150, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(dialog, new Rect(10, 135, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(st, new Rect(0, 260, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(back, new Rect(-215, 250, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(forward, new Rect(215, 250, mainLayout.WidthRequest, mainLayout.HeightRequest));
        mainLayout.SetLayoutBounds(character, new Rect(0, 20, mainLayout.WidthRequest, mainLayout.HeightRequest));
        Grid grid = new Grid();
        Content = mainLayout;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }
    private void AddRange(AbsoluteLayout layout, params IView[] views) => Array.ForEach(views, layout.Children.Add);
}