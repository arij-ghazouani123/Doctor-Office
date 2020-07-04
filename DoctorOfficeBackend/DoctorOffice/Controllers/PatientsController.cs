using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoctorOfficeDataAccess;
namespace DoctorOffice.Controllers
{
    public class PatientsController : ApiController
    {
        [HttpPost]
        public void AddVisit([FromBody] Visite visite)
        {
            using( DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                entities.Visites.Add(visite);
                var entity = entities.Patients.FirstOrDefault(x => x.ID == visite.IDPat);
                entity.NbreVisits += 1;
                entity.LastVisit = visite.VisitDateDay+"/"+ visite.VisitDateMonth+"/"+ visite.VisitDateYear+" à "+ visite.VisitTime;
                entities.SaveChanges();
            }
        }
        [HttpPut]
        public void EditVisit([FromBody] Visite visite)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var entity = entities.Visites.FirstOrDefault(x => x.ID == visite.ID);
                entity.MedsList = visite.MedsList;
                entity.ToDoNext = visite.ToDoNext;
                entity.VisitDateDay = visite.VisitDateDay;
                entity.VisitDateMonth = visite.VisitDateMonth;
                entity.VisitDateYear = visite.VisitDateYear;
                entity.VisitTime = visite.VisitTime;
                entity.Consultation = visite.Consultation;
                entities.SaveChanges();
            }
        }
        [HttpGet]
        public IHttpActionResult LoadVisitByID (int ID)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                return Ok(entities.Visites.FirstOrDefault(x => x.ID == ID));
            }
        }
        [HttpGet]
        public IHttpActionResult LoadVisitsByPat(int ID)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var VisitsByPat = new List<Visite>();
                foreach(Visite visit in entities.Visites)
                {
                    if (visit.IDPat == ID)
                    {
                        VisitsByPat.Add(visit);
                    }
                }
                return Ok(VisitsByPat);
            }
        }
        [HttpGet]
        public IHttpActionResult LoadAllergiesByPat(int ID)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var allergies = new List<Allergy>();

                foreach (Allergy alg in entities.Allergies)
                {
                    if(alg.PatID == ID)
                    {
                        allergies.Add(alg);
                    }
                }
                return Ok(allergies);
            }
        }
        [HttpGet]
        public IHttpActionResult LoadMaladiesByPat(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var maladies = new List<Malady>();
                foreach (Malady mld in entities.Maladies)
                {
                    if (mld.PatID == ID)
                    {
                        maladies.Add(mld);
                    }
                }
                return Ok(maladies);
            }
        }
        [HttpPost]
        public void AddIllness([FromBody]Malady[] illnesses)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                foreach (Malady ill in illnesses)
                {
                    entities.Maladies.Add(ill);
                }
                entities.SaveChanges();

            }
        }
        [HttpPost]
        public void AddSingleIllness([FromBody]Malady illness)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                    entities.Maladies.Add(illness);
                    entities.SaveChanges();
            }
        }
        [HttpPost]
        public void AddAllergy([FromBody]Allergy[] allergies)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
              foreach(Allergy allergy in allergies)
                {
                    entities.Allergies.Add(allergy);
                   
                }
                entities.SaveChanges();
            }
        }
        [HttpPost]
        public void AddSingleAllergy([FromBody]Allergy allergy)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                    entities.Allergies.Add(allergy);
                    entities.SaveChanges();
            }
        }
        [HttpPost]
        public IHttpActionResult AddPatient([FromBody]Patient patient)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                /*  string datee = patient.BirthDate;
                  string datetimee = "31/01/2020 12:45";
                  CultureInfo info = new CultureInfo("fr-FR");
                  DateTime dd = Convert.ToDateTime(datee, info);
                  DateTime dt = Convert.ToDateTime(datetimee, info);
                  patient.LastVisit = dt;
                  patient.BirthDate = dd;*/
                var Pat = entities.Patients.ToList();
                entities.Patients.Add(patient);
                entities.SaveChanges();
              
                return Ok(patient.ID);
            }
        }
        [HttpGet]
        public IHttpActionResult LoadPatients(int ID)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                return Ok(entities.Patients.Where(x => x.IDDoct == ID).ToList());
            }

        }
        [HttpGet]
        public IHttpActionResult LoadRdvsByDoct(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                return Ok(entities.RDVs.ToList().Where(x => x.IDDoct == ID));
            }
        }

        [HttpGet]
        public IHttpActionResult LoadPatientById(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var allergies = entities.Allergies.Where(x => x.PatID == ID).ToList();
                var maladies = entities.Maladies.Where(x => x.PatID == ID).ToList();
                return Ok(new { 
                    PatDetails = entities.Patients.FirstOrDefault(p => p.ID == ID),
                    Allergies = allergies,
                    Maladies = maladies
                });
            }

        }

        [HttpGet]
        public IHttpActionResult LoadPatDetails(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                return Ok(entities.Patients.FirstOrDefault(p => p.ID == ID));
            }
        }
        [HttpPut]
        public void EditPat([FromBody]Patient patient)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var entity = entities.Patients.FirstOrDefault(x => x.ID == patient.ID);
                entity.Adress = patient.Adress;
                entity.Age = patient.Age;
                entity.CIN = patient.CIN;
                entity.FirstName = patient.FirstName;
                entity.LastName = patient.LastName;
                entity.LastVisit = patient.LastVisit;
                entity.PhoneNumber = patient.PhoneNumber;
                entity.Gender = patient.Gender;
                entities.SaveChanges();
            }
        }
        [HttpPost]
        public IHttpActionResult AddRDV(RDV rdv)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                if (rdv.PatID != null)
                {
                    var entity = entities.Patients.FirstOrDefault(x => x.ID == rdv.PatID);
                    rdv.LastVisit = entity.LastVisit;
                    rdv.NbreVisits = entity.NbreVisits;
                    rdv.NewPat = false;
                } else
                {
                    rdv.NewPat = true;
                }
                foreach( RDV rend in entities.RDVs)
                {
                    if (rend.VisitDateDay == rdv.VisitDateDay && rend.VisitDateMonth == rdv.VisitDateMonth && rend.VisitDateYear == rdv.VisitDateYear && rend.VisitTime == rdv.VisitTime)
                    {
                        return Ok(false);
                    }
                }
                entities.RDVs.Add(rdv);
                entities.SaveChanges();
                return Ok(true);
            }
        }
        
        [HttpDelete]
        public void DelPatient(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                entities.Patients.Remove(entities.Patients.FirstOrDefault(x => x.ID == ID));
                entities.SaveChanges();
            }
        }
        [HttpDelete]
        public void DelRdv(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                entities.RDVs.Remove(entities.RDVs.FirstOrDefault(x => x.ID == ID));
                entities.SaveChanges();
            }
        }
        [HttpDelete]
        public void DelVisit(int ID, int PatID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var VisitsList = new List<Visite>();
                entities.Visites.Remove(entities.Visites.FirstOrDefault(x => x.ID == ID));
                entities.SaveChanges();
                VisitsList = entities.Visites.Where(x => x.IDPat == PatID).ToList();
                var patient = entities.Patients.FirstOrDefault(x => x.ID == PatID);
                if (VisitsList.Count != 0)
                {
                patient.LastVisit = VisitsList.ElementAt(VisitsList.Count-1).VisitDateDay+"/"+ VisitsList.ElementAt(VisitsList.Count - 1).VisitDateMonth+"/"+ VisitsList.ElementAt(VisitsList.Count - 1).VisitDateYear+" à "+ VisitsList.ElementAt(VisitsList.Count - 1).VisitTime;
                } else
                {
                    patient.LastVisit = null;
                }
                entities.SaveChanges();
            }
        }
        [HttpDelete]
        public void DelAllergy(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                entities.Allergies.Remove(entities.Allergies.FirstOrDefault(x => x.ID == ID));
                entities.SaveChanges();
            }
        }
        [HttpDelete]
        public void DelIll(int ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                entities.Maladies.Remove(entities.Maladies.FirstOrDefault(x => x.ID == ID));
                entities.SaveChanges();
            }
        }
    }
}
