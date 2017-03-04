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
using System.Diagnostics;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CheckersGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        CheckerControl activeChecker;
        CheckerControl[,] checkerControls;
        Game checkersGame;

        public MainPage()
        {
            this.InitializeComponent();
            PaintGrid();
            checkersGame = new Game();

            checkerControls = new CheckerControl[8, 8];
  
            InitCheckers();


        }

        //paints cells to black and white
        private void PaintGrid()
        {

            for (int i = 0; i < CheckersGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < CheckersGrid.ColumnDefinitions.Count; j++)
                {
                    Rectangle rect = new Rectangle();
                    Windows.UI.Color color;
                    if ((i + j) % 2 == 0)
                    {
                        color = Windows.UI.Color.FromArgb(255, 245, 235, 200);
                    }
                    else
                    {
                        color = Windows.UI.Color.FromArgb(255, 80, 80, 80);
                    }
                    rect.Fill = new SolidColorBrush(color);
                    rect.PointerPressed += Rect_PointerPressed;
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    Canvas.SetZIndex(rect, -20);
                    CheckersGrid.Children.Add(rect);
                }
            }
        }

        //initialize checkers' controls for start position
        private void InitCheckers()
        {
            List<CheckerUnit> checkers =  checkersGame.GetCheckers();
            foreach (var checker in checkers)
            {
                CheckerControl checkerControl = new CheckerControl();
                checkerControl.Color = checker.color;
                Grid.SetColumn(checkerControl, checker.column);
                Grid.SetRow(checkerControl, checker.row);
                checkerControl.PointerPressed += CheckerControl_PointerPressed;
                CheckersGrid.Children.Add(checkerControl);

                checkerControls[checker.row, checker.column] = checkerControl;

            }
        }


        private void CheckerControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            activeChecker = (CheckerControl)sender;
            Debug.WriteLine("CheckerControl " + Grid.GetRow(activeChecker) + "," + Grid.GetColumn(activeChecker) + " pressed");
        }

        //field is chosen
        private void Rect_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
            Rectangle rect = (Rectangle) sender;
            Debug.WriteLine("Rect " + Grid.GetRow(rect) + "," + Grid.GetColumn(rect) + " pressed");
            //pass information to the game
            //if move is correct, ask for transactions
            //realize transactions
            if (activeChecker!=null)
            {
                CheckerTransaction transaction;
                transaction.startRow = Grid.GetRow(activeChecker);
                transaction.startCol = Grid.GetColumn(activeChecker);
                transaction.targetRow = Grid.GetRow(rect);
                transaction.targetCol = Grid.GetColumn(rect);
                checkersGame.PassTransaction(transaction);
                MoveCheckerControls(checkersGame.GetMoves());
                //Grid.SetRow(activeChecker, Grid.GetRow(rect));
                //Grid.SetColumn(activeChecker, Grid.GetColumn(rect));
                //activeChecker.MoveTo(Grid.GetRow(rect), Grid.GetColumn(rect));
            }
            activeChecker = null;
            MessageBlock.Text = checkersGame.Turn;
        }

        private void MoveCheckerControls(List<CheckerTransaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                CheckerControl checkerControl = checkerControls[transaction.startRow, transaction.startCol];

                if (transaction.targetRow == -1)
                {
                    CheckersGrid.Children.Remove(checkerControl);
                }else
                {
                    Grid.SetRow(checkerControl, transaction.targetRow);
                    Grid.SetColumn(checkerControl, transaction.targetCol);
                    checkerControls[transaction.targetRow, transaction.targetCol] = checkerControl;
                }
                checkerControls[transaction.startRow, transaction.startCol] = null;


            }
        }
    }
}
