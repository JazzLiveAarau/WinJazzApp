using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Functions for the initialization (creation) of XML objects from the XML file JazzNewsletter.xml
    /// <para>The XML file registers newsletters</para>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {
        #region XML objects, file names and directory names

        /// <summary>The XML document (object) that corresponds to the newsletter XML file (JazzNewsletter.xml)</summary>
        static private XDocument m_xdocument_newsletter = null;

        /// <summary>Returns the XML document (object) that corresponds to the newsletter XML file (JazzNewsletter.xml)</summary>
        static public XDocument GetObjectNewsletter() { return m_xdocument_newsletter; }

        /// <summary>Status for m_xdocument_newsletter</summary>
        static private int m_xdocument_newsletter_status = -12345;

        /// <summary>Error message for the creation of m_xdocument_newsletter</summary>
        static private string m_xdocument_newsletter_error = "Not set";

        /// <summary>The name of the newsletter XML file</summary>
        static private string m_newsletter_xml_filename = @"";

        /// <summary>Returns the name of the newsletter XML file</summary>
        static public string GetFileNameObjectNewsletter() { return m_newsletter_xml_filename; }

        /// <summary>The URL to the folder with the newsletter XML file</summary>
        static private string m_newsletter_xml_file_folder = @"";

        /// <summary>Returns the URL path to the folder with the newsletter XML file</summary>
        static public string GetUrlXmlNewsletterFile() { return m_newsletter_xml_file_folder; }

        #endregion // XML objects, file names and directory names

        #region Init functions

        /// <summary>Initialization of newsletter member parameters 
        /// <para>Initialize the XML object (m_xdocument_newsletter) that corresponds to the newsletter XML file (JazzNewsletter.xml). Call of InitXmlDocumentNewsletter</para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_newsletter_xml_file_folder">Server path to the XML newsletter file<</param>
        /// <param name="i_newsletter_xml_filename">Name of the XML newsletter file</param>
        /// <param name="o_error">Error message</param>
        static public bool InitNewsletter(string i_newsletter_xml_file_folder, string i_newsletter_xml_filename, out string o_error)
        {
            o_error = @"";

            m_newsletter_xml_file_folder = i_newsletter_xml_file_folder;
            m_newsletter_xml_filename = i_newsletter_xml_filename;
            // TODO Check data

            string error_message = @"";
            if (!InitXmlDocumentNewsletter(out error_message))
            {
                o_error = @"JazzXml.InitNewsletter Programming error " + error_message;
                return false;
            }

            return true;

        } // InitNewsletter

        /// <summary>Application XML object is not created</summary>
        static private string GetWebSiteUrlTemp()
        {
            return @"http://www.jazzliveaarau.ch/";
        }

        /// <summary>Initialize the XML object (m_xdocument_newsletter) that corresponds to the newsletter XML file (JazzNewsletter.xml)</summary>
        static private bool InitXmlDocumentNewsletter(out string o_error)
        {
            o_error = @"";

            string url_file_newsletter = GetWebSiteUrlTemp() + GetUrlXmlNewsletterFile() + @"/" + GetFileNameObjectNewsletter();
            if (url_file_newsletter.Trim().Length == 0)
            {
                o_error = @"JazzXml.InitXmlDocumentNewsletter URL for the newsletter file is not defined";

                return false;
            }

            JazzOsUtils.LoadXmlDocument(url_file_newsletter, 11, -1);

            string error_message = @"";
            if (GetXmlDocumentNewsletterStatus(out error_message) < 0)
            {
                o_error = @"JazzXml.InitXmlDocumentNewsletter Programming error: " + error_message;

                return false;
            }

            // Additional (not necessary) check that object is created
            if (null == GetObjectNewsletter())
            {
                o_error = @"JazzXml.InitXmlDocumentNewsletter newsletter object is not created (JazzOsUtils.LoadXmlDocument failed)";

                return false;
            }

            return true;

        } // InitXmlDocumentNewsletter

        /// <summary>Set the XML object document newsletter (m_xdocument_newsletter) that corresponds to the newsletter XML file (JazzNewsletter.xml)</summary>
        static public void SetXmlDocumentNewsletter(XDocument i_xdocument_newsletter)
        {
            m_xdocument_newsletter_status = 0;
            m_xdocument_newsletter_error = "";

            m_xdocument_newsletter = i_xdocument_newsletter;

        } // SetXmlDocumentNewsletter

        /// <summary>Set status for document newsletter</summary>
        static public void SetXmlDocumentNewsletterStatus(int i_status, string i_error)
        {
            m_xdocument_newsletter_status = i_status;
            m_xdocument_newsletter_error = i_error;

        } // SetXmlDocumentNewsletterStatus

        /// <summary>Get status for document newsletter</summary>
        static public int GetXmlDocumentNewsletterStatus(out string o_error)
        {
            o_error = m_xdocument_newsletter_error;

            return m_xdocument_newsletter_status;

        } // GetXmlDocumentNewsletterStatus

        #endregion // Init functions

        #region Get and set newsletter functions

        /// <summary>Returns the newsletter EML path</summary>
        static public string GetNewsletterEmlPath(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterEmlPath()); }

        /// <summary>Sets the newsletter EML path</summary>
        static public void SetNewsletterEmlPath(int i_newsletter, string i_eml_path) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterEmlPath(), i_eml_path); }

        /// <summary>Returns the newsletter EML file</summary>
        static public string GetNewsletterEmlFile(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterEmlFile()); }

        /// <summary>Sets the newsletter EML file</summary>
        static public void SetNewsletterEmlFile(int i_newsletter, string i_eml_file) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterEmlFile(), i_eml_file); }

        /// <summary>Returns the newsletter year number as a string</summary>
        static public string GetNewsletterYear(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterYear()); }

        /// <summary>Sets the newsletter year number as a string</summary>
        static public void SetNewsletterYear(int i_newsletter, string i_newsletter_year_str) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterYear(), i_newsletter_year_str); }

        /// <summary>Returns the newsletter month number as a string</summary>
        static public string GetNewsletterMonth(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterMonth()); }

        /// <summary>Sets the newsletter month number as a string</summary>
        static public void SetNewsletterMonth(int i_newsletter, string i_newsletter_month_str) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterMonth(), i_newsletter_month_str); }

        /// <summary>Returns the newsletter day number as a string</summary>
        static public string GetNewsletterDay(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterDay()); }

        /// <summary>Sets the newsletter day number as a string</summary>
        static public void SetNewsletterDay(int i_newsletter, string i_newsletter_month_str) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterDay(), i_newsletter_month_str); }

        /// <summary>Returns the newsletter subject</summary>
        static public string GetNewsletterSubject(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterSubject()); }

        /// <summary>Sets the newsletter subject</summary>
        static public void SetNewsletterSubject(int i_newsletter, string i_newsletter_subject) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterSubject(), i_newsletter_subject); }

        /// <summary>Returns the newsletter from</summary>
        static public string GetNewsletterFrom(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterFrom()); }

        /// <summary>Sets the newsletter from</summary>
        static public void SetNewsletterFrom(int i_newsletter, string i_newsletter_from) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterFrom(), i_newsletter_from); }

        /// <summary>Returns the newsletter HTML message</summary>
        static public string GetNewsletterMsgHtml(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterMsgHtml()); }

        /// <summary>Sets the newsletter HTML message</summary>
        static public void SetNewsletterMsgHtml(int i_newsletter, string i_newsletter_msg) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterMsgHtml(), i_newsletter_msg); }

        /// <summary>Returns the newsletter image path</summary>
        static public string GetNewsletterImagePath(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterImagePath()); }

        /// <summary>Sets the newsletter image path</summary>
        static public void SetNewsletterImagePath(int i_newsletter, string i_newsletter_path) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterImagePath(), i_newsletter_path); }

        /// <summary>Returns the newsletter image file</summary>
        static public string GetNewsletterImageFile(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterImageFile()); }

        /// <summary>Sets the newsletter image file</summary>
        static public void SetNewsletterImageFile(int i_newsletter, string i_newsletter_file) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterImageFile(), i_newsletter_file); }

        /// <summary>Returns the newsletter embedded flag</summary>
        static public string GetNewsletterEmbeddedFlag(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterEmbeddedFlag()); }

        /// <summary>Sets the newsletter embedded flag</summary>
        static public void SetNewsletterEmbeddedFlag(int i_newsletter, string i_newsletter_flag) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterEmbeddedFlag(), i_newsletter_flag); }

        /// <summary>Returns the newsletter attachment path</summary>
        static public string GetNewsletterAttachmentPath(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterAttachmentPath()); }

        /// <summary>Sets the newsletter attachment path</summary>
        static public void SetNewsletterAttachmentPath(int i_newsletter, string i_newsletter_path) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterAttachmentPath(), i_newsletter_path); }

        /// <summary>Returns the newsletter attachment file</summary>
        static public string GetNewsletterAttachmentFile(int i_newsletter) { return GetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterAttachmentFile()); }

        /// <summary>Sets the newsletter attachment file</summary>
        static public void SetNewsletterAttachmentFile(int i_newsletter, string i_newsletter_file) { SetInnerTextNewsletterNode(i_newsletter, GetTagNewsletterAttachmentFile(), i_newsletter_file); }

        #endregion // Get and set newsletter functions

        #region Get functions for objects JazzNewsletter

        /// <summary>Returns the number of newsletters defined in XML object m_xdocument_newsletter corresponding to the file JazzNewsletter.xml</summary>
        public static int GetNumberOfNewsletters(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_newsletter, GetTagNewsletter(), out o_error);

        } // GetNumberOfNewsletters

        /// <summary>Returns all the newsletters</summary>
        static public JazzNewsletter[] GetAllNewsletters(out string o_error)
        {
            o_error = @"";
            JazzNewsletter[] ret_newsletters = null;

            string error_message = @"";
            int n_newsletters = GetNumberOfRequests(out error_message);
            if (n_newsletters <= 0)
            {
                o_error = @"JazzXml.GetAllNewsletters n_newsletters <= 0";
                return ret_newsletters;
            }

            ret_newsletters = new JazzNewsletter[n_newsletters];

            for (int newsletter_number = 1; newsletter_number <= n_newsletters; newsletter_number++)
            {
                JazzNewsletter current_newsletter = new JazzNewsletter();

                current_newsletter.SendYear = GetNewsletterYear(newsletter_number);

                current_newsletter.SetToEmptyStringsForValuesNotYetSet();

                if (!current_newsletter.CheckParameterValues(out o_error))
                {
                    o_error = @"JazzXmlNewsletter.GetAllNewsletters JazzNewsletter value not OK " + o_error;
                    return null;
                }

                ret_newsletters[newsletter_number - 1] = current_newsletter;

            } // Loop newsletter_number

            return ret_newsletters;

        } // GetAllNewsletters

        #endregion // Get functions for objects JazzNewsletter

        #region Append (add) newsletter

        /// <summary>Adds a request node to the requests XDocument object (m_xdocument_newsletter) corresponding to XML file JazzAnfragen.xml</summary>
        /// <param name="i_jazz_newsletter">Object JazzNewsletter that shall be added as a new node</param>
        /// <param name="o_error">Error message</param>
        static public bool AddNewsletter(JazzNewsletter i_jazz_newsletter, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_newsletter)
            {
                o_error = @"JazzXml.AddNewsletter Input JazzNewsletter is null";
                return false;
            }

            XElement newsletter_element = NewsletterElement(i_jazz_newsletter, out o_error);

            if (null == newsletter_element)
            {
                o_error = @"JazzXml.AddNewsletter NewsletterElement failed " + o_error;
                return false;
            }

            XElement appl_root = m_xdocument_newsletter.Root;

            appl_root.Add(newsletter_element);

            return true;

        } // AddNewsletter

        /// <summary>Creates a newsletter XElement (a sub-tree)
        /// <para>The returned XElement will be added to an existing XDocument</para>
        /// </summary>
        /// <param name="i_jazz_newsletter">JazzNewsletter object that shall become the output XElement</param>
        /// <param name="o_error">Error message</param>
        public static XElement NewsletterElement(JazzNewsletter i_jazz_newsletter, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_newsletter)
            {
                o_error = @"JazzXml.NewsletterElement Input JazzNewsletter is null";
                return null;
            }

            XElement ret_element = new XElement(GetTagNewsletter(),
                                   new XElement(GetTagNewsletterEmlPath(), i_jazz_newsletter.EmlPathServer),
                                   new XElement(GetTagNewsletterEmlFile(), i_jazz_newsletter.EmlFileServer),
                                   new XElement(GetTagNewsletterYear(), i_jazz_newsletter.SendYear),
                                   new XElement(GetTagNewsletterMonth(), i_jazz_newsletter.SendMonth),
                                   new XElement(GetTagNewsletterDay(), i_jazz_newsletter.SendDay),
                                   new XElement(GetTagNewsletterSubject(), i_jazz_newsletter.Subject),
                                   new XElement(GetTagNewsletterFrom(), i_jazz_newsletter.From),
                                   new XElement(GetTagNewsletterMsgHtml(), i_jazz_newsletter.MsgHtml),
                                   new XElement(GetTagNewsletterImagePath(), i_jazz_newsletter.AttachmentImagePathServer),
                                   new XElement(GetTagNewsletterImageFile(), i_jazz_newsletter.AttachmentImageServer),
                                   new XElement(GetTagNewsletterEmbeddedFlag(), i_jazz_newsletter.EmbeddedPosterFlag),
                                   new XElement(GetTagNewsletterAttachmentPath(), i_jazz_newsletter.AttachmentPathServer),
                                   new XElement(GetTagNewsletterAttachmentFile(), i_jazz_newsletter.AttachmentFileServer)

                                   );

            return ret_element;

        } // NewsletterElement


        #endregion //  Append (add) newsletter		

    } // JazzXmlNewsletter

} // namespace
