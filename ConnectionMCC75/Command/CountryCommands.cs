using ConnectionMCC75.Context;
using ConnectionMCC75.Models;
using System.Data.SqlClient;

namespace ConnectionMCC75.Command;
public class CountryCommands
{
    SqlConnection sqlConnection;
    public int Insert(Country entity)
    {
        int result = 0;
        using (sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "INSERT INTO tb_m_countries VALUES (@name, @region_id);";

                // Parameter Name
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = entity.Name;
                sqlCommand.Parameters.Add(pName);

                // Parameter Region Id
                SqlParameter pRegionId = new SqlParameter
                {
                    ParameterName = "@region_id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = entity.RegionId
                };
                sqlCommand.Parameters.Add(pRegionId);

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

    public List<Country> GetAll()
    {
        List<Country> listCountries = new List<Country>();

        sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING);

        // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_countries;";

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
                    listCountries.Add(new Country
                    {
                        Id = Convert.ToInt16(sqlDataReader[0]),
                        Name = sqlDataReader[1].ToString(),
                        RegionId = Convert.ToInt16(sqlDataReader[2]),
                    });
                }
            }
            sqlDataReader.Close();
        }
        sqlConnection.Close();

        return listCountries;
    }

    public Country GetByIdRegion(int id)
    {
        sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING);

        try
        {
            // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_countries WHERE id = @id;";

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            // membuka koneksi
            sqlConnection.Open();

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                // Mengecek apakah ada data atau tidak
                if (sqlDataReader.HasRows)
                {
                    //jika ada, maka tampilkan datanya
                    sqlDataReader.Read();

                    return new Country
                    {
                        Id = Convert.ToInt16(sqlDataReader[0]),
                        Name = sqlDataReader[1].ToString(),
                        RegionId = Convert.ToInt16(sqlDataReader[2]),
                    };
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public int UpdateRegion(Country entity)
    {
        int result = 0;
        using (sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "UPDATE tb_m_countries SET name = @name WHERE id = @id;";

                // Parameter Name
                SqlParameter pName = new SqlParameter
                {
                    ParameterName = "@name",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = entity.Name
                };
                sqlCommand.Parameters.Add(pName);

                // Parameter Id
                SqlParameter pId = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = entity.Id
                };
                sqlCommand.Parameters.Add(pId);

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
        }
        return result;
    }

    public int DeleteRegion(int id)
    {
        int result = 0;
        using (sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "DELETE FROM tb_m_countries WHERE id = @id;";

                // Parameter Id
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = id;
                sqlCommand.Parameters.Add(pId);

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
        }
        return result;
    }
}
