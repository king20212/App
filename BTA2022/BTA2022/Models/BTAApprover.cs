using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace BTA2022.Models
{
    public class BTAApprover
    {
        public int BTA_APPROVER_ID { get; set; }

        [Required]
        public string USER_ID { get; set; }

        [Required]
        public string APPROVER { get; set; }

        [Required]
        public int SEQ_NO { get; set; }

        public string? CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        public string? LAST_USER { get; set; }

        public DateTime? LAST_UPDATE { get; set; }


    }
}
