namespace SaisApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            Application.Current.UserAppTheme = AppTheme.Light;

            _serviceProvider = serviceProvider;

            MainPage = new NavigationPage(_serviceProvider.GetRequiredService<MainPage>());
        }
    }
}