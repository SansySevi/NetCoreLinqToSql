using NetCoreLinqToSqlSSG.Models;
using System.Data;
using System.Data.SqlClient;

namespace NetCoreLinqToSqlSSG.Repositories
{
    public class RepositoryEnfermos
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataAdapter adapter;
        private DataTable tablaEnfermos;

        public RepositoryEnfermos()
        {
            //string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2022";
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Integrated Security=True";

            string sql = "SELECT * FROM ENFERMO";

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;

            this.adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            adapter.Fill(this.tablaEnfermos);
        }


        //METODO PARA RECUPERAR LOS EMP CON LINQ
        //Siempre recuperaremos un List<> 
        public List<Enfermo> GetEnfermos()
        {
            //LA TABLA ESTA COMPUESTA POR FILAS (DataRow)
            //LA CONSULTA DEBE SER SOBRE TODAS LAS FILAS DE
            //LA TABLA
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           select datos;

            //AHORA MISMO TENEMOS EN CONSULTA UNA COLECCION LINQ
            //DE OBJETOS DataRow 
            //QUE PODEMOS ORDENAR, FILTRAR Y HACER TODO LO QUE DESEEMOS
            List<Enfermo> enfermos = new List<Enfermo>();


            //VAMOS A RECORRER TODOS LOS DATOS DE LA CONSULTA Y EXTRAERLOS
            foreach (var row in consulta)
            {
                Enfermo enfermo = new Enfermo
                {
                    Inscripcion = row.Field<string>("INSCRIPCION"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Direccion = row.Field<string>("DIRECCION"),
                    FechaNacimiento = row.Field<DateTime>("FECHA_NAC"),
                    Sexo = row.Field<string>("S"),
                    Nss = row.Field<string>("NSS")
                };
                enfermos.Add(enfermo);
            }
            return enfermos;
        }



        public List<Enfermo> GetEnfermos(DateTime fechanacimiento)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<DateTime>("FECHA_NAC") == fechanacimiento
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Enfermo> enfermos = new List<Enfermo>();
                foreach (var row in consulta)
                {
                    Enfermo enfermo = new Enfermo
                    {
                        Inscripcion = row.Field<string>("INSCRIPCION"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Direccion = row.Field<string>("DIRECCION"),
                        FechaNacimiento = row.Field<DateTime>("FECHA_NAC"),
                        Sexo = row.Field<string>("S"),
                        Nss = row.Field<string>("NSS")
                    };
                    enfermos.Add(enfermo);
                }
                return enfermos;
            }
        }

        public void Delete(int inscripcion)
        {
            string sql = "DELETE ENFERMO WHERE INSCRIPCION=@INSCRIPCION";
            SqlParameter paminscripcion = new SqlParameter("@INSCRIPCION", inscripcion);
            this.com.Parameters.Add(paminscripcion);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
