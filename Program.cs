namespace POS;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        string configPath = ApplicationConfig.GetConfigPath();
        if (!File.Exists(configPath))
        {
            using SetupForm setupForm = new(configPath);
            if (setupForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }

        ApplicationConfig configuration;
        try
        {
            configuration = ApplicationConfig.Load(configPath);
        }
        catch (Exception exception) when (
            exception is IOException or
            UnauthorizedAccessException or
            InvalidDataException)
        {
            MessageBox.Show(
                $"Unable to load application settings from '{configPath}'.\n\n{exception.Message}",
                "Application settings unavailable",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        string? cashierName = CashierDialog.Prompt();
        if (cashierName is null)
        {
            return;
        }

        Application.Run(new Home(configuration, cashierName));
    }    
}