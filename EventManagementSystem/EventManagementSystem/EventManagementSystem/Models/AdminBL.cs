using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace EventManagementSystem.Models
{
    public class AdminBL
    {
        public EventMS1Entities entity; //Object of the EntityFrameWork
        public AdminBL()
        {
            entity = new EventMS1Entities(); 
        }
        public bool AuthenticateUser(string EmailId,string password) // method to authenticate user
        {
            Admin admin = entity.Admins.Where(u => (u.Email == EmailId) && (u.Password == password)).SingleOrDefault();
            if (admin == null)
            {
                return false;
            }
                return true;
        }
        public bool CheckEmailId(string Email)
        {
            Admin details = entity.Admins.Where(u => u.Email == Email).SingleOrDefault();
            if (details != null)
                return true;
            else
                return false;
        }
        public string GetPassword(string value) //get EmailId from the user to send the password. 
        {
            return entity.Admins.Where(u => (u.Email == value)).Select(u => u.Password).FirstOrDefault();
        }
    }
}