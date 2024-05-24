using BlazorWebPage.Shared;
using Fare;


namespace BlazorWebPage.Client.Pages
{
    public partial class ComponentSample
    {
        private string cadena { get; set; } = string.Empty;
        public string Cadena
        {
            get
            {
                return cadena;
            }
            set
            {
                if (cadena != value)
                {
                    cadena = value;
                    ValueChangeHandler();
                }
            }
        }

        private string numero { get; set; } = "0";
        public string Numero
        {
            get
            {
                return numero;
            }
            set
            {
                if (numero != value)
                {
                    numero = value;
                    ValueChangeHandler();
                }
            }
        }

        private int cifra { get; set; }
        protected bool IsDisabled { get; set; } = true;
        private List<SampleModel> ListaEjemplo = new();
        private Random random = new();

        private void Aceptar()
        {
            try
            {
                cifra = int.Parse(numero);

                SampleModel sampleModel = new();
                sampleModel.Cadena = cadena;
                sampleModel.Numero = cifra;

                ListaEjemplo.Add(sampleModel);
            }
            catch (Exception)
            {
                GenerarRegistroAleatorio();
            }
            finally
            {
                cadena = string.Empty;
                numero = string.Empty;
            }
        }

        private int StepValue()
        {
            return random.Next(1, 16);
        }

        private void GenerarRegistroAleatorio()
        {
            String regex = @"^[a-zA-Z]{4}[a-zA-Z]+$";
            Xeger xeger = new(regex, new Random());

            SampleModel sampleModel = new();
            sampleModel.Numero = random.Next();
            sampleModel.Cadena = xeger.Generate();

            ListaEjemplo.Add(sampleModel);
        }

        private void ValueChangeHandler()
        {
            IsDisabled = (string.IsNullOrWhiteSpace(cadena) || string.IsNullOrWhiteSpace(numero));
        }
    }
}
