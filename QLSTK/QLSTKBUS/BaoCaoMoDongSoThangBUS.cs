﻿using QLSTKDAL;
using QLSTKDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSTKBUS
{
    public class BaoCaoMoDongSoThangBUS
    {
        private BaoCaoMoDongSoThangDAL bcDAL;
        public BaoCaoMoDongSoThangBUS()
        {
            this.bcDAL = new BaoCaoMoDongSoThangDAL();
        }
       
    }
}