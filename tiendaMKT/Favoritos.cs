using System;

using Xamarin.Forms;

namespace tiendaMKT
{
    public class Favoritos : ContentPage
    {
        public Favoritos()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

