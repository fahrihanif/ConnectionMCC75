using ConnectionMCC75.Context;
using ConnectionMCC75.Models;
using System.Data.SqlClient;

namespace ConnectionMCC75.Command;
public class RegionCommands
{
    SqlConnection sqlConnection;

    public int Insert(Region entity)
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

        sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING);

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
                    listRegion.Add(new Region
                    {
                        Id = Convert.ToInt16(sqlDataReader[0]),
                        Name = sqlDataReader[1].ToString()
                    });
                }
            }
            sqlDataReader.Close();
        }
        sqlConnection.Close();

        return listRegion;
    }

    public Region GetByIdRegion(int id)
    {
        sqlConnection = new SqlConnection(DbContext.CONNECTION_STRING);

        try
        {
            // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id;";

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

                    return new Region
                    {
                        Id = Convert.ToInt16(sqlDataReader[0]),
                        Name = sqlDataReader[1].ToString()
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

    public int UpdateRegion(Region entity)
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
                sqlCommand.CommandText = "UPDATE tb_m_regions SET name = @name WHERE id = @id;";

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
                sqlCommand.CommandText = "DELETE FROM tb_m_regions WHERE id = @id;";

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
