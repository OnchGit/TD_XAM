using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourSquare.Auxilliary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentCell : ViewCell
	{
		public CommentCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty AuthorProperty =
        BindableProperty.Create("Author", typeof(string), typeof(CommentCell), "");
        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        public static readonly BindableProperty DateProperty =
        BindableProperty.Create("Date", typeof(string), typeof(CommentCell), "");
        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text", typeof(string), typeof(CommentCell), "");
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}