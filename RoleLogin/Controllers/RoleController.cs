using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using RoleLogin.ViewModels;
using System.Threading.Tasks;

namespace RoleLogin.Controllers
{
    public class RoleController : Controller
    {
        SqlConnection sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        // GET: Role
        public ActionResult Index()
        {
            return View(GetDataRoles());
        }

        public async Task<ActionResult> GetDataRoles()
        {
            var result = await sqlconnection.QueryAsync<RoleVM>("EXEC SP_Retrieve_Role");
            return Json(new{ data = result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Create(RoleVM roleVM)
        {
            var affectedRows = await sqlconnection.ExecuteAsync("EXEC SP_Insert_Role @Name", new { Name = roleVM.Name});
            return Json(new { data = affectedRows }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Edit(RoleVM roleVM)
        {
            var affectedRows = await sqlconnection.ExecuteAsync("EXEC SP_Update_Role @Id, @Name", new { Name = roleVM.Name, Id = roleVM.Id });
            return Json(new { data = affectedRows }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await sqlconnection.QueryAsync<RoleVM>("EXEC SP_Delete_Role @Id", new { Id = id });
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}