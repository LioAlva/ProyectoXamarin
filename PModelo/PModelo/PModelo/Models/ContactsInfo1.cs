using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Models
{
    public class ContactsInfo1
    {

        //private string firstName;
        private string middelName;
        private string lastname;
        private string contactNo;
        private string email;
        private string address;
        private DateTime? birthDate;
        private string groupName;


        public ContactsInfo1()
        {

        }

        private string firstName;

        [Display(ShortName = "First Name")]
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                this.firstName = value;
            }
        }


        [Display(ShortName = "Middle Name")]
        public string MiddleName
        {
            get { return this.middelName; }
            set
            {
                this.middelName = value;
            }
        }

        [Display(ShortName = "Last Name")]
        public string LastName
        {
            get { return this.lastname; }
            set
            {
                this.lastname = value;
            }
        }

        [Display(ShortName = "Contact Number")]
        public string ContactNumber
        {
            get { return contactNo; }
            set
            {
                this.contactNo = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
            }
        }

        [Display(ShortName = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
            }
        }

        [Display(ShortName = "Group Name")]
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
            }
        }
    }
}
