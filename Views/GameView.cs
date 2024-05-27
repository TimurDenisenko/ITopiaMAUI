using ITopiaMAUI.Models;
using Microsoft.Maui.Controls;
using System.Reflection;

namespace ITopiaMAUI.Views;

public class GameView : ContentPage
{
	public GameView()
	{
        StackLayout mainLayout = new StackLayout
        {
        };
        ImageButton menu = new ImageButton
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.menu),
            WidthRequest = 25, 
            HeightRequest = 25,
        };
        Image character = new Image
        {

        };
        Label dialog = new Label
        {
        };
        Frame dialogFrame = new Frame 
        {
            CornerRadius = 25,
            Opacity = 0.9,
            BackgroundColor = Colors.Gray,
            BorderColor = Colors.Black,
            WidthRequest = mainLayout.WidthRequest,
            HeightRequest = 100,
        };
        AddRange(mainLayout,menu);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }
    private void AddRange(StackLayout layout, params IView[] views) => Array.ForEach(views, layout.Add);
}