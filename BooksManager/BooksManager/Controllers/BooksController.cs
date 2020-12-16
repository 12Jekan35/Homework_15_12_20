using BooksManager.Models;
using BooksManager.Services;
using BooksManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManager.Controllers
{
    public class BooksController
    {
        private ProgramContext context;
        private BooksForm form;
        public BooksController(BooksForm form, ProgramContext programContext)
        {
            this.form = form;
            context = programContext;

            this.form.RefreshAll += RefreshAllHandler;
            this.form.AddItem += AddItemHandler;
            this.form.EditItem += EditItemHandler;
            this.form.DeleteItem += DeleteItemHandler;
        }
        private void RefreshAllHandler()
        {
            var list = context.Books.GetAll();
            form.ShowData(list);
        }
        private void AddItemHandler()
        {
            var editor = new BookEditorForm();
            var result = editor.ShowDialog();
            if (result != DialogResult.OK)
                return;
            context.Books.AddItem(editor.Item);
            RefreshAllHandler();
        }
        private void EditItemHandler(Book book)
        {
            var editor = new BookEditorForm();
            var result = editor.ShowDialog(book);
            if (result != DialogResult.OK)
                return;
            context.Books.UpdateItem(editor.Item);
            RefreshAllHandler();
        }
        private void DeleteItemHandler(int id)
        {
            context.Books.DeleteItem(id);
            RefreshAllHandler();
        }

    }
}
