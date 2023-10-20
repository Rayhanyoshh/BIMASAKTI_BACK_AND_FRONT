using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using GSM07500Common.DTOs;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace GSM07500Back
{
    public class GSM07500Cls: R_BusinessObject<SaveBatchGSM07500DTO>
    {
   
        public List<GSM07500DTO> GetPeriodDetailDbList (GSM07500DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07500DTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();
                
                lcQuery = $"SELECT * FROM GSM_PERIOD_DT A (NOLOCK) " +
                          $"WHERE A.CCOMPANY_ID = '{poEntity.@CCOMPANY_ID}' " +
                          $"AND A.CCYEAR = '{poEntity.CCYEAR}' ";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                //
                // loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                // loDb.R_AddCommandParameter(loCmd, "@CCYEAR", System.Data.DbType.String, 50, poEntity.CCYEAR);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM07500DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        public List<GSM07500DTO> RftGenerateGSMPeriod (GeneratePeriodParameter poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07500DTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();
                
                    lcQuery = $"SELECT CPERIOD_NO, CSTART_DATE, CEND_DATE " +
                              $"FROM dbo.RFT_GENERATE_GSM_PERIODS(@CCOMPANY_ID, @CYEAR, 1, 12)";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                //
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", System.Data.DbType.String, 50, poEntity.CYEAR);
                // loDb.R_AddCommandParameter(loCmd, "@LPERIOD_MODE", System.Data.DbType.String, 50, true);
                // loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD", System.Data.DbType.String, 50, "12");


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM07500DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override SaveBatchGSM07500DTO R_Display(SaveBatchGSM07500DTO poEntity)
        {
            SaveBatchGSM07500DTO loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                loRtn = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Saving(SaveBatchGSM07500DTO poNewEntity, eCRUDMode poCRUDMode)
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

                    lcQuery = "DECLARE @CPERIOD_LIST AS RDT_COMMON_OBJECT ";

                    if (poNewEntity.CPERIOD_LIST != null && poNewEntity.CPERIOD_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO @CPERIOD_LIST  " +
                            "(COBJECT_ID, COBJECT_DESC, CATTRIBUTE01 ) " +
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
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' " +
                        ",@CPERIOD_LIST = @CPERIOD_LIST ";

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

        protected override void R_Deleting(SaveBatchGSM07500DTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
