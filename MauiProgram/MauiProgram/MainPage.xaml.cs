using Microsoft.Extensions.Logging;
using Microsoft.Maui.Animations;

namespace MauiProgram;

public partial class MainPage : ContentPage
{
	private readonly IApiService _apiService;
	private readonly ILogger<MainPage> _logger;

	public MainPage(IApiService apiService, ILogger<MainPage> logger)
	{
		_apiService = apiService;
		_logger = logger;

		_logger.LogInformation("MainPage constructor called.");
		InitializeComponent();
	}

    private async void OnGetDataClicked(object sender, EventArgs e)
	{
		_logger.LogInformation("OnGetDataClicked called.");

		CounterLabel.Text = $"Test Data: {_apiService?.GetTestData()}";
		SemanticScreenReader.Announce(CounterLabel.Text);

        var ticker = new Ticker();
		ticker.Start();
		var animationManager = new AnimationManager(ticker);
        var animation = new Microsoft.Maui.Controls.Animation();
		animationManager.Add(animation);

		CounterLabel.Animate("WiggleAnimation", animation);

		CounterLabel.RotateTo(1, 100);
		await CounterLabel.ScaleTo(1.05, 100);
		await CounterLabel.ScaleTo(1, 100);
		await CounterLabel.RotateTo(-2, 100);
		CounterLabel.RotateTo(0, 50);

	}
}