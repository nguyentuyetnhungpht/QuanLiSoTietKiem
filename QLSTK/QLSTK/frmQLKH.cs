﻿using QLSTKBUS;
using QLSTKDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSTK
{
    public partial class frmQLKH : Form
    {
        KhachHangBUS khBUS;
        public frmQLKH()
        {
            InitializeComponent();
            khBUS = new KhachHangBUS();
            txtMaKH.Text = khBUS.getNewMaSo();
        }

        private void frmQLKH_Load(object sender, EventArgs e)
        {
            khBUS = new KhachHangBUS();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //1. Map data from GUI
            KhachHangDTO kh = new KhachHangDTO();

            if(txtHoTen.Text.Length == 0)
            {
                MessageBox.Show("Nhập họ tên");
                return;
            }

            if (txtCMND.Text.Length == 0)
            {
                MessageBox.Show("Nhập CMND");
                return;
            }

            if (txtDC.Text.Length == 0)
            {
                MessageBox.Show("Nhập địa chỉ");
                return;
            }

            if (txtDienThoai.Text.Length == 0)
            {
                MessageBox.Show("Nhập số điện thoại");
                return;
            }

            if (txtEmail.Text.Length == 0)
            {
                MessageBox.Show("Nhập họ tên");
                return;
            }
            kh.StrMaKH = txtMaKH.Text;
            kh.StrHoTenKH = txtHoTen.Text;
            kh.StrDiaChi = txtDiaChi.Text;
            kh.StrCMND = txtCMND.Text;
            //----------------------------------------

            //2. Kiểm tra data hợp lệ or not

            //3. Thêm vào DB
            bool kq = khBUS.them(kh);
            if (kq == false)
                MessageBox.Show("Thêm Khách hàng thất bại. Vui lòng kiểm tra lại dũ liệu");
            else
                MessageBox.Show("Thêm Khách hàng thành công");
            //--------------------------------------------
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //1. Map key primary from GUI
            KhachHangDTO kh = new KhachHangDTO();
            kh.StrMaKH = txtMaKH.Text;
            //2. Kiểm tra data hợp lệ

            //3. Xóa khỏi DB
            bool kq = khBUS.xoa(kh);
            if (kq == false)
                MessageBox.Show("Xóa khách hàng thất bại. Vui lòng kiểm tra lại dũ liệu");
            else
                MessageBox.Show("Xóa khách hàng thành công");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //1. Map data from GUI
            KhachHangDTO kh = new KhachHangDTO();
            kh.StrMaKH = txtMaKH.Text;
            kh.StrHoTenKH = txtHoTen.Text;
            kh.StrDiaChi = txtDiaChi.Text;
            kh.StrCMND = txtCMND.Text;

            //2. Kiểm tra data hợp lệ or not

            //3. Thêm vào DB
            bool kq = khBUS.sua(kh);
            if (kq == false)
                MessageBox.Show("Sửa khách hàng thất bại. Vui lòng kiểm tra lại dũ liệu");
            else
                MessageBox.Show("Sửa khách hàng thành công");
        }

        private void TxtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= 65 && e.KeyChar <= 122) || e.KeyChar == 8);
        }

        private void TxtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            string patten = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z]*@[0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(txtEmail.Text, patten))
                    errorProvider1.Clear();
           else
                errorProvider1.SetError(this.txtEmail, "Email không hợp lệ");
        }
    }
}
