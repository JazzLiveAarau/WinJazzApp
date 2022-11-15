using System;
using System.Linq;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>The class JazzXml gets data from the XML files on the server, stores the data in XML objects and makes
    /// Holds all data for the application.
    /// <para>For each concert season there is an XML file with data about the concert</para>
    /// <para>GUI data for the application and data about the club and its members are defined in another XML fil</para>
    /// <para>The InitXml function gets the data from the XML files on the server and stores the data in static variables </para>
    /// <para>For each XML element there is a get function, e.g. for the element AboutUsHeader the function GetAboutUsHeader()</para>
    /// <para>The member variable m_document_current holds the current season XML object.</para>
    /// </summary>
    /// <remarks>
    /// <para>The server URL is hardcoded (member variable web_site_url)</para>
    /// <para>Passwords are not necessary since XML files are in the public website folder</para>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {

        #region Member variables

        #region Connection data

        /// <summary>FTP host</summary>
        static private string m_ftp_host = "www.jazzliveaarau.ch";

        /// <summary>Get or set the FTP host</summary>
        static public string FtpHost
        { get { return m_ftp_host; } set { m_ftp_host = value; } }

        /// <summary>FTP user</summary>
        static private string m_ftp_user = "";

        /// <summary>Get or set the FTP user</summary>
        static public string FtpUser
        { get { return m_ftp_user; } set { m_ftp_user = value; } }

        /// <summary>FTP password for the download and upload</summary>
        static private string m_ftp_password = "";

        /// <summary>Get or set the FTP password</summary>
        static public string FtpPassword
        { get { return m_ftp_password; } set { m_ftp_password = value; } }

        /// <summary>Local execution path</summary>
        static private string m_exe_path = "";

        /// <summary>Get or set local execution path</summary>
        static public string ExePath
        { get { return m_exe_path; } set { m_exe_path = value; } }

        #endregion // Connection data

        /// <summary>Website URL.Must (only) be used for function GetCurrentSeasonFileName and LoadApplicationDocument</summary>
        static private String m_web_site_url = "http://www.jazzliveaarau.ch/";
        // Does not work static private String m_web_site_url = "https://www.jazzliveaarau.ch/";
        static public string WebSiteUrlBeforeLoadXml { get { return m_web_site_url; } }

        /// <summary>The XML document (object) that holds the active (current) season XML object</summary>
        static private XDocument m_document_current = null;

        /// <summary>The XML document (object) that corresponds to the application data XML file</summary>
        static private XDocument m_document_application = null;

        /// <summary>The XML document (object) that corresponds to the application data XML file</summary>
        static private String m_application_xml_filename = "JazzApplication.xml";

        /// <summary>The jazz concert XML files on the server defined as season start years</summary>
        static private int[] m_seasons_start_years = null;

        /// <summary>The jazz concert XML files converted to season names</summary>
        static private String[] m_seasons_strings = null;

        /// <summary>The jazz concert XML files on the server defined as XML documents</summary>
        static private XDocument[] m_seasons_documents = null;

        /// <summary>The jazz concert XML files status Eq. 0: OK Eq. 1: Not published Eq. -1: File missing  Eq. -2:  File corrupt Eq. -3:  File corrupt or missing</summary>
        static private int[] m_seasons_status = null;

        /// <summary>Flag telling if the season XML objects are set, i.e. if function InitXmlAllSeasons has been called</summary>
        static private bool m_seasons_documents_initialized = false;

        /// <summary>String that defines an undefined (not yet set) node value</summary>
        static private String m_undefined_node_value = "NotYetSetNodeValue";

        #endregion // Member variables

        #region Initialization

        /// <summary>Sets the FTP connection data</summary>
        static public void SetFtpConnectionData(string i_ftp_host, string i_ftp_user, string i_ftp_password, string i_exe_path)
        {
            FtpHost = i_ftp_host;

            FtpUser = i_ftp_user;

            FtpPassword = i_ftp_password;

            ExePath = i_exe_path;

        } // SetFtpConnectionData

        /// <summary>Sets the application XML object and the active season XML object to the current season </summary>
        static public void InitApplicationAndCurrentSeasonXml()
        {
            EscapeNumberInit();

            string application_xml_url = GetApplicationFileName();

            JazzOsUtils.LoadXmlDocument(application_xml_url, 1, -12345);

            string current_season_xml_url = GetCurrentSeasonFileName();

            JazzOsUtils.LoadXmlDocument(current_season_xml_url, 2, -12345);

        } // InitApplicationAndCurrentSeasonXml

        /// <summary>Initialize all season XML documents i.e. set array m_seasons_documents</summary>
        static public Boolean InitXmlAllSeasons()
        {
            m_seasons_start_years = JazzUtils.GetSeasonStartYearsForExistingXmlFiles();
            if (null == m_seasons_start_years)
                return false;

            m_seasons_strings = JazzUtils.GetSeasonNamesForExistingXmlFiles(m_seasons_start_years);
            if (null == m_seasons_strings)
                return false;

            int size_seasons_documents = m_seasons_start_years.Length;
            if (0 == size_seasons_documents)
                return false;

            m_seasons_documents = new XDocument[size_seasons_documents];

            m_seasons_status = new int[size_seasons_documents];
            for (int index_init = 0; index_init < m_seasons_status.Length; index_init++)
                m_seasons_status[index_init] = -12345;

            for (int index_doc = 0; index_doc < size_seasons_documents; index_doc++)
            {
                String season_file_name = GetSeasonFileName(m_seasons_start_years[index_doc]);

                JazzOsUtils.LoadXmlDocument(season_file_name, 3, index_doc);
            }

            Boolean b_set_publish = SetPublishStatus();

            m_seasons_documents_initialized = true;

            return true;
        } // InitXmlAllSeasons

        /// <summary>Returns true if the season XML objects are set, i.e. if function InitXmlAllSeasons has been called</summary>
        static public bool SeasonDocumentsInitialized() { return m_seasons_documents_initialized; }

        /// <summary>Set application document</summary>
        static public void SetApplicationDocument(XDocument i_document_application) { m_document_application = i_document_application; }

        /// <summary>Get application document</summary>
        static public XDocument GetApplicationDocument() { return m_document_application; }

        /// <summary>Set current season document</summary>
        static public void SetCurrentSeasonDocument(XDocument i_document_current) { m_document_current = i_document_current; }

        /// <summary>Set season document</summary>
        static public void SetSeasonDocument(XDocument i_document_current, int i_index)
        {
            if (null == m_seasons_documents)
                return;
            if (i_index < 0)
                return;
            if (i_index >= m_seasons_documents.Length)
                return;

            m_seasons_documents[i_index] = i_document_current;

            SetSeasonDocumentStatus(0, i_index);

        } // SetSeasonDocument

        /// <summary>Set season document status</summary>
        static public void SetSeasonDocumentStatus(int i_status, int i_index)
        {
            if (null == m_seasons_status)
                return;
            if (i_index < 0)
                return;
            if (i_index >= m_seasons_status.Length)
                return;

            m_seasons_status[i_index] = i_status;

        } // SetSeasonDocumentStatus

        /// <summary>Set publish status flags i.e. the array m_seasons_status</summary>
        static private Boolean SetPublishStatus()
        {
            if (m_seasons_documents == null)
                return false;
            int length_arrays = m_seasons_documents.Length;
            if (0 == length_arrays)
                return false;

            if (m_seasons_status == null)
                return false;
            if (m_seasons_status.Length != length_arrays)
                return false;

            XDocument input_document_current = m_document_current;

            for (int index_publ = 0; index_publ < length_arrays; index_publ++)
            {
                int season_status = m_seasons_status[index_publ];
                if (0 == season_status)
                {
                    int start_year = m_seasons_start_years[index_publ];
                    Boolean b_set = SetXmlDocument(start_year);
                    if (!b_set)
                        return false;
                    String b_publish = GetPublishProgram();
                    if (b_publish == "FALSE")
                        m_seasons_status[index_publ] = 1;
                }
            }

            SetCurrentSeasonDocument(input_document_current);

            return true;
        } // SetPublishStatus

        /// <summary> Set current XML document, i.e. m_document_current</summary>
        static public Boolean SetXmlDocument(int i_start_year)
        {

            if (null == m_seasons_start_years)
                return false;

            int size_seasons_documents = m_seasons_start_years.Length;
            if (0 == size_seasons_documents)
                return false;

            if (null == m_seasons_documents)
                return false;

            if (0 == m_seasons_documents.Length)
                return false;

            for (int i_set = 0; i_set < size_seasons_documents; i_set++)
            {
                int season_start_year = m_seasons_start_years[i_set];

                if (i_start_year == season_start_year)
                {
                    if (m_seasons_status[i_set] < 0)
                        return false;

                    m_document_current = m_seasons_documents[i_set];

                    break;
                }
            }

            return true;

        } // SetXmlDocument




        #endregion // Initialization

        #region XML utility functions

        /// <summary>Returns true if the XMLnode value is set</summary>
        static public Boolean XmlNodeValueIsSet(String i_node_value)
        {
            if (i_node_value == m_undefined_node_value)
                return false;
            else
                return true;

        } // XmlNodeValueIsSet

        /// <summary>Returns the string for an undefined (not set) node value
        /// <para>XML node values are not allowed to be empty (or just spaces).</para>
        /// <para>This special value is also used in many conditional statments of the application.</para>
        /// <para>Please refer to (search for) function XmlNodeValueIsSet(string) calls.</para>
        /// </summary>
        static public string GetUndefinedNodeValue() { return m_undefined_node_value; }

        /// <summary>Constructs the current season XML file name, i.e the URL for the XML file.</summary>
        static public String GetCurrentSeasonFileName()
        {
            int start_year = JazzUtils.GetCurrentSeasonStartYear();

            String ret_season_file_name = GetSeasonFileName(start_year);

            return ret_season_file_name;
        } // GetCurrentSeasonFileName

        /// <summary>Constructs the season XML file name, i.e. the URL for the file</summary>
        static public String GetSeasonFileName(int i_start_year)
        {
            String ret_season_file_name = WebSiteUrlBeforeLoadXml + "XML/JazzProgramm_" + i_start_year.ToString() + "_" + (i_start_year + 1).ToString() + ".xml";

            return ret_season_file_name;
        } // GetSeasonFileName

        /// <summary>Constructs the season XML local file name</summary>
        static public String GetSeasonLocalFileName(int i_start_year)
        {
            String ret_season_file_name = "JazzProgramm_" + i_start_year.ToString() + "_" + (i_start_year + 1).ToString() + ".xml";

            return ret_season_file_name;
        } // GetSeasonLocalFileName

        /// <summary>Constructs the application XML file name, i.e. the URL for the file</summary>
        static public String GetApplicationFileName()
        {
            String xml_file_name = WebSiteUrlBeforeLoadXml + "XML/" + GetApplicationLocalFileName();

            return xml_file_name;
        } // GetApplicationFileName

        /// <summary>Returns the application XML local file name</summary>
        static public String GetApplicationLocalFileName()
        {
            String xml_file_name = m_application_xml_filename;

            return xml_file_name;
        } // GetApplicationLocalFileName


        /// <summary>Returns an array of start years for the XML files that are on the server</summary>
        static public int[] GetSeasonsStartYears() { return m_seasons_start_years; }

        /// <summary>Returns a string array of season names</summary>
        static public String[] GetSeasonsStrings() { return m_seasons_strings; }

        /// <summary>Returns true if season program can be published or if season program exists and a member has logged in</summary>
        static private Boolean PublishedOrMemberLogin(int i_status, Boolean i_member_login)
        {
            Boolean ret_value = false;

            if (i_status >= 0)
            {
                if (i_status == 0)
                    ret_value = true;
                else if (i_status == 1 & i_member_login)
                    ret_value = true;
            }

            return ret_value;
        } // PublishedOrMemberLogin

        /// <summary>Returns the current document</summary>
        static public XDocument GetDocumentCurrent() { return m_document_current; }

        /// <summary>Sets the current document</summary>
        static public void SetDocumentCurrent(XDocument i_current_document) { m_document_current = i_current_document; }

        /// <summary>Gets the season documents</summary>
        static public XDocument[] GetSeasonDocuments() { return m_seasons_documents; }

        /// <summary>Returns an array of strings for the XML files that are available, i.e. allowed to be published</summary>
        /// <param name="i_member_login">Flag telling if the member is logged in</param>
        static public String[] GetAvailableSeasons(Boolean i_member_login)
        {
            String[] ret_available_seasons = null;

            if (null == m_seasons_status)
                return ret_available_seasons;

            if (null == m_seasons_strings)
                return ret_available_seasons;

            if (m_seasons_status.Length != m_seasons_strings.Length)
                return ret_available_seasons;

            int n_available_seasons = 0;
            for (int index_available = 0; index_available < m_seasons_status.Length; index_available++)
            {
                if (PublishedOrMemberLogin(m_seasons_status[index_available], i_member_login))
                    n_available_seasons = n_available_seasons + 1;
            }

            ret_available_seasons = new String[n_available_seasons];

            int index_output = 0;
            for (int index_season = 0; index_season < m_seasons_strings.Length; index_season++)
            {
                if (PublishedOrMemberLogin(m_seasons_status[index_season], i_member_login))
                {
                    ret_available_seasons[index_output] = m_seasons_strings[index_season];
                    index_output = index_output + 1;
                }
            } // index_season

            return ret_available_seasons;
        } // GetAvailableSeasons

        /// <summary>Returns an array of XML documents for the XML files that are available, i.e. that are allowed to be published</summary>
        static public XDocument[] GetAvailableSeasonDocuments(Boolean i_member_login)
        {
            XDocument[] ret_available_documents = null;

            if (null == m_seasons_status)
                return ret_available_documents;

            if (null == m_seasons_documents)
                return ret_available_documents;

            if (m_seasons_status.Length != m_seasons_documents.Length)
                return ret_available_documents;

            int n_available_seasons = 0;
            for (int index_available = 0; index_available < m_seasons_status.Length; index_available++)
            {
                if (PublishedOrMemberLogin(m_seasons_status[index_available], i_member_login))
                    n_available_seasons = n_available_seasons + 1;
            }

            ret_available_documents = new XDocument[n_available_seasons];

            int index_output = 0;
            for (int index_season = 0; index_season < m_seasons_strings.Length; index_season++)
            {
                if (PublishedOrMemberLogin(m_seasons_status[index_season], i_member_login))
                {
                    ret_available_documents[index_output] = m_seasons_documents[index_season];
                    index_output = index_output + 1;
                }
            } // index_season

            return ret_available_documents;

        } // GetAvailableSeasonDocuments

        /// <summary>Set XML document for a new season, i.e. change to another season (that the user has selected)</summary>
        static public Boolean SetXmlNewSeason(int i_start_year) { return SetXmlDocument(i_start_year); }

        /// <summary>Shortens a string and ending it with three points</summary>
        static private String ShortenString(String i_string, int n_characters)
        {
            String ret_string = "Undefined";

            if (i_string.Length <= n_characters)
            {
                ret_string = i_string;
                return ret_string;
            }

            ret_string = i_string.Substring(0, n_characters - 4) + "...";

            return ret_string;
        } // ShortenString

        /// <summary>Returns seasons concerts numbers corresponding to GetSeasonConcerts</summary>
        static public int[] GetSeasonConcertNumbers()
        {
            int[] ret_current_season_concert_numbers = null;

            int n_concerts = GetNumberConcertsInCurrentDocument();
            if (n_concerts <= 0)
                return ret_current_season_concert_numbers;

            ret_current_season_concert_numbers = new int[n_concerts];

            for (int i_concert = 1; i_concert <= n_concerts; i_concert++)
            {
                String band_name = GetBandName(i_concert);
                if (XmlNodeValueIsSet(band_name))
                {
                    ret_current_season_concert_numbers[i_concert - 1] = i_concert;
                }
            }

            return ret_current_season_concert_numbers;
        } // GetSeasonConcertNumbers

        #endregion XML utility functions


        #region XML get functions

        /// <summary>Returns the inner text (the value) of the node for the application XML document
        /// <para>Please note that the called function XElement.Value returns &, <, > and " and not the escaped chars (strings) &amp;, &lt;, ...</para>
        /// <para>The function XElement.Value has probably changed or it is different for different XML libraries. TODO Find out how it works for Android and iOs</para>
        /// <para>The function ModifyReadXml should be removed (or moved to AdminUtils), but is kept while it is used by one function in AdminUtils</para>
        /// </summary>
        static private String GetInnerTextForApplicationNode(String i_tag_name)
        {
            String ret_inner_text = "";

            if (null == m_document_application)
                return "Error JazzXml.GetInnerTextForApplicationNode: Application XML Document is null";

            XElement appl_root = m_document_application.Root;

            XElement first_element = (from element in appl_root.Descendants(i_tag_name)
                                      select element).First();

            ret_inner_text = first_element.Value;

            //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

            return ret_inner_text;

        } // GetInnerTextForApplicationNode

        /// <summary>Returns the inner text (the value) of the node for a concerts XML document
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        static private String GetInnerTextForConcertsNode(String i_tag_name)
        {
            String ret_inner_text = "";

            if (null == m_document_current)
                return "Error JazzXml.GetInnerTextForConcertsNode: Current XML document is null";

            XElement concerts_root = m_document_current.Root;

            XElement first_element = (from element in concerts_root.Descendants(i_tag_name)
                                      select element).First();

            ret_inner_text = first_element.Value;

            //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

            return ret_inner_text;

        } // GetInnerTextForConcertsNode

        /// <summary>Returns the inner text of the node for the current concert XML document
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private String GetInnerTextForNode(int i_concert, String i_tag_name)
        {
            String ret_inner_text = "";

            if (null == m_document_current)
                return "Error JazzXml.GetInnerTextForNode: Current XML document is null";

            int current_concert_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert == current_concert_number)
                {
                    XElement first_element = (from el in element_concert.Descendants(i_tag_name)
                                              select el).First();

                    ret_inner_text = first_element.Value;

                    //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

                    return ret_inner_text;
                }
            }

            return "Error JazzXml.GetInnerTextForNode: No node for the given concert number";

        } // GetInnerTextForNode

        /// <summary>Returns the member inner text for a given tag name
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_member">Member number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private String GetMemberInnerText(int i_member, String i_tag_name)
        {
            String ret_inner_text = "";

            if (null == m_document_application)
                return "Error JazzXml.GetMemberInnerText: Application XML Document is null";

            int current_member_number = 0;
            foreach (XElement element_concert in m_document_application.Descendants("Member"))
            {
                current_member_number = current_member_number + 1;
                if (i_member == current_member_number)
                {
                    XElement first_element = (from el in element_concert.Descendants(i_tag_name)
                                              select el).First();

                    ret_inner_text = first_element.Value;

                    //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

                    return ret_inner_text;
                }
            }

            return "Error JazzXml.GetMemberInnerText: Member " + i_member.ToString() + " does not exist";
        }

        /// <summary>Returns the number of members</summary>
        static public int GetNumberOfMembers()
        {
            int ret_number_members = 0;

            if (null == m_document_application)
                return -1;

            foreach (XElement element_concert in m_document_application.Descendants("Member"))
            {
                ret_number_members = ret_number_members + 1;
            }

            return ret_number_members;
        }

        /// <summary>Returns the inner text of the node for the current concert XML document
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_concert">Concert number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_musician">Musician number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private String GetMusicianInnerText(int i_concert, int i_musician, String i_tag_name)
        {
            String ret_inner_text = "";

            if (null == m_document_current)
                return "Programming error JazzXml.GetMusicianInnerText: Current XML document is null";

            if (i_concert <= 0)
                return "Programming error JazzXml.GetMusicianInnerText: Concert number <= 0";

            if (i_musician <= 0)
                return "Programming error JazzXml.GetMusicianInnerText: Musician number <= 0";

            int current_concert_number = 0;
            int current_musician_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert == current_concert_number)
                {
                    foreach (XElement element_musician in element_concert.Descendants("Musician"))
                    {
                        current_musician_number = current_musician_number + 1;
                        if (i_musician == current_musician_number)
                        {
                            XElement first_element = (from el in element_musician.Descendants(i_tag_name)
                                                      select el).First();
                            ret_inner_text = first_element.Value;

                            //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

                            return ret_inner_text;
                        } // Musician exists

                    } // element_musician
                } // Concert exists
            } // element_concert

            return ret_inner_text;
        }

        /// <summary>Returns true if the musician exists (is set) for a given concert and musician number</summary>
        static public Boolean MusicianExists(int i_concert, int i_musician)
        {
            Boolean musician_is_set = false;

            if (null == m_document_current)
                return false;

            int current_concert_number = 0;
            int current_musician_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert == current_concert_number)
                {
                    foreach (XElement element_musician in element_concert.Descendants("Musician"))
                    {
                        current_musician_number = current_musician_number + 1;
                        if (i_musician == current_musician_number)
                        {
                            musician_is_set = true;
                        } // Musician is set
                    } // element_musician
                } // Concert exists
            } // element_concert

            return musician_is_set;
        }

        /// <summary>Returns the number of musicians for a given concert</summary>
        public static int GetNumberMusicians(int i_concert)
        {
            int ret_number_musicians = 0;

            if (null == m_document_current)
                return -1;

            int current_concert_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert == current_concert_number)
                {
                    foreach (XElement element_musician in element_concert.Descendants("Musician"))
                    {
                        ret_number_musicians = ret_number_musicians + 1;
                    } // element_musician
                } // Concert exists
            } // element_concert

            return ret_number_musicians;
        } // GetNumberMusicians

        /// <summary>Returns the number of concerts in the current saison document</summary>
        static public int GetNumberConcertsInCurrentDocument()
        {
            int ret_n_concerts = 0;

            int current_concert_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                String band_name = GetBandName(current_concert_number);

                if (XmlNodeValueIsSet(band_name))
                    ret_n_concerts = ret_n_concerts + 1;

            } // element_concert

            return ret_n_concerts;
        } // GetNumberConcertsInCurrentDocument


        /// <summary>Returns seasons concerts as strings (for a menu)</summary>
        static public String[] GetSeasonConcerts()
        {
            String[] ret_current_season_concerts = null;

            int n_concerts = GetNumberConcertsInCurrentDocument();
            if (n_concerts <= 0)
                return ret_current_season_concerts;

            ret_current_season_concerts = new String[n_concerts];

            for (int i_concert = 1; i_concert <= n_concerts; i_concert++)
            {
                String band_name = GetBandName(i_concert);
                if (XmlNodeValueIsSet(band_name))
                {
                    String band_day = GetDay(i_concert);
                    String band_month = GetMonth(i_concert);
                    // String band_year = GetYear(i_concert);
                    String concert_info = band_day + "/" + band_month + " " + band_name;

                    String concert_info_short = ShortenString(concert_info, 31);

                    ret_current_season_concerts[i_concert - 1] = concert_info_short;

                }
            }

            return ret_current_season_concerts;
        } // GetSeasonConcerts


        #endregion // XML get functions

        #region Musician data

        /// <summary>Returns the musician name for a given concert and musician number</summary>
        static public String GetMusicianName(int i_concert, int i_musician) { return GetMusicianInnerText(i_concert, i_musician, "Name"); }

        /// <summary>Returns the musician instrument for a given concert and musician number</summary>
        static public String GetMusicianInstrument(int i_concert, int i_musician) { return GetMusicianInnerText(i_concert, i_musician, "Instrument"); }

        /// <summary>Returns the musician text for a given concert and musician number</summary>
        static public String GetMusicianText(int i_concert, int i_musician) { return GetMusicianInnerText(i_concert, i_musician, "Text"); }

        /// <summary>Returns the musicians day of birth as string for a given concert and musician number</summary>
        static public String GetMusicianBirthYearStr(int i_concert, int i_musician) { return GetMusicianInnerText(i_concert, i_musician, "BirthYear"); }

        /// <summary>Returns the musicians gender as string for a given concert and musician number</summary>
        static public String GetMusicianGenderStr(int i_concert, int i_musician) { return GetMusicianInnerText(i_concert, i_musician, "Gender"); }

        /// <summary>Returns the musicians day of birth as int for a given concert and musician number
        ///  Returns negative value if not defined, if string not is a number or if concert/musician number <= 0</summary>
        static public int GetMusicianBirthYear(int i_concert, int i_musician)
        {
            int ret_year = -12345;

            if (i_concert <= 0)
                return -54321;
            if (i_musician <= 0)
                return -54321;

            String year_str = GetMusicianBirthYearStr(i_concert, i_musician);

            if (!XmlNodeValueIsSet(year_str))
            {
                return -1;
            }

            ret_year = JazzUtils.StringToInt(year_str);

            return ret_year;
        }


        /// <summary>Returns true if musician is female </summary>
        static public Boolean MusicianIsFemale(int i_concert, int i_musician)
        {
            Boolean ret_female = false;

            if (i_concert <= 0)
                return false;
            if (i_musician <= 0)
                return false;

            String gender_str = GetMusicianGenderStr(i_concert, i_musician);

            if (!XmlNodeValueIsSet(gender_str))
            {
                return false;
            }

            if (gender_str == "female")
                ret_female = true;
            else
                ret_female = false;

            return ret_female;
        } // MusicianIsFemale

        /// <summary>Returns true if the musician text is set for a given concert and musician number</summary>
        static public Boolean MusicianTextIsSet(int i_concert, int i_musician)
        {
            if (!MusicianExists(i_concert, i_musician))
                return false;

            if (XmlNodeValueIsSet(GetMusicianText(i_concert, i_musician)))
                return true;
            else
                return false;
        }


        #endregion // Musician data

        #region Application XML document: First element


        /// <summary>Returns the JAZZ live AARAU website URL</summary>
        static public String GetWebSiteUrl() { return GetInnerTextForApplicationNode("WebSiteUrl"); }

        /// <summary>Returns the JAZZ live AARAU Intranet URL</summary>
        static public String GetIntranetUrl() { return GetInnerTextForApplicationNode("IntranetUrl"); }

        /// <summary>Returns the street where the musicians may unload their instruments</summary>
        static public String GetUnloadStreet() { return GetInnerTextForApplicationNode("UnloadStreet"); }

        /// <summary>Returns the city where the musicians may unload their instruments</summary>
        static public String GetUnloadCity() { return GetInnerTextForApplicationNode("UnloadCity"); }

        /// <summary>Returns the address to a recommended parking place for the musicians</summary>
        static public String GetParkingOne() { return GetInnerTextForApplicationNode("ParkingOne"); }

        /// <summary>Returns the address to a recommended parking place for the musicians</summary>
        static public String GetParkingTwo() { return GetInnerTextForApplicationNode("ParkingTwo"); }

        /// <summary>Returns the link to the web page: Necessary documents for a request</summary>
        static public String GetMusicianDocumentsRequestLink() { return GetInnerTextForApplicationNode("MusicianDocumentsRequestLink"); }

        /// <summary>Returns the link to the web page: Necessary documents for a contract</summary>
        static public String GetMusicianDocumentsContractLink() { return GetInnerTextForApplicationNode("MusicianDocumentsContractLink"); }

        /// <summary>Returns the link to the web page: Musician information for a concert</summary>
        static public String GetMusicianConcertInfoLink() { return GetInnerTextForApplicationNode("MusicianConcertInfoLink"); }

        /// <summary>Returns the premises header</summary>
        static public String GetPremisesHeader() { return GetInnerTextForApplicationNode("PremisesHeader"); }

        /// <summary>Returns the premises for JAZZ live AARAU</summary>
        static public String GetPremises() { return GetInnerTextForApplicationNode("Premises"); }

        /// <summary>Returns the premises street</summary>
        static public String GetPremisesStreet() { return GetInnerTextForApplicationNode("PremisesStreet"); }

        /// <summary>Returns the premises city</summary>
        static public String GetPremisesCity() { return GetInnerTextForApplicationNode("PremisesCity"); }

        /// <summary>Returns the premises website</summary>
        static public String GetPremisesWebsite() { return GetInnerTextForApplicationNode("PremisesWebsite"); }

        /// <summary>Returns the premises telepone number</summary>
        static public String GetPremisesTelephone() { return GetInnerTextForApplicationNode("PremisesTelephone"); }

        /// <summary>Returns the premises photo</summary>
        static public String GetPremisesPhoto() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("PremisesPhoto"); }

        /// <summary>Returns the premises map</summary>
        static public String GetPremisesMap() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("PremisesMap"); }

        /// <summary>Returns the contacts header</summary>
        static public String GetContactsHeader() { return GetInnerTextForApplicationNode("ContactsHeader"); }

        /// <summary>Returns the musician contact IBAN header</summary>
        static public String GetIbanHeader() { return GetInnerTextForApplicationNode("IbanHeader"); }

        /// <summary>Returns the musician contact remark header</summary>
        static public String GetMusicianRemarkHeader() { return GetInnerTextForApplicationNode("MusicianRemarkHeader"); }

        /// <summary>Returns the mail header</summary>
        static public String GetMailHeader() { return GetInnerTextForApplicationNode("MailHeader"); }

        /// <summary>Returns the email header</summary>
        static public String GetEmailHeader() { return GetInnerTextForApplicationNode("EmailHeader"); }

        /// <summary>Returns the reservation header</summary>
        static public String GetReservationHeader() { return GetInnerTextForApplicationNode("ReservationHeader"); }

        /// <summary>Returns the newsletter header</summary>
        static public String GetNewsletterHeader() { return GetInnerTextForApplicationNode("NewsletterHeader"); }

        /// <summary>Returns the webmaster header</summary>
        static public String GetWebmasterHeader() { return GetInnerTextForApplicationNode("WebmasterHeader"); }

        /// <summary>Returns the mail address</summary>
        static public String GetMailAddress() { return GetInnerTextForApplicationNode("MailAddress"); }

        /// <summary>Returns the jazz club name</summary>
        static public String GetClubName() { return GetInnerTextForApplicationNode("ClubName"); }

        /// <summary>Returns the email address</summary>
        static public String GetEmailAddress() { return GetInnerTextForApplicationNode("EmailJazzLiveAarau"); }

        /// <summary>Returns the email subject</summary>
        static public String GetEmailSubject() { return GetInnerTextForApplicationNode("EmailSubject"); }

        /// <summary>Returns the email text</summary>
        static public String GetEmailText() { return GetInnerTextForApplicationNode("EmailText"); }

        /// <summary>Returns the address text</summary>
        static public String GetAddressText() { return GetInnerTextForApplicationNode("AddressText"); }

        /// <summary>Returns the telephone text</summary>
        static public String GetTelephoneText() { return GetInnerTextForApplicationNode("TelephoneText"); }

        /// <summary>Returns the short telephone text</summary>
        static public String GetTelephoneShortText() { return GetInnerTextForApplicationNode("TelephoneShortText"); }

        /// <summary>Returns the placeholder name text</summary>
        static public String GetPlaceholderName() { return GetInnerTextForApplicationNode("PlaceholderName"); }

        /// <summary>Returns the placeholder password text</summary>
        static public String GetPlaceholderPassword() { return GetInnerTextForApplicationNode("PlaceholderPassword"); }

        /// <summary>Returns the placeholder search text</summary>
        static public String GetPlaceholderSearch() { return GetInnerTextForApplicationNode("PlaceholderSearch"); }

        /// <summary>Returns the reservation link</summary>
        static public String GetReservationUrl() { return GetInnerTextForApplicationNode("ReservationUrl"); }

        /// <summary>Returns the reservation email address</summary>
        static public String GetEmailReservation() { return GetInnerTextForApplicationNode("EmailReservation"); }

        /// <summary>Returns the reservation subject</summary>
        static public String GetReservationSubject() { return GetInnerTextForApplicationNode("ReservationSubject"); }

        /// <summary>Returns the reservation text</summary>
        static public String GetReservationText() { return GetInnerTextForApplicationNode("ReservationText"); }

        /// <summary>Returns the number of reservation days (as a string) before a concert. Defines the date when the reservation icon will appear </summary>
        static public String GetReservationDays() { return GetInnerTextForApplicationNode("ReservationDays"); }

        /// <summary>Returns the number of reservation days (as an integer) before a concert. Defines the date when the reservation icon will appear</summary>
        static public int GetReservationDaysInt()
        {
            int ret_number = -12345;

            String reservation_days_string = GetReservationDays();

            if (!XmlNodeValueIsSet(reservation_days_string))
                return -1;

            ret_number = JazzUtils.StringToInt(reservation_days_string);

            return ret_number;
        } // GetReservationDaysInt

        /// <summary>Returns the newsletter subject</summary>
        static public String GetNewsletterSubject() { return GetInnerTextForApplicationNode("NewsletterSubject"); }

        /// <summary>Returns the newsletter text</summary>
        static public String GetNewsletterText() { return GetInnerTextForApplicationNode("NewsletterText"); }

        /// <summary>Returns text string showing that there is more information if the user clicks on the string</summary>
        static public String GetMoreInfoText() { return GetInnerTextForApplicationNode("MoreInfoText"); }

        /// <summary>Returns the webmaster email address</summary>
        static public String GetEmailWebmaster() { return GetInnerTextForApplicationNode("EmailWebmaster"); }

        /// <summary>Returns the webmaster telephone number</summary>
        static public String GetTelephoneWebmaster() { return GetInnerTextForApplicationNode("TelephoneWebmaster"); }

        /// <summary>Returns the about us header</summary>
        static public String GetAboutUsHeader() { return GetInnerTextForApplicationNode("AboutUsHeader"); }

        /// <summary>Returns the about us text paragraph one</summary>
        static public String GetAboutUsOne() { return GetInnerTextForApplicationNode("AboutUsOne"); }

        /// <summary>Returns the about us text paragraph two</summary>
        static public String GetAboutUsTwo() { return GetInnerTextForApplicationNode("AboutUsTwo"); }

        /// <summary>Returns the about us text paragraph three</summary>
        static public String GetAboutUsThree() { return GetInnerTextForApplicationNode("AboutUsThree"); }

        /// <summary>Returns the icon for the WWW link</summary>
        static public String GetIconWww() { return GetInnerTextForApplicationNode("IconWww"); }

        /// <summary>Returns the icon for the video link</summary>
        static public String GetIconVideo() { return GetInnerTextForApplicationNode("IconVideo"); }

        /// <summary>Returns the icon for the calendar link</summary>
        static public String GetIconCalendar() { return GetInnerTextForApplicationNode("IconCalendar"); }

        /// <summary>Returns the icon for the calendar link</summary>
        static public String GetIconJazzLiveAarau() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconJazzLiveAarau"); }

        /// <summary>Returns the icon for gallery one</summary>
        static public String GetIconGalleryOne() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconGalleryOne"); }

        /// <summary>Returns the icon for gallery two</summary>
        static public String GetIconGalleryTwo() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconGalleryTwo"); }

        /// <summary>Returns the JAZZ live AARAU text icon</summary>
        static public String GetIconTextJazzLiveAarau() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconTextJazzLiveAarau"); }

        /// <summary>Returns the contact icon</summary>
        static public String GetIconContact() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconContact"); }

        /// <summary>Returns the help icon</summary>
        static public String GetIconHelp() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconHelp"); }

        /// <summary>Returns the login icon</summary>
        static public String GetIconLogin() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconLogin"); }

        /// <summary>Returns the next icon</summary>
        static public String GetIconNext() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconNext"); }

        /// <summary>Returns the previous icon</summary>
        static public String GetIconPrevious() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconPrevious"); }

        /// <summary>Returns the search icon</summary>
        static public String GetIconSearch() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconSearch"); }

        /// <summary>Returns the settings icon</summary>
        static public String GetIconSettings() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconSettings"); }

        /// <summary>Returns the statistics icon</summary>
        static public String GetIconStatistics() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconStatistics"); }

        /// <summary>Returns the tools icon</summary>
        static public String GetIconTools() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconTools"); }

        /// <summary>Returns the wait left icon corresponding to the settings icon</summary>
        static public String GetIconWaitLeft() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconWaitLeft"); }

        /// <summary>Returns the wait mid icon corresponding to the statistics icon</summary>
        static public String GetIconWaitMid() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconWaitMid"); }

        /// <summary>Returns the wait right icon corresponding to the search icon</summary>
        static public String GetIconWaitRight() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconWaitRight"); }

        /// <summary>Returns the checked icon</summary>
        static public String GetIconChecked() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconChecked"); }

        /// <summary>Returns the unchecked icon</summary>
        static public String GetIconUnchecked() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconUnchecked"); }

        /// <summary>Returns the checked musician icon</summary>
        static public String GetIconCheckedMusician() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconCheckedMusician"); }

        /// <summary>Returns the unchecked musician icon</summary>
        static public String GetIconUncheckedMusician() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconUncheckedMusician"); }

        /// <summary>Returns the checked concert icon</summary>
        static public String GetIconCheckedConcert() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconCheckedConcert"); }

        /// <summary>Returns the unchecked concert icon</summary>
        static public String GetIconUncheckedConcert() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconUncheckedConcert"); }

        /// <summary>Returns the checked text icon</summary>
        static public String GetIconCheckedText() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconCheckedText"); }

        /// <summary>Returns the unchecked text icon</summary>
        static public String GetIconUncheckedText() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconUncheckedText"); }

        /// <summary>Returns the zip icon</summary>
        static public String GetIconZip() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconZip"); }

        /// <summary>Returns the member default icon</summary>
        static public String GetIconMemberDefault() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconMemberDefault"); }

        /// <summary>Returns the big poster default icon</summary>
        static public String GetIconPosterBigDefault() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconPosterBigDefault"); }

        /// <summary>Returns the reservation icon</summary>
        static public String GetIconReservation() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconReservation"); }

        /// <summary>Returns the poster default icon</summary>
        static public String GetIconPosterDefault() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("IconPosterDefault"); }

        /// <summary>Returns the flag telling if the icons shall be retrieved from the server for the Android version of the app</summary>
        static public String GetIconsFromServerAndroid() { return GetInnerTextForApplicationNode("IconsFromServerAndroid"); }

        /// <summary>Returns the poster with sponsors</summary>
        static public String GetSponsorsPoster() { return GetWebSiteUrl() + GetInnerTextForApplicationNode("SponsorsPoster"); }

        /// <summary>Returns caption for program button</summary>
        static public String GetCaptionProgram() { return GetInnerTextForApplicationNode("CaptionProgram"); }

        /// <summary>Returns caption for summer break button</summary>
        static public String GetCaptionProgramBreak() { return GetInnerTextForApplicationNode("CaptionProgramBreak"); }

        /// <summary>Returns error message (caption) for the program button when program not can be published</summary>
        static public String GetCaptionProgramNotPublished() { return GetInnerTextForApplicationNode("CaptionProgramNotPublished"); }

        /// <summary>Returns error message (caption) for the program button when the XML file is missing for the program</summary>
        static public String GetCaptionProgramNoData() { return GetInnerTextForApplicationNode("CaptionProgramNoData"); }

        /// <summary>Returns caption for premises button</summary>
        static public String GetCaptionCaptionPremises() { return GetInnerTextForApplicationNode("CaptionPremises"); }

        /// <summary>Returns caption for contact button</summary>
        static public String GetCaptionContact() { return GetInnerTextForApplicationNode("CaptionContact"); }

        /// <summary>Returns caption for the about us button</summary>
        static public String GetCaptionAbout() { return GetInnerTextForApplicationNode("CaptionAbout"); }

        /// <summary>Returns caption why</summary>
        static public String GetCaptionWhy() { return GetInnerTextForApplicationNode("CaptionWhy"); }

        /// <summary>Returns caption for additional text about a concert</summary>
        static public String GetCaptionAdditionalText() { return GetInnerTextForApplicationNode("CaptionAdditionalText"); }

        /// <summary>Returns caption tasks</summary>
        static public String GetCaptionTasks() { return GetInnerTextForApplicationNode("CaptionTasks"); }

        /// <summary>Returns caption tasks short version</summary>
        static public String GetCaptionTasksShort() { return GetInnerTextForApplicationNode("CaptionTasksShort"); }

        /// <summary>Returns caption members</summary>
        static public String GetCaptionMembers() { return GetInnerTextForApplicationNode("CaptionMembers"); }

        /// <summary>Returns caption member</summary>
        static public String GetCaptionMember() { return GetInnerTextForApplicationNode("CaptionMember"); }

        /// <summary>Returns caption musician info</summary>
        static public String GetCaptionMusicianInfo() { return GetInnerTextForApplicationNode("CaptionMusicianInfo"); }

        /// <summary>Returns caption members only</summary>
        static public String GetCaptionMembersOnly() { return GetInnerTextForApplicationNode("CaptionMembersOnly"); }

        /// <summary>Returns caption musician contacts</summary>
        static public String GetCaptionMusicianContacts() { return GetInnerTextForApplicationNode("CaptionMusicianContacts"); }

        /// <summary>Returns caption for JAZZ live AARAU member login</summary>
        static public String GetCaptionLoginVorstand() { return GetInnerTextForApplicationNode("CaptionLoginVorstand"); }

        /// <summary>Returns caption for JAZZ live AARAU musician login</summary>
        static public String GetCaptionLoginMusician() { return GetInnerTextForApplicationNode("CaptionLoginMusician"); }

        /// <summary>Returns caption for JAZZ live AARAU member login button</summary>
        static public String GetCaptionLoginButton() { return GetInnerTextForApplicationNode("CaptionLoginButton"); }

        /// <summary>Returns caption for JAZZ live AARAU member logout button</summary>
        static public String GetCaptionLogoutButton() { return GetInnerTextForApplicationNode("CaptionLogoutButton"); }

        /// <summary>Returns caption for JAZZ live AARAU member login name</summary>
        static public String GetCaptionLoginName() { return GetInnerTextForApplicationNode("CaptionLoginName"); }

        /// <summary>Returns caption for JAZZ live AARAU member login password</summary>
        static public String GetCaptionLoginPassword() { return GetInnerTextForApplicationNode("CaptionLoginPassword"); }

        /// <summary>Returns caption for tools</summary>
        static public String GetCaptionTools() { return GetInnerTextForApplicationNode("CaptionTools"); }

        /// <summary>Returns caption for help</summary>
        static public String GetCaptionHelp() { return GetInnerTextForApplicationNode("CaptionHelp"); }

        /// <summary>Returns caption musician documents for a request</summary>
        static public String GetCaptionMusicianDocumentsRequest() { return GetInnerTextForApplicationNode("CaptionMusicianDocumentsRequest"); }

        /// <summary>Returns caption musician documents for a contract</summary>
        static public String GetCaptionMusicianDocumentsContract() { return GetInnerTextForApplicationNode("CaptionMusicianDocumentsContract"); }

        /// <summary>Returns caption musician information for a concert</summary>
        static public String GetCaptionMusicianConcertInfo() { return GetInnerTextForApplicationNode("CaptionMusicianConcertInfo"); }

        /// <summary>Returns caption for the unload address</summary>
        static public String GetCaptionUnloadAddress() { return GetInnerTextForApplicationNode("CaptionUnloadAddress"); }

        /// <summary>Returns caption for the recommended parking place one</summary>
        static public String GetCaptionParkingOne() { return GetInnerTextForApplicationNode("CaptionParkingOne"); }

        /// <summary>Returns caption for the recommended parking place two</summary>
        static public String GetCaptionParkingTwo() { return GetInnerTextForApplicationNode("CaptionParkingTwo"); }

        /// <summary>Returns caption for user help</summary>
        static public String GetCaptionHelpUser() { return GetInnerTextForApplicationNode("CaptionHelpUser"); }

        /// <summary>Returns caption for current season help</summary>
        static public String GetCaptionHelpCurrent() { return GetInnerTextForApplicationNode("CaptionHelpCurrent"); }

        /// <summary>Returns caption for XML help</summary>
        static public String GetCaptionHelpXml() { return GetInnerTextForApplicationNode("CaptionHelpXml"); }

        /// <summary>Returns caption for XML check</summary>
        static public String GetCaptionCheckXml() { return GetInnerTextForApplicationNode("CaptionCheckXml"); }

        /// <summary>Returns caption for photos help</summary>
        static public String GetCaptionHelpPhoto() { return GetInnerTextForApplicationNode("CaptionHelpPhoto"); }

        /// <summary>Returns caption for previous seasons help</summary>
        static public String GetCaptionHelpHistory() { return GetInnerTextForApplicationNode("CaptionHelpHistory"); }

        /// <summary>Returns caption for login help</summary>
        static public String GetCaptionHelpLogin() { return GetInnerTextForApplicationNode("CaptionHelpLogin"); }

        /// <summary>Returns caption for login version</summary>
        static public String GetCaptionHelpVersion() { return GetInnerTextForApplicationNode("CaptionHelpVersion"); }

        /// <summary>Returns caption for Intranet</summary>
        static public String GetCaptionIntranet() { return GetInnerTextForApplicationNode("CaptionIntranet"); }

        /// <summary>Returns caption for season</summary>
        static public String GetCaptionSeason() { return GetInnerTextForApplicationNode("CaptionSeason"); }

        /// <summary>Returns caption for sponsors</summary>
        static public String GetCaptionSponsors() { return GetInnerTextForApplicationNode("CaptionSponsors"); }

        /// <summary>Returns caption for supporters</summary>
        static public String GetCaptionSupporters() { return GetInnerTextForApplicationNode("CaptionSupporters"); }

        /// <summary>Returns the title for the main page</summary>
        static public String GetTitleMain() { return GetInnerTextForApplicationNode("TitleMain"); }

        /// <summary>Returns the title for the concert page</summary>
        static public String GetTitleConcert() { return GetInnerTextForApplicationNode("TitleConcert"); }

        /// <summary>Returns the title for the login page</summary>
        static public String GetTitleLogin() { return GetInnerTextForApplicationNode("TitleLogin"); }

        /// <summary>Returns the title for the musician page</summary>
        static public String GetTitleMusician() { return GetInnerTextForApplicationNode("TitleMusician"); }

        /// <summary>Returns the title for the poster page</summary>
        static public String GetTitlePoster() { return GetInnerTextForApplicationNode("TitlePoster"); }

        /// <summary>Returns the title for the (statistics calculation) result page</summary>
        static public String GetTitleResult() { return GetInnerTextForApplicationNode("TitleResult"); }

        /// <summary>Returns the title for the search page</summary>
        static public String GetTitleSearch() { return GetInnerTextForApplicationNode("TitleSearch"); }

        /// <summary>Returns caption for the musician frequency distribution diagram</summary>
        static public String GetCaptionDistributionDiagram() { return GetInnerTextForApplicationNode("CaptionDistributionDiagram"); }

        /// <summary>Returns help text for the musician frequency distribution diagram</summary>
        static public String GetHelpDistributionDiagram() { return GetInnerTextForApplicationNode("HelpDistributionDiagram"); }

        /// <summary>Returns text for the supporter page,  paragraph 1</summary>
        static public String GetSupporters1() { return GetInnerTextForApplicationNode("Supporters1"); }

        /// <summary>Returns text for the supporter page,  paragraph 2</summary>
        static public String GetSupporters2() { return GetInnerTextForApplicationNode("Supporters2"); }

        /// <summary>Returns text for the supporter page,  paragraph 3</summary>
        static public String GetSupporters3() { return GetInnerTextForApplicationNode("Supporters3"); }

        /// <summary>Returns text for the supporter page,  paragraph 4</summary>
        static public String GetSupporters4() { return GetInnerTextForApplicationNode("Supporters4"); }

        /// <summary>Returns caption for user  help paragraph 1</summary>
        static public String GetHelpUser1() { return GetInnerTextForApplicationNode("HelpUser1"); }

        /// <summary>Returns caption for user  help paragraph 2</summary>
        static public String GetHelpUser2() { return GetInnerTextForApplicationNode("HelpUser2"); }

        /// <summary>Returns caption for user  help paragraph 3</summary>
        static public String GetHelpUser3() { return GetInnerTextForApplicationNode("HelpUser3"); }

        /// <summary>Returns caption for user  help paragraph 4</summary>
        static public String GetHelpUser4() { return GetInnerTextForApplicationNode("HelpUser4"); }

        /// <summary>Returns caption for XML  help paragraph 1</summary>
        static public String GetHelpXml1() { return GetInnerTextForApplicationNode("HelpXml1"); }

        /// <summary>Returns caption for XML  help paragraph 2</summary>
        static public String GetHelpXml2() { return GetInnerTextForApplicationNode("HelpXml2"); }

        /// <summary>Returns caption for XML  help paragraph 3</summary>
        static public String GetHelpXml3() { return GetInnerTextForApplicationNode("HelpXml3"); }

        /// <summary>Returns caption for XML  help paragraph 4</summary>
        static public String GetHelpXml4() { return GetInnerTextForApplicationNode("HelpXml4"); }

        /// <summary>Returns caption for photo  help paragraph 1</summary>
        static public String GetHelpPhoto1() { return GetInnerTextForApplicationNode("HelpPhoto1"); }

        /// <summary>Returns caption for photo  help paragraph 2</summary>
        static public String GetHelpPhoto2() { return GetInnerTextForApplicationNode("HelpPhoto2"); }

        /// <summary>Returns caption for photo  help paragraph 3</summary>
        static public String GetHelpPhoto3() { return GetInnerTextForApplicationNode("HelpPhoto3"); }

        /// <summary>Returns caption for photo  help paragraph 4</summary>
        static public String GetHelpPhoto4() { return GetInnerTextForApplicationNode("HelpPhoto4"); }

        /// <summary>Returns caption for history  help paragraph 1</summary>
        static public String GetHelpHistory1() { return GetInnerTextForApplicationNode("HelpHistory1"); }

        /// <summary>Returns caption for history  help paragraph 2</summary>
        static public String GetHelpHistory2() { return GetInnerTextForApplicationNode("HelpHistory2"); }

        /// <summary>Returns caption for history  help paragraph 3</summary>
        static public String GetHelpHistory3() { return GetInnerTextForApplicationNode("HelpHistory3"); }

        /// <summary>Returns caption for history  help paragraph 4</summary>
        static public String GetHelpHistory4() { return GetInnerTextForApplicationNode("HelpHistory4"); }

        /// <summary>Returns caption for login  help paragraph 1</summary>
        static public String GetHelpLogin1() { return GetInnerTextForApplicationNode("HelpLogin1"); }

        /// <summary>Returns caption for login  help paragraph 2</summary>
        static public String GetHelpLogin2() { return GetInnerTextForApplicationNode("HelpLogin2"); }

        /// <summary>Returns caption for login  help paragraph 3</summary>
        static public String GetHelpLogin3() { return GetInnerTextForApplicationNode("HelpLogin3"); }

        /// <summary>Returns caption for login  help paragraph 4</summary>
        static public String GetHelpLogin4() { return GetInnerTextForApplicationNode("HelpLogin4"); }

        /// <summary>Returns the version help paragraph 1 for Android</summary>
        static public String GetHelpVersion1() { return GetInnerTextForApplicationNode("HelpVersion1"); }

        /// <summary>Returns the version help paragraph 2 for Android</summary>
        static public String GetHelpVersion2() { return GetInnerTextForApplicationNode("HelpVersion2"); }

        /// <summary>Returns the version help paragraph 3 for Android</summary>
        static public String GetHelpVersion3() { return GetInnerTextForApplicationNode("HelpVersion3"); }

        /// <summary>Returns the version help paragraph 4 for Android</summary>
        static public String GetHelpVersion4() { return GetInnerTextForApplicationNode("HelpVersion4"); }

        /// <summary>Returns the version help paragraph 1 for iOS</summary>
        static public String GetHelpVersionIos1() { return GetInnerTextForApplicationNode("HelpVersionIos1"); }

        /// <summary>Returns the version help paragraph 2 for iOS</summary>
        static public String GetHelpVersionIos2() { return GetInnerTextForApplicationNode("HelpVersionIos2"); }

        /// <summary>Returns the version help paragraph 3 for iOS</summary>
        static public String GetHelpVersionIos3() { return GetInnerTextForApplicationNode("HelpVersionIos3"); }

        /// <summary>Returns the version help paragraph 4 for iOS</summary>
        static public String GetHelpVersionIos4() { return GetInnerTextForApplicationNode("HelpVersionIos4"); }

        /// <summary>Returns the iOS version. Hopefully temporary until a JazzUtil function has been implemented</summary>
        static public String GetIosVersion() { return GetInnerTextForApplicationNode("IosVersion"); }

        /// <summary>Returns the Android version. Hopefully temporary until a JazzUtil function has been implemented</summary>
        static public String GetAndroidVersion() { return GetInnerTextForApplicationNode("AndroidVersion"); }

        /// <summary>Returns subject for an E-Mail requesting photos</summary>
        static public String GetPhotoEmailSubject() { return GetInnerTextForApplicationNode("PhotoEmailSubject"); }

        /// <summary>Returns body (text) for an E-Mail requesting photos</summary>
        static public String GetPhotoEmailBody() { return GetInnerTextForApplicationNode("PhotoEmailBody"); }

        /// <summary>Returns the E-Mail for the gallery one photographer </summary>
        static public String GetPhotographerOneEmail() { return GetInnerTextForApplicationNode("PhotographerOneEmail"); }

        /// <summary>Returns the E-Mail for the gallery two photographer </summary>
        static public String GetPhotographerTwoEmail() { return GetInnerTextForApplicationNode("PhotographerTwoEmail"); }

        /// <summary>Returns error message for JAZZ live AARAU member login failure</summary>
        static public String GetLoginFailed() { return GetInnerTextForApplicationNode("LoginFailed"); }

        /// <summary>Returns error message for JAZZ live AARAU musician login failure</summary>
        static public String GetLoginMusicianFailed() { return GetInnerTextForApplicationNode("LoginMusicianFailed"); }

        /// <summary>Returns error message for JAZZ live AARAU musician login not possible</summary>
        static public String GetLoginMusicianNotPossible() { return GetInnerTextForApplicationNode("LoginMusicianNotPossible"); }

        /// <summary>Returns menu string for 'Select season'</summary>
        static public String GetMenuSelectSeason() { return GetInnerTextForApplicationNode("MenuSelectSeason"); }

        /// <summary>Returns menu string for 'Select concert'</summary>
        static public String GetMenuSelectConcert() { return GetInnerTextForApplicationNode("MenuSelectConcert"); }

        /// <summary>Returns the error message that Internet is unavailable</summary>
        static public String GetErrorInternet() { return GetInnerTextForApplicationNode("ErrorInternet"); }

        /// <summary>Returns the message that the application will close</summary>
        static public String GetMsgCloseApp() { return GetInnerTextForApplicationNode("MsgCloseApp"); }

        /// <summary>Returns the message that the login name is missing</summary>
        static public String GetMsgLoginNameMissing() { return GetInnerTextForApplicationNode("MsgLoginNameMissing"); }

        /// <summary>Returns the message that the login password is missing</summary>
        static public String GetMsgLoginPasswordMissing() { return GetInnerTextForApplicationNode("MsgLoginPasswordMissing"); }

        /// <summary>Returns the message that the login only is possible for the current season</summary>
        static public String GetMsgLoginOnlySeason() { return GetInnerTextForApplicationNode("MsgLoginOnlySeason"); }

        /// <summary>Returns the message that the application will close</summary>
        static public String GetMsgNotYetImpemented() { return GetInnerTextForApplicationNode("MsgNotYetImpemented"); }

        /// <summary>Returns the message that XML files for previous seasons will be downloaded</summary>
        static public String GetMsgDownloadXmlFiles() { return GetInnerTextForApplicationNode("MsgDownloadXmlFiles"); }

        /// <summary>Returns the caption for search option musician </summary>
        static public String GetSearchOptionMusicians() { return GetInnerTextForApplicationNode("SearchOptionMusicians"); }

        /// <summary>Returns the caption for search option bands </summary>
        static public String GetSearchOptionBands() { return GetInnerTextForApplicationNode("SearchOptionBands"); }

        /// <summary>Returns the caption for search option texts </summary>
        static public String GetSearchOptionTexts() { return GetInnerTextForApplicationNode("SearchOptionTexts"); }

        /// <summary>Returns the search message  "no hits" </summary>
        static public String GetSearchNoHits() { return GetInnerTextForApplicationNode("SearchNoHits"); }

        /// <summary>Returns the search message  "number of hits" </summary>
        static public String GetSearchNumberOfHits() { return GetInnerTextForApplicationNode("SearchNumberOfHits"); }

        /// <summary>Returns the message  that the search string is empty </summary>
        static public String GetSearchStringEmpty() { return GetInnerTextForApplicationNode("SearchStringEmpty"); }

        /// <summary>Returns the message  that no search alternative (option) is selected </summary>
        static public String GetSearchAlternativeNotSet() { return GetInnerTextForApplicationNode("SearchAlternativeNotSet"); }

        /// <summary>Returns caption for the JAZZ live AARAU contact person at a concert</summary>
        static public String GetCaptionContactPersonConcert() { return GetInnerTextForApplicationNode("CaptionContactPersonConcert"); }

        /// <summary>Returns member number (id) as a string for a JAZZ live AARAU contact person at a concert</summary>
        static public String GetContactConcertMemberNumber() { return GetInnerTextForApplicationNode("ContactConcertMemberNumber"); }

        /// <summary>Returns member number (id) as an int for a JAZZ live AARAU contact person at a concert</summary>
        static public int GetContactConcertMemberNumberInt()
        {
            int ret_number = -12345;

            String contact_concert_member_number_string = GetContactConcertMemberNumber();

            if (!XmlNodeValueIsSet(contact_concert_member_number_string))
                return -1;

            ret_number = JazzUtils.StringToInt(contact_concert_member_number_string);

            return ret_number;
        } // GetContactConcertMemberNumberInt

        /// <summary>Returns the telephone number to the contact person at the concert. This number should be to the JAZZ live AARAU mobile telephone</summary>
        static public String GetContactConcertTelephone() { return GetInnerTextForApplicationNode("ContactConcertTelephone"); }

        /// <summary>Returns the E-mailaddress to the contact person at the concert. The JAZZ live AARAU mobile telephone should read the E-Mails to this address</summary>
        static public String GetContactConcertEmail() { return GetInnerTextForApplicationNode("ContactConcertEmail"); }

        /// <summary>Returns message that an event has been added to the calendar</summary>
        static public String GetCalendarEventAdded() { return GetInnerTextForApplicationNode("CalendarEventAdded"); }

        /// <summary>Returns the caption for statistics selection button and menu </summary>
        static public String GetCaptionStatistics() { return GetInnerTextForApplicationNode("CaptionStatistics"); }

        /// <summary>Returns the selection statistics string for musician </summary>
        static public String GetStatisticsMusician() { return GetInnerTextForApplicationNode("StatisticsMusician"); }

        /// <summary>Returns the selection statistics string for premises </summary>
        static public String GetStatisticsPremises() { return GetInnerTextForApplicationNode("StatisticsPremises"); }

        /// <summary>Returns the selection statistics string for bands </summary>
        static public String GetStatisticsBand() { return GetInnerTextForApplicationNode("StatisticsBand"); }

        /// <summary>Returns the selection statistics string for members </summary>
        static public String GetStatisticsMembers() { return GetInnerTextForApplicationNode("StatisticsMembers"); }

        /// <summary>Returns the selection statistics string for birthyear </summary>
        static public String GetStatisticsBirthyear() { return GetInnerTextForApplicationNode("StatisticsBirthyear"); }

        /// <summary>Returns the selection statistics string for gender </summary>
        static public String GetStatisticsGender() { return GetInnerTextForApplicationNode("StatisticsGender"); }

        /// <summary>Returns the statistics string concerts</summary>
        static public String GetStatisticsConcerts() { return GetInnerTextForApplicationNode("StatisticsConcerts"); }

        /// <summary>Returns the statistics string years</summary>
        static public String GetStatisticsYears() { return GetInnerTextForApplicationNode("StatisticsYears"); }

        /// <summary>Returns the caption string for total number of musicians </summary>
        static public String GetNumberMusicians() { return GetInnerTextForApplicationNode("NumberMusicians"); }

        /// <summary>Returns the caption string for total number of female musicians </summary>
        static public String GetNumberFemale() { return GetInnerTextForApplicationNode("NumberFemale"); }

        /// <summary>Returns the caption string for total number of premises </summary>
        static public String GetNumberPremises() { return GetInnerTextForApplicationNode("NumberPremises"); }

        /// <summary>Returns the caption string for total number of bands </summary>
        static public String GetNumberBands() { return GetInnerTextForApplicationNode("NumberBands"); }

        /// <summary>Returns the caption string for total number of bands </summary>
        static public String GetNumberMembers() { return GetInnerTextForApplicationNode("NumberMembers"); }

        #endregion // Application XML document: First element

        #region Application XML document: Member data

        /// <summary>Returns the member name</summary>
        static public String GetMemberName(int i_member) { return GetMemberInnerText(i_member, "Name"); }

        /// <summary>Returns the member family name</summary>
        static public String GetMemberFamilyName(int i_member) { return GetMemberInnerText(i_member, "FamilyName"); }

        /// <summary>Returns the member Email address</summary>
        static public String GetMemberEmail(int i_member) { return GetMemberInnerText(i_member, "EmailAddress"); }

        /// <summary>Returns the member telephone number, mobile</summary>
        static public String GetMemberTelephone(int i_member) { return GetMemberInnerText(i_member, "Telephone"); }

        /// <summary>Returns the member telephone number, fix</summary>
        static public String GetTelephoneFix(int i_member) { return GetMemberInnerText(i_member, "TelephoneFix"); }

        /// <summary>Returns the member street</summary>
        static public String GetMemberStreet(int i_member) { return GetMemberInnerText(i_member, "Street"); }

        /// <summary>Returns the city</summary>
        static public String GetMemberCity(int i_member) { return GetMemberInnerText(i_member, "City"); }

        /// <summary>Returns the member post code</summary>
        static public String GetMemberPostCode(int i_member) { return GetMemberInnerText(i_member, "PostCode"); }

        /// <summary>Returns the member password</summary>
        static public String GetMemberPassword(int i_member) { return GetMemberInnerText(i_member, "Password"); }

        /// <summary>Returns the member Vorstand flag</summary>
        static public String GetMemberVorstandFlag(int i_member) { return GetMemberInnerText(i_member, "Vorstand"); }

        /// <summary>Returns the member mid size photo</summary>
        static public String GetMemberPhotoMidSize(int i_member) { return GetMemberInnerText(i_member, "PhotoMidSize"); }

        /// <summary>Returns the member small size photo</summary>
        static public String GetMemberPhotoSmallSize(int i_member) { return GetMemberInnerText(i_member, "PhotoSmallSize"); }

        /// <summary>Returns the member tasks</summary>
        static public String GetMemberTasks(int i_member) { return GetMemberInnerText(i_member, "Tasks"); }

        /// <summary>Returns the member main tasks </summary>
        static public String GetMemberTasksShort(int i_member) { return GetMemberInnerText(i_member, "TasksShort"); }

        /// <summary>Returns the caption for member tasks short version </summary>
        static public String GetCaptionTasksShort(int i_member) { return GetMemberInnerText(i_member, "CaptionTasksShort"); }

        /// <summary>Returns the member why (member)</summary>
        static public String GetMemberWhy(int i_member) { return GetMemberInnerText(i_member, "Why"); }

        /// <summary>Returns the member start year (member)</summary>
        static public String GetMemberStartYear(int i_member) { return GetMemberInnerText(i_member, "StartYear"); }

        /// <summary>Returns the member end year (member)</summary>
        static public String GetMemberEndYear(int i_member) { return GetMemberInnerText(i_member, "EndYear"); }

        /// <summary>Returns the member number (id) as string</summary>
        static public String GetMemberNumberString(int i_member) { return GetMemberInnerText(i_member, "Number"); }

        /// <summary>Returns the member Email private address.</summary>
        static public String GetMemberEmailPrivate(int i_member) { return GetMemberInnerText(i_member, "EmailPrivate"); }

        /// <summary>Returns the member fix telephone number.</summary>
        static public String GetMemberTelephoneFix(int i_member) { return GetMemberInnerText(i_member, "TelephoneFix"); }

        /// <summary>Returns the array member number for a given id (number)</summary>
        static public int GetMemberArrayNumber(int i_member_id)
        {
            int ret_array_number = -12345;

            int number_of_members = GetNumberOfMembers();

            for (int i_member = 1; i_member <= number_of_members; i_member++)
            {
                int id_number = GetMemberNumber(i_member);

                if (id_number == i_member_id)
                {
                    ret_array_number = i_member;
                    break;
                }
            }

            return ret_array_number;
        } // GetMemberArrayNumber


        /// <summary>Returns the member number (id)</summary>
        static public int GetMemberNumber(int i_member)
        {
            int ret_number = -12345;

            String member_number_string = GetMemberNumberString(i_member);

            if (!XmlNodeValueIsSet(member_number_string))
                return -1;

            ret_number = JazzUtils.StringToInt(member_number_string);

            return ret_number;
        } // GetMemberNumber


        /// <summary>Returns true if the member is in Vorstand</summary>
        static public Boolean MemberIsInVorstand(int i_member)
        {
            Boolean ret_in_vorstand = false;

            String vorstand_flag = GetMemberVorstandFlag(i_member);

            if ("true" == vorstand_flag)
                ret_in_vorstand = true;

            return ret_in_vorstand;
        } // MemberIsInVorstand


        /// <summary>Returns true if the member tag name exists</summary>
        static public Boolean MemberTagNameExists(String i_tag_name)
        {
            if (i_tag_name == "Name")
                return true;
            else if (i_tag_name == "FamilyName")
                return true;
            else if (i_tag_name == "EmailAddress")
                return true;
            else if (i_tag_name == "Telephone")
                return true;
            else if (i_tag_name == "Street")
                return true;
            else if (i_tag_name == "City")
                return true;
            else if (i_tag_name == "PostCode")
                return true;
            else if (i_tag_name == "Password")
                return true;
            else if (i_tag_name == "Vorstand")
                return true;
            else
            {
                return false;
            }
        } // MemberTagNameExists

        /// <summary> Returns a string array of members property defined by the tag name</summary>
        public static String[] GetMemberPropertyArray(String i_tag_name)
        {
            String[] ret_strings = null;

            if (!MemberTagNameExists(i_tag_name))
                return ret_strings;

            int number_of_members = GetNumberOfMembers();

            ret_strings = new String[number_of_members];

            for (int i_member = 1; i_member <= number_of_members; i_member++)
            {
                // String member_name = GetMemberName(i_member);

                String property = GetMemberInnerText(i_member, i_tag_name);

                ret_strings[i_member - 1] = property;
            }

            return ret_strings;
        } // GetMemberPropertyArray

        #endregion // Application XML document: Member data

        #region Concert XML documents: First element
        /// <summary>Returns the autumn year</summary>
        static public String GetYearAutum() { return GetInnerTextForConcertsNode("YearAutum"); }

        /// <summary>Returns the spring year</summary>
        static public String GetYearSpring() { return GetInnerTextForConcertsNode("YearSpring"); }

        /// <summary>Returns the autum year as integer</summary>
        static public int GetYearAutumnInt()
        {
            int ret_year = -1;

            String year_autum_str = GetYearAutum();

            ret_year = JazzUtils.StringToInt(year_autum_str);

            return ret_year;
        }

        /// <summary>Returns the spring year as integer</summary>
        static public int GetYearSpringInt()
        {
            int ret_year = -1;

            String year_spring_str = GetYearSpring();

            ret_year = JazzUtils.StringToInt(year_spring_str);

            return ret_year;
        }

        /// <summary>Returns the flag TRUE if season program may be published </summary>
        static public String GetPublishProgram() { return GetInnerTextForConcertsNode("PublishProgram"); }

        #endregion // Concert XML documents: First element


        #region Concert XML documents: Concert data

        /// <summary>Returns the place of the concert
        /// <para>For the statistic function is Salmen added to Spaghetti Factory</para>
        /// </summary>
        static public String GetPlace(int i_concert)
        {
            String ret_place_str = GetInnerTextForNode(i_concert, "Place");

            if (ret_place_str.Equals("Spaghetti Factory"))
            {
                ret_place_str = ret_place_str + " Salmen";
            }

            return ret_place_str;

        } // GetPlace

        /// <summary>Returns the street of the concert</summary>
        static public String GetStreet(int i_concert) { return GetInnerTextForNode(i_concert, "Street"); }

        /// <summary>Returns the city of the concert</summary>
        static public String GetCity(int i_concert) { return GetInnerTextForNode(i_concert, "City"); }

        /// <summary>Returns the address to the place of the concert</summary>
        static public String GetAddress(int i_concert) { return GetPlace(i_concert) + ", " + GetStreet(i_concert) + ", " + GetCity(i_concert); }

        /// <summary>Returns the flag telling if the concert is cancelled. Value TRUE or FALSE</summary>
        static public String GetConcertCancelled(int i_concert) 
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return "FALSE";
            }

            return GetInnerTextForNode(i_concert, "ConcertCancelled");

        } // GetConcertCancelled

        /// <summary>Returns the name of the concert</summary>
        static public String GetBandName(int i_concert) { return GetInnerTextForNode(i_concert, "BandName"); }

        /// <summary>Returns the short text for the concert</summary>
        static public String GetShortText(int i_concert) { return GetInnerTextForNode(i_concert, "ShortText"); }

        /// <summary>Returns the additional text for the concert</summary>
        static public String GetAdditionalText(int i_concert) { return GetInnerTextForNode(i_concert, "AdditionalText"); }

        /// <summary>Returns the additional text header for the concert</summary>
        static public String GetLabelAdditionalText(int i_concert)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return m_undefined_node_value;
            }

            return GetInnerTextForNode(i_concert, "LabelAdditionalText");

        } // GetLabelAdditionalText

        /// <summary>Returns the flyer text header for the concert</summary>
        static public String GetLabelFlyerText(int i_concert)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return m_undefined_node_value;
            }
            return GetInnerTextForNode(i_concert, "LabelFlyerText");
        } // GetLabelFlyerText

        /// <summary>Returns the flyer text for the concert</summary>
        static public String GetFlyerText(int i_concert)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return m_undefined_node_value;
            }
            return GetInnerTextForNode(i_concert, "FlyerText");
        } // GetFlyerText

        /// <summary>Returns the flag telling if the free flyer text may be published on the homepage. Value TRUE or FALSE</summary>
        static public String GetFlyerTextHomepagePublish(int i_concert) 
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return "TRUE";
            }

            return GetInnerTextForNode(i_concert, "FlyerTextHomepagePublish"); 
        }

        /// <summary>Returns the sound sample URL for the concert</summary>
        static public String GetSoundSample(int i_concert) { return GetInnerTextForNode(i_concert, "SoundSample"); }

        /// <summary>Returns the sound sample URL QR code for the concert</summary>
        static public String GetSoundSampleQrCode(int i_concert)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return "NotYetSetNodeValue";
            }

            return GetInnerTextForNode(i_concert, "SoundSampleQrCode");

        } // GetSoundSampleQrCode	

        /// <summary>Returns the photo gallery one URL for the concert</summary>
        static public String GetPhotoGalleryOne(int i_concert) { return GetInnerTextForNode(i_concert, "PhotoGalleryOne"); }

        /// <summary>Returns the photo gallery two URL for the concert</summary>
        static public String GetPhotoGalleryTwo(int i_concert) { return GetInnerTextForNode(i_concert, "PhotoGalleryTwo"); }

        /// <summary>Returns the photo gallery one ZIP URL for the concert</summary>
        static public String GetPhotoGalleryOneZip(int i_concert) { return GetInnerTextForNode(i_concert, "PhotoGalleryOneZip"); }

        /// <summary>Returns the photo gallery two ZIP URL for the concert</summary>
        static public String GetPhotoGalleryTwoZip(int i_concert) { return GetInnerTextForNode(i_concert, "PhotoGalleryTwoZip"); }

        /// <summary>Returns the musician login password</summary>
        static public String GetLoginPassword(int i_concert) { return GetInnerTextForNode(i_concert, "LoginPassword"); }

        /// <summary>Returns the band contact person</summary>
        static public String GetContactPerson(int i_concert) { return GetInnerTextForNode(i_concert, "ContactPerson"); }

        /// <summary>Returns the band contact email</summary>
        static public String GetContactEmail(int i_concert) { return GetInnerTextForNode(i_concert, "ContactEmail"); }

        /// <summary>Returns the band contact telephone</summary>
        static public String GetContactTelephone(int i_concert) { return GetInnerTextForNode(i_concert, "ContactTelephone"); }

        /// <summary>Returns the band contact street</summary>
        static public String GetContactStreet(int i_concert) { return GetInnerTextForNode(i_concert, "ContactStreet"); }

        /// <summary>Returns the band contact post code</summary>
        static public String GetContactPostCode(int i_concert) { return GetInnerTextForNode(i_concert, "ContactPostCode"); }

        /// <summary>Returns the band contact city</summary>
        static public String GetContactCity(int i_concert) { return GetInnerTextForNode(i_concert, "ContactCity"); }

        /// <summary>Returns the band IBAN number</summary>
        static public String GetIbanNumber(int i_concert) { return GetInnerTextForNode(i_concert, "IbanNumber"); }

        /// <summary>Returns the band contact remark</summary>
        static public String GetContactRemark(int i_concert) { return GetInnerTextForNode(i_concert, "ContactRemark"); }

        /// <summary>Returns the band website URL for the concert</summary>
        static public String GetBandWebsite(int i_concert) { return GetInnerTextForNode(i_concert, "BandWebsite"); }

        /// <summary>Returns the band website URL QR code for the concert</summary>
        static public String GetBandWebsiteQrCode(int i_concert)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2020)
            {
                return "NotYetSetNodeValue";
            }

            return GetInnerTextForNode(i_concert, "BandWebsiteQrCode");

        } // GetBandWebsiteQrCode

        /// <summary>Returns the small size poster URL for the concert</summary>
        static public String GetPosterSmallSize(int i_concert) { return GetInnerTextForNode(i_concert, "PosterSmallSize"); }

        /// <summary>Returns the mid size poster URL for the concert</summary>
        static public String GetPosterMidSize(int i_concert) { return GetWebSiteUrl() + GetInnerTextForNode(i_concert, "PosterMidSize"); }

        /// <summary>Returns the day of the concert as string</summary>
        static public String GetDay(int i_concert) { return GetInnerTextForNode(i_concert, "Day"); }

        /// <summary>Returns the month of the concert as string</summary>
        static public String GetMonth(int i_concert) { return GetInnerTextForNode(i_concert, "Month"); }

        /// <summary>Returns the year of the concert as string</summary>
        static public String GetYear(int i_concert) { return GetInnerTextForNode(i_concert, "Year"); }

        /// <summary>Returns the start hour of the concert as string</summary>
        static public String GetStartHour(int i_concert) { return GetInnerTextForNode(i_concert, "TimeStartHour"); }

        /// <summary>Returns the start minute of the concert as string</summary>
        static public String GetStartMinute(int i_concert) { return GetInnerTextForNode(i_concert, "TimeStartMinute"); }

        /// <summary>Returns the end hour of the concert as string</summary>
        static public String GetEndHour(int i_concert) { return GetInnerTextForNode(i_concert, "TimeEndHour"); }

        /// <summary>Returns the end minute of the concert as string</summary>
        static public String GetEndMinute(int i_concert) { return GetInnerTextForNode(i_concert, "TimeEndMinute"); }



        #endregion // Application XML document: Member data

        #region Convert strings to int and boolean
        /// <summary>Returns the start minute of the concert as integer</summary>
        static public int GetStartMinuteInt(int i_concert)
        {
            String start_minute_str = GetStartMinute(i_concert);
            if (!XmlNodeValueIsSet(start_minute_str))
                return -12345;

            return JazzUtils.StringToInt(start_minute_str);
        }

        /// <summary>Returns the end hour of the concert as integer</summary>
        static public int GetEndHourInt(int i_concert)
        {
            String end_hour_str = GetEndHour(i_concert);
            if (!XmlNodeValueIsSet(end_hour_str))
                return -12345;

            return JazzUtils.StringToInt(end_hour_str);
        }

        /// <summary>Returns the end minute of the concert as integer</summary>
        static public int GetEndMinuteInt(int i_concert)
        {
            String end_minute_str = GetEndMinute(i_concert);
            if (!XmlNodeValueIsSet(end_minute_str))
                return -12345;

            return JazzUtils.StringToInt(end_minute_str);
        }

        /// <summary>Returns the day of the concert as integer</summary>
        static public int GetDayInt(int i_concert)
        {
            String day_str = GetDay(i_concert);
            if (!XmlNodeValueIsSet(day_str))
                return -12345;

            return JazzUtils.StringToInt(day_str);
        }

        /// <summary>Returns the start hour of the concert as integer</summary>
        static public int GetStartHourInt(int i_concert)
        {
            String start_hour_str = GetStartHour(i_concert);
            if (!XmlNodeValueIsSet(start_hour_str))
                return -12345;

            return JazzUtils.StringToInt(start_hour_str);
        }

        /// <summary>Returns the month of the concert as integer</summary>
        static public int GetMonthInt(int i_concert)
        {
            String month_str = GetMonth(i_concert);
            if (!XmlNodeValueIsSet(month_str))
                return -12345;

            return JazzUtils.StringToInt(month_str);
        }

        /// <summary>Returns the year of the concert as integer</summary>
        static public int GetYearInt(int i_concert)
        {
            String year_str = GetYear(i_concert);
            if (!XmlNodeValueIsSet(year_str))
                return -12345;

            return JazzUtils.StringToInt(year_str);
        }


        /// <summary>Returns true if the season programmay be published</summary>
        static public Boolean PublishProgram()
        {
            String publish_str = GetPublishProgram();

            if ("TRUE" == publish_str)
                return true;
            else
                return false;

        } // PublishProgram

        /// <summary>Returns true if the concert is cancelled</summary>
        static public Boolean ConcertCancelled(int i_concert)
        {
            String cancelled_str = GetConcertCancelled(i_concert);

            if ("TRUE" == cancelled_str)
                return true;
            else
                return false;

        } // ConcertCancelled



        #endregion // Convert strings to int and boolean

        #region Escape characters
        /// <summary>Characters in XML text that must be replaced by "&lt;" , "&gt;", etc.</summary>
        private static char[] m_xml_escape_chars = new char[5];

        /// <summary>Replace strings like "&lt;" , "&gt;", etc.</summary>
        private static string[] m_xml_escape_replace = new string[5];

        /// <summary>Number characters</summary>
        private static char[] m_numbers = new char[10];

        /// <summary>Initialization</summary>
        public static void EscapeNumberInit()
        {
            m_xml_escape_chars[0] = '<';
            m_xml_escape_chars[1] = '>';
            m_xml_escape_chars[2] = '&';
            m_xml_escape_chars[3] = '\"';
            m_xml_escape_chars[4] = '\'';


            m_xml_escape_replace[0] = @"&lt;";
            m_xml_escape_replace[1] = @"&gt;";
            m_xml_escape_replace[2] = @"&amp;";
            m_xml_escape_replace[3] = @"&quot;";
            m_xml_escape_replace[4] = @"&apos;";

            m_numbers[0] = '0';
            m_numbers[1] = '1';
            m_numbers[2] = '2';
            m_numbers[3] = '3';
            m_numbers[4] = '4';
            m_numbers[5] = '5';
            m_numbers[6] = '6';
            m_numbers[7] = '7';
            m_numbers[8] = '8';
            m_numbers[9] = '9';

        } // Init

        /// <summary>Modify an empty string that shall be written to an XML (XDocument) object</summary>
        public static string SetUndefinedValueForEmptyString(string i_string)
        {
            if (i_string.Trim().Length == 0)
            {
                return JazzXml.GetUndefinedNodeValue();
            }
            else
            {
                return i_string;
            }

        } // SetUndefinedValueForEmptyString

        /// <summary>Modify string that shall be written to an XML (XDocument) object</summary>
        public static string ModifyWriteXml(string i_string)
        {
            if (i_string.Trim().Length == 0)
                return JazzXml.GetUndefinedNodeValue();

            // https://www.roelvanlisdonk.nl/2009/09/23/you-should-use-system-security-securityelement-escape-in-c-to-escape-special-characters-in-xml-and-not-system-web-httputility-htmlencode/
            // Not implemented for UWP and Windows telephones string ret_string = System.Security.SecurityElement.Escape(i_string);

            string ret_string = ReplaceNotAllowedChars(i_string);

            return ret_string;

        } // ModifyWriteXml

        /// <summary>Modify string that comes from an XML (XDocument) object and that shall be displayed in a dialog</summary>
        public static string ModifyReadXml(string i_string)
        {
            if (!JazzXml.XmlNodeValueIsSet(i_string))
                return i_string; // The undefined value. The App requires this value. Would be a big job to change to empty string ...
            // return @"";

            // Exception SecurityElement securityElement = System.Security.SecurityElement.FromString(i_string);
            // Exception string ret_string = securityElement.Text;

            string ret_string = i_string;
            for (int index_escape_replace = 0; index_escape_replace < m_xml_escape_replace.Length; index_escape_replace++)
            {
                ret_string = ReplaceSubstringWithChar(ret_string, m_xml_escape_replace[index_escape_replace], m_xml_escape_chars[index_escape_replace]);
            }


            return ret_string;

        } // ModifyReadXml

        /// <summary>Replace not allowed XML characters</summary>
        static private string ReplaceNotAllowedChars(string i_xml_value)
        {
            string ret_str = i_xml_value;

            if (ret_str.Length == 0)
                return ret_str;

            // Before there are & that this function added
            string amp_str = m_xml_escape_chars[2].ToString();
            string amp_html = m_xml_escape_replace[2];
            ret_str = ret_str.Replace(amp_str, amp_html);

            string lt_str = m_xml_escape_chars[0].ToString();
            string lt_html = m_xml_escape_replace[0];
            ret_str = ret_str.Replace(lt_str, lt_html);

            string gt_str = m_xml_escape_chars[1].ToString();
            string gt_html = m_xml_escape_replace[1];
            ret_str = ret_str.Replace(gt_str, gt_html);

            string qout_str = m_xml_escape_chars[3].ToString();
            string qout_html = m_xml_escape_replace[3];
            ret_str = ret_str.Replace(qout_str, qout_html);

            string apos_str = m_xml_escape_chars[4].ToString();
            string apos_html = m_xml_escape_replace[4];
            ret_str = ret_str.Replace(apos_str, apos_html);

            return ret_str;

        } // ReplaceNotAllowedChars


        /// <summary>Replace a given (input) substring with an input char</summary>
        private static string ReplaceSubstringWithChar(string i_string, string i_sub_string, char i_char)
        {
            return i_string.Replace(i_sub_string, i_char.ToString());

        } // ReplaceSubstringWithChar

        #endregion // Escape characters

        #region Check functions

        /// <summary>Return value Eq. 0: Valid number Eq. -1: All chars are not a number Eq. -2: Starts with zero (0)</summary>
        public static int ValidNumber(string i_number_string)
        {
            string number_string_trim = i_number_string.Trim();

            if (number_string_trim.Length == 0)
                return 0;

            for (int index_char = 0; index_char < number_string_trim.Length; index_char++)
            {
                string current_char = number_string_trim.Substring(index_char, 1);
                bool b_number = false;


                for (int index_number = 0; index_number < m_numbers.Length; index_number++)
                {
                    char current_number = m_numbers[index_number];
                    if (current_char.Equals(current_number.ToString()))
                    {
                        b_number = true;
                    }

                } // index_number

                if (!b_number)
                    return -1;
            } // index_char

            string start_char = number_string_trim.Substring(0, 1);
            if (start_char.Equals(m_numbers[0].ToString()))
                return -2;

            return 0;

        } // ValidNumber


        /// <summary>Return value Eq. 0: Valid number Eq. -1: All chars are not a number or space Eq. -3: Do not start with plus (+)</summary>
        public static int ValidTelephoneNumber(string i_telephon_number_string)
        {
            string telephon_number_check = i_telephon_number_string.Trim();
            if (telephon_number_check.Length == 0)
                return 0;

            string first_char = telephon_number_check.Substring(0, 1);
            if (!first_char.Equals("+"))
                return -3;

            telephon_number_check = telephon_number_check.Substring(1);

            for (int index_char = 0; index_char < telephon_number_check.Length; index_char++)
            {
                string current_char = telephon_number_check.Substring(index_char, 1);
                bool b_number = false;

                if (current_char.Equals(" ")) // space
                {
                    b_number = true;
                }
                else
                {
                    for (int index_number = 0; index_number < m_numbers.Length; index_number++)
                    {
                        char current_number = m_numbers[index_number];
                        if (current_char.Equals(current_number.ToString()))
                        {
                            b_number = true;
                        }

                    } // index_number
                }

                if (!b_number)
                    return -1;
            } // index_char

            return 0;

        } // ValidTelephoneNumber

        #endregion // Check functions

    }

} // namespace