using ICMXamarin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ICMXamarin.Views
{
    public class FotoPage: ContentPage
    {
        FotoPageVM vm = new FotoPageVM();
        Button bFoto;
        public FotoPage()
        {
            BindingContext = vm;
            Title = "Fotos";
            
            bFoto = new Button
            {
                Text = "Tirar Foto",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var camera = new CameraPreview
            {
                Camera = CameraOptions.Rear,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Content = new StackLayout
            {
                Padding = 30,
                Children =
                {
                    camera
                }
            };
        }
    }
}
