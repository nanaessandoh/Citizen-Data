﻿using System;
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
        //private readonly CitizenDataDBContext _context;
        private readonly IWebHostEnvironment _env;

        public UsersController(IWebHostEnvironment env, IUser userService)
        {
            _userService = userService;
            _env = env;
        }

        // GET: Users
        public IActionResult Index()
        {
            var model = _userService.GetAll();
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
            // Check if the file is an image
            bool fileIsImage = _userService.IsImage($"{user.FormFile.FileName}");
            if (ModelState.IsValid && fileIsImage )
            {
                // Create a random file name for the P
                string randomFileName = Guid.NewGuid().ToString();
                // Get the extension of the filename

                // Save the User
                user.ImageUrl = $"/images/{user.FormFile.FileName}";
                _userService.AddUser(user);

                // Copy the Profile Photo into the wwwroot/images folder
                string filePath = $"{_env.WebRootPath}\\images\\{user.FormFile.FileName}";
                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    user.FormFile.CopyTo(stream);
                    stream.Flush();
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = $"Select an image file with the correct extension i.e. .jpeg, .jpg, .png, .bmp ";
            return View(user);
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
        public IActionResult Edit(int id, [Bind("Id,GivenName,Surname,Age,DOB,Email,Address,Occupation,ImageUrl")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateUser(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userService.UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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
            _userService.DeleteUser(id);
    
            return RedirectToAction(nameof(Index));
        }

    }
}
