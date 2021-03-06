﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerformanceCalculator.DbContexts;
using PerformanceCalculator.Models;
using PerformanceCalculator.Services;
using PerformanceCalculator.Specifications;

namespace PerformanceCalculator.Pages.Exams
{
    public class DeleteModel : PageModel
    {
        private readonly IDbService<Exam> _service;

        public DeleteModel(IDbService<Exam> service)
        {
            _service = service;
        }

        [BindProperty] public Exam Exam { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var spec = new ExamWithCourseSpecification(id);
            Exam = await _service.GetModelWithSpec(spec);

            if (Exam == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var spec = new ExamWithCourseSpecification(id);
            Exam = await _service.GetModelWithSpec(spec);

            if (Exam != null)
            {
                await _service.DeleteAsync(Exam);
            }

            return RedirectToPage("./Index");
        }
    }
}