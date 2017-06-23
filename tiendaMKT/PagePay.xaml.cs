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
                DisplayAlert("Gracias por su compra", "En breve, le estaremos enviando un correo electrónico con la factura y los detalles de la compra." , "ok");

                //Navigation.PushModalAsync(  new tiendaMKTPage()  );
                Navigation.PopToRootAsync();


            };

        }
    }
}
