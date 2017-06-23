using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace tiendaMKT
{
    public partial class DetailsProduct : ContentPage
    {
        

        int cantC = 1;
        int precioC;

		int cantM = 1;
		int precioM;

        public DetailsProduct(JToken product)
        {
            InitializeComponent();
            //this.product = product;
            //Debug.WriteLine("DETAILS: " + product["nombre"] + " " + product["imagen"] );
            imagenView.Source = "http://area51.pe/sol/" + product["imagen"];



            precioC = (int)product["precioCiento"];
            precioM = (int)product["precioMillar"];


            codigo.Text = product["codigo"].ToString();
            ciento.Text = "S/ " + product["precioCiento"].ToString();
            millar.Text = "S/ " + product["precioMillar"].ToString();

            labelprecioC.Text = ciento.Text;
            labelprecioM.Text = millar.Text;

            sumaC.Clicked += (sender, e) => {
                cantC++;
                if(cantC > 1){
                    labelC.Text = "" + cantC + " cientos";
                }else{
                    labelC.Text = "Costo x 1 ciento";
                    cantC = 1;
                }

                labelprecioC.Text = "S/ " + cantC * precioC;

            };

			restaC.Clicked += (sender, e) =>
			{
                cantC--;
				if (cantC > 1)
				{
					labelC.Text = "" + cantC + " cientos";
				}
				else
				{   
					labelC.Text = "Costo x 1 ciento";
                    cantC = 1;
				}

				labelprecioC.Text = "S/ " + cantC * precioC;

			};


			sumaM.Clicked += (sender, e) =>
			{
				cantM++;
				if (cantM > 1)
				{
					labelM.Text = "" + cantM + " millar";
				}
				else
				{
					labelC.Text = "Costo x 1 millar";
					cantC = 1;
				}

				labelprecioM.Text = "S/ " + cantM * precioM;

			};

			restaM.Clicked += (sender, e) =>
			{
				cantM--;
				if (cantM > 1)
				{
					labelM.Text = "" + cantM + " millar";
				}
				else
				{
					labelM.Text = "Costo x 1 millar";
					cantM = 1;
				}

				labelprecioM.Text = "S/ " + cantM * precioM;

			};


            comprar.Clicked += async (sender, e) => {

                string txtCiento = "1 ciento ";
                if (cantC > 1)
                {
                    txtCiento = cantC + " cientos: ";
                }

				string txtMillar = "1 millar ";
				if (cantM > 1)
				{
					txtMillar = cantM + " millares: ";
				}

                string msgCiento = txtCiento + labelprecioC.Text;
                string msgMillar = txtMillar + labelprecioM.Text;

				var action = await DisplayActionSheet(product["nombre"] + ": ¿Qué cantidad desea comprar usted?", 
                                   "Cancelar", 
                                   null,
                                   msgCiento, 
                                   msgMillar
                                  );

                if (action == msgCiento)
                {
                    Debug.WriteLine("desde ciento");

                    openPay();

                }else{
					Debug.WriteLine("desde millar");
                    openPay();
                }
                    



            };

        }

        private void openPay()
        {
			PagePay detailPage = new PagePay();
			detailPage.Title = "Pasarale de Pago";
            Navigation.PushAsync(detailPage);
        }
    }
}
