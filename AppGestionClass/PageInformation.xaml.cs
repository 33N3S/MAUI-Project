namespace AppGestionClass;

public partial class PageInformation : ContentPage
{
    AppContext db = new AppContext();

	public PageInformation()
	{
		InitializeComponent();

        infor_nameStudent.TextChanged += OnEditorTextChanged;
       
    }
    private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {

        if (!string.IsNullOrEmpty(infor_nameStudent.Text))
        {
            if (int.TryParse(infor_nameStudent.Text, out int enteredValue))
            { 

                var student = db.Etudiants.FirstOrDefault(p => p.Cne.Equals(enteredValue));

            if (student != null)
            {
                var absencesCounts = db.Absences
                    .Where(p => p.EtudiantId == student.Id)
                    .GroupBy(p => p.statut)
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                int presenceCount = absencesCounts.FirstOrDefault(a => a.Status == 0)?.Count ?? 0;
                int absenceCount = absencesCounts.FirstOrDefault(a => a.Status == 1)?.Count ?? 0;

                presence.Text = presenceCount.ToString();
                absence.Text = absenceCount.ToString();
            }
            else
            {
                presence.Text = "";
                absence.Text = "";
            } 
        }
        }
        else
        {
            presence.Text = "";
            absence.Text = "";
        }
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
                infor_nameStudent.Text = "";
                alert.Text = "";
                return false; 
            });

            alert.Text = "Veuillez entrer un nombre!";
            alert.TextColor = Color.FromHex("#FF0000");
        }
        
    }


}