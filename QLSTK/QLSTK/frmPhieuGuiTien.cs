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
    public partial class frmPhieuGuiTien : Form
    {
        PhieuGuiTienBUS pgtBUS;
        SoTietKiemBUS stkBUS;
        KhachHangBUS khBUS;
        ThamSoBUS tsBUS;
        public frmPhieuGuiTien()
        {
            InitializeComponent();

        }

        private void frmPhieuGuiTien_Load(object sender, EventArgs e)
        {
            pgtBUS = new PhieuGuiTienBUS();
            stkBUS = new SoTietKiemBUS();
            khBUS = new KhachHangBUS();
            tsBUS = new ThamSoBUS();
            txtMaSoPGT.Text = pgtBUS.getNewMaSo();
            loadKhachhHang_Combobox();
            Load_MaSoSTk();
        }

        private void btnLuuVaXuatPhieu_Click(object sender, EventArgs e)
        {
            //1. Map data from GUI


            SoTietKiemDTO stk = stkBUS.getSoTietKiem(cmbMaSoSTK.Text);
            if (stk.StrMaLTK == "1")
            {
                PhieuGuiTienDTO pgt = new PhieuGuiTienDTO();
                pgt.StrMaSoPGT = txtMaSoPGT.Text;
                pgt.StrMaSTK = cmbMaSoSTK.Text;
                pgt.DSoTienGui = double.Parse(txtSoTienGui.Text);
                pgt.StrNgayGui = DateTime.Now.ToString();

                {
                    //3. Thêm vào DB
                    {
                        bool kq = pgtBUS.them(pgt);
                        if (kq == false)
                        {
                            MessageBox.Show("Thêm Phiếu gửi tiền thất bại. Vui lòng kiểm tra lại dũ liệu");
                        }
                        else
                        {
                            MessageBox.Show("Thêm Phiếu gửi tiền thành công");
                            stk.DSoTienGui = stk.DSoDu + double.Parse(txtSoTienGui.Text);
                            kq = stkBUS.suaSoTietKiem(stk);
                            if (kq == true)
                            {
                                MessageBox.Show("Cập nhật sổ tiết kiệm thành công");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật sổ tiết kiệm thất bại");
                            }

                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Bạn không thể gởi thêm tiền vào sổ tiết kiệm có kỳ hạn");
            }
            //--------------------------------------------
        }
        private void loadKhachhHang_Combobox()
        {

            List<KhachHangDTO> listLTK = khBUS.selectListLTK();

            if (listLTK == null)
            {
                MessageBox.Show("Có lỗi khi lấy LaoiTietKiem từ DB");
                return;
            }
            // Load Loai tiet kiem
            cmbKhachHang.DataSource = new BindingSource(listLTK, String.Empty);
            cmbKhachHang.DisplayMember = "StrHoTenKH";
            cmbKhachHang.ValueMember = "StrMaKH";
            CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[cmbKhachHang.DataSource];
            myCurrencyManager.Refresh();

            if (cmbKhachHang.Items.Count > 0)
            {
                cmbKhachHang.SelectedIndex = 0;
            }

        }
        private void Load_MaSoSTk()
        {
            List<SoTietKiemDTO> lsSTK = stkBUS.selectSTKcuaKhachHang(cmbKhachHang.SelectedValue.ToString());
            if (lsSTK == null)
            {
                MessageBox.Show("Có lỗi khi lấy Sổ tiết kiệm từ DB");
                return;
            }

            // Load Loai tiet kiem
            cmbMaSoSTK.DataSource = new BindingSource(lsSTK, String.Empty);
            cmbMaSoSTK.DisplayMember = "StrMaSoSTK";
            cmbMaSoSTK.ValueMember = "StrMaSoSTK";
            CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[cmbMaSoSTK.DataSource];
            myCurrencyManager.Refresh();
            if (cmbKhachHang.Items.Count > 0)
            {
                cmbKhachHang.SelectedIndex = 0;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đã huỷ, bạn có muốn đóng ứng dụng sổ tiết kiệm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void CmbMaSoSTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maSTK = cmbMaSoSTK.SelectedValue.ToString();
        }

        private void CmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maKh = cmbKhachHang.SelectedValue.ToString();
        }

        //rang buoc du lieu nhap vao, chi dc nhap so
        private void TxtSoTienGui_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
             
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            //1. Map data from GUI
            PhieuGuiTienDTO pgt = new PhieuGuiTienDTO();
            if (txtSoTienGui.Text.Length == 0)
            {
                MessageBox.Show("Nhập số tiền gửi");
                return;
            }

            pgt.StrMaSoPGT = txtMaSoPGT.Text;
            pgt.StrMaSTK = cmbMaSoSTK.Text;
            pgt.DSoTienGui = double.Parse(txtSoTienGui.Text);
            pgt.StrNgayGui = DateTime.Now.ToString();

            //----------------------------------------

            //2. Kiểm tra data hợp lệ or not

            //3. Thêm vào DB
            bool kq = pgtBUS.them(pgt);
            if (kq == false)
            {
                MessageBox.Show("Thêm Phiếu gửi tiền thất bại. Vui lòng kiểm tra lại dũ liệu");
            }
            else
            {
                MessageBox.Show("Thêm Phiếu gửi tiền thành công");
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
