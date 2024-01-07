namespace AppGestionClass;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}


	private async void addLesson(object sender,EventArgs e)
	{
        await Navigation.PushAsync(new AddLesson());
    }

    private async void AddStudent(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddStudent());
    }

    private async void markAbsence(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MarkAbsence());
    }
    private async void logout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void SearchFun(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Search());
    }

    private async void GoToPageInformation(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInformation());
    }

    private async void addFiliere(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddFiliere());
    }


 

}