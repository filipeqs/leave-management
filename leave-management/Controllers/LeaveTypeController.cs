using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using leave_management.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypes
        public IActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes);

            return View(model);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: LeaveTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;

                var isSucess = _repo.Create(leaveType);
                if (!isSucess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Edit/5
        public IActionResult Edit(int id)
        {
            if (!_repo.Exists(id))
                return NotFound();

            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);

            return View(model);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var leaveType = _mapper.Map<LeaveType>(model);

                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrond...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }
    }
}
