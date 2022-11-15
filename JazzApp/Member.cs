using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for a member
    /// <para></para>
    /// </summary>
    public class MemberData
    {
        /// <summary>First name of the member</summary>
        private string m_name = @"";
        /// <summary>First name of the member</summary>
        public string Name { get { return m_name; } set { m_name = value; } }

        /// <summary>Family name of the member</summary>
        private string m_family_name = @"";
        /// <summary>Family name of the member</summary>
        public string FamilyName { get { return m_family_name; } set { m_family_name = value; } }

        /// <summary>Jazz club E-Mail address to the member</summary>
        private string m_email_address = @"";
        /// <summary>Jazz club E-Mail address to the member</summary>
        public string EmailAddress { get { return m_email_address; } set { m_email_address = value; } }

        /// <summary>Private E-Mail address to the member</summary>
        private string m_private_email_address = @"";
        /// <summary>Private E-Mail address to the member</summary>
        public string PrivateEmailAddress { get { return m_private_email_address; } set { m_private_email_address = value; } }

        /// <summary>Telephone to the member</summary>
        private string m_telephone = @"";
        /// <summary>Telephone to the member</summary>
        public string Telephone { get { return m_telephone; } set { m_telephone = value; } }

        /// <summary>Telephone fix to the member</summary>
        private string m_telephone_fix = @"";
        /// <summary>Telephone fix to the member</summary>
        public string TelephoneFix { get { return m_telephone_fix; } set { m_telephone_fix = value; } }

        /// <summary>Member street address</summary>
        private string m_street = @"";
        /// <summary>Member street address</summary>
        public string Street { get { return m_street; } set { m_street = value; } }

        /// <summary>Member city address</summary>
        private string m_city = @"";
        /// <summary>Member city address</summary>
        public string City { get { return m_city; } set { m_city = value; } }

        /// <summary>Member post code </summary>
        private string m_post_code = @"";
        /// <summary>Member post code </summary>
        public string PostCode { get { return m_post_code; } set { m_post_code = value; } }

        /// <summary>URL to member mid size photo</summary>
        private string m_photo_mid_size = @"";
        /// <summary>URL to member mid size photo</summary>
        public string PhotoMidSize { get { return m_photo_mid_size; } set { m_photo_mid_size = value; } }

        /// <summary>URL to member small size photo</summary>
        private string m_photo_small_size = @"";
        /// <summary>URL to member small size photo</summary>
        public string PhotoSmallSize { get { return m_photo_small_size; } set { m_photo_small_size = value; } }

        /// <summary>Member tasks</summary>
        private string m_tasks = @"";
        /// <summary>Member tasks</summary>
        public string Tasks { get { return m_tasks; } set { m_tasks = value; } }

        /// <summary>Short description of member tasks</summary>
        private string m_tasks_short = @"";
        /// <summary>Short description of member tasks</summary>
        public string TasksShort { get { return m_tasks_short; } set { m_tasks_short = value; } }

        /// <summary>The reasons that the member is doing job for the jazz club</summary>
        private string m_why = @"";
        /// <summary>The reasons that the member is doing job for the jazz club</summary>
        public string Why { get { return m_why; } set { m_why = value; } }

        /// <summary>The year that the member started to do work for the jazz club</summary>
        private int m_start_year = -1245;
        /// <summary>The year that the member started to do work for the jazz club</summary>
        public int StartYear { get { return m_start_year; } set { m_start_year = value; } }

        /// <summary>The year that the member ended doing work for the jazz club</summary>
        private int m_end_year = -1245;
        /// <summary>The year that the member ended doing work for the jazz club</summary>
        public int EndYear { get { return m_end_year; } set { m_end_year = value; } }

        /// <summary>Login password for the user</summary>
        private string m_password = @"";
        /// <summary>Login password for the user</summary>
        public string Password { get { return m_password; } set { m_password = value; } }

        /// <summary>Flag telling the member is active in the jazz club</summary>
        private bool m_vorstand = false;
        /// <summary>Flag telling the member is active in the jazz club</summary>
        public bool Vorstand { get { return m_vorstand; } set { m_vorstand = value; } }

        /// <summary>List order number for (active) members. Also used as an identity e.g. contact person at a concert </summary>
        private int m_number = -1245;
        /// <summary>List order number for (active) members. Also used as an identity e.g. contact person at a concert </summary>
        public int Number { get { return m_number; } set { m_number = value; } }

    } // Member
} // namespace
