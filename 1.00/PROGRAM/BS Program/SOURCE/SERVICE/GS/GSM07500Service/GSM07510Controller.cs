using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using GSM07500Back;
using GSM07500Common;
using GSM07500Common.DTOs;
using GSM07510Common;
using R_BackEnd;

namespace GSM07500Service
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class GSM07510Controller  : ControllerBase, IGSM07510
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM07510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM07510DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM07510DTO> loRtn = null;
            GSM07510Cls loCls;
            LastYearDTO loYear;
            
            try
            {
                loCls = new GSM07510Cls();
                loRtn = new R_ServiceGetRecordResultDTO<GSM07510DTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                
                // loYear = loCls.GetLastYear(poParameter.Entity);
                // poParameter.Entity.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);


                // poParameter.Entity.CCOMPANY_ID = "rcd";
                // poParameter.Entity.CYEAR = "2023";
                // poParameter.Entity.CUSER_ID = "RYC";
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
        public R_ServiceSaveResultDTO<GSM07510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM07510DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM07510DTO> loRtn = new R_ServiceSaveResultDTO<GSM07510DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GSM07510Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM07510DTO> poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM07510Cls loCls;
            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                
                loCls = new GSM07510Cls();
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM07510ListDTO PeriodList()
        {
            R_Exception loEx = new R_Exception();
            GSM07510ListDTO loRtn = null;
            List<GSM07510DTO> loResult;
            GSM07510DTO loDbPar;
            GSM07510Cls loCls;

            try
            {
                loDbPar = new GSM07510DTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                // loDbPar.CCOMPANY_ID = "RCD";

                loCls = new GSM07510Cls();
                loResult = loCls.GetPeriodDbList(loDbPar);
                foreach (var item in loResult)
                {
                    if (item.LPERIOD_MODE)
                    {
                        item.DescPeriodMode = "Normal Calendar";
                    }
                    else
                    {
                        item.DescPeriodMode = "Custom Period";
                    }
                }
                loRtn = new GSM07510ListDTO { Data = loResult };
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM07510DTO> PeriodListStream()
        {
            R_Exception loException = new R_Exception();
            GSM07510DTO loDbPar;
            List<GSM07510DTO> loRtnTmp;
            GSM07510Cls loCls;
            IAsyncEnumerable<GSM07510DTO> loRtn = null;

            try
            {
                loDbPar = new GSM07510DTO();
                loDbPar.CCOMPANY_ID = "RCD";
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                
                loCls = new GSM07510Cls();
                loRtnTmp = loCls.GetPeriodDbList(loDbPar);

                loRtn = GetPeriodHelper(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        [HttpPost]
        public LastYearDTO GetLastYear()
        {
            R_Exception loEx = new R_Exception();
            LastYearDTO loRtn = null;
            GSM07510DTO loDbPar;
            GSM07510Cls loCls;
            LastYearDTO loResult = null;
            
            try
            {
                loDbPar = new GSM07510DTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                
                loCls = new GSM07510Cls();
                loResult = loCls.GetLastYear(loDbPar);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private async IAsyncEnumerable<GSM07510DTO> GetPeriodHelper (List<GSM07510DTO> poParameter)
        {
            foreach (GSM07510DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
