﻿using System;
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
using System.Windows.Shapes;
using Projekt_wesele.ViewModels;

namespace Projekt_wesele.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EventListView.xaml
    /// </summary>
    public partial class EventListView : UserControl
    {
        public EventListView()
        {
            InitializeComponent();
            DataContext = new EventListViewModel();
        }
    }
}
