using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MyStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private static readonly List<User> _users = new List<User>
        {
            new User
            {
                Name = "user 1"
            },
            new User
            {
                Name = "user 2"
            },
            new User
            {
                Name = "user 3"
            }
        };

        public IActionResult Get()
            => Ok(_users);
    }

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}