using BooksManager.Services;
using BooksManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Controllers
{
    public class SignInController
    {
        private ProgramContext context;
        private SignInForm form;
        public SignInController(SignInForm signIn, ProgramContext programContext)
        {
            form = signIn;
            context = programContext;

            form.SignIn += SignInHandler;
        }
        private void SignInHandler(string login, string password)
        {
            bool result = context.Authorization.VerifyPassword(login, password);
            if (!result)
                return;
            var newForm = new BooksForm(context.Authorization.Me.Role);
            var controller = new BooksController(newForm, context);
            form.Hide();
            newForm.ShowDialog();
            form.Show();
        }
    }
}
