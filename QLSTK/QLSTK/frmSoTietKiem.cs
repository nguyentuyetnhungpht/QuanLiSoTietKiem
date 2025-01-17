﻿using QLSTKBUS;
using QLSTKDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSTK
{
    public partial class frmSoTietKiem : DevExpress.XtraEditors.XtraForm
    {
        private SoTietKiemBUS stkBUS;
        private LoaiTietKiemBUS ltkBUS;
        private KhachHangBUS khBUS;

        public frmSoTietKiem()
        {
            InitializeComponent();
            stkBUS = new SoTietKiemBUS();
            ltkBUS = new LoaiTietKiemBUS();
            txtMaSoSTK.Text = stkBUS.getNewMaSo();
        }

        public frmSoTietKiem(SoTietKiemDTO stk)
        {
            InitializeComponent();

            khBUS = new KhachHangBUS();
            KhachHangDTO kh = khBUS.getKhachHang(stk.StrMaKH);
            txtMaSoSTK.Text = stk.StrMaSoSTK;
            txtMaKH.Text = stk.StrMaKH;
            txtCMND.Text = kh.StrCMND;
            txtDiaChi.Text = kh.StrDiaChi;
            txtSoTienGui.Text = stk.DSoDu.ToString();
            cmbLoaiTietKiem.Text = stk.StrMaLTK;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            //1. Map data from GUI
            if (txtMaKH.Text.Length == 0)
            {
                MessageBox.Show("Nhập mã khách hàng");
                return;
            }
            if (txtCMND.Text.Length == 0)
            {
                MessageBox.Show("Nhập CMND");
                return;
            }

            if (txtDiaChi.Text.Length == 0)
            {
                MessageBox.Show("Nhập địa chỉ");
                return;
            }

            if (txtSoTienGui.Text.Length == 0)
            {
                MessageBox.Show("Nhập số tiền gửi");
                return;
            }

            //1.1 info từ form Sổ tiết kiểm
            SoTietKiemDTO stk = new SoTietKiemDTO();
            stk.StrMaSoSTK = txtMaSoSTK.Text;
            stk.StrMaKH = txtMaKH.Text;
            stk.DSoTienGui = double.Parse(txtSoTienGui.Text);
            stk.StrMaLTK = cmbLoaiTietKiem.Text;
            stk.StrNgayMoSo = (DateTime.Now).ToString();
            stk.DSoDu = double.Parse(txtSoTienGui.Text);
            //----------------------------------------


            //1.2 info từ table tblLoaiTietKiem
            LoaiTietKiemDTO ltk = ltkBUS.getLoaiTietKiem(cmbLoaiTietKiem.Text);
            stk.FLaiSuatCamKet = ltk.FLaiSuat;
            stk.IKyHanCamKet = ltk.IKyHan;
            //----------------------------------------------

            //2. Kiểm tra data hợp lệ or not

            //3. Thêm vào DB
            bool kq = stkBUS.themSoTietKiem(stk);
            if (kq == false)
                MessageBox.Show("Thêm Sổ tiết kiệm thất bại. Vui lòng kiểm tra lại dũ liệu");
            else
                MessageBox.Show("Thêm Sổ tiết kiệm thành công");
            //--------------------------------------------
        }

        private void BtnDongSo_Click(object sender, EventArgs e)
        {
            //1. Map key primary from GUI
            SoTietKiemDTO stk = new SoTietKiemDTO();
            stk.StrMaSoSTK = txtMaSoSTK.Text;
            //2. Kiểm tra data hợp lệ

            //3. Xóa khỏi DB
            bool kq = stkBUS.dongSoTietKiem(stk);
            if (kq == false)
                MessageBox.Show("Đóng Sổ Tiết Kiệm thất bại. Vui lòng kiểm tra lại dũ liệu");
            else
                MessageBox.Show("Đóng Sổ tiết kiệm thành công");
        }

        private void FrmSTK_Load(object sender, EventArgs e)
        {
            load_MaLTK();
        }

        private void load_MaLTK()
        {
            List<LoaiTietKiemDTO> listLTK = ltkBUS.selectListLTK();

            if (listLTK == null)
            {
                MessageBox.Show("Có lỗi khi lấy LoaiTietKiem từ DB");
                return;
            }
            // Load Loai tiet kiem
            cmbLoaiTietKiem.DataSource = new BindingSource(listLTK, String.Empty);
            cmbLoaiTietKiem.DisplayMember = "StrMaLTK";
            cmbLoaiTietKiem.ValueMember = "StrMaLTK";
            CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[cmbLoaiTietKiem.DataSource];
            myCurrencyManager.Refresh();
        }

        private void TxtMaKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TxtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TxtSoTienGui_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng ứng dụng sổ tiết kiệm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        //Tải ảnh khách hàng
        private void BtnBrower_Click(object sender, EventArgs e)
        {
            //chọn ảnh từ ổ cứng
            linkAnh.ShowDialog();
            string duongdan;
            duongdan = linkAnh.FileName;
            this.picKhachHang.Image = Image.FromFile(duongdan);
        }
    }
}