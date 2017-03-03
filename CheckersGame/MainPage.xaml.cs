using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using CheckersGame.Source;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CheckersGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            PaintGrid();
            Game game = new Game();

        }

        //paints cells to black and white
        void PaintGrid()
        {

            for (int i = 0; i < CheckersGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < CheckersGrid.ColumnDefinitions.Count; j++)
                {
                    Rectangle rect = new Rectangle();
                    Windows.UI.Color color;
                    if ((i + j) % 2 == 0)
                    {
                        color = Windows.UI.Color.FromArgb(255, 255, 255, 255);
                    }
                    else
                    {
                        color = Windows.UI.Color.FromArgb(255, 0, 0, 0);
                    }
                    rect.Fill = new SolidColorBrush(color);
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    CheckersGrid.Children.Add(rect);
                }
            }
        }

    }
}
