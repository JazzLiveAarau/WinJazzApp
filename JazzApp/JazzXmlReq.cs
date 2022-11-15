using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Functions for the initialization (creation) of XML objects from the XML file JazzAnfrage.xml
    /// <para>The XML file registers the requests from bands that wish to play in the jazz club.</para>
    /// <para>It holds the URLs to audio samples (CDs). Comments and private notes can also be saved.</para>
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

        /// <summary>The XML document (object) that corresponds to the requests XML file (JazzAnfrage.xml)</summary>
        static private XDocument m_xdocument_req = null;

        /// <summary>Returns the XML document (object) that corresponds to the requests XML file (JazzAnfrage.xml)</summary>
        static public XDocument GetObjectReq() { return m_xdocument_req; }

        /// <summary>Status for m_xdocument_req</summary>
        static private int m_xdocument_req_status = -12345;

        /// <summary>Error message for the creation of m_xdocument_req</summary>
        static private string m_xdocument_req_error = "Not set";

        /// <summary>The name of the requirements XML file</summary>
        static private string m_req_xml_filename = @"";

        /// <summary>Returns the name of the requirements XML file</summary>
        static public string GetFileNameObjectReq() { return m_req_xml_filename; }

        /// <summary>The URL to the folder with the request XML files</summary>
        static private string m_url_xml_req_files_folder = @"";

        /// <summary>Returns the URL path to the folder with the request XML files</summary>
        static public string GetUrlXmlReqFiles() { return m_url_xml_req_files_folder; }

        #endregion // XML objects, file names and directory names

        #region Init functions

        /// <summary>Initialization of requests member parameters 
        /// <para>Initialize the XML object (m_xdocument_req) that corresponds to the requests XML file (JazzAnfrage.xml). Call of InitXmlDocumentReq</para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_url_xml_req_files_folder">Server path to the XML request file<</param>
        /// <param name="i_req_xml_filename">Name of the XML requests file</param>
        /// <param name="o_error">Error message</param>
        static public bool InitReq(string i_url_xml_req_files_folder, string i_req_xml_filename, out string o_error)
        {
            o_error = @"";

            m_url_xml_req_files_folder = i_url_xml_req_files_folder;
            m_req_xml_filename = i_req_xml_filename;
            // TODO Check data

            string error_message = @"";
            if (!InitXmlDocumentReq(out error_message))
            {
                o_error = @"JazzXml.InitReq Programming error " + error_message;
                return false;
            }

            return true;
        } // InitReq

        /// <summary>Initialize the XML object (m_xdocument_req) that corresponds to the requests XML file (JazzAnfrage.xml)</summary>
        static private bool InitXmlDocumentReq(out string o_error)
        {
            o_error = @"";

            string url_file_req = GetWebSiteUrl() + GetUrlXmlReqFiles() + @"/" + GetFileNameObjectReq();
            if (url_file_req.Trim().Length == 0)
            {
                o_error = @"JazzXml.InitXmlDocumentReq URL for the templates file is not defined";

                return false;
            }

            JazzOsUtils.LoadXmlDocument(url_file_req, 7, -1); 

            string error_message = @"";
            if (GetXmlDocumentReqStatus(out error_message) < 0)
            {
                o_error = @"JazzXml.InitXmlDocumentReq Programming error: " + error_message;

                return false;
            }

            // Additional (not necessary) check that object is created
            if (null == GetObjectReq())
            {
                o_error = @"JazzXml.InitXmlDocumentReq Requests object is not created (JazzOsUtils.LoadXmlDocument failed)";

                return false;
            }

            return true;

        } // InitXmlDocumentReq

        /// <summary>Set the XML object document requests (m_xdocument_req) that corresponds to the requests XML file (JazzAnfrage.xml)</summary>
        static public void SetXmlDocumentReq(XDocument i_xdocument_req)
        {
            m_xdocument_req_status = 0;
            m_xdocument_req_error = "";

            m_xdocument_req = i_xdocument_req;
        } // SetXmlDocumentReq

        /// <summary>Set status for document requests</summary>
        static public void SetXmlDocumentReqStatus(int i_status, string i_error)
        {
            m_xdocument_req_status = i_status;
            m_xdocument_req_error = i_error;

        } // SetXmlDocumentReq

        /// <summary>Get status for document requests</summary>
        static public int GetXmlDocumentReqStatus(out string o_error)
        {
            o_error = m_xdocument_req_error;

            return m_xdocument_req_status;

        } // GetXmlDocumentReqStatus

        #endregion // Init functions

        #region Get and set request functions

        /// <summary>Returns the last register number as string</summary>
        static public string GetReqLastRegNumber() { return GetInnerTextReqSingleNode(GetTagReqLastReqNumber()); }

        /// <summary>Sets the last register number as a string</summary>
        static public void SetReqLastRegNumber(string i_last_reg_number) { SetInnerTextReqSingleNode(GetTagReqLastReqNumber(), i_last_reg_number); }

        /// <summary>Returns the register number as a string</summary>
        static public string GetReqRegNumber(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqRegNumber()); }

        /// <summary>Sets the register number as a string</summary>
        static public void SetReqRegNumber(int i_req, string i_reg_number_str) { SetInnerTextReqNode(i_req, GetTagReqRegNumber(), i_reg_number_str); }

        /// <summary>Returns the register day number as a string</summary>
        static public string GetReqRegDay(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqRegDay()); }

        /// <summary>Sets the register day number as a string</summary>
        static public void SetReqRegDay(int i_req, string i_reg_day_str) { SetInnerTextReqNode(i_req, GetTagReqRegDay(), i_reg_day_str); }

        /// <summary>Returns the register month number as a string</summary>
        static public string GetReqRegMonth(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqRegMonth()); }

        /// <summary>Sets the register month number as a string</summary>
        static public void SetReqRegMonth(int i_req, string i_reg_month_str) { SetInnerTextReqNode(i_req, GetTagReqRegMonth(), i_reg_month_str); }

        /// <summary>Returns the register year number as a string</summary>
        static public string GetReqRegYear(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqRegYear()); }

        /// <summary>Sets the register year number as a string</summary>
        static public void SetReqRegYear(int i_req, string i_reg_year_str) { SetInnerTextReqNode(i_req, GetTagReqRegYear(), i_reg_year_str); }

        /// <summary>Returns the band name</summary>
        static public string GetReqBandName(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqBandName()); }

        /// <summary>Sets the band name</summary>
        static public void SetReqBandName(int i_req, string i_band_name) { SetInnerTextReqNode(i_req, GetTagReqBandName(), i_band_name); }

        /// <summary>Returns the comments for the request</summary>
        static public string GetReqComments(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqComments()); }

        /// <summary>Sets the comments for the request</summary>
        static public void SetReqComments(int i_req, string i_comments) { SetInnerTextReqNode(i_req, GetTagReqComments(), i_comments); }

        /// <summary>Returns the private notes for the request</summary>
        static public string GetReqPrivateNotes(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPrivateNotes()); }

        /// <summary>Sets the private notes for the request</summary>
        static public void SetReqPrivateNotes(int i_req, string i_private_notes) { SetInnerTextReqNode(i_req, GetTagReqPrivateNotes(), i_private_notes); }

        /// <summary>Returns the band website for the request</summary>
        static public string GetReqBandWebsite(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqBandWebsite()); }

        /// <summary>Sets the band website for the request</summary>
        static public void SetReqBandWebsite(int i_req, string i_band_website) { SetInnerTextReqNode(i_req, GetTagReqBandWebsite(), i_band_website); }

        /// <summary>Returns the sound sample for the request</summary>
        static public string GetReqSoundSample(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqSoundSample()); }

        /// <summary>Sets the sound sample for the request</summary>
        static public void SetReqSoundSample(int i_req, string i_band_website) { SetInnerTextReqNode(i_req, GetTagReqSoundSample(), i_band_website); }

        /// <summary>Returns the name of the server directory one with audio (mp3) files, i.e. CD number one</summary>
        static public string GetReqAudioOne(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqAudioOne()); }

        /// <summary>Sets the name of the server directory one with audio (mp3) files, i.e. CD number one</summary>
        static public void SetReqAudioOne(int i_req, string i_audio_one_dir) { SetInnerTextReqNode(i_req, GetTagReqAudioOne(), i_audio_one_dir); }

        /// <summary>Returns the name of the server directory two with audio (mp3) files, i.e. CD number two</summary>
        static public string GetReqAudioTwo(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqAudioTwo()); }

        /// <summary>Sets the name of the server directory two with audio (mp3) files, i.e. CD number two</summary>
        static public void SetReqAudioTwo(int i_req, string i_audio_two_dir) { SetInnerTextReqNode(i_req, GetTagReqAudioTwo(), i_audio_two_dir); }

        /// <summary>Returns the name of the server directory three with audio (mp3) files, i.e. CD number three</summary>
        static public string GetReqAudioThree(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqAudioThree()); }

        /// <summary>Sets the name of the server directory three with audio (mp3) files, i.e. CD number three</summary>
        static public void SetReqAudioThree(int i_req, string i_audio_three_dir) { SetInnerTextReqNode(i_req, GetTagReqAudioThree(), i_audio_three_dir); }

        /// <summary>Returns the name of CD number one</summary>
        static public string GetReqAudioOneCd(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqAudioOneCd()); }

        /// <summary>Sets the name of CD number one</summary>
        static public void SetReqAudioOneCd(int i_req, string i_audio_one_cd_name) { SetInnerTextReqNode(i_req, GetTagReqAudioOneCd(), i_audio_one_cd_name); }

        /// <summary>Returns the name of CD number two</summary>
        static public string GetReqAudioTwoCd(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqAudioTwoCd()); }

        /// <summary>Sets the name of CD number two</summary>
        static public void SetReqAudioTwoCd(int i_req, string i_audio_two_cd_name) { SetInnerTextReqNode(i_req, GetTagReqAudioTwoCd(), i_audio_two_cd_name); }

        /// <summary>Returns the name of CD number three</summary>
        static public string GetReqAudioThreeCd(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqAudioThreeCd()); }

        /// <summary>Sets the name of CD number three</summary>
        static public void SetReqAudioThreeCd(int i_req, string i_audio_three_cd_name) { SetInnerTextReqNode(i_req, GetTagReqAudioThreeCd(), i_audio_three_cd_name); }

        /// <summary>Returns the flag (as a string) that tells if the request shall be evaluated (at the next meeting)</summary>
        static public string GetReqToBeEvaluated(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqToBeEvaluated()); }

        /// <summary>Sets the flag (as a string) that tells if the request shall be evaluated (at the next meeting)</summary>
        static public void SetReqToBeEvaluated(int i_req, string i_to_be_evaluated) { SetInnerTextReqNode(i_req, GetTagReqToBeEvaluated(), i_to_be_evaluated); }

        /// <summary>Returns the info one file name</summary>
        static public string GetReqInfoOne(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqInfoOne()); }

        /// <summary>Sets the info one file name</summary>
        static public void SetReqInfoOne(int i_req, string i_info_one) { SetInnerTextReqNode(i_req, GetTagReqInfoOne(), i_info_one); }

        /// <summary>Returns the info two file name</summary>
        static public string GetReqInfoTwo(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqInfoTwo()); }

        /// <summary>Sets the info two file name</summary>
        static public void SetReqInfoTwo(int i_req, string i_info_two) { SetInnerTextReqNode(i_req, GetTagReqInfoTwo(), i_info_two); }

        /// <summary>Returns the info three file name</summary>
        static public string GetReqInfoThree(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqInfoThree()); }

        /// <summary>Sets the info three file name</summary>
        static public void SetReqInfoThree(int i_req, string i_info_three) { SetInnerTextReqNode(i_req, GetTagReqInfoThree(), i_info_three); }

        /// <summary>Returns link 1</summary>
        static public string GetReqLinkOne(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkOne()); }

        /// <summary>Sets link 1</summary>
        static public void SetReqLinkOne(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkOne(), i_link); }

        /// <summary>Returns link 2</summary>
        static public string GetReqLinkTwo(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkTwo()); }

        /// <summary>Sets link 2</summary>
        static public void SetReqLinkTwo(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkTwo(), i_link); }

        /// <summary>Returns link 3</summary>
        static public string GetReqLinkThree(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkThree()); }

        /// <summary>Sets link 3</summary>
        static public void SetReqLinkThree(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkThree(), i_link); }

        /// <summary>Returns link 4</summary>
        static public string GetReqLinkFour(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkFour()); }

        /// <summary>Sets link 4</summary>
        static public void SetReqLinkFour(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkFour(), i_link); }

        /// <summary>Returns link 5</summary>
        static public string GetReqLinkFive(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkFive()); }

        /// <summary>Sets link 5</summary>
        static public void SetReqLinkFive(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkFive(), i_link); }

        /// <summary>Returns link 6</summary>
        static public string GetReqLinkSix(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkSix()); }

        /// <summary>Sets link 6</summary>
        static public void SetReqLinkSix(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkSix(), i_link); }

        /// <summary>Returns link 7</summary>
        static public string GetReqLinkSeven(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkSeven()); }

        /// <summary>Sets link 7</summary>
        static public void SetReqLinkSeven(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkSeven(), i_link); }

        /// <summary>Returns link 8</summary>
        static public string GetReqLinkEight(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkEight()); }

        /// <summary>Sets link 8</summary>
        static public void SetReqLinkEight(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkEight(), i_link); }

        /// <summary>Returns link 9</summary>
        static public string GetReqLinkNine(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkNine()); }

        /// <summary>Sets link 9</summary>
        static public void SetReqLinkNine(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkNine(), i_link); }

        /// <summary>Returns link 1</summary>
        static public string GetReqLinkOneType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkOneType()); }

        /// <summary>Sets link type 1</summary>
        static public void SetReqLinkOneType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkOneType(), i_link); }

        /// <summary>Returns link type 2</summary>
        static public string GetReqLinkTwoType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkTwoType()); }

        /// <summary>Sets link type 2</summary>
        static public void SetReqLinkTwoType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkTwoType(), i_link); }

        /// <summary>Returns link type 3</summary>
        static public string GetReqLinkThreeType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkThreeType()); }

        /// <summary>Sets link type 3</summary>
        static public void SetReqLinkThreeType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkThreeType(), i_link); }

        /// <summary>Returns link type 4</summary>
        static public string GetReqLinkFourType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkFourType()); }

        /// <summary>Sets link type 4</summary>
        static public void SetReqLinkFourType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkFourType(), i_link); }

        /// <summary>Returns link type 5</summary>
        static public string GetReqLinkFiveType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkFiveType()); }

        /// <summary>Sets link type 5</summary>
        static public void SetReqLinkFiveType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkFiveType(), i_link); }

        /// <summary>Returns link type 6</summary>
        static public string GetReqLinkSixType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkSixType()); }

        /// <summary>Sets link type 6</summary>
        static public void SetReqLinkSixType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkSixType(), i_link); }

        /// <summary>Returns link type 7</summary>
        static public string GetReqLinkSevenType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkSevenType()); }

        /// <summary>Sets link type 7</summary>
        static public void SetReqLinkSevenType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkSevenType(), i_link); }

        /// <summary>Returns link type 8</summary>
        static public string GetReqLinkEightType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkEightType()); }

        /// <summary>Sets link type 8</summary>
        static public void SetReqLinkEightType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkEightType(), i_link); }

        /// <summary>Returns link type 9</summary>
        static public string GetReqLinkNineType(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkNineType()); }

        /// <summary>Sets link type 9</summary>
        static public void SetReqLinkNineType(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkNineType(), i_link); }

        /// <summary>Returns link text 1</summary>
        static public string GetReqLinkOneText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkOneText()); }

        /// <summary>Sets link text 1</summary>
        static public void SetReqLinkOneText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkOneText(), i_link); }

        /// <summary>Returns link text 2</summary>
        static public string GetReqLinkTwoText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkTwoText()); }

        /// <summary>Sets link text 2</summary>
        static public void SetReqLinkTwoText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkTwoText(), i_link); }

        /// <summary>Returns link text 3</summary>
        static public string GetReqLinkThreeText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkThreeText()); }

        /// <summary>Sets link text 3</summary>
        static public void SetReqLinkThreeText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkThreeText(), i_link); }

        /// <summary>Returns link text 4</summary>
        static public string GetReqLinkFourText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkFourText()); }

        /// <summary>Sets link text 4</summary>
        static public void SetReqLinkFourText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkFourText(), i_link); }

        /// <summary>Returns link text 5</summary>
        static public string GetReqLinkFiveText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkFiveText()); }

        /// <summary>Sets link text 5</summary>
        static public void SetReqLinkFiveText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkFiveText(), i_link); }

        /// <summary>Returns link text 6</summary>
        static public string GetReqLinkSixText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkSixText()); }

        /// <summary>Sets link text 6</summary>
        static public void SetReqLinkSixText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkSixText(), i_link); }

        /// <summary>Returns link text 7</summary>
        static public string GetReqLinkSevenText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkSevenText()); }

        /// <summary>Sets link text 7</summary>
        static public void SetReqLinkSevenText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkSevenText(), i_link); }

        /// <summary>Returns link text 8</summary>
        static public string GetReqLinkEightText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkEightText()); }

        /// <summary>Sets link text 8</summary>
        static public void SetReqLinkEightText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkEightText(), i_link); }

        /// <summary>Returns link text 9</summary>
        static public string GetReqLinkNineText(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqLinkNineText()); }

        /// <summary>Sets link text 9</summary>
        static public void SetReqLinkNineText(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqLinkNineText(), i_link); }

        /// <summary>Returns photo 1</summary>
        static public string GetReqPhotoOne(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoOne()); }

        /// <summary>Sets photo 1</summary>
        static public void SetReqPhotoOne(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoOne(), i_link); }

        /// <summary>Returns photo 2</summary>
        static public string GetReqPhotoTwo(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoTwo()); }

        /// <summary>Sets photo 2</summary>
        static public void SetReqPhotoTwo(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoTwo(), i_link); }

        /// <summary>Returns photo 3</summary>
        static public string GetReqPhotoThree(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoThree()); }

        /// <summary>Sets photo 3</summary>
        static public void SetReqPhotoThree(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoThree(), i_link); }

        /// <summary>Returns photo 4</summary>
        static public string GetReqPhotoFour(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoFour()); }

        /// <summary>Sets photo 4</summary>
        static public void SetReqPhotoFour(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoFour(), i_link); }

        /// <summary>Returns photo 5</summary>
        static public string GetReqPhotoFive(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoFive()); }

        /// <summary>Sets photo 5</summary>
        static public void SetReqPhotoFive(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoFive(), i_link); }

        /// <summary>Returns photo 6</summary>
        static public string GetReqPhotoSix(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoSix()); }

        /// <summary>Sets photo 6</summary>
        static public void SetReqPhotoSix(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoSix(), i_link); }

        /// <summary>Returns photo 7</summary>
        static public string GetReqPhotoSeven(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoSeven()); }

        /// <summary>Sets photo 7</summary>
        static public void SetReqPhotoSeven(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoSeven(), i_link); }

        /// <summary>Returns photo 8</summary>
        static public string GetReqPhotoEight(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoEight()); }

        /// <summary>Sets photo 8</summary>
        static public void SetReqPhotoEight(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoEight(), i_link); }

        /// <summary>Returns photo 9</summary>
        static public string GetReqPhotoNine(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqPhotoNine()); }

        /// <summary>Sets photo 9</summary>
        static public void SetReqPhotoNine(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqPhotoNine(), i_link); }

        /// <summary>Returns concert number</summary>
        static public string GetReqConcertNumber(int i_req) { return GetInnerTextReqNode(i_req, GetTagReqConcertNumber()); }

        /// <summary>Sets concert number</summary>
        static public void SetReqConcertNumber(int i_req, string i_link) { SetInnerTextReqNode(i_req, GetTagReqConcertNumber(), i_link); }

        #endregion // Get and set request functions

        #region Get functions for objects JazzReq

        /// <summary>Returns the number of requests defined in XML object m_xdocument_req corresponding to the file JazzAnfragen.xml</summary>
        public static int GetNumberOfRequests(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_req, GetTagReqRequest(), out o_error);
        } // GetNumberOfRequests

        /// <summary>Returns all the requests</summary>
        static public JazzReq[] GetAllRequests(out string o_error)
        {
            o_error = @"";
            JazzReq[] ret_requests = null;

            string error_message = @"";
            int n_requests = GetNumberOfRequests(out error_message);
            if (n_requests <= 0)
            {
                o_error = @"JazzXml.GetAllRequests n_requests <= 0";
                return ret_requests;
            }

            ret_requests = new JazzReq[n_requests];

            for (int request_number = 1; request_number <= n_requests; request_number++)
            {
                JazzReq current_request = new JazzReq();

                current_request.RegNumber = GetReqRegNumber(request_number);
                current_request.RegDay = GetReqRegDay(request_number);
                current_request.RegMonth = GetReqRegMonth(request_number);
                current_request.RegYear = GetReqRegYear(request_number);
                current_request.BandName = GetReqBandName(request_number);
                current_request.Comments = GetReqComments(request_number);
                current_request.PrivateNotes = GetReqPrivateNotes(request_number);
                current_request.BandWebsite = GetReqBandWebsite(request_number);
                current_request.SoundSample = GetReqSoundSample(request_number);
                current_request.AudioOne = GetReqAudioOne(request_number);
                current_request.AudioTwo = GetReqAudioTwo(request_number);
                current_request.AudioThree = GetReqAudioThree(request_number);
                current_request.AudioOneCd = GetReqAudioOneCd(request_number);
                current_request.AudioTwoCd = GetReqAudioTwoCd(request_number);
                current_request.AudioThreeCd = GetReqAudioThreeCd(request_number);
                current_request.ToBeEvaluated = GetReqToBeEvaluated(request_number);
                current_request.InfoOne = GetReqInfoOne(request_number);
                current_request.InfoTwo = GetReqInfoTwo(request_number);
                current_request.InfoThree = GetReqInfoThree(request_number);

                current_request.LinkOne = GetReqLinkOne(request_number);
                current_request.LinkTwo = GetReqLinkTwo(request_number);
                current_request.LinkThree = GetReqLinkThree(request_number);
                current_request.LinkFour = GetReqLinkFour(request_number);
                current_request.LinkFive = GetReqLinkFive(request_number);
                current_request.LinkSix = GetReqLinkSix(request_number);
                current_request.LinkSeven = GetReqLinkSeven(request_number);
                current_request.LinkEight = GetReqLinkEight(request_number);
                current_request.LinkNine = GetReqLinkNine(request_number);

                current_request.LinkTypeOne = GetReqLinkOneType(request_number);
                current_request.LinkTypeTwo = GetReqLinkTwoType(request_number);
                current_request.LinkTypeThree = GetReqLinkThreeType(request_number);
                current_request.LinkTypeFour = GetReqLinkFourType(request_number);
                current_request.LinkTypeFive = GetReqLinkFiveType(request_number);
                current_request.LinkTypeSix = GetReqLinkSixType(request_number);
                current_request.LinkTypeSeven = GetReqLinkSevenType(request_number);
                current_request.LinkTypeEight = GetReqLinkEightType(request_number);
                current_request.LinkTypeNine = GetReqLinkNineType(request_number);

                current_request.LinkTextOne = GetReqLinkOneText(request_number);
                current_request.LinkTextTwo = GetReqLinkTwoText(request_number);
                current_request.LinkTextThree = GetReqLinkThreeText(request_number);
                current_request.LinkTextFour = GetReqLinkFourText(request_number);
                current_request.LinkTextFive = GetReqLinkFiveText(request_number);
                current_request.LinkTextSix = GetReqLinkSixText(request_number);
                current_request.LinkTextSeven = GetReqLinkSevenText(request_number);
                current_request.LinkTextEight = GetReqLinkEightText(request_number);
                current_request.LinkTextNine = GetReqLinkNineText(request_number);

                current_request.PhotoOne = GetReqPhotoOne(request_number);
                current_request.PhotoTwo = GetReqPhotoTwo(request_number);
                current_request.PhotoThree = GetReqPhotoThree(request_number);
                current_request.PhotoFour = GetReqPhotoFour(request_number);
                current_request.PhotoFive = GetReqPhotoFive(request_number);
                current_request.PhotoSix = GetReqPhotoSix(request_number);
                current_request.PhotoSeven = GetReqPhotoSeven(request_number);
                current_request.PhotoEight = GetReqPhotoEight(request_number);
                current_request.PhotoNine = GetReqPhotoNine(request_number);

                current_request.ConcertNumber = GetReqConcertNumber(request_number);

                current_request.SetToEmptyStringsForValuesNotYetSet();

                if (!current_request.CheckParameterValues(out o_error))
                {
                    o_error = @"JazzXmlReq.GetAllRequests JazzReq value not OK " + o_error;
                    return null;
                }

                ret_requests[request_number - 1] = current_request;

            } // Loop request_number

            return ret_requests;

        } // GetAllRequests

        /// <summary>Returns JazzReq object for a given band name</summary>
        static public JazzReq GetRequest(string i_band_name, out string o_error)
        {
            o_error = @"";
            JazzReq ret_request = null;

            string error_message = @"";
            int n_requests = GetNumberOfRequests(out error_message);
            if (n_requests <= 0)
            {
                o_error = @"JazzXml.GetRequest n_requests <= 0 " + error_message;
                return ret_request;
            }

            JazzReq[] all_requests = GetAllRequests(out error_message);
            if (null == all_requests)
            {
                o_error = @"JazzXml.GetRequest all_requests is null " + error_message;
                return ret_request;
            }

            for (int request_index = 0; request_index < n_requests; request_index++)
            {
                JazzReq current_request = all_requests[request_index];

                string band_name = current_request.BandName;

                if (band_name.Equals(i_band_name))
                {
                    ret_request = current_request;
                    return ret_request;
                }
            }

            o_error = @"JazzXml.GetRequest No band name= " + i_band_name;

            return ret_request;

        } // GetRequest

        #endregion // Get functions for objects JazzReq

        #region Set request object

        /// <summary>Set request object data. Empty strings will be replaced by GetUndefinedNodeValue().</summary>
        static public bool SetRequest(JazzReq i_jazz_req, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_req)
            {
                o_error = @"JazzXml.SetRequest Programming error: Input JazzReq is null";
                return false;
            }

            string reg_number = i_jazz_req.RegNumber;

            if (reg_number.Trim().Length == 0)
            {
                o_error = @"JazzXml.SetRequest Registration number is empty";
                return false;
            }

            string error_message = @"";

            JazzReq[] all_jazz_reqs = GetAllRequests(out error_message);

            if (null == all_jazz_reqs || all_jazz_reqs.Length == 0)
            {
                o_error = @"JazzXml.SetRequest No request objects " + error_message;
                return false;
            }

            int jazz_req_object_number = -12345;
            for (int index_jazz_req = 0; index_jazz_req < all_jazz_reqs.Length; index_jazz_req++)
            {
                JazzReq current_jazz_req = all_jazz_reqs[index_jazz_req];
                if (current_jazz_req.RegNumber.Equals(reg_number))
                {
                    jazz_req_object_number = index_jazz_req + 1;
                    break;
                }
            }

            if (jazz_req_object_number < 0)
            {
                o_error = @"JazzXml.SetRequest There is no JazzReq object with registration number " + reg_number;
                return false;
            }

            if (!SetXmlRequest(i_jazz_req, jazz_req_object_number))
            {
                o_error = @"JazzXml.SetRequest SetSeasonXmlDoc failed";
                return false;
            }

            return true;

        } // SetRequest

        /// <summary>Set XML request data. Object GetObjectWithUndefinedNodeValues is set</summary>
        static private bool SetXmlRequest(JazzReq i_jazz_req, int i_req)
        {
            if (null == i_jazz_req)
                return false; // Programming error

            string error_message = @"";
            if (i_req <= 0 || i_req > GetNumberOfRequests(out error_message))
                return false; // Programming error

            JazzReq object_undef_values = i_jazz_req.GetObjectWithUndefinedNodeValues(i_jazz_req);

            SetReqRegNumber(i_req, object_undef_values.RegNumber);
            SetReqRegDay(i_req, object_undef_values.RegDay);
            SetReqRegMonth(i_req, object_undef_values.RegMonth);
            SetReqRegYear(i_req, object_undef_values.RegYear);
            SetReqBandName(i_req, object_undef_values.BandName);
            SetReqComments(i_req, object_undef_values.Comments);
            SetReqPrivateNotes(i_req, object_undef_values.PrivateNotes);
            SetReqBandWebsite(i_req, object_undef_values.BandWebsite);
            SetReqSoundSample(i_req, object_undef_values.SoundSample);
            SetReqAudioOne(i_req, object_undef_values.AudioOne);
            SetReqAudioTwo(i_req, object_undef_values.AudioTwo);
            SetReqAudioThree(i_req, object_undef_values.AudioThree);
            SetReqAudioOneCd(i_req, object_undef_values.AudioOneCd);
            SetReqAudioTwoCd(i_req, object_undef_values.AudioTwoCd);
            SetReqAudioThreeCd(i_req, object_undef_values.AudioThreeCd);
            SetReqToBeEvaluated(i_req, object_undef_values.ToBeEvaluated);
            SetReqInfoOne(i_req, object_undef_values.InfoOne);
            SetReqInfoTwo(i_req, object_undef_values.InfoTwo);
            SetReqInfoThree(i_req, object_undef_values.InfoThree);

            SetReqLinkOne(i_req, object_undef_values.LinkOne);
            SetReqLinkTwo(i_req, object_undef_values.LinkTwo);
            SetReqLinkThree(i_req, object_undef_values.LinkThree);
            SetReqLinkFour(i_req, object_undef_values.LinkFour);
            SetReqLinkFive(i_req, object_undef_values.LinkFive);
            SetReqLinkSix(i_req, object_undef_values.LinkSix);
            SetReqLinkSeven(i_req, object_undef_values.LinkSeven);
            SetReqLinkEight(i_req, object_undef_values.LinkEight);
            SetReqLinkNine(i_req, object_undef_values.LinkNine);

            SetReqLinkOneType(i_req, object_undef_values.LinkTypeOne);
            SetReqLinkTwoType(i_req, object_undef_values.LinkTypeTwo);
            SetReqLinkThreeType(i_req, object_undef_values.LinkTypeThree);
            SetReqLinkFourType(i_req, object_undef_values.LinkTypeFour);
            SetReqLinkFiveType(i_req, object_undef_values.LinkTypeFive);
            SetReqLinkSixType(i_req, object_undef_values.LinkTypeSix);
            SetReqLinkSevenType(i_req, object_undef_values.LinkTypeSeven);
            SetReqLinkEightType(i_req, object_undef_values.LinkTypeEight);
            SetReqLinkNineType(i_req, object_undef_values.LinkTypeNine);

            SetReqLinkOneText(i_req, object_undef_values.LinkTextOne);
            SetReqLinkTwoText(i_req, object_undef_values.LinkTextTwo);
            SetReqLinkThreeText(i_req, object_undef_values.LinkTextThree);
            SetReqLinkFourText(i_req, object_undef_values.LinkTextFour);
            SetReqLinkFiveText(i_req, object_undef_values.LinkTextFive);
            SetReqLinkSixText(i_req, object_undef_values.LinkTextSix);
            SetReqLinkSevenText(i_req, object_undef_values.LinkTextSeven);
            SetReqLinkEightText(i_req, object_undef_values.LinkTextEight);
            SetReqLinkNineText(i_req, object_undef_values.LinkTextNine);

            SetReqPhotoOne(i_req, object_undef_values.PhotoOne);
            SetReqPhotoTwo(i_req, object_undef_values.PhotoTwo);
            SetReqPhotoThree(i_req, object_undef_values.PhotoThree);
            SetReqPhotoFour(i_req, object_undef_values.PhotoFour);
            SetReqPhotoFive(i_req, object_undef_values.PhotoFive);
            SetReqPhotoSix(i_req, object_undef_values.PhotoSix);
            SetReqPhotoSeven(i_req, object_undef_values.PhotoSeven);
            SetReqPhotoEight(i_req, object_undef_values.PhotoEight);
            SetReqPhotoNine(i_req, object_undef_values.PhotoNine);

            SetReqConcertNumber(i_req, object_undef_values.ConcertNumber);

            return true;

        } // SetXmlRequest

        #endregion // Set request object

        #region Add request

        /// <summary>Adds a request node to the requests XDocument object (m_xdocument_req) corresponding to XML file JazzAnfragen.xml</summary>
        /// <param name="i_jazz_req">Object JazzReq that shall be added as a new node</param>
        /// <param name="o_error">Error message</param>
        static public bool AddRequest(JazzReq i_jazz_req, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_req)
            {
                o_error = @"JazzXml.AddRequest Input JazzReq is null";
                return false;
            }

            int reg_number_check = 0; // For a new request
            if (!CheckRequestBandName(i_jazz_req.BandName, reg_number_check, out o_error))
            {
                o_error = @"JazzXml.AddRequest CheckRequestBandName failed " + o_error;
                return false;
            }

            JazzReq mod_jazz_req = i_jazz_req;
            if (!SetRegNumberForAddRequest(ref mod_jazz_req, out o_error))
            {
                o_error = @"JazzXml.AddRequest SetRegNumberForAddRequest failed " + o_error;
                return false;
            }

            XElement request_element = RequestElement(mod_jazz_req, out o_error);

            if (null == request_element)
            {
                o_error = @"JazzXml.AddRequest RequestElement failed " + o_error;
                return false;
            }
                
            XElement appl_root = m_xdocument_req.Root;

            appl_root.Add(request_element);

            return true;

        } // AddRequest

        /// <summary>Creates a request XElement (a sub-tree)
        /// <para>The returned XElement will be added to an existing XDocument</para>
        /// </summary>
        /// <param name="i_jazz_req">JazzRec object that shall become the output XElement</param>
        /// <param name="o_error">Error message</param>
        public static XElement RequestElement(JazzReq i_jazz_req, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_req)
            {
                o_error = @"JazzXml.RequestElement Input JazzReq is null";
                return null;
            }

            XElement ret_element = new XElement(GetTagReqRequest(),
                                   new XElement(GetTagReqRegNumber(), i_jazz_req.RegNumber),
                                   new XElement(GetTagReqRegDay(), i_jazz_req.RegDay),
                                   new XElement(GetTagReqRegMonth(), i_jazz_req.RegMonth),
                                   new XElement(GetTagReqRegYear(), i_jazz_req.RegYear),
                                   new XElement(GetTagReqBandName(), i_jazz_req.BandName),
                                   new XElement(GetTagReqComments(), i_jazz_req.Comments),
                                   new XElement(GetTagReqPrivateNotes(), i_jazz_req.PrivateNotes),
                                   new XElement(GetTagReqBandWebsite(), i_jazz_req.BandWebsite),
                                   new XElement(GetTagReqSoundSample(), i_jazz_req.SoundSample),
                                   new XElement(GetTagReqAudioOne(), i_jazz_req.AudioOne),
                                   new XElement(GetTagReqAudioTwo(), i_jazz_req.AudioTwo),
                                   new XElement(GetTagReqAudioThree(), i_jazz_req.AudioThree),
                                   new XElement(GetTagReqAudioOneCd(), i_jazz_req.AudioOneCd),
                                   new XElement(GetTagReqAudioTwoCd(), i_jazz_req.AudioTwoCd),
                                   new XElement(GetTagReqAudioThreeCd(), i_jazz_req.AudioThreeCd),
                                   new XElement(GetTagReqToBeEvaluated(), i_jazz_req.ToBeEvaluated),
                                   new XElement(GetTagReqInfoOne(), i_jazz_req.InfoOne),
                                   new XElement(GetTagReqInfoTwo(), i_jazz_req.InfoTwo),
                                   new XElement(GetTagReqInfoThree(), i_jazz_req.InfoThree),

                                   new XElement(GetTagReqLinkOne(), i_jazz_req.LinkOne),
                                   new XElement(GetTagReqLinkTwo(), i_jazz_req.LinkTwo),
                                   new XElement(GetTagReqLinkThree(), i_jazz_req.LinkThree),
                                   new XElement(GetTagReqLinkFour(), i_jazz_req.LinkFour),
                                   new XElement(GetTagReqLinkFive(), i_jazz_req.LinkFive),
                                   new XElement(GetTagReqLinkSix(), i_jazz_req.LinkSix),
                                   new XElement(GetTagReqLinkSeven(), i_jazz_req.LinkSeven),
                                   new XElement(GetTagReqLinkEight(), i_jazz_req.LinkEight),
                                   new XElement(GetTagReqLinkNine(), i_jazz_req.LinkNine),

                                   new XElement(GetTagReqLinkOneType(), i_jazz_req.LinkTypeOne),
                                   new XElement(GetTagReqLinkTwoType(), i_jazz_req.LinkTypeTwo),
                                   new XElement(GetTagReqLinkThreeType(), i_jazz_req.LinkTypeThree),
                                   new XElement(GetTagReqLinkFourType(), i_jazz_req.LinkTypeFour),
                                   new XElement(GetTagReqLinkFiveType(), i_jazz_req.LinkTypeFive),
                                   new XElement(GetTagReqLinkSixType(), i_jazz_req.LinkTypeSix),
                                   new XElement(GetTagReqLinkSevenType(), i_jazz_req.LinkTypeSeven),
                                   new XElement(GetTagReqLinkEightType(), i_jazz_req.LinkTypeEight),
                                   new XElement(GetTagReqLinkNineType(), i_jazz_req.LinkTypeNine),

                                   new XElement(GetTagReqLinkOneText(), i_jazz_req.LinkTextOne),
                                   new XElement(GetTagReqLinkTwoText(), i_jazz_req.LinkTextTwo),
                                   new XElement(GetTagReqLinkThreeText(), i_jazz_req.LinkTextThree),
                                   new XElement(GetTagReqLinkFourText(), i_jazz_req.LinkTextFour),
                                   new XElement(GetTagReqLinkFiveText(), i_jazz_req.LinkTextFive),
                                   new XElement(GetTagReqLinkSixText(), i_jazz_req.LinkTextSix),
                                   new XElement(GetTagReqLinkSevenText(), i_jazz_req.LinkTextSeven),
                                   new XElement(GetTagReqLinkEightText(), i_jazz_req.LinkTextEight),
                                   new XElement(GetTagReqLinkNineText(), i_jazz_req.LinkTextNine),

                                   new XElement(GetTagReqPhotoOne(), i_jazz_req.PhotoOne),
                                   new XElement(GetTagReqPhotoTwo(), i_jazz_req.PhotoTwo),
                                   new XElement(GetTagReqPhotoThree(), i_jazz_req.PhotoThree),
                                   new XElement(GetTagReqPhotoFour(), i_jazz_req.PhotoFour),
                                   new XElement(GetTagReqPhotoFive(), i_jazz_req.PhotoFive),
                                   new XElement(GetTagReqPhotoSix(), i_jazz_req.PhotoSix),
                                   new XElement(GetTagReqPhotoSeven(), i_jazz_req.PhotoSeven),
                                   new XElement(GetTagReqPhotoEight(), i_jazz_req.PhotoEight),
                                   new XElement(GetTagReqPhotoNine(), i_jazz_req.PhotoNine),

                                   new XElement(GetTagReqConcertNumber(), i_jazz_req.ConcertNumber)

                                   );

            return ret_element;

        } // RequestElement

        /// <summary>Set registration number for the JazzRec object that shall be added 
        /// <para>1. Get last register name. Call of GetReqLastRegNumber</para>
        /// <para>2. Convert to integer, add one (1) and save. Call of SetReqLastRegNumber.</para>
        /// <para>3. Set register number of the input JazzReq object (JazzReq.RegNumber)</para>
        /// </summary>
        /// <param name="i_jazz_req">Object JazzReq</param>
        /// <param name="o_error">Error message</param>
        static private bool SetRegNumberForAddRequest(ref JazzReq i_jazz_req, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_req)
            {
                o_error = @"JazzXml.SetRegNumberForAddRequest Input JazzReq is null";
                return false;
            }

            string reg_number_last_str = GetReqLastRegNumber();

            int reg_number_last = JazzUtils.StringToInt(reg_number_last_str);
            if (reg_number_last <= 0)
            {
                o_error = @"JazzXml.SetRegNumberForAddRequest reg_number_last= " + reg_number_last.ToString() + @" <= 0";
                return false;
            }

            int new_reg_number = reg_number_last + 1;

            string new_reg_number_str = new_reg_number.ToString();

            SetReqLastRegNumber(new_reg_number_str);

            i_jazz_req.RegNumber = new_reg_number_str;

            return true;

        } // SetRegNumberForAddRequest

        #endregion // Add request

        #region Remove request

        /// <summary>Removes a request XElement of the requests XDocument object (m_xdocument_req) corresponding to the XML file JazzAnfragen.xml</summary>
        ///  <param name="i_jazz_req">Object JazzReq that shall be removed.</param>
        static public bool RemoveRequest(JazzReq i_jazz_req, out string o_error)
        {
            o_error = @"";

            if (null == i_jazz_req)
            {
                o_error = @"JazzXml.RemoveRequest Input JazzReq is null";
                return false;
            }

            int n_requests = GetNumberOfRequests(out o_error);
            if (n_requests <= 0)
            {
                o_error = @"JazzXml.RemoveRequest Number of requests <= 0";
                return false;
            }

            if (n_requests == 1)
            {
                o_error = @"JazzXml.RemoveRequest Number of requests == 1. Last request cannot be delleted";
                return false;
            }

            string error_message = @"";

            JazzReq[] all_jazz_reqs = GetAllRequests(out error_message);

            if (null == all_jazz_reqs || all_jazz_reqs.Length == 0)
            {
                o_error = @"JazzXml.RemoveRequest No request objects " + error_message;
                return false;
            }

            int jazz_req_object_number = -12345;
            for (int index_jazz_req = 0; index_jazz_req < all_jazz_reqs.Length; index_jazz_req++)
            {
                JazzReq current_jazz_req = all_jazz_reqs[index_jazz_req];
                if (current_jazz_req.RegNumber.Equals(i_jazz_req.RegNumber))
                {
                    jazz_req_object_number = index_jazz_req + 1;
                    break;
                }
            }

            int current_request_number = 0;
            foreach (XElement element_request in m_xdocument_req.Descendants(GetTagReqRequest()))
            {
                current_request_number = current_request_number + 1;
                if (jazz_req_object_number == current_request_number)
                {
                    element_request.Remove();

                    return true;

                } // current_request_number == current_request_number

            } // Loop all request elements

            o_error = @"JazzXml.RemoveRequest No request was removed. Programming error";
            return false;

        } // RemoveRequest

        #endregion // Remove request

        #region Check functions

        /// <summary>Check band name
        /// <para>The band name is not allowed to be empty.</para>
        /// <para>The band name must be unique because the calling application lets the user select the band name (and not RegNumber that is unique)</para>
        /// <para></para>
        /// </summary>
        /// <param name="i_band_name">The band name that shall be checked</param>
        /// <param name="i_reg_number">Registration number for the request that is  being edited. Eq. 0: New request</param>
        /// <param name="o_error">Error message</param>
        static public bool CheckRequestBandName(string i_band_name, int i_reg_number, out string o_error)
        {
            o_error = @"";

            if (!JazzXml.XmlNodeValueIsSet(i_band_name))
            {
                o_error = @"JazzXml.CheckRequestBandName Error: Band name is " + JazzXml.GetUndefinedNodeValue();

                return false;
            }

            if (i_band_name.Trim().Length == 0)
            {
                o_error = @"JazzXml.CheckRequestBandName Error: Band name is empty";

                return false;
            }

            string error_message = @"";

            JazzReq[] all_jazz_reqs = GetAllRequests(out error_message);

            if (null == all_jazz_reqs || all_jazz_reqs.Length == 0)
            {
                o_error = @"JazzXml.CheckRequestBandName No request objects " + error_message;
                return false;
            }

            for (int index_jazz_req = 0; index_jazz_req < all_jazz_reqs.Length; index_jazz_req++)
            {
                JazzReq current_jazz_req = all_jazz_reqs[index_jazz_req];
                int current_reg_number = current_jazz_req.RegNumberInt;

                // Only check if it isn't the request that is being edited.
                if (i_reg_number != current_reg_number)
                {
                    if (current_jazz_req.BandName.Equals(i_band_name))
                    {
                        o_error = @"JazzXml.CheckRequestBandName Band name " + i_band_name + @" for JazzReq object with registration number " + current_jazz_req.RegNumber;
                        return false;
                    }
                }

            } // index_jazz_req

            return true;

        } // CheckRequestBandName

        /// <summary>Check registration number
        /// <para>The registration number is not allowed to be empty.</para>
        /// <para>The registration number must be between 1 and last registration number GetReqLastRegNumber()</para>
        /// <para>The registration number must be unique (and a new number must be last number + 1)</para>
        /// <para></para>
        /// </summary>
        /// <param name="o_error">Error message</param>
        static public bool CheckRequestRegNumber(string i_reg_number_str, out string o_error)
        {
            o_error = @"";

            if (!JazzXml.XmlNodeValueIsSet(i_reg_number_str))
            {
                o_error = @"JazzXml.CheckRequestRegNumber Error: Registration number is " + JazzXml.GetUndefinedNodeValue();

                return false;
            }

            if (i_reg_number_str.Trim().Length == 0)
            {
                o_error = @"JazzXml.CheckRequestRegNumber Error: Registration number is empty";

                return false;
            }

            string reg_number_last_str = GetReqLastRegNumber();
            int reg_number_last = JazzUtils.StringToInt(reg_number_last_str);

            if (reg_number_last <= 0)
            {
                o_error = @"JazzXml.CheckRequestRegNumber reg_number_last= " + reg_number_last.ToString() + @" <= 0";
                return false;
            }

            int reg_number = JazzUtils.StringToInt(i_reg_number_str);
            if (reg_number <= 0)
            {
                o_error = @"JazzXml.CheckRequestRegNumber reg_number= " + reg_number.ToString() + @" <= 0";
                return false;
            }

            if (reg_number < 1)
            {
                o_error = @"JazzXml.CheckRequestRegNumber reg_number= " + reg_number.ToString() + @" < 1";
                return false;
            }

            if (reg_number > reg_number_last)
            {
                o_error = @"JazzXml.CheckRequestRegNumber reg_number= " + reg_number.ToString() + @" > reg_number_last= " + reg_number_last.ToString();
                return false;
            }

            string error_message = @"";

            JazzReq[] all_jazz_reqs = GetAllRequests(out error_message);

            if (null == all_jazz_reqs || all_jazz_reqs.Length == 0)
            {
                o_error = @"JazzXml.CheckRequestRegNumber No request objects " + error_message;
                return false;
            }

            for (int index_jazz_req = 0; index_jazz_req < all_jazz_reqs.Length; index_jazz_req++)
            {
                JazzReq current_jazz_req = all_jazz_reqs[index_jazz_req];
                if (current_jazz_req.BandName.Equals(i_reg_number_str))
                {
                    o_error = @"JazzXml.CheckRequestRegNumber Registration number " + current_jazz_req.RegNumber + @" already exists for index= " + index_jazz_req.ToString();
                    return false;
                }
            }

            return true;

        } // CheckRequestRegNumber

        #endregion // Check functions

    } // JazzXml

} // namespace
