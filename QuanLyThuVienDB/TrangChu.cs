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
    public partial class TrangChu : Form
    {
        string Conn = "Data Source=Hz52B;Initial Catalog=Quan_ly_thu_vien;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;

        private string _message;
        public TrangChu()
        {
            InitializeComponent();
        }
        public TrangChu(string Message) : this()
        {
            _message = Message;
            txtXinChao.Text = _message;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            try
            {
                mySqlconnection = new SqlConnection(Conn);
                mySqlconnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DocGia();
            NhanVien();
            PhichSach();
            NCC();
            Muontra();
            cbMuontra();
            tracuusach();
            thongke();             
        }
        private void S_Click(object sender, EventArgs e)
        {
            Sach S = new Sach();
            S.Show();
        }
        public void tracuusach()
        {
            try
            {
                string query = "SELECT * FROM SACH";
                using (SqlCommand command = new SqlCommand(query, mySqlconnection))
                {
                    SqlDataReader dr = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dgvTimSach.DataSource = dt; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiemm_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string query = "SELECT * FROM SACH WHERE ";
                if (btnTenSach.Checked)
                {
                    query += "TenSach LIKE @search";
                }
                else if (btnLoaiSach.Checked)
                {
                    query += "LoaiSach LIKE @search";
                }
                else if (btnTacGia.Checked)
                {
                    query += "TacGia LIKE @search";
                }
                else if (btnNXB.Checked)
                {
                    query += "NhaXB LIKE @search";
                }

                using (SqlCommand command = new SqlCommand(query, mySqlconnection))
                {
                    command.Parameters.AddWithValue("@search", "%" + txtTimKiemm.Text + "%");
                    SqlDataReader dr = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dgvTimSach.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DocGia()
        {
            try
            {
                string query = "select * from DOCGIA";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                using (SqlDataReader dr = mySqlCommand.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dgvSinhVien.DataSource = dt;
                }
                SetControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu độc giả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnThemSV_Click(object sender, EventArgs e)
        {
            dgvSinhVien.RowEnter -= dgvSinhVien_RowEnter;
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            txtNganhHoc.Clear();
            txtDC.Clear();
            cbbDV.Clear();

            bien = 1;

            SetControls(true);
            txtMaSinhVien.Focus();
            dgvSinhVien.RowEnter += dgvSinhVien_RowEnter;
        }
        private void SetControls(bool edit)
        {
            txtMaSinhVien.Enabled = edit;
            txtHoTen.Enabled = edit;
            cbbDV.Enabled = edit;
            txtNganhHoc.Enabled = edit;
            txtDC.Enabled = edit;
            cbbDV.Enabled = edit;
            txtNS.Enabled = edit;
            txtNCT.Enabled = edit;
            btnThemSV.Enabled = !edit;
            btnSuaSV.Enabled = !edit;
            btnXoaSV.Enabled = !edit;
            btnGhiSV.Enabled = edit;
            btnHuySV.Enabled = edit;
            //.Enabled = edit;
            cbTenSach.Enabled = edit;
            cbNgayMuon.Enabled = edit;
            cbNgayHenTra.Enabled = edit;
            cbMaSV.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnMuon.Enabled = !edit;
            btnGiaHan.Enabled = !edit;
            btnTraSach.Enabled = !edit;
            btnGhii.Enabled = edit;
            btnHuyy.Enabled = edit;
            txtMaPhieuMUon.Enabled = edit;
            cbNgayMuon.Visible = true;
            lbNgayMuon.Visible = true;
        }
        private void SetControlNV(bool edit)
        {
            txtMaNV.Enabled = edit;
            txtTenNV.Enabled = edit;
            cbND.Enabled = !edit;
            txtTSDM.Enabled = !edit;
            txtMaDon.Enabled = !edit;
            txtMaNCCD.Enabled = !edit;
            txtDG.Enabled = !edit;
            btnDangKy.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoaNV.Enabled = !edit;
            btnGhiNV.Enabled = edit;
            btnHuyNV.Enabled = edit;
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            txtHoTen.Focus();
            bien = 2;

            SetControls(true);
            txtMaSinhVien.Enabled = false;
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvSinhVien.CurrentRow.Index;
                string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from DOCGIA where MaDG = " + MaSV;
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DocGia();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xoá. Sinh này đang mượn sách", "Thông Báo");
            }
        }

        private void btnGhiSV_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các điều khiển để đảm bảo chúng không null
                if (txtMaSinhVien == null || txtHoTen == null || txtNganhHoc == null || txtNS == null || cbbDV == null || txtDC == null || txtNCT == null || dgvSinhVien == null)
                {
                    MessageBox.Show("Một hoặc nhiều điều khiển chưa được khởi tạo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra các giá trị đầu vào có trống hay không
                if (string.IsNullOrWhiteSpace(txtMaSinhVien.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtNganhHoc.Text) ||
                    string.IsNullOrWhiteSpace(cbbDV.Text) || string.IsNullOrWhiteSpace(txtDC.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra giá trị của txtMaSinhVien có phải là số hay không
                if (!double.TryParse(txtMaSinhVien.Text, out _))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên dưới dạng số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trường hợp thêm mới sinh viên
                if (bien == 1)
                {
                    // Kiểm tra trùng mã sinh viên
                    for (int i = 0; i < dgvSinhVien.RowCount; i++)
                    {
                        if (txtMaSinhVien.Text == dgvSinhVien.Rows[i].Cells[0].Value?.ToString())
                        {
                            MessageBox.Show("Trùng mã sinh viên. Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Thêm sinh viên mới
                    string query1 = "INSERT INTO DOCGIA(MaDG, TenDG, Ngaysinh, Donvi, Diachi, CN, Ngaycapthe) VALUES (@MaDG, @TenDG, @Ngaysinh, @Donvi, @Diachi, @CN, @Ngaycapthe)";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@MaDG", txtMaSinhVien.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenDG", txtHoTen.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ngaysinh", txtNS.Value);
                    mySqlCommand.Parameters.AddWithValue("@Donvi", cbbDV.Text);
                    mySqlCommand.Parameters.AddWithValue("@Diachi", txtDC.Text);
                    mySqlCommand.Parameters.AddWithValue("@CN", txtNganhHoc.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ngaycapthe", txtNCT.Value);

                    mySqlCommand.ExecuteNonQuery();
                    DocGia();
                    MessageBox.Show("Thêm sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Trường hợp sửa thông tin sinh viên
                {
                    int row = dgvSinhVien.CurrentRow?.Index ?? -1;
                    if (row < 0)
                    {
                        MessageBox.Show("Không thể tìm thấy sinh viên hiện tại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string MaSV = dgvSinhVien.Rows[row].Cells[0].Value?.ToString();
                    if (string.IsNullOrEmpty(MaSV))
                    {
                        MessageBox.Show("Không thể tìm thấy mã sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string query2 = "UPDATE DOCGIA SET MaDG = @MaDG, TenDG = @TenDG, Ngaysinh = @Ngaysinh, Donvi = @Donvi, Diachi = @Diachi, CN = @CN, Ngaycapthe = @Ngaycapthe WHERE MaDG = @OldMaDG";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@MaDG", txtMaSinhVien.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenDG", txtHoTen.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ngaysinh", txtNS.Value);
                    mySqlCommand.Parameters.AddWithValue("@Donvi", cbbDV.Text);
                    mySqlCommand.Parameters.AddWithValue("@Diachi", txtDC.Text);
                    mySqlCommand.Parameters.AddWithValue("@CN", txtNganhHoc.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ngaycapthe", txtNCT.Value);
                    mySqlCommand.Parameters.AddWithValue("@OldMaDG", MaSV);

                    mySqlCommand.ExecuteNonQuery();
                    DocGia();
                    MessageBox.Show("Sửa sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuySV_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void dgvSinhVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvSinhVien.Rows.Count > 0)
                {
                    int r = e.RowIndex;
                    txtMaSinhVien.Text = dgvSinhVien.Rows[r].Cells[0].Value?.ToString();
                    txtHoTen.Text = dgvSinhVien.Rows[r].Cells[1].Value?.ToString();
                    txtNS.Text = dgvSinhVien.Rows[r].Cells[2].Value?.ToString();
                    cbbDV.Text = dgvSinhVien.Rows[r].Cells[3].Value?.ToString();
                    txtDC.Text = dgvSinhVien.Rows[r].Cells[4].Value?.ToString();
                    txtNganhHoc.Text = dgvSinhVien.Rows[r].Cells[5].Value?.ToString();
                    txtNCT.Text = dgvSinhVien.Rows[r].Cells[6].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin độc giả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtTimKiemSV_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnMSV.Checked)
            {
                string query = "select * from DOCGIA where MaDG like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }

            if (btnTSV.Checked)
            {
                string query = "select * from DOCGIA where TenDG like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }
        }

        public void NhanVien()
        {
            txtMaNV.Enabled = !true;
            txtTenNV.Enabled = !true;
            cbND.Enabled = !true;
            txtTSDM.Enabled = !true;
            txtMaDon.Enabled = !true;
            txtMaNCCD.Enabled = !true;
            txtDG.Enabled = !true;
            btnDangKy.Enabled = true;
            btnSua.Enabled = true;
            btnXoaNV.Enabled = true;
            btnGhiNV.Enabled = true;
            btnHuyNV.Enabled = true;
            btnThemDH.Enabled = true;
            string query = "select Manhanvien, Tennhanvien from NHANVIEN";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTTNV.DataSource = dt;
            string query1 = "select DDS.Madon, DS.Manhanvien, DS.Ngaydat, DDS.MaNCC, DDS.TensachDM, DDS.Dongia from DONSACH DS join DONGDONSACH DDS on DDS.Madon = DS.Madon join NHACUNGCAP NCC on NCC.MaNCC = DDS.MaNCC";
            mySqlCommand = new SqlCommand(query1, mySqlconnection);
            SqlDataReader dr1 = mySqlCommand.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(dr1);
            dgvTTDH.DataSource = dt1;
        }
        private void btnDangKy_Click_1(object sender, EventArgs e)
        {
            bien = 7;
            txtMaNV.Focus();
            SetControlNV(true);

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            bien = 8;
            txtMaNV.Focus();
            SetControlNV(true);
        }
        private void btnThemDH_Click(object sender, EventArgs e)
        {
            bien = 9;
            txtMaNV.Enabled = !true;
            txtTenNV.Enabled = !true;
            cbND.Enabled = true;
            txtTSDM.Enabled = true;
            txtMaDon.Enabled = true;
            txtMaNCCD.Enabled = true;
            txtDG.Enabled = true;
            btnDangKy.Enabled = !true;
            btnSua.Enabled = !true;
            btnXoaNV.Enabled = !true;
            btnGhiNV.Enabled = true;
            btnHuyNV.Enabled = true;
            btnThemDH.Enabled = !true;
        }
        private void btnGhiNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (bien == 7) 
                {
                    if (string.IsNullOrEmpty(txtMaNV.Text) || string.IsNullOrEmpty(txtTenNV.Text))
                    {
                        MessageBox.Show("Vui lòng nhập đủ thông tin Mã nhân viên và Tên nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string insertQuery = "INSERT INTO NHANVIEN (Manhanvien, Tennhanvien) VALUES (@Manhanvien, @Tennhanvien)";

                    SqlCommand command = new SqlCommand(insertQuery, mySqlconnection);

                    command.Parameters.AddWithValue("@Manhanvien", txtMaNV.Text);
                    command.Parameters.AddWithValue("@Tennhanvien", txtTenNV.Text);

                    command.ExecuteNonQuery();
                    NhanVien();
                    MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (bien == 8) 
                {
                    if (string.IsNullOrEmpty(txtMaNV.Text) || string.IsNullOrEmpty(txtTenNV.Text))
                    {
                        MessageBox.Show("Vui lòng nhập đủ thông tin Mã nhân viên và Tên nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string updateQuery = "UPDATE NHANVIEN SET Tennhanvien = @Tennhanvien WHERE Manhanvien = @Manhanvien";

                    SqlCommand command = new SqlCommand(updateQuery, mySqlconnection);

                    command.Parameters.AddWithValue("@Manhanvien", txtMaNV.Text);
                    command.Parameters.AddWithValue("@Tennhanvien", txtTenNV.Text);

                    command.ExecuteNonQuery();
                    NhanVien();
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (bien == 9)
                {
                    if (string.IsNullOrEmpty(txtMaDon.Text) || string.IsNullOrEmpty(txtMaNV.Text) || string.IsNullOrEmpty(txtMaNCCD.Text) || string.IsNullOrEmpty(txtTSDM.Text) || string.IsNullOrEmpty(txtDG.Text))
                    {
                        MessageBox.Show("Vui lòng nhập đủ thông tin Mã đơn, Mã nhân viên, Mã NCC, Tên sách, và Đơn giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SqlTransaction transaction = mySqlconnection.BeginTransaction();

                    try
                    {
                        string insertDonSachQuery = "INSERT INTO DONSACH (Madon, Manhanvien, Ngaydat) VALUES (@Madon, @Manhanvien, @Ngaydat)";
                        SqlCommand commandDonSach = new SqlCommand(insertDonSachQuery, mySqlconnection, transaction);

                        commandDonSach.Parameters.AddWithValue("@Madon", txtMaDon.Text);
                        commandDonSach.Parameters.AddWithValue("@Manhanvien", txtMaNV.Text);
                        commandDonSach.Parameters.AddWithValue("@Ngaydat", cbND.Value);

                        commandDonSach.ExecuteNonQuery();

                        string insertDongDonSachQuery = "INSERT INTO DONGDONSACH (Madon, MaNCC, TensachDM, Dongia) VALUES (@Madon, @MaNCC, @TensachDM, @Dongia)";
                        SqlCommand commandDongDonSach = new SqlCommand(insertDongDonSachQuery, mySqlconnection, transaction);

                        commandDongDonSach.Parameters.AddWithValue("@Madon", txtMaDon.Text);
                        commandDongDonSach.Parameters.AddWithValue("@MaNCC", txtMaNCCD.Text);
                        commandDongDonSach.Parameters.AddWithValue("@TensachDM", txtTSDM.Text);
                        commandDongDonSach.Parameters.AddWithValue("@Dongia", txtDG.Text);

                        commandDongDonSach.ExecuteNonQuery();
                        transaction.Commit();
                        NhanVien();
                        MessageBox.Show("Thêm đơn sách và dòng đơn sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi khi thêm đơn sách và dòng đơn sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện thêm hoặc cập nhật thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaNV_Click_1(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvTTNV.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < dgvTTNV.Rows.Count)
                {
                    string maNV = dgvTTNV.Rows[rowIndex].Cells["Manhanvien"].Value.ToString();
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên với mã {maNV}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string query = "DELETE FROM NHANVIEN WHERE Manhanvien = @Manhanvien";
                        using (SqlCommand command = new SqlCommand(query, mySqlconnection))
                        {
                            command.Parameters.AddWithValue("@Manhanvien", maNV);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Xóa nhân viên thành công.", "Thông báo");
                        NhanVien();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaDH_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvTTDH.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < dgvTTDH.Rows.Count)
                {
                    string maDon = dgvTTDH.Rows[rowIndex].Cells["Madon"].Value.ToString();
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn sách với mã {maDon}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteDongDonQuery = "DELETE FROM DONGDONSACH WHERE Madon = @Madon";
                        using (SqlCommand command = new SqlCommand(deleteDongDonQuery, mySqlconnection))
                        {
                            command.Parameters.AddWithValue("@Madon", maDon);
                            command.ExecuteNonQuery();
                        }

                        string deleteDonQuery = "DELETE FROM DONSACH WHERE Madon = @Madon";
                        using (SqlCommand command = new SqlCommand(deleteDonQuery, mySqlconnection))
                        {
                            command.Parameters.AddWithValue("@Madon", maDon);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Xóa đơn sách thành công.", "Thông báo");
                        NhanVien();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa đơn sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvTTNV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (rowIndex >= 0 && rowIndex < dgvTTNV.Rows.Count)
                {

                    string maNV = dgvTTNV.Rows[rowIndex].Cells["Manhanvien"].Value.ToString();
                    string tenNV = dgvTTNV.Rows[rowIndex].Cells["Tennhanvien"].Value.ToString();

                    txtMaNV.Text = maNV;
                    txtTenNV.Text = tenNV;

                    txtMaNV.Enabled = false;
                    txtTenNV.Enabled = false;
                    ClearOtherTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin nhân viên từ DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearOtherTextBoxes()
        {
            txtMaDon.Text = string.Empty;
            txtDG.Text = string.Empty;
            txtMaNCCD.Text = string.Empty;
            txtTSDM.Text = string.Empty;
        }
        private void dgvTTDH_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvTTDH.Rows[e.RowIndex];

                txtMaDon.Text = selectedRow.Cells["Madon"].Value.ToString();
                txtMaNV.Text = selectedRow.Cells["Manhanvien"].Value.ToString();
                cbND.Value = Convert.ToDateTime(selectedRow.Cells["Ngaydat"].Value);

                txtMaNCCD.Text = selectedRow.Cells["MaNCC"].Value.ToString();
                txtTSDM.Text = selectedRow.Cells["TensachDM"].Value.ToString();
                txtDG.Text = selectedRow.Cells["Dongia"].Value.ToString();

                txtMaDon.ReadOnly = true;
                txtMaNV.ReadOnly = true;
                txtMaNCCD.ReadOnly = true;
            }
        }

        private void btnHuyNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtMaNV.Enabled = !true;
            txtTenNV.Enabled = !true;
            cbND.Enabled = !true;
            txtTSDM.Enabled = !true;
            txtMaDon.Enabled = !true;
            txtMaNCCD.Enabled = !true;
            txtDG.Enabled = !true;
            btnDangKy.Enabled = true;
            btnSua.Enabled = true;
            btnXoaNV.Enabled = true;
            btnGhiNV.Enabled = true;
            btnHuyNV.Enabled = true;
            btnThemDH.Enabled = true;
        }
        public void Muontra()
        {
            string query1 = "select DM.Maphieumuon, SV.MaDG, SV.TenDG, DM.Masach, S.Tensach,MS.Ngaymuon,DM.Ngayhentra from MUON MS join DONGMUON DM on MS.Maphieumuon = DM.Maphieumuon join DOCGIA SV on SV.MaDG = MS.MaDG join SACH S on DM.Masach = S.Masach";
            mySqlCommand = new SqlCommand(query1, mySqlconnection);
            SqlDataReader dr1 = mySqlCommand.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(dr1);
            dgvMuonSach.DataSource = dt1;
            txtMaPhieuMUon.Enabled = false;
            ttMaSach.Enabled = false;
            ttTenSach.Enabled = false;
            string query2 = "select TS.MaDG, SV.TenDG, TS.Masach, S.Tensach,TS.Ngaytra,TS.Tinhtrang from TRA TS join DOCGIA SV on SV.MaDG = TS.MaDG join SACH S on TS.Masach = S.Masach";
            mySqlCommand = new SqlCommand(query2, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTraSach.DataSource = dt;
            txtMaPhieuMUon.Enabled = false;
            ttMaSach.Enabled = false;
            ttTenSach.Enabled = false;
        }
        public void cbMuontra()
        {
            string sSql2 = "select MaDG from DOCGIA";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                cbMaSV.Items.Add(dr[0].ToString());
            }
            string sSql9 = "select Tensach from SACH";
            mySqlCommand = new SqlCommand(sSql9, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(mySqlCommand);
            da9.Fill(dt9);
            foreach (DataRow dr in dt9.Rows)
            {
                cbTenSach.Items.Add(dr[0].ToString());
            }
        }
        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql1 = "select s.Masach, s.Tensach, s.TenTG from SACH s where s.Tensach = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["Masach"].ToString();
                ttTenSach.Text = dr["Tensach"].ToString();
                ttTenTG.Text = dr["TenTG"].ToString();
            }
        }
        private void txtTimKiemSachMuon_KeyUp(object sender, KeyEventArgs e)
        {
            if (raMaSV.Checked)
            {
                string query = "select DM.Maphieumuon, SV.MaDG, SV.TenDG, S.Masach, S.Tensach,MS.Ngaymuon,TS.Ngaytra,TS.Tinhtrang from MUON MS join SACH S on S.Ma=sach = MS.Masach join DOCGIA SV on SV.MaDG = MS.MaDG join TRA TS on TS.MaDG = MS.MaDG where SV.MaDG like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
            if (raMaSach.Checked)
            {
                string query = "select MS.Maphieumuon, SV.MaDG, SV.TenDG, S.Masach, S.Tensach,MS.Ngaymuon,TS.Ngaytra,TS.Tinhtrang from MUON MS join SACH S on S.Ma=sach = MS.Masach join DOCGIA SV on SV.MaDG = MS.MaDG join TRA TS on TS.MaDG = MS.MaDG where S.Masach like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
        }
        private void btnMuon_Click(object sender, EventArgs e)
        {
            cbTenSach.Focus();
            bien = 5;
            //SetControls(true);
            cbTenSach.Enabled = true;
            cbNgayMuon.Enabled = true;
            cbNgayHenTra.Enabled = true;
            cbNgayHenTra.Enabled = false;
            cbMaSV.Enabled = true;
            txtGhiChu.Enabled = false;
            btnMuon.Enabled = !true;
            btnGiaHan.Enabled = !true;
            btnTraSach.Enabled = !true;
            btnGhii.Enabled = true;
            btnHuyy.Enabled = true;
            txtMaPhieuMUon.Enabled = true;
            txtMaPhieuMUon.Enabled = true;
            cbNgayMuon.Visible = true;
            lbNgayMuon.Visible = true;
        }
        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            bien = 6;
            SetControls(true);
            txtMaPhieuMUon.Enabled = false;
            cbTenSach.Enabled = false;
            cbMaSV.Enabled = false;
            txtGhiChu.Enabled = false;
            cbNgayMuon.Enabled = false;
        }
        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn trả? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;

            try
            {
                int row = dgvMuonSach.CurrentRow.Index;
                string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
                string MaSV = dgvMuonSach.Rows[row].Cells[1].Value.ToString();
                string Masach = dgvMuonSach.Rows[row].Cells[3].Value.ToString();

                DateTime NgayHenTra = Convert.ToDateTime(dgvMuonSach.Rows[row].Cells["Ngayhentra"].Value);
                DateTime NgayTra = DateTime.Now;
                string TinhTrang = txtGhiChu.Text;

                if (NgayTra > NgayHenTra)
                {
                    TinhTrang = "Quá hạn";
                }

                string query5 = "INSERT INTO TRA (MaDG, Masach, Ngaytra, Tinhtrang) VALUES (@MaSV, @Masach, @NgayTra, @TinhTrang)";
                using (SqlCommand command3 = new SqlCommand(query5, mySqlconnection))
                {
                    command3.Parameters.AddWithValue("@MaSV", MaSV);
                    command3.Parameters.AddWithValue("@Masach", Masach);
                    command3.Parameters.AddWithValue("@NgayTra", NgayTra.ToString("yyyy-MM-dd"));
                    command3.Parameters.AddWithValue("@TinhTrang", TinhTrang);
                    command3.ExecuteNonQuery();
                }

                string query3 = "DELETE FROM DONGMUON WHERE Maphieumuon = @MaMuonTra";
                string query4 = "DELETE FROM MUON WHERE Maphieumuon = @MaMuonTra";
                using (SqlCommand command1 = new SqlCommand(query3, mySqlconnection))
                using (SqlCommand command2 = new SqlCommand(query4, mySqlconnection))
                {
                    command1.Parameters.AddWithValue("@MaMuonTra", MaMuonTra);
                    command2.Parameters.AddWithValue("@MaMuonTra", MaMuonTra);
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                }

                Muontra();
                MessageBox.Show("Trả sách thành công.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi trả sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGhii_Click(object sender, EventArgs e)
        {
            try
            {
                if (bien == 5)
                {
                    if (string.IsNullOrEmpty(txtMaPhieuMUon.Text))
                    {
                        MessageBox.Show("Mã phiếu mượn không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(cbMaSV.Text))
                    {
                        MessageBox.Show("Mã sinh viên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string insertMuon = "INSERT INTO MUON (Maphieumuon, MaDG, Ngaymuon) VALUES (@Maphieumuon, @MaDG, @Ngaymuon)";
                    mySqlCommand = new SqlCommand(insertMuon, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@Maphieumuon", txtMaPhieuMUon.Text);
                    mySqlCommand.Parameters.AddWithValue("@MaDG", cbMaSV.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ngaymuon", cbNgayMuon.Value);
                    mySqlCommand.ExecuteNonQuery();
                    string insertDongMuon = "INSERT INTO DONGMUON (Maphieumuon, Masach, Ngayhentra) VALUES (@Maphieumuon, @Masach, @Ngayhentra)";
                    mySqlCommand = new SqlCommand(insertDongMuon, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@Maphieumuon", txtMaPhieuMUon.Text);
                    mySqlCommand.Parameters.AddWithValue("@Masach", ttMaSach.Text);
                    mySqlCommand.Parameters.AddWithValue("@Ngayhentra", cbNgayHenTra.Value);
                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Mượn sách thành công.", "Thông báo");
                    Muontra();
                }
                else if (bien == 6)
                {
                    if (string.IsNullOrEmpty(txtMaPhieuMUon.Text))
                    {
                        MessageBox.Show("Mã phiếu mượn không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrEmpty(cbMaSV.Text))
                    {
                        MessageBox.Show("Mã sinh viên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string insertTra = "INSERT INTO TRA (MaDG, Masach, Ngaytra, Tinhtrang) VALUES (@MaDG, @Masach, @Ngaytra, @Tinhtrang)";
                    mySqlCommand = new SqlCommand(insertTra, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@MaDG", cbMaSV.Text);
                    mySqlCommand.Parameters.AddWithValue("@Masach", ttMaSach.Text);

                    mySqlCommand.Parameters.AddWithValue("@Ngaytra", cbNgayHenTra.Value);

                    mySqlCommand.Parameters.AddWithValue("@Tinhtrang", txtGhiChu.Text);
                    mySqlCommand.ExecuteNonQuery();

                    MessageBox.Show("Trả sách thành công.", "Thông báo");

                    Muontra();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }
        private void btnHuyy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }
        private void dgvMuonSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (dgvMuonSach.Rows[r].Cells[0].Value != null)
                txtMaPhieuMUon.Text = dgvMuonSach.Rows[r].Cells[0].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[4].Value != null)
                cbTenSach.Text = dgvMuonSach.Rows[r].Cells[4].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[1].Value != null)
                cbMaSV.Text = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[5].Value != null)
                cbNgayMuon.Text = dgvMuonSach.Rows[r].Cells[5].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[6].Value != null)
                cbNgayHenTra.Text = dgvMuonSach.Rows[r].Cells[6].Value.ToString();
            string sSql1 = "select s.Masach, s.Tensach, s.TenTG from SACH s where s.Tensach = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["Masach"].ToString();
                ttTenSach.Text = dr["Tensach"].ToString();
                ttTenTG.Text = dr["TenTG"].ToString();
            }
            string sSql2 = "select MaDG, TenDG from DOCGIA where MaDG = '" + cbMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["MaDG"].ToString();
                ttTenSV.Text = dr["TenDG"].ToString();
            }
        }
        private void cbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql2 = "select MaDG, TenDG from DOCGIA where MaDG = '" + cbMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["MaDG"].ToString();
                ttTenSV.Text = dr["TenDG"].ToString();
            }
        }
        private void dgvTraSach_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (dgvMuonSach.Rows[r].Cells[3].Value != null)
                cbTenSach.Text = dgvTraSach.Rows[r].Cells[3].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[0].Value != null)
                cbMaSV.Text = dgvTraSach.Rows[r].Cells[0].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[4].Value != null)
                cbNgayHenTra.Text = dgvTraSach.Rows[r].Cells[4].Value.ToString();
            if (dgvMuonSach.Rows[r].Cells[5].Value != null)
                txtGhiChu.Text = dgvTraSach.Rows[r].Cells[5].Value.ToString();
            if (!string.IsNullOrEmpty(cbTenSach.Text))
            {
                string sSql1 = "select s.Masach, s.Tensach, s.TenTG from SACH s where s.Tensach = '" + cbTenSach.Text + "'";
                mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
                da1.Fill(dt1);
                foreach (DataRow dr in dt1.Rows)
                {
                    ttMaSach.Text = dr["Masach"].ToString();
                    ttTenSach.Text = dr["Tensach"].ToString();
                    ttTenTG.Text = dr["TenTG"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(cbMaSV.Text))
            {
                string sSql2 = "select MaDG, TenDG from DOCGIA where MaDG = '" + cbMaSV.Text + "'";
                mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    ttMaSV.Text = dr["MaDG"].ToString();
                    ttTenSV.Text = dr["TenDG"].ToString();
                }
            }
        }
        public void PhichSach()
        {
            /*txtMaPS.Enabled = !true;
            txtND.Enabled = !true;
            txtST.Enabled = !true;
            txtTTND.Enabled = !true;*/
            btnThemPS.Enabled = true;
            btnXoaPS.Enabled = true;
            btnSuaPS.Enabled = true;
            btnInPS.Enabled = true;
            string query = "select Maphichsach, Nhande, TTND, Sotrang, Manhanvien from PHICHSACH";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvPS.DataSource = dt;
        }

        private void btnThemPS_Click(object sender, EventArgs e)
        {
            /*txtMaPS.Enabled = true;
            txtND.Enabled = true;
            txtST.Enabled = true;
            txtTTND.Enabled = true;*/
            try
            {
                string query = "INSERT INTO PHICHSACH (Maphichsach, Nhande, TTND, Sotrang, Manhanvien) VALUES (@Maphichsach, @Nhande, @TTND, @Sotrang, @Manhanvien)";
                using (SqlCommand command = new SqlCommand(query, mySqlconnection))
                {
                    command.Parameters.AddWithValue("@Maphichsach", txtMaPS.Text);
                    command.Parameters.AddWithValue("@Nhande", txtND.Text);
                    command.Parameters.AddWithValue("@TTND", txtTTND.Text);
                    command.Parameters.AddWithValue("@Sotrang", txtST.Text);
                    command.Parameters.AddWithValue("@Manhanvien", txtMaNV.Text); 

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm phiếu sách thành công.", "Thông báo");
                PhichSach(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phiếu sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSuaPS_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE PHICHSACH SET Nhande = @Nhande, TTND = @TTND, Sotrang = @Sotrang WHERE Maphichsach = @Maphichsach";
                using (SqlCommand command = new SqlCommand(query, mySqlconnection))
                {
                    command.Parameters.AddWithValue("@Maphichsach", txtMaPS.Text);
                    command.Parameters.AddWithValue("@Nhande", txtND.Text);
                    command.Parameters.AddWithValue("@TTND", txtTTND.Text);
                    command.Parameters.AddWithValue("@Sotrang", txtST.Text);

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Sửa phiếu sách thành công.", "Thông báo");
                PhichSach(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa phiếu sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaPS_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvPS.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < dgvPS.Rows.Count)
                {
                    string maPS = dgvPS.Rows[rowIndex].Cells["Maphichsach"].Value.ToString();
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu sách với mã {maPS}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string query = "DELETE FROM PHICHSACH WHERE Maphichsach = @Maphichsach";
                        using (SqlCommand command = new SqlCommand(query, mySqlconnection))
                        {
                            command.Parameters.AddWithValue("@Maphichsach", maPS);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Xóa phiếu sách thành công.", "Thông báo");
                        PhichSach(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phiếu sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvPS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (rowIndex >= 0 && rowIndex < dgvPS.Rows.Count)
                {
                    txtMaPS.Text = dgvPS.Rows[rowIndex].Cells["Maphichsach"].Value.ToString();
                    txtND.Text = dgvPS.Rows[rowIndex].Cells["Nhande"].Value.ToString();
                    txtTTND.Text = dgvPS.Rows[rowIndex].Cells["TTND"].Value.ToString();
                    txtST.Text = dgvPS.Rows[rowIndex].Cells["Sotrang"].Value.ToString();

                    /*txtMaPS.Enabled = false;
                    txtND.Enabled = true;
                    txtST.Enabled = true;
                    txtTTND.Enabled = true;*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin phiếu sách từ DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            string query = "select SV.MaDG, SV.TenDG, SV.CN, S.Tensach,TS.Ngaytra,TS.Tinhtrang from MUON MS join DONGMUON DM on MS.Maphieumuon = DM.Maphieumuon join SACH S on S.Masach = DM.Masach join DOCGIA SV on SV.MaDG = MS.MaDG join TRA TS on TS.MaDG = MS.MaDG WHERE TS.Tinhtrang = 'Quá hạn'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = false;
            lbquahan.Visible = true;
            int totalRows = dgvDSMuon.RowCount - 1;
            lbTong.Text = totalRows.ToString();
        }

        private void btnDangMuon_Click(object sender, EventArgs e)
        {
            string query = "select MS.Maphieumuon, SV.MaDG, SV.TenDG, SV.CN, S.Tensach,MS.Ngaymuon,DM.Ngayhentra from MUON MS join DONGMUON DM on MS.Maphieumuon = DM.Maphieumuon join SACH S on S.Masach = DM.Masach join DOCGIA SV on SV.MaDG = MS.MaDG LEFT JOIN TRA TS ON TS.MaDG = MS.MaDG WHERE TS.Ngaytra IS NULL";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            int totalRows = dgvDSMuon.RowCount - 1;
            lbTong.Text = totalRows.ToString();
        }
        public void thongke()
        {
            string query = "select MS.Maphieumuon, SV.MaDG, SV.TenDG, SV.CN, S.Tensach,MS.Ngaymuon, DM.Ngayhentra,TS.Ngaytra,TS.Tinhtrang from MUON MS join DONGMUON DM on MS.Maphieumuon = DM.Maphieumuon join SACH S on S.Masach = DM.Masach join DOCGIA SV on SV.MaDG = MS.MaDG join TRA TS on TS.MaDG = MS.MaDG where TS.Ngaytra > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            int totalRows = dgvDSMuon.RowCount - 1;
            lbTong.Text = totalRows.ToString();

            NhanVienn();
            SinhVien();
            Sach();
            MuonTraSach();
            LoaiSach();
            TacGia();
            NhaXuatBan();
        }
        public void NhanVienn()
        {
            string sSql1 = "select count(*) from NHANVIEN";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkAdmin.Text = dt.Rows[0][0].ToString();
        }
        public void SinhVien()
        {
            string sSql1 = "select count(*) from DOCGIA";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSinhVien.Text = dt.Rows[0][0].ToString();
        }
        public void Sach()
        {
            string sSql1 = "select count(*) from SACH";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSach.Text = dt.Rows[0][0].ToString();
        }
        public void MuonTraSach()
        {
            string sSql1 = "select count(*) from MUON";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkMuonSach.Text = dt.Rows[0][0].ToString();
        }
        public void LoaiSach()
        {
            try
            {
                string sSql1 = "SELECT COUNT(DISTINCT Loaisach) FROM SACH";
                mySqlCommand = new SqlCommand(sSql1, mySqlconnection);

                object result = mySqlCommand.ExecuteScalar();
                if (result != null)
                {
                    TKLoaiSach.Text = result.ToString();
                }
                else
                {
                    TKLoaiSach.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đếm số loại sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void TacGia()
        {
            try
            {
                string sSql1 = "SELECT COUNT(DISTINCT TenTG) FROM SACH";
                mySqlCommand = new SqlCommand(sSql1, mySqlconnection);

                object result = mySqlCommand.ExecuteScalar();
                if (result != null)
                {
                    TKTacGia.Text = result.ToString();
                }
                else
                {
                    TKTacGia.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đếm số tác giả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void NhaXuatBan()
        {
            try
            {
                string sSql1 = "SELECT COUNT(DISTINCT NXB) FROM SACH";
                mySqlCommand = new SqlCommand(sSql1, mySqlconnection);

                object result = mySqlCommand.ExecuteScalar();
                if (result != null)
                {
                    TKNhaXB.Text = result.ToString();
                }
                else
                {
                    TKNhaXB.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đếm số tác giả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void dgvDSMuon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //string MaSV, msv, tsv, sdt, sdtt;
            //int r = e.RowIndex;
            //MaSV = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            //
            //string sSql2 = "select MaSV, TenSV, SoDienThoai from SinhVien where MaSV = '" + MaSV + "'";
            //mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            //mySqlCommand.ExecuteNonQuery();
            //DataTable dt2 = new DataTable();
            //SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            //da2.Fill(dt2);
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    msv = dr["MaSV"].ToString();
            //    tsv = dr["TenSV"].ToString();
            //    sdt = dr["SoDienThoai"].ToString();
            //    MessageBox.Show(sdt, "Thông Tin Sinh Viên");
            //}
            //sdtt = Convert.ToString(sdt);
            //MessageBox.Show(sdt,"Thông Tin Sinh Viên");
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            this.Hide();
            Login DN = new Login();
            DN.Show();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvTTNV.CurrentRow.Index;
            string MaNV = dgvTTNV.Rows[row].Cells[0].Value.ToString();
            string query3 = "delete from NhanVien where MaNhanVien = '" + MaNV + "'";
            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            NhanVien();
            MessageBox.Show("Xoá thành công.", "Thông báo");
        }      
        private void TrangChu_Resize_1(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in guna2TabControl1.TabPages)
            {
                tabPage.Width = this.ClientSize.Width;
                tabPage.Height = this.ClientSize.Height;
            }
        }
        public void NCC()
        {
            btnThemNCC.Enabled = true;
            btnXoaNCC.Enabled = true;
            btnSuaNCC.Enabled = true;
            btnInNCC.Enabled = true;
            string query = "select NCC.MaNCC, NCC.TenNCC, CC.Malo, DCC.Masach, DCC.Soluongsach, CC.Ngaygiao from NHACUNGCAP NCC join CUNGCAP CC on CC.MaNCC = NCC.MaNCC join DONGCUNGCAP DCC on DCC.Malo = CC.Malo join SACH S on DCC.Masach = S.Masach";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvLoNCC.DataSource = dt;
            string query1 = "select MaNCC, TenNCC from NHACUNGCAP";
            mySqlCommand = new SqlCommand(query1, mySqlconnection);
            SqlDataReader dr1 = mySqlCommand.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(dr1);
            dgvTTNCC.DataSource = dt1;
        }
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            try
            {
                string queryNCC = "INSERT INTO NHACUNGCAP (MaNCC, TenNCC) VALUES (@MaNCC, @TenNCC)";
                string queryCC = "INSERT INTO CUNGCAP (MaLo, MaNCC, Ngaygiao) VALUES (@MaLo, @MaNCC, @Ngaygiao)";
                string queryDCC = "INSERT INTO DONGCUNGCAP (MaLo, Masach, Soluongsach) VALUES (@MaLo, @Masach, @Soluongsach)";

                using (SqlCommand commandNCC = new SqlCommand(queryNCC, mySqlconnection))
                {
                    commandNCC.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    commandNCC.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text);
                    commandNCC.ExecuteNonQuery();
                }

                using (SqlCommand commandCC = new SqlCommand(queryCC, mySqlconnection))
                {
                    commandCC.Parameters.AddWithValue("@MaLo", txtMaLo.Text);
                    commandCC.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    commandCC.Parameters.AddWithValue("@Ngaygiao", cbNG.Value);
                    commandCC.ExecuteNonQuery();
                }

                using (SqlCommand commandDCC = new SqlCommand(queryDCC, mySqlconnection))
                {
                    commandDCC.Parameters.AddWithValue("@MaLo", txtMaLo.Text);
                    commandDCC.Parameters.AddWithValue("@Masach", txtMaSachCC.Text);
                    commandDCC.Parameters.AddWithValue("@Soluongsach", txtSLS.Text);
                    commandDCC.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm nhà cung cấp thành công.", "Thông báo");
                NCC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            try
            {
                string queryNCC = "UPDATE NHACUNGCAP SET TenNCC = @TenNCC WHERE MaNCC = @MaNCC";
                string queryCC = "UPDATE CUNGCAP SET Ngaygiao = @Ngaygiao WHERE MaLo = @MaLo AND MaNCC = @MaNCC";
                string queryDCC = "UPDATE DONGCUNGCAP SET Masach = @Masach, Soluongsach = @Soluongsach WHERE MaLo = @MaLo";

                using (SqlCommand commandNCC = new SqlCommand(queryNCC, mySqlconnection))
                {
                    commandNCC.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    commandNCC.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text);
                    commandNCC.ExecuteNonQuery();
                }

                using (SqlCommand commandCC = new SqlCommand(queryCC, mySqlconnection))
                {
                    commandCC.Parameters.AddWithValue("@MaLo", txtMaLo.Text);
                    commandCC.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    commandCC.Parameters.AddWithValue("@Ngaygiao", cbNG.Value);
                    commandCC.ExecuteNonQuery();
                }

                using (SqlCommand commandDCC = new SqlCommand(queryDCC, mySqlconnection))
                {
                    commandDCC.Parameters.AddWithValue("@MaLo", txtMaLo.Text);
                    commandDCC.Parameters.AddWithValue("@Masach", txtMaSachCC.Text);
                    commandDCC.Parameters.AddWithValue("@Soluongsach", txtSLS.Text);
                    commandDCC.ExecuteNonQuery();
                }
                MessageBox.Show("Sửa nhà cung cấp thành công.", "Thông báo");
                NCC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvLoNCC.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < dgvLoNCC.Rows.Count)
                {
                    string maNCC = dgvLoNCC.Rows[rowIndex].Cells["MaNCC"].Value.ToString();
                    string maLo = dgvLoNCC.Rows[rowIndex].Cells["Malo"].Value.ToString();

                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp với mã {maNCC} và mã lô {maLo}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string queryDCC = "DELETE FROM DONGCUNGCAP WHERE MaLo = @MaLo";
                        string queryCC = "DELETE FROM CUNGCAP WHERE MaLo = @MaLo";
                        string queryNCC = "DELETE FROM NHACUNGCAP WHERE MaNCC = @MaNCC";

                        using (SqlCommand commandDCC = new SqlCommand(queryDCC, mySqlconnection))
                        {
                            commandDCC.Parameters.AddWithValue("@MaLo", maLo);
                            commandDCC.ExecuteNonQuery();
                        }

                        using (SqlCommand commandCC = new SqlCommand(queryCC, mySqlconnection))
                        {
                            commandCC.Parameters.AddWithValue("@MaLo", maLo);
                            commandCC.ExecuteNonQuery();
                        }

                        using (SqlCommand commandNCC = new SqlCommand(queryNCC, mySqlconnection))
                        {
                            commandNCC.Parameters.AddWithValue("@MaNCC", maNCC);
                            commandNCC.ExecuteNonQuery();
                        }

                        MessageBox.Show("Xóa nhà cung cấp thành công.", "Thông báo");
                        NCC();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvTTNCC_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (rowIndex >= 0 && rowIndex < dgvTTNCC.Rows.Count)
                {
                    txtMaNCC.Text = dgvTTNCC.Rows[rowIndex].Cells["MaNCC"].Value.ToString();
                    txtTenNCC.Text = dgvTTNCC.Rows[rowIndex].Cells["TenNCC"].Value.ToString();

                    txtMaNCC.Enabled = false;
                    txtTenNCC.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin nhà cung cấp từ DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvLoNCC_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (rowIndex >= 0 && rowIndex < dgvLoNCC.Rows.Count)
                {
                    txtMaNCC.Text = dgvLoNCC.Rows[rowIndex].Cells["MaNCC"].Value.ToString();
                    txtTenNCC.Text = dgvLoNCC.Rows[rowIndex].Cells["TenNCC"].Value.ToString();
                    txtMaLo.Text = dgvLoNCC.Rows[rowIndex].Cells["Malo"].Value.ToString();
                    txtMaSachCC.Text = dgvLoNCC.Rows[rowIndex].Cells["Masach"].Value.ToString();
                    txtSLS.Text = dgvLoNCC.Rows[rowIndex].Cells["Soluongsach"].Value.ToString();
                    cbNG.Value = Convert.ToDateTime(dgvLoNCC.Rows[rowIndex].Cells["Ngaygiao"].Value);

                    /*txtMaNCC.Enabled = false;
                    txtTenNCC.Enabled = true;
                    txtMaLo.Enabled = true;
                    txtMaSachCC.Enabled = true;
                    txtSLS.Enabled = true;
                    cbNG.Enabled = true;*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin lô cung cấp từ DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnInNCC_Click(object sender, EventArgs e)
        {

        }
    }
}