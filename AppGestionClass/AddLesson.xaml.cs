using AppGestionClass.Models;

namespace AppGestionClass
{
    public partial class AddLesson : ContentPage
    {
        AppContext db = new AppContext();

        public AddLesson()
        {
            InitializeComponent();
        }

        private void ajouterLesson(object sender, EventArgs e)
        {
            string lessonName = lesson_name.Text;
            if (lessonName == null || lessonName.Equals(""))
            {
                alert.Text = "Veuillez entrer le nom du cours.";
                alert.TextColor = Color.FromHex("#FF0000");
            }
            else
            {
                if (db.Lessons.Any(l => l.NameLesson == lessonName))
                {
                    alert.Text = "Le nom du cours existe déjà.";
                    alert.TextColor = Color.FromHex("#FF0000"); 
                }
                else
                {
                    Lesson ls = new Lesson();
                    ls.NameLesson = lessonName;
                    db.Lessons.Add(ls);
                    db.SaveChanges();
                    alert.Text = "Cours ajouté avec succès";
                    alert.TextColor = Color.FromHex("#00f204");
                    lesson_name.Text = "";
                }
            }
        }
    }
}
