using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using GLT00600Common.DTOs;
using GLT00600Common;

namespace GLT00600Model
{
    public class GLT00600Model: R_BusinessObjectServiceClientBase<GLT00600DTO>, IGLT00600
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLT00600";
        private const string DEFAULT_MODULE = "GL";
        
        
        public GLT00600Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            string pcModuleName = DEFAULT_MODULE, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) 
            : base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GetJournalDetailGrid

        public async Task<JournalDetailDataDTO> GetJournalDetailGrid()
        {
            var loEx = new R_Exception();
            JournalDetailDataDTO loResult = null;   

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<JournalDetailDataDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00600.JournalDetailGrid), 
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

        public GSM_COMPANY_DTO VarGSMCompany()
        {
            throw new NotImplementedException();
        }

        public GL_SYSTEM_PARAM_DTO GLSystemParameter()
        {
            throw new NotImplementedException();
        }

        public List<USER_DEPARTMENT_LIST_DTO> UserDepartList()
        {
            throw new NotImplementedException();
        }

        public PeriodDTO PeriodStartDate()
        {
            throw new NotImplementedException();
        }

        public EnableOptionDTO EnableOption()
        {
            throw new NotImplementedException();
        }

        public GSM_TRANSACTION_CODE_DTO TransactionCode()
        {
            throw new NotImplementedException();
        }

        public PeriodDTO MinMaxYear()
        {
            throw new NotImplementedException();
        }

        public List<StatusCodeDTO> StatusCode()
        {
            throw new NotImplementedException();
        }

        public List<GLT00600DTO> JournalList()
        {
            throw new NotImplementedException();
        }

        public List<JournalDetailDTO> JournalDetailGrid()
        {
            throw new NotImplementedException();
        }
    }
}