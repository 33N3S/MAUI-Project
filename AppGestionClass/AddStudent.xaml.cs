using AppGestionClass.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppGestionClass;

public partial class AddStudent : ContentPage
{
    AppContext db = new AppContext();
    int idFilliere = -1;
	public AddStudent()
	{
		InitializeComponent();


        var data = db.Filieres.ToList();
        picker.ItemsSource = data.Select(item => item.NameFiliere).ToList();

        picker.SelectedIndexChanged += (sender, e) =>
        {
            if (picker.SelectedIndex != -1)
            {
                var selectedText = picker.SelectedItem.ToString();
                var selectedItem = data.FirstOrDefault(item => item.NameFiliere == selectedText);
                idFilliere = selectedItem.Id;
            }
        };
    }

    private void addEtudiant(object sender,EventArgs e)
    {
        string cne = etudiant_cne.Text;
        string firstname = etudiant_firstname.Text;
        string lastname=etudiant_lassstname.Text;
        string email = etudiant_email.Text;
        string phone = etudiant_phone.Text;
        if(cne==null || cne.Equals("")
            || firstname==null || firstname.Equals("")
            || lastname==null  || lastname.Equals("")
            || email==null || email.Equals("")
            || phone==null || phone.Equals("")
             || idFilliere == -1)
        {
            alert.Text = "Veuillez remplir tous les champs!";
            alert.TextColor = Color.FromHex("#FF0000");

        }
        else if (db.Etudiants.Any(l => l.Cne == Convert.ToInt32(cne)))
        {
            alert.Text = "Le CNE existe déjà.";
            alert.TextColor = Color.FromHex("#FF0000");
        }
        else
        {
            Etudiant et = new Etudiant();
            et.Nom = firstname;
            et.Cne = Convert.ToInt32(cne);
            et.Prenom = lastname;
            et.Phone = phone;
            et.Email = email;
            et.IdFillier = idFilliere;
            db.Etudiants.Add(et);
            db.SaveChanges();
            alert.Text="Etudiant ajouté avec succés";
            alert.TextColor = Color.FromHex("#00f204");

            clear();
           
        }

    }

    private async void cancel(object sender,EventArgs e)
    {
        clear();
        await Navigation.PushAsync(new Home()) ;
      
    }

    private void clear()
    {
        etudiant_cne.Text = "";
        etudiant_firstname.Text = "";
        etudiant_lassstname.Text = "";
        etudiant_email.Text = "";
        etudiant_phone.Text = "";
    }
    [Obsolete]
    private void OnNameStudentTextChanged(object sender, TextChangedEventArgs e)
    {
        string numericPattern = "^[0-9]*$";

        string enteredText = e.NewTextValue;

        if (!System.Text.RegularExpressions.Regex.IsMatch(enteredText, numericPattern))
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.8), () =>
            {
                etudiant_cne.Text = "";
                alert.Text = "";
                return false;
            });

            alert.Text = "Veuillez entrer un nombre!";
            alert.TextColor = Color.FromHex("#FF0000");
        }

    }
}
