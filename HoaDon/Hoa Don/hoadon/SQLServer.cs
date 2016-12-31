using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace hoadon
{
    public class SQLServer
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        private SqlDataAdapter adapter;

        public string ConnectionString { get; set; }

        public SQLServer()
        {
            //constructor
        }

        public SQLServer(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private void OpenConnection()
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
        }

        public DataTable ExecuteCommandText(string cmdText,string bien)
			{
				//method: query SQL
				try
				{
					OpenConnection();
					command = new SqlCommand();
					command.CommandType = CommandType.Text;
                    command.CommandText = cmdText;
                    if (bien != "khong co bien")
                        command.Parameters.Add(new SqlParameter("@bien", bien));
                    command.Connection = connection;

					//DataTable <-> SqlDataReader
					adapter = new SqlDataAdapter(command);
					//Get Data: Fill
					DataTable result = new DataTable();
					adapter.Fill(result);			//lay xong du lieu
					adapter = null;
					
					//Return
					return result;
				}
				catch(Exception ex)
				{
                    throw new Exception("Truy van gap loi: " + ex.Message);
				}
				finally
				{
					//Dong ket noi
					connection.Close();
					connection.Dispose();
				}
			}

        public int ExecuteStoredProcedure(string spName, string SoHD, DateTime NgayTao, string MaKH, float TongTriGia, int VAT, float TongTienThue, float TongCong, string MaUser)
        {
            //method: query stored procedure
            try
            {
                OpenConnection();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                command.Parameters.Add(new SqlParameter("@SoHD", SoHD));
                command.Parameters.Add(new SqlParameter("@NgayTao", NgayTao));
                command.Parameters.Add(new SqlParameter("@MaKH", MaKH));
                command.Parameters.Add(new SqlParameter("@TongTriGia", TongTriGia));
                command.Parameters.Add(new SqlParameter("@VAT", VAT));
                command.Parameters.Add(new SqlParameter("@TongTienThue", TongTienThue));
                command.Parameters.Add(new SqlParameter("@TongCong", TongCong));
                command.Parameters.Add(new SqlParameter("@MaUser", MaUser));
                command.Connection = connection;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Truy van gap loi: " + ex.Message);
            }
            finally
            {
                //Dong ket noi
                connection.Close();
                connection.Dispose();
            }
        }
        public int ExecuteStoredProcedure(string spName, string MaHH, string SoHD, int SoLuong, float DonGia, float Tong)
        {
            try
            {
                OpenConnection();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                command.Parameters.Add(new SqlParameter("@MaHH", MaHH));
                command.Parameters.Add(new SqlParameter("@SoHD", SoHD));
                command.Parameters.Add(new SqlParameter("@SoLuong", SoLuong));
                command.Parameters.Add(new SqlParameter("@DonGia", DonGia));
                command.Parameters.Add(new SqlParameter("@Tong", Tong));
                command.Connection = connection;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Truy van gap loi: " + ex.Message);
            }
            finally
            {
                //Dong ket noi
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
