using System;
using System.Linq;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Functions for the initialization (creation) of XML objects from the XML files JazzDokumente.xml and JazzDokumente_20xx_20yy.xml
    /// <para>There are XML files that hold the information about jazz documents that shall be stored on the server.</para>
    /// <para>Jazz documents are for example the season program (a Word document) and the concert information (program) for each concert.</para>
    /// <para></para>
    /// <para>The main (original) documents types are mainly of the type Microsoft Word and Excel, but for most documents there will also be a PDF copy.</para>
	/// <para>The PDF copy can be used for printing or be displayed on the website. For some jazz documents pictures (posters) are also created 
    /// and stored on the server.</para>
	/// <para></para>
	/// <para>The templates XML file (JazzDokumente.xml) hold information about each document like for instance a description and 
    /// instructions/information how the the document is used.</para>
	/// <para>For each season there is a season document file (JazzDokumente_20xx_20yy.xml) holding information about the name of the file and 
    /// where it is stored on the server.</para>
    /// <para></para>
    /// <para>This c# file (JazzXmlDoc.cs) has functions for the creation of XML objects (XDocument) from the XML files JazzDokumente.xml and 
    /// JazzDokumente_20xx_20yy.xml on the server.</para>
    /// <para>The function InitDoc creates the XML objects and store them as member variables: One XDocument variable for JazzDokumente.xml 
    /// and an XDocument array for the JazzDokumente_20xx_20yy.xml files.</para>
    /// <para>Additional arrays corresponding to the XDocument array are also created: An array with the names of the XML files, an array 
    /// with season names and a "status array" telling if the XML object is OK</para>
    /// <para></para>
    /// <para>Applications (like for instance JAZZ live AARAU Admin) must first make the initialization of the XML objects (and also when 
    /// an application has added an additional JazzDokumente_20xx_20yy.xml).</para>
    /// <para>The application must also set the active document by calling SetObjectActiveDoc (member variable) before the get and set 
    /// functions (defined in JazzXmlAdmin.cs) can be called.</para>
    /// <para>There are two functions that tell if the intitialization is made: DocInitialized and GetObjectActiveDoc</para>
    /// <para></para>
    /// <para>The functions for the adding of a new XML file JazzDokumente_20xx_20yy.xml and storing it on the server are Admin application 
    /// functions (and not JazzXml functions).</para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {
        #region Member variables: XML objects and file names

        /// <summary>The XML document (object) that holds the active season document</summary>
        static private XDocument m_xdocument_active_doc = null;

        /// <summary>Returns the XML document object that holds the active season document</summary>
        static public XDocument GetObjectActiveDoc() { return m_xdocument_active_doc; }

        /// <summary>Sets the XML document object that holds the active season document</summary>
        static public void SetObjectActiveDoc(XDocument i_xdocument_active_doc) { m_xdocument_active_doc = i_xdocument_active_doc; }

        /// <summary>The XML document (object) that corresponds to the document templates XML file</summary>
        static private XDocument m_xdocument_templates = null;

        /// <summary>Returns the XML document (object) that corresponds to the document templates XML file</summary>
        static public XDocument GetObjectDocTemplates() { return m_xdocument_templates; }

        /// <summary>Status for m_xdocument_templates</summary>
        static private int m_xdocument_templates_status = -12345;

        /// <summary>Error message for the creation of m_xdocument_templates</summary>
        static private string m_xdocument_templates_error = "Not set";

        /// <summary>The URL to the folder with the document XML files</summary>
        static private string m_url_xml_doc_files_folder = @"";

        /// <summary>Returns the URL path to the folder with the document XML files</summary>
        static public string GetUrlXmlDocFiles() { return m_url_xml_doc_files_folder; }

        /// <summary>The name of the XML file corresponding to the active season document object (m_xdocument_active_doc)</summary>
        static private string m_xdocument_active_doc_file_name = @"";

        /// <summary>Get the name of the XML file corresponding to the active season document object (m_xdocument_active_doc)</summary>
        static public string GetFileNameActiveObject() { return m_xdocument_active_doc_file_name; }

        /// <summary>Set the name of the XML file corresponding to the active season document object (m_xdocument_active_doc)</summary>
        static public void SetFileNameActiveObject(string i_xdocument_active_doc_file_name) 
            { m_xdocument_active_doc_file_name = i_xdocument_active_doc_file_name; }

        /// <summary>The season name corresponding to the active season document object (m_xdocument_active_doc)</summary>
        static private string m_xdocument_active_doc_season_name = @"";

        /// <summary>Get the season name corresponding to the active season document object (m_xdocument_active_doc)</summary>
        static public string GetSeasonNameActiveObject() { return m_xdocument_active_doc_season_name; }

        /// <summary>Set the season name corresponding to the active season document object (m_xdocument_active_doc)</summary>
        static public void SetSeasonNameActiveObject(string i_xdocument_active_doc_season_name) 
                { m_xdocument_active_doc_season_name = i_xdocument_active_doc_season_name; }

        /// <summary>The name of the document templates XML file</summary>
        static private string m_templates_xml_filename = @"";

        /// <summary>Returns the name of the document templates XML file</summary>
        static public string GetFileNameObjectDocTemplates() { return m_templates_xml_filename; }

        /// <summary>Start years for existing XML document files</summary>
        static public int[] m_seasons_documents_start_years = null;

        /// <summary>Season names corresponding to m_seasons_documents_start_years</summary>
        static private string[] m_season_names_documents_strings = null;

        /// <summary>The names of the XML files JazzDokumente_20XX_20YY.xml corresponding to m_xdocument_documents</summary>
        static private string[] m_xdocument_doc_file_names = null;

        /// <summary>Status corresponding to m_xdocument_documents</summary>
        static public int[] m_seasons_documents_status = null;

        /// <summary>XML doc objects corresponding to the XML files JazzDokumente_20XX_20YY.xml</summary>
        static private XDocument[] m_xdocument_documents = null;

        /// <summary>Get all the XML doc objects corresponding to the XML files JazzDokumente_20XX_20YY.xml</summary>
        static public XDocument[] GetObjectAllDocs() { return m_xdocument_documents; }

        /// <summary>Get the names of the XML files JazzDokumente_20XX_20YY.xml corresponding to GetObjectAllDocs</summary>
        static public string[] GetFileNamesAllDocs() { return m_xdocument_doc_file_names; }

        /// <summary>Get season names corresponding to GetObjectAllDocs and GetFileNamesAllDocs</summary>
        static public string[] GetSeasonNamesAllDocs() { return m_season_names_documents_strings; }

        /// <summary>The start season year for document XML files </summary>
        static private int m_documents_start_year = -12345;

        /// <summary>Flag telling if the season document XML objects are set, i.e. if function InitDoc has been called</summary>
        static private bool m_xdocument_documents_initialized = false;

        /// <summary>Returns true if the season document XML objects are set, i.e. if function InitDoc has been called</summary>
        static public bool DocInitialized() { return m_xdocument_documents_initialized; }

        #endregion // Member variables: XML objects and file names

        #region Initialization

        /// <summary>Initialization of document member parameters corresponding to XML files JazzDokumente.xml and JazzDokumente_20xx_20yy.xml 
        /// <para>1. Initialization of five JazzXml document arrays. Call of InitXmlDocumentsAllSeasons.</para>
        /// <para>2. Initialize the XML object with the document templates. Call of InitXmlDocumentTemplates</para>
        /// <para></para>
        /// <para>After this initialization the calling application must also set the active document m_xdocument_active_doc</para>
        /// <para>The following functions can be called: SetObjectActiveDoc, SetActiveXmlObjectAndFileToThisSeason or SetActiveXmlObjectAndFileToNextSeason</para>
        /// <para></para>
        /// <para>The calling application defines in which directory the season document files (JazzDokumente_20xx_20yy.xml) are</para>
        /// <para>The application also defines the name of the templates file (JazzDokumente.xml)</para>
        /// <para>Based on the input start year InitXmlDocumentsAllSeasons (a JazzUtilDoc functions) all season document files are "found", i.e.</para>
        /// <para>for the adding of an additional season document file (JazzDokumente_20xx_20yy.xml) just put it on the right server directory</para>
        /// <para></para>
        /// </summary>
        /// <param name="i_url_xml_doc_files_folder">Server path to the XML document files<</param>
        /// <param name="i_templates_xml_filename">Name of the XML documentation template file</param>
        /// <param name="i_documents_start_year">Start year for XML document files</param>
        /// <param name="o_error">Error message</param>
        static public bool InitDoc(string i_url_xml_doc_files_folder, string i_templates_xml_filename,
                                   int i_documents_start_year, out string o_error)
        {
			o_error = @"";

            m_url_xml_doc_files_folder = i_url_xml_doc_files_folder;
			m_templates_xml_filename = i_templates_xml_filename;
            m_documents_start_year = i_documents_start_year;
            // TODO Check data

            if (!InitXmlDocumentsAllSeasons())
            {
                o_error = @"JazzXml.InitDoc InitXmlDocumentsAllSeasons failed";
                return false;
            }

            string error_message = @"";
            if (!InitXmlDocumentTemplates(out error_message))
            {
                o_error = @"JazzXml.InitDoc Programming error " + error_message;
                return false;
            }

            m_xdocument_documents_initialized = true;

            return true;
        } // InitDoc


        /// <summary>Initialization of the JazzXml document arrays 
        /// <para>There are five JazzXml document arrays that are initialized (set):</para>
        /// <para>- Array m_xdocument_documents with XDocument objects created from XML document season files (JazzDokumente_201xx_2099.xml)</para>
        /// <para>- Array m_xdocument_doc_file_names with strings holding the names of the season document files (JazzDokumente_201xx_2099.xml)</para>
        /// <para>- Array m_season_names_documents_strings with strings holding the names of the seasons</para>
        /// <para>- Array m_seasons_documents_start_years with ints holding the start years</para>
        /// <para>- Array m_seasons_documents_status with integers holding the status of the XML objects/files, i.e. if they are corrupt</para>
        /// <para>These five arrays have the same number of elements and each element (index_element) correspond to each other.</para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="m_url_xml_doc_files_folder">Server path to the XML document files<</param>
        /// <param name="m_templates_xml_filename">Name of the XML documentation template file</param>
        /// <param name="m_documents_start_year">Start year for XML document files</param>
        /// <param name="o_error">Error message</param>
        static public Boolean InitXmlDocumentsAllSeasons()
        {
            m_seasons_documents_start_years = JazzUtils.GetSeasonStartYearsForExistingXmlDocumentsFiles(m_url_xml_doc_files_folder, m_documents_start_year);
            if (null == m_seasons_start_years)
                return false;

            m_season_names_documents_strings = JazzUtils.GetSeasonNamesForExistingXmlFiles(m_seasons_documents_start_years);
            if (null == m_season_names_documents_strings)
                return false;

            int size_seasons_documents = m_seasons_documents_start_years.Length;
            if (0 == size_seasons_documents)
                return false;

            m_xdocument_documents = new XDocument[size_seasons_documents];

            m_seasons_documents_status = new int[size_seasons_documents];
            for (int index_init = 0; index_init < m_seasons_documents_status.Length; index_init++)
                m_seasons_documents_status[index_init] = -12345;

            m_xdocument_doc_file_names = new string[size_seasons_documents];

            for (int index_doc = 0; index_doc < size_seasons_documents; index_doc++)
            {
                String xdocument_doc_file_name = GetSeasonDocumentsFileName(m_seasons_documents_start_years[index_doc], m_url_xml_doc_files_folder);

                m_xdocument_doc_file_names[index_doc] = xdocument_doc_file_name;

                JazzOsUtils.LoadXmlDocument(xdocument_doc_file_name, 5, index_doc);
            }

            // TODO Boolean b_set_publish = SetPublishStatus();

            // TODO m_season_docs_documents_initialized = true;

            return true;
        } // InitXmlDocumentsAllSeasons

        /// <summary>Initialize the XML object (m_xdocument_templates) with the document templates</summary>
        static bool InitXmlDocumentTemplates(out string o_error)
        {
            o_error = @"";

            string url_file_document_templates = GetWebSiteUrl() + GetUrlXmlDocFiles() + @"/" + GetFileNameObjectDocTemplates();
            if (url_file_document_templates.Trim().Length == 0)
            {
                o_error = @"JazzXml.InitXmlDocumentTemplates URL for the templates file is not defined";

                return false;
            }
                
            JazzOsUtils.LoadXmlDocument(url_file_document_templates, 6, -1);

            string error_message = @"";
            if (GetXmlDocumentTemplatesStatus(out error_message) < 0)
            {
                o_error = @"JazzXml.InitXmlDocumentTemplates Programming error: " + error_message;

                return false;
            }

            // Additional (not necessary) check that object is created
            if (null == GetObjectDocTemplates())
            {
                o_error = @"JazzXml.InitXmlDocumentTemplates Template object is not created (JazzOsUtils.LoadXmlDocument failed)";

                return false;
            }

            return true;

        } // InitXmlDocumentTemplates

        #endregion // Initialization

        #region Set functions

        /// <summary>Set document templates</summary>
        static public void SetXmlDocumentTemplates(XDocument i_xdocument_templates)
        {
            m_xdocument_templates_status = 0;
            m_xdocument_templates_error = "";

            m_xdocument_templates = i_xdocument_templates;
        } // SetXmlDocumentTemplates

        /// <summary>Set status for document templates</summary>
        static public void SetXmlDocumentTemplatesStatus(int i_status, string i_error)
        {
            m_xdocument_templates_status = i_status;
            m_xdocument_templates_error = i_error;

        } // SetXmlDocumentTemplatesStatus

        /// <summary>Get status for document templates</summary>
        static public int GetXmlDocumentTemplatesStatus(out string o_error)
        {
            o_error = m_xdocument_templates_error;

            return m_xdocument_templates_status;

        } // GetXmlDocumentTemplatesStatus

        /// <summary>Set season documents XDocument</summary>
        static public void SetSeasonDocumentsXDocument(XDocument i_document_current, int i_index)
        {
            if (null == m_xdocument_documents)
                return;
            if (i_index < 0)
                return;
            if (i_index >= m_xdocument_documents.Length)
                return;

            m_xdocument_documents[i_index] = i_document_current;

            SetSeasonDocumentXDocumentStatus(0, i_index);

        } // SetSeasonDocumentsXDocument

        /// <summary>Set season document status</summary>
        static public void SetSeasonDocumentXDocumentStatus(int i_status, int i_index)
        {
            if (null == m_seasons_documents_status)
                return;
            if (i_index < 0)
                return;
            if (i_index >= m_seasons_documents_status.Length)
                return;

            m_seasons_documents_status[i_index] = i_status;

        } // SetSeasonDocumentXDocumentStatus

        #endregion // Set functions

        #region Get number of "document" concerts

        /// <summary>Returns the number of bands (concerts)</summary>
        static public int GetNumberDocConcerts(out string o_error)
        {
            o_error = @"";

            if (null == m_xdocument_active_doc)
            {
                o_error = @"JazzXml.GetNumberDocConcerts  XDocument m_xdocument_active_doc is null";
                return -1;
            }


            int ret_n_concerts = 0;

            try
            {
                int current_concert_number = 0;
                foreach (XElement element_concert in m_xdocument_active_doc.Descendants(GetTagDocConcert()))
                {
                    current_concert_number = current_concert_number + 1;
                    String band_name = GetDocConcertBandName(current_concert_number);

                    if (XmlNodeValueIsSet(band_name))
                        ret_n_concerts = ret_n_concerts + 1;

                } // element_concert
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.GetNumberDocConcerts " + e.ToString();
                return -2;
            }

            return ret_n_concerts;
        } // GetNumberDocConcerts 

        #endregion // Get number of "document" concerts

        #region Get array functions

        /// <summary>Returns all the document templates</summary>
        static public JazzDocTemplate[] GetAllDocTemplates(out string o_error)
        {
            o_error = @"";
            JazzDocTemplate[] ret_templates = null;

            string error_message = @"";
            int n_templates = GetNumberOfTemplates(out error_message);
            if (n_templates <= 0)
            {
                o_error = @"JazzXml.GetAllDocTemplates n_templates <= 0";
                return ret_templates;
            }

            ret_templates = new JazzDocTemplate[n_templates];

            for (int template_number=1; template_number <= n_templates; template_number++)
            {
                String template_name = GetTemplTemplateName(template_number);

                if (XmlNodeValueIsSet(template_name))
                {
                    JazzDocTemplate current_template = new JazzDocTemplate();

                    current_template.TemplateName = template_name;

                    string template_extensions = GetTemplTemplateExtensions(template_number);
                    current_template.TemplateExtensions = template_extensions;

                    string template_description = GetTemplTemplateDescription(template_number);
                    current_template.TemplateDescription = template_description;

                    string template_document_type = GetTemplDocumentType(template_number);
                    current_template.TemplateDocumentType = template_document_type;

                    string template_document_dialog = GetTemplDocumentDialog(template_number);
                    current_template.TemplateDocumentDialog = template_document_dialog;

                    string template_document_dialog_title = GetTemplDocumentDialogTitle(template_number);
                    current_template.TemplateDocumentDialogTitle = template_document_dialog_title;

                    string template_file_path_description = GetTemplFilePathDescription(template_number);
                    current_template.TemplateFilePathDescription = template_file_path_description;

                    string template_file_name_doc_description = GetTemplFileNameDocDescription(template_number);
                    current_template.TemplateFileNameDocDescription = template_file_name_doc_description;

                    string template_file_name_xls_description = GetTemplFileNameXlsDescription(template_number);
                    current_template.TemplateFileNameXlsDescription = template_file_name_xls_description;

                    string template_file_name_pdf_description = GetTemplFileNamePdfDescription(template_number);
                    current_template.TemplateFileNamePdfDescription = template_file_name_pdf_description;

                    string template_file_name_txt_description = GetTemplFileNameTxtDescription(template_number);
                    current_template.TemplateFileNameTxtDescription = template_file_name_txt_description;

                    string template_file_name_img_description = GetTemplFileNameImgDescription(template_number);
                    current_template.TemplateFileNameImgDescription = template_file_name_img_description;

                    string template_published_description = GetTemplPublishedDescription(template_number);
                    current_template.TemplatePublishedDescription = template_published_description;

                    string template_name_description = GetTemplTemplateNameDescription(template_number);
                    current_template.TemplateNameDescription = template_name_description;

                    error_message = @"";
                    if (!current_template.CheckInput(out error_message))
                    {
                        o_error = @"JazzXmlDoc.GetAllDocTemplates " + error_message;
                    }

                    ret_templates[template_number - 1] = current_template;
                }

            } // Loop template_number

            return ret_templates;
        } // GetAllDocTemplates

        /// <summary>Returns the band names in the active document m_xdocument_active_doc</summary>
        static public string[] GetDocBandNames()
        {
            string[] ret_band_names = null;

            string error_message = @"";
            int n_concerts = GetNumberDocConcerts(out error_message);
            if (n_concerts <= 0)
                return ret_band_names;

            ret_band_names = new string[n_concerts];


            for (int concert_number=1; concert_number<= n_concerts; concert_number++)
            {
                string band_name = GetDocConcertBandName(concert_number);

                ret_band_names[concert_number - 1] = band_name;
            }

            return ret_band_names;

        } // GetDocBandNames

        #endregion // Get array functions

        #region Get and set document templates functions

        /// <summary>Returns the templates description</summary>
        static public string GetTmplsDescription() { return GetInnerTextTemplatesNode(GetTagTemplsDescription()); }

        /// <summary>Sets the templates description</summary>
        static public void SetTmplsDescription(string i_description) { SetInnerTextTemplatesNode(GetTagTemplsDescription(), i_description); }

        /// <summary>Returns the templates path</summary>
        static public string GetTemplsTemplatesPath() { return GetInnerTextTemplatesNode(GetTagTemplsTemplatesPath()); }

        /// <summary>Sets the templates path</summary>
        static public void SetTemplsTemplatesPath(string i_description) { SetInnerTextTemplatesNode(GetTagTemplsTemplatesPath(), i_description); }

        /// <summary>Returns the template name</summary>
        static public string GetTemplTemplateName(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplTemplateName()); }

        /// <summary>Sets the template name</summary>
        static public void SetTemplTemplateName(int i_template, string i_name) { SetInnerTextTemplateNode(i_template, GetTagTemplTemplateName(), i_name); }

        /// <summary>Returns the template extensions</summary>
        static public string GetTemplTemplateExtensions(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplTemplateExtensions()); }

        /// <summary>Sets the template extensions</summary>
        static public void SetTemplTemplateExtensions(int i_template, string i_extensions) { SetInnerTextTemplateNode(i_template, GetTagTemplTemplateExtensions(), i_extensions); }

        /// <summary>Returns the template description</summary>
        static public string GetTemplTemplateDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplTemplateDescription()); }

        /// <summary>Sets the template description</summary>
        static public void SetTemplTemplateDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplTemplateDescription(), i_description); }

        /// <summary>Returns the template file path description</summary>
        static public string GetTemplFilePathDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplFilePathDescription()); }

        /// <summary>Sets the template file path description</summary>
        static public void SetTemplFilePathDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplFilePathDescription(), i_description); }

        /// <summary>Returns the template DOC description</summary>
        static public string GetTemplFileNameDocDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplFileNameDocDescription()); }

        /// <summary>Sets the template DOC description</summary>
        static public void SetTemplFileNameDocDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplFileNameDocDescription(), i_description); }

        /// <summary>Returns the template XLS description</summary>
        static public string GetTemplFileNameXlsDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplFileNameXlsDescription()); }

        /// <summary>Sets the template XLS description</summary>
        static public void SetTemplFileNameXlsDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplFileNameXlsDescription(), i_description); }

        /// <summary>Returns the template PDF description</summary>
        static public string GetTemplFileNamePdfDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplFileNamePdfDescription()); }

        /// <summary>Sets the template PDF description</summary>
        static public void SetTemplFileNamePdfDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplFileNamePdfDescription(), i_description); }

        /// <summary>Returns the template TXT description</summary>
        static public string GetTemplFileNameTxtDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplFileNameTxtDescription()); }

        /// <summary>Sets the template TXT description</summary>
        static public void SetTemplFileNameTxtDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplFileNameTxtDescription(), i_description); }

        /// <summary>Returns the template TXT description</summary>
        static public string GetTemplFileNameImgDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplFileNameImgDescription()); }

        /// <summary>Sets the template TXT description</summary>
        static public void SetTemplFileNameImgDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplFileNameImgDescription(), i_description); }

        /// <summary>Returns the template published description</summary>
        static public string GetTemplPublishedDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplPublishedDescription()); }

        /// <summary>Sets the template published description</summary>
        static public void SetTemplPublishedDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplPublishedDescription(), i_description); }

        /// <summary>Returns the template name description</summary>
        static public string GetTemplTemplateNameDescription(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplTemplateNameDescription()); }

        /// <summary>Sets the template name description</summary>
        static public void SetTemplTemplateNameDescription(int i_template, string i_description) { SetInnerTextTemplateNode(i_template, GetTagTemplTemplateNameDescription(), i_description); }

        /// <summary>Returns the document type</summary>
        static public string GetTemplDocumentType(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplDocumentType()); }

        /// <summary>Sets the document type</summary>
        static public void SetTemplDocumentType(int i_template, string i_type) { SetInnerTextTemplateNode(i_template, GetTagTemplDocumentType(), i_type); }

        /// <summary>Returns the document dialog</summary>
        static public string GetTemplDocumentDialog(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplDocumentDialog()); }

        /// <summary>Sets the document dialog</summary>
        static public void SetTemplDocumentDialog(int i_template, string i_dialog) { SetInnerTextTemplateNode(i_template, GetTagTemplDocumentDialog(), i_dialog); }

        /// <summary>Returns the document dialog</summary>
        static public string GetTemplDocumentDialogTitle(int i_template) { return GetInnerTextTemplateNode(i_template, GetTagTemplDocumentDialogTitle()); }

        /// <summary>Sets the document dialog</summary>
        static public void SetTemplDocumentDialogTitle(int i_template, string i_dialog_type) { SetInnerTextTemplateNode(i_template, GetTagTemplDocumentDialogTitle(), i_dialog_type); }

        // TODO static public string GetTagTemplTemplateInstructions() { return m_text_tags_template[3]; }
        // TODOstatic public string GetTagTemplTemplateInstruction() { return m_text_tags_template[4]; }


        #endregion // Get and set document templates functions

        #region Get and set season documents functions

        /// <summary>Returns the jazz season program years, e.g. 2017-2018</summary>
        static public string GetDocSeasonYears() { return GetInnerTextDocSeasonNode(GetTagDocSeasonYears()); }

        /// <summary>Sets the jazz season years, e.g. 2017-2018</summary>
        static public void SetDocSeasonYears(string i_season_programm) { SetInnerTextDocSeasonNode(GetTagDocSeasonYears(), i_season_programm); }

        /// <summary>Returns the path to the XML season documents</summary>
        static public string GetDocDocumentsPath() { return GetInnerTextDocSeasonNode(GetTagDocDocumentsPath()); }

        /// <summary>Sets the path to the XML season documents</summary>
        static public void SetDocDocumentsPath(string i_season_programm) { SetInnerTextDocSeasonNode(GetTagDocDocumentsPath(), i_season_programm); }

        /// <summary>Returns a flag telling if the path has been used,i.e. if subdirectories have been created</summary>
        static public string GetDocDocumentsPathUsed() { return GetInnerTextDocSeasonNode(GetTagDocDocumentsPathUsed()); }

        /// <summary>Sets a flag telling that the path has been used,i.e. subdirectories have been created</summary>
        static public void SetDocDocumentsPathUsed(string i_season_programm) { SetInnerTextDocSeasonNode(GetTagDocDocumentsPathUsed(), i_season_programm); }


        /// <summary>Returns the band name</summary>
        static public string GetDocConcertBandName(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertBandName()); }

        /// <summary>Sets the band name</summary>
        static public void SetDocConcertBandName(int i_concert, string i_band_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertBandName(), i_band_name); }

        /// <summary>Returns the season document template name</summary>
        static public string GetDocSeasonTemplateName(int i_document) { return GetInnerTextDocConcertNode(i_document, GetTagDocSeasonConcertTemplateName()); }

        /* TODO
             ret_season_doc.TemplateName = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertTemplateName());
            ret_season_doc.FilePath = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFilePath());
            ret_season_doc.FileNameDoc = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameDoc());
            ret_season_doc.FileNamePdf = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNamePdf());
            ret_season_doc.FileNameTxt = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameTxt());
            ret_season_doc.FileNameImg = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameImg());
            string published = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertPublished());
         */

        /// <summary>Returns the template name</summary>
        // static public string GetDocConcertTemplateName(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertTemplateName()); }

        /// <summary>Sets the template name</summary>
        // static public void SetDocConcertTemplateName(int i_concert, string i_template_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertTemplateName(), i_template_name); }

        /// <summary>Returns the file path</summary>
        // static public string GetDocConcertFilePath(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertFilePath()); }

        /// <summary>Sets the file path</summary>
        // static public void SetDocConcertFilePath(int i_concert, string i_file_path) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertFilePath(), i_file_path); }

        /// <summary>Returns the DOC file name</summary>
        // static public string GetDocConcertFileNameDoc(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNameDoc()); }

        /// <summary>Sets the DOC file name</summary>
        // static public void SetDocConcertFileNameDoc(int i_concert, string i_doc_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNameDoc(), i_doc_name); }

        /// <summary>Returns the PDF file name</summary>
        // static public string GetDocConcertFileNamePdf(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNamePdf()); }

        /// <summary>Sets the PDF file name</summary>
        // static public void SetDocConcertFileNamePdf(int i_concert, string i_pdf_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNamePdf(), i_pdf_name); }

        /// <summary>Returns the TXT file name</summary>
        // static public string GetDocConcertFileNameTxt(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNameTxt()); }

        /// <summary>Sets the TXT file name</summary>
        // static public void SetDocConcertFileNameTxt(int i_concert, string i_txt_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNameTxt(), i_txt_name); }

        /// <summary>Returns the IMG file name</summary>
        // static public string GetDocConcertFileNameImg(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNameImg()); }

        /// <summary>Sets the IMG file name</summary>
        // static public void SetDocConcertFileNameImg(int i_concert, string i_img_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertFileNameImg(), i_img_name); }

        /// <summary>Returns a flag telling if the document has been published</summary>
        // static public string GetDocConcertPublished(int i_concert) { return GetInnerTextDocConcertNode(i_concert, GetTagDocConcertPublished()); }

        /// <summary>Sets a flag telling if the document has been published</summary>
        // static public void SetDocConcertPublished(int i_concert, string i_img_name) { SetInnerTextDocConcertNode(i_concert, GetTagDocConcertPublished(), i_img_name); }

        #endregion // Get and set season documents functions

        #region Construct XML file name

        /// <summary>Constructs the season XML documents file name, i.e. the URL for the file</summary>
        static public String GetSeasonDocumentsFileName(int i_start_year, string i_url_xml_doc_files_folder)
        {
            String ret_season_documents_file_name = m_web_site_url + 
                i_url_xml_doc_files_folder + "/JazzDokumente_" + 
                i_start_year.ToString() + "_" + (i_start_year + 1).ToString() + ".xml";

            return ret_season_documents_file_name;
        } // GetSeasonDocumentsFileName

        #endregion // Construct XML file name

        #region Get season documents



        #endregion // Get season documents

        #region Get concert documents

        /// <summary>Get all concert documents for a given band name as an array</summary>
        static public JazzDoc[] GetAllConcertDocumentsAsArrayBandName(string i_band_name, out string o_error)
        {
            JazzDoc[] ret_array = null;
            o_error = @"";

            int concert_number = GetConcertNumberFromBandName(i_band_name, out o_error);
            if (concert_number < 1)
            {
                o_error = @"JazzXml.GetAllConcertDocumentsAsArrayBandName " + concert_number.ToString() + " " + o_error;
                return ret_array;
            }

            ret_array = GetAllConcertDocumentsAsArray(concert_number);

            return ret_array;

        } // GetAllConcertDocumentsAsArrayBandName


        /// <summary>Get all concert documents for a given concert as an array</summary>
        static public JazzDoc[] GetAllConcertDocumentsAsArray(int i_concert_number)
        {
            JazzDoc[] ret_concert_documents = null;

            string error_message = @"";
            int number_concert_documents = GetNumberOfDocuments(i_concert_number, out error_message);
            if (number_concert_documents <= 0)
                return ret_concert_documents; // Programming error

            ret_concert_documents = new JazzDoc[number_concert_documents];

            for (int index_concert_doc = 0; index_concert_doc < number_concert_documents; index_concert_doc++)
            {
                JazzDoc current_concert_doc = new JazzDoc();

                SetConcertObject(current_concert_doc, i_concert_number, index_concert_doc + 1);

                ret_concert_documents[index_concert_doc] = current_concert_doc;
            }

            return ret_concert_documents;

        } // GetAllConcertDocumentsAsArray

        /// <summary>Set concert object</summary>
        static private JazzDoc SetConcertObject(JazzDoc i_concert_doc, int i_concert_number, int i_doc_number)
        {
            if (null == i_concert_doc)
                return null; // Programming error

            string error_message = @"";
            int number_concert_documents = GetNumberOfDocuments(i_concert_number, out error_message);
            if (number_concert_documents <= 0)
                return null; // Programming error
            if (i_doc_number <= 0 || i_doc_number > number_concert_documents)
                return null; // Programming error

            JazzDoc ret_season_doc = i_concert_doc;

            ret_season_doc.TemplateName = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertTemplateName());
            ret_season_doc.FilePath = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFilePath());
            ret_season_doc.FileNameDoc = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameDoc());
            ret_season_doc.FileNameXls = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameXls());
            ret_season_doc.FileNamePdf = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNamePdf());
            ret_season_doc.FileNameTxt = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameTxt());
            ret_season_doc.FileNameImg = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertFileNameImg());
            string published = GetDocumentInnerText(i_concert_number, i_doc_number, GetTagDocSeasonConcertPublished());
            if (published.Equals("true"))
                ret_season_doc.Published = true;
            else
                ret_season_doc.Published = false;

            return ret_season_doc;

        } // SetConcertObject
       

        /// <summary>Get all concert documents in current XML document (object)</summary>
        static public JazzDoc[] GetAllConcertDocumentsInCurrentDocument()
        {
            JazzDoc[] ret_concert_documents = null;

            string error_message = @"";
            int number_concert_documents = GetNumberDocConcerts(out error_message);
            if (number_concert_documents <= 0)
                return ret_concert_documents; // Programming error

            ret_concert_documents = new JazzDoc[number_concert_documents];

            for (int index_concert_doc = 0; index_concert_doc < number_concert_documents; index_concert_doc++)
            {
                JazzDoc current_concert_doc = new JazzDoc();

                SetConcertJazzDoc(current_concert_doc, index_concert_doc + 1);

                ret_concert_documents[index_concert_doc] = current_concert_doc;
            }

            return ret_concert_documents;

        } // GetAllConcertDocumentsInCurrentDocument


        /// <summary>Set concert jazz document data</summary>
        static private JazzDoc SetConcertJazzDoc(JazzDoc i_concert_doc, int i_concert_doc_number)
        {
            if (null == i_concert_doc)
                return null; // Programming error

            string error_message = @"";
            int number_concert_documents = GetNumberDocConcerts(out error_message);
            if (i_concert_doc_number <= 0 || i_concert_doc_number > number_concert_documents)
                return null; // Programming error

            JazzDoc ret_season_doc = i_concert_doc;

            ret_season_doc.TemplateName = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertTemplateName());
            ret_season_doc.FilePath = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertFilePath());
            ret_season_doc.FileNameDoc = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertFileNameDoc());
            ret_season_doc.FileNameXls = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertFileNameXls());
            ret_season_doc.FileNamePdf = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertFileNamePdf());
            ret_season_doc.FileNameTxt = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertFileNameTxt());
            ret_season_doc.FileNameImg = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertFileNameImg());
            string published = GetInnerTextDocConcertNode(i_concert_doc_number, GetTagDocSeasonConcertPublished());
            if (published.Equals("true"))
                ret_season_doc.Published = true;
            else
                ret_season_doc.Published = false;

            return ret_season_doc;

        } // SetConcertJazzDoc

        #endregion // Get concert documents

        #region Get season documents

        /// <summary>Get all season documents for a given concert</summary>
        static public JazzDoc[] GetAllSeasonDocumentsAsArray()
        {
            JazzDoc[] ret_season_documents = null;

            string error_message = @"";
            int number_season_documents = GetNumberOfSeasonDocuments(out error_message);
            if (number_season_documents <= 0)
                return ret_season_documents; // Programming error

            ret_season_documents = new JazzDoc[number_season_documents];

            for (int index_season_doc = 0; index_season_doc < number_season_documents; index_season_doc++)
            {
                JazzDoc current_season_doc = new JazzDoc();

                SetSeasonObject(current_season_doc, index_season_doc + 1);

                ret_season_documents[index_season_doc] = current_season_doc;
            }

            return ret_season_documents;

        } // GetAllSeasonDocuments



        /// <summary>Set season object</summary>
        static private JazzDoc SetSeasonObject(JazzDoc i_season_doc, int i_season_doc_number)
        {
            if (null == i_season_doc)
                return null; // Programming error

            string error_message = @"";
            int number_season_documents = GetNumberOfSeasonDocuments(out error_message);
            if (i_season_doc_number <= 0 || i_season_doc_number > number_season_documents)
                return null; // Programming error

            JazzDoc ret_season_doc = i_season_doc;

            ret_season_doc.TemplateName = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertTemplateName());
            ret_season_doc.FilePath     = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertFilePath());
            ret_season_doc.FileNameDoc  = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertFileNameDoc());
            ret_season_doc.FileNameXls  = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertFileNameXls());
            ret_season_doc.FileNamePdf  = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertFileNamePdf());
            ret_season_doc.FileNameTxt  = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertFileNameTxt());
            ret_season_doc.FileNameImg  = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertFileNameImg());
            string published            = GetInnerTextSeasonNode(i_season_doc_number, GetTagDocSeasonConcertPublished());
            if (published.Equals("true"))
                ret_season_doc.Published = true;
            else
                ret_season_doc.Published = false;

            return ret_season_doc;

        } // SetSeasonObject


        #endregion // Get season documents

        #region Set season documents

        /// <summary>Set season document data</summary>
        static public bool SetSeasonDoc(JazzDoc i_season_doc, string i_template_name, out string o_error)
        {
            o_error = @"";

            if (null == i_season_doc)
            {
                o_error = @"JazzXml.SetSeasonDoc Programming error: Input JazzDoc is null";
                return false;
            }

            if (i_template_name.Trim().Length == 0)
            {
                o_error = @"JazzXml.SetSeasonDoc Programming error: Input template name is empty";
                return false;
            }

            JazzDoc[] all_season_docs = GetAllSeasonDocumentsAsArray();

            if (null == all_season_docs || all_season_docs.Length == 0)
            {
                o_error = @"JazzXml.SetSeasonDoc Programming error: There are no season documents set";
                return false;
            }

            int season_doc_number = -12345;
            for (int index_season_doc=0; index_season_doc< all_season_docs.Length; index_season_doc++)
            {
                JazzDoc current_season_doc = all_season_docs[index_season_doc];
                if (current_season_doc.TemplateName.Equals(i_template_name))
                {
                    season_doc_number = index_season_doc + 1;
                    break;
                }
            }

            if (season_doc_number < 0)
            {
                o_error = @"JazzXml.SetSeasonDoc There is no season document with template name " + i_template_name;
                return false;
            }

            if (!SetSeasonXmlDoc(i_season_doc, season_doc_number))
            {
                o_error = @"JazzXml.SetSeasonDoc Programmin error:  SetSeasonXmlDoc failed";
                return false;
            }

            return true;

        } // SetSeasonDoc
     

        /// <summary>Set season XML document data</summary>
        static private bool SetSeasonXmlDoc(JazzDoc i_season_doc, int i_season_doc_number)
        {
            if (null == i_season_doc)
                return false; // Programming error

            string error_message = @"";
            if (i_season_doc_number <= 0 || i_season_doc_number > GetNumberOfSeasonDocuments(out error_message))
                return false; // Programming error


            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertTemplateName(), i_season_doc.TemplateName);
            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertFilePath(),     i_season_doc.FilePath);
            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertFileNameDoc(),  i_season_doc.FileNameDoc);
            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertFileNameXls(),  i_season_doc.FileNameXls);
            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertFileNamePdf(),  i_season_doc.FileNamePdf);
            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertFileNameTxt(),  i_season_doc.FileNameTxt);
            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertFileNameImg(),  i_season_doc.FileNameImg);

            string str_published = @"";
            if (i_season_doc.Published)
            {
                str_published = @"true";
            }
            else
            {
                str_published = @"false";
            }

            SetInnerTextDocSeasonDocumentNode(i_season_doc_number, GetTagDocSeasonConcertPublished(), str_published);

            return true;

        } // SetSeasonXmlDoc

        #endregion // Set season documents

        #region Set concert document

        /// <summary>Write concert JazzDoc object
        /// <para>1. Get concert number </para>
        /// <para>2. Call WriteConcertDocIndex.</para>
        /// <para></para>
        /// </summary>
        /// <param name="i_concert_doc">Input JazzDoc object</param>
        /// <param name="i_band_name">Band name</param>
        /// <param name="o_error">Error message</param>
        static public bool WriteConcertDoc(JazzDoc i_concert_doc, string i_band_name, out string o_error)
        {
            o_error = @"";

            int concert_number = GetConcertNumberFromBandName(i_band_name, out o_error);
            if (concert_number < 1)
            {
                o_error = @"JazzXml.WriteConcertDoc " + concert_number.ToString() + " " + o_error;
            }

            if (!WriteConcertDocIndex(i_concert_doc, concert_number, out o_error))
            {
                o_error = @"JazzXml.WriteConcertDoc WriteConcertDocIndex failed " + o_error;

                return false;
            }

            return true;
        } // WriteConcertDoc

        /// <summary>Get the concert number from the input band name
        /// <para>1. Get array with band names. Call of GetDocBandNames </para>
        /// <para>2. Return index for the input bandname plus one (+ 1)</para>
        /// <para>A negative value (-2) will be returned if the band name is missing in the array</para>
        /// </summary>
        static private int GetConcertNumberFromBandName(string i_band_name, out string o_error)
        {
            int ret_concert_number = -12345;
            o_error = @"";

            string[] band_names = GetDocBandNames();

            if (null == band_names)
            {
                o_error = @"JazzXml.GetConcertNumberFromBandName band_names is null";
                return -1;
            }

            for (int index_band = 0; index_band < band_names.Length; index_band++)
            {
                string band_name = band_names[index_band];
                if (band_name.Equals(i_band_name))
                {
                    ret_concert_number = index_band + 1;

                    return ret_concert_number;
                }
            }

            string error_band_names = "";

            for (int index_error = 0; index_error < band_names.Length; index_error++)
            {
                error_band_names = " " + band_names[index_error] + ",";
            }


            o_error = @"JazzXml.GetConcertNumberFromBandName i_band_name= '" + i_band_name + "' band_names[0]= '" + band_names[0] + "'";

            return -2;

        } // GetConcertNumberFromBandName

        /// <summary>Write concert JazzDoc object
        /// <para>All JazzDoc objects for the given concert number of the active XML document object is retrieved. Call of  GetAllConcertDocumentsAsArray(i_concert_number).</para>
        /// <para>The JazzDoc number (index) in the retrieved array is determined with the input template name in the input JazzDoc object.</para>
        /// <para>The input JazzDoc object is written to the the active XML document object. Call of SetConcertXmlDoc(i_concert_doc, concert_doc_number).</para>
        /// <para></para>
        /// </summary>
        /// <param name="i_concert_doc">Input JazzDoc object</param>
        /// <param name="i_concert_number">Concert number</param>
        /// <param name="o_error">Error message</param>
        static public bool WriteConcertDocIndex(JazzDoc i_concert_doc, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == i_concert_doc)
            {
                o_error = @"JazzXml.WriteConcertDocIndex Programming error: Input JazzDoc is null";
                return false;
            }

            if (i_concert_number < 1)
            {
                o_error = @"JazzXml.WriteConcertDocIndex Input concert number <= 1 i_concert_number=  " + i_concert_number.ToString();
                return false;
            }

            string template_name = i_concert_doc.TemplateName;

            if (template_name.Trim().Length == 0)
            {
                o_error = @"JazzXml.WriteConcertDocIndex Programming error: Template name of the input JazzDoc object is empty";
                return false;
            }

            JazzDoc[] all_concert_docs = GetAllConcertDocumentsAsArray(i_concert_number);

            if (null == all_concert_docs || all_concert_docs.Length == 0)
            {
                o_error = @"JazzXml.WriteConcertDocIndex Programming error: There are no concert documents for the given concert i_concert_number= " + i_concert_number.ToString();
                return false;
            }

            int concert_doc_number = -12345;
            for (int index_concert_doc = 0; index_concert_doc < all_concert_docs.Length; index_concert_doc++)
            {
                JazzDoc current_concert_doc = all_concert_docs[index_concert_doc];

                if (current_concert_doc.TemplateName.Equals(template_name))
                {
                    concert_doc_number = index_concert_doc + 1;
                    break;
                }
            }

            if (concert_doc_number < 0)
            {
                o_error = @"JazzXml.WriteConcertDocIndex No concert document with template name " + template_name + " for  i_concert_number" + i_concert_number.ToString();
                return false;
            }

            if (!SetConcertXmlDoc(i_concert_doc, i_concert_number, concert_doc_number))
            {
                o_error = @"JazzXml.WriteConcertDocIndex Programming error:  SetConcertXmlDoc failed";
                return false;
            }

            return true;

        } // WriteConcertDocIndex

        /// <summary>Set concert XML document data</summary>
        static private bool SetConcertXmlDoc(JazzDoc i_concert_doc, int i_concert_number, int i_concert_doc_number)
        {
            if (null == i_concert_doc)
                return false; // Programming error

            if (i_concert_number <= 0)
                return false; // Programming error

            if (i_concert_doc_number <= 0 )
                return false; // Programming error

            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertTemplateName(), i_concert_doc.TemplateName);
            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertFilePath(), i_concert_doc.FilePath);
            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertFileNameDoc(), i_concert_doc.FileNameDoc);
            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertFileNameXls(), i_concert_doc.FileNameXls);
            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertFileNamePdf(), i_concert_doc.FileNamePdf);
            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertFileNameTxt(), i_concert_doc.FileNameTxt);
            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertFileNameImg(), i_concert_doc.FileNameImg);

            string str_published = @"";
            if (i_concert_doc.Published)
            {
                str_published = @"true";
            }
            else
            {
                str_published = @"false";
            }

            SetInnerTextDocConcertDocumentNode(i_concert_number, i_concert_doc_number, GetTagDocSeasonConcertPublished(), str_published);

            return true;

        } // SetConcertXmlDoc

        #endregion // Set concert document

        #region Debug

        /// <summary>Returns a string with state (initialization) data corresponding to XML files JazzDokumente.xml and JazzDokumente_20xx_20yy.xml 
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_b_array_values">Flag telling if array values shall be added</param>
        /// <param name="i_b_active_xml_content">Flag telling if the content of the active XLM object shall be added</param>
        static public string DebugGetDocStateData(bool i_b_array_values, bool i_b_active_xml_content)
        {
            string ret_string = @"";
            ret_string = ret_string + @"Two member variables m_xdocument_documents_initialized and m_xdocument_active_doc tell if doc objects have been initialized " + NewLine();
            ret_string = ret_string + @"DocInitialized() returns " + DocInitialized().ToString() + NewLine();

            if (GetObjectActiveDoc() != null)
            {
                ret_string = ret_string + @"GetObjectActiveDoc() has been called. Pointer is not null." + NewLine();
            }
            else
            {
                ret_string = ret_string + @"GetObjectActiveDoc() has not been called. Pointer is null." + NewLine();
            }

            ret_string = ret_string + @"" + NewLine();

            ret_string = ret_string + @"URL path to the folder with the document XML files GetUrlXmlDocFiles()= " + GetUrlXmlDocFiles() + NewLine();
            ret_string = ret_string + @"The name of the document templates XML file GetFileNameObjectDocTemplates()= " + GetFileNameObjectDocTemplates() + NewLine();
            ret_string = ret_string + @"The start season year for document XML files m_documents_start_year= " + m_documents_start_year.ToString() + NewLine();
            ret_string = ret_string + @"" + NewLine();

            ret_string = ret_string + DebugAppendArraySizes();

            ret_string = ret_string + DebugAppendActiveDoc();

            if (i_b_array_values)
            {
                ret_string = ret_string + DebugAppendFileNames();

                ret_string = ret_string + DebugAppendSeasonNames();

                ret_string = ret_string + DebugAppendSeasonStartYears();

                ret_string = ret_string + DebugAppendSeasonStatus();
            }

            if (i_b_active_xml_content)
            {
                string xdocument_active_doc_str = @"";

                if (GetObjectActiveDoc() != null)
                {
                    xdocument_active_doc_str = GetObjectActiveDoc().ToString();
                    ret_string = ret_string + @"Content of the active doc XML object" + NewLine();
                    ret_string = ret_string + xdocument_active_doc_str;
                }
            }

            return ret_string;

        } // DebugGetDocStateData

        /// <summary>Append the lengths of the doc arrays</summary>
        static private string DebugAppendArraySizes()
        {
            string ret_string = @"";

            if (!DocInitialized())
            {
                return ret_string;
            }

            ret_string = ret_string + @"Length of array GetObjectAllDocs() (m_xdocument_documents) is " + m_xdocument_documents.Length.ToString() + NewLine();
            ret_string = ret_string + @"Length of array GetFileNamesAllDocs() (m_xdocument_doc_file_names) is " + m_xdocument_doc_file_names.Length.ToString() + NewLine();
            ret_string = ret_string + @"Length of array GetSeasonNamesAllDocs() (m_season_names_documents_strings) is " + m_season_names_documents_strings.Length.ToString() + NewLine();
            ret_string = ret_string + @"Length of array m_seasons_documents_start_years is " + m_seasons_documents_start_years.Length.ToString() + NewLine();
            ret_string = ret_string + @"Length of array m_seasons_documents_status is " + m_seasons_documents_status.Length.ToString() + NewLine();
            ret_string = ret_string + @"" + NewLine();

            return ret_string;

        } // DebugAppendArraySizes

        /// <summary>Append active doc values</summary>
        static private string DebugAppendActiveDoc()
        {
            string ret_string = @"";

            if (GetObjectActiveDoc() == null)
            {
                return ret_string;
            }

            ret_string = ret_string + @"Name of the active object XML file GetFileNameActiveObject() (m_xdocument_active_doc_file_name) is " + m_xdocument_active_doc_file_name + NewLine();
            ret_string = ret_string + @"Name of the active object season name GetSeasonNameActiveObject() (m_xdocument_active_doc_season_name) is " + m_xdocument_active_doc_season_name + NewLine();
            ret_string = ret_string + @"" + NewLine();

            return ret_string;

        } // DebugAppendActiveDoc

        /// <summary>Append file names</summary>
        static private string DebugAppendFileNames()
        {
            string ret_string = @"";

            string[] file_names = GetFileNamesAllDocs();

            if (file_names == null || file_names.Length == 0)
            {
                return ret_string;
            }

            ret_string = ret_string + @"GetFileNamesAllDocs()" + NewLine();

            for (int index_name = 0; index_name < file_names.Length; index_name++)
            {
                ret_string = ret_string + index_name.ToString() + @"     " + file_names[index_name] + NewLine();
            }

            ret_string = ret_string + @"" + NewLine();

            return ret_string;

        } // DebugAppendFileNames

        /// <summary>Append season names</summary>
        static private string DebugAppendSeasonNames()
        {
            string ret_string = @"";

            string[] season_names = GetSeasonNamesAllDocs();

            if (season_names == null || season_names.Length == 0)
            {
                return ret_string;
            }

            ret_string = ret_string + @"GetSeasonNamesAllDocs()" + NewLine();

            for (int index_name = 0; index_name < season_names.Length; index_name++)
            {
                ret_string = ret_string + index_name.ToString() + @"     " + season_names[index_name] + NewLine();
            }

            ret_string = ret_string + @"" + NewLine();

            return ret_string;

        } // DebugAppendSeasonNames

        /// <summary>Append season start years</summary>
        static private string DebugAppendSeasonStartYears()
        {
            string ret_string = @"";

            if (m_seasons_documents_start_years == null || m_seasons_documents_start_years.Length == 0)
            {
                return ret_string;
            }

            ret_string = ret_string + @"m_seasons_documents_start_years" + NewLine();

            for (int index_name = 0; index_name < m_seasons_documents_start_years.Length; index_name++)
            {
                ret_string = ret_string + index_name.ToString() + @"     " + m_seasons_documents_start_years[index_name].ToString() + NewLine();
            }

            ret_string = ret_string + @"" + NewLine();

            return ret_string;

        } // DebugAppendSeasonStartYears

        /// <summary>Append season status</summary>
        static private string DebugAppendSeasonStatus()
        {
            string ret_string = @"";

            if (m_seasons_documents_status == null || m_seasons_documents_status.Length == 0)
            {
                return ret_string;
            }

            ret_string = ret_string + @"m_seasons_documents_status" + NewLine();

            for (int index_name = 0; index_name < m_seasons_documents_status.Length; index_name++)
            {
                ret_string = ret_string + index_name.ToString() + @"     " + m_seasons_documents_status[index_name].ToString() + NewLine();
            }

            ret_string = ret_string + @"" + NewLine();

            return ret_string;

        } // DebugAppendSeasonStatus

        /// <summary>Returns new line</summary>
        static private string NewLine() { return "\r\n"; }

        #endregion // Debug

    } // JazzXml

} // namespace