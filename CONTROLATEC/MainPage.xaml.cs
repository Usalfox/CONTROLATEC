using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLitePCL;
using CONTROLATEC.Modelos;

namespace CONTROLATEC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
        }

        public async void BtnGuardar_Clicked(object sender, EventArgs e)
        {

            try
            {
                //var item = new CUENTA
                //{
                //    CUENTA = id_cuenta.Text,
                //    fecha = id_cuenta.Text,
                //    etiqueta = etiqueta.Text,
                //    total = total.Text,
                //    status = estatus.Text
                //};
                //var result = await App.DBCuentas.InsertItemAsny(item);
                //if(result==1)
                //{
                //    await Navigation.PopAsync();
                //}
                //else
                //{
                //    await DisplayAlert("Error", "No se logro", "Aceptar");
                //}
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }
    }
}
