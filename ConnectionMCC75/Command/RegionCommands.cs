using ConnectionMCC75.Models;
using System.Data.SqlClient;

namespace ConnectionMCC75.Command;
public class RegionCommands
{
    SqlConnection sqlConnection;
    string connectionString = "Data Source=CAMOUFLY;Initial Catalog=db_hr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public int Insert(Region entity)
    {
        int result = 0;
        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "INSERT INTO tb_m_regions VALUES (@name);";

                // Parameter Name
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = entity.Name;
                sqlCommand.Parameters.Add(pName);

                // Untuk menjalankan perintah transaksi
                result = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                sqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    sqlTransaction.Rollback();
                }
                catch (Exception exRollback)
                {
                    Console.WriteLine(exRollback.Message);
                }
            }

            return result;
        }
    }

    public List<Region> GetAll()
    {
        List<Region> listRegion = new List<Region>();

        sqlConnection = new SqlConnection(connectionString);

        // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_regions;";

        // membuka koneksi
        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            // Mengecek apakah ada data atau tidak
            if (sqlDataReader.HasRows)
            {
                //jika ada, maka tampilkan datanya
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("Id : " + sqlDataReader[0]);
                    Console.WriteLine("Name : " + sqlDataReader[1]);

                    Region region = new Region();
                    region.Id = Convert.ToInt16(sqlDataReader[0]);
                    region.Name = sqlDataReader[1].ToString();

                    listRegion.Add(region);
                }
            }
            sqlDataReader.Close();
        }
        sqlConnection.Close();

        return listRegion;
    }

}
