using App1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Storm.Mvvm;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{

		public MainPage ()
		{         
			InitializeComponent ();
            Console.WriteLine("MAIN" + " " + TokenService.GetAccessToken());
        }
	}
}