using Che168.Core.Utils.Util;

namespace ErShouCheSqlServerExtendToken.App_Code.Dal.UsedCar
{
    public static class DealersDal
    {
        public static List<Dealer> GetDealers()
        {
            string sql = @"SELECT dsr.DealerId FROM DealerScopeRelation AS dsr WITH(NOLOCK) WHERE dsr.Status = @Status;
                        --\ErShouCheSqlServerExtendToken\App_Code\Dal\UsedCar\DealersDal.cs GetDealers";
            List<DbParameter> dbParams = new List<DbParameter>() { };
            dbParams.Add(DbParamProvider.Instance.MakeInParam("@Status", (DbType)SqlDbType.TinyInt, 1, 0));
            return DataBaseOperator.UsedCarRead.ExecuteDataSet(sql, CommandType.Text, dbParams.ToArray()).Tables[0].ToList<Dealer>();
        }
    }

}
