
namespace ITopiaMAUI.Views;

public partial class MainFormView : ContentPage
{
    AbsoluteLayout layout;
    public MainFormView()
    {
        BackgroundImageSource = FileManage.ConvertToImageSource(Properties.Resources.University);
        layout = new AbsoluteLayout();
        Label title1 = new Label { Text = "IT", TextColor = Colors.Black, FontSize = 60 };
        Label title2 = new Label { Text = "opia", TextColor = Colors.Orange, FontSize = 60 };
        AddRange(title1, title2);
        layout.SetLayoutBounds(title1, new Rect(350, 40, layout.Width, layout.Height));
        layout.SetLayoutBounds(title2, new Rect(360, 40, layout.Width, layout.Height));
        Content = layout;
    }
    private void AddRange(params IView[] views) => Array.ForEach(views, layout.Add);
}