using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagementSystem.Models
{
    public class EventBL
    {
        EventMS1Entities entity;
        public EventBL()
        {
            entity = new EventMS1Entities();
        }
        public void EventUpdate()
        {
            entity.Events.Where(w => w.Event_start < DateTime.Today).ToList<Event>().ForEach(x => x.Flag = 0);
            entity.SaveChanges();
        }
        public List<Event> ReturnPastEvents()
        {
            EventBL eventObject = new EventBL();
            eventObject.EventUpdate();
            return entity.Events.Where(c => c.Flag == 0).ToList<Event>();
        }
        public List<Event> ReturnUpcomingEvents()
        {
            EventBL eventObject = new EventBL();
            eventObject.EventUpdate();
            return entity.Events.Where(c => c.Flag == 1).ToList<Event>();
        }
    }
}