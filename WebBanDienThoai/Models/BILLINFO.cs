//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BILLINFO
    {
        public int PRODUCTID { get; set; }
        public int BILLID { get; set; }
        public Nullable<int> STOCK { get; set; }
        public Nullable<int> PRICE { get; set; }
    
        public virtual BILL BILL { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
