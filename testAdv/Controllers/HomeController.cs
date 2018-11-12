using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using testAdv.Models;
using testAdv.ViewModels;

namespace testAdv.Controllers
{
    public class HomeController : Controller
    {
        ChatContext db = new ChatContext();

        public ActionResult Index()
        {
            var context = HttpContext.Request;

            var guid = HasAuthorize(context);

            if (guid == null)
                return Redirect("~/LogIn");

            var model = new IndexViewModel();
            model.Title = "Мега-Чат";
            
            return View(model);
        }

        [HttpGet]
        public ActionResult LogIn(bool valide = true)
        {
            var model = new LogInViewModels();
            model.Valide = valide;
            model.Title = " Авторизация Мега-Чат";

            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(string Name)
        {
            if (db.Users.Any(x => x.Name == Name))
                return Redirect("LogIn?valide=false");

            var user = new User()
            {
                Guid = Guid.NewGuid(),
                Name = Name
            };

            db.Users.Add(user);
            db.SaveChanges();

            var request = HttpContext.Request;
            setAuthorize(request, user);

            return Redirect("~/");
        }

        public ActionResult Error()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult GetItems()
        {
            var guid = HasAuthorize(HttpContext.Request);
            if(guid == null)
                return new JsonResult();

            var date = DateTime.Now.AddDays(-1);
            var items = db.Messages.Where(x => x.Date > date).ToList();

            return Json(items);
        }

        [HttpPost]
        public JsonResult GetUser()
        {
            var guid = HasAuthorize(HttpContext.Request);
            if (guid == null)
                return new JsonResult();

            var user = db.Users.Single(x => x.Guid == guid);

            return Json(user);
        }

        [HttpPost]
        public JsonResult SendMessage(string text)
        {
            var guid = HasAuthorize(HttpContext.Request);
            if (guid == null || string.IsNullOrEmpty(text))
                return new JsonResult();

            var user = db.Users.Single(x => x.Guid == guid);

            var message = new Message()
            {
                Author = user.Name,
                Date = DateTime.Now,
                Text = text
            };

            db.Messages.Add(message);
            db.SaveChanges();

            return Json("success");
        }

        #region Private
        private Guid? HasAuthorize(HttpRequestBase request)
        {
            var userCookies = request.Cookies["User"];

            if (string.IsNullOrEmpty(userCookies?.Value))
                return null;
            else
                return Guid.Parse(userCookies.Value);
        }

        private void setAuthorize(HttpRequestBase request, User user)
        {
            HttpContext.Response.Cookies.Add(new HttpCookie("User") { Value = user.Guid.ToString(), Domain = request.Url.Host.ToLower(), HttpOnly = true });
        }
        #endregion
    }
}