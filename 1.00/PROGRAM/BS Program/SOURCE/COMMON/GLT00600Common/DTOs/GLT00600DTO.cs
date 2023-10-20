using System;

namespace GLT00600Common.DTOs
{
    public class GLT00600DTO
    {
        public int INO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURENCY_CODE { get; set; }
        public decimal NTRANS_AMMOUNT { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CREC_ID { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public bool LAALOW_APPROVE { get; set; }
    }
}