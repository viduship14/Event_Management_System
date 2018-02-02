using EventManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        EventMS1Entities db = new EventMS1Entities();
        public ActionResult Index()
        {
            try
            {
                /*Image List*/
                Image[] ar = db.Images.ToArray();
                /**/
                EventBL EBl = new EventBL();


                List<string> listCarousel = new List<string>();
                foreach (Image i in ar)
                {
                    if (i.Event.E_name.Equals("Carousel"))
                        listCarousel.Add(i.Path);
                }
                ViewBag.Carousel = listCarousel;

                Event[] events = db.Events.ToArray();
                List<Event> pastEvents = new List<Event>();
                List<Event> upEvents = new List<Event>();

                for (int i = 0; i < events.Count(); i++)
                {
                    if (events[i].Flag == 0 && events[i].E_name.EndsWith("Carousel") == false)
                    {
                        pastEvents.Add(events[i]);
                    }
                    else if (events[i].E_name.EndsWith("Carousel") == false)

                        upEvents.Add(events[i]);

                }
                List<Event> SortUpEvents = upEvents.OrderBy(o => o.Event_start).Reverse().ToList();
                List<Event> SortPastEvents = pastEvents.OrderBy(o => o.Event_start).Reverse().ToList();
                ViewBag.UpEvents = SortUpEvents;
                ViewBag.PastEvents = SortPastEvents;

                List<string> UpEvImg = new List<string>();

                foreach (Event e in SortUpEvents)
                {
                    foreach (Image i1 in ar)
                    {
                        if (i1.E_id == e.E_id)
                        {
                            UpEvImg.Add(i1.Path);
                            break;
                        }
                    }
                }
                ViewBag.UpEvImg = UpEvImg;
                return View(ViewBag);
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}