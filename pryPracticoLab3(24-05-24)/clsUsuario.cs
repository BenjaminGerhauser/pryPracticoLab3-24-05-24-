using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace pryPracticoLab3_24_05_24_
{
    internal class clsUsuario
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBD;
        OleDbDataAdapter adapter;
        


        OleDbDataAdapter adaptadorBD;
        DataSet objDS;

        string rutaArchivo;
        public string estadoConexion;

        public clsUsuario()
        {
            try
            {
                rutaArchivo = @"C:\Users\Alumno\source\repos\pryPracticoLab3(24-05-24)\pryPracticoLab3(24-05-24)\Archivos/BDusuarios2.mdb";

                conexionBD = new OleDbConnection();
                conexionBD.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Alumno\\source\\repos\\pryPracticoLab3(24-05-24)\\pryPracticoLab3(24-05-24)\\Archivos/BDusuarios2.mdb";
                conexionBD.Open();

                objDS = new DataSet();

                estadoConexion = "Conectado";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }
        }

        public void RegistroLogInicioSesion()
        {
            try
            {
                comandoBD = new OleDbCommand();

                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = System.Data.CommandType.TableDirect;
                comandoBD.CommandText = "Logs";

                adaptadorBD = new OleDbDataAdapter(comandoBD);

                adaptadorBD.Fill(objDS, "Logs");

                DataTable objTabla = objDS.Tables["Logs"];
                DataRow nuevoRegistro = objTabla.NewRow();

                nuevoRegistro["Categoria"] = "Inicio Sesión";
                nuevoRegistro["FechaHora"] = DateTime.Now;
                nuevoRegistro["Descripcion"] = "Inicio exitoso";

                objTabla.Rows.Add(nuevoRegistro);

                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptadorBD);
                adaptadorBD.Update(objDS, "Logs");

                estadoConexion = "Registro exitoso de log";
            }
            catch (Exception error)
            {

                estadoConexion = error.Message;
            }

        }

        public bool ValidarUsuario(string nombreUser, string passUser)
        {
            try
            {
                comandoBD = new OleDbCommand();

                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = System.Data.CommandType.TableDirect;
                comandoBD.CommandText = "Usuario";

                lectorBD = comandoBD.ExecuteReader();

                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read())
                    {
                        if (lectorBD[1].ToString() == nombreUser && lectorBD[2].ToString() == passUser)
                        {
                            estadoConexion = "Usuario EXISTE";
                        }
                    }
                }
                if (estadoConexion == "Usuario EXISTE") return true;
                else return false;

            }
            catch (Exception error)
            {

                estadoConexion = error.Message;
                return false;
            }
        }

        public bool registrarUser(string usuario,string contraseña,string categoria)
        {
            try
            {
                comandoBD = new OleDbCommand();

                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = System.Data.CommandType.Text;
                comandoBD.CommandText = $"Insert into Usuario (Nombre,Contraseña,Perfil) VALUES ({usuario},{contraseña},{categoria})";

                if (ValidarUsuario(usuario, contraseña) != true)
                {
                    comandoBD.Parameters.AddWithValue("@Nombre", usuario);
                    comandoBD.Parameters.AddWithValue("@Contraseña", contraseña);
                    comandoBD.Parameters.AddWithValue("@Categoria", categoria);
                    comandoBD.ExecuteNonQuery();
                    return true;
                }
                else return false;
            }
            catch (Exception error)
            {

                estadoConexion = error.Message;
                return false;
            }
        }
    }
}
