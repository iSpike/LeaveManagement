using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagement.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _requestRepo;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _allocationRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            ILeaveRequestRepository requestRepo,
            ILeaveTypeRepository leaveTypeRepo,
            ILeaveAllocationRepository allocationRepo,
            IMapper mapper,
            UserManager<Employee> userManager)
        {
            _requestRepo = requestRepo;
            _leaveTypeRepo = leaveTypeRepo;
            _allocationRepo = allocationRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest
        public ActionResult Index()
        {
            var leaveRequests = _mapper.Map<List<LeaveRequestVM>>(_requestRepo.FindAll());
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(r => r.Approved == true),
                PendingRequests = leaveRequests.Count(r => !r.Approved.HasValue),
                RejectedRequests = leaveRequests.Count(r => r.Approved == false),
                LeaveRequests = leaveRequests
            };

            return View(model);
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(int id)
        {
            var model = _mapper.Map<LeaveRequestVM>(_requestRepo.FindById(id));
            return View(model);
        }

        public ActionResult ApproveRequest(int id)
        {
            LeaveRequest model = null;
            try
            {
                model = _requestRepo.FindById(id);
                model.Approved = true;
                model.ApprovedById = _userManager.GetUserId(User);
                model.DateActioned = DateTime.Now;
                var allocation = _allocationRepo.GetLeaveAllocationsByEmployeeAndType(model.RequestingEmployee.Id, model.LeaveTypeId);
                allocation.NumberOfDays -= (int)(model.EndDate - model.StartDate).TotalDays;
                var isSuccess = _requestRepo.Update(model);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error req");
                    return View(model);
                }

                isSuccess = _allocationRepo.Update(allocation);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error alloc");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }
        }

        public ActionResult RejectRequest(int id)
        {
            LeaveRequest model = null;
            try
            {
                model = _requestRepo.FindById(id);
                model.Approved = false;
                model.ApprovedById = _userManager.GetUserId(User);
                model.DateActioned = DateTime.Now;
                var isSuccess = _requestRepo.Update(model);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }
        }

        // GET: LeaveRequest/Create
        public ActionResult Create()
        {
            var leaveTypes = _leaveTypeRepo.FindAll().Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name });
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypes
            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestVM model)
        {
            try
            {
                var leaveTypes = _leaveTypeRepo.FindAll().Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name });
                model.LeaveTypes = leaveTypes;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
                {
                    ModelState.AddModelError("", "Error");
                    return View(model);
                }

                var employee = _userManager.GetUserAsync(User).Result;
                var allocations = _allocationRepo.GetLeaveAllocationsByEmployeeAndType(employee.Id, model.LeaveTypeId);
                int daysRequested = (int)(model.EndDate.Date - model.StartDate.Date).TotalDays;
                if (daysRequested > allocations.NumberOfDays)
                {
                    ModelState.AddModelError("", "Error to mach days");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                var isSuccess = _requestRepo.Create(leaveRequest);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error create");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}