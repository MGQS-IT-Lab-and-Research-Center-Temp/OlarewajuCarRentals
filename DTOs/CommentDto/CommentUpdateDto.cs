﻿using System.ComponentModel.DataAnnotations;

namespace CarRentals.DTOs.CommentDto
{
    public class CommentUpdateDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Comment text cannot be empty")]
        [MinLength(20, ErrorMessage = "The minimum lenghth is 3.")]
        public string CommentText { get; set; }
    }
}
