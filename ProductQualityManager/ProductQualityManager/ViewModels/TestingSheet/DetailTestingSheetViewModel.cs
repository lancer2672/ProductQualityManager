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
    public class DetailTestingSheetViewModel : BaseViewModel
    {
        public ObservableCollection<CreateTestingSheetModel> _testingCriteraList;

        public ObservableCollection<CreateTestingSheetModel> TestingCriteraList { get { return _testingCriteraList; } set { _testingCriteraList = value; OnPropertyChanged(nameof(TestingCriteraList)); } }

        public DetailTestingSheetViewModel(TestingSheetModel item)
        {

            LoadList(item);
        }
        private void LoadList(TestingSheetModel item)
        {
            TestingCriteraList = new ObservableCollection<CreateTestingSheetModel>();
            PHIEUKIEMNGHIEM sheet = DataProvider.Ins.DB.PHIEUKIEMNGHIEMs.Where(t => t.MaPhieuKiemNghiem == item.MaPhieuKiemNghiem).FirstOrDefault();

            
                List<CHITIETPHIEUKIEMNGHIEM> list = DataProvider.Ins.DB.CHITIETPHIEUKIEMNGHIEMs.Where(t => t.MaPhieuKiemNghiem == sheet.MaPhieuKiemNghiem).ToList();
                foreach (CHITIETPHIEUKIEMNGHIEM p in list)
                {
                    int id = (int)p.MaChiTieu;
                    CHITIEUSANPHAM ct = DataProvider.Ins.DB.CHITIEUSANPHAMs.Where(t => t.MaChiTieu == id).FirstOrDefault();
                    CHITIETPHIEUDANGKY f = DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Where(t => t.MaPhieuDangKy == sheet.MaPhieuDangKy && t.MaChiTieu == ct.MaChiTieu).FirstOrDefault();
                    //Lấy dữ liệu cho listview chỉ tiêu
                    
                    DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == ct.MaDonViTinh).FirstOrDefault();
                    CreateTestingSheetModel newItem = new CreateTestingSheetModel(unit, f, p, ct);
                    TestingCriteraList.Add(newItem);
                }
            
        }
    }
}
