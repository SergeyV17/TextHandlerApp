using System.Windows;
using TextHandlerApp.Models;
using TextHandlerApp.ViewModels;

namespace TextHandlerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TextHanlderService textHandler = new TextHanlderService(new FileReader(), new FileWriter());

            DataContext = new MainViewModel(this, new DialogService(), textHandler, new InfoFileReader());
        }
    }
}
