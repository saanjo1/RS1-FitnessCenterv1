﻿using FitnessCenter.Data.Entities;

namespace FitnessCenter.Web.ViewModels
{
    public class EventsIndexViewModel
    {
        public List<Event> Events { get; set; }

        public virtual Event Event { get; set; }
    }
}
