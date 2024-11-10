﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserIdentity.Identity;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public AccountController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result=userManager.Create(user, model.Password);

                if (result.Succeeded)
                {

                    return RedirectToAction("Login");
                }
                else 
                {
                    foreach (var error in result.Errors) 
                    { 
                    ModelState.AddModelError("",error);
                    }
                }
                    
                }
             return View(model);
        }
    } }