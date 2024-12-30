﻿using EventApp.Application.Common.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Services
{
    public class HashPassword : IHashPassword
    {
        private readonly int _workFactor;

        public HashPassword(int workFactor = 10)
        {
            _workFactor = workFactor;
        }

        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.");

            return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(providedPassword) || string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Passwords cannot be null or empty.");

            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }

}