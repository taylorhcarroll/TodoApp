using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Models.ViewModels
{
    public class TodoItemFormViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int TodoStatusId { get; set; }

        public List<SelectListItem> TodoStatusOptions { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


    }
}