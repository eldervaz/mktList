using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace tiendaMKT
{
    public class Master : MasterDetailPage
    {
        public Master()
        {
            


           //*
			// Create ListView for the master page.
			var listView = new ListView();
            listView.Margin = Device.OnPlatform(new Thickness(0, 10, 0, 0), new Thickness(0), new Thickness(0));
            listView.RowHeight = 50;
            listView.BackgroundColor = Color.FromHex("dfdfdf");

            listView.SeparatorColor = Color.White;

			var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.White);
			

			listView.ItemsSource = new string[]{
			  "Favoritos",
			  "Los más buscados",
			  "Los más vendidos",
              "Promoción del mes",
			  "Conócenos",
			  "Se nuestro proveedor",
			  "Búscanos en Facebook"
			};
            //listView.ItemTemplate = template;


			

			this.Master = new ContentPage
			{
				Title = "menu",
				Icon = "menuIcon.png",

				Content = new StackLayout
				{
					Children =
					{
                        new Image{Source="fotoDemo.jpg", 
                                    Margin=new Thickness(0, 30, 0, 0) },
						listView
					}
				}
			};

            listView.ItemSelected += (sender, args) =>
                {
                    DisplayAlert("Próximamente", args.SelectedItem.ToString(), "ok");
                    // Set the BindingContext of the detail page.
                    this.Detail.BindingContext = args.SelectedItem;
                    
                    // Show the detail page.
                    this.IsPresented = false;

                    //((ListView)sender).SelectedItem = null;
				};
           // */



            this.Detail = new NavigationPage(new tiendaMKTPage() );


			//fin del constructor
		}
    }





}

