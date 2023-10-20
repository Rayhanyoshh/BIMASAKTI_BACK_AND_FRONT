using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using GSM01000Common;
using GSM01000Back;
using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GSM001000Back;
using GSM01000Common.DTOs;

namespace GSM01000Service;

public class GSM01000PrintController : R_ReportControllerBase
{
    private R_ReportFastReportBackClass _ReportCls;
    private GSM01000PrintParamCOADTO _AllGSM01000Parameter;

    #region instantiation

    public GSM01000PrintController()
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
        pcFileTemplate = System.IO.Path.Combine("Reports", "GSM01000.frx");
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GenerateDataPrint(_AllGSM01000Parameter));
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
    public R_DownloadFileResultDTO AllCOAPost(GSM01000PrintParamCOADTO poParameter)
    {
        R_Exception loException = new R_Exception();
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            R_DistributedCache.R_Set(loRtn.GuidResult,
                R_NetCoreUtility.R_SerializeObjectToByte<GSM01000PrintParamCOADTO>(poParameter));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ChartOfAccountReport(string pcGuid)
    {
        R_Exception loException = new R_Exception();
        FileStreamResult loRtn = null;
        try
        {
            //Get Parameter
            _AllGSM01000Parameter =
                R_NetCoreUtility.R_DeserializeObjectFromByte<GSM01000PrintParamCOADTO>(
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
    private GSM01000PrintCOAResultWithBaseHeaderPrintDTO GenerateDataPrint(GSM01000PrintParamCOADTO poParam)
    {
        var loEx = new R_Exception();
        GSM01000PrintCOAResultWithBaseHeaderPrintDTO loRtn = new GSM01000PrintCOAResultWithBaseHeaderPrintDTO();

        try
        {
            var loCls = new GSM01000Cls();

            var loCollection = loCls.GetPrintDataResult(poParam);

            // Set Base Header Data
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                CPRINT_NAME = "Chart Of Account",
                CUSER_ID = poParam.CUSER_LOGIN_ID.ToUpper(),
            };

            // get image
            //Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
            //var lcResourceFile = "BIMASAKTI_GS_API.Image.CompanyLogo.png";
            //using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
            //{

            //    var ms = new MemoryStream();
            //    resFilestream.CopyTo(ms);
            //    var bytes = ms.ToArray();

            //    loParam.BLOGO_COMPANY = bytes;
            //}


            var loData = new GSM01000PrintCOAResultDTo();

            loData.Header = $"{poParam.CCOMPANY_ID}";
            loData.Data = loCollection;

            loRtn.BaseHeaderData = loParam;
            loRtn.CenterData = loData;
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

