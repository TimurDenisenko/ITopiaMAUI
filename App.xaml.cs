using ITopiaMAUI.Views;

namespace ITopiaMAUI;

public partial class App : Application
{
	public App()
	{
		MainPage = new Shell { CurrentItem = new MainFormView()};
	}
}
