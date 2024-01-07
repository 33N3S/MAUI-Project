using AppGestionClass.Models;

namespace AppGestionClass
{
    public partial class MainPage : ContentPage
    {
        AppContext db = new AppContext();

        public MainPage()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
        }

        private async void Login(object sender, EventArgs e)
        {
            string name = prof_name.Text;
            string password = prof_password.Text;

            if (name == null || password == null)
            {
                alert.Text = "Veuillez remplir tous les champs";
                alert.TextColor = Color.FromHex("#FF0000");

            }
            else
            {
                Proff prf = db.Proffs.FirstOrDefault(p => p.NameProf.Equals(name) && p.Password.Equals(password));
                if (prf == null)
                {
                    alert.Text = "Mot de passe ou Nom est incorrect";
                    alert.TextColor = Color.FromHex("#FF0000");
                }
                else
                {
                    await Navigation.PushAsync(new Home());
                }
            }
        }

        private async void SignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inscription());
        }

        private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            prof_password.IsPassword = !e.Value;
        }

    }
   
}
