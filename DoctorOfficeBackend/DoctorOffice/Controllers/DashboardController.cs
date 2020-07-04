using DoctorOfficeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoctorOffice.Controllers
{
    public class DashboardController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LoadTodayAppointements (int IDDoct, int Day, int Month, int Year)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
               
                var RdvList = entities.RDVs.ToList().Where(x => x.IDDoct == IDDoct && x.VisitDateDay == Day && x.VisitDateMonth == Month && x.VisitDateYear == Year);
                int NbreVisits = RdvList.Count();
                return Ok(new{RdvList, NbreVisits});
            }
        }
        [HttpGet]
        public IHttpActionResult LoadChart(int? ID)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var NbreVisitsMonth = 0;
                var ChartArray = new List<int>();
                var DoctVist = new List<Visite>();
                foreach (Visite Vs in entities.Visites)
                {
                    if (Vs.IDDoct == ID && Vs.VisitDateYear == DateTime.Now.Year)
                        DoctVist.Add(Vs);
                }
                int month = 1;
                while (month < 13)
                {
                    int i = 0;
                    foreach (Visite vs in DoctVist)
                    {
                        if (vs.VisitDateMonth == month)
                        {
                            i++;
                            NbreVisitsMonth++;

                        }
                    }
                    ChartArray.Add(i);
                    month++;
                }
                return Ok(new { ChartArray, NbreVisitsMonth });
            }
        }
        [HttpGet]
        public IHttpActionResult LoadAgeChart(int IDDoct)
        {
            using(DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var FirstCat = 0;
                var SecondCat = 0;
                var ThirdCat = 0;
                var FourthCat = 0;
                var FifthCat = 0;
                var DoctorsClients = new List<Patient>();
                DoctorsClients = entities.Patients.Where(x => x.IDDoct == IDDoct).ToList();
                foreach(Patient pat in DoctorsClients)
                {if (pat.Age <= 10)
                    {
                        FirstCat++;
                    } else if (pat.Age > 10 && pat.Age <= 20)
                    {
                        SecondCat++;
                    } else if (pat.Age > 20 && pat.Age <= 40)
                    {
                        ThirdCat++;
                    } else if (pat.Age > 40 && pat.Age <= 70)
                    {
                        FourthCat++;
                    }else {
                        FifthCat++;
                    }

                }
                return Ok(new { FirstCat, SecondCat, ThirdCat, FourthCat, FifthCat });
            }
        }
        [HttpGet]
        public IHttpActionResult LoadVisitsByWeek (int IDDoct, int Day, int Month, int Year)
        {
            using (DoctorOfficeEntities entities = new DoctorOfficeEntities())
            {
                var DoctorVisit = new List<Visite>();
                int NbreVisits = 0;
                foreach (Visite visit in entities.Visites)
                {
                    if (visit.IDDoct == IDDoct)
                    { DoctorVisit.Add(visit); }
                    if (visit.IDDoct == IDDoct && visit.VisitDateMonth == Month)
                    { NbreVisits++; }

                }
                var Visits = new List<int>(); 
                for (int i = 0; i< 7; i++)
                {
                    int NbrVisits = 0;

                        foreach (Visite visit in DoctorVisit)
                    {
                        if ( visit.VisitDateDay == Day && visit.VisitDateMonth == Month && visit.VisitDateYear == Year)
                        { NbrVisits++;
                        }
                    }
                    if (Day == 1 && Month == 1)
                    {
                        Month = 12;
                        Year--;
                        Day = DateTime.DaysInMonth(Year, Month);
                        
                    }
                    else if (Day == 1 && Month != 1)
                    {
                        Month--;
                        Day = DateTime.DaysInMonth(Year, Month);
                    }
                    else if (Day > 1 && Month > 1)
                    {
                        Day--;
                    }
                    Visits.Add(NbrVisits);

                }
                return Ok(new { Visits, NbreVisits});
            }
        }
    }
}
