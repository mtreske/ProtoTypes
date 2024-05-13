using Microsoft.Extensions.Logging;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private readonly ILogger<MainPage> _logger;
    int count = 0;

    public MainPage(ILogger<MainPage> logger)
    {
        _logger = logger;
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        _logger.LogInformation("----logentry:------------------------------------------------");
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}