using AppGestionClass.Models;

namespace AppGestionClass;

public partial class AddFiliere : ContentPage

{
    AppContext db = new AppContext();

	public AddFiliere()
	{
		InitializeComponent();
	}
    private void ajouterFiliere(object sender, EventArgs e)
    {
        string filiereName = filiere_name.Text;

        if (filiereName == null || filiereName.Equals(""))
        {
            alert.Text = "Veuillez entrer le nom de la fili�re!";
            alert.TextColor = Color.FromHex("#FF0000");
        }
        else
        {
            if (db.Filieres.Any(f => f.NameFiliere == filiereName))
            {
                alert.Text = "Le nom de la fili�re existe d�j�!";
                alert.TextColor = Color.FromHex("#FF0000");
            }
            else
            {
                Filiere filiere = new Filiere();
                filiere.NameFiliere = filiereName;

                db.Filieres.Add(filiere);
                db.SaveChanges();

                alert.Text = "Fili�re ajout�e avec succ�s";
                alert.TextColor = Color.FromHex("#00f204");

                clear();
            }

        }
        
    }
    private void clear()
    {
        filiere_name.Text = "";

    }

}