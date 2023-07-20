using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Semana9Movil.Model;

namespace Semana9Movil
{
    public partial class MainPage : ContentPage
    {
        private string url = "https://gfe19c555b39142-databasedemo.adb.us-ashburn-1.oraclecloudapps.com/ords/admin/dusers/users";
        HttpClient cliente = new HttpClient();

        public IList<CAT_USUARIO> catUsuarios { get; private set; }

        public MainPage()
        {
            catUsuarios = new List<CAT_USUARIO>();
            InitializeComponent();
            btn01.Clicked += Btn01_Clicked;
            btn02.Clicked += Btn02_Clicked;
            btn03.Clicked += Btn03_Clicked;
            btn04.Clicked += Btn04_Clicked;
            btn05.Clicked += Btn05_Clicked;
            btn06.Clicked += Btn06_Clicked;
            btn07.Clicked += Btn07_Clicked;

            catUsuarios.Add(new CAT_USUARIO { Id = "1", Nombre = "Fer", Clave = "999", Activa = "0" });

            BindingContext = this;
            lblstatus.Text = "( Mensajes )";
        }

        public async void ConsultarDatosDirecto()

        {

            string Idx = "";

            string Nombrex = "";

            string Clavex = "";

            string Activax = "";

            string DData = txtid.Text;

            string[] stringArray = new string[3];

            string contenido = await cliente.GetStringAsync(url + "/" + DData);



            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayProblem>(contenido);



            foreach (var item in data.items)

            {

                Idx = string.Format("{0}", item.Id.ToString());

                Nombrex = string.Format("{0}", item.Nombre.ToString());

                Clavex = string.Format("{0}", item.Clave.ToString());

                Activax = string.Format("{0}", item.Activa.ToString());



                catUsuarios.Add(new CAT_USUARIO

                {

                    Id = Idx,

                    Nombre = Nombrex,

                    Clave = Clavex,

                    Activa = Activax

                });

                ltusuario.ItemsSource = null;           //Desconecta la lista de la clase Usuarios

                ltusuario.ItemsSource = catUsuarios;

            }

        }

        public async void ActualizarenBD()
        {
            CAT_USUARIO CA = new CAT_USUARIO
            {
                Id = txtid.Text,
                Nombre = txtnombre.Text,
                Clave = txtclave.Text,
                Activa = txtactiva.Text
            };
            var json = JsonConvert.SerializeObject(CA);
            //var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync(url, contentJson);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //await DisplayAlert("Datos", "Se actualizó correctamente la info");
                lblstatus.Text = "Se actualizó correctamente el registro.";
            }
            else { lblstatus.Text = "La operación update falló"; }
        }


        public async void AgregarenBD()
        {
            CAT_USUARIO CA = new CAT_USUARIO
            {
                Id = txtid.Text,
                Nombre = txtnombre.Text,
                Clave = txtclave.Text,
                Activa = txtactiva.Text
            };
            var json = JsonConvert.SerializeObject(CA);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync(url, contentJson);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //await DisplayAlert("Datos", "Se actualizó correctamente la info");
                lblstatus.Text = "Se ingresó correctamente el registro";
            }
            else { lblstatus.Text = "La operación Insert falló"; }
        }


        public async void ConsultarDatos()
        {
            string Idx = "";
            string Nombrex = "";
            string Clavex = "";
            string Activax = "";
            string DData = "";
            string[] stringArray = new string[3];
            string contenido = await cliente.GetStringAsync(url);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayProblem>(contenido);
            foreach (var item in data.items)
            {
                Idx = string.Format("{0}", item.Id.ToString());
                Nombrex = string.Format("{0}", item.Nombre.ToString());
                Clavex = string.Format("{0}", item.Clave.ToString());
                Activax = string.Format("{0}", item.Activa.ToString());
                catUsuarios.Add(new CAT_USUARIO
                {
                    Id = Idx,
                    Nombre = Nombrex,
                    Clave = Clavex,
                    Activa = Activax
                });
                ltusuario.ItemsSource = null; //Desconecta la lista de la clase Usuarios
                ltusuario.ItemsSource = catUsuarios;
            }
        }

        public void RefrescarEntorno()
        {
            ltusuario.ItemsSource = null;
            //Desconecta la lista de la clase Usuarios
            ltusuario.ItemsSource = catUsuarios;
            //Obliga a volver a leer la clase para cargar la lista
        }


        public void BorrarList()
        {
            ltusuario.ItemsSource = null;
            catUsuarios.Clear(); //Limpia la clase
            ltusuario.ItemsSource = catUsuarios;
            //Obliga a volver a leer pero la clase está vacía
        }


        public void BorrarInterfaz()
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtclave.Text = "";
            txtactiva.Text = "";
        }

        private void Btn01_Clicked(object sender, EventArgs e)
        {
            catUsuarios.Add(new CAT_USUARIO
            {
                Id = txtid.Text,
                Nombre = txtnombre.Text,
                Clave = txtclave.Text,
                Activa = txtactiva.Text
            });
            ltusuario.ItemsSource = null; //Desconecta la lista de la clase Usuarios
            ltusuario.ItemsSource = catUsuarios;
        }



        private void Btn02_Clicked(object sender, EventArgs e)
        {
            BorrarList();
            ConsultarDatos();
            RefrescarEntorno();
        }



        private void Btn03_Clicked(object sender, EventArgs e)
        {
            BorrarInterfaz();
            BorrarList();
        }

        private void Btn04_Clicked(object sender, EventArgs e)
        {
            if (txtid.Text == null)
            {
                lblstatus.Text = "No se puede editar datos con los entry vacìos";
                return;
            }
            ActualizarenBD();
        }

        private void Btn05_Clicked(object sender, EventArgs e)
        {
            if (txtid.Text == null)
            {
                lblstatus.Text = "No se pueden ingresar datos con los entry vacíos";
                return;
            }
            AgregarenBD();
            BorrarInterfaz();
        }

        private void Btn06_Clicked(object sender, EventArgs e)
        {
            BorrarList();
        }

        private void Btn07_Clicked(object sender, EventArgs e)
        {
            if (txtid.Text != null)

            {   // Se requiere el ID para buscar

                BorrarList();

                ConsultarDatosDirecto();

                lblstatus.Text = "( Mensajes )";

            }

            else
            {

                lblstatus.Text = "Debe colocar un ID a localizar";

            }
        }
    }
}
