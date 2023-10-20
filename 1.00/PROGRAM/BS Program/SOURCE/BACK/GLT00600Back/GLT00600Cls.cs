using R_BackEnd;
using R_Common;
using GLT00600Common.DTOs;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLT00600Back
{
    public class GLT00600Cls : R_BusinessObject<GLT00600DTO>
    {
        protected override GLT00600DTO R_Display(GLT00600DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(GLT00600DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(GLT00600DTO poEntity)
        {
            throw new NotImplementedException();
        }
        
        public GSM_COMPANY_DTO CompanyCheckCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM_COMPANY_DTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT CCOGS_METHOD " +
                          ",LENABLE_CENTER_IS " +
                          ",LENABLE_CENTER_BS " +
                          ",LPRIMARY_ACCOUNT " +
                          ",CBASE_CURRENCY_CODE " +
                          ",CLOCAL_CURRENCY_CODE " +
                          $"FROM GSM_COMPANY (NOLOCK) WHERE CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' ";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM_COMPANY_DTO>(loDataTable).FirstOrDefault();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }

        public GL_SYSTEM_PARAM_DTO SystemParamCls(ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GL_SYSTEM_PARAM_DTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", System.Data.DbType.String, 50, poEntity.CLANGUAGE_ID);
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<GL_SYSTEM_PARAM_DTO>(loDataTable).FirstOrDefault();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }

        public List<USER_DEPARTMENT_LIST_DTO> DepartmentListLookup(ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            List<USER_DEPARTMENT_LIST_DTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_DEPT_LOOKUP_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<USER_DEPARTMENT_LIST_DTO>(loDataTable).ToList();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }
        
        public EnableOptionDTO CommitJrnCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            EnableOptionDTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT IOPTION " +
                          "FROM GLM_SYSTEM_ENABLE_OPTION (NOLOCK) " +
                          "WHERE CCOMPANY_ID= @CCOMPANY_ID " +
                          "AND COPTION_CODE='GL014001';";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
             
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<EnableOptionDTO>(loDataTable).FirstOrDefault();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }
        
        public PeriodDTO PeriodStartCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PeriodDTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT CSTART_DATE " +
                          "FROM GSM_PERIOD_DT (NOLOCK) " +
                          "WHERE CCOMPANY_ID= @CCOMPANY_ID " +
                          "AND CCYEAR= @CCYEAR " +
                          "AND CPERIOD_NO= @CPERIOD_NO;";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCYEAR", System.Data.DbType.String, 50, poEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", System.Data.DbType.String, 50, poEntity.CPERIOD_NO);
             
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<PeriodDTO>(loDataTable).FirstOrDefault();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }
        
        public GSM_TRANSACTION_CODE_DTO TransactionCodeCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM_TRANSACTION_CODE_DTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT LINCREMENT_FLAG " +
                          ",LAPPROVAL_FLAG " +
                          "FROM GSM_TRANSACTION_CODE (NOLOCK) " +
                          "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                          "AND CTRANSACTION_CODE= @CTRANSACTION_CODE ";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", System.Data.DbType.String, 50, poEntity.CTRANSACTION_CODE);
             
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<GSM_TRANSACTION_CODE_DTO>(loDataTable).FirstOrDefault();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }
        
        public PeriodDTO MinMaxYearCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PeriodDTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT IMIN_YEAR=CAST(MIN(CYEAR) AS INT) " +
                          ",IMAX_YEAR=CAST(MAX(CYEAR) AS INT) " +
                          "FROM GSM_PERIOD (NOLOCK) " +
                          "WHERE CCOMPANY_ID = @CCOMPANY_ID ";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
             
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<PeriodDTO>(loDataTable).FirstOrDefault();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }
        
        public List<StatusCodeDTO> StatusCodeCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            List<StatusCodeDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT CCODE='' " +
                          ",CNAME='All' " +
                          "UNION SELECT CCODE " +
                          ",CDESCRIPTION AS CNAME " +
                          "FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', @CCOMPANY_ID, '_GL_JOURNAL_STATUS', '', @LANG_ID) " +
                          "ORDER BY CCODE";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LANG_ID", System.Data.DbType.String, 50, poEntity.LANG_ID);
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<StatusCodeDTO>(loDataTable).ToList();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }
        
        public List<GLT00600DTO> JournalListCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            List<GLT00600DTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GL_SEARCH_JOURNAL_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", System.Data.DbType.String, 50, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", System.Data.DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", System.Data.DbType.String, 50, poEntity.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", System.Data.DbType.String, 50, poEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", System.Data.DbType.String, 50, poEntity.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", System.Data.DbType.String, 50, poEntity.CLANGUAGE_ID);
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<GLT00600DTO>(loDataTable).ToList();
    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            
            return loRtn;
        }

        public List<JournalDetailDTO> JournalDetailCls (ParamDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            List<JournalDetailDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GL_GET_JOURNAL_DETAIL_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID", System.Data.DbType.String, 50, poEntity.CJRN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", System.Data.DbType.String, 50, poEntity.CLANGUAGE_ID);
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                
                loRtn = R_Utility.R_ConvertTo<JournalDetailDTO>(loDataTable).ToList();
    
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

