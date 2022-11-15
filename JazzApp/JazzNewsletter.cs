using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for one newsletter
    /// <para>This class has extracted member variables from the class EmlToXml.JazzEml.</para>
    /// <para></para>
    /// </summary>
    public class JazzNewsletter
    {
        #region Define, set and get member variables (as strings)

        /// <summary>Server path for the eml file</summary>
        private string m_path_server = @"";
        /// <summary>Get and set server path for the eml file</summary>
        public string EmlPathServer { get { return m_path_server; } set { m_path_server = value; } }

        /// <summary>Server name for the eml file</summary>
        private string m_file_server = @"";
        /// <summary>Get and set server name for the eml file</summary>
        public string EmlFileServer { get { return m_file_server; } set { m_file_server = value; } }

        /// <summary>Send year</summary>
        private string m_send_year_str = @"";
        /// <summary>Get and set send year</summary>
        public string SendYear { get { return m_send_year_str; } set { m_send_year_str = value; } }

        /// <summary>Send month</summary>
        private string m_send_month_str = @"";
        /// <summary>Get and set send month</summary>
        public string SendMonth { get { return m_send_month_str; } set { m_send_month_str = value; } }

        /// <summary>Send day</summary>
        private string m_send_day_str = @"";
        /// <summary>Get and set send day</summary>
        public string SendDay { get { return m_send_day_str; } set { m_send_day_str = value; } }

        /// <summary>Email subject</summary>
        private string m_subject = @"";
        /// <summary>Get and set email subject</summary>
        public string Subject { get { return m_subject; } set { m_subject = value; } }

        /// <summary>Email from</summary>
        private string m_from = @"";
        /// <summary>Get and set email from</summary>
        public string From { get { return m_from; } set { m_from = value; } }

        /// <summary>Email to</summary>
        private string m_to = @"";
        /// <summary>Get and set email to</summary>
        public string To { get { return m_to; } set { m_to = value; } }

        /// <summary>Message HTML</summary>
        private string m_message_html = @"";
        /// <summary>Get and set message HTML</summary>
        public string MsgHtml { get { return m_message_html; } set { m_message_html = value; } }

        /// <summary>Server attachments path</summary>
        private string m_attachment_image_path_server = @"";
        /// <summary>Get and set the server attachments path</summary>
        public string AttachmentImagePathServer { get { return m_attachment_image_path_server; } set { m_attachment_image_path_server = value; } }

        /// <summary>Server attachment image name</summary>
        private string m_attachment_image_server = @"";
        /// <summary>Get and set the server attachment image name</summary>
        public string AttachmentImageServer { get { return m_attachment_image_server; } set { m_attachment_image_server = value; } }

        /// <summary>Embedded poster flag</summary>
        private string m_b_embedded_poster_str = "FALSE";
        /// <summary>Get and set embedded poster flag</summary>
        public string EmbeddedPosterFlag { get { return m_b_embedded_poster_str; } set { m_b_embedded_poster_str = value; } }

        /// <summary>Server attachments path</summary>
        private string m_attachment_path_server = @"";
        /// <summary>Get and set the server attachments path</summary>
        public string AttachmentPathServer { get { return m_attachment_path_server; } set { m_attachment_path_server = value; } }

        /// <summary>Server attachment file name</summary>
        private string m_attachment_file_server = @"";
        /// <summary>Get and set the server attachment file name</summary>
        public string AttachmentFileServer { get { return m_attachment_file_server; } set { m_attachment_file_server = value; } }

        #endregion // Define, set and get member variables (as strings)

        #region Get and set some member variables as booleans and integers

        /// <summary>Get and set send year as integer</summary>
        public int SendYearInt { get { return JazzUtils.StringToInt(m_send_year_str); } set { m_send_year_str = value.ToString(); } }

        /// <summary>Get and set send month as integer</summary>
        public int SendMonthInt { get { return JazzUtils.StringToInt(m_send_month_str); } set { m_send_month_str = value.ToString(); } }

        /// <summary>Get and set send day as integer</summary>
        public int SendDayInt { get { return JazzUtils.StringToInt(m_send_day_str); } set { m_send_day_str = value.ToString(); } }

        /// <summary>Get and set the evaluation flag as boolean</summary>
        public bool EmbeddedPosterFlagBoolean {
            get
            {
                if (EmbeddedPosterFlag.Equals("FALSE"))
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    m_b_embedded_poster_str = "TRUE";
                else
                    m_b_embedded_poster_str = "FALSE";
            }
        } // EmbeddedPosterFlagBoolean
		
        #endregion // Get and set some member variables as booleans
		
		#region Set to empty strings if member parameter values not yet are set

        /// <summary>Returns empty string if value not yet is set</summary>
        private string EmptyStringIfValueNotYetSet(string i_value)
        {
            string ret_string = i_value;

            if (!JazzXml.XmlNodeValueIsSet(i_value))
            {
                ret_string = @"";
            }

            return ret_string;

        } // EmptyStringIfValueNotYetSet
		
        /// <summary>Set to empty strings for some member parameter if values not yet are set</summary>
        public void SetToEmptyStringsForValuesNotYetSet()
        {
            EmlPathServer = EmptyStringIfValueNotYetSet(EmlPathServer);

            EmlFileServer = EmptyStringIfValueNotYetSet(EmlFileServer);

            SendYear = EmptyStringIfValueNotYetSet(SendYear);

            SendMonth = EmptyStringIfValueNotYetSet(SendMonth);

            SendDay = EmptyStringIfValueNotYetSet(SendDay);

            Subject = EmptyStringIfValueNotYetSet(Subject);
			
			From = EmptyStringIfValueNotYetSet(From);
			
			To = EmptyStringIfValueNotYetSet(To);
			
			MsgHtml = EmptyStringIfValueNotYetSet(MsgHtml);
			
			AttachmentImagePathServer = EmptyStringIfValueNotYetSet(AttachmentImagePathServer);
			
			AttachmentImageServer = EmptyStringIfValueNotYetSet(AttachmentImageServer);
			
			AttachmentPathServer = EmptyStringIfValueNotYetSet(AttachmentPathServer);
			
			AttachmentFileServer = EmptyStringIfValueNotYetSet(AttachmentFileServer);
			
		} // SetToEmptyStringsForValuesNotYetSet		
		
		/// <summary>Returns not yet set value if string is empty</summary>
        private string SetToValueNotYetSetIfEmptyString(string i_value)
        {
            string ret_string = i_value;

            if (i_value.Length == 0)
            {
                ret_string = JazzXml.GetUndefinedNodeValue();
            }

            return ret_string;

        } // SetToValueNotYetSetIfEmptyString
		
        /// <summary>Returns a JazzNewsletter object where empty strings have been replaced by JazzXml.GetUndefinedNodeValue()</summary>
        public JazzNewsletter GetObjectWithUndefinedNodeValues(JazzNewsletter i_jazz_newsletter)
        {
            JazzNewsletter ret_jazz_newletter = i_jazz_newsletter;

            EmlPathServer = SetToValueNotYetSetIfEmptyString(EmlPathServer);

            EmlFileServer = SetToValueNotYetSetIfEmptyString(EmlFileServer);

            SendYear = SetToValueNotYetSetIfEmptyString(SendYear);

            SendMonth = SetToValueNotYetSetIfEmptyString(SendMonth);

            SendDay = SetToValueNotYetSetIfEmptyString(SendDay);

            Subject = SetToValueNotYetSetIfEmptyString(Subject);
			
			From = SetToValueNotYetSetIfEmptyString(From);
			
			To = SetToValueNotYetSetIfEmptyString(To);
			
			MsgHtml = SetToValueNotYetSetIfEmptyString(MsgHtml);
			
			AttachmentImagePathServer = SetToValueNotYetSetIfEmptyString(AttachmentImagePathServer);
			
			AttachmentImageServer = SetToValueNotYetSetIfEmptyString(AttachmentImageServer);
			
			AttachmentPathServer = SetToValueNotYetSetIfEmptyString(AttachmentPathServer);
			
			AttachmentFileServer = SetToValueNotYetSetIfEmptyString(AttachmentFileServer);			
		
			return ret_jazz_newletter;

        } // GetObjectWithUndefinedNodeValues
		
		#endregion // Set to empty strings if member parameter values not yet are set

		#region Check parameter values
		
        /// <summary>Check the parameter values</summary>
        public bool CheckParameterValues(out string o_error)
        {
            o_error = @"";
            bool ret_bool = true;


            if (SendYear.Length != 4)
            {
                o_error = @"JazzNewsletter.CheckParameterValues SendYear string length is not four (4)";

                return false;
            }

            if (SendMonth.Length < 1 || SendMonth.Length > 2)
            {
                o_error = @"JazzNewsletter.CheckParameterValues SendMonth string length < 1 or > 2";

                return false;
            }

            if (SendDay.Length < 1 || SendDay.Length > 2)
            {
                o_error = @"JazzNewsletter.CheckParameterValues SendDay string length < 1 or > 2";

                return false;
            }

            if (SendYearInt < 1996)
            {
                o_error = @"JazzNewsletter.CheckParameterValues Error SendYearInt= " + SendYearInt.ToString();
				
                return false;
            }
			
			if (SendMonthInt < 1 || SendMonthInt > 12)
            {
                o_error = @"JazzNewsletter.CheckParameterValues Error SendMonthInt= " + SendMonthInt.ToString();
				
                return false;
            }

			if (SendDayInt < 1 || SendDayInt > 31)
            {
                o_error = @"JazzNewsletter.CheckParameterValues Error SendDayInt= " + SendDayInt.ToString();
				
                return false;
            }

            return ret_bool;

        } // CheckParameterValues

		#endregion Check parameter values

    } // JazzNewsletter

} // namespace
