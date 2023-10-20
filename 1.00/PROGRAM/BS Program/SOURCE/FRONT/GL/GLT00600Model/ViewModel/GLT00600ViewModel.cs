using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Helpers;
using GLT00600Common.DTOs;

namespace GLT00600Model.ViewModel
{
    public class GLT00600ViewModel : R_ViewModel<GLT00600DTO>
    {
        private GLT00600Model _GLT00600Model = new GLT00600Model();
        public JournalDetailDTO loJournalDetailEntity = new JournalDetailDTO();
        public ObservableCollection<JournalDetailDTO> loGridList { get; set; } = new ObservableCollection<JournalDetailDTO>();


        public async Task GetGridJournalList()
        {
            R_Exception loEx = new R_Exception();
            JournalDetailDataDTO loResult = null;
            try
            {
                loResult = await _GLT00600Model.GetJournalDetailGrid();
                loGridList = new ObservableCollection<JournalDetailDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }
}
