using BTA2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BTA2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTATicketsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BTATicketsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{RequestID}")]
        public JsonResult Get(int RequestID)
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();

                    myCommand.CommandText = "PKG_BTA_REQUEST.SP_GET_BTA_TICKETS";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[4];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_Id", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = RequestID;

                    paramsArray[++parameterIndex] = new OracleParameter("po_RetCD", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                    paramReturnCode = paramsArray[parameterIndex];

                    paramsArray[++parameterIndex] = new OracleParameter("po_RetMsg", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Size = 255;
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                    paramReturnMessage = paramsArray[parameterIndex];

                    paramsArray[++parameterIndex] = new OracleParameter("po_Bta_Tickets", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;


                    foreach (OracleParameter parameter in paramsArray)
                    {
                        myCommand.Parameters.Add(parameter);
                    }

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(List<BTATicket> btatickets)
        {
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                foreach (var btaticket in btatickets)
                {
                    using (OracleCommand myCommand = myCon.CreateCommand())
                    {
                        myCon.Open();
                        myCommand.CommandText = "PKG_BTA_REQUEST.SP_INS_BTA_TICKETS";
                        myCommand.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] paramsArray = new OracleParameter[14];
                        OracleParameter paramReturnNewTicketNo = null;
                        OracleParameter paramReturnCode = null;
                        OracleParameter paramReturnMessage = null;

                        int parameterIndex = -1;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Request_Id", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.REQUEST_ID;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Ticket_Date", OracleDbType.Date);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.TICKET_DATE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Ticket_From", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.TICKET_FROM;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Ticket_To", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.TICKET_TO;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Ticket_Class", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.TICKET_CLASS;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Flight_Details", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.FLIGHT_DETAILS;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_CURRENCY", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.CURRENCY;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_EXCHANGE_RATE", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.EXCHANGE_RATE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_FLIGHT_FARE", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.FLIGHT_FARE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_TICKET_SURCHARGE", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btaticket.TICKET_SURCHARGE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = "KINCHA";

                        paramsArray[++parameterIndex] = new OracleParameter("po_Bta_Ticket_Id", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                        paramReturnNewTicketNo = paramsArray[parameterIndex];

                        paramsArray[++parameterIndex] = new OracleParameter("po_RetCD", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                        paramReturnCode = paramsArray[parameterIndex];

                        paramsArray[++parameterIndex] = new OracleParameter("po_RetMsg", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Size = 255;
                        paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                        paramReturnMessage = paramsArray[parameterIndex];

                        foreach (OracleParameter parameter in paramsArray)
                        {
                            myCommand.Parameters.Add(parameter);
                        }

                        myCommand.ExecuteNonQuery();

                        myCon.Close();

                    }
                }
            }

            return new JsonResult("Added Successfully.");

        }
    }
}
