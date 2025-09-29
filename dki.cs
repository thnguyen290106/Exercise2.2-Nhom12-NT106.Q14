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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.SQLite;
using System.Configuration;
using System.Security.Cryptography;

namespace baitap001
{
    public partial class dki : Form
    {
        public dki()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
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

        private bool IsEmailExists(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaiKhoanConnectionString"].ConnectionString;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM TaiKhoan WHERE email = @email", connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    try
                    {
                        connection.Open();
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi database: " + ex.Message);
                        return true;
                    }
                }
            }
        }

        private bool IsUsernameExists(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaiKhoanConnectionString"].ConnectionString;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM TaiKhoan WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    try
                    {
                        connection.Open();
                        long count = (long) command.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi database: " + ex.Message);
                        return true;
                    }
                }
            }
        }

        private bool RegisterUser(string username, string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaiKhoanConnectionString"].ConnectionString;
            string hashedPassword = HashPassword(password);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO TaiKhoan (username, email, password) VALUES (@username, @email, @password)", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi đăng ký: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        private void btnĐK_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtDKTK.Text == "")
            {
                errorProvider1.SetError(txtDKTK, "Vui lòng nhập tài khoản");
            }
            else if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Vui lòng nhập email");
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Email không hợp lệ");
            }
            else if (txtMK.Text == "")
            {
                errorProvider1.SetError(txtMK, "Vui lòng nhập mật khẩu");
            }
            else if (txtNLMK.Text == "")
            {
                errorProvider1.SetError(txtNLMK, "Vui lòng nhập lại mật khẩu");
            }
            else if (txtMK.MaxLength < 8)
            {
                errorProvider1.SetError(txtMK, "Mật khẩu phải lớn hơn 8 ký tự");
            }
            else if (txtMK.Text != txtNLMK.Text)
            {
                errorProvider1.SetError(txtNLMK, "Mật khẩu không khớp");
            }
            else if (txtMK.Text.Length < 8)
            {
                errorProvider1.SetError(txtMK, "Mật khẩu phải lớn hơn 8 ký tự");
            }
            else if (IsEmailExists(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Email đã tồn tại");
            }
            else if (IsUsernameExists(txtDKTK.Text))
            {
                errorProvider1.SetError(txtDKTK, "Tài khoản đã tồn tại");
            }
            else if (txtDKTK.Text != "" && txtMK.Text != "" && txtNLMK.Text != "" && txtMK.Text == txtNLMK.Text && txtMK.Text.Length >= 8 && txtEmail.Text != "" && IsValidEmail(txtEmail.Text) && !IsUsernameExists(txtDKTK.Text))
            {
                if (RegisterUser(txtDKTK.Text, txtEmail.Text, txtMK.Text))
                {
                    MessageBox.Show("Đăng kí thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại");
                }
            }
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '*';
        }

        private void txtNLMK_TextChanged(object sender, EventArgs e)
        {
            txtNLMK.PasswordChar = '*';
        }

        private void chkMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMK.Checked)
            {
                txtMK.PasswordChar = '\0';
                picShow1.Hide();
                picHide1.Show();
            }
            else
            {
                txtMK.PasswordChar = '*';
                picShow1.Show();
                picHide1.Hide();
            }
        }

        private void chkNLMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNLMK.Checked)
            {
                txtNLMK.PasswordChar = '\0';
                picShow2.Hide();
                picHide2.Show();
            }
            else
            {
                txtNLMK.PasswordChar = '*';
                picShow2.Show();
                picHide2.Hide();
            }
        }
    }
}