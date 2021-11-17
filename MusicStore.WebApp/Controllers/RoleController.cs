using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Business.Interfaces;
using MusicStore.Data;
using MusicStore.WebApp.Models;

namespace MusicStore.WebApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
       
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Index() => View(_roleManager.Roles.ToList());
        
        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            //if role is not empty
            if (!string.IsNullOrEmpty(name))
            {
                //than create it and push to database
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult UserList()
        {
            var userModel = new UserViewModel()
            {
                Users = _userManager.Users.ToList()
            };
            return View(userModel);
        } 
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string userId)
        {
         
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if(user!=null)
            {
              
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View("Edit", model);
            }
 
            return NotFound();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, List<string> roles)
        {
          
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if(user!=null)
            {
            
                var userRoles = await _userManager.GetRolesAsync(user);
              
                var allRoles = _roleManager.Roles.ToList();
              
                var addedRoles = roles.Except(userRoles);
              
                var removedRoles = userRoles.Except(roles);
 
                await _userManager.AddToRolesAsync(user, addedRoles);
 
                await _userManager.RemoveFromRolesAsync(user, removedRoles);
 
                return RedirectToAction("UserList");
            }
 
            return NotFound();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname
                };
                return PartialView("UserDetails", userViewModel);
            }
            return NotFound();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser userDto)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userDto.Id);
            if (ModelState.IsValid)
            {
                user.Name = userDto.Name;
                user.Surname = userDto.Surname;
                await _userManager.UpdateAsync(user);
                return Redirect("/Role/UserList");
            }
            return View("UserDetails");
        }
        
        [Authorize(Roles = "Admin")]
        public  IActionResult RemoveUser(string id, string name)
        {
            var userViewModel = new UserViewModel()
            {
                Id = id,
                Name = name
            };
            return View("ConfirmDelete", userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveUser(string id)
        {
            await _userService.RemoveUser(id);
            return Redirect("/Role/UserList");
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var passwordModel = new ChangePasswordModel()
            {
                Id = user.Id
            };
            return PartialView(passwordModel);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeAndHashPass(ChangePasswordModel model)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            //hash user's password
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            var result = await _userManager.UpdateAsync(user); //and update it
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                await  ChangeAndHashPass(model);
                return Redirect("/Role/UserList");
            }
            return View();
        }
    }   
    }
