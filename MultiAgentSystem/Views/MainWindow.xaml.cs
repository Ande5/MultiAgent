using System.Windows;
using System.Windows.Controls;
using MultiAgentSystem.ViewModels;

namespace MultiAgentSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MapsAgentViewModel();
            Content = CreateGrid();
        }

        /// <summary>
        /// Создание сетки
        /// </summary>
        /// <returns></returns>
        private Grid CreateGrid()
        {
            var grid = new Grid
            {
                Width = 750,
                Height = 750,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                ShowGridLines = true
            };

            for (int i = 0; i <= 15; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }

            return grid;
        }

    }
}
