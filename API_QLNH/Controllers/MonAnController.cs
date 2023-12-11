using API_QLNH.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public MonAnController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }    
       
        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select MaMonAn ,TenMonAn,ThucDon,NgayTao"+",AnhMonAn from MonAn";
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
        public JsonResult Post(MonAn monAn)
        {
            string query = @"Insert into MonAn values" + "('" + monAn.TenMonAn + "'"+",'"+monAn.ThucDon+"'"+",'"+monAn.NgayTao+"'"+",'"+monAn.AnhMonAn+"')";
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
        public JsonResult put(MonAn monAn)
        {
            string query = @"update MonAn set TenMonAn='" + monAn.TenMonAn + "'"+"ThucDon='"+monAn.ThucDon+"'"+"NgayTao='"+monAn.NgayTao+"'"+"AnhMonAn='"+monAn.AnhMonAn+"'" + "where MaMonAn=" + monAn.MaMonAn;
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
        public JsonResult Delete(MonAn monAn)
        {
            string query = @"Delete From ThucDon where MaThucDon =" + monAn.MaMonAn;
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
        [Route("savefile")]
        [HttpPost]
        public JsonResult savefile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postFile = httpRequest.Files[0];
                string filename = postFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                using (var stream = new FileStream(physicalPath,FileMode.Create))
                {
                    postFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch(Exception) {
                return new JsonResult("com.jpg");
            }
           
        }
        [Route("GetAllTenThucDon")]
        [HttpGet]
        public JsonResult GetAllTenThucDon()
        {
            string query = "Select TenThucDon from ThucDon";
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
    }
}
