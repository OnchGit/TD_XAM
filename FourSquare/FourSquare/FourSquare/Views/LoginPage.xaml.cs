using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourSquare.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourSquare
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            BindingContext = new LoginPageViewModel();
            InitializeComponent ();
		}
	}
}