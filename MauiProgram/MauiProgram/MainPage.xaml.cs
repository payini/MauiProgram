namespace MauiProgram;

public partial class MainPage : ContentPage
{
	private readonly IApiService _apiService;

	public MainPage(IApiService apiService)
	{
		_apiService = apiService;
		InitializeComponent();
	}

    private void OnGetDataClicked(object sender, EventArgs e)
	{
		CounterLabel.Text = $"Donut Data: {_apiService?.GetDonuts()}";

		SemanticScreenReader.Announce(CounterLabel.Text);
	}
}

