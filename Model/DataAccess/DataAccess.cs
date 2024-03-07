using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Model.DataAccess
{
    public class DataAccess
    {
        string databaseConnection = ConfigurationManager.ConnectionStrings["DBEscuela"].ConnectionString;
        //string databaseConnection = "Data Source=MARIFER\\VD_SERVER;Initial Catalog=EscuelaDB;user id=sa;password=Feb2024;";

        public void SaveAlumno(Alumno alumno)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    SqlCommand command = new SqlCommand("[Escuela].[SaveAlumno]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter nombreParameter = new SqlParameter();
                    nombreParameter.ParameterName = "Nombre";
                    nombreParameter.Value = alumno.Nombre;

                    SqlParameter apellidosParameter = new SqlParameter();
                    apellidosParameter.ParameterName = "Apellidos";
                    apellidosParameter.Value = alumno.Apellidos;

                    SqlParameter promedioParameter = new SqlParameter();
                    promedioParameter.ParameterName = "Promedio";
                    promedioParameter.Value = alumno.Promedio;

                    command.Parameters.Add(nombreParameter);
                    command.Parameters.Add(apellidosParameter);
                    command.Parameters.Add(promedioParameter);

                    MessageBox.Show("Agregado exitosamente!");

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<Alumno> GetAlumnosList()
        {
            List<Alumno> resultado = new List<Alumno>();
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    SqlCommand command = new SqlCommand("[Escuela].[GetAlumnoList]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    var reader = command.ExecuteReader(); //Leer

                    while (reader.Read())
                    {
                        Alumno alumno = new Alumno();
                        alumno.Id = int.Parse(reader["Id"].ToString());
                        alumno.Nombre = reader["Nombre"].ToString();
                        alumno.Apellidos = reader["Apellidos"].ToString();
                        alumno.Promedio = decimal.Parse(reader["Promedio"].ToString());
                        resultado.Add(alumno);
                    }
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return resultado;
        }

        public void Selection(object Fila, System.EventArgs s, DataGridView dataGridView)//Seleccionar una fila de DataGrid y ponerlo en TextBox
        {
            int Select;
            Select = dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (Select > 0)
            {
                System.Text.StringBuilder fila = new System.Text.StringBuilder();

                for (int i = 0; i < Select; i++)
                {
                    fila.Append(dataGridView.SelectedRows[i].Index.ToString());
                    fila.Append(fila.ToString());
                }
            }
        }

        public void ModifyAlumno(Alumno alumnos)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    SqlCommand command = new SqlCommand("[Escuela].[ModifyAlumno]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "@Id";
                    idParameter.Value = alumnos.Id;

                    SqlParameter nombreParameter = new SqlParameter();
                    nombreParameter.ParameterName = "@Nombre";
                    nombreParameter.Value = alumnos.Nombre;

                    SqlParameter apellidosParameter = new SqlParameter();
                    apellidosParameter.ParameterName = "@Apellidos";
                    apellidosParameter.Value = alumnos.Apellidos;

                    SqlParameter promedioParameter = new SqlParameter();
                    promedioParameter.ParameterName = "@Promedio";
                    promedioParameter.Value = alumnos.Promedio;

                    command.Parameters.Add(idParameter);
                    command.Parameters.Add(nombreParameter);
                    command.Parameters.Add(apellidosParameter);
                    command.Parameters.Add(promedioParameter);

                    MessageBox.Show("Modicado exitosamente!");

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    public void DeleteAlumno(Alumno alumno)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    DialogResult Resultado = (DialogResult)MessageBox.Show("Estas seguro de eliminar este registro?", "Alerta!", MessageBoxButton.YesNo);
                    if (Resultado == DialogResult.Yes)
                    {
                        SqlCommand command = new SqlCommand("[Escuela].[DeleteAlumno]", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter idParameter = new SqlParameter();
                        idParameter.ParameterName = "Id";
                        idParameter.Value = alumno.Id;

                        SqlParameter nombreParameter = new SqlParameter();
                        nombreParameter.ParameterName = "Nombre";
                        nombreParameter.Value = alumno.Nombre;

                        SqlParameter apellidosParameter = new SqlParameter();
                        apellidosParameter.ParameterName = "Apellidos";
                        apellidosParameter.Value = alumno.Apellidos;

                        SqlParameter promedioParameter = new SqlParameter();
                        promedioParameter.ParameterName = "Promedio";
                        promedioParameter.Value = alumno.Promedio;

                        command.Parameters.Add(idParameter);
                        command.Parameters.Add(nombreParameter);
                        command.Parameters.Add(apellidosParameter);
                        command.Parameters.Add(promedioParameter);

                        MessageBox.Show("Se ha eliminado un registro");

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se ha eliminado un registro");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
