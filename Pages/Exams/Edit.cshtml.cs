﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerformanceCalculator.DbContexts;
using PerformanceCalculator.Models;

namespace PerformanceCalculator.Pages.Exams
{
    public class EditModel : PageModel
    {
        private readonly PerformanceCalculator.DbContexts.ApplicationDbContext _context;

        public EditModel(PerformanceCalculator.DbContexts.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Exam Exam { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exam = await _context.Exams
                .Include(e => e.Course).FirstOrDefaultAsync(m => m.Id == id);

            if (Exam == null)
            {
                return NotFound();
            }
           ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(Exam.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExamExists(Guid id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
