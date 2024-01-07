
using AppGestionClass.Models;
using Microsoft.Maui.Layouts;
using System.Collections;
using System.Collections.Generic;

namespace AppGestionClass;

public partial class MarkAbsence : ContentPage
{
    IDictionary absenceData = new System.Collections.Generic.Dictionary<int,Absence>();

    AppContext db = new AppContext();
    int idFiliere = -1;

    int idlesson = -1;

	public MarkAbsence()
	{
		InitializeComponent();
        var data = db.Filieres.ToList();
        picker_Filiere.ItemsSource = data.Select(item => item.NameFiliere).ToList();

        picker_Filiere.SelectedIndexChanged += (sender, e) =>
        {
            if (picker_Filiere.SelectedIndex != -1)
            {
                var selectedText = picker_Filiere.SelectedItem.ToString();
                var selectedItem = data.FirstOrDefault(item => item.NameFiliere == selectedText);
                idFiliere= selectedItem.Id;
                getStudentsOfFiliere(idFiliere);
            }
        };


        var data1 = db.Lessons.ToList();
        
        picker_lesson.ItemsSource = data1.Select(item => item.NameLesson).ToList();

        picker_lesson.SelectedIndexChanged += (sender, e) =>
        {
            if (picker_lesson.SelectedIndex != -1)
            {
                var selectedText = picker_lesson.SelectedItem.ToString();
                var selectedItem = data1.FirstOrDefault(item => item.NameLesson == selectedText);
                idlesson = selectedItem.Id;
            }
        };

    }


    private void getStudentsOfFiliere(int id)
    {

        student_list.ItemsSource = db.Etudiants.Where(item=>item.IdFillier==id).ToList();

        student_list.ItemTemplate = new DataTemplate(() =>
        {

            var label = new Label();
            label.SetBinding(Label.TextProperty, "Nom");

            

            var checkBox = new CheckBox();
            checkBox.CheckedChanged += (sender, args) =>
            {
                if (checkBox.BindingContext is Etudiant student)
                {
                    var idEtudiant = student.Id;
                    if(idFiliere!=-1 && idlesson != -1)
                    {
                        Absence abs = new Absence();
                        abs.EtudiantId = idEtudiant;
                        abs.FiliereId = idFiliere;
                        abs.LessonId = idlesson;
                        abs.dateAbsence = DateTime.Now.ToString("yyyy/MM/dd");
                        if (checkBox.IsChecked)
                            abs.statut = 1;
                        else
                            abs.statut = 0;
                        if (!absenceData.Contains(idEtudiant))
                        {
                            absenceData.Add(idEtudiant, abs);
                        }
                        else
                        {
                            absenceData[idEtudiant] = abs;
                        }
                    }

                }


            };


   
            var flex = new FlexLayout
            {

                AlignItems = FlexAlignItems.Center,
                JustifyContent = FlexJustify.SpaceBetween, 
                Children =
                 {
            label,
            checkBox
                }
            };

            return new Frame
            {
                Content = flex
            };

        });
    }

    [Obsolete]
    private void SaveAbsence(object sender, EventArgs e)
    {
        if (idlesson != -1 && idFiliere != -1)
        {
            var x = db.Etudiants.ToList();
            foreach (var id in x.Select(p => p.Id).ToList())
            {
                if (absenceData.Contains(id))
                {
                    Absence a = absenceData[id] as Absence;
                    db.Absences.Add(a);
                }
                else
                {
                    Absence abs = new Absence();
                    abs.statut = 0;
                    abs.EtudiantId = id;
                    abs.FiliereId = idFiliere;
                    abs.LessonId = idlesson;
                    abs.dateAbsence = DateTime.Now.ToString("yyyy/MM/dd");
                    db.Absences.Add(abs);
                }
            }
            db.SaveChanges();
            alert.Text = "Absence ajoutée avec succès";
            alert.TextColor = Color.FromHex("#00f204");
            absenceData.Clear();
            getStudentsOfFiliere(idFiliere);
        }
        else
        {
            alert.Text = "Veuillez choisir d'abord le cours et la filière";
            alert.TextColor = Color.FromHex("#FF0000");
        }
    }

    private void CancelAbsence(object sender, EventArgs e)
    {
        absenceData.Clear();
        getStudentsOfFiliere(idFiliere);
    }


}