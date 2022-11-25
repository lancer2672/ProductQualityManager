using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.ProductCriteria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.ProductCriteriaVM
{
    public class ManageProductCriteriaViewModel : BaseViewModel
    {
        private ObservableCollection<ProductCriteria> _criteriaList;
        private ProductCriteria _selectedCriteria;
        public ObservableCollection<ProductCriteria> CriteriaList { get { return _criteriaList; } set { _criteriaList = value; OnPropertyChanged(nameof(CriteriaList)); } }
        public ProductCriteria SelectedCriteria { get { return _selectedCriteria; } set { _selectedCriteria = value; OnPropertyChanged(nameof(SelectedCriteria)); } }
        public ICommand OpenAddCriteriaCommand { get; set; }
        public ICommand OpenEditCriteriaCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }

        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }
        private SnackbarMessageQueue myMessageQueue;
        public ManageProductCriteriaViewModel()
        {
            CriteriaList = new ObservableCollection<ProductCriteria>();
            OpenAddCriteriaCommand  = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddCriteria(p); });
            OpenEditCriteriaCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenEditCriteria(p); });
            RefreshDataCommand = new RelayCommand<object>((p) => { return true; }, (p) => { RefreshData(p); });
            LoadCriteriaList();

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            MyMessageQueue.DiscardDuplicates = true;
        }
        public ObservableCollection<ProductCriteria> GetCriteriaFromList(List<CHITIEUSANPHAM> criteriaList)
        {
            ObservableCollection<ProductCriteria> list = new ObservableCollection<ProductCriteria>();

            for (int i = 0; i < criteriaList.Count; i++)
            {
                ProductCriteria newCriteria = new ProductCriteria();
                newCriteria.MaChiTieu = criteriaList[i].MaChiTieu;
                newCriteria.TenChiTieu = criteriaList[i].TenChiTieu;
                newCriteria.GiaTriTieuChuan = (decimal)criteriaList[i].GiaTriTieuChuan;
                newCriteria.TenDonViTinh = FindUnit((int)criteriaList[i].MaDonViTinh);
                list.Add(newCriteria);

            }
            return list;
        }
        public void LoadCriteriaList()
        {          
            List<CHITIEUSANPHAM> listCriteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            CriteriaList = GetCriteriaFromList(listCriteria);
        }
        public string FindUnit(int maDonVi)
        {
            List<CHITIEUSANPHAM> listCriteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            List<DONVITINH> listUnit = DataProvider.Ins.DB.DONVITINHs.ToList();
            string tenDonVi = "";
            for (int i = 0; i < listUnit.Count(); i++)
            {
                if (maDonVi == listUnit[i].MaDonViTinh)
                {
                    tenDonVi = (string)listUnit[i].TenDonViTinh;
                    break;
                }    
            }    
            
            return tenDonVi;
        }
        public void OpenAddCriteria(object p)
        {
            AddProductCriteria window = new AddProductCriteria(this);
            window.ShowDialog();
        }
        //public void OpenEditCriteria(ListView listView)
        //{
        //    System.Collections.IList list = listView.SelectedItems;
        //    if (list.Count != 1)
        //    {
        //        return;
        //    }
        //    EditProductCriteria editCriteriaWindow = new EditProductCriteria(this);
        //    editCriteriaWindow.ShowDialog();
        //}
        public void OpenEditCriteria(object p)
        {
            ProductCriteria selectedItem = p as ProductCriteria;
            EditProductCriteria editProductCriteria = new EditProductCriteria(selectedItem, this);
            editProductCriteria.Show();
        }
        public void RefreshData(object p)
        {
            List<CHITIEUSANPHAM> listCriteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            CriteriaList = GetCriteriaFromList(listCriteria);
        }
    }
}
