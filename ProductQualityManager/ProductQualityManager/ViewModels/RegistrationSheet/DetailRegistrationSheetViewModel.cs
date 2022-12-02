using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class DetailRegistrationSheetViewModel : BaseViewModel
    {
        private RegistrationSheetModel _selectedRecord;
        private int _idRegistrationSheet;
        private int _idFactory;
        private DateTime _registrationDate;
        private ObservableCollection<DetailRegistrationSheetModel> _detailRegistrationSheetList;

        public RegistrationSheetModel SelectedRecord { get { return _selectedRecord; } set { _selectedRecord = value; OnPropertyChanged(nameof(_selectedRecord)); } }
        public int IdRegistrationSheet { get { return _idRegistrationSheet; } set { _idRegistrationSheet = value; OnPropertyChanged(nameof(_idRegistrationSheet)); } }
        public int IdFactory { get { return _idFactory; } set { _idFactory = value; OnPropertyChanged(nameof(_idFactory)); } }
        public DateTime RegistrationDate { get { return _registrationDate; } set { _registrationDate = value; OnPropertyChanged(nameof(_registrationDate)); } }
        public ObservableCollection<DetailRegistrationSheetModel> DetailRegistrationSheetList { get { return _detailRegistrationSheetList; } set { _detailRegistrationSheetList = value; OnPropertyChanged(nameof(_detailRegistrationSheetList)); } }
        
        public DetailRegistrationSheetViewModel(RegistrationSheetModel SelectedItem)
        {
            IdRegistrationSheet = SelectedItem.MaPhieuDangKy;
            RegistrationDate = (DateTime)SelectedItem.NgayDangKy;
            SelectedRecord = SelectedItem;
            DetailRegistrationSheetList = new ObservableCollection<DetailRegistrationSheetModel>();

            LoadDataListView();
        }

        public void LoadDataListView()
        {
            //List<CHITIETPHIEUDANGKY> List = DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Where(p => p.MaPhieuDangKy == IdRegistrationSheet).ToList();
            //DetailRegistrationSheetList = GetDataSheetFromList(List);
        }
        //Lấy dữ liệu từ list ở Database chuyển về Obs Collection
        public ObservableCollection<DetailRegistrationSheetModel> GetDataSheetFromList(List<CHITIETPHIEUDANGKY> SheetList)
        {
            ObservableCollection<DetailRegistrationSheetModel> SheetListObs = new ObservableCollection<DetailRegistrationSheetModel>();
            int NumberOfRecord = SheetList.Count();

            //for (int i = 0; i < NumberOfRecord; i++)
            //{
            //    DetailRegistrationSheetModel item = new DetailRegistrationSheetModel(SheetList[i]);
            //    item.TenSanPham = GetProductName((int)item.MaSanPham);
            //    SheetListObs.Add(item);
            //}
            return SheetListObs;
        }
        public string GetProductName(int ProductId)
        {
            SANPHAM product = DataProvider.Ins.DB.SANPHAMs.Where(p => p.MaSanPham == ProductId).FirstOrDefault();
            return product.TenSanPham;
        }
    }
}
