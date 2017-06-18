using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace tiendaMKT
{
    public partial class ListCategory : ContentPage
    {

        JArray _data = null;

        public ListCategory(JArray data)
        {
            InitializeComponent();

            this._data = data;

            //Debug.WriteLine("******data: " +  data );


			var people = new List<Product>();

			for (int i = 0; i < data.Count; i++)
			{

				

				Product tmp = new Product
				{
					nombre = data[i]["nombre"].ToString(),
					imagen = "http://area51.pe/sol/" + data[i]["imagen"].ToString(),
                    pCiento = (int)data[i]["precioMillar"],
                    colores = (JArray)data[i]["colores"]
				};

				people.Add(tmp);
			}


			list.ItemsSource = people;

			list.ItemSelected += (sender, args) =>
				{
					((ListView)sender).SelectedItem = null;

					if (args.SelectedItem == null)
					{
						return;
					}

					//Debug.WriteLine("Product: " + (list.SelectedItem as Product));
                    
                var index = (  list.ItemsSource as List<Product>).IndexOf(args.SelectedItem as Product);


                    //Debug.WriteLine("_data[index]: " + _data[index]  );



                    DetailsProduct detailPage = new DetailsProduct( _data[index] );
				    detailPage.Title = (list.SelectedItem as Product).nombre;
					    Navigation.PushAsync(detailPage);

				};

		}



    }



   

}
