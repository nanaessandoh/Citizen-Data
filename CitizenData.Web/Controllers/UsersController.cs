using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitizenData.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CitizenData.Web.Services;

namespace CitizenData.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUser _userService;
        private readonly IWebHostEnvironment _env;

        public UsersController(IWebHostEnvironment env, IUser userService)
        {
            _userService = userService;
            _env = env;
        }

        // GET: Users
        public IActionResult Index(string searchString)
        {
            var model = _userService.GetAll();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                model =  model.Where(asset => 
                                    asset.Address.Contains(searchString) ||
                                    asset.Age.Contains(searchString) ||
                                    asset.DOB.Contains(searchString) ||
                                    asset.Email.Contains(searchString) ||
                                    asset.GivenName.Contains(searchString) ||
                                    asset.Occupation.Contains(searchString) ||
                                    asset.Surname.Contains(searchString));
            }
            return View(model);
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,GivenName,Surname,Age,DOB,Email,Address,Occupation,FormFile")] User user)
        {
            if(user.FormFile != null)
            {
                // Check if the file is an image
                bool fileIsImage = _userService.IsImage($"{user.FormFile.FileName}");
                if (ModelState.IsValid && fileIsImage)
                {
                    // Create a random file name for the Profile Image
                    string randomFileName = Guid.NewGuid().ToString();
                    // Get the extension of the filename
                    string imageExtension = _userService.GetImageExtension($"{user.FormFile.FileName}");
                    // Save the User
                    user.ImageUrl = $"/images/{randomFileName}{imageExtension}";
                    _userService.AddUser(user);

                    // Copy the Profile Photo into the wwwroot/images folder
                    _userService.UploadProfileImage(randomFileName, imageExtension, user.FormFile);

                    // Alert that citizen has been created
                    TempData["Message"] = "Citizen Created Successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Message"] = "Accepted extensions are .jpeg .jpg .png .bmp ";
                return View(user);
            }
            else
            {
                TempData["Message"] = "Select a Display Photo ";
                return View(user);
            }

        }

        // GET: Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,GivenName,Surname,Age,DOB,Email,Address,Occupation,ImageUrl,FormFile")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                // Check if a new profile photo was selected
                if(user.FormFile != null)
                {
                    // Check if the file is an image
                    bool fileIsImage = _userService.IsImage($"{user.FormFile.FileName}");

                    if (fileIsImage) 
                    {
                        // Create a random file name for the New Profile Image
                        string randomFileName = Guid.NewGuid().ToString();
                        // Get the extension of the filename
                        string imageExtension = _userService.GetImageExtension($"{user.FormFile.FileName}");
                        // Update the User
                        string newImageUrl = $"/images/{randomFileName}{imageExtension}";
                        _userService.UpdateAndDeleteOldPhoto(user, newImageUrl);
                        // Copy the Profile Photo into the wwwroot/images folder
                        _userService.UploadProfileImage(randomFileName, imageExtension, user.FormFile);
                        // Alert that citizen has been Updated
                        TempData["Success"] = "Citizen Updated Successfully";
                        return RedirectToAction("Edit", new { Id = id });
                    }

                    // Display warning if the file format is not acceptable
                    TempData["Message"] = "Accepted extensions are .jpeg .jpg .png .bmp ";
                    return View(user);


                }

                if(user.FormFile == null)
                {
                    _userService.UpdateUser(user);
                }

                // Alert that citizen has been Updated
                TempData["Success"] = "Citizen Updated Successfully";
                return RedirectToAction("Edit", new { Id = id });
                //return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

  
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userService.DeleteProfilePhoto(id);
            _userService.DeleteUser(id);
    
            return RedirectToAction(nameof(Index));
        }

    }
}
