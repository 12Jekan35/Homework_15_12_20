using BooksManager.Models;
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
    public partial class BooksForm : Form
    {
        public event Action AddItem;
        public event Action RefreshAll;
        public event Action<Book> EditItem;
        public event Action<int> DeleteItem;
        public BooksForm(string role)
        {
            InitializeComponent();
            booksView.AutoGenerateColumns = false;

            if (role != "admin")
            {
                deleteButton.Visible = false;
                addButton.Visible = false;
                editButton.Visible = false;
            }
        }
        public void ShowData(List<Book> books)
        {
            booksView.DataSource = books;
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (booksView.SelectedRows.Count < 1)
                return;
            int id = (booksView.SelectedRows[0].DataBoundItem as Book).Id;
            DeleteItem(id);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (booksView.SelectedRows.Count < 1)
                return;
            var item = booksView.SelectedRows[0].DataBoundItem as Book;
            EditItem(item);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddItem();
        }
    }
}
