using AppGestionClass.Models;

namespace AppGestionClass;

public partial class Inscription : ContentPage
{
    AppContext db = new AppContext();

    public Inscription()
    {
        InitializeComponent();

    }

    private async void SignUp(object sender, EventArgs e)
    {
        string name = prof_name.Text;
        string password = prof_password.Text;
        string passwordConfirmation = prof_passwordConfirm.Text;
        if (name == null || password == null || passwordConfirmation == null)
        {
            alert.Text = "Veuillez remplir tous les champs!";
        }
        else
        {
            if (passwordConfirmation.Equals(password))
            {
                Proff pr = new Proff();
                pr.NameProf = name.ToString();
                pr.Password = password.ToString();
                db.Proffs.Add(pr);
                db.SaveChanges();
                await DisplayAlert("Succès", "Inscription réussie", "OK");
                clear();
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                alert.Text = "La confirmation du mot de passe a échoué!";
            }
        }
    }

    private async void Cancel(object sender, EventArgs e)
    {
        clear();
        await Navigation.PushAsync(new MainPage());
    }

    private void clear()
    {
        prof_name.Text = "";
        prof_password.Text = "";
        prof_passwordConfirm.Text = "";
    }

    private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        prof_password.IsPassword = !e.Value;
        prof_passwordConfirm.IsPassword = !e.Value;
    }

}
