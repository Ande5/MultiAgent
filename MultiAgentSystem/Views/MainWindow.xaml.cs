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
        private StackPanel CreateGrid()
        {
            var stackPanel = new StackPanel();
            var grid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ShowGridLines = true
            };

            for (int i = 0; i <= 15; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(30)});
                grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(30)});
            }

            stackPanel.Children.Add(grid);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            return stackPanel;
        }

    }
}
