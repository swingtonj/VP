﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VP
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CP_Analytics_predictorEntities : DbContext
    {
        public CP_Analytics_predictorEntities()
            : base("name=CP_Analytics_predictorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_M_Business_Imperative> Tbl_M_Business_Imperative { get; set; }
        public virtual DbSet<Tbl_M_Industry> Tbl_M_Industry { get; set; }
        public virtual DbSet<Tbl_M_Mailer_Template> Tbl_M_Mailer_Template { get; set; }
        public virtual DbSet<Tbl_T_UserManagement> Tbl_T_UserManagement { get; set; }
        public virtual DbSet<Tbl_M_Analytics> Tbl_M_Analytics { get; set; }
        public virtual DbSet<Tbl_M_Parameter> Tbl_M_Parameter { get; set; }
        public virtual DbSet<Tbl_T_Specify> Tbl_T_Specify { get; set; }
        public virtual DbSet<Tbl_T_Specify_Value> Tbl_T_Specify_Value { get; set; }
    
        public virtual ObjectResult<Nullable<int>> SP_Registration(string organisation_name, string username, string passowrd, string email, string mobile)
        {
            var organisation_nameParameter = organisation_name != null ?
                new ObjectParameter("organisation_name", organisation_name) :
                new ObjectParameter("organisation_name", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passowrdParameter = passowrd != null ?
                new ObjectParameter("passowrd", passowrd) :
                new ObjectParameter("passowrd", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Registration", organisation_nameParameter, usernameParameter, passowrdParameter, emailParameter, mobileParameter);
        }
    
        public virtual ObjectResult<SP_Validate_Login_Result> SP_Validate_Login(string username, string passowrd)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passowrdParameter = passowrd != null ?
                new ObjectParameter("passowrd", passowrd) :
                new ObjectParameter("passowrd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Validate_Login_Result>("SP_Validate_Login", usernameParameter, passowrdParameter);
        }
    }
}
