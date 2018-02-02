using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;


namespace EventManagementSystem.Controllers
{
    public class EventsController : Controller
    {

        EventMS1Entities db = new EventMS1Entities();
    
    public ActionResult Index()                                          //this method will display the records on the View Page
    {
        if (Session["email"] != null)
           return View(db.Events.ToList());                                //returning data in Events table to the View 
        else
           return RedirectToAction("Index", "Login");
    }

    public ActionResult Details(int? id)                                 //E_id is passed from View for showing record on details view page
    {
            if (Session["email"] != null)
            {
                Event e = db.Events.Find(id);                                    //getting the record into event object matching with the id
                return View(e);
            }
            else
                return RedirectToAction("Index", "Login");//returning the record on details view page
        }


    public ActionResult Create()                                        //this method will return the Create view page on clicking the Create Link 
    {
        if (Session["email"] != null)
                return View();
            else
                return RedirectToAction("Index", "Login");
        }


    [HttpPost]
    public ActionResult Create(Event e)                                 //after submitting the form this method will be called
    {
            if (Session["email"] != null)
            {
                if (e.E_name == null || e.Event_end == null || e.Event_start == null || e.Specification == null || e.Venue == null) //checking for if there is any null value 
                {
                   
                    return View(e);                                            //returning the same view after any null value is found 
                }
                
                else
                {
                    if (ModelState.IsValid)
                    {
                        
                            e.Flag = 1;
                            db.Events.Add(e);                                    //adding the record entered by user passed by form in Event e object               
                            db.SaveChanges();                                    //saving the changes in table
                            return RedirectToAction("Index", "Events");          //redirecting to Index Action after successfully adding new record to database and will display new record in Index View Page 
                        
                    }
                }
                return View(e);
            }
            else
                return RedirectToAction("Index", "Login");
        }


    public ActionResult Edit(int? id)                               //this method will recieve the E_id on clicking the Edit Link
    {
            if (Session["email"] != null)
            {
                Event e = db.Events.Find(id);                               //will find the record with matched id from record 
                return View(e);                                             //this will display the data found into the fields of Edit View Page so that user can edit and know which data is to be updated
            }
            else
                return RedirectToAction("Index", "Login");
        }


    [HttpPost]
    public ActionResult Edit(Event e)                              //this method will be called once the user submit the form in Edit View Page with updated values
    {
            if (Session["email"] != null)
            {
                if (e.E_name == null || e.Event_end == null || e.Event_start == null || e.Specification == null || e.Venue == null) //checking for if there is any null value 
                {
                    return View(e);                                       // will prevent user to save changes to database if there is any null record and redirect to same page 
                }
                else
                {

                    if (ModelState.IsValid)                          // check if there is amy model state errors 
                    {
                        e.Flag = 1;
                        db.Entry(e).State = EntityState.Modified;    //modifying the record which user entered 
                        db.SaveChanges();                            //saving the changes to database
                        return RedirectToAction("Index");            //redirecting to index page after successfull updation of records
                    }
                }
                return View(e);
            }
            else
                return RedirectToAction("Index", "Login");
        }


    public ActionResult Delete(int? id)                      //this method will be called after clicking the delete link in index view and recieve the E_id passed from action link
    {
            if (Session["email"] != null)
            {
                Event e = db.Events.Find(id);                        //matched record into e obejct              

                return View(e);                                      //this will return matched record to Delete View Page
            }
            else
                return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult Delete(int id)                           //this method will be called after user click the delete button in delete view page
        {
            if (Session["email"] != null)
            {
                Event e = db.Events.Find(id);                            //object e stores the matched record of id
                db.Events.Remove(e);                                     //deleting the record 
                db.SaveChanges();                                        //saving the changes to database
                return RedirectToAction("Index");                        //redirecting to Index page after successfull deletion of record
            }
            else
                return RedirectToAction("Index", "Login");
        }

       

        public ActionResult LogOut()
        {
            Session["email"] = null;
            //Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
}
}