using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using GLT00600Back;
using GLT00600Common;
using GLT00600Common.DTOs;
using R_BackEnd;

namespace GLT00600Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00600Controller : ControllerBase, IGLT00600
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLT00600DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLT00600DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GLT00600DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLT00600DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLT00600DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public GSM_COMPANY_DTO VarGSMCompany()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            GSM_COMPANY_DTO loRtn = new GSM_COMPANY_DTO();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CCOMPANY_ID = "rcd";
                loRtn = loCls.CompanyCheckCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public GL_SYSTEM_PARAM_DTO GLSystemParameter()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            GL_SYSTEM_PARAM_DTO loRtn = new GL_SYSTEM_PARAM_DTO();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParam.CCOMPANY_ID = "rcd";
                loParam.CLANGUAGE_ID = "en";
                
                loRtn = loCls.SystemParamCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public List<USER_DEPARTMENT_LIST_DTO> UserDepartList()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            List<USER_DEPARTMENT_LIST_DTO> loRtn = new List<USER_DEPARTMENT_LIST_DTO>();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CCOMPANY_ID = "rcd";
                loParam.CUSER_ID = "RYC";
                
                loRtn = loCls.DepartmentListLookup(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public PeriodDTO PeriodStartDate()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            PeriodDTO loRtn = new PeriodDTO();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CCOMPANY_ID = "rcd";
                loParam.CCYEAR = "2023";
                loParam.CPERIOD_NO = "01";
                // loParam.CLANGUAGE_ID = "en";
                
                loRtn = loCls.PeriodStartCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public EnableOptionDTO EnableOption()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            EnableOptionDTO loRtn = new EnableOptionDTO();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParam.CCOMPANY_ID = "rcd";
                // loParam.CLANGUAGE_ID = "en";
                
                loRtn = loCls.CommitJrnCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public GSM_TRANSACTION_CODE_DTO TransactionCode()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            GSM_TRANSACTION_CODE_DTO loRtn = new GSM_TRANSACTION_CODE_DTO();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CCOMPANY_ID = "rcd";
                loParam.CTRANSACTION_CODE = "000040";
                
                loRtn = loCls.TransactionCodeCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public PeriodDTO MinMaxYear()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            PeriodDTO loRtn = new PeriodDTO();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CCOMPANY_ID = "rcd";
                
                loRtn = loCls.MinMaxYearCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public List<StatusCodeDTO> StatusCode()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            List<StatusCodeDTO> loRtn = new List<StatusCodeDTO>();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.LANG_ID = R_BackGlobalVar.CULTURE;
                loParam.CCOMPANY_ID = "rcd";
                loParam.LANG_ID = "en";
                
                loRtn = loCls.StatusCodeCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public List<GLT00600DTO> JournalList()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            List<GLT00600DTO> loRtn = new List<GLT00600DTO>();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CCOMPANY_ID = "rcd";
                loParam.CLANGUAGE_ID = "en";
                loParam.CUSER_ID = "ryc";
                loParam.CTRANS_CODE = "000020";
                loParam.CDEPT_CODE = "ACC";
                loParam.CPERIOD = "202307";
                loParam.CSTATUS = "80";
                loParam.CSEARCH_TEXT = "";
                
                loRtn = loCls.JournalListCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        [HttpPost]
        public List<JournalDetailDTO> JournalDetailGrid()
        {
            R_Exception loException = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            List<JournalDetailDTO> loRtn = new List<JournalDetailDTO>();
            GLT00600Cls loCls = new GLT00600Cls(); 
            try
            {
                loParam.CJRN_ID = "2C6AFFF4-848E-405F-9A0C-C6E9AD240748";
                loParam.CLANGUAGE_ID = "en";
                
                loRtn = loCls.JournalDetailCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }
    }
}

