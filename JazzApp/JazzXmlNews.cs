using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Functions for the initialization (creation) of XML objects from the XML file JazzNews.xml
    /// <para>The XML file holds news that can be displayed on the homepage</para>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {
        #region XML objects, file names and directory names

        /// <summary>The XML document (object) that corresponds to the news XML file (JazzNews.xml)</summary>
        static private XDocument m_xdocument_news = null;

        /// <summary>Returns the XML document (object) that corresponds to the news XML file (JazzNews.xml)</summary>
        static public XDocument GetObjectNews() { return m_xdocument_news; }

        /// <summary>Status for m_xdocument_news</summary>
        static private int m_xdocument_news_status = -12345;

        /// <summary>Error message for the creation of m_xdocument_news</summary>
        static private string m_xdocument_news_error = "Not set";

        /// <summary>The name of the news XML file</summary>
        static private string m_news_xml_filename = @"";

        /// <summary>Returns the name of the news XML file</summary>
        static public string GetFileNameObjectNews() { return m_news_xml_filename; }

        /// <summary>The URL to the folder with the news XML files</summary>
        static private string m_url_xml_news_files_folder = @"";

        /// <summary>Returns the URL path to the folder with the news XML files</summary>
        static public string GetUrlXmlNewsFiles() { return m_url_xml_news_files_folder; }

        #endregion // XML objects, file names and directory names

        #region Init functions

        /// <summary>Initialization of news member parameters 
        /// <para>Initialize the XML object (m_xdocument_news) that corresponds to the news XML file (JazzNews.xml). Call of InitXmlDocumentNews</para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_url_xml_news_files_folder">Server path to the XML news file<</param>
        /// <param name="i_news_xml_filename">Name of the XML news file</param>
        /// <param name="o_error">Error message</param>
        static public bool InitNews(string i_url_xml_news_files_folder, string i_news_xml_filename, out string o_error)
        {
            o_error = @"";

            m_url_xml_news_files_folder = i_url_xml_news_files_folder;
            m_news_xml_filename = i_news_xml_filename;
            // TODO Check data

            string error_message = @"";
            if (!InitXmlDocumentNews(out error_message))
            {
                o_error = @"JazzXml.InitNews Programming error " + error_message;
                return false;
            }

            return true;

        } // InitNews

        /// <summary>Initialize the XML object (m_xdocument_news) that corresponds to the news XML file (JazzNews.xml)</summary>
        static private bool InitXmlDocumentNews(out string o_error)
        {
            o_error = @"";

            string url_file_news = GetWebSiteUrl() + GetUrlXmlNewsFiles() + @"/" + GetFileNameObjectNews();
            if (url_file_news.Trim().Length == 0)
            {
                o_error = @"JazzXml.InitXmlDocumentNews URL for the news file is not defined";

                return false;
            }

            JazzOsUtils.LoadXmlDocument(url_file_news, 10, -1);

            string error_message = @"";
            if (GetXmlDocumentNewsStatus(out error_message) < 0)
            {
                o_error = @"JazzXml.InitXmlDocumentNews Programming error: " + error_message;

                return false;
            }

            // Additional (not necessary) check that object is created
            if (null == GetObjectNews())
            {
                o_error = @"JazzXml.InitXmlDocumentNews news object is not created (JazzOsUtils.LoadXmlDocument failed)";

                return false;
            }

            return true;

        } // InitXmlDocumentNews

        /// <summary>Set the XML object document news (m_xdocument_news) that corresponds to the news XML file (JazzNews.xml)</summary>
        static public void SetXmlDocumentNews(XDocument i_xdocument_news)
        {
            m_xdocument_news_status = 0;
            m_xdocument_news_error = "";

            m_xdocument_news = i_xdocument_news;
        } // SetXmlDocumentNews

        /// <summary>Set status for document news</summary>
        static public void SetXmlDocumentNewsStatus(int i_status, string i_error)
        {
            m_xdocument_news_status = i_status;
            m_xdocument_news_error = i_error;

        } // SetXmlDocumentNews

        /// <summary>Get status for document news</summary>
        static public int GetXmlDocumentNewsStatus(out string o_error)
        {
            o_error = m_xdocument_news_error;

            return m_xdocument_news_status;

        } // GetXmlDocumentNewsStatus

        #endregion // Init functions

        #region Get and set news functions

        /// <summary>Returns the news background color as string</summary>
        static public string GetNewsBackgroundColor() { return GetInnerTextNewsSingleNode(GetTagNewsBackgroundColor()); }

        /// <summary>Sets the news background color as a string</summary>
        static public void SetNewsBackgroundColor(string i_background_color) { SetInnerTextNewsSingleNode(GetTagNewsBackgroundColor(), i_background_color); }

        /// <summary>Returns the news text color as string</summary>
        static public string GetNewsTextColor() { return GetInnerTextNewsSingleNode(GetTagNewsTextColor()); }

        /// <summary>Sets the news text color as a string</summary>
        static public void SetNewsTextColor(string i_text_color) { SetInnerTextNewsSingleNode(GetTagNewsTextColor(), i_text_color); }

        /// <summary>Returns the news header as a string</summary>
        static public string GetNewsHeader(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsHeader()); }

        /// <summary>Sets the news header as a string</summary>
        static public void SetNewsHeader(int i_news_number, string i_news_header_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsHeader(), i_news_header_str); }

        /// <summary>Returns the news content as a string</summary>
        static public string GetNewsContent(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsContent()); }

        /// <summary>Sets the news content as a string</summary>
        static public void SetNewsContent(int i_news_number, string i_news_content_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsContent(), i_news_content_str); }

        /// <summary>Returns the news image URL as a string</summary>
        static public string GetNewsImage(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImage()); }

        /// <summary>Sets the news image URL as a string</summary>
        static public void SetNewsImage(int i_news_number, string i_news_image_url_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImage(), i_news_image_url_str); }

        /// <summary>Returns the news image width as a string</summary>
        static public string GetNewsImageWidth(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImageWidth()); }

        /// <summary>Sets the news image width as a string</summary>
        static public void SetNewsImageWidth(int i_news_number, string i_news_image_width_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImageWidth(), i_news_image_width_str); }

        /// <summary>Returns the news image title as a string</summary>
        static public string GetNewsImageTitle(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImageTitle()); }

        /// <summary>Sets the news image title as a string</summary>
        static public void SetNewsImageTitle(int i_news_number, string i_news_image_title_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImageTitle(), i_news_image_title_str); }

        /// <summary>Returns the news image caption as a string</summary>
        static public string GetNewsImageCaption(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImageCaption()); }

        /// <summary>Sets the news image caption as a string</summary>
        static public void SetNewsImageCaption(int i_news_number, string i_news_image_caption_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsImageCaption(), i_news_image_caption_str); }

        /// <summary>Returns the news link URL as a string</summary>
        static public string GetNewsLink(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsLink()); }

        /// <summary>Sets the news link URL as a string</summary>
        static public void SetNewsLink(int i_news_number, string i_news_link_url_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsLink(), i_news_link_url_str); }

        /// <summary>Returns the news link caption as a string</summary>
        static public string GetNewsLinkCaption(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsLinkCaption()); }

        /// <summary>Sets the news link caption as a string</summary>
        static public void SetNewsLinkCaption(int i_news_number, string i_news_link_caption_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsLinkCaption(), i_news_link_caption_str); }

        /// <summary>Returns the news email subject as a string</summary>
        static public string GetNewsEmailSubject(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEmailSubject()); }

        /// <summary>Sets the news email subject as a string</summary>
        static public void SetNewsEmailSubject(int i_news_number, string i_news_email_subject_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEmailSubject(), i_news_email_subject_str); }

        /// <summary>Returns the news email text as a string</summary>
        static public string GetNewsEmailText(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEmailText()); }

        /// <summary>Sets the news email text as a string</summary>
        static public void SetNewsEmailText(int i_news_number, string i_news_email_text_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEmailText(), i_news_email_text_str); }

        /// <summary>Returns the news email caption as a string</summary>
        static public string GetNewsEmailCaption(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEmailCaption()); }

        /// <summary>Sets the news email caption as a string</summary>
        static public void SetNewsEmailCaption(int i_news_number, string i_news_email_caption_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEmailCaption(), i_news_email_caption_str); }

        /// <summary>Returns the news start year as a string</summary>
        static public string GetNewsStartYear(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsStartYear()); }

        /// <summary>Sets the news start year as a string</summary>
        static public void SetNewsStartYear(int i_news_number, string i_news_start_year_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsStartYear(), i_news_start_year_str); }

        /// <summary>Returns the news start month as a string</summary>
        static public string GetNewsStartMonth(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsStartMonth()); }

        /// <summary>Sets the news start month as a string</summary>
        static public void SetNewsStartMonth(int i_news_number, string i_news_start_month_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsStartMonth(), i_news_start_month_str); }

        /// <summary>Returns the news start day as a string</summary>
        static public string GetNewsStartDay(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsStartDay()); }

        /// <summary>Sets the news start day as a string</summary>
        static public void SetNewsStartDay(int i_news_number, string i_news_start_day_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsStartDay(), i_news_start_day_str); }

        /// <summary>Returns the news end year as a string</summary>
        static public string GetNewsEndYear(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEndYear()); }

        /// <summary>Sets the news end year as a string</summary>
        static public void SetNewsEndYear(int i_news_number, string i_news_end_year_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEndYear(), i_news_end_year_str); }

        /// <summary>Returns the news end month as a string</summary>
        static public string GetNewsEndMonth(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEndMonth()); }

        /// <summary>Sets the news end month as a string</summary>
        static public void SetNewsEndMonth(int i_news_number, string i_news_end_month_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEndMonth(), i_news_end_month_str); }

        /// <summary>Returns the news end month as a string</summary>
        static public string GetNewsEndDay(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEndDay()); }

        /// <summary>Sets the news end month as a string</summary>
        static public void SetNewsEndDay(int i_news_number, string i_news_end_day_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsEndDay(), i_news_end_day_str); }

        /// <summary>Returns the news test flag as a string (value TRUE or FALSE)</summary>
        static public string GetNewsTestFlag(int i_news_number) { return GetInnerTextCurrentNewsNode(i_news_number, GetTagNewsTestFlag()); }

        /// <summary>Sets the news test flag as a string (value TRUE or FALSE)</summary>
        static public void SetNewsTestFlag(int i_news_number, string i_news_test_flag_str) { SetInnerTextCurrentNewsNode(i_news_number, GetTagNewsTestFlag(), i_news_test_flag_str); }

        /// <summary>Returns the news test flag as a boolean (value true or false)</summary>
        static public bool GetNewsTestFlagBool(int i_news_number)
        {
            string test_flag_str = GetNewsTestFlag(i_news_number);

            if (test_flag_str.Equals("TRUE"))
            {
                return true;
            }
            else
            {
                return false;
            }

        } // GetNewsTestFlagBool

        /// <summary>Sets the news test flag as a boolean (value true or false)</summary>
        static public void SetNewsTestFlagBool(int i_news_number, bool i_news_test_flag_bool)
        {
            if (i_news_test_flag_bool)
            {
                SetNewsTestFlag(i_news_number, "TRUE");
            }
            else
            {
                SetNewsTestFlag(i_news_number, "FALSE");
            }

        } // SetNewsTestFlagBool

        /// <summary>Returns the news concert number  as a string</summary>
        static public string GetConcertNewsNumber(int i_news_number) { return GetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsNumber()); }

        /// <summary>Sets the news concert number as a string</summary>
        static public void SetConcertNewsNumber(int i_news_number, string i_concert_news_number_str) { SetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsNumber(), i_concert_news_number_str); }

        /// <summary>Returns the news concert header as a string</summary>
        static public string GetConcertNewsHeader(int i_news_number) { return GetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsHeader()); }

        /// <summary>Sets the news concert header as a string</summary>
        static public void SetConcertNewsHeader(int i_news_number, string i_concert_news_header_str) { SetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsHeader(), i_concert_news_header_str); }

        /// <summary>Returns the news concert content as a string</summary>
        static public string GetConcertNewsContent(int i_news_number) { return GetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsContent()); }

        /// <summary>Sets the news concert content as a string</summary>
        static public void SetConcertNewsContent(int i_news_number, string i_concert_news_content_str) { SetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsContent(), i_concert_news_content_str); }

        /// <summary>Returns the concert news test flag as a string (value TRUE or FALSE)</summary>
        static public string GetConcertNewsTestFlag(int i_news_number) { return GetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsTestFlag()); }

        /// <summary>Sets the concert news test flag as a string (value TRUE or FALSE)</summary>
        static public void SetConcertNewsTestFlag(int i_news_number, string i_concert_news_test_flag_str) { SetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsTestFlag(), i_concert_news_test_flag_str); }

        /// <summary>Returns the concert news test flag as a boolean (value true or false)</summary>
        static public bool GetConcertNewsTestFlagBool(int i_news_number)
        {
            string test_flag_str = GetConcertNewsTestFlag(i_news_number);

            if (test_flag_str.Equals("TRUE"))
            {
                return true;
            }
            else
            {
                return false;
            }

        } // GetConcertNewsTestFlagBool

        /// <summary>Sets the concert news test flag as a boolean (value true or false)</summary>
        static public void SetConcertNewsTestFlagBool(int i_news_number, bool i_concert_news_test_flag_bool)
        {
            if (i_concert_news_test_flag_bool)
            {
                SetConcertNewsTestFlag(i_news_number, "TRUE");
            }
            else
            {
                SetConcertNewsTestFlag(i_news_number, "FALSE");
            }

        } // SetConcertNewsTestFlagBool

        /// <summary>Returns the concert news cancelled flag as a string (value TRUE or FALSE)</summary>
        static public string GetConcertNewsCancelledFlag(int i_news_number) { return GetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsCancelledFlag()); }

        /// <summary>Sets the concert news cancelled flag as a string (value TRUE or FALSE)</summary>
        static public void SetConcertNewsCancelledFlag(int i_news_number, string i_concert_news_cancelled_flag_str) { SetInnerTextConcertNewsNode(i_news_number, GetTagConcertNewsCancelledFlag(), i_concert_news_cancelled_flag_str); }

        /// <summary>Returns the concert news cancelled flag as a boolean (value true or false)</summary>
        static public bool GetConcertNewsCancelledFlagBool(int i_news_number)
        {
            string cancelled_flag_str = GetConcertNewsCancelledFlag(i_news_number);

            if (cancelled_flag_str.Equals("TRUE"))
            {
                return true;
            }
            else
            {
                return false;
            }

        } // GetConcertNewsCancelledFlagBool

        /// <summary>Sets the concert news cancelled flag as a boolean (value true or false)</summary>
        static public void SetConcertNewsCancelledFlagBool(int i_news_number, bool i_concert_news_cancelled_flag_bool)
        {
            if (i_concert_news_cancelled_flag_bool)
            {
                SetConcertNewsCancelledFlag(i_news_number, "TRUE");
            }
            else
            {
                SetConcertNewsCancelledFlag(i_news_number, "FALSE");
            }

        } // SetConcertNewsCancelledFlagBool

        #endregion // Get and set news functions

        #region Get functions for objects JazzNews

        /// <summary>Returns the number of current news defined in XML object m_xdocument_news corresponding to the file JazzNews.xml</summary>
        public static int GetNumberOfCurrentNews(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_news, GetTagCurrentNews(), out o_error);

        } // GetNumberOfCurrentNews

        /// <summary>Returns the number of concert news defined in XML object m_xdocument_news corresponding to the file JazzNews.xml</summary>
        public static int GetNumberOfConcertNews(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_news, GetTagConcertNews(), out o_error);

        } // GetNumberOfConcertNews

        #endregion // Get functions for objects JazzNews

        #region Delete news element

        /// <summary>Removes current news element in the XML object m_xdocument_news corresponding to the file JazzNews.xml</summary>
        public static bool RemoveNewsElement(int i_news_number, out string o_error)
        {
            o_error = @"";

            if (!JazzXml.RemoveFirstLevelElement(m_xdocument_news, GetTagCurrentNews(), i_news_number, out o_error))
            {
                o_error = @"JazzXml.RemoveNewsElement failed " + o_error;

                return false;
            }

            return true;

        } // RemoveNewsElement

        /// <summary>Removes concert news element in the XML object m_xdocument_news corresponding to the file JazzNews.xml</summary>
        public static bool RemoveConcertNewsElement(int i_news_number, out string o_error)
        {
            o_error = @"";

            if (!JazzXml.RemoveFirstLevelElement(m_xdocument_news, GetTagConcertNews(), i_news_number, out o_error))
            {
                o_error = @"JazzXml.RemoveConcertNewsElement failed " + o_error;

                return false;
            }

            return true;

        } // RemoveConcertNewsElement

        #endregion // Delete news element

        #region Add news element

        /// <summary>Adds a current news element to the XML object corresponding to XML file JazzNews.xml
        /// <para>1. Create a current news element. Call of CreateCurrentNewsElement</para>
        /// <para>2. Add the element. Call of AddFirstLevelElement</para>
        /// </summary>
        public static bool AddCurrentNewsElement(out string o_error)
        {
            o_error = @"";

            XElement current_news_element = CreateCurrentNewsElement();

            if (!AddFirstLevelElement(m_xdocument_news, current_news_element, out  o_error))
            {
                o_error = @"JazzXml.AddCurrentNewsElement AddFirstLevelElement failed " + o_error;

                return false;
            }

            return true;

        } // AddCurrentNewsElement

        /// <summary>Adds a concert news element to the XML object corresponding to XML file JazzNews.xml
        /// <para>1. Create a concert news element. Call of CreateConcertNewsElement</para>
        /// <para>2. Add the element. Call of AddFirstLevelElement</para>
        /// </summary>
        public static bool AddConcertNewsElement(out string o_error)
        {
            o_error = @"";

            XElement current_news_element = CreateConcertNewsElement();

            if (!AddFirstLevelElement(m_xdocument_news, current_news_element, out o_error))
            {
                o_error = @"JazzXml.AddConcertNewsElement AddFirstLevelElement failed " + o_error;

                return false;
            }

            return true;

        } // AddConcertNewsElement

        /// <summary>Creates a current news XElement (a sub-tree)
        /// <para>Only header and test flag are set. All other values are undefined</para>
        /// </summary>
        public static XElement CreateCurrentNewsElement()
        {
            XElement ret_element = new XElement(GetTagCurrentNews(),
                                   new XElement(GetTagNewsHeader(), "Header ...."),
                                   new XElement(GetTagNewsContent(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsImage(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsImageWidth(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsImageTitle(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsImageCaption(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsLink(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsLinkCaption(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsEmailSubject(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsEmailText(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsEmailCaption(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsStartYear(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsStartMonth(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsStartDay(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsEndYear(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsEndMonth(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsEndDay(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagNewsTestFlag(), "TRUE")
                                   );

            return ret_element;

        } // CreateCurrentNewsElement

        /// <summary>Creates a current news XElement (a sub-tree)
        /// <para>Only number, test and cancelled flags are set. All other values are undefined</para>
        /// </summary>
        public static XElement CreateConcertNewsElement()
        {
            XElement ret_element = new XElement(GetTagConcertNews(),
                                   new XElement(GetTagConcertNewsNumber(), "0"),
                                   new XElement(GetTagConcertNewsHeader(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagConcertNewsContent(), JazzXml.GetUndefinedNodeValue()),
                                   new XElement(GetTagConcertNewsTestFlag(), "TRUE"),
                                   new XElement(GetTagConcertNewsCancelledFlag(), "FALSE")
                                   );

            return ret_element;

        } // CreateConcertNewsElement


        #endregion // Add news element

    } // JazzXml

} // namespace
