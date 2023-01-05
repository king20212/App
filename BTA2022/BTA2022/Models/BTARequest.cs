using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace BTA2022.Models
{
    public class BTARequest
    {
        public int REQUEST_ID { get; set; }
        public string? REQUESTED_BY { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? REQUEST_DATE { get; set; }

        public string? REQUEST_STATUS { get; set; }
        public string? DEPARTMENT { get; set; }
        public string? PASSPORT_NAME { get; set; }
        public string? JOB_TITLE { get; set; }
        public string? PASSPORT_TYPE { get; set; }
        public string? EXTN_NO { get; set; }
        public string? PURPOSE { get; set; }
        public string? AIR_TICKET { get; set; }
        public string? HOTEL_RESERVATION { get; set; }
        public string? OFFSHORE_CAR_SERVICE { get; set; }
        public string? OFFSHORE_CAR_REASON { get; set; }
        public string? COST_CENTER1 { get; set; }
        public decimal? COST_CENTER1_PERCENTAGE { get; set; }
        public string? COST_CENTER2 { get; set; }
        public decimal? COST_CENTER2_PERCENTAGE { get; set; }
        public string? COST_CENTER3 { get; set; }
        public decimal? COST_CENTER3_PERCENTAGE { get; set; }
        public string? COST_CENTER4 { get; set; }
        public decimal? COST_CENTER4_PERCENTAGE { get; set; }
        public string? COST_CENTER5 { get; set; }
        public decimal? COST_CENTER5_PERCENTAGE { get; set; }
        public int? TRIP_ADVANCE_DAY { get; set; }
        public int? TRIP_ADVANCE_WEEK { get; set; }
        public decimal? TRIP_ADVANCE_SPECIAL { get; set; }
        public string? TRIP_ADVANCE_REASON { get; set; }
        public string? FIRST_APPROVER { get; set; }
        public DateTime? FIRST_APPROVAL_DATE { get; set; }
        public string? SECOND_APPROVER { get; set; }
        public DateTime? SECOND_APPROVAL_DATE { get; set; }
        public string? THIRD_APPROVER { get; set; }
        public DateTime? THIRD_APPROVAL_DATE { get; set; }
        public string? CURRENT_APPROVER { get; set; }
        public string? CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string? LAST_USER { get; set; }
        public DateTime? LAST_UPDATE { get; set; }
        public string? PASSPORT_OTHER { get; set; }
        public string? TICKET_REMARK { get; set; }
        public string? HOTEL_REMARK { get; set; }
        public string? RETURN_TO_TRAVELER_REMARK { get; set; }
        public string? COST_CENTER_6 { get; set; }
        public decimal? COST_CENTER_6_PERCENTAGE { get; set; }
        public string? DEPARTMENT_OTHER { get; set; }
        public string? TRAVEL_TYPE { get; set; }
        public string? LOCATION_CODE { get; set; }
        public string? HR_APPROVER { get; set; }
        public string? HOME_VISIT { get; set; }
        public DateTime? HR_APPROVAL_DATE { get; set; }

    }
}
