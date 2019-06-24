﻿using QLSTKBUS;
using QLSTKDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraSplashScreen;

namespace QLSTK
{
    public partial class frmSplashScreen : SplashScreen
    {
        public SqlConnection con = new SqlConnection("Data Source=NHUNG;Initial Catalog=QLSTK;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private TaiKhoanBUS tkBUS;
        public Boolean result;

        public Boolean checkConnection()
        {
            try
            {
                con.Open();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public frmSplashScreen()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © 1998-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }


        private void SplashScreen1_Load(object sender, EventArgs e)
        {
            //pnDangNhap.Width = 0;
            //pnLogo.Location = new drawing
            tbtMatKhauMoi.Visible = false;
            tbtNhapLaiMatKhau.Visible = false;
            btnOK.Visible = false;
            lbnMatKhauMoi.Visible = false;
            lblTroVe.Visible = false;

            lblTrangThai.Text = "";
            tbtTaiKhoan.Focus();

            timer1.Interval = 1;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
                progressBar1.Value += 1;
            else
            {
                timer1.Stop();
                if (checkConnection() == true)
                {
                    pnDangNhap.Width = 674;
                    lblTrangThai.Text = "Kết nối thành công";
                }
                else
                {
                    MessageBox.Show("Kết nối thất bại! Vui lòng kiểm tra lại cài đặt cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }
        private void LblThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LblDoiMatKhau_Click(object sender, EventArgs e)
        {
            tbtMatKhauMoi.Visible = true;
            tbtNhapLaiMatKhau.Visible = true;
            btnOK.Visible = true;
            lbnMatKhauMoi.Visible = true;
            lblTroVe.Visible = true;

            lblDoiMatKhau.Visible = false;
            btnDangNhap.Visible = false;

            tbtTaiKhoan.Text = "";
            tbtMatKhau.Text = "";
            tbtMatKhauMoi.Text = "";
            tbtNhapLaiMatKhau.Text = "";
            lblTrangThaiMK.Text = "";
            
        }

        private void LblTroVe_Click(object sender, EventArgs e)
        {
            lblDoiMatKhau.Visible = true;
            btnDangNhap.Visible = true;

            tbtMatKhauMoi.Visible = false;
            tbtNhapLaiMatKhau.Visible = false;
            btnOK.Visible = false;
            lbnMatKhauMoi.Visible = false;
            lblTroVe.Visible = false;

            tbtTaiKhoan.Text = "";
            tbtMatKhau.Text = "";
            tbtMatKhauMoi.Text = "";
            tbtNhapLaiMatKhau.Text = "";
            lblTrangThaiMK.Text = "";

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            if (tbtTaiKhoan.Text.Length == 0)
            {
                lblTrangThaiMK.Text = "Nhập tài khoản";
                return;
            }

            if (tbtMatKhau.Text.Length == 0)
            {
                lblTrangThaiMK.Text = "Nhập mật khẩu";
                return;
            }

            bool ok = false;
            int loaiTK = 0;

            //error
            result = tkBUS.KiemTra(tbtTaiKhoan.Text, tbtMatKhau.Text, ref ok, ref loaiTK);
            if (result == false)
            {
                MessageBox.Show("Lỗi truy xuất dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbtTaiKhoan.Focus();
                return;
            }

            if (ok == true)
            {
                if (loaiTK == 1) // admin
                {
                    this.Hide();
                    frmMain frmMain = new frmMain();
                    frmMain.Show();
                    //hiển thị quyền
                }
                else //employee
                {
                    this.Hide();
                    frmMain frmMain = new frmMain();
                    frmMain.Show();
                    //vô hiệu hoá tile quy định
                    //hiển thị quyền
                }
            }
            else
            {
                lblTrangThaiMK.Text = "Tài khoản hoặc mật khẩu không đúng";
            }
            
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (tbtTaiKhoan.Text.Length == 0)
            {
                lblTrangThaiMK.Text = "Nhập tài khoản";
                return;
            }

            if (tbtMatKhau.Text.Length == 0)
            {
                lblTrangThaiMK.Text = "Nhập mật khẩu";
                return;
            }

            if (tbtMatKhauMoi.Text.Length == 0)
            {
                lblTrangThaiMK.Text = "Nhập mật khẩu mới";
                return;
            }

            if (tbtNhapLaiMatKhau.Text.Length == 0)
            {
                lblTrangThaiMK.Text = "Nhập lại mật khẩu";
                return;
            }

            if (tbtNhapLaiMatKhau.Text != tbtMatKhauMoi.Text)
            {
                lblTrangThaiMK.Text = "Mật khẩu không khớp";
                return;
            }

            bool ok = false;
            int loaiTK = 0;

            result = tkBUS.KiemTra(tbtTaiKhoan.Text, tbtMatKhau.Text, ref ok, ref loaiTK);
            if (result == false)
            {
                MessageBox.Show("Lỗi truy xuất dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbtTaiKhoan.Focus();
                return;
            }

            if (ok == true)
            {
                ok = false; //kiểm tra update thành công
                result = tkBUS.Update(tbtTaiKhoan.Text, tbtMatKhau.Text, ref ok);
                if (result == false)
                {
                    MessageBox.Show("Lỗi truy xuất dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbtTaiKhoan.Focus();
                    return;
                }

                if (ok == true)
                    lblTrangThaiMK.Text = "Đổi mật khẩu thành công!";
                else
                    lblTrangThaiMK.Text = "Đổi mật khẩu thất bại!";
            }
            else
                lblTrangThaiMK.Text = "Mã đăng nhập hoặc mật khẩu không đúng!";

            tbtTaiKhoan.Text = "";
            tbtMatKhau.Text = "";
            tbtMatKhauMoi.Text = "";
            tbtNhapLaiMatKhau.Text = "";
        }

        private void TbtNhapLaiMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}