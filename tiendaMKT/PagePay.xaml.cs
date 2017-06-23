using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace tiendaMKT
{
    public partial class PagePay : ContentPage
    {
        public PagePay()
        {
            InitializeComponent();

            btnPagar.Clicked += (sender, e) => {
                DisplayAlert("Gracias por su compra", "CONFIRMADO" , "ok");

                //Navigation.PushModalAsync(  new tiendaMKTPage()  );
                Navigation.PopToRootAsync();


            };

        }
    }
}
