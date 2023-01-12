using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using BTA2022.Models;

namespace BTA2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTARequestsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BTARequestsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_ENQUIRY.SP_GET_SUBMISSION";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[8];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Type", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = 0;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_From", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_To", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Status", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("po_Submission", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(BTARequest btareq)
        {
            string returnRequestId = "";
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    //myCommand.CommandText = query;
                    myCommand.CommandText = "PKG_BTA_REQUEST.SP_INS_BTA_REQUEST";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[47];
                    OracleParameter paramReturnNewReqNo = null;
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;
                    string returnMessage = string.Empty;

                    //Initilize Parameters
                    paramsArray[++parameterIndex] = new OracleParameter("pi_Requested_By", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.REQUESTED_BY;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_Status", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.REQUEST_STATUS;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Department", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.DEPARTMENT;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Department_Oth", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.DEPARTMENT_OTHER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Passport_Name", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.PASSPORT_NAME;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Job_Title", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.JOB_TITLE;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_TRAVEL_TYPE", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.TRAVEL_TYPE;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Passport_Type", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.PASSPORT_TYPE;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Extn_No", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.EXTN_NO;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Purpose", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.PURPOSE;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Air_Ticket", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.AIR_TICKET;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Hotel_Reservation", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.HOTEL_RESERVATION;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Offshore_Car_Service", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.OFFSHORE_CAR_SERVICE;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Offshore_Car_Reason", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.OFFSHORE_CAR_REASON;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Current_App", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.CURRENT_APPROVER;


                    paramsArray[++parameterIndex] = new OracleParameter("pi_Passport_Other", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.PASSPORT_OTHER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Ticket_Remark", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.TICKET_REMARK;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Hotel_Remark", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.HOTEL_REMARK;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center1", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;

                    if (string.IsNullOrEmpty(btareq.COST_CENTER1.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = btareq.COST_CENTER1;
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center1_Percentage", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER1_PERCENTAGE.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDecimal(btareq.COST_CENTER1_PERCENTAGE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center2", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER2.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = btareq.COST_CENTER2;
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center2_Percentage", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER2_PERCENTAGE.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDecimal(btareq.COST_CENTER2_PERCENTAGE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center3", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER3.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = btareq.COST_CENTER3;
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center3_Percentage", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER3_PERCENTAGE.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDecimal(btareq.COST_CENTER3_PERCENTAGE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center4", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER4.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = btareq.COST_CENTER4;
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center4_Percentage", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER4_PERCENTAGE.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDecimal(btareq.COST_CENTER4_PERCENTAGE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center5", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER5.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = btareq.COST_CENTER5;
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center5_Percentage", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER5_PERCENTAGE.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDecimal(btareq.COST_CENTER5_PERCENTAGE);
                    }


                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center6", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER_6.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = btareq.COST_CENTER_6;
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Cost_Center6_Percentage", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.COST_CENTER_6_PERCENTAGE.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDecimal(btareq.COST_CENTER_6_PERCENTAGE);
                    }


                    paramsArray[++parameterIndex] = new OracleParameter("pi_Trip_Adv_Day", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.TRIP_ADVANCE_DAY.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToInt32(btareq.TRIP_ADVANCE_DAY);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Trip_Adv_Week", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.TRIP_ADVANCE_WEEK.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToInt32(btareq.TRIP_ADVANCE_WEEK);
                    }


                    paramsArray[++parameterIndex] = new OracleParameter("pi_Trip_Adv_Special", OracleDbType.Decimal);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.TRIP_ADVANCE_SPECIAL.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDouble(btareq.TRIP_ADVANCE_SPECIAL);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Trip_Adv_Reason", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.TRIP_ADVANCE_REASON;



                    paramsArray[++parameterIndex] = new OracleParameter("pi_First_Approver", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.FIRST_APPROVER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_First_Approval_Date", OracleDbType.Date);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.FIRST_APPROVAL_DATE.ToString()) || btareq.FIRST_APPROVAL_DATE.ToString().Equals(DateTime.MinValue.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDateTime(btareq.FIRST_APPROVAL_DATE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Second_Approver", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.SECOND_APPROVER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Second_Approval_Date", OracleDbType.Date);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.SECOND_APPROVAL_DATE.ToString()) || btareq.SECOND_APPROVAL_DATE.ToString().Equals(DateTime.MinValue.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDateTime(btareq.SECOND_APPROVAL_DATE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Third_Approver", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.THIRD_APPROVER;


                    paramsArray[++parameterIndex] = new OracleParameter("pi_Third_Approval_Date", OracleDbType.Date);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.THIRD_APPROVAL_DATE.ToString()) || btareq.THIRD_APPROVAL_DATE.ToString().Equals(DateTime.MinValue.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDateTime(btareq.THIRD_APPROVAL_DATE);
                    }



                    paramsArray[++parameterIndex] = new OracleParameter("pi_HR_Approval_Date", OracleDbType.Date);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    if (string.IsNullOrEmpty(btareq.HR_APPROVAL_DATE.ToString()) || btareq.HR_APPROVAL_DATE.ToString().Equals(DateTime.MinValue.ToString()))
                    {
                        paramsArray[parameterIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        paramsArray[parameterIndex].Value = Convert.ToDateTime(btareq.HR_APPROVAL_DATE);
                    }

                    paramsArray[++parameterIndex] = new OracleParameter("pi_HR_Approver", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.HR_APPROVER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Home_Visit", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btareq.HOME_VISIT;



                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

                    paramsArray[++parameterIndex] = new OracleParameter("po_Request_Id", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;
                    paramReturnNewReqNo = paramsArray[parameterIndex];

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

                    returnRequestId = paramReturnNewReqNo.Value.ToString();

                    myCon.Close();

                }
            }

            return new JsonResult(returnRequestId);

        }

        [HttpPut]
        public JsonResult Put(BTARequest btareq)
        {
            string query = @"update bta_request set 
                            REQUESTED_BY = '" + btareq.REQUESTED_BY + @"'
                            REQUEST_DATE = '" + btareq.REQUEST_DATE + @"'
                            REQUEST_STATUS = '" + btareq.REQUEST_STATUS + @"'
                            DEPARTMENT = '" + btareq.DEPARTMENT + @"'
                            PASSPORT_NAME = '" + btareq.PASSPORT_NAME + @"'
                            JOB_TITLE = '" + btareq.JOB_TITLE + @"'
                            PASSPORT_TYPE = '" + btareq.PASSPORT_TYPE + @"'
                            EXTN_NO = '" + btareq.EXTN_NO + @"'
                            PURPOSE = '" + btareq.PURPOSE + @"'
                            AIR_TICKET = '" + btareq.AIR_TICKET + @"'
                            HOTEL_RESERVATION = '" + btareq.HOTEL_RESERVATION + @"'
                            OFFSHORE_CAR_SERVICE = '" + btareq.OFFSHORE_CAR_SERVICE + @"'
                            OFFSHORE_CAR_REASON = '" + btareq.OFFSHORE_CAR_REASON + @"'
                            COST_CENTER1 = '" + btareq.COST_CENTER1 + @"'
                            COST_CENTER1_PERCENTAGE = '" + btareq.COST_CENTER1_PERCENTAGE + @"'
                            COST_CENTER2 = '" + btareq.COST_CENTER2 + @"'
                            COST_CENTER2_PERCENTAGE = '" + btareq.COST_CENTER2_PERCENTAGE + @"'
                            COST_CENTER3 = '" + btareq.COST_CENTER3 + @"'
                            COST_CENTER3_PERCENTAGE = '" + btareq.COST_CENTER3_PERCENTAGE + @"'
                            COST_CENTER4 = '" + btareq.COST_CENTER4 + @"'
                            COST_CENTER4_PERCENTAGE = '" + btareq.COST_CENTER4_PERCENTAGE + @"'
                            COST_CENTER5 = '" + btareq.COST_CENTER5 + @"'
                            COST_CENTER5_PERCENTAGE = '" + btareq.COST_CENTER5_PERCENTAGE + @"'
                            TRIP_ADVANCE_DAY = '" + btareq.TRIP_ADVANCE_DAY + @"'
                            TRIP_ADVANCE_WEEK = '" + btareq.TRIP_ADVANCE_WEEK + @"'
                            TRIP_ADVANCE_SPECIAL = '" + btareq.TRIP_ADVANCE_SPECIAL + @"'
                            TRIP_ADVANCE_REASON = '" + btareq.TRIP_ADVANCE_REASON + @"'
                            FIRST_APPROVER = '" + btareq.FIRST_APPROVER + @"'
                            FIRST_APPROVAL_DATE = '" + btareq.FIRST_APPROVAL_DATE + @"'
                            SECOND_APPROVER = '" + btareq.SECOND_APPROVER + @"'
                            SECOND_APPROVAL_DATE = '" + btareq.SECOND_APPROVAL_DATE + @"'
                            THIRD_APPROVER = '" + btareq.THIRD_APPROVER + @"'
                            THIRD_APPROVAL_DATE = '" + btareq.THIRD_APPROVAL_DATE + @"'
                            CURRENT_APPROVER = '" + btareq.CURRENT_APPROVER + @"'
                            CREATED_BY = '" + btareq.CREATED_BY + @"'
                            CREATED_DATE = '" + btareq.CREATED_DATE + @"'
                            LAST_USER = '" + btareq.LAST_USER + @"'
                            LAST_UPDATE = '" + btareq.LAST_UPDATE + @"'
                            PASSPORT_OTHER = '" + btareq.PASSPORT_OTHER + @"'
                            TICKET_REMARK = '" + btareq.TICKET_REMARK + @"'
                            HOTEL_REMARK = '" + btareq.HOTEL_REMARK + @"'
                            RETURN_TO_TRAVELER_REMARK = '" + btareq.RETURN_TO_TRAVELER_REMARK + @"'
                            COST_CENTER_6 = '" + btareq.COST_CENTER_6 + @"'
                            COST_CENTER_6_PERCENTAGE = '" + btareq.COST_CENTER_6_PERCENTAGE + @"'
                            DEPARTMENT_OTHER = '" + btareq.DEPARTMENT_OTHER + @"'
                            TRAVEL_TYPE = '" + btareq.TRAVEL_TYPE + @"'
                            LOCATION_CODE = '" + btareq.LOCATION_CODE + @"'
                            HR_APPROVER = '" + btareq.HR_APPROVER + @"'
                            HOME_VISIT = '" + btareq.HOME_VISIT + @"'
                            HR_APPROVAL_DATE = '" + btareq.HR_APPROVAL_DATE + @"'
                            where request_id = '" + btareq.REQUEST_ID + @"' ";
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = query;

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully.");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from bta_request where request_id = " + id + @"
                            ";

            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = query;

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully.");

        }

        [Route("GetPendingApprovalRequests")]
        [HttpGet]
        public JsonResult GetPendingApprovalRequests()
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_ENQUIRY.SP_GET_APPROVAL";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[12];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "APP";

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Type", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = 0;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Access", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = 0;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_From", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_To", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Traveler", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Department", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Status", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_HRApproval", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = 0;

                    paramsArray[++parameterIndex] = new OracleParameter("po_Approval", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);

        }

        [Route("GetDepartments")]
        [HttpGet]
        public JsonResult GetDepartments()
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();

                    myCommand.CommandText = "PKG_BTA_LOOKUP.SP_GET_DEPTLIST";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[4];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

                    paramsArray[++parameterIndex] = new OracleParameter("po_DeptList", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [Route("GetCurrencies")]
        [HttpGet]
        public JsonResult GetCurrencies()
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_LOOKUP.SP_GET_CURRENCYF";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[3];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("po_Currency", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);

        }

        [Route("GetTravelClasses")]
        [HttpGet]
        public JsonResult GetTravelClasses()
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();

                    myCommand.CommandText = "PKG_BTA_LOOKUP.SP_GET_CLASSLIST";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[4];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

                    paramsArray[++parameterIndex] = new OracleParameter("po_ClassList", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [Route("GetTravelTypes")]
        [HttpGet]
        public JsonResult GetTravelTypes()
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();

                    myCommand.CommandText = "PKG_BTA_LOOKUP.SP_GET_TRAVELLIST";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[4];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

                    paramsArray[++parameterIndex] = new OracleParameter("po_TravelList", OracleDbType.RefCursor);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Output;

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [Route("ApproveRequest/{id}")]
        [HttpPatch]
        public JsonResult ApproveRequest(int id)
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_REQUEST.SP_APP_REQUEST";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[4];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_Id", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = id;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "APP";

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Approved Successfully.");
        }

        [Route("RejectRequest/{id}")]
        [HttpPatch]
        public JsonResult RejectRequest(int id)
        {
            //string query = @"update bta_request set 
            //                REQUEST_STATUS = 'R'
            //                where request_id = '" + id + @"' ";
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_REQUEST.SP_REJ_REQUEST";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[5];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Request_Id", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = id;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Remark", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = "KINCHA";

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

                    OracleDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Rejected Successfully.");
        }
    }
}
