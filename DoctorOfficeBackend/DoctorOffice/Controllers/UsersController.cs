using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoctorOfficeDataAccess;

namespace DoctorOffice.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Login(string login, string pwd)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                foreach(Doctor user in entities.Doctors)
                {
                    if (user.Email == login && user.Password == pwd)
                    {
                        return Ok(new { user, Type = "Doctor", UserExist = "Found"});
                    }
                    if (user.Email == login && user.Password != pwd)
                    {
                        return Ok(new {UserExist = "WrongPwd" });
                    }
                }
                foreach (Nurse user in entities.Nurses)
                {
                    if (user.Email == login && user.Password == pwd && user.IDDoct == null)
                    {
                        return Ok(new {user, UserExist = "FoundNoDoct"});
                    }
                    if (user.Email == login && user.Password == pwd && user.IDDoct != null)
                    {
                        return Ok(new { user, Type = "Nurse", UserExist = "Found" });
                    }
                    if (user.Email == login && user.Password != pwd)
                    {
                        return Ok(new { UserExist = "WrongPwd" });
                    }
                }
                return Ok(new { UserExist = "NotFound" });
            }
        }
        [HttpGet]
        public IHttpActionResult AvailableNurses(int ID)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var AvailNurses = new List<Nurse>();
                foreach(Nurse nr in entities.Nurses)
                {
                    if (nr.IDDoct == null)
                    AvailNurses.Add(nr);
                }
                return Ok(AvailNurses);
            }
        }
        [HttpGet]
        public IHttpActionResult InviteNurse(int IDNr, int IDDr, string JoinDate)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var nurse = entities.Nurses.FirstOrDefault(x => x.ID == IDNr && x.IDDoct == null);
                if (nurse != null)
                {
                    nurse.IDDoct = IDDr;
                    nurse.JoinDate = JoinDate;
                    entities.SaveChanges();
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
                
            }
        }
        [HttpPost]
        public IHttpActionResult DoctorSingUp(Doctor Dr)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                foreach(Doctor dr in entities.Doctors)
                {
                    if (dr.Email == Dr.Email)
                    {
                        return Ok("UserExists");
                    }
                }
                foreach (Nurse Nr in entities.Nurses)
                {
                    if (Nr.Email == Dr.Email)
                    {
                        return Ok("UserExists");
                    }
                }
                entities.Doctors.Add(Dr);
                entities.SaveChanges();
                return Ok();
            }
        }
        [HttpGet]
        public IHttpActionResult LoadMyNurses(int IDDoct)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                return Ok(entities.Nurses.ToList().Where(x => x.IDDoct == IDDoct));
            }
        }
        [HttpPut]
        public void RemoveNurse([FromBody]int ID)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                entities.Nurses.FirstOrDefault(x => x.ID == ID).IDDoct = null;
                entities.SaveChanges();
            }
        }
        [HttpPost]
        public IHttpActionResult NurseSingUp(Nurse Nr)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                foreach (Doctor dr in entities.Doctors)
                {
                    if (dr.Email == Nr.Email)
                    {
                        return Ok("UserExists");
                    }
                }
                foreach (Nurse nr in entities.Nurses)
                {
                    if (nr.Email == Nr.Email)
                    {
                        return Ok("UserExists");
                    }
                }
                entities.Nurses.Add(Nr);
                entities.SaveChanges();
                return Ok();
            }
        }
    }
}
