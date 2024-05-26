using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienDB
{
    public partial class Sach : Form
    {
        string Conn = "Data Source=Hz52B;Initial Catalog=Quan_ly_thu_vien;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public Sach()
        {
            InitializeComponent();
        }

        private void Sach_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            SetControls(false);
            Display();
        }
        private void Display()
        {
            string query = "select * from SACH";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvSach.DataSource = dt;
            //dgvSach.Columns[0].HeaderText = "Mã Sách";
            //dgvSach.Columns[0].Width = 55;
            //dgvSach.Columns[1].HeaderText = "Tên Sách";
            //dgvSach.Columns[1].Width = 90;
            //dgvSach.Columns[2].HeaderText = "Tác Giả";
            //dgvSach.Columns[2].Width = 80;
            //dgvSach.Columns[3].HeaderText = "Nhà Xuất Bản";
            //dgvSach.Columns[3].Width = 80;
            //dgvSach.Columns[4].HeaderText = "Giá Bán";
            //dgvSach.Columns[4].Width = 50;
            //dgvSach.Columns[5].HeaderText = "Số Lượng";
            //dgvSach.Columns[5].Width = 60;
        }

        private void dgvSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaSach.Text = dgvSach.Rows[r].Cells[0].Value.ToString();
            txtTenSach.Text = dgvSach.Rows[r].Cells[1].Value.ToString();
            cbTacGia.Text = dgvSach.Rows[r].Cells[2].Value.ToString();
            cbNhaXB.Text = dgvSach.Rows[r].Cells[3].Value.ToString();
            txtNamXB.Text = dgvSach.Rows[r].Cells[4].Value.ToString();
            txtGiaTien.Text = dgvSach.Rows[r].Cells[5].Value.ToString();
            txtNN.Text = dgvSach.Rows[r].Cells[6].Value.ToString();
            cbLoaiSach.Text = dgvSach.Rows[r].Cells[7].Value.ToString();            
            txtMaPhichSach.Text = dgvSach.Rows[r].Cells[8].Value.ToString();
        }        

        private void Homee_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtTimSach_KeyUp(object sender, KeyEventArgs e)
        {
            string query = "select * from SACH where Tensach like N'%" + txtTimSach.Text + "%'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvSach.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            cbTacGia.Clear();
            cbNhaXB.Clear();
            txtMaPhichSach.Clear();
            txtNamXB.Clear();
            txtGiaTien.Clear();
            txtNN.Clear();
            cbLoaiSach.Clear();
            txtMaSach.Focus();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenSach.Focus();
            bien = 2;
            SetControls(true);
            txtMaSach.Enabled = false;
        }
        private void SetControls(bool edit)
        {
            txtMaSach.Enabled = edit;
            txtTenSach.Enabled = edit;
            txtMaPhichSach.Enabled = edit;
            txtNamXB.Enabled = edit;
            txtGiaTien.Enabled = edit;
            cbLoaiSach.Enabled = edit;
            cbNhaXB.Enabled = edit;
            txtNN.Enabled = edit;
            cbTacGia.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;

                int row = dgvSach.CurrentRow.Index;
                string MaSach = dgvSach.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from SACH where Masach = '" + MaSach + "'";
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                Display();
                MessageBox.Show("Đã xoá thành công", "Thông báo");
            }

            catch (Exception)
            {
                MessageBox.Show("Không thể xoá sách này đang được sinh viên mượn.", "Thông Báo");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaSach.Text.Trim() == "" || txtTenSach.Text.Trim() == "" || cbTacGia.Text.Trim() == "" || cbNhaXB.Text.Trim() == "" || txtNamXB.Text.Trim() == "" || txtGiaTien.Text.Trim() == "" || txtNN.Text.Trim() == "" || cbLoaiSach.Text.Trim() == "" || txtMaPhichSach.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!");
                }
                else
                {

                    double x;
                    bool kt = double.TryParse(txtGiaTien.Text, out x);
                    bool kt1 = double.TryParse(txtMaPhichSach.Text, out x);
                    bool kt2 = double.TryParse(txtNamXB.Text, out x);
                    if (kt == false || kt1 == false || kt2 == false || Convert.ToInt32(txtGiaTien.Text) < 0 || Convert.ToInt32(txtMaPhichSach.Text) < 0 || Convert.ToInt32(txtNamXB.Text) < 0)
                    {
                        MessageBox.Show("Vui lòng Nhập lại dưới dạng số dương!", "Thông báo");
                        return;
                    }
                    for (int i = 0; i < dgvSach.RowCount; i++)
                    {
                        if (txtMaSach.Text == dgvSach.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã sách. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    string query1 = "insert into SACH(Masach,Tensach, TenTG, NXB, NamXB, Giatien, Ngonngu, Loaisach, Maphichsach) values(N'" + txtMaSach.Text + "',N'" + txtTenSach.Text + "',N'" + cbTacGia.Text + "', N'" + cbNhaXB.Text + "','" + txtNamXB.Text + "', N'" + txtGiaTien.Text + "', N'" + txtNN.Text + "',N'" + cbLoaiSach.Text + "',N'" + txtMaPhichSach.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    SetControls(false);
                    MessageBox.Show("Đã thêm sách thành công", "Thông báo");
                }
            }
            else
            {
                if (txtMaSach.Text.Trim() == "" || txtTenSach.Text.Trim() == "" || cbTacGia.Text.Trim() == "" || cbNhaXB.Text.Trim() == "" || txtNamXB.Text.Trim() == "" || txtGiaTien.Text.Trim() == "" || txtNN.Text.Trim() == "" || cbLoaiSach.Text.Trim() == "" || txtMaPhichSach.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!");
                    return;
                }
                else
                {
                    double x;
                    bool kt = double.TryParse(txtGiaTien.Text, out x);
                    bool kt1 = double.TryParse(txtMaPhichSach.Text, out x);
                    bool kt2 = double.TryParse(txtNamXB.Text, out x);
                    if (kt == false || kt1 == false || kt2 == false || Convert.ToInt32(txtGiaTien.Text) < 0 || Convert.ToInt32(txtMaPhichSach.Text) < 0 || Convert.ToInt32(txtNamXB.Text) < 0)
                    {
                        MessageBox.Show("Vui lòng Nhập lại dưới dạng số dương!", "Thông báo");
                        return;
                    }
                    int row = dgvSach.CurrentRow.Index;
                    string MaSach = dgvSach.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update SACH set Tensach = N'" + txtTenSach.Text + "', TenTG = N'" + cbTacGia.Text + "', NXB = N'" + cbNhaXB.Text + "',NamXB = '" + txtNamXB.Text + "',Giatien = N'" + txtGiaTien.Text + "', Ngonngu = N'" + txtNN.Text + "',Loaisach = '" + cbLoaiSach.Text + "', Maphichsach = N'" + txtMaPhichSach.Text + "' where Masach = '" + MaSach + "'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Sửa sách thành công");
                    Display();
                    SetControls(false);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }
    }
}
