using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using BTA2022.Models;
using System.Globalization;

namespace BTA2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTAApproversController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BTAApproversController(IConfiguration configuration)
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

                    myCommand.CommandText = "PKG_BTA_APPROVER.SP_GET_APPROVER";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[4];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Traveler", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = DBNull.Value;

                    paramsArray[++parameterIndex] = new OracleParameter("po_Approver", OracleDbType.RefCursor);
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
        public JsonResult Post(BTAApprover btaapp)
        {
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_APPROVER.SP_INS_APPROVER";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[6];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Traveler", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.USER_ID;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Approver", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.APPROVER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_SeqNo", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.SEQ_NO;

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

                    myCommand.ExecuteNonQuery();

                    myCon.Close();

                }
            }

            return new JsonResult("Added Successfully.");

        }

        [HttpPut]
        public JsonResult Put(BTAApprover btaapp)
        {
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();
                    myCommand.CommandText = "PKG_BTA_APPROVER.SP_UPD_APPROVER";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[7];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_ApproverID", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.BTA_APPROVER_ID;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Traveler", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.USER_ID;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Approver", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.APPROVER;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_SeqNo", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = btaapp.SEQ_NO;

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

                    myCommand.ExecuteNonQuery();

                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully.");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();

                    myCommand.CommandText = "PKG_BTA_APPROVER.SP_DEL_APPROVER";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[3];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_ApproverID", OracleDbType.Int32);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = id;

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

            return new JsonResult("Deleted Successfully.");

        }

        [Route("GetAllUserNames")]
        [HttpGet]
        public JsonResult GetAllUserNames()
        {
            string query = @"select user_id, user_name from user_table";
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

            return new JsonResult(table);

        }

        [Route("GetAllApproverNames")]
        [HttpGet]
        public JsonResult GetAllApproverNames()
        {
            string query = @"select user_id, user_name from user_table";
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

            return new JsonResult(table);

        }

    }
}
