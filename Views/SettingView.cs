using ITopiaMAUI.Models;
using ITopiaMAUI.ViewModels;
namespace ITopiaMAUI.Views;

public class SettingView : ContentPage
{
    private static TaskCompletionSource<bool> tcs;
    private static DBSaveModel selectedItem;
    private readonly Entry name;
    private readonly Button load, delete, cancel, save, exit;
    private readonly ListView saves;
    public SettingView()
    {
        StackLayout settingLayout = new StackLayout
        {
            BackgroundColor = Colors.Black,
        };
        name = new Entry
        {
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 400,
            HeightRequest = 60,
            BackgroundColor= Colors.White,
            PlaceholderColor = Colors.Gray,
            Placeholder = "Nimi...",
        };
        name.Completed += (s, e) => StartGame();
        ImageButton ok = new ImageButton
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.ok),
            WidthRequest = 25,
            HeightRequest = 25,
            Margin= new Thickness(20, 0, 0, 0),
        };
        ok.Clicked += (s, e) => StartGame();
        AddRange(settingLayout, new HorizontalStackLayout { Children = { name, ok }, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 140, 0, 0), });
        Content = settingLayout;
    }
    private async void StartGame()
    {
        if (name.Text==string.Empty || !name.Text.All(char.IsAsciiLetter))
        {
            await DisplayAlert("Viga", "Nimi on tühi või sisaldab keelatud tähemärke", "Tühista");
            return;
        }
        NovellaScenario.Name = name.Text;
        Application.Current.MainPage = new GameView();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }

    public static Frame SettingFrame()
	{
        tcs = new TaskCompletionSource<bool>();
        StackLayout st = new StackLayout();
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
			Value = GameSetting.Volume,
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
            Maximum = 50,
            Value = GameSetting.TextSpeed,
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
        };
        btn.Clicked += (s, e) =>
        {
            templateFrame.IsVisible = false;
            tcs.SetResult(true);
        };
        return templateFrame;
	}
    public static Frame MenuFrame()
    {
        StackLayout st = new StackLayout();
        Frame templateFrame = new Frame
        {
            Content = st,
            CornerRadius = 45,
            WidthRequest = 400,
            HeightRequest = 200,
            BackgroundColor = Color.FromRgb(124, 32, 58),
        };
        Button setting = new Button
        {
            Text = "Sätted"
        };
        setting.Clicked += async (s, e) =>
        {
            templateFrame.WidthRequest = 600;
            templateFrame.HeightRequest = 300;
            templateFrame.Content = SettingFrame();
            await tcs.Task;
            templateFrame.IsVisible = false;
        };
        Button saveLoad = new Button
        {
            Text = "Salvesta juhtimine"
        };
        saveLoad.Clicked += async (s, e) =>
        {
            templateFrame.Content = SavingFrame();
            await tcs.Task;
            templateFrame.IsVisible = false;
        };
        Button exit = new Button
        {
            Text = "Välja"
        };
        exit.Clicked += (s,e) => Application.Current.MainPage = new MainFormView();
        Array.ForEach(new Button[] { setting, saveLoad, exit }, x =>
            {
                x.WidthRequest = 500;
                x.BackgroundColor = Colors.Transparent;
                x.TextColor = Color.FromRgb(255, 159, 104);
                x.FontSize = 25;
            }
        );
        AddRange(st,setting, saveLoad, exit );
        return templateFrame;
    }
    public static Frame SavingFrame()
    {
        tcs = new TaskCompletionSource<bool>();
        StackLayout st = new StackLayout();
        Frame templateFrame = new Frame
        {
            Content = st,
            CornerRadius = 45,
            WidthRequest = 400,
            HeightRequest = 200,
            BackgroundColor = Color.FromRgb(124, 32, 58),
        };
        ImageButton exit = new ImageButton
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.exit),
            WidthRequest = 25,
            HeightRequest = 25,
            HorizontalOptions = LayoutOptions.Start,
        };
        Button load = new Button
        {
            Text = "Laadi",
        };
        Button delete = new Button
        {
            Text = "Kustuta",
        };
        Button cancel = new Button
        {
            Text = "Tühista",
        };
        exit.Clicked += (s, e) =>
        {
            templateFrame.IsVisible = false;
            tcs.SetResult(true);
        };
        ListView saves = new ListView
        {
            ItemsSource = App.Database.GetSaves(),
            ItemTemplate = new DataTemplate(() =>
            {
                Label name = new Label { TextColor = Colors.White };
                Label back = new Label { TextColor = Colors.White };
                Label num = new Label { TextColor = Colors.White };
                name.SetBinding(Label.TextProperty, "Name");
                back.SetBinding(Label.TextProperty, "CurrentBackground");
                num.SetBinding(Label.TextProperty, "PageNum");
                return new ViewCell
                {
                    View = new HorizontalStackLayout
                    {
                        Children = { 
                            new Label { Text = "Nimi: ",TextColor = Color.FromRgb(255, 159, 104) }, name,
                            new Label { Text = " Taust: ", TextColor = Color.FromRgb(255, 159, 104) }, back,
                            new Label { Text = " Lehekülje number: ", TextColor = Color.FromRgb(255, 159, 104) }, num
                        },
                        HeightRequest = 25,
                    }
                };
            }),
            WidthRequest = 300,
            HeightRequest = 150,
            Margin = new Thickness(30,0,0,0),
        };
        Button save = new Button
        {
            Text = "Salvesta",
            BorderColor = Colors.Black,
        };
        foreach (Button item in new Button[] { load, delete, cancel, save })
        {
            item.TextColor = Color.FromRgb(255, 159, 104);
            item.FontSize = 25;
            item.BackgroundColor = Color.FromRgb(124, 32, 58);
        }
        save.Clicked += (s, e) =>
        {
            DBSaveModel save = new DBSaveModel 
            { 
                Name = NovellaScenario.Name,
                PageNum = NovellaScenario.PageNum,
                Scenario = FileManage.SerializeToFile(NovellaScenario.Scenario),
                CurrentBackground = NovellaScenario.CurrentBackground,
                CurrentPers = NovellaScenario.CurrentPers,
                RevBackground = NovellaScenario.RevBackground,
                RevPers = NovellaScenario.RevPers,
            };
            if (new string[] { save.Name, save.PageNum == 0 ? string.Empty : "fill", save.Scenario }.All(x => !string.IsNullOrEmpty(x)))
            {
                App.Database.SaveSave(save);
                saves.ItemsSource = App.Database.GetSaves();
            }
        };
        saves.ItemSelected += (s, e) =>
        {
            selectedItem = new DBSaveModel((DBSaveModel)e.SelectedItem);
            st.Clear();
            AddRange(st, load, delete, cancel);
            saves.ItemsSource = App.Database.GetSaves();
        };
        load.Clicked += (s, e) => Application.Current.MainPage = new GameView(new SaveViewModel { Save = selectedItem });
        delete.Clicked += (s, e) =>
        {
            App.Database.DeleteSave(selectedItem.ID);
            st.Clear();
            AddRange(st, exit, saves, save);
            saves.ItemsSource = App.Database.GetSaves();
        };
        cancel.Clicked += (s, e) =>
        {
            st.Clear();
            AddRange(st, exit, saves, save);
            saves.ItemsSource = App.Database.GetSaves();
        };
        AddRange(st, exit, saves, save);
        return templateFrame;
    }
    private static void AddRange(StackLayout layout, params IView[] views) => Array.ForEach(views, layout.Add);
}