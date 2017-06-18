using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace tiendaMKT
{
    public partial class tiendaMKTPage : ContentPage
    {


        JArray arrCourse;



        public tiendaMKTPage()
        {
            InitializeComponent();
            Title = "Relación de Productos Disponibles";
            loadJSON();
        }

        private async void loadJSON()
        {
			var client = new HttpClient()
			{
				BaseAddress = new Uri("http://area51.pe/sol/")
			};
			string url = string.Format("tienda.json");
			var resp = await client.GetAsync(url);
			var result = resp.Content.ReadAsStringAsync().Result;

			JObject values = JObject.Parse(result);

			arrCourse = (JArray)values["tienda"];

            Debug.WriteLine("fin lectura JSON");

			FillData(arrCourse);
        }

        private void FillData(JArray arrCourse)
        {

            //list.IsPullToRefreshEnabled = true;

            var people = new List<Product>();

            for (int i = 0; i < arrCourse.Count; i++){

                string cant = (((JArray)arrCourse[i]["items"]).Count).ToString();

                Product tmp  = new Product { nombre = arrCourse[i]["nombre"].ToString(), 
                                            imagen = "http://area51.pe/sol/" + arrCourse[i]["imagen"].ToString(),
                                            cantidad = cant
                        , items = ( JArray )arrCourse[i]["items"]
				};

                people.Add( tmp );
            }


            list.ItemsSource = people;


			list.ItemSelected += (sender, args) =>
				{
                    ((ListView)sender).SelectedItem = null;

					if (args.SelectedItem == null)
					{
						return;
					}

                    Debug.WriteLine(  (list.SelectedItem as Product).items   );

					//DisplayAlert("Próximamente", (list.SelectedItem as Product).nombre  , "ok");

                    ListCategory detailPage = new ListCategory( (list.SelectedItem as Product).items );
                    detailPage.Title = (list.SelectedItem as Product).nombre;
                    Navigation.PushAsync(detailPage);
					
				};

        }


    }
}
