﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.DAL.Entities;
using ECommerceApp.PL.Models;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.DAL.Entities;
using ECommerceApp.PL.Models;



namespace ECommerceApp.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(
            RoleManager<ApplicationRole> roleManager,
            ILogger<RolesController> logger,
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {


                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }

            return View(role);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {

            if (id is null)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole appRole)
        {
            if (id != appRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);

                    role.Name = appRole.Name;
                    role.NormalizedName = appRole.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                }
            }

            return View(appRole);
        }

        public async Task<IActionResult> Delete(string id, ApplicationRole appRole)
        {
            if (id != appRole.Id)
                return NotFound();

            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                //ViewBag.Errors = result.Errors;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return BadRequest();


            ViewBag.RoleId = roleId;


            var usersInRole = new List<UserInRoleViewModel>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserName = user.UserName,
                    UserId = user.Id
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> users, string roleId)
        {


            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if (appUser != null)
                    {


                        if (user.IsSelected && !(await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && (await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);

                    }
                }

                return RedirectToAction(nameof(Update), new { id = roleId });

            }
            return View(users);
        }



    }
}

