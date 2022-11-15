using System;
using System.Linq;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>XML functions specific for the JazzAdmin application 
    /// <para>Before the defined functions in this C# file (JazzXmlAdmin.cs) can be called the InitDoc function defined in JazzXmlDoc must be called.</para>
    /// <para>The InitDoc function creates five (5) arrays corresponding to the  JazzDokumente_20xx_20yy.xml files. 
    /// For the Admin application an additional URL array must be created</para>
    /// <para>This URL array with server file addresses is created by function InitAdmin.</para>
    /// <para></para>
    /// <para>The set and get functions for documents are based on an active XML object (member variable m_xdocument_active_doc) that must be set by the Admin application (SetObjectActiveDoc).</para>
    /// <para>Available functions to set the the active season XML object are: SetActiveXmlObjectAndFile, SetActiveXmlObjectAndFileToThisSeason and SetActiveXmlObjectAndFileToNextSeason</para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    static public partial class JazzXml
    {
        /// <summary>Initialization of member parameters</summary>
        static public bool InitAdmin(out string o_error)
        {
            o_error = @"";

            if (!SetSeasonFileUrls(out o_error))
            {
                o_error = @"JazzXml.InitAdmin " + o_error;
                return false;
            }

            return true;
        } // InitAdmin

        #region XML file names

        /// <summary>The URL for the XML file corresponding to the (current set) season XML object (m_document_current)</summary>
        static private string m_document_current_file_url = @"";

        /// <summary>Get the URL for the XML file corresponding to the (current set) season XML object (m_document_current)</summary>
        static public string GetCurrentSeasonFileUrl() { return m_document_current_file_url; }

        /// <summary>The URLs for the season XML files on the server XML directory, i.e. the XMLs that the app uses</summary>
        static private string[] m_season_file_urls = null;

        /// <summary>Sets the string array (m_season_file_urls) of season file names: http: ..JazzProgramm_1996_1997,  etc
        /// <para>This array is only needed for the administration application, i.e. an array corresponding to the array of existing XDocuments </para>
        /// <para>The mobile application only use URLs for the initialization of XDocuments</para>
        /// </summary>
        static private bool SetSeasonFileUrls(out string o_error)
        {
            o_error = @"";
            int[] start_years = JazzUtils.GetSeasonStartYearsForExistingXmlFiles();
            if (null == start_years || start_years.Length == 0)
            {
                o_error = @"JazzXml.SetSeasonFileUrls Array start_years is null or has no elements";
            }

            m_season_file_urls = GetSeasonXmlFileUrls(start_years);
            if (null == m_season_file_urls || m_season_file_urls.Length == 0)
            {
                o_error = @"JazzXml.SetSeasonFileUrls Array m_season_file_urls is null or has no elements";
            }

            return true;
        } // SetSeasonFileUrls

        /// <summary>Returns a string array of season file names: http: ..JazzProgramm_1996_1997,  etc
        /// <para>Please note that there is an XML file on the server for each file name</para>
        /// </summary>
        /// <param name="i_start_years">Array of start years</param>
        private static String[] GetSeasonXmlFileUrls(int[] i_start_years)
        {
            String[] ret_strings = null;

            if (i_start_years.Length == 0)
            {
                return ret_strings; // null
            }

            ret_strings = new String[i_start_years.Length];

            for (int i_str = 0; i_str < i_start_years.Length; i_str++)
            {
                ret_strings[i_str] = JazzXml.GetSeasonFileName(i_start_years[i_str]);
            }

            return ret_strings;

        } // GetSeasonXmlFileUrls

        /// <summary>Set the URL for the XML file corresponding to the (current set) season XML object (m_document_current)</summary>
        static public void SetCurrentSeasonFileUrl()
        {
            if (null == m_season_file_urls)
                return;
            if (null == m_seasons_documents)
                return;

            if (m_season_file_urls.Length != m_seasons_documents.Length)
                return;

            XDocument current_doc = JazzXml.GetDocumentCurrent();

            for (int index_doc=0; index_doc< m_season_file_urls.Length; index_doc++)
            {
                if (current_doc == m_seasons_documents[index_doc])
                {
                    m_document_current_file_url = m_season_file_urls[index_doc];
                    return;
                }
            }

            // Error coming here

        } // SetCurrentSeasonFileUrl

        #endregion // XML file names

        #region Set values for the application XML file

        /// <summary>Sets the about aus header</summary>
        static public void SetAboutUsHeader(string i_about_us_header) { SetInnerTextForApplicationNode(m_text_tags_appl[0], i_about_us_header); }

        /// <summary>Sets the about aus text one</summary>
        static public void SetAboutUsOne(string i_about_us_one) { SetInnerTextForApplicationNode(m_text_tags_appl[1], i_about_us_one); }

        /// <summary>Sets the about aus text two</summary>
        static public void SetAboutUsTwo(string i_about_us_two) { SetInnerTextForApplicationNode(m_text_tags_appl[2], i_about_us_two); }

        /// <summary>Sets the about aus text three</summary>
        static public void SetAboutUsThree(string i_about_us_three) { SetInnerTextForApplicationNode(m_text_tags_appl[3], i_about_us_three); }

        /// <summary>Sets the premises header</summary>
        static public void SetPremisesHeader(string i_premises_header) { SetInnerTextForApplicationNode(m_text_tags_appl[4], i_premises_header); }

        /// <summary>Sets the premises name</summary>
        static public void SetPremises(string i_premises) { SetInnerTextForApplicationNode(m_text_tags_appl[5], i_premises); }

        /// <summary>Sets the premises street</summary>
        static public void SetPremisesStreet(string i_premises_street) { SetInnerTextForApplicationNode(m_text_tags_appl[6], i_premises_street); }

        /// <summary>Sets the premises city</summary>
        static public void SetPremisesCity(string i_premises_city) { SetInnerTextForApplicationNode(m_text_tags_appl[7], i_premises_city); }

        /// <summary>Sets the premises website</summary>
        static public void SetPremisesWebsite(string i_premises_website) { SetInnerTextForApplicationNode(m_text_tags_appl[8], i_premises_website); }

        /// <summary>Sets the premises website</summary>
        static public void SetPremisesTelephone(string i_premises_telephone) { SetInnerTextForApplicationNode(m_text_tags_appl[9], i_premises_telephone); }

        /// <summary>Sets the premises photo</summary>
        static public void SetPremisesPhoto(string i_premises_photo) { SetInnerTextForApplicationNode(m_text_tags_appl[10], i_premises_photo); }

        /// <summary>Sets the premises map</summary>
        static public void SetPremisesMap(string i_premises_map) { SetInnerTextForApplicationNode(m_text_tags_appl[11], i_premises_map); }

        /// <summary>Sets the contacts header</summary>
        static public void SetContactsHeader(string i_contact_header) { SetInnerTextForApplicationNode(m_text_tags_appl[12], i_contact_header); }

        /// <summary>Sets the mail header</summary>
        static public void SetMailHeader(string i_mail_header) { SetInnerTextForApplicationNode(m_text_tags_appl[13], i_mail_header); }

        /// <summary>Sets the email header</summary>
        static public void SetEmailHeader(string i_email_header) { SetInnerTextForApplicationNode(m_text_tags_appl[14], i_email_header); }

        /// <summary>Sets the reservation header</summary>
        static public void SetReservationHeader(string i_reservation_header) { SetInnerTextForApplicationNode(m_text_tags_appl[15], i_reservation_header); }

        /// <summary>Sets the newsletter header</summary>
        static public void SetNewsletterHeader(string i_newsletter_header) { SetInnerTextForApplicationNode(m_text_tags_appl[16], i_newsletter_header); }

        /// <summary>Sets the webmaster header</summary>
        static public void SetWebmasterHeader(string i_webmaster_header) { SetInnerTextForApplicationNode(m_text_tags_appl[17], i_webmaster_header); }

        /// <summary>Sets the club name</summary>
        static public void SetClubName(string i_club_name) { SetInnerTextForApplicationNode(m_text_tags_appl[18], i_club_name); }

        /// <summary>Sets the club email address</summary>
        static public void SetMailAddress(string i_club_email) { SetInnerTextForApplicationNode(m_text_tags_appl[19], i_club_email); }

        /// <summary>Sets the JAZZ live AARAU email address</summary>
        static public void SetEmailJazzLiveAarau(string i_email) { SetInnerTextForApplicationNode(m_text_tags_appl[20], i_email); }

        /// <summary>Sets the JAZZ live AARAU email reservation address</summary>
        static public void SetEmailReservation(string i_email_reservation) { SetInnerTextForApplicationNode(m_text_tags_appl[21], i_email_reservation); }

        /// <summary>Sets the JAZZ live AARAU email reservation subject</summary>
        static public void SetReservationSubject(string i_email_reservation_subject) { SetInnerTextForApplicationNode(m_text_tags_appl[22], i_email_reservation_subject); }

        /// <summary>Sets the JAZZ live AARAU email reservation text</summary>
        static public void SetReservationText(string i_email_reservation_text) { SetInnerTextForApplicationNode(m_text_tags_appl[23], i_email_reservation_text); }

        /// <summary>Sets the JAZZ live AARAU email newsletter subject</summary>
        static public void SetNewsletterSubject(string i_email_newsletter_subject) { SetInnerTextForApplicationNode(m_text_tags_appl[24], i_email_newsletter_subject); }

        /// <summary>Sets the JAZZ live AARAU email newsletter text</summary>
        static public void SetNewsletterText(string i_email_newsletter_text) { SetInnerTextForApplicationNode(m_text_tags_appl[25], i_email_newsletter_text); }

        /// <summary>Sets the webmaster telephone number</summary>
        static public void SetTelephoneWebmaster(string i_webmaster_telephone) { SetInnerTextForApplicationNode(m_text_tags_appl[26], i_webmaster_telephone); }

        /// <summary>Sets the webmaster email address</summary>
        static public void SetEmailWebmaster(string i_webmaster_email) { SetInnerTextForApplicationNode(m_text_tags_appl[27], i_webmaster_email); }

        /// <summary>Sets the concert contact member number </summary>
        static public void SetContactConcertMemberNumber(string i_member_number) { SetInnerTextForApplicationNode(m_text_tags_appl[28], i_member_number); }

        /// <summary>Sets the concert contact telephone number </summary>
        static public void SetContactConcertTelephone(string i_telephone_number) { SetInnerTextForApplicationNode(m_text_tags_appl[29], i_telephone_number); }

        /// <summary>Sets the concert contact telephone number </summary>
        static public void SetContactConcertEmail(string i_email) { SetInnerTextForApplicationNode(m_text_tags_appl[30], i_email); }

        /// <summary>Sets the unload street </summary>
        static public void SetUnloadStreet(string i_unload_street) { SetInnerTextForApplicationNode(m_text_tags_appl[31], i_unload_street); }

        /// <summary>Sets the unload city </summary>
        static public void SetUnloadCity(string i_unload_city) { SetInnerTextForApplicationNode(m_text_tags_appl[32], i_unload_city); }

        /// <summary>Sets parking one </summary>
        static public void SetParkingOne(string i_parking_one) { SetInnerTextForApplicationNode(m_text_tags_appl[33], i_parking_one); }

        /// <summary>Sets parking two </summary>
        static public void SetParkingTwo(string i_parking_two) { SetInnerTextForApplicationNode(m_text_tags_appl[34], i_parking_two); }

        /// <summary>Sets publish season start year </summary>
        static public void SetPublishSeasonStartYear(string i_publish_start_year) { SetInnerTextForApplicationNode(m_text_tags_appl[35], i_publish_start_year); }

        /// <summary>Sets request caption</summary>
        static public void SetRequestCaption(string i_request_caption) { SetInnerTextForApplicationNode(GetTagRequestCaption(), i_request_caption); }

        /// <summary>Sets request header</summary>
        static public void SetRequestHeader(string i_request_header) { SetInnerTextForApplicationNode(GetTagRequestHeader(), i_request_header); }

        /// <summary>Sets request dates display flag</summary>
        static public void SetRequestDatesDisplay(string i_request_dates_display_flag) { SetInnerTextForApplicationNode(GetTagRequestDatesDisplay(), i_request_dates_display_flag); }

        /// <summary>Sets the flag telling if the concert dates shall be displayed</summary>
        static public void SetRequestDatesDisplayBool(bool i_display_dates)
        {
            if (i_display_dates)
            {
                SetRequestDatesDisplay("TRUE");
            }
            else
            {
                SetRequestDatesDisplay("FALSE");
            }

        } // SetRequestDatesDisplayBool

        /// <summary>Sets request no dates text</summary>
        static public void SetRequestNoDatesText(string i_request_no_dates_text) { SetInnerTextForApplicationNode(GetTagRequestNoDatesText(), i_request_no_dates_text); }

        /// <summary>Sets request dates text</summary>
        static public void SetRequestDatesText(string i_request_dates_text) { SetInnerTextForApplicationNode(GetTagRequestDatesText(), i_request_dates_text); }

        /// <summary>Sets request content header</summary>
        static public void SetRequestContentHeader(string i_request_content_header) { SetInnerTextForApplicationNode(GetTagRequestContentHeader(), i_request_content_header); }

        /// <summary>Sets request content one</summary>
        static public void SetRequestContentOne(string i_request_content_one) { SetInnerTextForApplicationNode(GetTagRequestContentOne(), i_request_content_one); }

        /// <summary>Sets request content two</summary>
        static public void SetRequestContentTwo(string i_request_content_two) { SetInnerTextForApplicationNode(GetTagRequestContentTwo(), i_request_content_two); }

        /// <summary>Sets request content three</summary>
        static public void SetRequestContentThree(string i_request_content_three) { SetInnerTextForApplicationNode(GetTagRequestContentThree(), i_request_content_three); }

        /// <summary>Sets request content 4</summary>
        static public void SetRequestContentFour(string i_request_content_four) { SetInnerTextForApplicationNode(GetTagRequestContentFour(), i_request_content_four); }

        /// <summary>Sets request content five</summary>
        static public void SetRequestContentFive(string i_request_content_five) { SetInnerTextForApplicationNode(GetTagRequestContentFive(), i_request_content_five); }

        /// <summary>Sets request content six</summary>
        static public void SetRequestContentSix(string i_request_content_six) { SetInnerTextForApplicationNode(GetTagRequestContentSix(), i_request_content_six); }

        /// <summary>Sets request content seven</summary>
        static public void SetRequestContentSeven(string i_request_content_seven) { SetInnerTextForApplicationNode(GetTagRequestContentSeven(), i_request_content_seven); }

        /// <summary>Sets request content eight</summary>
        static public void SetRequestContentEight(string i_request_content_eight) { SetInnerTextForApplicationNode(GetTagRequestContentEight(), i_request_content_eight); }

        /// <summary>Sets request content nine</summary>
        static public void SetRequestContentNine(string i_request_content_nine) { SetInnerTextForApplicationNode(GetTagRequestContentNine(), i_request_content_nine); }

        /// <summary>Sets request email address</summary>
        static public void SetRequestEmailAddress(string i_request_email_address) { SetInnerTextForApplicationNode(GetTagRequestEmailAddress(), i_request_email_address); }

        /// <summary>Sets request email title</summary>
        static public void SetRequestEmailTitle(string i_request_email_title) { SetInnerTextForApplicationNode(GetTagRequestEmailTitle(), i_request_email_title); }

        /// <summary>Sets request email caption</summary>
        static public void SetRequestEmailCaption(string i_request_email_caption) { SetInnerTextForApplicationNode(GetTagRequestEmailCaption(), i_request_email_caption); }

        /// <summary>Sets request remark</summary>
        static public void SetRequestEmailRemark(string i_request_email_remark) { SetInnerTextForApplicationNode(GetTagRequestEmailRemark(), i_request_email_remark); }

        /// <summary>Sets request end paragraph</summary>
        static public void SetRequestEndParagraph(string i_request_end_paragraph) { SetInnerTextForApplicationNode(GetTagRequestEndParagraph(), i_request_end_paragraph); }

        /// <summary>Sets the flag (string TRUE or FALSE) telling if it is allowed to make reservations</summary>
        static public void SetReservationNotAllowed(string i_reservation_not_allowed_flag) { SetInnerTextForApplicationNode(GetTagReservationNotAllowed(), i_reservation_not_allowed_flag); }

        /// <summary>Sets the text for the case that reservations not are allowed</summary>
        static public void SetReservationNotAllowedTextReservationNotAllowedText(string i_reservation_not_allowed_text) { SetInnerTextForApplicationNode(GetTagReservationNotAllowedText(), i_reservation_not_allowed_text); }

        #endregion // Set values for the application XML file

        #region Set single XML elements of the season XML files

        /// <summary>Sets flag defining if the season program shall be published</summary>
        static public void SetPublishProgram(bool i_publish_program)
        {
            string publish_program = @"FALSE";
            if (i_publish_program)
                publish_program = @"TRUE";

            SetInnerTextForConcertsNode(m_text_tags_season[0], publish_program);

        } // PublishProgram

        /// <summary>Returns true if season program may be published </summary>
        static public bool GetPublishProgramBool()
        {
            string publish_program = GetPublishProgram();
            if (publish_program.Equals(@"TRUE"))
                return true;
            else
                return false;
        } // GetPublishProgramBool

        /// <summary>Sets flag defining if the season program shall be published</summary>
        static public void SetPublishProgram(string i_publish_program)
        {
            SetInnerTextForConcertsNode(m_text_tags_season[0], i_publish_program);
        } // SetPublishProgram

        /// <summary>Sets year autumn</summary>
        static public void SetYearAutum(string i_year_autumn)
        {
            SetInnerTextForConcertsNode(m_text_tags_season[1], i_year_autumn);
        }

        /// <summary>Sets year spring</summary>
        static public void SetYearSpring(string i_year_spring)
        {
            SetInnerTextForConcertsNode(m_text_tags_season[2], i_year_spring);
        }

        #endregion // Set single XML elements of the season XML files

        #region Set concert data values in the season XML files


        /// <summary>Sets the name of the concert</summary>
        static public void SetBandName(int i_concert, string i_band_name) { SetInnerTextForNode(i_concert, m_text_tags_concert[0], i_band_name); }

        /// <summary>Sets the year of the concert as string</summary>
        static public void SetYear(int i_concert, string i_year) { SetInnerTextForNode(i_concert, m_text_tags_concert[1], i_year); }

        /// <summary>Sets the month of the concert as string</summary>
        static public void SetMonth(int i_concert, string i_month) { SetInnerTextForNode(i_concert, m_text_tags_concert[2], i_month); }

        /// <summary>Sets the day of the concert as string</summary>
        static public void SetDay(int i_concert, string i_day) { SetInnerTextForNode(i_concert, m_text_tags_concert[3], i_day); }

        /// <summary>Sets the start hour of the concert as string</summary>
        static public void SetStartHour(int i_concert, string i_start_hour) { SetInnerTextForNode(i_concert, m_text_tags_concert[4], i_start_hour); }

        /// <summary>Sets the start minute of the concert as string</summary>
        static public void SetStartMinute(int i_concert, string i_start_minute) { SetInnerTextForNode(i_concert, m_text_tags_concert[5], i_start_minute); }

        /// <summary>Sets the end hour of the concert as string</summary>
        static public void SetEndHour(int i_concert, string i_end_hour) { SetInnerTextForNode(i_concert, m_text_tags_concert[6], i_end_hour); }

        /// <summary>Sets the end minute of the concert as string</summary>
        static public void SetEndMinute(int i_concert, string i_end_minute) { SetInnerTextForNode(i_concert, m_text_tags_concert[7], i_end_minute); }

        /// <summary>Sets the short text for the concert</summary>
        static public void SetShortText(int i_concert, string i_short_text) { SetInnerTextForNode(i_concert, m_text_tags_concert[8], i_short_text); }

        /// <summary>Sets the additional text for the concert</summary>
        static public void SetAdditionalText(int i_concert, string i_additional_text) { SetInnerTextForNode(i_concert, m_text_tags_concert[9], i_additional_text); }

        /// <summary>Sets the additional text header for the concert</summary>
        static public void SetLabelAdditionalText(int i_concert, string i_label_additional_text)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return;
            }

            SetInnerTextForNode(i_concert, m_text_tags_concert[32], i_label_additional_text);

        } // SetLabelAdditionalText

        /// <summary>Sets the flyer text header for the concert</summary>
        static public void SetLabelFlyerText(int i_concert, string i_label_flyer_text)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return;
            }

            SetInnerTextForNode(i_concert, m_text_tags_concert[33], i_label_flyer_text);

        } // SetLabelFlyerText

        /// <summary>Sets the flyer text for the concert</summary>
        static public void SetFlyerText(int i_concert, string i_flyer_text)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2019)
            {
                return;
            }

            SetInnerTextForNode(i_concert, m_text_tags_concert[34], i_flyer_text);

        } // SetFlyerText

        /// <summary>Sets the sound sample URL for the concert</summary>
        static public void SetSoundSample(int i_concert, string i_sound_sample) { SetInnerTextForNode(i_concert, m_text_tags_concert[10], i_sound_sample); }

        /// <summary>Sets the band website URL for the concert</summary>
        static public void SetBandWebsite(int i_concert, string i_web_site) { SetInnerTextForNode(i_concert, m_text_tags_concert[11], i_web_site); }

        /// <summary>Sets the sound sample URL QR code for the concert</summary>
        static public void SetSoundSampleQrCode(int i_concert, string i_sound_sample_qr)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2020)
            {
                return;
            }

            SetInnerTextForNode(i_concert, m_text_tags_concert[37], i_sound_sample_qr);

        } // SetSoundSampleQrCode

        /// <summary>Sets the band website URL QR code for the concert</summary>
        static public void SetBandWebsiteQrCode(int i_concert, string i_web_site_qr)
        {
            int autumn_year = GetYearAutumnInt();
            if (autumn_year < 2020)
            {
                return;
            }

            SetInnerTextForNode(i_concert, m_text_tags_concert[38], i_web_site_qr);

        } // SetBandWebsiteQrCode

        /// <summary>Sets the photo gallery one URL for the concert</summary>
        static public void SetPhotoGalleryOne(int i_concert, string i_photo_url_1) { SetInnerTextForNode(i_concert, m_text_tags_concert[12], i_photo_url_1); }

        /// <summary>Sets the photo gallery two URL for the concert</summary>
        static public void SetPhotoGalleryTwo(int i_concert, string i_photo_url_2) { SetInnerTextForNode(i_concert, m_text_tags_concert[13], i_photo_url_2); }

        /// <summary>Sets the photo gallery one ZIP URL for the concert</summary>
        static public void SetPhotoGalleryOneZip(int i_concert, string i_photo_zip_url_1) { SetInnerTextForNode(i_concert, m_text_tags_concert[14], i_photo_zip_url_1); }

        /// <summary>Sets the photo gallery two ZIP URL for the concert</summary>
        static public void SetPhotoGalleryTwoZip(int i_concert, string i_photo_zip_url_2) { SetInnerTextForNode(i_concert, m_text_tags_concert[15], i_photo_zip_url_2); }

        /// <summary>Sets the contact person for the band</summary>
        static public void SetContactPerson(int i_concert, string i_contact_person) { SetInnerTextForNode(i_concert, m_text_tags_concert[16], i_contact_person); }

        /// <summary>Sets the contact email for the band</summary>
        static public void SetContactEmail(int i_concert, string i_contact_email) { SetInnerTextForNode(i_concert, m_text_tags_concert[17], i_contact_email); }

        /// <summary>Sets the contact telephone for the band</summary>
        static public void SetContactTelephone(int i_concert, string i_contact_telephone) { SetInnerTextForNode(i_concert, m_text_tags_concert[18], i_contact_telephone); }

        /// <summary>Sets the contact street for the band</summary>
        static public void SetContactStreet(int i_concert, string i_contact_street) { SetInnerTextForNode(i_concert, m_text_tags_concert[19], i_contact_street); }

        /// <summary>Sets the contact post code  for the band</summary>
        static public void SetContactPostCode(int i_concert, string i_contact_post_code) { SetInnerTextForNode(i_concert, m_text_tags_concert[20], i_contact_post_code); }

        /// <summary>Sets the contact city  for the band</summary>
        static public void SetContactCity(int i_concert, string i_contact_city) { SetInnerTextForNode(i_concert, m_text_tags_concert[21], i_contact_city); }

        /// <summary>Sets the contact IBAN  for the band</summary>
        static public void SetIbanNumber(int i_concert, string i_contact_iban_number) { SetInnerTextForNode(i_concert, m_text_tags_concert[30], i_contact_iban_number); }

        /// <summary>Sets the contact remark  for the band</summary>
        static public void SetContactRemark(int i_concert, string i_contact_remark) { SetInnerTextForNode(i_concert, m_text_tags_concert[31], i_contact_remark); }

        /// <summary>Sets the login password  for the band</summary>
        static public void SetLoginPassword(int i_concert, string i_login_password) { SetInnerTextForNode(i_concert, m_text_tags_concert[22], i_login_password); }

        /// <summary>Sets the premises for the concert</summary>
        static public void SetPlace(int i_concert, string i_place) { SetInnerTextForNode(i_concert, m_text_tags_concert[23], i_place); }

        /// <summary>Sets the premises street for the concert</summary>
        static public void SetStreet(int i_concert, string i_street) { SetInnerTextForNode(i_concert, m_text_tags_concert[24], i_street); }

        /// <summary>Sets the premises city for the concert</summary>
        static public void SetCity(int i_concert, string i_city) { SetInnerTextForNode(i_concert, m_text_tags_concert[25], i_city); }

        /// <summary>Sets the big poster URL for the concert</summary>
        static public void SetPosterBigSize(int i_concert, string i_poster_big) { SetInnerTextForNode(i_concert, m_text_tags_concert[26], i_poster_big); }

        /// <summary>Sets the mid poster URL for the concert</summary>
        static public void SetPosterMidSize(int i_concert, string i_poster_mid) { SetInnerTextForNode(i_concert, m_text_tags_concert[27], i_poster_mid); }

        /// <summary>Sets the small poster URL for the concert</summary>
        static public void SetPosterSmallSize(int i_concert, string i_poster_small) { SetInnerTextForNode(i_concert, m_text_tags_concert[28], i_poster_small); }

        /// <summary>Sets the nme of the day for the concert</summary>
        static public void SetDayName(int i_concert, string i_week_day) { SetInnerTextForNode(i_concert, m_text_tags_concert[29], i_week_day); }


        #endregion Set concert data values in the season XML files

        #region Set musician data


        /// <summary>Sets the musician name for a given concert and musician number</summary>
        static public void SetMusicianData(int i_concert, int i_musician, string i_musician_tag, string i_musician_data)
        {
            if (m_text_tags_musician[0].Equals(i_musician_tag))
                SetMusicianName(i_concert, i_musician, i_musician_data);
            else if (m_text_tags_musician[1].Equals(i_musician_tag))
                SetMusicianInstrument(i_concert, i_musician, i_musician_data);
            else if (m_text_tags_musician[2].Equals(i_musician_tag))
                SetMusicianText(i_concert, i_musician, i_musician_data);
            else if (m_text_tags_musician[3].Equals(i_musician_tag))
                SetMusicianBirthYearStr(i_concert, i_musician, i_musician_data);
            else if (m_text_tags_musician[4].Equals(i_musician_tag))
                SetMusicianGenderStr(i_concert, i_musician, i_musician_data);

        } // SetMusicianData

        /// <summary>Returns the value (text) for gender male</summary>
        static public string GetGenderMaleValue() { return "male"; }

        /// <summary>Returns the value (text) for gender female</summary>
        static public string GetGenderFemaleValue() { return "female"; }

        /// <summary>Sets the musician name for a given concert and musician number</summary>
        static public void SetMusicianName(int i_concert, int i_musician, string i_musician_name)
        {
            SetMusicianInnerText(i_concert, i_musician, m_text_tags_musician[0], i_musician_name);
        } // SetMusicianName

        /// <summary>Sets the musician instrument for a given concert and musician number</summary>
        static public void SetMusicianInstrument(int i_concert, int i_musician, string i_musician_instrument)
        {
            SetMusicianInnerText(i_concert, i_musician, m_text_tags_musician[1], i_musician_instrument);
        } // SetMusicianInstrument

        /// <summary>Sets the musician text for a given concert and musician number</summary>
        static public void SetMusicianText(int i_concert, int i_musician, string i_musician_text)
        {
            SetMusicianInnerText(i_concert, i_musician, m_text_tags_musician[2], i_musician_text);
        } // SetMusicianText

        /// <summary>Sets the musician birth year for a given concert and musician number</summary>
        static public void SetMusicianBirthYearStr(int i_concert, int i_musician, string i_musician_birth_year)
        {
            SetMusicianInnerText(i_concert, i_musician, m_text_tags_musician[3], i_musician_birth_year);
        } // SetMusicianBirthYearStr

        /// <summary>Sets the musician gender for a given concert and musician number</summary>
        static public void SetMusicianGenderStr(int i_concert, int i_musician, string i_musician_gender)
        {
            SetMusicianInnerText(i_concert, i_musician, m_text_tags_musician[4], i_musician_gender);
        } // SetMusicianGenderStr




        #endregion // Set musician data

        #region Set title data

        /// <summary>Defines the musician title tags that can be written</summary>
        static public string[] m_title_tags =
        {
            @"TitleMusician", // 0

        }; // m_title_tags

        /// <summary>Sets title data</summary>
        static public void SetTitleData(string i_title_tag, string i_titel_data)
        {
            if (m_title_tags[0].Equals(i_title_tag))
                SetTitleMusician(i_titel_data);

        } // SetTitleData


        /// <summary>Sets the title for the musician page</summary>
        static public void SetTitleMusician(string i_title_musician)
        {
            SetInnerTextForApplicationNode(m_title_tags[0], i_title_musician);

        } // SetTitleMusician

        #endregion // Set title data

        #region Set member data

        /// <summary>Sets the member name</summary>
        static public void SetMemberName(int i_member, string i_name) { SetMemberInnerText(i_member, m_text_tags_member[0], i_name); }

        /// <summary>Sets the member family name</summary>
        static public void SetMemberFamilyName(int i_member, string i_family_name) { SetMemberInnerText(i_member, m_text_tags_member[1], i_family_name); }

        /// <summary>Sets the member email</summary>
        static public void SetMemberEmailAddress(int i_member, string i_email) { SetMemberInnerText(i_member, m_text_tags_member[2], i_email); }

        /// <summary>Sets the member telephone number</summary>
        static public void SetMemberTelephone(int i_member, string i_telephone) { SetMemberInnerText(i_member, m_text_tags_member[3], i_telephone); }

        /// <summary>Sets the member street</summary>
        static public void SetMemberStreet(int i_member, string i_street) { SetMemberInnerText(i_member, m_text_tags_member[4], i_street); }

        /// <summary>Sets the member city</summary>
        static public void SetMemberCity(int i_member, string i_city) { SetMemberInnerText(i_member, m_text_tags_member[5], i_city); }

        /// <summary>Sets the member postal code</summary>
        static public void SetMemberPostCode(int i_member, string i_post_code) { SetMemberInnerText(i_member, m_text_tags_member[6], i_post_code); }

        /// <summary>Sets the member mid size photo</summary>
        static public void SetMemberPhotoMidSize(int i_member, string i_photo_mid) { SetMemberInnerText(i_member, m_text_tags_member[7], i_photo_mid); }

        /// <summary>Sets the member small size photo</summary>
        static public void SetMemberPhotoSmallSize(int i_member, string i_photo_small) { SetMemberInnerText(i_member, m_text_tags_member[8], i_photo_small); }

        /// <summary>Sets the member tasks</summary>
        static public void SetMemberTasks(int i_member, string i_tasks) { SetMemberInnerText(i_member, m_text_tags_member[9], i_tasks); }

        /// <summary>Sets the member tasks, short description</summary>
        static public void SetMemberTasksShort(int i_member, string i_tasks_short) { SetMemberInnerText(i_member, m_text_tags_member[10], i_tasks_short); }

        /// <summary>Sets the member why</summary>
        static public void SetMemberWhy(int i_member, string i_why) { SetMemberInnerText(i_member, m_text_tags_member[11], i_why); }

        /// <summary>Sets the member start year</summary>
        static public void SetMemberStartYear(int i_member, string i_start_year) { SetMemberInnerText(i_member, m_text_tags_member[12], i_start_year); }

        /// <summary>Sets the member end year</summary>
        static public void SetMemberEndYear(int i_member, string i_end_year) { SetMemberInnerText(i_member, m_text_tags_member[13], i_end_year); }

        /// <summary>Sets the member password</summary>
        static public void SetMemberPassword(int i_member, string i_password) { SetMemberInnerText(i_member, m_text_tags_member[14], i_password); }

        /// <summary>Sets the flag telling if the member is active</summary>
        static public void SetMemberVorstand(int i_member, string i_vorstand) { SetMemberInnerText(i_member, m_text_tags_member[15], i_vorstand); }

        /// <summary>Sets the member number (identity)</summary>
        static public void SetMemberNumber(int i_member, string i_number) { SetMemberInnerText(i_member, m_text_tags_member[16], i_number); }

        /// <summary>Sets the member private email</summary>
        static public void SetMemberEmailPrivate(int i_member, string i_email_private) { SetMemberInnerText(i_member, m_text_tags_member[17], i_email_private); }

        /// <summary>Sets the member fix telephone number</summary>
        static public void SetMemberTelephoneFix(int i_member, string i_telephone_fix) { SetMemberInnerText(i_member, m_text_tags_member[18], i_telephone_fix); }

        #endregion // Set member data

        #region Get member data

        /// <summary>Returns the members as an array of strings</summary>
        public static String[] GetMembersAsStrings()
        {
            String[] ret_members = null;

            int number_members = GetNumberOfMembers();
            if (number_members <= 0)
                return ret_members; // Programming error

            ret_members = new String[number_members];

            for (int index_member = 0; index_member < number_members; index_member++)
            {
                ret_members[index_member] = GetMemberName(index_member + 1) + " " + GetMemberFamilyName(index_member + 1);
            }

            return ret_members;

        } // GetMembersAsStrings


        /// <summary>Get all (active and not active) members</summary>
        static public MemberData[] GetAllMembers()
        {
            MemberData[] ret_members = null;

            int number_members = GetNumberOfMembers();
            if (number_members <= 0)
                return ret_members; // Programming error

            ret_members = new MemberData[number_members];

            for (int index_member = 0; index_member < number_members; index_member++)
            {
                MemberData current_member = new MemberData();

                SetMember(current_member, index_member + 1);

                ret_members[index_member] = current_member;
            }

            return ret_members;

        } // GetAllMembers

        /// <summary>Returns the member number for a member that shall be added. Returns negative value for failure</summary>
        static public int GetAddMemberNumber()
        {
            int ret_number = -12345;

            MemberData[] all_members = GetAllMembers();

            int max_member_number = -10000;

            for (int index_member=0; index_member< all_members.Length; index_member++)
            {
                MemberData current_member = all_members[index_member];

                if (current_member.Number > max_member_number)
                    max_member_number = current_member.Number;
            }

            ret_number = max_member_number + 1;

            return ret_number;

        } // GetAddMemberNumber


        /// <summary>Get only active members</summary>
        static public MemberData[] GetActiveMembers()
        {
            MemberData[] ret_active_members = null;

            int number_active_members = GetNumberOfActiveMembers();
            if (number_active_members <= 0)
                return ret_active_members; // Programming error

            ret_active_members = new MemberData[number_active_members];

            MemberData[] all_members = GetAllMembers();


            int index_active_member = 0;
            for (int index_member = 0; index_member < all_members.Length; index_member++)
            {
                MemberData current_member = all_members[index_member];

                if (current_member.Vorstand)
                {
                    ret_active_members[index_active_member] = all_members[index_member];
                    index_active_member = index_active_member + 1;
                }

            } //index_member


            return ret_active_members;

        } // GetActiveMembers

        /// <summary>Set member data</summary>
        static private MemberData SetMember(MemberData i_member, int i_member_number)
        {
            if (null == i_member)
                return null;

            if (i_member_number <= 0 || i_member_number > GetNumberOfMembers())
                return i_member;

            MemberData ret_member = i_member;

            ret_member.Name = GetMemberName(i_member_number);
            ret_member.FamilyName = GetMemberFamilyName(i_member_number);
            ret_member.EmailAddress = GetMemberEmail(i_member_number);
            ret_member.PrivateEmailAddress = GetMemberEmailPrivate(i_member_number);
            ret_member.Telephone = GetMemberTelephone(i_member_number);
            ret_member.TelephoneFix = GetMemberTelephoneFix(i_member_number);
            ret_member.Street = GetMemberStreet(i_member_number);
            ret_member.City = GetMemberCity(i_member_number);
            ret_member.PostCode = GetMemberPostCode(i_member_number);
            ret_member.PhotoMidSize = GetMemberPhotoMidSize(i_member_number);
            ret_member.PhotoSmallSize = GetMemberPhotoSmallSize(i_member_number);
            ret_member.Tasks = GetMemberTasks(i_member_number);
            ret_member.TasksShort = GetMemberTasksShort(i_member_number);
            ret_member.Why = GetMemberWhy(i_member_number);
            ret_member.StartYear = GetMemberStartYearInt(i_member_number);
            ret_member.EndYear = GetMemberEndYearInt(i_member_number);
            ret_member.Vorstand = MemberIsInVorstand(i_member_number);
            ret_member.Password = GetMemberPassword(i_member_number);
            ret_member.Number = GetMemberNumber(i_member_number);

            return ret_member;

        } // SetMember

        /// <summary>Returns the member start year as int</summary>
        static public int GetMemberStartYearInt(int i_member)
        {
            int ret_start_year = -12345;

            String member_start_year_string = GetMemberStartYear(i_member);

            if (!XmlNodeValueIsSet(member_start_year_string))
                return -1;

            ret_start_year = JazzUtils.StringToInt(member_start_year_string);

            return ret_start_year;
        } // GetMemberStartYearInt

        /// <summary>Returns the member end year as int. Eq. 0: End year is not set</summary>
        static public int GetMemberEndYearInt(int i_member)
        {
            int ret_end_year = -12345;

            String member_end_year_string = GetMemberEndYear(i_member);

            if (!XmlNodeValueIsSet(member_end_year_string))
                return 0;

            ret_end_year = JazzUtils.StringToInt(member_end_year_string);

            return ret_end_year;
        } // GetMemberEndYearInt

        /// <summary>Returns the number of active members</summary>
        static public int GetNumberOfActiveMembers()
        {
            int ret_number = -12345;

            MemberData[] all_members = GetAllMembers();
            if (null == all_members)
                return ret_number;

            ret_number = 0;
            for (int index_member = 0; index_member < all_members.Length; index_member++)
            {
                MemberData current_member = all_members[index_member];

                if (current_member.Vorstand)
                    ret_number = ret_number + 1;
            }

                return ret_number;
        } // GetNumberOfActiveMembers


        /// <summary>Returns the member Vorstand flag, i.e. the flag telling if the member is active</summary>
        static public bool GetMemberActiveFlag(int i_member)
        {
            string active_str = GetMemberVorstandFlag(i_member);

            if (active_str.Equals(@"true"))
                return true;
            else
                return false;
        } // GetMemberActive

        /// <summary>Sets the member Vorstand flag, i.e. the flag telling if the member is active</summary>
        static public void SetMemberActiveFlag(int i_member, bool i_active)
        {
            if (i_active)
            {
                SetMemberVorstand(i_member, @"true");
            }
            else
            {
                SetMemberVorstand(i_member, @"false");
            }

        } // SetMemberActiveFlag

        #endregion // Get member data

        #region Add and remove XML elements

        /// <summary>Adds a musician node</summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_musician_name">Musician default name</param>
        static public void AddMusicianNode(int i_concert, string i_musician_name)
        {
            if (null == m_document_current)
                return;

            if (i_concert <= 0)
                return;

            if (i_musician_name.Trim().Length == 0)
                return;

            XElement musician_element = MusicianElementName(i_musician_name);

            if (null == musician_element)
                return;

            int current_concert_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert == current_concert_number)
                {
                    XElement musicians_element = (from el in element_concert.Descendants("Musicians")
                                                  select el).First();

                    musicians_element.Add(musician_element);

                } // i_concert == current_concert_number

            } // Loop all concert elements

        } // AddMusicianNode

        /// <summary>Adds a musicians node to the last (added) concert</summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_musician_name">Musician name</param>
        ///  <param name="o_error">Error message</param>
        static public bool  AddMusiciansNodeToLastConcert(string i_musician_name, out string o_error)
        {
            o_error = @"";

            if (null == m_document_current)
                return false;

            if (i_musician_name.Trim().Length == 0)
                return false;

            XElement musician_element = MusicianElementName(i_musician_name);

            if (null == musician_element)
                return false;

            XElement musicians_element = new XElement("Musicians", musician_element);

            if (null == musicians_element)
                return false;

            int n_concerts = GetNumberConcertsInCurrentDocument();

            int current_concert_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (n_concerts == current_concert_number)
                {
                    element_concert.Add(musicians_element);

                    return true;
                } // i_concert == current_concert_number

            } // Loop all concert elements

            o_error = @"JazzXml.AddConcertNode Programming error: Concert not found";
            return false;

        } // AddMusicianNode

        /// <summary>Adds a member node</summary>
        ///  <param name="i_member_first_name">Member default first name</param>
        ///   <param name="i_member_family_name">Member default family name</param>
        static public void AddMemberNode(string i_member_first_name, string i_member_family_name)
        {
            if (null == m_document_current)
                return;

            if (i_member_first_name.Trim().Length == 0)
                return;

            if (i_member_family_name.Trim().Length == 0)
                return;

            int add_member_number = GetAddMemberNumber();

            int current_year = JazzUtils.GetCurrentYear();

            XElement member_element = MemberElement(i_member_first_name, i_member_family_name, current_year, add_member_number);

            if (null == member_element)
                return;

            XElement appl_root = m_document_application.Root;

            XElement members_element = (from element in appl_root.Descendants("Members")
                                      select element).First();

            members_element.Add(member_element);

        } // AddMemberNode

        /// <summary>Adds a concert node</summary>
        ///  <param name="i_concert_name">Concert default name</param>
        ///  <param name="i_musician_name">Musician default name</param>
        static public bool AddConcertNode(string i_concert_name, string i_musician_name, out string o_error)
        {
            o_error = @"";

            if (null == m_document_current)
                return false;

            if (i_concert_name.Trim().Length == 0)
                return false;

            if (i_musician_name.Trim().Length == 0)
                return false;

            XElement concert_element = ConcertElement_Remove(i_concert_name, i_musician_name);


            if (null == concert_element)
            {
                o_error = @"JazzXml.AddConcertNode Programming error: Concert element is null";
                return false;
            }

            try
            {
                XElement season_program_root = m_document_current.Root;

                season_program_root.Add(concert_element);

                if (!AddMusiciansNodeToLastConcert(i_musician_name, out o_error))
                    return false;
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.AddConcertNode " + e.ToString();
                return false;
            }

            return true;

        } // AddConcertNode

        /// <summary>Removes a musician XElement. Returns error -1 if the input number of musicians is one (1)</summary>
        ///  <param name="i_concert">Concert number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_musician">Musician number 1, 2, 3, ....</param>
        static public int RemoveMusicianNode(int i_concert, int i_musician)
        {
            if (null == m_document_current)
                return -10;

            if (i_concert <= 0)
                return -11;

            int number_musicians = JazzXml.GetNumberMusicians(i_concert);
            if (number_musicians <= 1)
                return -1;

            if (i_musician <= 0 || i_musician > number_musicians)
                return -12;


            int current_concert_number = 0;
            int current_musician_number = 0;
            bool b_removed = false;
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
                            element_musician.Remove();
                            b_removed = true;
                            break;

                        } // Musician exists

                    } // element_musician
                } // Concert exists
                if (b_removed)
                    break;
            } // element_concert

            if (b_removed)
                return 0;
            else
                return -20;

        } // RemoveMusicianNode


        /// <summary>Removes a concert XElement. Returns error -1 if the input number of concerts is one (1)</summary>
        ///  <param name="i_concert">Concert number 1, 2, 3, 4, 5, ....</param>
        static public int RemoveConcertNode(int i_concert)
        {
            if (null == m_document_current)
                return -10;

            if (i_concert <= 0)
                return -11;

            int n_concerts = GetNumberConcertsInCurrentDocument();
            if (n_concerts <= 1)
                return -1;

            int current_concert_number = 0;
            foreach (XElement element_concert in m_document_current.Descendants("Concert"))
            {
                current_concert_number = current_concert_number + 1;
                if (i_concert == current_concert_number)
                {
                    element_concert.Remove();

                    return 0;
                } // i_concert == current_concert_number

            } // Loop all concert elements

            // No concert was removed
            return -20;

        } // RemoveConcertNode

        /// <summary>Removes a member XElement. Returns error -1 if the input number of members is one (1)</summary>
        ///  <param name="i_member">Member number 1, 2, 3, 4, 5, ....</param>
        static public int RemoveMemberNode(int i_member)
        {
            if (null == m_document_application)
                return -10;

            if (i_member <= 0)
                return -11;

            int number_members = GetNumberOfMembers();
            if (number_members <= 1)
                return -1;

            int current_member_number = 0;
            foreach (XElement element_member in m_document_application.Descendants("Member"))
            {
                current_member_number = current_member_number + 1;
                if (i_member == current_member_number)
                {
                    element_member.Remove();

                    return 0;
                }
            }

            // A member was not removed
            return -20;

        } // RemoveMemberNode

        #endregion // Add and remove XML elements

        #region Create XML elements

        /// <summary>Creates a jazz season XML object (XDocument) 
        /// <para>The season program XElement must first be created. 
        /// Thereafter the XDocument can be created with this XElement, i.e. the whole XML tree.</para>
        /// <para>1. Create the (start) season program XML element. Call of GetTagSeason</para>
        /// <para>2. Add XML elements YearAutum, YearSpring and PublishProgram to the season program XML element. 
        /// Calls of GetTagSeasonYearAutum, GetTagSeasonYearSpring, GetTagSeasonPublishProgram and XElement.Add</para>
        /// <para>3. Loop for all concerts. Call of JazzSeason.GetNumberOfConcerts.</para>
        /// <para>3.1 Get JazzConcert object. Call of JazzSeason.GetJazzConcert.</para>
        /// <para>3.2 Create the JazzConcert XElement. Call of JazzXml.ConcertElement.</para>
        /// <para>3.3 Add comment XML element 'Start concert' (in order to make the XML text file better readable). 
        /// Call of XComment and XElement.Add</para>
        /// <para>3.4 Add XML element concert. Call of XElement.Add.</para>
        /// <para>3.5 Add comment XML element 'End concert' (in order to make the XML text file better readable). 
        /// Call of XComment and XElement.Add</para>
        /// <para>4. Create the output season program XDocument. Calls of XDeclaration and XComment</para>
        /// </summary>
        /// <param name="i_jazz_season">JazzConcert object. Empty parameter strings must have value NotYetSetNodeValue</param>
        public static XDocument SeasonProgramXml(JazzSeason i_jazz_season)
        {
            XElement season_xml_element = null;

            season_xml_element = new XElement(
            new XElement(GetTagSeason()));

            XElement year_autumn_el = new XElement(
                GetTagSeasonYearAutum(),
                i_jazz_season.YearAutumn);

            XElement year_spring_el = new XElement(
                GetTagSeasonYearSpring(),
                i_jazz_season.YearSpring);

            XElement publish_program_el = new XElement(
                GetTagSeasonPublishProgram(),
                i_jazz_season.PublishProgram);

            season_xml_element.Add(year_autumn_el);

            season_xml_element.Add(year_spring_el);

            season_xml_element.Add(publish_program_el);

            int n_concerts = i_jazz_season.GetNumberOfConcerts();

            for (int concert_number = 1; concert_number <= n_concerts; concert_number++)
            {
                string error_msg = @"";

                JazzConcert jazz_concert = i_jazz_season.GetJazzConcert(concert_number, out error_msg);

                if (null == jazz_concert)
                {
                    return null;
                }

                XElement concert_el = ConcertElement(jazz_concert);

                if (null == concert_el)
                {
                    return null;
                }

                string start_comment_str = @"  Konzert " + concert_number.ToString() + @" Start  ";

                string end_comment_str = @"  Konzert " + concert_number.ToString() + @" End  ";

                season_xml_element.Add(new XComment(start_comment_str));

                season_xml_element.Add(concert_el);

                season_xml_element.Add(new XComment(end_comment_str));

            } // concert_number

            XDocument ret_object = new XDocument(
             new XDeclaration("1.0", "utf-8", "yes"),
             new XComment(i_jazz_season.SeasonComment),
             season_xml_element);
             
            return ret_object;

        } // SeasonProgramXml

        /// <summary>Creates a concert XElement (a sub-tree)
        /// <para>The returned XElement will be added to an existing XDocument</para>
        /// </summary>
        /// <param name="i_jazz_concert">JazzConcert object. Empty parameter strings must have value NotYetSetNodeValue</param>
        public static XElement ConcertElement(JazzConcert i_jazz_concert)
        {
           
            XElement musicians_element = MusicianElements(i_jazz_concert);

            XElement ret_element = new XElement(GetTagConcert(),
                                   new XElement(GetTagConcertDayName(), i_jazz_concert.ConcertDayName),
                                   new XElement(GetTagConcertYear(), i_jazz_concert.ConcertYear),
                                   new XElement(GetTagConcertMonth(), i_jazz_concert.ConcertMonth),
                                   new XElement(GetTagConcertDay(), i_jazz_concert.ConcertDay),
                                   new XElement(GetTagConcertTimeStartHour(), i_jazz_concert.ConcertTimeStartHour),
                                   new XElement(GetTagConcertTimeStartMinute(), i_jazz_concert.ConcertTimeStartMinute),
                                   new XElement(GetTagConcertTimeEndHour(), i_jazz_concert.ConcertTimeEndHour),
                                   new XElement(GetTagConcertTimeEndMinute(), i_jazz_concert.ConcertTimeEndMinute),
                                   new XElement(GetTagConcertPlace(), i_jazz_concert.ConcertPlace),
                                   new XElement(GetTagConcertStreet(), i_jazz_concert.ConcertStreet),
                                   new XElement(GetTagConcertCity(), i_jazz_concert.ConcertCity),
                                   new XElement(GetTagConcertCancelled(), i_jazz_concert.ConcertCancelled),
                                   new XElement(GetTagConcertBandName(), i_jazz_concert.ConcertBandName),
                                   new XElement(GetTagConcertShortText(), i_jazz_concert.ConcertShortText),
                                   new XElement(GetTagConcertAdditionalText(), i_jazz_concert.ConcertAdditionalText),
                                   new XElement(GetTagConcertLabelAdditionalText(), i_jazz_concert.ConcertLabelAdditionalText),
                                   new XElement(GetTagFlyerTextHomepagePublish(), i_jazz_concert.FlyerTextHomepagePublish),
                                   new XElement(GetTagConcertLabelFlyerText(), i_jazz_concert.ConcertLabelFlyerText),
                                   new XElement(GetTagConcertFlyerText(), i_jazz_concert.ConcertFlyerText),
                                   new XElement(GetTagConcertPosterBigSize(), i_jazz_concert.ConcertPosterBigSize),
                                   new XElement(GetTagConcertPosterMidSize(), i_jazz_concert.ConcertPosterMidSize),
                                   new XElement(GetTagConcertPosterSmallSize(), i_jazz_concert.ConcertPosterSmallSize),
                                   new XElement(GetTagConcertSoundSample(), i_jazz_concert.ConcertSoundSample),
                                   new XElement(GetTagConcertBandWebsite(), i_jazz_concert.ConcertBandWebsite),
                                   new XElement(GetTagConcertSoundSampleQrCode(), i_jazz_concert.ConcertSoundSampleQrCode),
                                   new XElement(GetTagConcertBandWebsiteQrCode(), i_jazz_concert.ConcertBandWebsiteQrCode),
                                   new XElement(GetTagConcertPhotoGalleryOne(), i_jazz_concert.ConcertPhotoGalleryOne),
                                   new XElement(GetTagConcertPhotoGalleryTwo(), i_jazz_concert.ConcertPhotoGalleryTwo),
                                   new XElement(GetTagConcertPhotoGalleryOneZip(), i_jazz_concert.ConcertPhotoGalleryOneZip),
                                   new XElement(GetTagConcertPhotoGalleryTwoZip(), i_jazz_concert.ConcertPhotoGalleryTwoZip),
                                   new XElement(GetTagConcertContactPerson(), i_jazz_concert.ConcertContactPerson),
                                   new XElement(GetTagConcertContactEmail(), i_jazz_concert.ConcertContactEmail),
                                   new XElement(GetTagConcertContactTelephone(), i_jazz_concert.ConcertContactTelephone),
                                   new XElement(GetTagConcertContactStreet(), i_jazz_concert.ConcertContactStreet),
                                   new XElement(GetTagConcertContactPostCode(), i_jazz_concert.ConcertContactPostCode),
                                   new XElement(GetTagIbanNumber(), i_jazz_concert.IbanNumber),
                                   new XElement(GetTagContactRemark(), i_jazz_concert.ContactRemark),
                                   new XElement(GetTagConcertContactCity(), i_jazz_concert.ConcertContactCity),
                                   new XElement(GetTagConcertLoginPassword(), i_jazz_concert.ConcertLoginPassword),
                                   musicians_element);
           

            return ret_element;

        } // ConcertElement

        /// <summary>Creates a concert XElement (a sub-tree)
        /// <para>The returned XElement will be added to an existing XDocument</para>
        /// </summary>
        /// <param name="i_concert_name">Concert default name</param>
        /// <param name="i_musician_name">Musician default name</param>
        public static XElement ConcertElement_Remove(string i_concert_name, string i_musician_name)
        {
            if (i_concert_name.Trim().Length == 0)
                return null;

            if (i_musician_name.Trim().Length == 0)
                return null;

            XElement musician_element = MusicianElementName(i_musician_name);

            XElement ret_element = new XElement(GetTagConcert(),
                                   new XElement(GetTagConcertDayName(), "Samstag"),
                                   new XElement(GetTagConcertYear(), "2020"),
                                   new XElement(GetTagConcertMonth(), "10"),
                                   new XElement(GetTagConcertDay(), "14"),
                                   new XElement(GetTagConcertTimeStartHour(), "15"),
                                   new XElement(GetTagConcertTimeStartMinute(), "30"),
                                   new XElement(GetTagConcertTimeEndHour(), "18"),
                                   new XElement(GetTagConcertTimeEndMinute(), "15"),
                                   new XElement(GetTagConcertPlace(), m_undefined_node_value),
                                   new XElement(GetTagConcertStreet(), m_undefined_node_value),
                                   new XElement(GetTagConcertCity(), m_undefined_node_value),
                                   new XElement(GetTagConcertCancelled(), "FALSE"),
                                   new XElement(GetTagConcertBandName(), i_concert_name),
                                   new XElement(GetTagConcertShortText(), m_undefined_node_value),
                                   new XElement(GetTagConcertAdditionalText(), m_undefined_node_value),
                                   new XElement(GetTagConcertLabelAdditionalText(), m_undefined_node_value),
                                   new XElement(GetTagFlyerTextHomepagePublish(), "TRUE"),
                                   new XElement(GetTagConcertLabelFlyerText(), m_undefined_node_value),
                                   new XElement(GetTagConcertFlyerText(), m_undefined_node_value),
                                   new XElement(GetTagConcertPosterBigSize(), m_undefined_node_value),
                                   new XElement(GetTagConcertPosterMidSize(), m_undefined_node_value),
                                   new XElement(GetTagConcertPosterSmallSize(), m_undefined_node_value),
                                   new XElement(GetTagConcertSoundSample(), m_undefined_node_value),
                                   new XElement(GetTagConcertBandWebsite(), m_undefined_node_value),
                                   new XElement(GetTagConcertSoundSampleQrCode(), m_undefined_node_value),
                                   new XElement(GetTagConcertBandWebsiteQrCode(), m_undefined_node_value),
                                   new XElement(GetTagConcertPhotoGalleryOne(), m_undefined_node_value),
                                   new XElement(GetTagConcertPhotoGalleryTwo(), m_undefined_node_value),
                                   new XElement(GetTagConcertPhotoGalleryOneZip(), m_undefined_node_value),
                                   new XElement(GetTagConcertPhotoGalleryTwoZip(), m_undefined_node_value),
                                   new XElement(GetTagConcertContactPerson(), m_undefined_node_value),
                                   new XElement(GetTagConcertContactEmail(), m_undefined_node_value),
                                   new XElement(GetTagConcertContactTelephone(), m_undefined_node_value),
                                   new XElement(GetTagConcertContactStreet(), m_undefined_node_value),
                                   new XElement(GetTagConcertContactPostCode(), m_undefined_node_value),
                                   new XElement(GetTagIbanNumber(), m_undefined_node_value),
                                   new XElement(GetTagContactRemark(), m_undefined_node_value),
                                   new XElement(GetTagConcertContactCity(), m_undefined_node_value),
                                   new XElement(GetTagConcertLoginPassword(), m_undefined_node_value),
                                   musician_element);


            return ret_element;

        } // ConcertElement_Remove

        /// <summary>Returns a musicians XElement, i.e. the XML sub-tree 'Musicians'.
        /// <para>The returned XElement can be added to an existing XElement or XDocument object</para>
        /// </summary>
        /// <param name="i_jazz_concert">JazzConcert object. Empty parameter strings must have value NotYetSetNodeValue</param>
        public static XElement MusicianElements(JazzConcert i_jazz_concert)
        {
            XElement ret_element = new XElement(GetTagMusicians());

            int n_musicians = i_jazz_concert.GetNumberOfMusicians();

            string error_msg = @"";

            for (int musician_number = 1; musician_number <= n_musicians; musician_number++)
            {
                JazzMusician jazz_musician = i_jazz_concert.GetJazzMusician(musician_number, out error_msg);

                if (null == jazz_musician)
                {
                    return null;
                }

                string musician_name = jazz_musician.MusicianName; // TODO Temporary
                XElement musician_el = MusicianElementName(musician_name); // TODO Temporary

                ret_element.Add(musician_el);

            }

            return ret_element;

        } // MusicianElements

        /// <summary>Returns a musician XElement, i.e. the XML sub-tree 'Musician'.
        /// </summary>
        /// <param name="i_jazz_musician">JazzMusician object. Empty parameter strings must have the value NotYetSetNodeValue</param>
        /// <returns>Musician XML element</returns>
        public static XElement MusicianElement(JazzMusician i_jazz_musician)
        {

            XElement ret_element = new XElement(GetTagMusician(),
                                   new XElement(GetTagMusicianName(), i_jazz_musician.MusicianName),
                                   new XElement(GetTagMusicianInstrument(), i_jazz_musician.MusicianInstrument),
                                   new XElement(GetTagMusicianText(), i_jazz_musician.MusicianText),
                                   new XElement(GetTagMusicianBirthYear(), i_jazz_musician.MusicianBirthYear),
                                   new XElement(GetTagMusicianGender(), i_jazz_musician.MusicianGender));

            return ret_element;

        } // MusicianElement

        /// <summary>Returns a musician XElement, i.e. the XML sub-tree 'Musician'.
        /// <para>Input is the musician name. All other values (except gender) will be set to NotYetSetNodeValue</para>
        /// <para>1. Create a JazzMusician object and set MusicianName. The constructor sets MusicianGender to 'male'</para>
        /// <para>2. Set empty member variable strings to NotYetSetNodeValue. Call of JazzMusician.SetToValuesNotYetSetForEmptyStrings</para>
        /// <para>2. Create a JazzMusician XML element. Call of JazzXml.MusicianElement</para>
        /// </summary>
        /// <param name="i_musician_name">Musician default name</param>
        /// <returns>Musician XML element</returns>
        public static XElement MusicianElementName(string i_musician_name)
        {
            if (i_musician_name.Trim().Length == 0)
                return null;

            JazzMusician jazz_musician = new JazzMusician();

            jazz_musician.MusicianName = i_musician_name;

            jazz_musician.SetToValuesNotYetSetForEmptyStrings();

            XElement ret_element = MusicianElement(jazz_musician);

            return ret_element;

        } // MusicianElementName

        /// <summary>Creates a member XElement (a sub-tree)
        /// <para>The returned XElement can be added to an existing XDocument or XElement</para>
        /// </summary>
        /// <param name="i_member_first_name">Member default first name</param>
        /// <param name="i_member_family_name">Member default family name</param>
        /// <param name="i_current_year">Current year</param>
        /// <param name="i_add_member_number">Member number</param>
        public static XElement MemberElement(string i_member_first_name, string i_member_family_name, int i_current_year, int i_add_member_number)
        {
            if (i_member_first_name.Trim().Length == 0)
                return null;

            if (i_member_family_name.Trim().Length == 0)
                return null;

            if (i_current_year < 2017)
                return null;

            if (i_add_member_number <= 0)
                return null;

            XElement ret_element = new XElement("Member",
                                   new XElement(m_text_tags_member[0], i_member_first_name),
                                   new XElement(m_text_tags_member[1], i_member_family_name),
                                   new XElement(m_text_tags_member[2], m_undefined_node_value),
                                   new XElement(m_text_tags_member[17], m_undefined_node_value), // Private E-Mail
                                   new XElement(m_text_tags_member[3], m_undefined_node_value),
                                   new XElement(m_text_tags_member[18], m_undefined_node_value), // Telephone Fix
                                   new XElement(m_text_tags_member[4], m_undefined_node_value),
                                   new XElement(m_text_tags_member[5], m_undefined_node_value),
                                   new XElement(m_text_tags_member[6], m_undefined_node_value),
                                   new XElement(m_text_tags_member[7], m_undefined_node_value),
                                   new XElement(m_text_tags_member[8], m_undefined_node_value),
                                   new XElement(m_text_tags_member[9], m_undefined_node_value),
                                   new XElement(m_text_tags_member[10], m_undefined_node_value),
                                   new XElement(m_text_tags_member[11], m_undefined_node_value),
                                   new XElement(m_text_tags_member[12], i_current_year.ToString()),
                                   new XElement(m_text_tags_member[13], m_undefined_node_value),
                                   new XElement(m_text_tags_member[14], m_undefined_node_value),
                                   new XElement(m_text_tags_member[15], "true"),
                                   new XElement(m_text_tags_member[16], i_add_member_number.ToString()));

            return ret_element;

        } // MemberElement

        #endregion // Create XML elements

        #region XML utility functions

        /// <summary>Returns the musicians as an array of strings</summary>
        public static String[] GetMusiciansAsStrings(int i_concert)
        {
            String[] ret_musicians = null;
            if (i_concert<=0)
                return ret_musicians; // Programming error

            int number_musicians = GetNumberMusicians(i_concert);
            if (number_musicians <= 0)
                return ret_musicians; // Programming error

            ret_musicians = new String[number_musicians];

            for (int index_musician = 0; index_musician < number_musicians; index_musician++)
            {
                ret_musicians[index_musician] = JazzXml.GetMusicianName(i_concert, index_musician + 1);
            }

            return ret_musicians;

        } // GetMusiciansAsStrings

        /// <summary>Returns the instruments as an array of strings</summary>
        public static String[] GetInstrumentsAsStrings(int i_concert)
        {
            String[] ret_instruments = null;
            if (i_concert <= 0)
                return ret_instruments; // Programming error

            int number_musicians = GetNumberMusicians(i_concert);
            if (number_musicians <= 0)
                return ret_instruments; // Programming error

            ret_instruments = new String[number_musicians];

            for (int index_musician = 0; index_musician < number_musicians; index_musician++)
            {
                ret_instruments[index_musician] = JazzXml.GetMusicianInstrument(i_concert, index_musician + 1);
            }

            return ret_instruments;

        } // GetInstrumentsAsStrings

        /// <summary>Returns a string that is the the current season name.</summary>
        public static String GetSeasonName()
        {
            // TODO Should perhaps check that there is a non-corrupt XML 
            // on the server corresponding to the returned season name
            return JazzUtils.SeasonName(JazzUtils.GetCurrentSeasonStartYear());
        } // GetSeasonName

        /// <summary>Returns a string that is the next (after the current) season name.</summary>
        public static String GetNextSeasonName()
        {
            // TODO Should perhaps check that there is a non-corrupt XML 
            // on the server corresponding to the returned season name
            return JazzUtils.SeasonName(JazzUtils.GetCurrentSeasonStartYear() + 1);
        } // GetNextSeasonName

        /// <summary>Returns a string that is the last (added) season name.
        /// <para></para>
        /// </summary>
        public static String GetLastSeasonName()
        {
            // TODO Should perhaps check that there is a non-corrupt XML 
            // on the server corresponding to the returned season name

            // Used if JazzUtils.GetSeasonStartYearsForExistingXmlFiles. TODO Should be an error
            string def_season_name = JazzUtils.SeasonName(JazzUtils.GetCurrentSeasonStartYear() + 1);

            int[] start_years = JazzUtils.GetSeasonStartYearsForExistingXmlFiles();
            if (null != start_years)
            {
                int index_last_year = start_years.Length - 1;

                def_season_name = JazzUtils.SeasonName(start_years[index_last_year]);
            }


            return def_season_name;

        } // GetLastSeasonName


        /// <summary>Sets the XML object and corresponding XML file defined by the input season name
        /// <para>1. Get the index for the input season name in array m_season_names_documents_strings. Call of GetIndexForSeasonName.</para>
        /// <para>2. Get the corresponding elements in arrays m_xdocument_documents and m_xdocument_doc_file_names.</para>
        /// <para>3. Set active document object. Call of JazzXml.SetObjectActiveDoc</para>
        /// <para>4. Set active XML file name. Call of JazzXml.SetFileNameActiveObject</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs (m_xdocument_documents), GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs (m_season_names_documents_strings) correspond to each other</para>
        /// </summary>
        /// <param name="i_season_name">Season name that is one of the names in array m_season_names_documents_strings</param>
        /// <param name="o_error">Error message.</param>
        public static bool SetActiveXmlObjectAndFile(string i_season_name, out string o_error)
        {
            o_error = @"";

            string error_message = @"";

            int index_season_name = GetIndexForSeasonName(i_season_name, out error_message);
            if (index_season_name < 0)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFile GetIndexForSeasonName failed:  " + error_message;
                return false;
            }

            if (null == m_xdocument_documents)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFile m_xdocument_documents is null ";
                return false;
            }

            if (null == m_xdocument_doc_file_names)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFile m_xdocument_doc_file_names is null ";
                return false;
            }

            if (m_xdocument_doc_file_names.Length != m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFile m_xdocument_doc_file_names.Length != m_xdocument_documents.Length ";
                return false;
            }


            if (m_xdocument_doc_file_names.Length != m_season_names_documents_strings.Length)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFile m_xdocument_doc_file_names.Length != m_season_names_documents_strings.Length ";
                return false;
            }

            XDocument xml_object_season = m_xdocument_documents[index_season_name];

            string xml_file_name = m_xdocument_doc_file_names[index_season_name];

            JazzXml.SetObjectActiveDoc(xml_object_season); 

            JazzXml.SetFileNameActiveObject(xml_file_name);

            string xml_season_name = m_season_names_documents_strings[index_season_name];

            JazzXml.SetSeasonNameActiveObject(xml_season_name);

            return true;

        } // SetActiveXmlObjectAndFile


        /// <summary>Returns the index for the input name in the array m_season_names_documents_strings
        /// <para>Returns negative value if name is missing in the array (-1) or if raay is null or has no elements (-2)</para>
        /// <para>2. Get XML file name for this season. Get the file name from array m_xdocument_doc_file_names for the index retrieved with GetXmlObjectForThisSeason</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs, GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs (m_season_names_documents_strings) correspond to each other</para>
        /// </summary>
        /// <param name="i_season_name">Season name that is one of the names in array m_season_names_documents_strings</param>
        /// <param name="o_error">Error message.</param>
        private static int GetIndexForSeasonName(string i_season_name, out string o_error)
        {
            o_error = @"";
            int ret_index_season_name = -12345;

            if (null == m_season_names_documents_strings || m_season_names_documents_strings.Length == 0)
            {
                o_error = @"JazzXml.GetIndexForSeasonName m_season_names_documents_strings is null or has no elements";
                return -2;
            }

            for (int index_season_name=0; index_season_name< m_season_names_documents_strings.Length; index_season_name++)
            {
                string season_name = m_season_names_documents_strings[index_season_name];
                if (season_name.Equals(i_season_name))
                {
                    ret_index_season_name = index_season_name;
                    return ret_index_season_name;
                }
            }

            o_error = @"JazzXml.GetIndexForSeasonName In array m_season_names_documents_strings there is no i_season_name= " + i_season_name;
            return -1;

        } // GetIndexForSeasonName

        /// <summary>Sets the XML object and corresponding XML file for the input season as the active XML object and XML file 
        /// <para>1. Get the XDocument object and index in array GetObjectAllDocs (m_xdocument_documents) for this season. Call of GetXmlObjectForInputSeason.</para>
        /// <para>2. Get XML file name for this season. Get the file name from array m_xdocument_doc_file_names for the index retrieved with GetXmlObjectForThisSeason</para>
        /// <para>3. Set active document object. Call of JazzXml.SetObjectActiveDoc</para>
        /// <para>4. Set active XML file name. Call of JazzXml.SetFileNameActiveObject</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs, GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs (m_season_names_documents_strings) correspond to each other</para>
        /// </summary>
        /// <param name="o_error">Error message.</param>
        public static bool SetActiveXmlObjectAndFileToInputSeason(int i_season_start_year, out string o_error)
        {
            o_error = @"";

            string error_message = @"";

            int index_this_season = -12345;
            XDocument xml_object_this_season = GetXmlObjectForInputSeason(i_season_start_year, out index_this_season, out error_message);
            if (null == xml_object_this_season || index_this_season < 0)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToInputSeason GetXmlObjectForThisSeason failed  " + error_message;
                return false;
            }

            if (null == m_xdocument_doc_file_names)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToInputSeason m_xdocument_doc_file_names is null ";
                return false;
            }

            if (m_xdocument_doc_file_names.Length != m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToThisYearSeason m_xdocument_doc_file_names.Length (" + m_xdocument_doc_file_names.Length.ToString() + ") != m_xdocument_documents.Length (" +
                   m_xdocument_documents.Length.ToString() + @")";
                return false;
            }

            string xml_file_name = m_xdocument_doc_file_names[index_this_season];

            JazzXml.SetObjectActiveDoc(xml_object_this_season);

            JazzXml.SetFileNameActiveObject(xml_file_name);

            string xml_season_name = m_season_names_documents_strings[index_this_season];

            JazzXml.SetSeasonNameActiveObject(xml_season_name);

            return true;

        } // SetActiveXmlObjectAndFileToInputSeason

        /// <summary>Sets the XML object and corresponding XML file for this season as the active XML object and XML file 
        /// <para>1. Get the XDocument object and index in array GetObjectAllDocs (m_xdocument_documents) for this season. Call of GetXmlObjectForThisSeason.</para>
        /// <para>2. Get XML file name for this season. Get the file name from array m_xdocument_doc_file_names for the index retrieved with GetXmlObjectForThisSeason</para>
        /// <para>3. Set active document object. Call of JazzXml.SetObjectActiveDoc</para>
        /// <para>4. Set active XML file name. Call of JazzXml.SetFileNameActiveObject</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs, GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs (m_season_names_documents_strings) correspond to each other</para>
        /// </summary>
        /// <param name="o_error">Error message.</param>
        public static bool SetActiveXmlObjectAndFileToThisSeason(out string o_error)
        {
            o_error = @"";

            string error_message = @"";

            int index_this_season = -12345;
            XDocument xml_object_this_season = GetXmlObjectForThisSeason(out index_this_season, out error_message);
            if (null == xml_object_this_season || index_this_season < 0)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToThisSeason GetXmlObjectForThisSeason failed  " + error_message;
                return false;
            }

            if (null == m_xdocument_doc_file_names)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToThisSeason m_xdocument_doc_file_names is null ";
                return false;
            }

            if (m_xdocument_doc_file_names.Length != m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToThisYearSeason m_xdocument_doc_file_names.Length (" + m_xdocument_doc_file_names.Length.ToString() + ") != m_xdocument_documents.Length (" +
                   m_xdocument_documents.Length.ToString() + @")";
                return false;
            }

            string xml_file_name = m_xdocument_doc_file_names[index_this_season];

            JazzXml.SetObjectActiveDoc(xml_object_this_season);

            JazzXml.SetFileNameActiveObject(xml_file_name);

            string xml_season_name = m_season_names_documents_strings[index_this_season];

            JazzXml.SetSeasonNameActiveObject(xml_season_name);

            return true;

        } // SetActiveXmlObjectAndFileToThisSeason

        /// <summary>Sets the XML object and corresponding XML file for this season as the active XML object and XML file 
        /// <para>1. Get the XDocument object and index in array GetObjectAllDocs (m_xdocument_documents) for this season. Call of GetXmlObjectForThisSeason.</para>
        /// <para>2. Get XML file name for this season. Get the file name from array m_xdocument_doc_file_names for the index retrieved with GetXmlObjectForThisSeason</para>
        /// <para>3. Set active document object. Call of JazzXml.SetObjectActiveDoc</para>
        /// <para>4. Set active XML file name. Call of JazzXml.SetFileNameActiveObject</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs, GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs (m_season_names_documents_strings) correspond to each other</para>
        /// </summary>
        /// <param name="o_error">Error message.</param>
        public static bool SetActiveXmlObjectAndFileToNextSeason(out string o_error)
        {
            o_error = @"";

            string error_message = @"";

            int index_next_season = -12345;
            XDocument xml_object_next_season = GetXmlObjectForNextSeason(out index_next_season, out error_message);
            if (null == xml_object_next_season || index_next_season < 0)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToNextSeason GetXmlObjectForNextSeason failed  " + error_message;
                return false;
            }

            if (null == m_xdocument_doc_file_names)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToNextSeason m_xdocument_doc_file_names is null ";
                return false;
            }

            if (m_xdocument_doc_file_names.Length != m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.SetActiveXmlObjectAndFileToNextSeason m_xdocument_doc_file_names.Length != m_xdocument_documents.Length ";
                return false;
            }

            string xml_file_name = m_xdocument_doc_file_names[index_next_season];

            JazzXml.SetObjectActiveDoc(xml_object_next_season);

            JazzXml.SetFileNameActiveObject(xml_file_name);

            string xml_season_name = m_season_names_documents_strings[index_next_season];

            JazzXml.SetSeasonNameActiveObject(xml_season_name);

            return true;

        } // SetActiveXmlObjectAndFileToNextSeason

        /// <summary>Returns the XDocument for the input season start year
        /// <para>Get season index for the input season start year. Call of GetDocumentSeasonIndex</para>
        /// <para>Get output XML from the array m_xdocument_documents for the season index</para>
        /// </summary>
        /// <param name="i_season_start_year">Start year for the season</param>
        /// <param name="o_index_input_season">Season index corresponding to i_season_start_year</param>
        /// <param name="o_error">Error message when the function fails and returns null for the XML object</param>
        public static XDocument GetXmlObjectForInputSeason(int i_season_start_year, out int o_index_input_season, out string o_error)
        {
            XDocument ret_document = null;

            o_index_input_season = -12345;

            o_error = @"";

            if (i_season_start_year < 2015)
            {
                o_error = @"JazzXml.GetXmlObjectForInputSeason Season start year < 2015 not OK";

                return ret_document;
            }

            int season_status = -12345;
            o_index_input_season = GetDocumentSeasonIndex(i_season_start_year, out season_status);
            if (o_index_input_season < 0)
            {
                o_error = @"JazzXml.GetXmlObjectForInputSeason index_input_season= " + o_index_input_season.ToString();

                return ret_document;
            }
            if (season_status < 0)
            {
                o_error = @"JazzXml.GetXmlObjectForInputSeason season_status= " + season_status.ToString();

                return ret_document;
            }

            if (o_index_input_season >= m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.GetXmlObjectForInputSeason index_input_season >= m_xdocument_documents.Length= " + m_xdocument_documents.Length.ToString();
                
                return ret_document;
            }

            ret_document = m_xdocument_documents[o_index_input_season];

            return ret_document;

        } // GetXmlObjectForInputSeason

        /// <summary>Returns the XDocument for this season. 
        /// <para>Function GetIndexForThisYearSeason returns the index in array GetObjectAllDocs (m_xdocument_documents) for this season</para>
        /// <para>Returns null if the XDocument is corrupt or if GetIndexForThisSeason failed for other reasons.</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs (m_xdocument_documents), GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs correspond to each other</para>
        /// </summary>
        /// <param name="o_index_this_season">Index in array m_xdocument_documents for this season</param>
        /// <param name="o_error">Error message when the function fails and returns null for the XML object</param>
        public static XDocument GetXmlObjectForThisSeason(out int o_index_this_season, out string o_error)
        {
            XDocument ret_document = null;
            o_error = @"";

            o_index_this_season = GetIndexForThisSeason(out o_error);
            if (o_index_this_season < 0)
            {
                o_error = @"" + o_error;
                return ret_document;
            }

            if (o_index_this_season >= m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.GetXmlObjectForThisSeason index_this_year_season >= m_xdocument_documents.Length= " + m_xdocument_documents.Length.ToString();
                return ret_document;
            }

            ret_document = m_xdocument_documents[o_index_this_season];

            return ret_document;

        } // GetXmlObjectForThisSeason


        /// <summary>Returns the XDocument for the next season. 
        /// <para>Function GetIndexForNextYearSeason returns the index in array GetObjectAllDocs (m_xdocument_documents) for the next season</para>
        /// <para>Returns null if the XDocument is corrupt or if GetIndexForNextSeason failed for other reasons.</para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs (m_xdocument_documents), GetFileNamesAllDocs (m_xdocument_doc_file_names) and GetSeasonNamesAllDocs correspond to each other</para>
        /// </summary>
        /// <param name="o_index_next_season">Index in array m_xdocument_documents for the next season</param>
        /// <param name="o_error">Error message when the function fails and returns null for the XML object</param>
        public static XDocument GetXmlObjectForNextSeason(out int o_index_next_season, out string o_error)
        {
            XDocument ret_document = null;
            o_error = @"";

            o_index_next_season = GetIndexForNextSeason(out o_error);
            if (o_index_next_season < 0)
            {
                o_error = @"" + o_error;
                return ret_document;
            }

            if (o_index_next_season >= m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.GetXmlObjectForNextSeason index_this_year_season >= m_xdocument_documents.Length= " + m_xdocument_documents.Length.ToString();
                return ret_document;
            }

            ret_document = m_xdocument_documents[o_index_next_season];

            return ret_document;

        } // GetXmlObjectForNextSeason

        /// <summary>Returns true if the XDocument (JazzDokumente_20xx_20yy.xml) for the next season exists. 
        /// <para></para>
        /// </summary>
        public static bool ExistsXmlObjectForNextSeason()
        {
            int index_next_season = -12345;
            string error_message = @"";

            XDocument xml_next_season = GetXmlObjectForNextSeason(out index_next_season, out error_message);
            if (null == xml_next_season)
            {
                return false;
            }
            else
            {
                return true;
            }

        } // ExistsXmlObjectForNextSeason

        /// <summary>Returns the index in array GetObjectAllDocs (m_xdocument_documents) for this season 
        /// <para>This season is defined by function JazzXml.GetPublishSeasonStartYearInt()</para>
        /// <para>Function  JazzXml.GetSeasonIndex returns the index for this season</para>
        /// <para>The function returns a negative index value if the corresponding XDocument in m_xdocument_documents is corrupt.</para>
        /// <para></para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs (m_xdocument_documents), GetFileNamesAllDocs and GetSeasonNamesAllDocs correspond to each other</para>
        /// </summary>
        /// <param name="o_error">Error message when function has failed and returns a negative value</param>
        public static int GetIndexForThisSeason(out string o_error)
        {
            int ret_index_this_season = -12345;
            o_error = @"";

            int this_year_season = JazzXml.GetPublishSeasonStartYearInt();
            if (this_year_season < 2015)
            {
                o_error = @"JazzXml.GetIndexForThisSeason Publish season start year not OK";
                return ret_index_this_season;
            }

            if (null == m_xdocument_documents)
            {
                o_error = @"JazzXml.GetIndexForThisSeason m_xdocument_documents == null";
                return ret_index_this_season;
            }

            int season_status = -12345;
            int index_this_season = GetDocumentSeasonIndex(this_year_season, out season_status);
            if (index_this_season < 0)
            {
                o_error = @"JazzXml.GetDocumentSeasonIndex index_this_year_season= " + index_this_season.ToString();
                return ret_index_this_season;
            }
            if (season_status < 0)
            {
                o_error = @"JazzXml.GetDocumentThisYearSeason season_status= " + season_status.ToString();
                return ret_index_this_season;
            }

            if (index_this_season >= m_xdocument_documents.Length)
            {
                o_error = @"JazzXml.GetIndexForThisSeason index_this_year_season >= m_xdocument_documents.Length= " + m_xdocument_documents.Length.ToString();
                return ret_index_this_season;
            }

            ret_index_this_season = index_this_season;

            return ret_index_this_season;

        } // GetIndexForThisSeason

        /// <summary>Returns the index in array GetObjectAllDocs (m_xdocument_documents) for the next season 
        /// <para>The next season is defined by function JazzXml.GetPublishSeasonStartYearInt() plus one (1)</para>
        /// <para>Function  JazzXml.GetSeasonIndex returns the index for the next season</para>
        /// <para>The function returns a negative index value if the corresponding XDocument in m_xdocument_documents is corrupt.</para>
        /// <para></para>
        /// <para></para>
        /// <para>Please note that arrays GetObjectAllDocs, GetFileNamesAllDocs and GetSeasonNamesAllDocs correspond to each other</para>
        /// </summary>
        /// <param name="o_error">Error message when function has failed and returns a negative value</param>
        private static int GetIndexForNextSeason(out string o_error)
        {
            int ret_index_next_season = -12345;
            o_error = @"";

            int next_season_start_year = JazzXml.GetPublishSeasonStartYearInt();
            if (next_season_start_year < 2015)
            {
                o_error = @"JazzXml.GetIndexForNextSeason Publish season start year not OK";
                return ret_index_next_season;
            }

            next_season_start_year = next_season_start_year + 1;

            if (null == m_xdocument_documents)
            {
                o_error = @"JazzXml.GetIndexForNextSeason m_xdocument_documents == null";
                return ret_index_next_season;
            }

            int season_status = -12345;
            int index_next_season = GetDocumentSeasonIndex(next_season_start_year, out season_status);
            if (index_next_season < 0)
            {
                o_error = @"JazzXml.GetDocumentSeasonIndex index_this_year_season= " + index_next_season.ToString();
                return ret_index_next_season;
            }
            if (season_status < 0)
            {
                o_error = @"JazzXml.GetIndexForNextSeason season_status= " + season_status.ToString();
                return ret_index_next_season;
            }

            if (index_next_season >= m_seasons_documents.Length)
            {
                o_error = @"JazzXml.GetIndexForNextSeason index_this_year_season >= m_xdocument_documents.Length= " + m_xdocument_documents.Length.ToString();
                return ret_index_next_season;
            }

            ret_index_next_season = index_next_season;

            return ret_index_next_season;

        } // GetIndexForNextSeason

        /// <summary>Returns the XDocument (JazzProgramm_20xx_20yy.xml) for this year season. Returns null if the XDocument is corrupt
        /// <para>There is also a function GetDocumentCurrent that returns the active XDocument</para>
        /// <para>corresponding to m_document_current. This function returns the XDocument for this year</para>
        /// <para>TODO Check that GetDocumentCurrent not is used where this function should be used</para>
        /// </summary>
        public static XDocument GetDocumentThisYearSeason(out string o_error)
        {
            XDocument ret_document = null;
            o_error = @"";

            if (null == m_seasons_documents)
            {
                o_error = @"JazzXml.GetDocumentThisYearSeason m_seasons_documents == null";
                return ret_document;
            }

            int this_year_season = JazzXml.GetPublishSeasonStartYearInt();
            if (this_year_season < 2015)
            {
                o_error = @"JazzXml.GetDocumentThisYearSeason Publish season start year not OK";
                return ret_document;
            }

            int season_status = -12345;
            int index_this_year_season = GetProgramSeasonIndex(this_year_season, out season_status);
            if (index_this_year_season < 0)
            {
                o_error = @"JazzXml.GetDocumentThisYearSeason index_this_year_season= " + index_this_year_season.ToString();
                return ret_document;
            }
            if (season_status < 0)
            {
                o_error = @"JazzXml.GetDocumentThisYearSeason season_status= " + season_status.ToString();
                return ret_document;
            }


            if (index_this_year_season >= m_seasons_documents.Length)
            {
                o_error = @"JazzXml.GetDocumentThisYearSeason index_this_year_season >= m_seasons_documents.Length= " + m_seasons_documents.Length.ToString();
                return ret_document;
            }

            ret_document = m_seasons_documents[index_this_year_season];

            return ret_document;
        } // GetDocumentThisYearSeason

        /// <summary>Returns the XDocument (JazzProgramm_20xx_20yy.xml) for the next year season. Returns null if the XDocument is corrupt</summary>
        public static XDocument GetDocumentNextYearSeason(out string o_error)
        {
            XDocument ret_document = null;
            o_error = @"";

            if (null == m_seasons_documents)
            {
                o_error = @"JazzXml.GetDocumentNextYearSeason m_seasons_documents == null";
                return ret_document;
            }

            int next_year_season = JazzXml.GetPublishSeasonStartYearInt() + 1;
            if (next_year_season < 2015)
            {
                o_error = @"JazzXml.GetDocumentNextYearSeason Publish season start year not OK";
                return ret_document;
            }


            int season_status = -12345;
            int index_next_year_season = GetProgramSeasonIndex(next_year_season, out season_status);
            if (index_next_year_season < 0)
            {
                o_error = @"JazzXml.GetDocumentNextYearSeason index_next_year_season= " + index_next_year_season.ToString();
                return ret_document;
            }
            if (season_status < 0)
            {
                o_error = @"JazzXml.GetDocumentNextYearSeason season_status= " + season_status.ToString();
                return ret_document;
            }

            if (index_next_year_season >= m_seasons_documents.Length)
            {
                o_error = @"JazzXml.GetDocumentNextYearSeason index_next_year_season >= m_seasons_documents.Length= " + m_seasons_documents.Length.ToString();
                return ret_document;
            }

            ret_document = m_seasons_documents[index_next_year_season];

            return ret_document;
        } // GetDocumentNextYearSeason

        /// <summary>Returns the XDocument (JazzProgramm_20xx_20yy.xml) for the input (start) year season. Returns null if the XDocument is corrupt</summary>
        public static XDocument GetDocumentInputYearSeason(int i_season_start_year, out string o_error)
        {
            XDocument ret_document = null;
            o_error = @"";

            if (null == m_seasons_documents)
            {
                o_error = @"JazzXml.GetDocumentInputYearSeason m_seasons_documents == null";
                return ret_document;
            }

            int input_year_season = i_season_start_year;

            if (input_year_season < 1996)
            {
                o_error = @"JazzXml.GetDocumentInputYearSeason Publish season start year not OK";

                return ret_document;
            }


            int season_status = -12345;

            int index_input_year_season = GetProgramSeasonIndex(input_year_season, out season_status);

            if (index_input_year_season < 0)
            {
                o_error = @"JazzXml.GetDocumentInputYearSeason index_input_year_season= " + index_input_year_season.ToString();

                return ret_document;
            }
            if (season_status < 0)
            {
                o_error = @"JazzXml.GetDocumentInputYearSeason season_status= " + season_status.ToString();

                return ret_document;
            }

            if (index_input_year_season >= m_seasons_documents.Length)
            {
                o_error = @"JazzXml.GetDocumentInputYearSeason index_input_year_season >= m_seasons_documents.Length= " + m_seasons_documents.Length.ToString();
                
                return ret_document;
            }

            ret_document = m_seasons_documents[index_input_year_season];

            return ret_document;

        } // GetDocumentInputYearSeason

        /// <summary>
        /// Returns true if season documents exist
        /// </summary>
        /// <param name="i_season_start_year"></param>
        public static bool SeasonDocumentsExists(int i_season_start_year)
        {
            bool ret_b_exists = true;

            int season_status = -12345;

            int index_document = GetDocumentSeasonIndex(i_season_start_year, out season_status);

            if (index_document >= 0)
            {
                ret_b_exists = true;
            }
            else
            {
                ret_b_exists = false;
            }

            return ret_b_exists;

        } // SeasonDocumentsExists

        /// <summary>Returns the index for arrays m_seasons_start_years and m_seasons_status (JazzDokumente_20xx_20yy.xml). Eq. -1: Year is not in the array</summary>
        ///  <param name="i_season_start_year">Season start year</param>
        ///  <param name="o_season_status">Status for returned XDocument (from m_seasons_status)</param>
        private static int GetDocumentSeasonIndex(int i_season_start_year, out int o_season_status)
        {
            int ret_index = -1;
            o_season_status = -12345; // m_seasons_documents_start_years

            if (null == m_seasons_documents_start_years || null == m_seasons_documents_status || m_seasons_documents_status.Length != m_seasons_documents_start_years.Length)
                return -99;

            for (int index_season=0; index_season< m_seasons_documents_start_years.Length; index_season++)
            {
                if (m_seasons_documents_start_years[index_season] == i_season_start_year)
                {
                    ret_index = index_season;
                    o_season_status = m_seasons_documents_status[index_season];
                    break;
                }
            } // index_season

            return ret_index;
        } // GetDocumentSeasonIndex

        /// <summary>Returns the index for arrays m_seasons_start_years and m_seasons_status (JazzProgramm_20xx_20yy.xml). Eq. -1: Year is not in the array</summary>
        ///  <param name="i_season_start_year">Season start year</param>
        ///  <param name="o_season_status">Status for returned XDocument (from m_seasons_status)</param>
        private static int GetProgramSeasonIndex(int i_season_start_year, out int o_season_status)
        {
            int ret_index = -1;
            o_season_status = -12345; 

            if (null == m_seasons_start_years || null == m_seasons_status || m_seasons_status.Length != m_seasons_start_years.Length)
                return -99;

            for (int index_season = 0; index_season < m_seasons_start_years.Length; index_season++)
            {
                if (m_seasons_start_years[index_season] == i_season_start_year)
                {
                    ret_index = index_season;
                    o_season_status = m_seasons_status[index_season];
                    break;
                }
            } // index_season

            return ret_index;
        } // GetProgramSeasonIndex

        /// <summary>Writes the input X document to a file
        /// <para></para>
        /// </summary>
        /// <param name="i_document">X document</param>
        /// <param name="i_full_file_name">File name with path</param>
        /// <param name="o_error">Error message</param>
        static public bool WriteToFile(XDocument i_document, string i_full_file_name, out string o_error)
        {
            o_error = @"";

            if (null == i_document)
            {
                o_error = @"JazzXml.WriteToFile Programming error: Input document is null";
                return false;
            }

            if (i_full_file_name.Trim().Length == 0)
            {
                o_error = @"JazzXml.WriteToFile Programming error: Input file name is empty";
                return false;
            }

            try
            {
                i_document.Save(i_full_file_name);
            }
            catch (Exception e)
            {
                String exc_msg = e.ToString();

                o_error = @"JazzXml.WriteToFile Programming error: " + exc_msg;

                return false;
            }

            return true;
        } // WriteToFile

        /// <summary>Returns true if the next season exists</summary>
        static public bool NextSeasonExists(int i_season_start_year)
        {
            bool ret_exists = false;

            if (i_season_start_year <= 2015)
                return false; // Programming error

            int next_start_year = i_season_start_year + 1;

            int[] start_years = GetSeasonsStartYears();
            if (null == start_years)
                return false; // Programming error

            if (null == m_seasons_status)
                return false; // Programming error

            if (start_years.Length != m_seasons_status.Length)
                return false; // Programming error

            for (int index_year=0; index_year< start_years.Length; index_year++)
            {
                if (start_years[index_year] == next_start_year)
                {
                    if (m_seasons_status[index_year] >= 0)
                    {
                        ret_exists = true;
                    }
                    else
                    {
                        ret_exists = false;
                    }
                    break;
                }

            }

            return ret_exists;
        } // NextSeasonExists

        /// <summary>Returns the day name of the concert as string TODO Move to file JazzXml.cs</summary>
        static public String GetDayName(int i_concert) { return GetInnerTextForNode(i_concert, "DayName"); }

        /// <summary>Returns the big size poster URL for the concert TODO Move to file JazzXml.cs</summary>
        static public String GetPosterBigSize(int i_concert) { return GetWebSiteUrl() + GetInnerTextForNode(i_concert, "PosterBigSize"); }

        /// <summary>Returns the publish season start year. TODO Move to JazzXml</summary>
        static public String GetPublishSeasonStartYear() { return GetInnerTextForApplicationNode("PublishSeasonStartYear"); }

        /// <summary>Returns the publish season start year as integer. TODO Move to JazzXml</summary>
        static public int GetPublishSeasonStartYearInt()
        {
            int ret_number = -12345;

            String publish_season_start_year_string = GetPublishSeasonStartYear();

            if (!XmlNodeValueIsSet(publish_season_start_year_string))
                return -1;

            ret_number = JazzUtils.StringToInt(publish_season_start_year_string);

            return ret_number;
        } // GetPublishSeasonStartYearInt

        /// <summary>Returns request caption</summary>
        static public String GetRequestCaption() { return GetInnerTextForApplicationNode(GetTagRequestCaption()); }

        /// <summary>Returns request header</summary>
        static public String GetRequestHeader() { return GetInnerTextForApplicationNode(GetTagRequestHeader()); }

        /// <summary>Returns request dates display flag</summary>
        static public String GetRequestDatesDisplay() { return GetInnerTextForApplicationNode(GetTagRequestDatesDisplay()); }

        /// <summary>Returns true if the concert dates shall be displayed</summary>
        static public Boolean GetRequestDatesDisplayBool()
        {
            Boolean ret_display_dates = false;

            String display_dates = GetRequestDatesDisplay();

            if (display_dates.Equals("TRUE"))
            {
                ret_display_dates = true;
            }

            return ret_display_dates;

        } // GetRequestDatesDisplayBool

        /// <summary>Returns request no dates text</summary>
        static public String GetRequestNoDatesText() { return GetInnerTextForApplicationNode(GetTagRequestNoDatesText()); }

        /// <summary>Returns request dates text</summary>
        static public String GetRequestDatesText() { return GetInnerTextForApplicationNode(GetTagRequestDatesText()); }

        /// <summary>Returns request content header</summary>
        static public String GetRequestContentHeader() { return GetInnerTextForApplicationNode(GetTagRequestContentHeader()); }

        /// <summary>Returns request content one</summary>
        static public String GetRequestContentOne() { return GetInnerTextForApplicationNode(GetTagRequestContentOne()); }

        /// <summary>Returns request content two</summary>
        static public String GetRequestContentTwo() { return GetInnerTextForApplicationNode(GetTagRequestContentTwo()); }

        /// <summary>Returns request content three</summary>
        static public String GetRequestContentThree() { return GetInnerTextForApplicationNode(GetTagRequestContentThree()); }

        /// <summary>Returns request content four</summary>
        static public String GetRequestContentFour() { return GetInnerTextForApplicationNode(GetTagRequestContentFour()); }

        /// <summary>Returns request content five</summary>
        static public String GetRequestContentFive() { return GetInnerTextForApplicationNode(GetTagRequestContentFive()); }

        /// <summary>Returns request content six</summary>
        static public String GetRequestContentSix() { return GetInnerTextForApplicationNode(GetTagRequestContentSix()); }

        /// <summary>Returns request content seven</summary>
        static public String GetRequestContentSeven() { return GetInnerTextForApplicationNode(GetTagRequestContentSeven()); }

        /// <summary>Returns request content eight</summary>
        static public String GetRequestContentEight() { return GetInnerTextForApplicationNode(GetTagRequestContentEight()); }

        /// <summary>Returns request content nine</summary>
        static public String GetRequestContentNine() { return GetInnerTextForApplicationNode(GetTagRequestContentNine()); }

        /// <summary>Returns request email address</summary>
        static public String GetRequestEmailAddress() { return GetInnerTextForApplicationNode(GetTagRequestEmailAddress()); }

        /// <summary>Returns request email title</summary>
        static public String GetRequestEmailTitle() { return GetInnerTextForApplicationNode(GetTagRequestEmailTitle()); }

        /// <summary>Returns request email caption</summary>
        static public String GetRequestEmailCaption() { return GetInnerTextForApplicationNode(GetTagRequestEmailCaption()); }

        /// <summary>Returns request remark</summary>
        static public String GetRequestEmailRemark() { return GetInnerTextForApplicationNode(GetTagRequestEmailRemark()); }

        /// <summary>Returns request end paragraph</summary>
        static public String GetRequestEndParagraph() { return GetInnerTextForApplicationNode(GetTagRequestEndParagraph()); }

        /// <summary>Returns the flag (string TRUE or FALSE) telling if it is allowed to make reservations</summary>
        static public String GetReservationNotAllowed() { return GetInnerTextForApplicationNode(GetTagReservationNotAllowed()); }

        /// <summary>Returns the text for the case that reservations not are allowed</summary>
        static public String GetReservationNotAllowedText() { return GetInnerTextForApplicationNode(GetTagReservationNotAllowedText()); }

        #endregion // XML utility functions

        #region Reload XML 
        // These functions should typically be called before a checkout is made by the calling function
        // Checkout meaning blocking others to make changes of the file.
        // Reload should be called for the following case:
        // - Multiple users are using Admin at the same time. 
        // - One user make changes to an XML file and stores it on the server (i.e. Checkout -> Change -> Upload -> Checkin)
        // - Another user changes the same XML file and stores it on the server
        // For this case (without an XML reload) the changes made by the first users will be lost

        /// <summary>Reload the application XML (JazzApplication.xml)</summary>
        static public void ReloadApplicationXml()
        {
            string application_xml_url = GetApplicationFileName();

            JazzOsUtils.LoadXmlDocument(application_xml_url, 1, -12345);

        } // ReloadApplicationXml

        /// <summary>Reload the current (active) season program XML (JazzProgramm_20YY_20YY.xml)</summary>
        static public void ReloadCurrentSeasonProgramXml()
        {
            //QQ 2018-08-28 string current_season_xml_url = GetCurrentSeasonFileName();
            string current_season_xml_url = GetCurrentSeasonFileUrl();

            JazzOsUtils.LoadXmlDocument(current_season_xml_url, 2, -12345);

        } // ReloadCurrentSeasonProgramXml

        /// <summary>Reload the current season document XML (JazzDokumente_20YY_20YY.xml)</summary>
        static public void ReloadCurrentSeasonDocumentXml()
        {
            string current_season_document_xml_url = GetCurrentSeasonDocumentFileName();
            if (current_season_document_xml_url.Length == 0)
                return;

            int index_doc = GetCurrentSeasonDocumentIndex();
            if (index_doc < 0)
                return;

            JazzOsUtils.LoadXmlDocument(current_season_document_xml_url, 5, index_doc);

        } // ReloadCurrentSeasonDocumentXml

        // m_seasons_documents_start_years = JazzUtils.GetSeasonStartYearsForExistingXmlDocumentsFiles(m_url_xml_doc_files_folder, m_documents_start_year);
        // if (null == m_seasons_start_years)
        //  return false;

        /// <summary>Returns the index for the current season document XML (JazzDokumente_20YY_20YY.xml)</summary>
        static private int GetCurrentSeasonDocumentIndex()
        {
            int ret_index = -12345;

            int[] seasons_documents_start_years = JazzUtils.GetSeasonStartYearsForExistingXmlDocumentsFiles(m_url_xml_doc_files_folder, m_documents_start_year);
            if (null == seasons_documents_start_years || seasons_documents_start_years.Length == 0)
                return ret_index;

            int start_year = GetCurrentSeasonDocumentStartYear();
            if (start_year < 0)
                return ret_index;

            for (int index_year = 0; index_year < seasons_documents_start_years.Length; index_year++)
            {
                int current_year = seasons_documents_start_years[index_year];

                if (current_year == start_year)
                {
                    ret_index = index_year;
                    break;
                }
            }

            return ret_index;

        } // GetCurrentSeasonDocumentIndex

        /// <summary>Returns the start year for the current season document XML (JazzDokumente_20YY_20YY.xml)</summary>
        static private int GetCurrentSeasonDocumentStartYear()
        {
            int ret_start_year = -12345;

            string season_years = JazzXml.GetDocSeasonYears();
            string start_year_str = season_years.Substring(0, 4);
            int start_year = JazzUtils.StringToInt(start_year_str);
            if (start_year < 2000)
                return ret_start_year;

            ret_start_year = start_year;

            return ret_start_year;

        } // GetCurrentSeasonDocumentStartYear

        /// <summary>Returns the current season document XML file name with path(JazzDokumente_20YY_20YY.xml)</summary>
        static private string GetCurrentSeasonDocumentFileName()
        {
            string ret_file_name = @"";

            int start_year = GetCurrentSeasonDocumentStartYear();
            if (start_year < 0)
                return ret_file_name;

            ret_file_name = GetSeasonDocumentsFileName(start_year, m_url_xml_doc_files_folder);

            return ret_file_name;

        } // GetCurrentSeasonDocumentFileName

        #endregion // Reload XML 

    } // JazzXml

} // namespace