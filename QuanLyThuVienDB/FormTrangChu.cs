using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienDB
{
    public partial class FormTrangChu : Form
    {
        private string _user;
        public FormTrangChu()
        {
            InitializeComponent();
        }
        public FormTrangChu(string user)
            : this()
        {
            _user = user;
        }

        private void quảnLýSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new FormQuanLySach(_user));
        }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            Text = "Trang chủ - User : " + _user;
        }
        private void CloseAllMdiChildsForm(Form frmChild)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            frmChild.MdiParent = this;
            frmChild.Show();
        }

        private void độcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new FormDocGia(_user));
        }

        private void mượnTrảSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new FormMuonTraSach(_user));
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new FormQuanLyNhanVien(_user));
        }

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new FormBaoCaoThongKe(_user));
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new FormGioiThieu(_user));
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildsForm(new DoiMatKhau());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            this.Hide();
            Login DN = new Login();
            DN.Show();
        }
    }
}
