using AppGestionClass.Models;
using Microsoft.Maui.Layouts;

namespace AppGestionClass
{
    public partial class Search : ContentPage
    {
        AppContext db = new AppContext();
        int idFiliere = -1;
        int idlesson = -1;
        Absence abs = null;

        public Search()
        {
            InitializeComponent();

            action2.IsVisible = false;

            var data = db.Filieres.ToList();
            searsh_filiere.ItemsSource = data.Select(item => item.NameFiliere).ToList();

            searsh_filiere.SelectedIndexChanged += (sender, e) =>
            {
                if (searsh_filiere.SelectedIndex != -1)
                {
                    var selectedText = searsh_filiere.SelectedItem.ToString();
                    var selectedItem = data.FirstOrDefault(item => item.NameFiliere == selectedText);
                    idFiliere = selectedItem.Id;

                }
            };


            var data1 = db.Lessons.ToList();

            searsh_lesson.ItemsSource = data1.Select(item => item.NameLesson).ToList();

            searsh_lesson.SelectedIndexChanged += (sender, e) =>
            {
                if (searsh_lesson.SelectedIndex != -1)
                {
                    var selectedText = searsh_lesson.SelectedItem.ToString();
                    var selectedItem = data1.FirstOrDefault(item => item.NameLesson == selectedText);
                    idlesson = selectedItem.Id;
                }
            };
        }

        [Obsolete]
        private void HideLabelAfterDelay(Label label , int delayInSeconds = 2)
        {
            Device.StartTimer(TimeSpan.FromSeconds(delayInSeconds), () =>
            {
                label.Text = "";
                return false;
            });
        }

        [Obsolete]
        private void searshFun(object sender, EventArgs e)
        {
            if (idFiliere != -1 && idlesson != -1 && !string.IsNullOrEmpty(search_nameStudent.Text))
            {
                if (int.TryParse(search_nameStudent.Text, out int studentCne))
                {
                    var student = db.Etudiants.SingleOrDefault(p => p.Cne == studentCne);

                    string dataToday = DateTime.Now.ToString("yyyy/MM/dd");
                    if (student != null)
                    {
                        var x = db.Absences.FirstOrDefault(p => p.FiliereId == idFiliere
                                                            && p.LessonId == idlesson
                                                            && p.EtudiantId == student.Id
                                                            && p.dateAbsence.Equals(dataToday));

                        if (x == null)
                        {
                            alert.Text = "Vous n'avez pas encore saisi l'absence pour aujourd'hui.";
                            alert.TextColor = Color.FromHex("#FF0000");
                            alert.FontSize = 15;
                            HideLabelAfterDelay(alert);
                        }
                        else
                        {
                            search_nameStudent.IsVisible = false;
                            searsh_filiere.IsVisible = false;
                            searsh_lesson.IsVisible = false;
                            action.IsVisible = false;
                            action2.IsVisible = true;

                            List<Etudiant> l = new List<Etudiant>();
                            l.Add(student);

                            student_list.ItemsSource = l;
                            student_list.ItemTemplate = new DataTemplate(() =>
                            {
                                var label = new Label();
                                label.SetBinding(Label.TextProperty, "Nom");
                                var checkBox = new CheckBox();

                                if (x.statut == 1)
                                {
                                    checkBox.IsChecked = true;
                                }

                                checkBox.CheckedChanged += (sender, args) =>
                                {
                                    if (checkBox.BindingContext is Etudiant student)
                                    {
                                        var idEtudiant = student.Id;
                                        if (idFiliere != -1 && idlesson != -1)
                                        {
                                            abs = x;
                                            abs.statut = checkBox.IsChecked ? 1 : 0;
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
                        label1.Text = "";
                        label2.Text = "";
                    }
                    else
                    {
                        alert.Text = "Aucun étudiant trouvé";
                        alert.TextColor = Color.FromHex("#FF0000");
                        HideLabelAfterDelay(alert);

                    }
                }
                else
                {
                    alert.Text = "Veuillez entrer un CNE valide!";
                    alert.TextColor = Color.FromHex("#FF0000");
                    HideLabelAfterDelay(alert);

                }
            }
        }


        private async void searshcancel(object sender, EventArgs e)
        {
            search_nameStudent.Text = "";
            await Navigation.PushAsync(new MainPage());
        }

        public void SaveAbsence(object sender, EventArgs e)
        {
            if (abs == null)
            {
                alert.Text = "Aucune modification apportée";
                alert.TextColor = Color.FromHex("#FF0000");
                HideLabelAfterDelay(alert);

            }
            else
            {
                db.Absences.Update(abs);
                db.SaveChanges();
                alert.Text = "Absence mise à jour avec succès";
                alert.TextColor = Color.FromHex("#00f204");
                HideLabelAfterDelay(alert);

            }
        }

        public void CancelAbsence(object sender, EventArgs e)
        {
            search_nameStudent.IsVisible = true;
            searsh_filiere.IsVisible = true;
            searsh_lesson.IsVisible = true;
            action.IsVisible = true;
            action2.IsVisible = false;
            student_list.IsVisible = false;
            label1.Text = "Sélectionnez une filière";
            label2.Text = "Sélectionnez un cours";
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
                    search_nameStudent.Text = "";
                    alert.Text = "";
                    return false;
                });

                alert.Text = "Veuillez entrer un nombre !";
                alert.TextColor = Color.FromHex("#FF0000");
            }
        }
    }
}
