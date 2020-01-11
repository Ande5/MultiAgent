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
        private readonly MapsAgentViewModel _viewModel = new MapsAgentViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            Map.Content = CreateGrid();
        }

        /// <summary>
        /// Создание сетки
        /// </summary>
        /// <returns></returns>
        private StackPanel CreateGrid(int gridX = 15, int size = 30)
        {
            var stackPanel = new StackPanel();
            var grid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ShowGridLines = true
            };

            for (int i = 0; i <= gridX; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(size)});
                grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(size)});
            }

            stackPanel.Children.Add(grid);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            return stackPanel;
        }

        private void Apply_Clicked(object sender, RoutedEventArgs e)
        {
            Map.Content = CreateGrid(_viewModel.GridX, _viewModel.Size);
        }
    }
}
