using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BTA2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTAUserAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BTAUserAuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("UserAuth")]
        [HttpGet]
        public JsonResult UserAuth(string UserID, string Password)
        {
            DataTable table = new DataTable();
            string con = _configuration.GetConnectionString("BTACon");
            using (OracleConnection myCon = new OracleConnection(con))
            {
                using (OracleCommand myCommand = myCon.CreateCommand())
                {
                    myCon.Open();

                    myCommand.CommandText = "PKG_BTA_UTILITY.SP_GET_USERAUTH";
                    myCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] paramsArray = new OracleParameter[5];
                    OracleParameter paramReturnCode = null;
                    OracleParameter paramReturnMessage = null;

                    int parameterIndex = -1;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_User", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = UserID;

                    paramsArray[++parameterIndex] = new OracleParameter("pi_Password", OracleDbType.Varchar2);
                    paramsArray[parameterIndex].Direction = ParameterDirection.Input;
                    paramsArray[parameterIndex].Value = Password;

                    paramsArray[++parameterIndex] = new OracleParameter("po_UserAuth", OracleDbType.RefCursor);
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
    }
}
