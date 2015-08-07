using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rafael.Echeverria.Criptografia;
using System.IO;

namespace Final
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pw = txtPW.Text;
            txtPW.Text = "";
            txtUser.Text = "";
            txtUser.Focus();
            if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(pw))
            {
                if (validateLogin(user, pw))
                {
                    WelcomeForm welcomeForm = new WelcomeForm();
                    welcomeForm.UserName = user;
                    welcomeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User or Password is not Valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User or Password is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool validateLogin(string user, string pwd)
        {
            bool result = false;

            string hashedInput = RafaEcnrypt.Rafacrypt(pwd, short.Parse(user.Length.ToString()));

            string storedPW = string.Empty;

            using (StreamReader reader = new StreamReader("user.login"))
            {
                storedPW = reader.ReadLine();
            }

            result = hashedInput == storedPW;

            return result;
        }
    }

}
