﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingModels
{
    public interface IUserRegisteredEvent
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    public interface IProfileCreatedEvent
    {
        public string Id { get; set; }
    }
}
