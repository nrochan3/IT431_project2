namespace AdmissionsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Social Security Number")]
        [Required(ErrorMessage = "The social security number is required")]
        [StringLength(15)]
        [RegularExpression(@"^(?!000|666)[0-8][0-9]{2}-(?!00)[0-9]{2}-(?!0000)[0-9]{4}$",
            ErrorMessage = "Please enter a valid Social Security number in the format XXX-XX-XXXX. " +
            "The first three digits cannot be 000, 666, or between 900 and 999. " +
            "Digits four and five must be from 01 to 99. " +
            "The last four digits must be from 0001 to 9999.")]
        public string SSN { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "The first name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(15)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The last name is required")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$",
            ErrorMessage = "Please enter a valid Email address")]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Home Phone")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$",
            ErrorMessage = "Please enter a valid phone number in the format XXX-XXX-XXXX.")]
        [StringLength(12)]
        public string HomePhone { get; set; }

        [Display(Name = "Cell Phone")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$",
            ErrorMessage = "Please enter a valid phone number in the format XXX-XXX-XXXX.")]
        [StringLength(12)]
        public string CellPhone { get; set; }

        [Display(Name = "Street Address")]
        [StringLength(50)]
        public string StreetAddress { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(30)]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "The date of birth is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        public int? GenderId { get; set; }

        [Display(Name = "High School Name")]
        [StringLength(50)]
        public string HSName { get; set; }

        [Display(Name = "High School City")]
        [StringLength(50)]
        public string HSCity { get; set; }

        [Display(Name = "Graduation Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Current GPA")]
        [Required(ErrorMessage = "The current GPA is required")]
        [RegularExpression(@"[0]\.[0]|([0-3]\.\d?)|[4].[0]",
            ErrorMessage = "Please enter a GPA in the format x.x and must be between 1.0 and 4.0")]
        [DisplayFormat(DataFormatString = "{0:n1}", ApplyFormatInEditMode = true)]
        public decimal CurrentGPA { get; set; }

        [Display(Name = "Math SAT Score")]
        [Required(ErrorMessage = "The Math SAT Score is required")]
        [Range(0, 800, ErrorMessage = "Please enter a Math SAT Score between 0 and 800")]
        public int MathSATScore { get; set; }

        [Display(Name = "Verbal SAT Score")]
        [Required(ErrorMessage = "The Verbal SAT Score is required")]
        [Range(0, 800, ErrorMessage = "Please enter a Verbal SAT Score between 0 and 800")]
        public int VerbalSATScore { get; set; }

        [Display(Name = "Combined SAT Score")]
        public int? CombinedSATScore { get; set; }

        public int? MajorId { get; set; }

        public int? EnrollmentSemesterId { get; set; }

        [Display(Name = "Enrollment Year")]
        [StringLength(10)]
        public string EnrollmentYear { get; set; }

        [Display(Name = "Submission Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SubmissionDate { get; set; }

        public int? AdmissionDecisionId { get; set; }

        public virtual AdmissionDecision AdmissionDecision { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Major Major { get; set; }

        public virtual Semester Semester { get; set; }
    }
}
