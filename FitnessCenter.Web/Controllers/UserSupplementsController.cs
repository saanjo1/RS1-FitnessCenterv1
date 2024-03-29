﻿using AutoMapper;
using FitnessCenter.Data;
using FitnessCenter.Data.Entities;
using FitnessCenter.Web.Resources;
using FitnessCenter.Web.Utilities.Constants;
using FitnessCenter.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace FitnessCenter.Web.Controllers
{
    public class UserSupplementsController : Controller
    {
        public DatabaseContext _databaseContext;
        public readonly IFlashMessage _flashMessage;
        public readonly IMapper _mapper;
        private readonly UserManager _userManager;

        public UserSupplementsController(IMapper mapper, IFlashMessage flashMessage, DatabaseContext databaseContext, UserManager userManager)
        {
            _mapper = mapper;
            _flashMessage = flashMessage;
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Manage(int id)
        {
            UserSupplementsManageViewModel viewModel;

            if (id == 0)
            {
                viewModel = new UserSupplementsManageViewModel
                {
                    UserId = _userManager.Get().Id,
                };
            }
            else
            {
                viewModel = _databaseContext.UserSupplements
                    .Where(us => us.Id == id)
                    .Select(s => _mapper.Map<UserSupplementsManageViewModel>(s)).Single();
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Manage(UserSupplementsManageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                UserSupplement userSupplement;
                if (viewModel.Id == 0)
                {
                    userSupplement = new UserSupplement();
                    _databaseContext.Add(userSupplement);
                }
                else
                {
                    userSupplement = _databaseContext.UserSupplements.Find(viewModel.Id);
                }

                _mapper.Map(viewModel, userSupplement);
                _databaseContext.SaveChanges();
            }
            catch
            {
                _flashMessage.Danger(Translations.UserSupplementFail);
            }
            return RedirectToAction("Index");
        }
    }
}
