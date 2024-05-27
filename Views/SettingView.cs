using ITopiaMAUI.Models;

namespace ITopiaMAUI.Views;

public class SettingView : ContentPage
{
	public SettingView()
    {
        StackLayout settingLayout = new StackLayout
        {
            BackgroundColor = Colors.Black,
        };
        Entry name = new Entry
        {
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 400,
            HeightRequest = 60,
            BackgroundColor= Colors.White,
            PlaceholderColor = Colors.Gray,
            Placeholder = "Nimi...",
        };
        ImageButton ok = new ImageButton
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.ok),
            WidthRequest = 25,
            HeightRequest = 25,
            Margin= new Thickness(20, 0, 0, 0),
        };
        ok.Clicked += async (s, e) =>
        {
            if (name.Text==string.Empty || name.Text.All(char.IsAsciiLetter))
            {
                await DisplayAlert("Viga", "Nimi on tühi või sisaldab keelatud tähemärke", "Tühista");
                return;
            }
            NovellaScenario.Name = name.Text;
            await Navigation.PushAsync(new GameView());
        };
        AddRange(settingLayout, new HorizontalStackLayout { Children = { name, ok }, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 140, 0, 0), });
        Content = settingLayout;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }

    public static Frame SettingLayout()
	{
        StackLayout st = new StackLayout
        {
		};
		Label settingLabel = new Label
		{ 
			Text = "Sätted",
            TextColor = Color.FromRgb(255, 159, 104),
            FontSize = 35,
			HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(200,0,0,0)
        };
		Label volumeLabel = new Label
		{
			Text = "Helitugevus",
			TextColor = Color.FromRgb(255, 159, 104),
			FontSize = 25
        };
		Slider volume = new Slider
		{
			Minimum = 0,
			Maximum = 100,
			Value = 75,
			ThumbColor = Colors.White,	
			MinimumTrackColor = Colors.White,
			WidthRequest = 400,
			VerticalOptions = LayoutOptions.Center,
		};
		volume.ValueChanged += (s, e) => GameSetting.Volume = (float)e.NewValue;
        Label textSpeedLabel = new Label
        {
            Text = "Teksti väli-\nmuse kiirus",
            TextColor = Color.FromRgb(255, 159, 104),
            FontSize = 25
        };
        Slider textSpeed = new Slider
        {
            Minimum = 0,
            Maximum = 100,
            Value = 75,
            ThumbColor = Colors.White,
            MinimumTrackColor = Colors.White,
            WidthRequest = 400,
            VerticalOptions = LayoutOptions.Center,
        };
        textSpeed.ValueChanged += (s, e) => GameSetting.TextSpeed = (int)e.NewValue;
        ImageButton btn = new ImageButton
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.exit),
            WidthRequest = 25,
            HeightRequest = 25,
            HorizontalOptions = LayoutOptions.Start,
            Padding = new Thickness(0, 0, 0, 0)
        };
        AddRange(st,
            new HorizontalStackLayout { Children = { btn, settingLabel } },
            new HorizontalStackLayout { Children = { volumeLabel, volume }, Padding = 20 },
			new HorizontalStackLayout { Children = { textSpeedLabel, textSpeed }, Padding = 20 });
		Frame templateFrame = new Frame
        {
            Content = st,
            CornerRadius = 45,
            WidthRequest = 600,
            HeightRequest = 300,
            BackgroundColor = Color.FromRgb(124, 32, 58),
            Opacity = 0.9,
        };
        btn.Clicked += (s, e) => templateFrame.IsVisible = !templateFrame.IsVisible;
        return templateFrame;
	}
    private static void AddRange(StackLayout layout, params IView[] views) => Array.ForEach(views, layout.Add);
}