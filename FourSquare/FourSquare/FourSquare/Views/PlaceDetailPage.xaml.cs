using FourSquare.Services;
using FourSquare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourSquare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaceDetailPage : ContentPage
	{
		public PlaceDetailPage ()
		{

            BindingContext = new PlaceDetailPageViewModel();           
			InitializeComponent ();
            
		}
	}
}