using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GLT00600Common.DTOs
{
    public class JournalDetailDataDTO : R_APIResultBaseDTO
    {

        public List<JournalDetailDTO> Data { get; set; }

    }

    public class JournalDetailDTO :R_APIResultBaseDTO
    {
        public long INO { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CREC_ID { get; set; }
        public string CSEQ_NO { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public string CDETAIL_DESC { get; set;}
        public string CDOCUMENT_NO { get; set;}
        public string CDOCUMENT_DATE { get; set;}
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NLDEBIT { get; set; }
        public decimal NLCREDIT { get; set; }
        public decimal NBDEBIT { get; set; }
        public decimal NBCREDIT { get; set; }
        public string CBSIS { get; set; }
    }
}