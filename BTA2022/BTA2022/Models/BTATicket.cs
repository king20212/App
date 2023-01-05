namespace BTA2022.Models
{
    public class BTATicket
    {
        public int BTA_TICKET_ID { get; set; }

        public int REQUEST_ID { get; set; }

        public DateTime TICKET_DATE { get; set; }

        public string TICKET_FROM { get; set; }

        public string TICKET_TO { get; set; }

        public string TICKET_CLASS { get; set; }

        public string FLIGHT_DETAILS { get; set; }

        public string CURRENCY { get; set; }

        public decimal EXCHANGE_RATE { get; set; }

        public decimal FLIGHT_FARE { get; set; }

        public decimal TICKET_SURCHARGE { get; set; }

        public string? CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        public string? LAST_USER { get; set; }

        public DateTime? LAST_UPDATE { get; set; }
    }
}
