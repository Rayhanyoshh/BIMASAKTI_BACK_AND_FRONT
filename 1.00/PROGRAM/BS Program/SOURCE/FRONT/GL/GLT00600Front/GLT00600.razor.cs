using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;
using R_BlazorFrontEnd.Helpers;
using Lookup_GSFRONT;
using GLT00600Model.ViewModel;
using GLT00600Common.DTOs;

namespace GLT00600Front;

public partial class GLT00600
{
    private GLT00600ViewModel _GLT00600ViewModel = new GLT00600ViewModel();
    private R_ConductorGrid _conductorGridJournal;
    private R_Grid<JournalDetailDTO> _gridJournalRef;

    #region Predefined
    private void Predef_JournalEntry(R_InstantiateDockEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(JournalEntry);
        eventArgs.Parameter = "Journal Entry";
    }
    private void AfterPredef_JournalEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
    { }
    #endregion

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _GLT00600ViewModel.GetGridJournalList();
           
            await _gridJournalRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task JournalGrid_R_ServiceGetListRecord (R_ServiceGetListRecordEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            await _GLT00600ViewModel.GetGridJournalList();
            arg.ListEntityResult = _GLT00600ViewModel.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
}