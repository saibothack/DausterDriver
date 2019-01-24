using DausterDriver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DausterDriver.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
		public MasterPage ()
		{
			InitializeComponent ();
            BindingContext = new MasterViewModel();
        }
	}
}