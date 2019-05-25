
namespace com.co.uan.DMiravalle.Administracion
{
   public   class UsuarioDTO
    {

        private string nombre;

        private string apellido;

        private string correo;

        private SedeDTO sedeasignada;

        private int idusuario;

        private string nombreusuario;

        private int perfil;

        private string clave;

        public UsuarioDTO(string nombre, string apellido, string correo, SedeDTO sedeasignada, int idusuario, int idSede, string nombreusuario, string clave, int perfil)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.correo = correo;
            this.sedeasignada = sedeasignada;
            this.idusuario = idusuario;
            this.nombreusuario = nombreusuario;
            this.perfil = perfil;
        }

        public string Nombre { get { return nombre; } }

        public string Apellido { get { return Apellido; } }

        public string Correo { get { return correo; } }

        public string Clave { get { return clave; } }

        public SedeDTO SedeAsignada { get { return sedeasignada; } }

        public int IdUsuario { get { return idusuario; } }

        public string NombreUsuario { get { return nombreusuario; } }

        public int Perfil { get { return perfil; } }

    }
}
