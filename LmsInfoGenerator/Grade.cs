//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LmsInfoGenerator
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grade
    {
        public int studentId { get; set; }
        public int assignmentId { get; set; }
        public string comments { get; set; }
        public string rubric { get; set; }
        public Nullable<int> value { get; set; }
        public string filePath { get; set; }
    
        public virtual Assignment Assignment { get; set; }
        public virtual Grade Grade1 { get; set; }
        public virtual Grade Grade2 { get; set; }
        public virtual Student Student { get; set; }
    }
}