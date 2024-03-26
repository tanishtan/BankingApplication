using BankingApp.Common;
using BankingApp.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.DataAccess
{
    public class BankingDataAccess : SqlBankingApplication
    {

        public List<AccountInfo> GetAllDetails(int id)
        {
            OpenConnection();
            string sql = $"sp_GetAccountDetails"; // Assuming this stored procedure exists

            List<AccountInfo> accounts = new List<AccountInfo>();
            try
            {
                var reader = ExecuteReader(sql, CommandType.StoredProcedure,
                    parameters: new SqlParameter("@accountId", id));
                while (reader.Read())
                {
                    var obj = new AccountInfo
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(2)),
                        Balance = reader.IsDBNull(3) ? 0 : (double)reader.GetDecimal(3)

                };
                    accounts.Add(obj);
                }
                if (!reader.IsClosed) reader.Close();
            }
            catch (SqlException Sqle)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return accounts;
        }


        public void CreateNewAccount(AccountInfo info)
        {
            OpenConnection();
            string sql = "sp_CreateNewAcount";
            try
            {
                ExecuteNonQuery(
                   sqltext: sql,
                   commandType: CommandType.StoredProcedure,
                       new SqlParameter("@customerName", info.Name),
                       new SqlParameter("@accountTypeName", info.Type.ToString()),
                       new SqlParameter("@amount", info.Balance),
                       new SqlParameter("@accountId", info.Id)
                  );
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdateAccount(string Name, int id)
        {
            OpenConnection();
            string sql = "sp_Update";
            try
            {
                ExecuteNonQuery(
                  sqltext: sql,
                  commandType: CommandType.StoredProcedure,
                    new SqlParameter("@customerName", Name),
                    new SqlParameter("@accId", id));
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Deposit(int accId, int amount)
        {
            OpenConnection();
            string sql = "sp_Deposit";
            try
            {
                ExecuteNonQuery(
                  sqltext: sql,
                  commandType: CommandType.StoredProcedure,
                    new SqlParameter("@accountId", accId),
                    new SqlParameter("@amount", amount));
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Withdraw(int accId, int amount)
        {
            OpenConnection();
            string sql = "sp_Withdraw";
            try
            {
                ExecuteNonQuery(
                  sqltext: sql,
                  commandType: CommandType.StoredProcedure,
                    new SqlParameter("@accountId", accId),
                    new SqlParameter("@amount", amount));
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void FundTransfer(int accId1, int amount, int accId2)
        {
            OpenConnection();
            string sql = "sp_FundTransfer";
            try
            {
                ExecuteNonQuery(
                  sqltext: sql,
                  commandType: CommandType.StoredProcedure,
                    new SqlParameter("@accountId1", accId1),
                    new SqlParameter("@accountId2", accId2),
                    new SqlParameter("@amount", amount));
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
