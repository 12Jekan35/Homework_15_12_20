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
    public partial class BookEditorForm : Form
    {
        private int Id;
        public Book Item
        {
            get
            {
                return new Book
                {
                    Id = Id,
                    Title = titleBox.Text,
                    Pages = (int) pagesBox.Value,
                    Genre = genreBox.Text,
                    Authors = authorsBox.Text
                };
            }
        }
        public BookEditorForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Book book)
        {
            Id = book.Id;
            titleBox.Text = book.Title;
            pagesBox.Value = book.Pages;
            genreBox.Text = book.Genre;
            authorsBox.Text = book.Authors;

            return ShowDialog();
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(titleBox.Text))
                return;
            if (string.IsNullOrEmpty(genreBox.Text))
                return;
            if (string.IsNullOrEmpty(authorsBox.Text))
                return;
            
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
