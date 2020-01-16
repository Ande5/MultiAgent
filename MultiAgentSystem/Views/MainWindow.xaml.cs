using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using MultiAgentSystem.Model;
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
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Reflection);
            timer.Interval = new TimeSpan(0, 0, 50);
            timer.Start();
            // _viewModel.OnSelect += UpdateMapContent;
        }

        private void UpdateMapContent(object obj)
        {
            Map.Content = LoadGrid(_viewModel.Size);
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

                    AddShipUI(k, z, ref stack);

                    AddTargetUI(k,z, ref stack);

                    Grid.SetRow(stack, k);
                    Grid.SetColumn(stack, z); 
                    grid.Children.Add(stack);
                }
            }

            stackPanel.Children.Add(grid);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            return stackPanel;
        }

        private void AddShipUI(int gridX, int gridY, ref StackPanel stack)
        {
            foreach (var shipAgent in _viewModel.ShipAgents)
            {
                var stackPosition = new Position { X = gridX, Y = gridY };

                if (shipAgent.Location.X == stackPosition.X
                    && shipAgent.Location.Y == stackPosition.Y)
                {
                    var ship = new Rectangle
                    {
                        Height = 25,
                        Width = 50,
                        RadiusX = 10,
                        RadiusY = 10,
                        Margin = new Thickness(0, 15, 0, 0),
                        Fill = new SolidColorBrush(Color.FromRgb(143, 97, 173))
                    };

                    stack.Children.Add(ship);
                }
            }
        }

        private void AddTargetUI(int gridX, int gridY, ref StackPanel stack)
        {
            foreach (var targetAgent in _viewModel.TargetAgents)
            {
                var stackPosition = new Position { X = gridX, Y = gridY };

                if (targetAgent.Location.X == stackPosition.X
                    && targetAgent.Location.Y == stackPosition.Y)
                {
                    var ship = new Ellipse
                    {
                        Height = 30,
                        Width = 30,
                        Margin = new Thickness(0, 15, 0, 0),
                        Fill = new SolidColorBrush(Color.FromRgb(226, 255, 0))
                    };

                    stack.Children.Add(ship);
                }
            }
        }


        private void Reflection(object sender, EventArgs e)
        {
            Map.Content = LoadGrid(_viewModel.Size);
        }

        private void Apply_Clicked(object sender, RoutedEventArgs e)
        {
            Map.Content = LoadGrid(_viewModel.Size);
        }
    }
}
