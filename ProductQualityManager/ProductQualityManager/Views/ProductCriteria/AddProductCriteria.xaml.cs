﻿using ProductQualityManager.ViewModels.ProductCriteriaVM;
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

namespace ProductQualityManager.Views.ProductCriteria
{
    /// <summary>
    /// Interaction logic for AddCriteriaCriteria.xaml
    /// </summary>
    public partial class AddCriteriaCriteria : Window
    {
        public AddCriteriaCriteria(ManageProductCriteriaViewModel ProductCriteriaVM)
        {
            InitializeComponent();
            AddCriteriaCriteriaViewModel addCriteriaVM = new AddCriteriaCriteriaViewModel(ProductCriteriaVM);
            this.DataContext = addCriteriaVM;
        }
    }
}
