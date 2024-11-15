﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserIdentity.Identity;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleAdminController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));

        }


        // GET: RoleAdmin
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name)
        {
            if (ModelState.IsValid)
            {
                var result = roleManager.Create(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }

                }

            }
            return View(name);

        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var role = roleManager.FindById(id);
            if (role != null)
            {
                var result = roleManager.Delete(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }

            }
            else
            {
                return View("Error", new string[] { "Role Bulunamadı" });
            }

        }
        [HttpPost]
        public ActionResult Edit(string id) 
        { 
        var role = roleManager.FindByName(id);
            var memebers=new List<ApplicationUser>();
            var nonmembers=new List<ApplicationUser>();
            foreach (var user in userManager.Users.ToList())
            {
            var list =userManager.IsInRole(user.Id,role.Name)?
                    memebers:nonmembers;

                list.Add(user);
            }
            return View(new RoleEditModel() 
            {
            Role = role,
            Members = memebers,
            NonMember = nonmembers
            });
        
        }
        [HttpGet]
        public ActionResult Edit(RoleUpdateModel model)  
        {
            IdentityResult result;
            if (ModelState.IsValid) 
            {
                foreach (var userId in (model.IdsToAdd != null ? new[] { model.IdsToAdd } : Array.Empty<string>()))
                {
                    result = userManager.AddToRole(userId, model.RoleName);
                    if (result.Succeeded) {
                    
                    return View("Error",result.Errors);
                    }
                }
                foreach (var userId in (model.IdsToDelete != null ? new[] { model.IdsToDelete } : Array.Empty<string>())) 
                {
                result=userManager.RemoveFromRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                
                }
                return RedirectToAction("Index");
            }
            return View("Error",new string[] {"Aranılan Role Yok "});
        
        }
    }
}