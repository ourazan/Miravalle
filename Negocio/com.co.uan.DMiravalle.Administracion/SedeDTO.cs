
namespace com.co.uan.DMiravalle.Administracion
{
    public class SedeDTO
    {

        private UsuarioDTO administrador;

        private string nombresede;

        private string direccion;

        private string ciudad;

        private int idsede;

        public SedeDTO()
        { }
        public SedeDTO(string nombresede, string direccion, string ciudad, int idSede, UsuarioDTO administrador)
        {
            this.administrador = administrador;
            this.nombresede = nombresede;
            this.direccion = direccion;
            this.ciudad = ciudad;
            this.idsede = idSede;
        }

        public UsuarioDTO Administrador
        {  get { return administrador; }}

        public string NombreSede
        { get { return nombresede; } }

        public string Direccion
        { get { return direccion; } }

        public string Ciudad
        { get { return ciudad; } }

        public int IdSede
        { get { return idsede; } }

    }
}
