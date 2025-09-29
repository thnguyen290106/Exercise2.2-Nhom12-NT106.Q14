using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Configuration;

namespace baitap001
{
    public partial class hienthi : Form
    {
        private string currentUsername;

        public hienthi()
        {
            InitializeComponent();
        }

        public hienthi(string username)
        {
            InitializeComponent();
            this.currentUsername = username;
        }

        private string GetUserEmail(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaiKhoanConnectionString"].ConnectionString;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT email FROM TaiKhoan WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        return result?.ToString() ?? "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi lấy thông tin: " + ex.Message);
                        return "";
                    }
                }
            }
        }
        private void hienthi_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentUsername))
            {
                lblHienTK.Text = currentUsername;
                lblHienEmail.Text = GetUserEmail(currentUsername);
            }
        }
        private void lblHienTK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentUsername))
            {
                lblHienTK.Text = currentUsername;
            }
        }
        private void lblHienEmail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentUsername))
            {
                lblHienEmail.Text = GetUserEmail(currentUsername);
            }
        }
    }
}
