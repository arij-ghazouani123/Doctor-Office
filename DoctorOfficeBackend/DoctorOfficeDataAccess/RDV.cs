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
    
    public partial class RDV
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Tel { get; set; }
        public Nullable<int> PatID { get; set; }
        public int IDDoct { get; set; }
        public string VisitTime { get; set; }
        public string LastVisit { get; set; }
        public Nullable<bool> NewPat { get; set; }
        public Nullable<int> NbreVisits { get; set; }
        public int VisitDateDay { get; set; }
        public int VisitDateMonth { get; set; }
        public int VisitDateYear { get; set; }
    }
}
