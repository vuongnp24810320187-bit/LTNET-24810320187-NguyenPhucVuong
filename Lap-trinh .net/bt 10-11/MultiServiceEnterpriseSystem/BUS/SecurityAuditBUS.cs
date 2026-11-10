using System.Data;
using MultiServiceEnterpriseSystem.DAL;

namespace MultiServiceEnterpriseSystem.BUS
{
    public class SecurityAuditBUS
    {
        private readonly SecurityAuditDAL _dal = new();

        public (DataTable table, string executedQuery) TestUnsafeSql(string keyword)
        {
            return _dal.ExecuteUnsafeSearch(keyword);
        }

        public (DataTable table, string executedQuery) TestSafeSql(string keyword)
        {
            return _dal.ExecuteSafeSearch(keyword);
        }

        public (bool success, string auditLog) TestRollbackTransaction(bool forceFailure)
        {
            return _dal.SimulateTransactionRollback(forceFailure);
        }
    }
}

