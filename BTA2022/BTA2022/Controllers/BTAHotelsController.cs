using BTA2022.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using System.Security.Principal;

namespace BTA2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTAHotelsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BTAHotelsController(IConfiguration configuration)
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

                    myCommand.CommandText = "PKG_BTA_REQUEST.SP_GET_HOTEL_BOOKINGS";
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

                    paramsArray[++parameterIndex] = new OracleParameter("po_Hotel_Bookings", OracleDbType.RefCursor);
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
        public JsonResult Post(List<BTAHotel> btahotels)
        {

            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                foreach (var btahotel in btahotels)
                {
                    using (OracleCommand myCommand = myCon.CreateCommand())
                    {
                        myCon.Open();
                        myCommand.CommandText = "PKG_BTA_REQUEST.SP_INS_HOTEL_BOOKINGS";
                        myCommand.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] paramsArray = new OracleParameter[14];
                        OracleParameter paramReturnNewHotelNo = null;
                        OracleParameter paramReturnCode = null;
                        OracleParameter paramReturnMessage = null;

                        int parameterIndex = -1;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Request_Id", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.REQUEST_ID;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Check_In_Date", OracleDbType.Date);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value =  btahotel.CHECK_IN_DATE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Check_Out_Date", OracleDbType.Date);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.CHECK_OUT_DATE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Hotel_Name", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.HOTEL_NAME;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Hotel_Surchare", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.HOTEL_SURCHARE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Fare_Per_Night", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.HOTLE_FARE_PER_NIGHT;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_Num_Nights", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.NUM_NIGHTS;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_CURRENCY", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.CURRENCY;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_EXCHANGE_RATE", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.EXCHANGE_RATE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_SERVICE_CHARGE", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = btahotel.SERVICE_CHARGE;

                        paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                        paramsArray[parameterIndex].Value = "KINCHA";

                        paramsArray[++parameterIndex] = new OracleParameter("po_Hotel_Bookings_Id", OracleDbType.Int32);
                        paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                        paramReturnNewHotelNo = paramsArray[parameterIndex];

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
