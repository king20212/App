using System.ComponentModel.DataAnnotations;

namespace BTA2022.Models
{
    public class BTAHotel
    {
        public int BTA_HOTEL_BOOKINGS_ID { get; set; }

        public int REQUEST_ID { get; set; }

        public DateTime? CHECK_IN_DATE { get; set; }

        public DateTime? CHECK_OUT_DATE { get; set; }

        public string? HOTEL_NAME { get; set; }

        public decimal HOTEL_SURCHARE { get; set; }

        public decimal HOTLE_FARE_PER_NIGHT { get; set; }

        public int NUM_NIGHTS { get; set; }

        public string? CURRENCY { get; set; }

        public decimal EXCHANGE_RATE { get; set; }

        public decimal SERVICE_CHARGE { get; set; }

        public string? CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        public string? LAST_USER { get; set; }

        public DateTime? LAST_UPDATE { get; set; }
    }
}
