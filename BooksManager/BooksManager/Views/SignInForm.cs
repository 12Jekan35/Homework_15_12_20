using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManager.Views
{
    public partial class SignInForm : Form
    {
        public event Action<string, string> SignIn;
        public SignInForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loginBox.Text))
                return;
            if (string.IsNullOrEmpty(passwordBox.Text))
                return;
            var login = loginBox.Text;
            var password = passwordBox.Text;
            SignIn(login, password);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
