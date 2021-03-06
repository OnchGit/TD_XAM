﻿using FourSquare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourSquare.Auxilliary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaceViewCell : ViewCell
	{
		public PlaceViewCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty IdProperty =
        BindableProperty.Create("PlaceId", typeof(int), typeof(PlaceViewCell), 0);
        public int PlaceId
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }


        public static readonly BindableProperty TitleProperty =
        BindableProperty.Create("Title", typeof(string), typeof(PlaceViewCell), "");
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly BindableProperty DescProperty =
        BindableProperty.Create("Description", typeof(string), typeof(PlaceViewCell), "");
        public string Description
        {
            get { return (string)GetValue(DescProperty); }
            set { SetValue(DescProperty, value); }
        }

        


        public static readonly BindableProperty ImgProperty =
        BindableProperty.Create("Img", typeof(int), typeof(PlaceViewCell), 0);
        public int Img
        {
            get { return (int)GetValue(ImgProperty); }
            set { SetValue(ImgProperty, value); }
        }

        public static readonly BindableProperty ImgSrcProperty =
        BindableProperty.Create("ImgSrc", typeof(Image), typeof(PlaceViewCell), new Image());
        public Image ImgSrc
        {
            get { return (Image)GetValue(ImgSrcProperty); }
            set { SetValue(ImgSrcProperty, value); }
        }
    }
}