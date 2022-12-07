using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class ProductStatusChart
    {
        public Nullable<int> MaCoSo { get; set; }
        public string TinhTrang { get; set; }
        public int TyLe { get; set; }
        public ProductStatusChart(int maCoSo, string tinhTrang, int tyLe)
        {
            MaCoSo = maCoSo;
            TinhTrang = tinhTrang;
            TyLe = tyLe;
        }
    }
}
