﻿using QLSTKDAL;
using QLSTKDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSTKBUS
{
    public class BaoCaoDoanhSoNgayBUS
    {
        private BaoCaoDoanhSoNgayDAL bcDAL;
        public BaoCaoDoanhSoNgayBUS()
        {
            this.bcDAL = new BaoCaoDoanhSoNgayDAL();
        }

    }
}