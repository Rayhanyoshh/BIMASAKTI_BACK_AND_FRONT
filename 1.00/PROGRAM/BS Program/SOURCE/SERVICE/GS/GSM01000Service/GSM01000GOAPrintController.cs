using BaseHeaderReportCOMMON;
using GSM001000Back;
using GSM01000Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM01000Service
{
    public class GSM01000GOAPrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GSM01000PrintParamGOADTO _AllGSM01000GOAParameter;

        #region instantiation

        public GSM01000GOAPrintController()
        {
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion
        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GSM01000GOA.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllGSM01000GOAParameter));
            pcDataSourceName = "ResponseDataModel";
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }

        #endregion


        [HttpPost]
        public R_DownloadFileResultDTO AllGOAPost(GSM01000PrintParamGOADTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult,
                    R_NetCoreUtility.R_SerializeObjectToByte<GSM01000PrintParamGOADTO>(poParameter));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult GroupOfAccountReport(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                _AllGSM01000GOAParameter =
                    R_NetCoreUtility.R_DeserializeObjectFromByte<GSM01000PrintParamGOADTO>(
                        R_DistributedCache.Cache.Get(pcGuid));
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }


        #region Helper
        private GSM01000PrintGOAResultWithBaseHeaderPrintDTO GenerateDataPrint(GSM01000PrintParamGOADTO poParam)
        {
            var loEx = new R_Exception();
            GSM01000PrintGOAResultWithBaseHeaderPrintDTO loRtn = new GSM01000PrintGOAResultWithBaseHeaderPrintDTO();
            var loParam = new BaseHeaderDTO();
            
            try
            {
                {
                    loParam.CCOMPANY_NAME = "PT Realta Chackradarma";
                    loParam.CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper();
                    loParam.CPRINT_NAME = "Group Of Account";
                    loParam.CUSER_ID = poParam.CUSER_LOGIN_ID.ToUpper();
                };
                
                GSM01000PrintGOAResultDTo loData = new GSM01000PrintGOAResultDTo()
                {
                     Title = "Group Of Accounts",
                     Header = "001",
                     Column = new GSM01000PrintColoumnGOADTO(),
                     Data = new List<GSM01000DataResultGOADTO>()
                };
                var loCls = new GSM01000Cls();
                var loCollection = loCls.GetPrintDataResultGOA(poParam);
                var loTempData = loCollection
                    .GroupBy(data1a => new
                    {
                        data1a.CGOA_CODE,
                        data1a.CGOA_NAME,
                        data1a.CGOA_BSIS,
                        data1a.CGOA_DBCR,

                    }).Select(data1b => new GSM01000DataResultGOADTO()
                    {
                        CGOA_CODE = data1b.Key.CGOA_CODE,
                        CGOA_NAME = data1b.Key.CGOA_NAME,
                        CGOA_BSIS = data1b.Key.CGOA_BSIS,
                        CGOA_DBCR = data1b.Key.CGOA_DBCR,
                    
                    
                        GSM01001DataResultGOADTO = data1b.GroupBy(data2a => new
                        {
                            data2a.CGLACCOUNT_NO,
                            data2a.CGLACCOUNT_NAME,
                            data2a.CBSIS,
                            data2a.CDBCR,
                            data2a.CCASH_FLOW_CODE,
                            data2a.CCASH_FLOW_NAME,
                            data2a.LACTIVE,
                        }).Select(data2b => new GSM01001DataResultGOADTO()
                        {
                            CGLACCOUNT_NO = data2b.Key.CGLACCOUNT_NO,
                            CGLACCOUNT_NAME = data2b.Key.CGLACCOUNT_NAME,
                            CBSIS = data2b.Key.CBSIS,
                            CDBCR = data2b.Key.CDBCR,
                            CCASH_FLOW_CODE = data2b.Key.CCASH_FLOW_CODE,
                            CCASH_FLOW_NAME = data2b.Key.CCASH_FLOW_NAME,
                            LACTIVE = data2b.Key.LACTIVE,   
                        }).ToList()
                    }).ToList();

                loRtn.GOAData = loData;
                loRtn.BaseHeaderData = loParam;
                loData.Data = loTempData;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
}
