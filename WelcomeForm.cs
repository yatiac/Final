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
    public partial class WelcomeForm : Form
    {
        public string UserName { get; set; }
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {

            label1.Text = "Welcome " + UserName;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPw = txtOldPW.Text;
            string newPw = txtNewPW.Text;
            string confirmPw = txtConfirmNewPW.Text;
            string newUser = txtNewUser.Text;
            txtNewUser.Text = "";
            txtNewPW.Text = "";
            txtOldPW.Text = "";
            txtConfirmNewPW.Text = "";

            if (!(string.IsNullOrEmpty(oldPw) || string.IsNullOrEmpty(newPw) || string.IsNullOrEmpty(confirmPw) || string.IsNullOrEmpty(newUser)))
            {
                string hashedPW = RafaEcnrypt.Rafacrypt(oldPw, short.Parse(UserName.Length.ToString()));
                string storedPW = string.Empty;
                string storedUser = string.Empty;
                using (StreamReader reader = new StreamReader("user.login"))
                {
                    storedUser = reader.ReadLine();
                    storedPW = reader.ReadLine();
                }
                if (hashedPW == storedPW)
                {
                    if (newPw == confirmPw)
                    {
                        using(TextWriter tw = new StreamWriter("user.login",false))
                        {
                            tw.WriteLine(newUser);
                            tw.Write(RafaEcnrypt.Rafacrypt(newPw,short.Parse(newUser.Length.ToString())));
                        }
                        
                        if(MessageBox.Show("Password and Login has been changed succesfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                        {
                            this.Hide();
                            LoginForm lf = new LoginForm();
                            lf.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("New password doesn't match with confirmation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The old Password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("One or many fields are empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
