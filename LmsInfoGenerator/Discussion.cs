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
    
    public partial class Discussion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discussion()
        {
            this.Replies = new HashSet<Reply>();
        }
    
        public int discussionId { get; set; }
        public Nullable<int> sectionId { get; set; }
        public Nullable<System.DateTime> openDate { get; set; }
        public Nullable<System.DateTime> closeDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
