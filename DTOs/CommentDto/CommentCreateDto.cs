﻿using System.ComponentModel.DataAnnotations;

namespace CarRentals.DTOs.CommentDto
{
    public class CommentCreateDto
    {

        public string UserId { get; set; }
        public string CarId { get; set; }
        [Required(ErrorMessage = "Comment text cannot be empty")]
        [MinLength(20, ErrorMessage = "The minimum length is 20.")]

        public string CommentText { get; set; }
    }
}
