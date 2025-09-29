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
using System.Security.Cryptography;
using System.IO;

namespace baitap001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            string dbPath = "TaiKhoan.db";
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    using (SQLiteCommand command = new SQLiteCommand(@"
                        CREATE TABLE TaiKhoan (
                            username TEXT PRIMARY KEY NOT NULL,
                            email TEXT UNIQUE NOT NULL,
                            password TEXT NOT NULL
                        );
                    ", connection))
                    {
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi database: " + ex.Message);
                        }
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaiKhoanConnectionString"].ConnectionString;
            string hashedPassword = HashPassword(password);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM TaiKhoan WHERE username = @username AND password = @password", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    try
                    {
                        connection.Open();
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi database: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        private void lblTK_Click(object sender, EventArgs e)
        {

        }

        private void lblMK_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (AuthenticateUser(txtDN.Text, txtMK.Text))
            {
                MessageBox.Show("Đăng nhập thành công !");
                this.Hide();
                hienthi f = new hienthi(txtDN.Text);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu , vui lòng nhập lại");
            }
        }

        private void btnĐK_Click(object sender, EventArgs e)
        {
            dki f = new dki();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void chkHide_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHide.Checked)
            {
                txtMK.PasswordChar = '\0';
                picShow.Hide();
                picHide.Show();
            }
            else
            {
                txtMK.PasswordChar = '*';
                picShow.Show();
                picHide.Hide();
            }
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '*';
        }
    }
}
