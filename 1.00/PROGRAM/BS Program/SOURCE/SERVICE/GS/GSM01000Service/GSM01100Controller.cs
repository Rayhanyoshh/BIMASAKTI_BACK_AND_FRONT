using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using GSM001000Back;
using GSM01000Common;
using GSM01000Back;
using GSM01000Common.DTOs;
using R_BackEnd;

namespace GSM01000Service
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class GSM01100Controller : ControllerBase, IGSM01100
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM01100DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM01100DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM01100DTO> loRtn = null;
            GSM01100Cls loCls;

            try
            {
                loCls = new GSM01100Cls();
                loRtn = new R_ServiceGetRecordResultDTO<GSM01100DTO>();
                
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                // poParameter.Entity.CCOMPANY_ID = "RCD";
                // poParameter.Entity.CUSER_ID = "Admin";
                // poParameter.Entity.CGLACCOUNT_NO = "11.10.1000";
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
        public R_ServiceSaveResultDTO<GSM01100DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM01100DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM01100DTO> loRtn = null;
            GSM01100Cls loCls;

            try
            {
                loCls = new GSM01100Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM01100DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CCREATE_BY = R_BackGlobalVar.USER_ID;
                
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM01100DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public GSM01100UserListDTO GetCoAUserList()
        {
            R_Exception loEx = new R_Exception();
            GSM01100UserListDTO loRtn = null; 
            List<GSM01100DTO> loResult;
            GOAHeadListDbParameter loDbPar;
            GSM01100Cls loCls;
            
            try
            {
                loDbPar = new GOAHeadListDbParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CGLACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGLACCOUNT_NO);
                // loDbPar.CCOMPANY_ID = "RCD";
                // loDbPar.CGLACCOUNT_NO = "11.10.1000";

                loCls = new GSM01100Cls();
                loResult = loCls.GetCoAUserListDb(loDbPar);
                loRtn = new GSM01100UserListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM01100DTO> GetCoAUserListStream()
        {
            R_Exception loException = new R_Exception();
            GOAHeadListDbParameter loDbPar;
            List<GSM01100DTO> loRtnTmp;
            GSM01100Cls loCls;
            IAsyncEnumerable<GSM01100DTO> loRtn = null;
            
            try
            {
                loDbPar = new GOAHeadListDbParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CGLACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGLACCOUNT_NO);
                // loDbPar.CCOMPANY_ID = "RCD";
                // loDbPar.CGLACCOUNT_NO = "11.10.1000";

                loCls = new GSM01100Cls();
                loRtnTmp = loCls.GetCoAUserListDb(loDbPar);

                loRtn = GetCOAUserStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM01100DTO> GetCOAUserStream(List<GSM01100DTO> poParameter)
        {
            foreach (GSM01100DTO item in poParameter)
            {
                yield return item;
            }
        }
        
        [HttpPost]
        public IAsyncEnumerable<AssignUserDTO> GetUserToAssignList()
        {
            GOAHeadListDbParameter loDbPar;
            List<AssignUserDTO> loRtnTemp = null;
            R_Exception loEx = new R_Exception();
            GSM01100Cls loCls;
            try
            {
                loDbPar = new GOAHeadListDbParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CPROGRAM_ID = "GSM01000";
                loDbPar.CGLACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGLACCOUNT_NO);
                loCls = new GSM01100Cls();
                loRtnTemp = loCls.GetUserToAssignList(loDbPar);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return GetUserListHelper(loRtnTemp);

        }

        [HttpPost]
        public AssignUserResultDTO AssignUserAction(UsertoAssignParam poParam)
        {
            var loEx = new R_Exception();
            AssignUserResultDTO loRtn = new AssignUserResultDTO();
            GSM01100DTO loparam;

            try
            {
                GSM01100Cls loCls = new GSM01100Cls();

                loparam = new GSM01100DTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CGLACCOUNT_NO = poParam.CGLACCOUNT_NO,
                    CUSER_LIST = poParam.CUSER_LIST
                };
                
                loCls.SaveAssignUserToDb(loparam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM01100DTO GetParameterInfo()
        {
            R_Exception loException = new R_Exception();
            UsertoAssignParam loDbPar;
            GSM01100Cls loCls;
            GSM01100DTO loReturn = null;

            try
            {
                loDbPar = new();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

         
                loDbPar.CGLACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGLACCOUNT_NO);
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new ();

                loReturn = loCls.GSM01100GetParameterDb(loDbPar);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        private async IAsyncEnumerable<AssignUserDTO> GetUserListHelper(List<AssignUserDTO> loRtnTemp)
        {
            foreach (AssignUserDTO loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }
    }
}

