using R_CommonFrontBackAPI;
using System.Collections.Generic;
using GLT00600Common.DTOs;

namespace GLT00600Common
{
    public interface IGLT00600 : R_IServiceCRUDBase<GLT00600DTO>
    {
        GSM_COMPANY_DTO VarGSMCompany();
        GL_SYSTEM_PARAM_DTO GLSystemParameter();
        List<USER_DEPARTMENT_LIST_DTO> UserDepartList();
        PeriodDTO PeriodStartDate();
        EnableOptionDTO EnableOption();
        GSM_TRANSACTION_CODE_DTO TransactionCode();
        PeriodDTO MinMaxYear();
        List<StatusCodeDTO> StatusCode();
        List<GLT00600DTO> JournalList();
        List<JournalDetailDTO> JournalDetailGrid();
    }
}