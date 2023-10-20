using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using GSM01000Back;
using GSM01000Common;
using GSM01000Common.DTOs;
using R_BackEnd;

namespace GSM01000Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM01001Controller  : ControllerBase, IGSM01001
    {
        [HttpPost]
        public IAsyncEnumerable<GSM01001ErrorValidateDTO> GetErrorProcess()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM01001ErrorValidateDTO> loRtn = null;

            try
            {
                var lcKeyGuid = R_Utility.R_GetStreamingContext<string>("UploadCOAKeyGuid");

                // var loCls = new GSM01001Cls();
                //
                // var loResult = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

                // loRtn = GetStream<GSM01001ErrorValidateDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }
    }
}

