using API_QLNH.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ThucDonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select MaThucDon ,TenThucDon from ThucDon";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QLnhahang");
            SqlDataReader myreader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(ThucDon thucDon)
        {
            string query = @"Insert into ThucDon values" + "('" + thucDon.TenThucDon + "')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QLnhahang");
            SqlDataReader myreader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Thêm mới thành công");
        }

        [HttpPut]
        public JsonResult put(ThucDon thucDon)
        {
            string query = @"update ThucDon set TenThucDon='" + thucDon.TenThucDon + "'" + "where MaThucDon =" + thucDon.MaThucDon;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QLnhahang");
            SqlDataReader myreader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Cập nhật thành công");
        }
        [HttpDelete]
        public JsonResult Delete(ThucDon thucDon)
        {
            string query = @"Delete From ThucDon where MaThucDon =" + thucDon.MaThucDon;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QLnhahang");
            SqlDataReader myreader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Xóa thành công");
        }
    }
}
