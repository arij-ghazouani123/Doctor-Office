//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorOfficeDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visite
    {
        public int ID { get; set; }
        public string Consultation { get; set; }
        public string MedsList { get; set; }
        public string ToDoNext { get; set; }
        public int VisitDateDay { get; set; }
        public int IDPat { get; set; }
        public int IDDoct { get; set; }
        public int VisitDateMonth { get; set; }
        public int VisitDateYear { get; set; }
        public string VisitTime { get; set; }
    }
}