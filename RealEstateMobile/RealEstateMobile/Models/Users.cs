﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateMobile.Models
{
    public partial class Users
    {
        private bool isTenant;
        public float Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
}
