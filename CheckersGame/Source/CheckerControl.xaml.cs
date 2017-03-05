using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CheckersGame.Source
{
    public sealed partial class CheckerControl : UserControl
    {
        private CheckerColor color;

        public CheckerColor Color
        {
            get { return color; }
            set
            {
                color = value;
                if (value == CheckerColor.White)
                {
                    CheckerEllipse.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                    
                }else
                {
                    CheckerEllipse.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
                }
            }
        }

        private bool isKing = false;

        public bool IsKing
        {
            get { return isKing; }
            set
            {
                if (value == true)
                {
                    Crown.Visibility = Visibility.Visible;
                }
                else{
                    Crown.Visibility = Visibility.Collapsed;
                }
                isKing = value;
            }
        }

        private bool isActive = false;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                if (value == true)
                {
                    BorderEllipse.Visibility = Visibility.Visible;
                }
                else
                {
                    BorderEllipse.Visibility = Visibility.Collapsed;
                }
                
            }
        }

        public CheckerControl()
        {
            this.InitializeComponent();
        }

        public void MoveTo(int row, int col)
        {
            Debug.WriteLine("Moving CheckerControl");
            Grid.SetColumn(this, row);
            Grid.SetColumn(this, col);
        }

    }
}
