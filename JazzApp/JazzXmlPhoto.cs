using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Functions for the initialization (creation) of XML objects from the XML files JazzGalerieEin.xml and JazzGalerieZwei.xml
    /// <para>The XML files hold photo gallery data.</para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {
        #region XML objects, file names and directory names

        /// <summary>The XML document (object) that corresponds to the gallery one XML file (JazzGalerieEin.xml)</summary>
        static private XDocument m_xdocument_photo_one = null;

        /// <summary>The XML document (object) that corresponds to the gallery two XML file (JazzGalerieZwei.xml)</summary>
        static private XDocument m_xdocument_photo_two = null;

        /// <summary>Returns the XML document (object) that corresponds to the gallery one XML file (JazzGalerieEin.xml)</summary>
        static public XDocument GetObjectPhotoOne() { return m_xdocument_photo_one; }

        /// <summary>Returns the XML document (object) that corresponds to the gallery two XML file (JazzGalerieTwo.xml)</summary>
        static public XDocument GetObjectPhotoTwo() { return m_xdocument_photo_two; }

        /// <summary>Sets the XML document (object) that corresponds to the gallery two XML file (JazzGalerieTwo.xml)</summary>
        static public void SetObjectPhotoTwo(XDocument i_xdocument_photo_two) { m_xdocument_photo_two = i_xdocument_photo_two; }

        /// <summary>Status for m_xdocument_photo_one</summary>
        static private int m_xdocument_photo_one_status = -12345;

        /// <summary>Status for m_xdocument_photo_two</summary>
        static private int m_xdocument_photo_two_status = -12345;

        /// <summary>Error message for the creation of m_xdocument_photo_one</summary>
        static private string m_xdocument_photo_one_error = "Not set";

        /// <summary>Error message for the creation of m_xdocument_photo_two</summary>
        static private string m_xdocument_photo_two_error = "Not set";

        /// <summary>The name of the gallery one XML file</summary>
        static private string m_photo_one_xml_filename = @"";

        /// <summary>The name of the gallery two XML file</summary>
        static private string m_photo_two_xml_filename = @"";

        /// <summary>Returns the name of the gallery one XML file</summary>
        static public string GetFileNameObjectPhotoOne() { return m_photo_one_xml_filename; }

        /// <summary>Returns the name of the gallery two XML file</summary>
        static public string GetFileNameObjectPhotoTwo() { return m_photo_two_xml_filename; }

        /// <summary>The URL to the folder with the photo gallery XML files</summary>
        static private string m_url_xml_photo_files_folder = @"";

        /// <summary>Returns the URL path to the folder with the photo gallery XML files</summary>
        static public string GetUrlXmlPhotoFiles() { return m_url_xml_photo_files_folder; }

        #endregion // XML objects, file names and directory names

        #region Init functions

        /// <summary>Initialization of the photo gallery parameters 
        /// <para>Initialize the XML object (m_xdocument_photo_one) that corresponds to the photo XML file (JazzGalerieEin.xml). Call of InitXmlDocumentReq</para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_url_xml_photo_files_folder">Server path to the XML photo gallery file<</param>
        /// <param name="i_photo_one_xml_filename">Name of the XML photo gallery file</param>
        /// <param name="o_error">Error message</param>
        static public bool InitPhotoOne(string i_url_xml_photo_files_folder, string i_photo_one_xml_filename, out string o_error)
        {
            o_error = @"";

            m_url_xml_photo_files_folder = i_url_xml_photo_files_folder;
            m_photo_one_xml_filename = i_photo_one_xml_filename;
            // TODO Check data

            string error_message = @"";
            if (!InitXmlDocumentPhotoOne(out error_message))
            {
                o_error = @"JazzXml.InitPhotoOne Programming error " + error_message;
                return false;
            }

            return true;
        } // InitPhotoOne

        /// <summary>Initialize the XML object (m_xdocument_photo_one) that corresponds to the photo gallery one XML file (JazzGalerieEin.xml)</summary>
        static private bool InitXmlDocumentPhotoOne(out string o_error)
        {
            o_error = @"";

            string url_file_photo_one = GetWebSiteUrl() + GetUrlXmlPhotoFiles() + @"/" + GetFileNameObjectPhotoOne();
            if (url_file_photo_one.Trim().Length == 0)
            {
                o_error = @"JazzXml.InitXmlDocumentPhotoOne URL for the templates file is not defined";

                return false;
            }

            JazzOsUtils.LoadXmlDocument(url_file_photo_one, 8, -1);

            string error_message = @"";
            if (GetXmlDocumentPhotoOneStatus(out error_message) < 0)
            {
                o_error = @"JazzXml.InitXmlDocumentPhotoOne Programming error: " + error_message;

                return false;
            }

            // Additional (not necessary) check that object is created
            if (null == GetObjectPhotoOne())
            {
                o_error = @"JazzXml.InitXmlDocumentPhotoOne Photo gallery one object is not created (JazzOsUtils.LoadXmlDocument failed)";

                return false;
            }

            return true;

        } // InitXmlDocumentPhotoOne

        /// <summary>Set the XML object document requests (m_xdocument_photo_one) that corresponds to the photo gallery one XML file (JazzGalerieEin.xml)</summary>
        static public void SetXmlDocumentObjectOne(XDocument i_xdocument_photo_one)
        {
            m_xdocument_photo_one_status = 0;
            m_xdocument_photo_one_error = "";

            m_xdocument_photo_one = i_xdocument_photo_one;
        } // SetXmlDocumentObjectOne

        /// <summary>Set status for document photo one</summary>
        static public void SetXmlDocumentObjectPhotoOneStatus(int i_status, string i_error)
        {
            m_xdocument_photo_one_status = i_status;
            m_xdocument_photo_one_error = i_error;

        } // SetXmlDocumentObjectPhotoOneStatus

        /// <summary>Get status for document photo one</summary>
        static public int GetXmlDocumentPhotoOneStatus(out string o_error)
        {
            o_error = m_xdocument_photo_one_error;

            return m_xdocument_photo_one_status;

        } // GetXmlDocumentPhotoOneStatus



        /// <summary>Initialization of the photo gallery parameters 
        /// <para>Initialize the XML object (m_xdocument_photo_two) that corresponds to the photo XML file (JazzGalerieZwei.xml). Call of InitXmlDocumentReq</para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_url_xml_photo_files_folder">Server path to the XML photo gallery file<</param>
        /// <param name="i_photo_two_xml_filename">Name of the XML photo gallery file</param>
        /// <param name="o_error">Error message</param>
        static public bool InitPhotoTwo(string i_url_xml_photo_files_folder, string i_photo_two_xml_filename, out string o_error)
        {
            o_error = @"";

            m_url_xml_photo_files_folder = i_url_xml_photo_files_folder;
            m_photo_two_xml_filename = i_photo_two_xml_filename;
            // TODO Check data

            string error_message = @"";
            if (!InitXmlDocumentPhotoTwo(out error_message))
            {
                o_error = @"JazzXml.InitPhotoTwo Programming error " + error_message;
                return false;
            }

            return true;
        } // InitPhotoTwo

        /// <summary>Initialize the XML object (m_xdocument_photo_two) that corresponds to the photo gallery two XML file (JazzGalerieZwei.xml)</summary>
        static private bool InitXmlDocumentPhotoTwo(out string o_error)
        {
            o_error = @"";

            string url_file_photo_two = GetWebSiteUrl() + GetUrlXmlPhotoFiles() + @"/" + GetFileNameObjectPhotoTwo();
            if (url_file_photo_two.Trim().Length == 0)
            {
                o_error = @"JazzXml.InitXmlDocumentPhotoTwo URL for the templates file is not defined";

                return false;
            }

            JazzOsUtils.LoadXmlDocument(url_file_photo_two, 9, -1);

            string error_message = @"";
            if (GetXmlDocumentPhotoTwoStatus(out error_message) < 0)
            {
                o_error = @"JazzXml.InitXmlDocumentPhotoOne Programming error: " + error_message;

                return false;
            }

            // Additional (not necessary) check that object is created
            if (null == GetObjectPhotoTwo())
            {
                o_error = @"JazzXml.InitXmlDocumentPhotoTwo Photo gallery two object is not created (JazzOsUtils.LoadXmlDocument failed)";

                return false;
            }

            return true;

        } // InitXmlDocumentPhotoTwo

        /// <summary>Set the XML object document requests (m_xdocument_photo_two) that corresponds to the photo gallery one XML file (JazzGalerieZwei.xml)</summary>
        static public void SetXmlDocumentObjectTwo(XDocument i_xdocument_photo_two)
        {
            m_xdocument_photo_two_status = 0;
            m_xdocument_photo_two_error = "";

            m_xdocument_photo_two = i_xdocument_photo_two;
        } // SetXmlDocumentObjectTwo

        /// <summary>Set status for document photo two</summary>
        static public void SetXmlDocumentObjectPhotoTwoStatus(int i_status, string i_error)
        {
            m_xdocument_photo_two_status = i_status;
            m_xdocument_photo_two_error = i_error;

        } // SetXmlDocumentObjectPhotoTwoStatus

        /// <summary>Get status for document photo two</summary>
        static public int GetXmlDocumentPhotoTwoStatus(out string o_error)
        {
            o_error = m_xdocument_photo_two_error;

            return m_xdocument_photo_two_status;

        } // GetXmlDocumentPhotoTwoStatus

        #endregion // Init functions

        #region Get number of season and photo elements

        /// <summary>Returns the number of seasons in the photo gallery one or two
        /// <para>A negative value will be returned for error</para>
        /// </summary>
        /// <param name="i_b_photo_one">Flag defining if m_xdocument_photo_one or m_xdocument_photo_two will be used</param>
        /// <param name="o_error">Error message</param>
        public static int GetNumberOfPhotoSeasons(bool i_b_photo_one, out string o_error)
        {
            return GetNumberOfFirstLevelElements(GetPhotoXDocument(i_b_photo_one), GetTagPhotosSeason(), out o_error);

        } // GetNumberOfPhotoSeasons


        /// <summary>Returns the number of concerts for a given season in the photo gallery one or two
        /// <para>A negative value will be returned for error</para>
        /// </summary>
        /// <param name="i_b_photo_one">Flag defining if m_xdocument_photo_one or m_xdocument_photo_two will be used</param>
        /// <param name="i_season">Season</param>
        /// <param name="o_error">Error message</param>
        public static int GetNumberOfPhotoConcerts(bool i_b_photo_one, int i_season, out string o_error)
        {
            return GetNumberOfSecondLevelElements(GetPhotoXDocument(i_b_photo_one), GetTagPhotosSeason(), GetTagPhotoConcert(), i_season, out o_error);

        } // GetNumberOfPhotoConcerts


        /// <summary>Returns the number of photo gallery one (JazzFotoGalerieEin.xml) seasons</summary>
        public static int GetNumberOfPhotoOneSeasons(out string o_error)
        {
            return GetNumberOfPhotoSeasons(true, out o_error);

        } // GetNumberOfPhotoOneSeasons

        /// <summary>Returns the number of photo gallery two (JazzFotoGalerieZwei.xml) seasons</summary>
        public static int GetNumberOfPhotoTwoSeasons(out string o_error)
        {
            return GetNumberOfPhotoSeasons(false, out o_error);

        } // GetNumberOfPhotoTwoSeasons

        /// <summary>Returns the number of concerts for a given season in the photo gallery one (JazzFotoGalerieEin.xml). Negative value is returned for error</summary>
        public static int GetNumberOfPhotoOneConcerts(int i_season, out string o_error)
        {
            return GetNumberOfPhotoConcerts(true, i_season, out o_error);

        } // GetNumberOfPhotoOneConcerts

        /// <summary>Returns the number of concerts for a given season in the photo gallery one (JazzFotoGalerieZwei.xml) Negative value is returned for error</summary>
        public static int GetNumberOfPhotoTwoConcerts(int i_season, out string o_error)
        {
            return GetNumberOfPhotoConcerts(false, i_season, out o_error);

        } // GetNumberOfPhotoTwoConcerts

        #endregion // Get number of season and photo elements

        #region Get and set photo element values

        // There are no elements for GetInnerTextPhotoOneSingleNode/SetInnerTextPhotoOneSingleNode

        /// <summary>Returns the season start year as string: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneStartYearSeason(int i_season) { return GetInnerTextPhotoOneNode(i_season, GetTagPhotoStartYearSeason()); }

        /// <summary>Sets the season start year as a string: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneStartYearSeason(int i_season, string i_start_year) { SetInnerTextPhotoOneNode(i_season, GetTagPhotoStartYearSeason(), i_start_year); }

        /// <summary>Returns the season start year as string: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoStartYearSeason(int i_season) { return GetInnerTextPhotoTwoNode(i_season, GetTagPhotoStartYearSeason()); }

        /// <summary>Sets the season start year as a string: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoStartYearSeason(int i_season, string i_start_year) { SetInnerTextPhotoTwoNode(i_season, GetTagPhotoStartYearSeason(), i_start_year); }


        /// <summary>Returns the name of the band for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneBandName(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoBandName()); }

        /// <summary>Sets the name of a band for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneBandName(int i_season, int i_concert, string i_band_name) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoBandName(), i_band_name); }

        /// <summary>Returns the name of the band for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoBandName(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoBandName()); }

        /// <summary>Sets the name of a band for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoBandName(int i_season, int i_concert, string i_band_name) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoBandName(), i_band_name); }

        /// <summary>Returns the year for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneYear(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoYear()); }

        /// <summary>Sets the year for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneYear(int i_season, int i_concert, string i_year) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoYear(), i_year); }

        /// <summary>Returns the year for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoYear(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoYear()); }

        /// <summary>Sets the year for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoYear(int i_season, int i_concert, string i_year) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoYear(), i_year); }

        /// <summary>Returns the month for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneMonth(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoMonth()); }

        /// <summary>Sets the month for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneMonth(int i_season, int i_concert, string i_month) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoMonth(), i_month); }

        /// <summary>Returns the month for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoMonth(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoMonth()); }

        /// <summary>Sets the month for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoMonth(int i_season, int i_concert, string i_month) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoMonth(), i_month); }

        /// <summary>Returns the day for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneDay(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoDay()); }

        /// <summary>Sets the day for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneDay(int i_season, int i_concert, string i_day) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoDay(), i_day); }

        /// <summary>Returns the day for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoDay(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoDay()); }

        /// <summary>Sets the day for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoDay(int i_season, int i_concert, string i_day) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoDay(), i_day); }

        /// <summary>Returns the gallery name for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneGalleryName(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagGalleryName()); }

        /// <summary>Sets the gallery name for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneGalleryName(int i_season, int i_concert, string i_gallery_name) { SetPhotoOneInnerText(i_season, i_concert, GetTagGalleryName(), i_gallery_name); }

        /// <summary>Returns the gallery name for a given season and concert: Photo gallery one (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoGalleryName(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagGalleryName()); }

        /// <summary>Sets the gallery name for a given season and concert: Photo gallery one (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoGalleryName(int i_season, int i_concert, string i_gallery_name) { SetPhotoTwoInnerText(i_season, i_concert, GetTagGalleryName(), i_gallery_name); }

        /// <summary>Returns text one for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextOne(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextOne()); }

        /// <summary>Sets text one for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextOne(int i_season, int i_concert, string i_text_one) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextOne(), i_text_one); }

        /// <summary>Returns text one for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextOne(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextOne()); }

        /// <summary>Sets text one for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextOne(int i_season, int i_concert, string i_text_one) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextOne(), i_text_one); }

        /// <summary>Returns text two for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextTwo(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextTwo()); }

        /// <summary>Sets text two for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextTwo(int i_season, int i_concert, string i_text_two) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextTwo(), i_text_two); }

        /// <summary>Returns text two for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextTwo(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextTwo()); }

        /// <summary>Sets text two for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextTwo(int i_season, int i_concert, string i_text_two) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextTwo(), i_text_two); }

        /// <summary>Returns text three for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextThree(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextThree()); }

        /// <summary>Sets text three for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextThree(int i_season, int i_concert, string i_text_three) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextThree(), i_text_three); }

        /// <summary>Returns text three for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextThree(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextThree()); }

        /// <summary>Sets text three for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextThree(int i_season, int i_concert, string i_text_three) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextThree(), i_text_three); }

        /// <summary>Returns text four for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextFour(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextFour()); }

        /// <summary>Sets text four for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextFour(int i_season, int i_concert, string i_text_four) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextFour(), i_text_four); }

        /// <summary>Returns text four for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextFour(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextFour()); }

        /// <summary>Sets text four for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextFour(int i_season, int i_concert, string i_text_four) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextFour(), i_text_four); }

        /// <summary>Returns text five for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextFive(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextFive()); }

        /// <summary>Sets text five for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextFive(int i_season, int i_concert, string i_text_five) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextFive(), i_text_five); }

        /// <summary>Returns text five for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextFive(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextFive()); }

        /// <summary>Sets text five for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextFive(int i_season, int i_concert, string i_text_five) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextFive(), i_text_five); }

        /// <summary>Returns text six for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextSix(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextSix()); }

        /// <summary>Sets text six for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextSix(int i_season, int i_concert, string i_text_six) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextSix(), i_text_six); }

        /// <summary>Returns text six for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextSix(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextSix()); }

        /// <summary>Sets text six for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextSix(int i_season, int i_concert, string i_text_six) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextSix(), i_text_six); }

        /// <summary>Returns text seven for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextSeven(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextSeven()); }

        /// <summary>Sets text seven for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextSeven(int i_season, int i_concert, string i_text_seven) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextSeven(), i_text_seven); }

        /// <summary>Returns text seven for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextSeven(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextSeven()); }

        /// <summary>Sets text seven for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextSeven(int i_season, int i_concert, string i_text_seven) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextSeven(), i_text_seven); }

        /// <summary>Returns text eight for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextEight(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextEight()); }

        /// <summary>Sets text eight for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextEight(int i_season, int i_concert, string i_text_eight) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextEight(), i_text_eight); }

        /// <summary>Returns text eight for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextEight(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextEight()); }

        /// <summary>Sets text eight for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextEight(int i_season, int i_concert, string i_text_eight) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextEight(), i_text_eight); }

        /// <summary>Returns text nine for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneTextNine(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextNine()); }

        /// <summary>Sets text nine for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneTextNine(int i_season, int i_concert, string i_text_nine) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoTextNine(), i_text_nine); }

        /// <summary>Returns text nine for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoTextNine(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextNine()); }

        /// <summary>Sets text nine for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoTextNine(int i_season, int i_concert, string i_text_nine) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoTextNine(), i_text_nine); }

        /// <summary>Returns photographer name for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOnePhotographerName(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotographerName()); }

        /// <summary>Sets photographer name for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOnePhotographerName(int i_season, int i_concert, string i_text_photographer_name) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotographerName(), i_text_photographer_name); }

        /// <summary>Returns photographer name for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoPhotographerName(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotographerName()); }

        /// <summary>Sets photographer name for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoPhotographerName(int i_season, int i_concert, string i_text_photographer_name) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotographerName(), i_text_photographer_name); }

        /// <summary>Returns ZIP file name for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneZipName(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoZipName()); }

        /// <summary>Sets ZIP file name for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneZipName(int i_season, int i_concert, string i_text_zip_name) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoZipName(), i_text_zip_name); }

        /// <summary>Returns ZIP file name for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoZipName(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoZipName()); }

        /// <summary>Sets ZIP file name for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoZipName(int i_season, int i_concert, string i_text_zip_name) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoZipName(), i_text_zip_name); }

        /// <summary>Returns  concert number for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public string GetPhotoOneConcertNumber(int i_season, int i_concert) { return GetPhotoOneInnerText(i_season, i_concert, GetTagPhotoConcertNumber()); }

        /// <summary>Sets concert number for a given season and concert: Photo gallery one (JazzFotoGalerieEin.xml)</summary>
        static public void SetPhotoOneConcertNumber(int i_season, int i_concert, string i_text_concert_number) { SetPhotoOneInnerText(i_season, i_concert, GetTagPhotoConcertNumber(), i_text_concert_number); }

        /// <summary>Returns  concert number for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public string GetPhotoTwoConcertNumber(int i_season, int i_concert) { return GetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoConcertNumber()); }

        /// <summary>Sets concert number for a given season and concert: Photo gallery two (JazzFotoGalerieZwei.xml)</summary>
        static public void SetPhotoTwoConcertNumber(int i_season, int i_concert, string i_text_concert_number) { SetPhotoTwoInnerText(i_season, i_concert, GetTagPhotoConcertNumber(), i_text_concert_number); }


        #endregion // Get and set photo element values

        #region Get functions for objects JazzPhoto

        /// <summary>Returns all photo one objects (JazzPhoto) for a given season</summary>
        static public JazzPhoto[] GetPhotoOneObjects(int i_season_number, out string o_error)
        {
            o_error = @"";
            JazzPhoto[] ret_photos = null;

            int n_number_seasons_one = -12345;
            if (!CheckInputSeasonNumberOne(i_season_number, out n_number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.GetPhotoOneObjects JazzXml.CheckInputSeasonNumberOne failed " + o_error;
                return ret_photos;
            }

            int n_number_concerts_one = JazzXml.GetNumberOfPhotoOneConcerts(i_season_number, out o_error);
            if (n_number_concerts_one <= 0)
            {
                o_error = @"JazzXmlPhoto.GetPhotoOneObjects JazzXml.GetNumberOfPhotoOneConcerts failed " + o_error;
                return ret_photos;
            }

            ret_photos = new JazzPhoto[n_number_concerts_one];

            for (int concert_number_one = 1; concert_number_one <= n_number_concerts_one; concert_number_one++)
            {
                JazzPhoto current_object_one = new JazzPhoto();

                current_object_one.BandName = GetPhotoOneBandName(i_season_number, concert_number_one);
                current_object_one.Year = GetPhotoOneYear(i_season_number, concert_number_one);
                current_object_one.Month = GetPhotoOneMonth(i_season_number, concert_number_one);
                current_object_one.Day = GetPhotoOneDay(i_season_number, concert_number_one);
                current_object_one.GalleryName = GetPhotoOneGalleryName(i_season_number, concert_number_one);
                current_object_one.TextOne = GetPhotoOneTextOne(i_season_number, concert_number_one);
                current_object_one.TextTwo = GetPhotoOneTextTwo(i_season_number, concert_number_one);
                current_object_one.TextThree = GetPhotoOneTextThree(i_season_number, concert_number_one);
                current_object_one.TextFour = GetPhotoOneTextFour(i_season_number, concert_number_one);
                current_object_one.TextFive = GetPhotoOneTextFive(i_season_number, concert_number_one);
                current_object_one.TextSix = GetPhotoOneTextSix(i_season_number, concert_number_one);
                current_object_one.TextSeven = GetPhotoOneTextSeven(i_season_number, concert_number_one);
                current_object_one.TextEight = GetPhotoOneTextEight(i_season_number, concert_number_one);
                current_object_one.TextNine = GetPhotoOneTextNine(i_season_number, concert_number_one);
                current_object_one.PhotographerName = GetPhotoOnePhotographerName(i_season_number, concert_number_one);
                current_object_one.ZipName = GetPhotoOneZipName(i_season_number, concert_number_one);
                current_object_one.ConcertNumber = GetPhotoOneConcertNumber(i_season_number, concert_number_one);

                current_object_one.SetToEmptyStringsForValuesNotYetSet();

                //TODO if (!current_object_one.CheckParameterValues(out o_error))
                //TODO {
                //TODO o_error = @"JazzXmlPhoto.GetPhotoOneObjects JazzPhoto value not OK " + o_error;
                //TODO return null;
                //TODO }

                ret_photos[concert_number_one - 1] = current_object_one;

            } // concert_number_one

            return ret_photos;

        } // GetPhotoOneObjects

        /// <summary>Returns all photo two objects (JazzPhoto) for a given season</summary>
        static public JazzPhoto[] GetPhotoTwoObjects(int i_season_number, out string o_error)
        {
            o_error = @"";
            JazzPhoto[] ret_photos = null;

            int n_number_seasons_two = -12345;
            if (!CheckInputSeasonNumberTwo(i_season_number, out n_number_seasons_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.GetPhotoOneObjects JazzXml.CheckInputSeasonNumberTwo failed " + o_error;
                return ret_photos;
            }

            int n_number_concerts_two = JazzXml.GetNumberOfPhotoTwoConcerts(i_season_number, out o_error);
            if (n_number_concerts_two <= 0)
            {
                o_error = @"JazzXmlPhoto.GetPhotoTwoObjects JazzXml.GetNumberOfPhotoTwoConcerts failed " + o_error;
                return ret_photos;
            }

            ret_photos = new JazzPhoto[n_number_concerts_two];

            for (int concert_number_two = 1; concert_number_two <= n_number_concerts_two; concert_number_two++)
            {
                JazzPhoto current_object_two = new JazzPhoto();

                current_object_two.BandName = GetPhotoTwoBandName(i_season_number, concert_number_two);
                current_object_two.Year = GetPhotoTwoYear(i_season_number, concert_number_two);
                current_object_two.Month = GetPhotoTwoMonth(i_season_number, concert_number_two);
                current_object_two.Day = GetPhotoTwoDay(i_season_number, concert_number_two);
                current_object_two.GalleryName = GetPhotoTwoGalleryName(i_season_number, concert_number_two);
                current_object_two.TextOne = GetPhotoTwoTextOne(i_season_number, concert_number_two);
                current_object_two.TextTwo = GetPhotoTwoTextTwo(i_season_number, concert_number_two);
                current_object_two.TextThree = GetPhotoTwoTextThree(i_season_number, concert_number_two);
                current_object_two.TextFour = GetPhotoTwoTextFour(i_season_number, concert_number_two);
                current_object_two.TextFive = GetPhotoTwoTextFive(i_season_number, concert_number_two);
                current_object_two.TextSix = GetPhotoTwoTextSix(i_season_number, concert_number_two);
                current_object_two.TextSeven = GetPhotoTwoTextSeven(i_season_number, concert_number_two);
                current_object_two.TextEight = GetPhotoTwoTextEight(i_season_number, concert_number_two);
                current_object_two.TextNine = GetPhotoTwoTextNine(i_season_number, concert_number_two);
                current_object_two.PhotographerName = GetPhotoTwoPhotographerName(i_season_number, concert_number_two);
                current_object_two.ZipName = GetPhotoTwoZipName(i_season_number, concert_number_two);
                current_object_two.ConcertNumber = GetPhotoTwoConcertNumber(i_season_number, concert_number_two);

                current_object_two.SetToEmptyStringsForValuesNotYetSet();

                //TODO if (!current_object_two.CheckParameterValues(out o_error))
                //TODO {
                //TODO o_error = @"JazzXmlPhoto.GetPhotoTwoObjects JazzPhoto value not OK " + o_error;
                //TODO return null;
                //TODO }

                ret_photos[concert_number_two - 1] = current_object_two;

            } // concert_number_two

            return ret_photos;

        } // GetPhotoTwoObjects

        #endregion // Get functions for objects JazzPhoto

        #region Get season functions 

        /// <summary>Returns the season number (as int) for the input start season year. Eq. 0: No seasons elements Eq. -1: No season element for the input year. Other negative values for error</summary>
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_season_start_year_str">Start season year as string</param>
        /// <param name="o_error">Error message</param>
        public static int GetSeasonNumber(bool i_b_photo_one, string i_season_start_year_str, out string o_error)
        {
            o_error = @"";

            XDocument xdocument_photo = GetPhotoXDocument(i_b_photo_one);

            if (null == xdocument_photo)
            {
                o_error = @"JazzXmlPhoto.GetSeasonNumber xdocument_photo is null";
                return -99;
            }

            int current_season_number = 0;

            bool there_are_season_elements = false;

            foreach (XElement element_season in xdocument_photo.Descendants(GetTagPhotoStartYearSeason()))
            {
                there_are_season_elements = true;

                current_season_number = current_season_number + 1;
                string start_year = element_season.Value;

                if (start_year.Equals(i_season_start_year_str))
                {
                    return current_season_number;
                }

            } // element_season

            if (!there_are_season_elements)
            {
                // There are no season elements
                return 0;
            }

            // There is no season element i_season_start_year_str
            return -1;

        } // GetSeasonNumber

        #endregion // Get season functions

        #region Utility functions

        /// <summary>Returns XDocument object for photo gallery one (JazzFotoGalerieEin.xml) or gallery two (JazzFotoGalerieZwei.xml)
        /// <para></para>
        /// </summary>
        /// <param name="i_b_photo_one">Flag defining if m_xdocument_photo_one or m_xdocument_photo_two will be used</param>
        private static XDocument GetPhotoXDocument(bool i_b_photo_one)
        {
            if (i_b_photo_one)
            {
                return m_xdocument_photo_one;
            }
            else
            {
                return m_xdocument_photo_two;
            }
        } // GetPhotoXDocument

        /// <summary>Returns element (XElement) for photo gallery one (JazzFotoGalerieEin.xml) or gallery two (JazzFotoGalerieZwei.xml)
        /// <para></para>
        /// </summary>
        /// <param name="i_b_photo_one">Flag defining if m_xdocument_photo_one or m_xdocument_photo_two will be used</param>
        private static XElement GetGalleryXElement(bool i_b_photo_one)
        {
            if (i_b_photo_one)
            {
                return m_xdocument_photo_one.Element(GetTagGalleryOne());
            }
            else
            {
                return m_xdocument_photo_two.Element(GetTagGalleryTwo());
            }

        } // GetGalleryXElement

        /// <summary>Returns the value (inner text) for a photo gallery one (JazzFotoGalerieEin.xml) element node</summary>
        ///  <param name="i_season">Season number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_concert">Concert number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetPhotoOneInnerText(int i_season, int i_concert, String i_tag_name)
        {
            if (!CheckTagPhoto(i_tag_name))
                return "JazzXml.GetPhotoOneInnerText Not a defined tag name " + i_tag_name;

            return GetSecondLevelInnerText(m_xdocument_photo_one, GetTagPhotosSeason(), GetTagPhotoConcert(), i_season, i_concert, i_tag_name);

        } // GetPhotoOneInnerText

        /// <summary>Returns the value (inner text) for a photo gallery two (JazzFotoGalerieZwei.xml) element node</summary>
        ///  <param name="i_season">Season number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_concert">Concert number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetPhotoTwoInnerText(int i_season, int i_concert, String i_tag_name)
        {
            if (!CheckTagPhoto(i_tag_name))
                return "JazzXml.GetPhotoTwoInnerText Not a defined tag name " + i_tag_name;

            return GetSecondLevelInnerText(m_xdocument_photo_two, GetTagPhotosSeason(), GetTagPhotoConcert(), i_season, i_concert, i_tag_name);

        } // GetPhotoTwoInnerText

        /// <summary>Checks the input season number for photo gallery one or two
        /// <para></para>
        /// </summary>
        /// <param name="i_b_photo_one">Flag defining if m_xdocument_photo_one or m_xdocument_photo_two will be used</param>
        /// <param name="o_error">Error message</param>
        static private bool CheckInputSeasonNumber(bool i_b_photo_one, int i_season_number, out int o_number_seasons, out string o_error)
        {
            o_error = @"";

            o_number_seasons = -12345;

            if (i_season_number <= 0)
            {
                o_error = @"JazzXmlPhoto.CheckInputSeasonNumber  i_season_number= " + i_season_number.ToString() + " <= 0 ";
                return false;
            }

            o_number_seasons = JazzXml.GetNumberOfPhotoSeasons(i_b_photo_one, out o_error);
            
            if (o_number_seasons < 0)
            {
                o_error = @"JazzXmlPhoto.CheckInputSeasonNumber JazzXml.GetNumberOfPhotoSeasons failed " + o_error;
                return false;
            }

            if (i_season_number > o_number_seasons)
            {
                o_error = @"JazzXmlPhoto.CheckInputSeasonNumber  i_season_number= " + i_season_number.ToString() + " > o_number_seasons_one= " + o_number_seasons.ToString();
                return false;
            }

            return true;

        } // CheckInputSeasonNumber

        /// <summary>Checks the input concert number for photo gallery one or two</summary>
        /// <param name="i_b_photo_one">Flag defining if m_xdocument_photo_one or m_xdocument_photo_two will be used</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number.</param>
        /// <param name="o_number_concerts">Total number of concerts for the input season</param>
        /// <param name="o_error">Error message</param>
        static private bool CheckInputConcertNumber(bool i_b_photo_one, int i_season_number, int i_concert_number, out int o_number_concerts, out string o_error)
        {
            o_error = @"";

            o_number_concerts = -12345;

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumber  i_concert_number= " + i_concert_number.ToString() + " <= 0";
                return false;
            }

            int number_seasons = -12345;
            if (!CheckInputSeasonNumber(i_b_photo_one, i_season_number, out number_seasons, out o_error))
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumber CheckInputSeasonNumber failed " + o_error;
                return false;
            }

            o_number_concerts = JazzXml.GetNumberOfPhotoConcerts(i_b_photo_one, i_season_number, out o_error);
            if (o_number_concerts == 0)
            {
                if (i_concert_number != 0)
                {
                    o_error = @"JazzXmlPhoto.CheckInputConcertNumber There are no photo objects and i_concert_number= " + i_concert_number.ToString() + " is not zero (0)";
                    return false;
                }
                else
                {
                    return true;
                }

            } // No elements
            else if (o_number_concerts == -2) // TODO Check if this really means no concerts
            {
                // There are no elements
                o_number_concerts = 0;

                if (i_concert_number != 0)
                {
                    o_error = @"JazzXmlPhoto.CheckInputConcertNumber There are no photo objects and i_concert_number= " + i_concert_number.ToString() + " is not zero (0)";
                    return false;
                }
                else
                {
                    return true;
                }

            } // No elements
            else if (o_number_concerts < 0)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberOne JazzXml.GetNumberOfPhotoConcerts failed " + o_error;
                return false;
            }

            if (i_concert_number > o_number_concerts)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumber  i_concert_number= " + i_concert_number.ToString() + " > o_number_concerts= " + o_number_concerts.ToString();
                return false;
            }

            return true;

        } // CheckInputConcertNumber

        /// <summary>Check input season number one</summary>
        static private bool CheckInputSeasonNumberOne(int i_season_number, out int o_number_seasons_one, out string o_error)
        {
            return CheckInputSeasonNumber(true, i_season_number, out o_number_seasons_one, out o_error);

        } // CheckInputSeasonNumberOne

        /// <summary>Check input season number two</summary>
        static private bool CheckInputSeasonNumberTwo(int i_season_number, out int o_number_seasons_two, out string o_error)
        {
            return CheckInputSeasonNumber(false, i_season_number, out o_number_seasons_two, out o_error);

        } // CheckInputSeasonNumberTwo

        /// <summary>Checks the input concert number for photo gallery one</summary>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number.</param>
        /// <param name="o_error">Error message</param>
        static private bool CheckInputConcertNumberOne(int i_season_number, int i_concert_number, out int o_number_concerts_one, out string o_error)
        {
            o_error = @"";

            o_number_concerts_one = -12345;

            int number_seasons_one = -12345;
            if (!CheckInputSeasonNumberOne(i_season_number, out number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberOne CheckInputSeasonNumberOne failed " + o_error;
                return false;
            }

            o_number_concerts_one = JazzXml.GetNumberOfPhotoOneConcerts(i_season_number, out o_error);
            if (o_number_concerts_one == 0) 
            {
                if (i_concert_number != 0)
                {
                    o_error = @"JazzXmlPhoto.CheckInputConcertNumberOne There are no photo objects and i_concert_number= " + i_concert_number.ToString() + " is not zero (0)";
                    return false;
                }
                else
                {
                    return true;
                }

            } // No elements
            else if (o_number_concerts_one == -2) // TODO Check if this really means no concerts
            {
                // There are no elements
                o_number_concerts_one = 0;

                if (i_concert_number != 0)
                {
                    o_error = @"JazzXmlPhoto.CheckInputConcertNumberOne There are no photo objects and i_concert_number= " + i_concert_number.ToString() + " is not zero (0)";
                    return false;
                }
                else
                {
                    return true;
                }

            } // No elements
            else if (o_number_concerts_one < 0)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberOne JazzXml.GetNumberOfPhotoOneConcerts failed " + o_error;
                return false;
            }

            if (i_concert_number < 0 || i_concert_number > o_number_concerts_one)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberOne  i_concert_number= " + i_concert_number.ToString() + " <= 0 or > o_number_concerts_one= " + o_number_concerts_one.ToString();
                return false;
            }

            return true;

        } // CheckInputConcertNumberOne

        /// <summary>Checks the input concert number for photo gallery two</summary>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number.</param>
        /// <param name="o_error">Error message</param>
        static private bool CheckInputConcertNumberTwo(int i_season_number, int i_concert_number, out int o_number_concerts_two, out string o_error)
        {
            o_error = @"";

            o_number_concerts_two = -12345;

            int number_seasons_two = -12345;
            if (!CheckInputSeasonNumberTwo(i_season_number, out number_seasons_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberTwo CheckInputSeasonNumberTwo failed " + o_error;
                return false;
            }

            o_number_concerts_two = JazzXml.GetNumberOfPhotoTwoConcerts(i_season_number, out o_error);
            if (o_number_concerts_two == 0)
            {
                if (i_concert_number != 0)
                {
                    o_error = @"JazzXmlPhoto.CheckInputConcertNumberTwo There are no photo objects and i_concert_number= " + i_concert_number.ToString() + " is not zero (0)";
                    return false;
                }
                else
                {
                    return true;
                }

            } // No elements
            else if (o_number_concerts_two == -2)
            {
                // There are no elements
                o_number_concerts_two = 0;

                if (i_concert_number != 0)
                {
                    o_error = @"JazzXmlPhoto.CheckInputConcertNumberTwo There are no photo objects and i_concert_number= " + i_concert_number.ToString() + " is not zero (0)";
                    return false;
                }
                else
                {
                    return true;
                }

            } // No elements
            else if (o_number_concerts_two < 0)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberTwo JazzXml.GetNumberOfPhotoOneConcerts failed " + o_error;
                return false;
            }

            if (i_concert_number < 0 || i_concert_number > o_number_concerts_two)
            {
                o_error = @"JazzXmlPhoto.CheckInputConcertNumberTwo  i_concert_number= " + i_concert_number.ToString() + " <= 0 or > o_number_concerts_two= " + o_number_concerts_two.ToString();
                return false;
            }

            return true;

        } // CheckInputConcertNumberTwo

        #endregion // Utility functions

        #region Set photo object

        /// <summary>Set photo one object data. Empty strings will be replaced by GetUndefinedNodeValue().</summary>
        static public bool SetPhotoOne(int i_season_number, int i_concert_number, JazzPhoto i_jazz_photo, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.SetPhotoOne Programming error: Input JazzPhoto is null";
                return false;
            }

            int n_number_seasons_one = -12345;
            if (!CheckInputSeasonNumberOne(i_season_number, out n_number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.SetPhotoOne JazzXml.CheckInputSeasonNumberOne failed " + o_error;
                return false;
            }

            int number_concerts_one = JazzXml.GetNumberOfPhotoOneConcerts(i_season_number, out o_error);
            if (number_concerts_one <= 0)
            {
                o_error = @"JazzXmlPhoto.SetPhotoOne JazzXml.GetNumberOfPhotoOneConcerts failed " + o_error;
                return false;
            }

            if (i_concert_number < 1 || i_concert_number > number_concerts_one)
            {
                o_error = @"JazzXmlPhoto.SetPhotoOne  i_concert_number= " + i_concert_number.ToString() + " < 1 or > number_concerts_one= " + number_concerts_one.ToString();
                return false;
            }

            if (!SetXmlPhotoOne(i_jazz_photo, i_season_number, i_concert_number))
            {
                o_error = @"JazzXmlPhoto.SetPhotoOne JazzXml.SetXmlPhotoOne failed";
                return false;
            }

            return true;

        } // SetPhotoOne

        /// <summary>Set photo two object data. Empty strings will be replaced by GetUndefinedNodeValue().</summary>
        static public bool SetPhotoTwo(int i_season_number, int i_concert_number, JazzPhoto i_jazz_photo, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.SetPhotoTwo Programming error: Input JazzPhoto is null";
                return false;
            }

            int n_number_seasons_two = -12345;
            if (!CheckInputSeasonNumberTwo(i_season_number, out n_number_seasons_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.SetPhotoTwo JazzXml.CheckInputSeasonNumberTwo failed " + o_error;
                return false;
            }

            int number_concerts_two = JazzXml.GetNumberOfPhotoTwoConcerts(i_season_number, out o_error);
            if (number_concerts_two <= 0)
            {
                o_error = @"JazzXmlPhoto.SetPhotoTwo JazzXml.GetNumberOfPhotoTwoConcerts failed " + o_error;
                return false;
            }

            if (i_concert_number < 1 || i_concert_number > number_concerts_two)
            {
                o_error = @"JazzXmlPhoto.SetPhotoTwo  i_concert_number= " + i_concert_number.ToString() + " < 1 or > number_concerts_one= " + number_concerts_two.ToString();
                return false;
            }

            if (!SetXmlPhotoTwo(i_jazz_photo, i_season_number, i_concert_number))
            {
                o_error = @"JazzXmlPhoto.SetPhotoTwo JazzXml.SetXmlPhotoTwo failed";
                return false;
            }

            return true;

        } // SetPhotoOne

        /// <summary>Set XML photo one data. Object GetObjectWithUndefinedNodeValues is set</summary>
        static private bool SetXmlPhotoOne(JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number)
        {
            if (null == i_jazz_photo)
                return false; // Programming error

            JazzPhoto object_undef_values = i_jazz_photo.GetObjectWithUndefinedNodeValues(i_jazz_photo);

            SetPhotoOneBandName(i_season_number, i_concert_number, object_undef_values.BandName);
            SetPhotoOneYear(i_season_number, i_concert_number, object_undef_values.Year);
            SetPhotoOneMonth(i_season_number, i_concert_number, object_undef_values.Month);
            SetPhotoOneDay(i_season_number, i_concert_number, object_undef_values.Day);
            SetPhotoOneGalleryName(i_season_number, i_concert_number, object_undef_values.GalleryName);
            SetPhotoOneTextOne(i_season_number, i_concert_number, object_undef_values.TextOne);
            SetPhotoOneTextTwo(i_season_number, i_concert_number, object_undef_values.TextTwo);
            SetPhotoOneTextThree(i_season_number, i_concert_number, object_undef_values.TextThree);
            SetPhotoOneTextFour(i_season_number, i_concert_number, object_undef_values.TextFour);
            SetPhotoOneTextFive(i_season_number, i_concert_number, object_undef_values.TextFive);
            SetPhotoOneTextSix(i_season_number, i_concert_number, object_undef_values.TextSix);
            SetPhotoOneTextSeven(i_season_number, i_concert_number, object_undef_values.TextSeven);
            SetPhotoOneTextEight(i_season_number, i_concert_number, object_undef_values.TextEight);
            SetPhotoOneTextNine(i_season_number, i_concert_number, object_undef_values.TextNine);
            SetPhotoOnePhotographerName(i_season_number, i_concert_number, object_undef_values.PhotographerName);
            SetPhotoOneZipName(i_season_number, i_concert_number, object_undef_values.ZipName);
            SetPhotoOneConcertNumber(i_season_number, i_concert_number, object_undef_values.ConcertNumber);

            return true;

        } // SetXmlPhotoOne

        /// <summary>Set XML photo two data. Object GetObjectWithUndefinedNodeValues is set</summary>
        static private bool SetXmlPhotoTwo(JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number)
        {
            if (null == i_jazz_photo)
                return false; // Programming error

            JazzPhoto object_undef_values = i_jazz_photo.GetObjectWithUndefinedNodeValues(i_jazz_photo);

            SetPhotoTwoBandName(i_season_number, i_concert_number, object_undef_values.BandName);
            SetPhotoTwoYear(i_season_number, i_concert_number, object_undef_values.Year);
            SetPhotoTwoMonth(i_season_number, i_concert_number, object_undef_values.Month);
            SetPhotoTwoDay(i_season_number, i_concert_number, object_undef_values.Day);
            SetPhotoTwoGalleryName(i_season_number, i_concert_number, object_undef_values.GalleryName);
            SetPhotoTwoTextOne(i_season_number, i_concert_number, object_undef_values.TextOne);
            SetPhotoTwoTextTwo(i_season_number, i_concert_number, object_undef_values.TextTwo);
            SetPhotoTwoTextThree(i_season_number, i_concert_number, object_undef_values.TextThree);
            SetPhotoTwoTextFour(i_season_number, i_concert_number, object_undef_values.TextFour);
            SetPhotoTwoTextFive(i_season_number, i_concert_number, object_undef_values.TextFive);
            SetPhotoTwoTextSix(i_season_number, i_concert_number, object_undef_values.TextSix);
            SetPhotoTwoTextSeven(i_season_number, i_concert_number, object_undef_values.TextSeven);
            SetPhotoTwoTextEight(i_season_number, i_concert_number, object_undef_values.TextEight);
            SetPhotoTwoTextNine(i_season_number, i_concert_number, object_undef_values.TextNine);
            SetPhotoTwoPhotographerName(i_season_number, i_concert_number, object_undef_values.PhotographerName);
            SetPhotoTwoZipName(i_season_number, i_concert_number, object_undef_values.ZipName);
            SetPhotoTwoConcertNumber(i_season_number, i_concert_number, object_undef_values.ConcertNumber);

            return true;

        } // SetXmlPhotoTwo

        #endregion // Set photo object

        #region Add photo object

        /// <summary>Appends a photo (concert) element to photo season element (defined by a season number)</summary>
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_jazz_photo">Object JazzPhoto that shall be added as a new node</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoAppend(bool i_b_photo_one, JazzPhoto i_jazz_photo, int i_season_number, out string o_error)
        {
            o_error = @"";

            XDocument xdocument_photo = GetPhotoXDocument(i_b_photo_one);

            if (null == xdocument_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoAppend xdocument_photo is null";
                return false;
            }

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoAppend Input JazzPhoto is null";
                return false;
            }

            int number_seasons = -12345;
            if (!CheckInputSeasonNumber(i_b_photo_one, i_season_number, out number_seasons, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoAppend CheckInputSeasonNumberOne failed " + o_error;
                return false;
            }

            XElement photo_element = PhotoElement(i_jazz_photo, out o_error);

            if (null == photo_element)
            {
                o_error = @"JazzXmlPhoto.PhotoAppend PhotoElement failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(xdocument_photo, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.PhotoAppend GetSeasonElement failed " + o_error;
                return false;
            }

            if (!PhotoElementAppend(season_element, photo_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoAppend PhotoElementAppend failed " + o_error;
                return false;
            }

            return true;

        } // PhotoAppend

        /// <summary>Insert a photo (concert) element after the input element defined by the input concert number</summary>
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_jazz_photo">Object JazzPhoto that shall be added as a new node</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object.</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoInsertAfter(bool i_b_photo_one, JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number, out string o_error)
        {
            o_error = @"";

            XElement photo_element = null;
            XElement season_element = null;
            if (!PhotoInsert(i_b_photo_one, i_jazz_photo, i_season_number, i_concert_number, out photo_element, out season_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoInsertAfter PhotoInsert failed " + o_error;
                return false;
            }

            if (!PhotoElementInsertAfter(season_element, photo_element, i_concert_number, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoInsertAfter PhotoElementInsertAfter failed " + o_error;
                return false;
            }

            return true;

        } // PhotoInsertAfter

        /// <summary>Insert a photo (concert) element before the input element defined by the input concert number</summary>
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_jazz_photo">Object JazzPhoto that shall be added as a new node</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number. The object will be added before this object.</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoInsertBefore(bool i_b_photo_one, JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number, out string o_error)
        {
            o_error = @"";

            XElement photo_element = null;
            XElement season_element = null;
            if (!PhotoInsert(i_b_photo_one, i_jazz_photo, i_season_number, i_concert_number, out photo_element, out season_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoInsertBefore PhotoInsert failed " + o_error;
                return false;
            }

            if (!PhotoElementInsertBefore(season_element, photo_element, i_concert_number, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoInsertBefore PhotoElementInsertBefore failed " + o_error;
                return false;
            }

            return true;

        } // PhotoInsertBefore

        /// <summary>Returns the element that shall be added and the element defining where it shall be inserted</summary>
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_jazz_photo">Object JazzPhoto that shall be added as a new node</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object.</param>
        /// <param name="o_photo_element">Element that shall be added</param>
        /// <param name="o_season_element">Element defining where the element shall be added</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoInsert(bool i_b_photo_one, JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number, out XElement o_photo_element, out XElement o_season_element, out string o_error)
        {
            o_error = @"";
            o_season_element = null;
            o_photo_element = null;

            XDocument xdocument_photo = GetPhotoXDocument(i_b_photo_one);

            if (null == xdocument_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoInsert xdocument_photo is null";
                return false;
            }

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoInsert Input JazzPhoto is null";
                return false;
            }

            int number_seasons_one = -12345;
            if (!CheckInputSeasonNumber(i_b_photo_one, i_season_number, out number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoInsert CheckInputSeasonNumber failed " + o_error;
                return false;
            }

            int number_concerts_one = -12345;
            if (!CheckInputConcertNumber(i_b_photo_one, i_season_number, i_concert_number, out number_concerts_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoInsert CheckInputConcertNumber failed " + o_error;
                return false;
            }

            XElement photo_element = PhotoElement(i_jazz_photo, out o_error);

            if (null == photo_element)
            {
                o_error = @"JazzXmlPhoto.PhotoInsert PhotoElement failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(xdocument_photo, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.PhotoInsert GetSeasonElement failed " + o_error;
                return false;
            }

            o_season_element = season_element;
            o_photo_element = photo_element;

            return true;

        } // PhotoInsert
        /*QQQQQ
        /// <summary>Adds a photo (concert) node to the photo one XDocument object (m_xdocument_photo_one) corresponding to XML file JazzGalerieEin.xml</summary>
        /// <param name="i_jazz_photo">Object JazzPhoto that shall be added as a new node</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object. Eq. 0: First element for the season</param>
        /// <param name="o_error">Error message</param>
        static public bool AddPhotoOne(JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == m_xdocument_photo_one)
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne m_xdocument_photo_one is null";
                return false;
            }

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne Input JazzPhoto is null";
                return false;
            }

            int number_seasons_one = -12345;
            if (!CheckInputSeasonNumberOne(i_season_number, out number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne CheckInputSeasonNumberOne failed " + o_error;
                return false; 
            }

            int number_concerts_one = -12345;
            if (!CheckInputConcertNumberOne(i_season_number, i_concert_number, out number_concerts_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne CheckInputConcertNumberOne failed " + o_error;
                return false;
            }

            XElement photo_element = PhotoElement(i_jazz_photo, out o_error);

            if (null == photo_element)
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne PhotoElement failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(m_xdocument_photo_one, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne GetSeasonElement failed " + o_error;
                return false;
            }

            if (!AddPhotoElement(season_element, photo_element, i_concert_number, out o_error))
            {
                o_error = @"JazzXmlPhoto.AddPhotoOne AddPhotoElement failed " + o_error;
                return false;
            }

            return true;

        } // AddPhotoOne

        /// <summary>Adds a photo (concert) node to the photo two XDocument object (m_xdocument_photo_two) corresponding to XML file JazzGalerieZwei.xml</summary>
        /// <param name="i_jazz_photo">Object JazzPhoto that shall be added as a new node</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object. Eq. 0: First element for the season</param>
        /// <param name="o_error">Error message</param>
        static public bool AddPhotoTwo(JazzPhoto i_jazz_photo, int i_season_number, int i_concert_number, out string o_error)
        {
            o_error = @"";

            bool b_photo_one = false;

            if (null == m_xdocument_photo_two)
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo m_xdocument_photo_two is null";
                return false;
            }

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo Input JazzPhoto is null";
                return false;
            }

            int number_seasons_two = -12345;
            if (!CheckInputSeasonNumber(b_photo_one, i_season_number, out number_seasons_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo CheckInputSeasonNumber failed " + o_error;
                return false;
            }

            int number_concerts_two = -12345;
            if (!CheckInputConcertNumber(b_photo_one, i_season_number, i_concert_number, out number_concerts_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo CheckInputConcertNumber failed " + o_error;
                return false;
            }

            XElement photo_element = PhotoElement(i_jazz_photo, out o_error);

            if (null == photo_element)
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo PhotoElement failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(m_xdocument_photo_two, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo GetSeasonElement failed " + o_error;
                return false;
            }

            if (!AddPhotoElement(season_element, photo_element, i_concert_number, out o_error))
            {
                o_error = @"JazzXmlPhoto.AddPhotoTwo AddPhotoElement failed " + o_error;
                return false;
            }

            return true;

        } // AddPhotoTwo
        QQQ*/
        /// <summary>Add photo element</summary>
        /// <param name="i_season_element">XElement where the photo element shall be added</param>
        /// <param name="i_photo_element">Photo XElement that shall be added</param>
        /// <param name="o_error">Error message</param>
        private static bool PhotoElementAppend(XElement i_season_element, XElement i_photo_element, out string o_error)
        {
            o_error = @"";

            if (null == i_season_element || null == i_photo_element)
            {
                o_error = @"JazzXmlPhoto.PhotoElementAppend i_season_element and/or i_photo_element is null";
                return false;
            }

            i_season_element.Add(i_photo_element);

            return true;

        } // PhotoElementAppend

        /// <summary>Insert photo element after a given photo element</summary>
        /// <param name="i_season_element">XElement where the photo element shall be added</param>
        /// <param name="i_photo_element">Photo XElement that shall be added</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object.</param>
        /// <param name="o_error">Error message</param>
        private static bool PhotoElementInsertAfter(XElement i_season_element, XElement i_photo_element_to_add, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == i_season_element || null == i_photo_element_to_add)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertAfter i_season_element and/or i_photo_element_to_add is null";
                return false;
            }

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertAfter i_concert_number <= 0";
                return false;
            }

            XElement concert_element = null;
            if (!PhotoElementInsert(i_season_element, i_concert_number, out concert_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertAfter PhotoElementInsert failed " + o_error;
                return false;
            }

            if (null == concert_element)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertAfter concert_element is null";
                return false;
            }

            concert_element.AddAfterSelf(i_photo_element_to_add);

            return true;

        } // PhotoElementInsertAfter

        /// <summary>Insert photo element before a given photo element</summary>
        /// <param name="i_season_element">XElement where the photo element shall be added</param>
        /// <param name="i_photo_element">Photo XElement that shall be added</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object.</param>
        /// <param name="o_error">Error message</param>
        private static bool PhotoElementInsertBefore(XElement i_season_element, XElement i_photo_element_to_add, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == i_season_element || null == i_photo_element_to_add)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertBefore i_season_element and/or i_photo_element_to_add is null";
                return false;
            }

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertBefore i_concert_number <= 0";
                return false;
            }

            XElement concert_element = null;
            if (!PhotoElementInsert(i_season_element, i_concert_number, out concert_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertBefore PhotoElementInsert failed " + o_error;
                return false;
            }

            if (null == concert_element)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertBefore concert_element is null";
                return false;
            }

            concert_element.AddBeforeSelf(i_photo_element_to_add);

            return true;

        } // PhotoElementInsertBefore

        /// <summary>Returns the element that will be used for insert</summary>
        /// <param name="i_season_element">XElement where the photo element shall be added</param>
        /// <param name="i_concert_number">Concert number. The object will be added before or after this object.</param>
        /// <param name="o_concert_element">Photo XElement for insert</param>
        /// <param name="o_error">Error message</param>
        private static bool PhotoElementInsert(XElement i_season_element, int i_concert_number, out XElement o_concert_element, out string o_error)
        {
            o_error = @"";
            o_concert_element = null;

            if (null == i_season_element)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsertAfter i_season_element is null";
                return false;
            }

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.PhotoElementInsert i_concert_number <= 0";
                return false;
            }

            int current_concert_number = 0;
            foreach (XElement element_concert in i_season_element.Descendants(GetTagPhotoConcert()))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert_number == current_concert_number)
                {
                    o_concert_element = element_concert;

                    return true;
                } // i_concert_number == current_concert_number
            } // element_concert


            o_error = @"JazzXmlPhoto.PhotoElementInsert Concert object not found for i_concert_number= " + i_concert_number.ToString();

            return false;

        } // PhotoElementInsert

        /// <summary>Add photo element</summary>
        /// <param name="i_season_element">XElement where the photo element shall be added</param>
        /// <param name="i_photo_element">Photo XElement that shall be added</param>
        /// <param name="i_concert_number">Concert number. The object will be added after this object. Eq. 0: First element for the season Eq. -1: Append element to season</param>
        /// <param name="o_error">Error message</param>
        private static bool AddPhotoElement(XElement i_season_element, XElement i_photo_element, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == i_season_element || null == i_photo_element)
            {
                o_error = @"JazzXmlPhoto.AddPhotoElement i_season_element and/or i_photo_element is null";
                return false;
            }

            if (i_concert_number < 0)
            {
                o_error = @"JazzXmlPhoto.AddPhotoElement i_concert_number < 0";
                return false;
            }

            if (0 == i_concert_number)
            {
                i_season_element.AddFirst(i_photo_element);
                return true;
            }

            if (-1 == i_concert_number)
            {
                i_season_element.Add(i_photo_element);
                return true;
            }

            int current_concert_number = 0;
            foreach (XElement element_concert in i_season_element.Descendants(GetTagPhotoConcert()))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert_number == current_concert_number)
                {
                    element_concert.AddAfterSelf(i_photo_element);

                    return true;
                } // i_concert_number == current_concert_number
            } // element_concert


            o_error = @"JazzXmlPhoto.AddPhotoElement Concert object not found for i_concert_number= " + i_concert_number.ToString();
            return false;

        } // AddPhotoElement

        /// <summary>Returns the XElement (a subtree) in the input photo XDocument object for the input season number</summary>
        /// <param name="i_xdocument">XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_season_number">Season number</param>
        /// <param name="o_error">Error message</param>
        private static XElement GetSeasonElement(XDocument i_xdocument, int i_season_number, out string o_error)
        {
            o_error = @"";

            XElement ret_element = null;
            if (null == i_xdocument)
            {
                o_error = @"JazzXmlPhoto.GetSeasonElement i_xdocument is null";
                return ret_element;
            }

            int current_season_number = 0;

            foreach (XElement element_season in i_xdocument.Descendants(GetTagPhotosSeason()))
            {
                current_season_number = current_season_number + 1;
                if (i_season_number == current_season_number)
                {
                    ret_element = element_season;
                    return ret_element;
                }

            } // element_season

            o_error = @"JazzXmlPhoto.GetSeasonElement Element not found for i_season_number= " + i_season_number.ToString();
            return ret_element;

        } // GetSeasonElement

        /// <summary>Creates a photo object XElement (a sub-tree)
        /// <para>The returned XElement will be added to an existing XDocument</para>
        /// </summary>
        /// <param name="i_jazz_photo">Photo object (JazzPhoto) that shall become the output XElement</param>
        /// <param name="o_error">Error message</param>
        public static XElement PhotoElement(JazzPhoto i_jazz_photo, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoElement Input JazzPhoto is null";
                return null;
            }

            XElement ret_element = new XElement(GetTagPhotoConcert(),
                                   new XElement(GetTagPhotoBandName(), i_jazz_photo.BandName),
                                   new XElement(GetTagPhotoYear(), i_jazz_photo.Year),
                                   new XElement(GetTagPhotoMonth(), i_jazz_photo.Month),
                                   new XElement(GetTagPhotoDay(), i_jazz_photo.Day),
                                   new XElement(GetTagGalleryName(), i_jazz_photo.GalleryName),
                                   new XElement(GetTagPhotoTextOne(), i_jazz_photo.TextOne),
                                   new XElement(GetTagPhotoTextTwo(), i_jazz_photo.TextTwo),
                                   new XElement(GetTagPhotoTextThree(), i_jazz_photo.TextThree),
                                   new XElement(GetTagPhotoTextFour(), i_jazz_photo.TextFour),
                                   new XElement(GetTagPhotoTextFive(), i_jazz_photo.TextFive),
                                   new XElement(GetTagPhotoTextSix(), i_jazz_photo.TextSix),
                                   new XElement(GetTagPhotoTextSeven(), i_jazz_photo.TextSeven),
                                   new XElement(GetTagPhotoTextEight(), i_jazz_photo.TextEight),
                                   new XElement(GetTagPhotoTextNine(), i_jazz_photo.TextNine),
                                   new XElement(GetTagPhotographerName(), i_jazz_photo.PhotographerName),
                                   new XElement(GetTagPhotoZipName(), i_jazz_photo.ZipName),
                                   new XElement(GetTagPhotoConcertNumber(), i_jazz_photo.ConcertNumber)
                                   );

            return ret_element;

        } // PhotoElement

        #endregion // Add photo object

        #region Add season object

        /// <summary>Inserts a photo season element after the input season element
        /// <para>The element will be added to XDocument m_xdocument_photo_one (JazzGalerieEin.xml) or m_xdocument_photo_two (JazzGalerieZwei.xml)</para>
        /// <para></para>
        /// </summary>           
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_season_start_year">Start year for the season</param>
        /// <param name="i_season_number">Season number. The element will be added after this element.</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoSeasonInsertAfter(bool i_b_photo_one, int i_season_start_year, int i_season_number, out string o_error)
        {
            o_error = @"";

            XElement season_element_to_add = null;
            XElement season_element = null;

            if (!PhotoSeasonInsert(i_b_photo_one, i_season_start_year, i_season_number, out season_element_to_add, out season_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsertAfter PhotoSeasonInsert failed " + o_error;
                return false;
            }

            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsertAfter season_element is null ";
                return false;
            }

            if (null == season_element_to_add)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsertAfter season_element_to_add is null ";
                return false;
            }

            season_element.AddAfterSelf(season_element_to_add);

            return true;

        } // PhotoSeasonInsertAfter

        /// <summary>Inserts a photo season element before the input season element
        /// <para>The element will be added to XDocument m_xdocument_photo_one (JazzGalerieEin.xml) or m_xdocument_photo_two (JazzGalerieZwei.xml)</para>
        /// <para></para>
        /// </summary>           
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_season_start_year">Start year for the season</param>
        /// <param name="i_season_number">Season number. The element will be added after this element.</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoSeasonInsertBefore(bool i_b_photo_one, int i_season_start_year, int i_season_number, out string o_error)
        {
            o_error = @"";

            XElement season_element_to_add = null;
            XElement season_element = null;

            if (!PhotoSeasonInsert(i_b_photo_one, i_season_start_year, i_season_number, out season_element_to_add, out season_element, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsertBefore PhotoSeasonInsert failed " + o_error;
                return false;
            }

            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsertBefore season_element is null ";
                return false;
            }

            if (null == season_element_to_add)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsertBefore season_element_to_add is null ";
                return false;
            }

            season_element.AddBeforeSelf(season_element_to_add);

            return true;

        } // PhotoSeasonInsertBefore

        /// <summary>Returns the photo season element (XElement) for insert before or after 
        /// <para>The element is for XDocument m_xdocument_photo_one (JazzGalerieEin.xml) or m_xdocument_photo_two (JazzGalerieZwei.xml)</para>
        /// <para> </para>
        /// <para> </para>
        /// <para></para>
        /// </summary>           
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_season_start_year">Start year for the season</param>
        /// <param name="i_season_number">Season number. The element will be added after this element.</param>
        /// <param name="o_season_element_to_add">The season XElement that shall be inserted.</param>
        /// <param name="o_season_element">The season XElement that defines where the season XElement shall be inserted</param>
        /// <param name="o_error">Error message</param>
        static private bool PhotoSeasonInsert(bool i_b_photo_one, int i_season_start_year, int i_season_number, out XElement o_season_element_to_add, out XElement o_season_element, out string o_error)
        {
            o_error = @"";
            o_season_element = null;
            o_season_element_to_add = null;

            if (i_season_start_year < 1996)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsert i_season_start_year < 1996 ";
                return false;
            }

            if (i_season_number <= 0)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsert i_season_number <= 0";
                return false;
            }

            XDocument xdocument_photo = GetPhotoXDocument(i_b_photo_one);

            if (null == xdocument_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsert xdocument_photo is null";
                return false;
            }

            XElement season_element_to_add = SeasonElement(i_season_start_year, out o_error);

            if (null == season_element_to_add)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsert SeasonElement failed " + o_error;
                return false;
            }

            int number_seasons = -12345;
            if (!CheckInputSeasonNumber(i_b_photo_one, i_season_number, out number_seasons, out o_error))
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsert CheckInputSeasonNumber failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(xdocument_photo, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonInsert GetSeasonElement failed " + o_error;
                return false;
            }

            o_season_element_to_add = season_element_to_add;
            o_season_element = season_element;

            return true;

        } // PhotoSeasonInsert

        /// <summary>Appends a photo season element
        /// <para>The element will be added to XDocument m_xdocument_photo_one (JazzGalerieEin.xml) or m_xdocument_photo_two (JazzGalerieZwei.xml)</para>
        /// <para> </para>
        /// <para> </para>
        /// <para></para>
        /// </summary>           
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="i_season_start_year">Start year for the season</param>
        /// <param name="o_error">Error message</param>
        static public bool PhotoSeasonAppend(bool i_b_photo_one, int i_season_start_year, out string o_error)
        {
            o_error = @"";

            if (i_season_start_year < 1996)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonAppend i_season_start_year= " + i_season_start_year.ToString() + @" < 1996 ";
                return false;
            }

            XDocument xdocument_photo = GetPhotoXDocument(i_b_photo_one);

            if (null == xdocument_photo)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonAppend xdocument_photo is null";
                return false;
            }

            XElement season_element_to_add = SeasonElement(i_season_start_year, out o_error);

            if (null == season_element_to_add)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonAppend SeasonElement failed " + o_error;
                return false;
            }

            int number_seasons = JazzXml.GetNumberOfPhotoSeasons(i_b_photo_one, out o_error);
            if (number_seasons < 0)
            {
                o_error = @"JazzXmlPhoto.PhotoSeasonAppend JazzXml.GetNumberOfPhotoSeasons failed number_seasons= " + number_seasons.ToString() + o_error;
                return false;
            }

            XElement element_gallery = GetGalleryXElement(i_b_photo_one);

            if (0 == number_seasons)
            {
                element_gallery.AddFirst(season_element_to_add);
            }
            else
            {
                element_gallery.Add(season_element_to_add);
            }

            return true;

        } // PhotoSeasonAppend

        /// <summary>Creates a season object XElement (a sub-tree)
        /// <para>The returned XElement will be added to an existing XDocument</para>
        /// </summary>
        /// <param name="i_season_start_year">Season start year</param>
        /// <param name="o_error">Error message</param>
        public static XElement SeasonElement(int i_season_start_year, out string o_error)
        {
            o_error = @"";

            if (i_season_start_year < 1996)
            {
                o_error = @"JazzXmlPhoto.RequestElement i_season_start_year < 1996 ";
                return null;
            }

            XElement ret_element = new XElement(GetTagPhotosSeason(),
                                   new XElement(GetTagPhotoStartYearSeason(), i_season_start_year.ToString())
                                   );

            return ret_element;

        } // SeasonElement

        #endregion  // Add season object

        #region Delete objects

        /// <summary>Delete a photo (concert) node of the photo one XDocument object (m_xdocument_photo_one) corresponding to XML file JazzGalerieEin.xml</summary>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number that shall be deleted</param>
        /// <param name="o_error">Error message</param>
        static public bool DeletePhotoOne(int i_season_number, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == m_xdocument_photo_one)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoOne m_xdocument_photo_one is null";
                return false;
            }

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoOne i_concert_number <= 0";
                return false;
            }

            int number_seasons_one = -12345;
            if (!CheckInputSeasonNumberOne(i_season_number, out number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeletePhotoOne CheckInputSeasonNumberOne failed " + o_error;
                return false;
            }

            int number_concerts_one = -12345;
            if (!CheckInputConcertNumberOne(i_season_number, i_concert_number, out number_concerts_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeletePhotoOne CheckInputConcertNumberOne failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(m_xdocument_photo_one, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoOne GetSeasonElement failed " + o_error;
                return false;
            }

            if (!DeletePhotoElement(season_element, i_concert_number, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeletePhotoOne DeletePhotoElement failed " + o_error;
                return false;
            }

            return true;

        } // DeletePhotoOne

        /// <summary>Delete a photo (concert) node of the photo two XDocument object (m_xdocument_photo_two) corresponding to XML file JazzGalerieZwei.xml</summary>
        /// <param name="i_season_number">Season number</param>
        /// <param name="i_concert_number">Concert number that shall be deleted</param>
        /// <param name="o_error">Error message</param>
        static public bool DeletePhotoTwo(int i_season_number, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == m_xdocument_photo_two)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoTwo m_xdocument_photo_two is null";
                return false;
            }

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoTwo i_concert_number <= 0";
                return false;
            }

            int number_seasons_one = -12345;
            if (!CheckInputSeasonNumberTwo(i_season_number, out number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeletePhotoTwo CheckInputSeasonNumberTwo failed " + o_error;
                return false;
            }

            int number_concerts_two = -12345;
            if (!CheckInputConcertNumberTwo(i_season_number, i_concert_number, out number_concerts_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeletePhotoTwo CheckInputConcertNumberTwo failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(m_xdocument_photo_two, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoTwo GetSeasonElement failed " + o_error;
                return false;
            }

            if (!DeletePhotoElement(season_element, i_concert_number, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeletePhotoTwo DeletePhotoElement failed " + o_error;
                return false;
            }

            return true;

        } // DeletePhotoTwo

        /// <summary>Delete photo element</summary>
        /// <param name="i_season_element">XElement with the photo element that shall be deleted</param>
        /// <param name="i_concert_number">Concert number that shall be deleted</param>
        /// <param name="o_error">Error message</param>
        private static bool DeletePhotoElement(XElement i_season_element, int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (null == i_season_element)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoElement i_season_element is null";
                return false;
            }

            if (i_concert_number <= 0)
            {
                o_error = @"JazzXmlPhoto.DeletePhotoElement i_concert_number <= 0";
                return false;
            }

            int current_concert_number = 0;
            foreach (XElement element_concert in i_season_element.Descendants(GetTagPhotoConcert()))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert_number == current_concert_number)
                {
                    element_concert.Remove();

                    return true;
                } // i_concert_number == current_concert_number
            } // element_concert


            o_error = @"JazzXmlPhoto.DeletePhotoElement Concert object not found for i_concert_number= " + i_concert_number.ToString();
            return false;

        } // DeletePhotoElement

        /// <summary>Delete a season node of the photo one XDocument object (m_xdocument_photo_one) corresponding to XML file JazzGalerieEin.xml</summary>
        /// <param name="i_season_number">Season number that shall be deleted</param>
        /// <param name="o_error">Error message</param>
        static public bool DeleteSeasonOne(int i_season_number, out string o_error)
        {
            o_error = @"";

            if (null == m_xdocument_photo_one)
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonOne m_xdocument_photo_one is null";
                return false;
            }

            if (i_season_number <= 0)
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonOne i_season_number <= 0";
                return false;
            }

            int number_seasons_one = -12345;
            if (!CheckInputSeasonNumberOne(i_season_number, out number_seasons_one, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonOne CheckInputSeasonNumberOne failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(m_xdocument_photo_one, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonOne GetSeasonElement failed " + o_error;
                return false;
            }

            season_element.Remove();

            return true;

        } // DeleteSeasonOne

        /// <summary>Delete a season node of the photo two XDocument object (m_xdocument_photo_two) corresponding to XML file JazzGalerieZwei.xml</summary>
        /// <param name="i_season_number">Season number that shall be deleted</param>
        /// <param name="o_error">Error message</param>
        static public bool DeleteSeasonTwo(int i_season_number, out string o_error)
        {
            o_error = @"";

            if (null == m_xdocument_photo_two)
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonTwo m_xdocument_photo_two is null";
                return false;
            }

            if (i_season_number <= 0)
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonTwo i_season_number <= 0";
                return false;
            }

            int number_seasons_two = -12345;
            if (!CheckInputSeasonNumberTwo(i_season_number, out number_seasons_two, out o_error))
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonTwo CheckInputSeasonNumberTwo failed " + o_error;
                return false;
            }

            XElement season_element = GetSeasonElement(m_xdocument_photo_two, i_season_number, out o_error);
            if (null == season_element)
            {
                o_error = @"JazzXmlPhoto.DeleteSeasonTwo GetSeasonElement failed " + o_error;
                return false;
            }

            season_element.Remove();

            return true;

        } // DeleteSeasonTwo

        #endregion // Delete objects

        #region Get gallery number for new photo object


        /// <summary>Returns the gallery name for a new photo object that shall be added</summary>
        /// <param name="i_b_photo_one">Flag telling if it is photo one or two gallery, i.e. XDocument m_xdocument_photo_one or m_xdocument_photo_two</param>
        /// <param name="o_error">Error message</param>
        public static string GetGalleryNumberNewPhoto(bool i_b_photo_one, out string o_error)
        {
            o_error = @"";
            string ret_gallery_number_str = @"";

            JazzPhoto[] season_photo_objects = null;  //GetPhotoOneObjects(season_number, out o_error)

            int n_seasons = GetNumberOfPhotoSeasons(i_b_photo_one, out o_error);
            if (n_seasons <= 0)
            {
                o_error = @"JazzXmlPhoto.GetGalleryNumberNewPhoto n_seasons= " + n_seasons.ToString() + @" <= 0";
                return ret_gallery_number_str;
            }

            int maximum_gallery_number = -5000000;

            for (int season_number=1; season_number<=n_seasons;season_number++)
            {
                if (i_b_photo_one)
                {
                    season_photo_objects = GetPhotoOneObjects(season_number, out o_error);
                }
                else
                {
                    season_photo_objects = GetPhotoTwoObjects(season_number, out o_error);
                }

                if (null == season_photo_objects)
                {
                    o_error = @"JazzXmlPhoto.GetGalleryNumberNewPhoto season_photo_objects is null";
                    return ret_gallery_number_str;
                }

                for (int index_photo=0; index_photo< season_photo_objects.Length; index_photo++)
                {
                    JazzPhoto current_photo = season_photo_objects[index_photo];

                    int gallery_number = current_photo.GalleryNumberInt;
                    if (gallery_number > maximum_gallery_number)
                    {
                        maximum_gallery_number = gallery_number;
                    }

                } // index_photo

            } // season_number

            int ret_number = maximum_gallery_number + 1;

            if (ret_number <= 9)
            {
                ret_gallery_number_str = @"G00" + ret_number.ToString();
            }
            else if (ret_number <= 99)
            {
                ret_gallery_number_str = @"G0" + ret_number.ToString();
            }
            else
            {
                ret_gallery_number_str = @"G" + ret_number.ToString();
            }

            return ret_gallery_number_str;

        } // GetGalleryNumberNewPhoto

        #endregion // Get gallery number for new photo object

    } // JazzXml(Photo)

} // namespace
