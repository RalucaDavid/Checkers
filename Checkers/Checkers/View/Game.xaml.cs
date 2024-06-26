﻿using Checkers.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public Game(object dContext)
        {
            InitializeComponent();
            DataContext = dContext;
        }
        private void PieceImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.OnMouseDownOnBoard(sender, e);
            }
        }
    }
}
