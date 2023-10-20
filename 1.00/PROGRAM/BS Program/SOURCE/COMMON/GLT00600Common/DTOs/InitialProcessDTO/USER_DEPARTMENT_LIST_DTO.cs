using System;

namespace GLT00600Common.DTOs
{
    public class USER_DEPARTMENT_LIST_DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CMANAGER_NAME { get; set; }
        public bool LEVERYONE { get; set; }
        public bool LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}