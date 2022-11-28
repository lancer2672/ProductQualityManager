﻿using ProductQualityManager.Models;
using ProductQualityManager.ViewModels.TestingSheet;
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
using System.Windows.Shapes;

namespace ProductQualityManager.Views.TestSheet
{
    /// <summary>
    /// Interaction logic for ModificationHistory.xaml
    /// </summary>
    public partial class ModificationHistory : Window
    {
        public ModificationHistory(RegistrationSheetModel SelectedItem)
        {
            InitializeComponent();
            DetailRegistrationSheetViewModel VM = new DetailRegistrationSheetViewModel(SelectedItem);
            this.DataContext = VM;
        }
    }
}