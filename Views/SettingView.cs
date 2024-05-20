namespace ITopiaMAUI.Views;

public class SettingView : ContentPage
{
	public SettingView()
	{

	}
	public static StackLayout SettingStackLayout()
	{
		StackLayout st = new StackLayout
		{
			WidthRequest = 600,
			HeightRequest = 300,
			BackgroundColor = Colors.Gray
		};
		//st.Children.Add();
		return st;
	}
}