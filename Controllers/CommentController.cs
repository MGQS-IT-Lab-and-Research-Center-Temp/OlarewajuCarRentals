﻿using AspNetCoreHero.ToastNotification.Abstractions;
using CarRentals.Models.Comment;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly INotyfService _notyf;

        public CommentController(ICommentService commentService, INotyfService notyf)
        {
            _commentService = commentService;
            _notyf = notyf;
        }

        //public IActionResult Index()
        //{
        //    var response = _commentService.GetAllComment();
        //    ViewData["Message"] = response.Message;
        //    ViewData["Status"] = response.Status;

        //    return View(response.Data);
        //}

        public IActionResult GetCommentDetail(string id)
        {
            var response = _commentService.GetComment(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Car");
            }

            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCommentViewModel request)
        {
            var response = _commentService.CreateComment(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Update(string id)
        {
            var response = _commentService.GetComment(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateCommentViewModel request)
        {
            var response = _commentService.UpdateComment(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteComment([FromRoute] string id)
        {
            var response = _commentService.DeleteComment(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }
    }
}
