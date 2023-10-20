using System.Reflection;
using GSM008500Common;
using GSM008500Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using GSM08500Common;
using GSM08500Common.DTOs;
using GSM08500Back;
using R_BackEnd;

namespace GSM08500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    
    public class GSM08500Controller : ControllerBase, IGSM08500
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM08500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM08500DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM08500DTO> loRtn = null;
            GSM08500Cls loCls;

            try
            {
                loCls = new GSM08500Cls();
                loRtn = new R_ServiceGetRecordResultDTO<GSM08500DTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CGLACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGLACCOUNT_NO);

                // poParameter.Entity.CCOMPANY_ID = "RCD";
                // poParameter.Entity.CUSER_ID = "RYC";
                // poParameter.Entity.CGLACCOUNT_NO = "21.50.0000";



                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM08500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM08500DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM08500DTO> loRtn = null;
            GSM08500Cls loCls;

            try
            {
                loCls = new GSM08500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM08500DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                // poParameter.Entity.CCOMPANY_ID = "RCD";
                // poParameter.Entity.CUSER_ID = "Admin";

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM08500DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM08500Cls loCls;

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                // poParameter.Entity.CCOMPANY_ID = "RCD";
                // poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM08500Cls();
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;        }
        
        [HttpPost]
        public GSM08500ListDTO GetStatisticAccDbList()
        {
            R_Exception loEx = new R_Exception();
            GSM08500ListDTO loRtn = null;
            List<GSM08500DTO> loResult;
            GSM08500DTO loDbPar;
            GSM08500Cls loCls;

            try
            {
                loDbPar = new GSM08500DTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                
                // loDbPar.CUSER_ID = "Admin";
                // loDbPar.CCOMPANY_ID = "RCD";

                loCls = new GSM08500Cls();
                loResult = loCls.GetStatAccListDb(loDbPar);
                loRtn = new GSM08500ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM08500DTO> GetStatisticAccDbListStream()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<CopyFromProcessCompanyDTO> GetCompanyList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<CopyFromProcessCompanyDTO> loRtn = null;
            GetCompanyDTO loParam = new GetCompanyDTO();
            GSM08500Cls loCls = new GSM08500Cls();
            List<CopyFromProcessCompanyDTO> loTempRtn = null;

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loTempRtn = loCls.GetCompanyList();

                loRtn = GetCompanyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public CopyFromProcess CopyFromProcess()
        {
            R_Exception loException = new R_Exception();
            CopyFromProcessParameter loParam = new CopyFromProcessParameter();
            CopyFromProcess loRtn = new CopyFromProcess();
            GSM08500Cls loCls = new GSM08500Cls(); 
            try
            {
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CCOPY_FROM_COMPANY_ID = R_Utility.R_GetContext<string>(ContextConstant.COPY_FROM_COMPANY_ID_CONTEXT);
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.CopyFromProcessGSM08500Cls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;  
        }

        [HttpPost]
        public ActiveInactiveDTO RSP_GS_ACTIVE_INACTIVE_StatAccMethod()
        {
            R_Exception loException = new R_Exception();
            ActiveInactiveParameterDTO loParam = new ActiveInactiveParameterDTO();
            ActiveInactiveDTO loRtn = new ActiveInactiveDTO();
            GSM08500Cls loCls = new GSM08500Cls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CGLACCOUNT_NO = R_Utility.R_GetContext<string>(ContextConstant.ACTIVE_INACTIVE_CGL_NO_CONTEXT);
                loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.ACTIVE_INACTIVE_LACTIVE_CONTEXT);
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.RSP_GS_ACTIVE_INACTIVE_StatAcc_Method(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM08500CoAExcelDTO CoAExcelTemplate()
        {
            var loEx = new R_Exception();
            var loRtn = new GSM08500CoAExcelDTO();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Statistic Account.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;           
        }

        public GSM08500UploadHeaderDTO CompanyDetail()
        {
            R_Exception loException = new R_Exception();
            GSM08500DTO loParam = new GSM08500DTO();
            GSM08500UploadHeaderDTO loRtn = new GSM08500UploadHeaderDTO();
            GSM08500Cls loCls = new GSM08500Cls(); 
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn = loCls.CompanyDetailCls(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn; 
        }

        private async IAsyncEnumerable<GSM08500DTO> GetStatAccStream(List<GSM08500DTO> poParameter)
        {
            foreach (GSM08500DTO item in poParameter)
            {
                yield return item;
            }
        }
        
        private async IAsyncEnumerable<CopyFromProcessCompanyDTO> GetCompanyStream(List<CopyFromProcessCompanyDTO> poParameter)
        {
            foreach (CopyFromProcessCompanyDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
