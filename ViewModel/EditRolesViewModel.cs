﻿namespace Psychiatrist_Management_System.ViewModel
{
    public class EditRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> UserRoles { get; set; } = new();
    }
}
