using Inventory_management_system.DAL;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Inventory_management_system.DAL.SCE_Users
{
    public class SEC_UserDALBase : DAL_Helper
    { 
       
        public DataTable dbo_PR_SEC_User_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionstr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SEC_User_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        
        public bool dbo_PR_SEC_User_Register(string UserName, string Password, string FirstName, string LastName, string Email, string MobileNo)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionstr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectUserName");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCMD))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    DbCommand dbCMD1 = sqlDB.GetStoredProcCommand("PR_SEC_User_Insert");
                    sqlDB.AddInParameter(dbCMD1, "UserName", SqlDbType.VarChar, UserName);
                    sqlDB.AddInParameter(dbCMD1, "Password", SqlDbType.VarChar, Password);
                    sqlDB.AddInParameter(dbCMD1, "FirstName", SqlDbType.VarChar, FirstName);
                    sqlDB.AddInParameter(dbCMD1, "LastName", SqlDbType.VarChar, LastName);
                    sqlDB.AddInParameter(dbCMD1, "MobileNo", SqlDbType.VarChar, MobileNo);
                    sqlDB.AddInParameter(dbCMD1, "Email", SqlDbType.VarChar, Email);
                    if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
