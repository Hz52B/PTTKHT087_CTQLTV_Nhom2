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
    public partial class Login : Form
    {
        string Conn = "Data Source=HZ52B;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string TenNV;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrangChu Home = new TrangChu(); // You can pass a placeholder name if needed
            Home.Show();
        }
    }
}
