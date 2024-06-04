namespace EcoLogTracking.Server.Models
{
    public class Log : BaseModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre de la categoría no puede estar vacío o contener solo espacios en blanco.");
                }
                _name = value;
            }
        }

        public Log()
        {
        }

        public Log(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Log(string name)
        {
            Name = name;
        }
    }
}
