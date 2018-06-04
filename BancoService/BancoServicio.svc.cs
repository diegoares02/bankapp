using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BancoService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class BancoServicio : IBancoServicio
    {
        private string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\DIEGO-PC\Documents\Visual Studio 2017\Projects\Banco-App\BancoService\App_Data\BancoDB.mdf';Integrated Security=True;Connect Timeout=30";
        public void Depositar(int cantidad, int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deposito", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ci", id));
                    cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                    DataSet dt = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public List<Cliente> Extracto(int id)
        {
            SqlConnection con = new SqlConnection(connectionstring);

            SqlCommand cmd = new SqlCommand("sp_extracto", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ci", id));
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt, "Cliente");
            //con.Close();


            List<Cliente> list = new List<Cliente>();
            foreach (DataRow item in dt.Tables["Cliente"].Rows)
            {
                list.Add(new Cliente()
                {
                    Id = Convert.ToInt32(item["CI"]),
                    Saldo = Convert.ToInt32(item["Saldo"]),
                    Fecha_Operacion = Convert.ToDateTime(item["Fecha"]),
                    Operacion = (Operaciones)Convert.ToInt32(item["Operacion"])
                });
            }
            return list;
        }

        public void Retirar(int Cantidad, int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deposito", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ci", id));
                    cmd.Parameters.Add(new SqlParameter("@cantidad", Cantidad));
                    DataSet dt = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public int Saldo_Actual(int id)
        {
            int saldo = 0;
            SqlConnection conn = new SqlConnection(connectionstring);

            SqlCommand cmd = new SqlCommand("sp_saldo", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ci", id));
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt, "Cliente");
            conn.Close();


            foreach (DataRow item in dt.Tables["Cliente"].Rows)
            {
                saldo = Convert.ToInt32(item["Saldo"]);
            }
            return saldo;
        }
    }
}
