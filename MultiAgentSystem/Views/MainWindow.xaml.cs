using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            Map.Content = LoadGrid();
        }

        private StackPanel LoadGrid(int size = 60)
        {
            var stackPanel = new StackPanel();
            var grid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ShowGridLines = true
            };

            for (int k=0; k < _viewModel.MapDepths.GetLength(0); k++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(size) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size) });

                for (int z = 0; z < _viewModel.MapDepths.GetLength(1); z++)
                {
                    int blueDegree, greenDegree = 50, redDegree = 0;

                    if (_viewModel.MapDepths[k,z] < 0)
                    {
                        greenDegree = 105;
                        redDegree = 115;
                        blueDegree = 0;
                    }
                    else
                    {
                        blueDegree = 255 - (int)(255 * _viewModel.MapDepths[k, z] / 100 * 0.55);
                    }

                    var stack = new StackPanel
                    {
                        Background =
                            new SolidColorBrush(Color.FromRgb((byte) redDegree, (byte) greenDegree, (byte) blueDegree))
                    };

                    Grid.SetRow(stack, k);
                    Grid.SetColumn(stack, z); 
                    grid.Children.Add(stack);
                }
            }

            stackPanel.Children.Add(grid);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            return stackPanel;
        }

        private void Apply_Clicked(object sender, RoutedEventArgs e)
        {
            Map.Content = LoadGrid(_viewModel.Size);
        }
    }
}
