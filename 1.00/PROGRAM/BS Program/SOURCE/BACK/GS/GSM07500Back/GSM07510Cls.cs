using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using GSM07500Common.DTOs;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace GSM07500Back
{
    public class GSM07510Cls : R_BusinessObject<GSM07510DTO>
    {
        protected override GSM07510DTO R_Display(GSM07510DTO poEntity)
            {
            R_Exception loEx = new R_Exception();
            GSM07510DTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_PERIOD_HD";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", System.Data.DbType.String, 4, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);

                // loRtn = loDb.SqlExecObjectQuery<GSM02000DTO>(lcQuery, loConn, true, poEntity).FirstOrDefault();
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM07510DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Saving(GSM07510DTO poNewEntity, eCRUDMode poCRUDMode)
        {
           var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCmd = loDb.GetCommand();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();

                    lcQuery = "CREATE TABLE #PERIOD_DETAIL " +
                              "(CPERIOD_NO	CHAR(2), " +
                              "CSTART_DATE	CHAR(8) , " +
                              "CEND_DATE	CHAR(8))";

                    if (poNewEntity.CPERIOD_LIST != null && poNewEntity.CPERIOD_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO #PERIOD_DETAIL " +
                                   "VALUES ";
                        foreach (var loRate in poNewEntity.CPERIOD_LIST)
                        {
                            lcQuery += $"('{loRate.CPERIOD_NO}', '{loRate.CSTART_DATE}', '{loRate.CEND_DATE}' ) ,";
                        }
                        lcQuery = lcQuery.Substring(0, lcQuery.Length - 1) + " ";

                    }

                    lcQuery += "EXEC RSP_GS_MAINTAIN_PERIOD " +
                        $"@CCOMPANY_ID = '{poNewEntity.CCOMPANY_ID}' " +
                        $",@CYEAR = '{poNewEntity.CYEAR}' " +
                        $",@LPERIOD_MODE = {poNewEntity.LPERIOD_MODE} " +
                        $",@INO_PERIOD = {poNewEntity.INO_PERIOD} " +
                        $",@CACTION = '{poNewEntity.CACTION}' " +
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' ";

                        R_ExternalException.R_SP_Init_Exception(loConn);

                        try
                        {
                            loDb.SqlExecQuery(lcQuery, loConn, false);
                        }
                        catch (Exception ex)
                        {
                            loEx.Add(ex);
                        }

                        loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    TransScope.Complete();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(GSM07510DTO poEntity)
        {
        R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loComm;
            DbConnection loConn = null;
            string lcAction = "";
        
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loComm = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);


                lcQuery = "RSP_GS_MAINTAIN_PERIOD";
                loComm.CommandType = CommandType.StoredProcedure;
                loComm.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loComm, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loComm, "@CYEAR", DbType.String, 20, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loComm, "@LPERIOD_MODE", DbType.String, 60, poEntity.LPERIOD_MODE);
                loDb.R_AddCommandParameter(loComm, "@INO_PERIOD", DbType.String, 1, poEntity.INO_PERIOD);
                loDb.R_AddCommandParameter(loComm, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loComm, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                // loDb.SqlExecNonQuery(loConn, loComm, true);
                
                try
                {
                    loDb.SqlExecNonQuery(loConn, loComm, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public List<GSM07510DTO> GetPeriodDbList (GSM07510DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07510DTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();
                
                lcQuery = "SELECT * FROM GSM_PERIOD A (NOLOCK) WHERE A.CCOMPANY_ID = @CCOMPANY_ID ORDER BY CYEAR ASC";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM07510DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        public LastYearDTO GetLastYear(GSM07510DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            LastYearDTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT CAST(ISNULL((SELECT TOP 1 CYEAR FROM GSM_PERIOD WHERE CCOMPANY_ID = @CCOMPANY_ID ORDER BY CYEAR DESC), 0) + 1 AS VARCHAR(4)) AS NEXT_YEAR";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);

                // loRtn = loDb.SqlExecObjectQuery<GSM02000DTO>(lcQuery, loConn, true, poEntity).FirstOrDefault();
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<LastYearDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
