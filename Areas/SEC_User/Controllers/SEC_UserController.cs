using Microsoft.AspNetCore.Mvc;
using System.Data;
using Inventory_management_system.DAL.SCE_Users;
using Inventory_management_system.Areas.SEC_User.Models;

namespace Inventory_management_system.Areas.SEC_User.Controllers
{
    [Area("SEC_User")]
    [Route("[controller]/[action]")]
    public class SEC_UserController : Controller
    {
        public IConfiguration Configuration;
        public SEC_UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(SEC_UserModel userModel)
        {
            string error = null;

            if (userModel.UserName == null)
            {
                error += "User Name is required";
            }
            if (userModel.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Login");
            }
            else
            {
                SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
                DataTable dt = sEC_UserDAL.dbo_PR_SEC_User_SelectByUserNamePassword(userModel.UserName, userModel.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine(dr);
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("Login");
                }
                 if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }
         
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Register(SEC_UserModel sEC_UserModel)
        {
            string error = null;
            if (sEC_UserModel.UserName == null)
            {
                error += "User Name is Required!";
            }
            if (sEC_UserModel.Password == null)
            {
                error += "<br/> Password is Required!";
            }
            if (sEC_UserModel.FirstName == null)
            {
                error += "<br/> First  Name is Required!";
            }
            if (sEC_UserModel.LastName == null)
            {
                error += "<br/>Last Name is Required!";
            }
            if (sEC_UserModel.MobileNo == null)
            {
                error += "<br/>MobileNo is Required!";
            }
            if (sEC_UserModel.Email == null)
            {
                error += "<br/>Email Address is Required!";
            }

            if (error != null)
            {
                TempData["Error"] = error;  
                return RedirectToAction("Registration");
            }
            else
            {
                SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
                bool IsSuccess = sEC_UserDAL.dbo_PR_SEC_User_Register(sEC_UserModel.UserName, sEC_UserModel.Password, sEC_UserModel.FirstName, sEC_UserModel.LastName, sEC_UserModel.Email, sEC_UserModel.MobileNo);
                if (IsSuccess)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Registration");
                }
            }
        }
    }
}