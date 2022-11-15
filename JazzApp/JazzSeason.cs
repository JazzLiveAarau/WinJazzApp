using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for a concert season
    /// <para></para>
    /// </summary>
    public class JazzSeason
    {
        #region Constructor

        /// <summary>Constructor</summary>
        public JazzSeason(int i_season_start_year, string i_season_comment, int i_n_concerts)
        {
            if (i_season_start_year < 1996)
            {
                return;
            }

            if (i_n_concerts < 1)
            {
                return;
            }

            SeasonStartYear = i_season_start_year;

            SeasonComment = i_season_comment;

            YearAutumnInt = SeasonStartYear;

            YearSpringInt = SeasonStartYear + 1;

            PublishProgram = "FALSE";

            InputNumberConcerts = i_n_concerts;

            m_concerts = new JazzConcert[InputNumberConcerts];

            for (int index_code=0; index_code < InputNumberConcerts; index_code++)
            {
                JazzConcert jazz_concert = new JazzConcert();

                m_concerts[index_code] = jazz_concert;
            }

        } // Constructor

        #endregion // Constructor

        #region Define, set and get member variables

        /// <summary>Season start year</summary>
        private int m_season_start_year = -12345;
        /// <summary>Get and set season start year</summary>
        private int SeasonStartYear { get { return m_season_start_year; } set { m_season_start_year = value; } }

        /// <summary>Season comment</summary>
        private string m_season_comment = @"";
        /// <summary>Get and set season comment</summary>
        public string SeasonComment { get { return m_season_comment; } set { m_season_comment = value; } }

        /// <summary>Number of concert objects</summary>
        private int m_n_concerts = -12345;
        /// <summary>Get number of concert objects</summary>
        private int InputNumberConcerts { get { return m_n_concerts; } set { m_n_concerts = value; } }

        /// <summary>Season year autumn as string</summary>
        private string m_season_year_autumn_str = @"";
        /// <summary>Get and set season year autumn as string</summary>
        public string YearAutumn { get { return m_season_year_autumn_str; } set { m_season_year_autumn_str = value; } }

        /// <summary>Season year spring as string</summary>
        private string m_season_year_spring_str = @"";
        /// <summary>Get and set season year spring as string</summary>
        public string YearSpring { get { return m_season_year_spring_str; } set { m_season_year_spring_str = value; } }

        /// <summary>Flag (as string) telling if the season program may be published</summary>
        private string m_publish_program_str = @"";
        /// <summary>Get and set flag (as string) telling if the season program may be published</summary>
        public string PublishProgram { get { return m_publish_program_str; } set { m_publish_program_str = value; } }

        #endregion // Define, set and get member variables

        #region Get and set some member variables as integers

        /// <summary>Get and set season autumn year as integer</summary>
        public int YearAutumnInt { get { return JazzUtils.StringToInt(m_season_year_autumn_str); } set { m_season_year_autumn_str = value.ToString(); } }

        /// <summary>Get and set season autumn year as integer</summary>
        public int YearSpringInt { get { return JazzUtils.StringToInt(m_season_year_spring_str); } set { m_season_year_spring_str = value.ToString(); } }

        #endregion // Get and set some member variables as integers

        #region Get and set some member variables as booleans

        /// <summary>Get and set the oublish program flag as boolean</summary>
        public bool PublishProgramBoolean
        {
            get
            {
                if (m_publish_program_str.Equals("FALSE"))
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    m_publish_program_str = "TRUE";
                else
                    m_publish_program_str = "FALSE";
            }

        } // PublishProgramBoolean

        #endregion // Get and set some member variables as booleans

        #region Definition and functions for the array of concerts

        /// <summary>Array of JazzConcert objects</summary>
        private JazzConcert[] m_concerts = null;

        /// <summary>Returns the number of concert objects</summary>
        public int GetNumberOfConcerts()
        {
            return m_concerts.Length;

        } // GetNumberOfConcerts

        /// <summary>Returns a JazzConcert object for a given number</summary>
        public JazzConcert GetJazzConcert(int i_concert_number, out string o_error)
        {
            JazzConcert ret_object = null;

            o_error = @"";

            string error_msg = @"";

            if (!CheckConcertNumber(i_concert_number, out error_msg))
            {
                o_error = @"JazzSeason.GetJazzConcert CheckConcertNumber failed " + error_msg;

                return ret_object;
            }

            ret_object = m_concerts[i_concert_number - 1];

            return ret_object;

        } // GetJazzConcert

        /// <summary>Sets a JazzConcert object for a given number</summary>
        public bool SetJazzConcert(int i_concert_number, JazzConcert i_jazz_concert, out string o_error)
        {
            o_error = @"";

            string error_msg = @"";

            if (!CheckConcertNumber(i_concert_number, out error_msg))
            {
                o_error = @"JazzSeason.GetJazzConcert CheckConcertNumber failed " + error_msg;

                return false;
            }

            if (!i_jazz_concert.CheckParameterValues(out o_error))
            {
                o_error = @"SeasonConcert.SetJazzConcert JazzConcert.CheckParameterValues failed " + o_error;

                return false;
            }

            m_concerts[i_concert_number - 1] = i_jazz_concert;

            return true;

        } // SetJazzConcert

        /// <summary>Appends a JazzMusician object</summary>
        public bool AppendJazzConcert(JazzConcert i_concert_object, out string o_error)
        {
            o_error = @"";

            string error_msg = @"";

            if (!i_concert_object.CheckParameterValues(out error_msg))
            {
                o_error = @"JazzSeason.AppendJazzConcert JazzConcert.CheckParameterValues failed " + error_msg;

                return false;
            }

            JazzConcert[] append_concert_array = new JazzConcert[GetNumberOfConcerts() + 1];

            for (int index_concert = 0; index_concert < GetNumberOfConcerts(); index_concert++)
            {
                append_concert_array[index_concert] = m_concerts[index_concert];
            }

            append_concert_array[GetNumberOfConcerts()] = i_concert_object;

            m_concerts = append_concert_array;

            return true;

        } // AppendJazzConcert

        /// <summary>Deletes a JazzConcert object</summary>
        public bool DeleteJazzConcert(int i_concert_number, out string o_error)
        {
            o_error = @"";

            string error_msg = @"";

            if (!CheckConcertNumber(i_concert_number, out error_msg))
            {
                o_error = @"JazzSeason.DeleteJazzConcert CheckConcertNumber failed " + error_msg;

                return false;
            }

            JazzConcert[] delete_concert_array = new JazzConcert[GetNumberOfConcerts() - 1];

            int index_out = 0;

            for (int index_concert = 0; index_concert < GetNumberOfConcerts(); index_concert++)
            {
                if (index_concert + 1 != i_concert_number)
                {
                    delete_concert_array[index_out] = m_concerts[index_concert];

                    index_out = index_out + 1;
                }
            }

            m_concerts = delete_concert_array;

            return true;

        } // DeleteJazzConcert

        /// <summary>Returns true if the musician object number exists</summary>
        private bool CheckConcertNumber(int i_concert_number, out string o_error)
        {
            o_error = @"";

            if (i_concert_number <= 0 || i_concert_number > GetNumberOfConcerts())
            {
                o_error = @"JazzSeason.CheckConcertNumber i_concert_number not between 1 and " + GetNumberOfConcerts().ToString();

                return false;
            }

            return true;

        } // CheckConcertNumber

        #endregion // Definition and functions for the array of concerts

        #region Set to empty strings "" if member parameter values not yet are set "NotYetSetNodeValue"

        /// <summary>Set to empty strings if values not yet are set</summary>
        public void SetToEmptyStringsForValuesNotYetSet()
        {
            SeasonComment = JazzSeasonUtil.EmptyStringIfValueNotYetSet(SeasonComment);

            YearAutumn = JazzSeasonUtil.EmptyStringIfValueNotYetSet(YearAutumn);

            YearSpring = JazzSeasonUtil.EmptyStringIfValueNotYetSet(YearSpring);

            PublishProgram = JazzSeasonUtil.EmptyStringIfValueNotYetSet(PublishProgram);

            for (int index_concert_empty = 0; index_concert_empty < GetNumberOfConcerts(); index_concert_empty++)
            {
                JazzConcert jazz_concert_empty = m_concerts[index_concert_empty];

                jazz_concert_empty.SetToEmptyStringsForValuesNotYetSet();

            }

        } // SetToEmptyStringsForValuesNotYetSet

        #endregion // Set to empty strings "" if member parameter values not yet are set "NotYetSetNodeValue"

        #region Set to values not yet set ("NotYetSetNodeValue") for member parameters with empty string values ("")

        /// <summary>Set to values not yet set ("NotYetSetNodeValue") for member parameters with empty string values ("")
        /// <para>The function JazzSeasonUtil.SetToValueNotYetSetIfEmptyString is called for all member variables</para>
        /// <para>The string "NotYetSetNodeValue" can be retrieved with JazzXml.GetUndefinedNodeValue()</para>
        /// </summary>
        public void SetToValuesNotYetSetForEmptyStrings(out string o_error)
        {
            o_error = @"";

            SeasonComment = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(SeasonComment);

            YearAutumn = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(YearAutumn);

            YearSpring = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(YearSpring);

            PublishProgram = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(PublishProgram);

            for (int index_concert_not_set = 0; index_concert_not_set < GetNumberOfConcerts(); index_concert_not_set++)
            {
                JazzConcert jazz_concert = m_concerts[index_concert_not_set];

                jazz_concert.SetToValuesNotYetSetForEmptyStrings();
            }

        } // SetToValuesNotYetSetForEmptyStrings

        #endregion // Set to values not yet set ("NotYetSetNodeValue") for member parameters with empty string values ("")

        #region Check functions

        /// <summary>Check the parameter values</summary>
        public bool CheckParameterValues(out string o_error)
        {
            o_error = @"";

            string error_msg = @"";

            for (int index_concert_check = 0; index_concert_check < GetNumberOfConcerts(); index_concert_check++)
            {
                JazzConcert jazz_concert_check = m_concerts[index_concert_check];

                if (!jazz_concert_check.CheckParameterValues(out error_msg))
                {
                    o_error = @"JazzSeason.CheckParameterValues JazzConcert.CheckParameterValues failed " + error_msg;

                    return false;
                }

            }

            return true;

        } // CheckParameterValues

        #endregion // Check functions

    } // JazzSeason

    /// <summary>Holds data for a concert
    /// <para></para>
    /// </summary>
    public class JazzConcert
    {
        #region Constructor 

        /// <summary>Constructor</summary>
        public JazzConcert()
        {

            JazzMusician jazz_musician = new JazzMusician();

            jazz_musician.MusicianName = "Musician name";

            m_concert_musicians = new JazzMusician[1];

            m_concert_musicians[0] = jazz_musician;

        } // Constructor

        #endregion // Constructor 

        #region Define, set and get member variables (as strings)

        /// <summary>Concert day name</summary>
        private string m_concert_day_name = @"";
        /// <summary>Get and set concert day name</summary>
        public string ConcertDayName { get { return m_concert_day_name; } set { m_concert_day_name = value; } }

        /// <summary>Concert year as string</summary>
        private string m_concert_year_str = @"";
        /// <summary>Get and set concert year as string</summary>
        public string ConcertYear { get { return m_concert_year_str; } set { m_concert_year_str = value; } }

        /// <summary>Concert month as string</summary>
        private string m_concert_month_str = @"";
        /// <summary>Get and set concert month as string</summary>
        public string ConcertMonth { get { return m_concert_month_str; } set { m_concert_month_str = value; } }

        /// <summary>Concert day as string</summary>
        private string m_concert_day_str = @"";
        /// <summary>Get and set concert day as string</summary>
        public string ConcertDay { get { return m_concert_day_str; } set { m_concert_day_str = value; } }

        /// <summary>Concert start hour as string</summary>
        private string m_concert_start_hour_str = @"";
        /// <summary>Get and set concert start hour as string</summary>
        public string ConcertTimeStartHour { get { return m_concert_start_hour_str; } set { m_concert_start_hour_str = value; } }

        /// <summary>Concert start minute as string</summary>
        private string m_concert_start_minute_str = @"";
        /// <summary>Get and set concert start minute as string</summary>
        public string ConcertTimeStartMinute { get { return m_concert_start_minute_str; } set { m_concert_start_minute_str = value; } }

        /// <summary>Concert end hour as string</summary>
        private string m_concert_end_hour_str = @"";
        /// <summary>Get and set concert end hour as string</summary>
        public string ConcertTimeEndHour { get { return m_concert_end_hour_str; } set { m_concert_end_hour_str = value; } }

        /// <summary>Concert end minute as string</summary>
        private string m_concert_end_minute_str = @"";
        /// <summary>Get and set concert end minute as string</summary>
        public string ConcertTimeEndMinute { get { return m_concert_end_minute_str; } set { m_concert_end_minute_str = value; } }

        /// <summary>Concert place</summary>
        private string m_concert_place = @"";
        /// <summary>Get and set concert place</summary>
        public string ConcertPlace { get { return m_concert_place; } set { m_concert_place = value; } }

        /// <summary>Concert street</summary>
        private string m_concert_street = @"";
        /// <summary>Get and set concert street</summary>
        public string ConcertStreet { get { return m_concert_street; } set { m_concert_street = value; } }

        /// <summary>Concert city</summary>
        private string m_concert_city = @"";
        /// <summary>Get and set concert city</summary>
        public string ConcertCity { get { return m_concert_city; } set { m_concert_city = value; } }

        /// <summary>Concert cancelled flag as string (TRUE or FALSE)</summary>
        private string m_concert_cancelled_str = @"FALSE";
        /// <summary>Get and set concert cancelled flag as string (TRUE or FALSE)</summary>
        public string ConcertCancelled { get { return m_concert_cancelled_str; } set { m_concert_cancelled_str = value; } }

        /// <summary>Concert band name</summary>
        private string m_concert_band_name = @"";
        /// <summary>Get and set concert band name</summary>
        public string ConcertBandName { get { return m_concert_band_name; } set { m_concert_band_name = value; } }

        /// <summary>Concert short text</summary>
        private string m_concert_short_text = @"";
        /// <summary>Get and set concert short text</summary>
        public string ConcertShortText { get { return m_concert_short_text; } set { m_concert_short_text = value; } }

        /// <summary>Concert additional text</summary>
        private string m_concert_additional_text = @"";
        /// <summary>Get and set concert additional text</summary>
        public string ConcertAdditionalText { get { return m_concert_additional_text; } set { m_concert_additional_text = value; } }

        /// <summary>Concert label additional text</summary>
        private string m_concert_label_additional_text = @"";
        /// <summary>Get and set concert label additional text</summary>
        public string ConcertLabelAdditionalText { get { return m_concert_label_additional_text; } set { m_concert_label_additional_text = value; } }

        /// <summary>Flag telling if the flyer free text shall be published on the homepage (as string TRUE or FALSE)</summary>
        private string m_flyer_text_homepage_publish_str = @"TRUE";
        /// <summary>Get and set flag telling if the flyer free text shall be published on the homepage (as string TRUE or FALSE)</summary>
        public string FlyerTextHomepagePublish { get { return m_flyer_text_homepage_publish_str; } set { m_flyer_text_homepage_publish_str = value; } }

        /// <summary>Label for the free text on the flyer</summary>
        private string m_concert_label_flyer_text = @"";
        /// <summary>Get and set the label for the free text on the flyer</summary>
        public string ConcertLabelFlyerText { get { return m_concert_label_flyer_text; } set { m_concert_label_flyer_text = value; } }

        /// <summary>Free text on the flyer</summary>
        private string m_concert_flyer_text = @"";
        /// <summary>Get and set the free text on the flyer</summary>
        public string ConcertFlyerText { get { return m_concert_flyer_text; } set { m_concert_flyer_text = value; } }

        /// <summary>URL to the concert big size poster image</summary>
        private string m_concert_poster_big_size = @"";
        /// <summary>Get and set the URL to the concert big size poster image</summary>
        public string ConcertPosterBigSize { get { return m_concert_poster_big_size; } set { m_concert_poster_big_size = value; } }

        /// <summary>URL to the concert mid size poster image</summary>
        private string m_concert_poster_mid_size = @"";
        /// <summary>Get and set the URL to the concert mid size poster image</summary>
        public string ConcertPosterMidSize { get { return m_concert_poster_mid_size; } set { m_concert_poster_mid_size = value; } }

        /// <summary>URL to the concert small size poster image</summary>
        private string m_concert_poster_small_size = @"";
        /// <summary>Get and set the URL to the concert small size poster image</summary>
        public string ConcertPosterSmallSize { get { return m_concert_poster_small_size; } set { m_concert_poster_small_size = value; } }

        /// <summary>URL to the concert sound sample file</summary>
        private string m_concert_sound_sample = @"";
        /// <summary>Get and set the URL to the concert sound sample file</summary>
        public string ConcertSoundSample { get { return m_concert_sound_sample; } set { m_concert_sound_sample = value; } }

        /// <summary>URL to the band website</summary>
        private string m_concert_band_website = @"";
        /// <summary>Get and set the URL to the band website</summary>
        public string ConcertBandWebsite { get { return m_concert_band_website; } set { m_concert_band_website = value; } }

        /// <summary>URL to the concert sound sample file QR Code</summary>
        private string m_concert_sound_sample_qr_code = @"";
        /// <summary>Get and set the URL to the concert sound sample file QR code</summary>
        public string ConcertSoundSampleQrCode { get { return m_concert_sound_sample_qr_code; } set { m_concert_sound_sample_qr_code = value; } }

        /// <summary>URL to the band website QR code</summary>
        private string m_concert_band_website_qr_code = @"";
        /// <summary>Get and set the URL to the band website QR code</summary>
        public string ConcertBandWebsiteQrCode { get { return m_concert_band_website_qr_code; } set { m_concert_band_website_qr_code = value; } }
        /// <summary>URL to the photo gallery one</summary>

        private string m_concert_photo_gallery_one = @"";
        /// <summary>Get and set the URL to the photo gallery one</summary>
        public string ConcertPhotoGalleryOne { get { return m_concert_photo_gallery_one; } set { m_concert_photo_gallery_one = value; } }

        /// <summary>URL to the photo gallery two</summary>
        private string m_concert_photo_gallery_two = @"";
        /// <summary>Get and set the URL to the photo gallery two</summary>
        public string ConcertPhotoGalleryTwo { get { return m_concert_photo_gallery_two; } set { m_concert_photo_gallery_two = value; } }

        /// <summary>Photo gallery one ZIP file name</summary>
        private string m_concert_photo_gallery_one_zip = @"";
        /// <summary>Get and set the photo gallery one ZIP file name</summary>
        public string ConcertPhotoGalleryOneZip { get { return m_concert_photo_gallery_one_zip; } set { m_concert_photo_gallery_one_zip = value; } }

        /// <summary>Photo gallery two ZIP file name</summary>
        private string m_concert_photo_gallery_two_zip = @"";
        /// <summary>Get and set the photo gallery two ZIP file name</summary>
        public string ConcertPhotoGalleryTwoZip { get { return m_concert_photo_gallery_two_zip; } set { m_concert_photo_gallery_two_zip = value; } }

        /// <summary>Concert band contact person</summary>
        private string m_concert_contact_person = @"";
        /// <summary>Get and set the concert band contact person</summary>
        public string ConcertContactPerson { get { return m_concert_contact_person; } set { m_concert_contact_person = value; } }

        /// <summary>Concert band contact email</summary>
        private string m_concert_contact_email = @"";
        /// <summary>Get and set the concert band contact email</summary>
        public string ConcertContactEmail { get { return m_concert_contact_email; } set { m_concert_contact_email = value; } }

        /// <summary>Concert band contact telephone</summary>
        private string m_concert_contact_telephone = @"";
        /// <summary>Get and set the concert band contact telephone</summary>
        public string ConcertContactTelephone { get { return m_concert_contact_telephone; } set { m_concert_contact_telephone = value; } }

        /// <summary>Concert band contact street</summary>
        private string m_concert_contact_street = @"";
        /// <summary>Get and set the concert band contact street</summary>
        public string ConcertContactStreet { get { return m_concert_contact_street; } set { m_concert_contact_street = value; } }

        /// <summary>Concert band contact post code</summary>
        private string m_concert_contact_post_code = @"";
        /// <summary>Get and set the concert band contact post code</summary>
        public string ConcertContactPostCode { get { return m_concert_contact_post_code; } set { m_concert_contact_post_code = value; } }

        /// <summary>Concert band contact IBAN number</summary>
        private string m_concert_contact_iban_number = @"";
        /// <summary>Get and set the concert band contact IBAN number</summary>
        public string IbanNumber { get { return m_concert_contact_iban_number; } set { m_concert_contact_iban_number = value; } }

        /// <summary>Concert band contact remark</summary>
        private string m_concert_contact_remark = @"";
        /// <summary>Get and set the concert band contact remark</summary>
        public string ContactRemark { get { return m_concert_contact_remark; } set { m_concert_contact_remark = value; } }

        /// <summary>Concert band contact city</summary>
        private string m_concert_contact_city = @"";
        /// <summary>Get and set the concert band contact city</summary>
        public string ConcertContactCity { get { return m_concert_contact_city; } set { m_concert_contact_city = value; } }

        /// <summary>Concert band login password</summary>
        private string m_concert_login_password = @"";
        /// <summary>Get and set the concert band login password</summary>
        public string ConcertLoginPassword { get { return m_concert_login_password; } set { m_concert_login_password = value; } }

        #endregion // Define, set and get member variables (as strings)

        #region Definition and functions for the array of musicians

        /// <summary>Array of JazzMusician objects</summary>
        private JazzMusician[] m_concert_musicians = null;

        /// <summary>Returns the number of musician objects</summary>
        public int GetNumberOfMusicians()
        {
            return m_concert_musicians.Length;

        } // GetNumberOfMusicians

        /// <summary>Returns a JazzMusician object for a given number</summary>
        public JazzMusician GetJazzMusician(int i_musician_number, out string o_error)
        {
            JazzMusician ret_object = null;

            o_error = @"";

            string error_msg = @"";

            if (!CheckMusicianNumber(i_musician_number, out error_msg))
            {
                o_error = @"JazzConcert.GetJazzMusician CheckMusicianNumber failed " + error_msg;

                return ret_object;
            }

            ret_object = m_concert_musicians[i_musician_number - 1];

            return ret_object;

        } // GetJazzMusician

        /// <summary>Sets a JazzConcert object for a given number</summary>
        public bool SetJazzMusician(int i_musician_number, JazzMusician i_jazz_musician, out string o_error)
        {
            o_error = @"";

            string error_msg = @"";

            if (!CheckMusicianNumber(i_musician_number, out error_msg))
            {
                o_error = @"JazzConcert.SetJazzMusician CheckMusicianNumber failed " + error_msg;

                return false;
            }

            if (!i_jazz_musician.CheckParameterValues(out o_error))
            {
                o_error = @"JazzConcert.SetJazzMusician JazzMusician.CheckParameterValues failed " + o_error;

                return false;
            }

            m_concert_musicians[i_musician_number - 1] = i_jazz_musician;

            return true;

        } // SetJazzMusician

        /// <summary>Set the array of musician objects
        /// <para>Minimum number of array elements is one )1)</para>
        /// </summary>
        /// <param name="i_concert_musicians">Array of musician objects</param>
        /// <param name="o_error">Error message</param>
        public bool SetMusicians(JazzMusician[] i_concert_musicians, out string o_error)
        {
            o_error = @"";

            if (null == i_concert_musicians)
            {
                o_error = @"JazzConcert.SetMusicians Input array of musician objects is null";

                return false;
            }

            if (i_concert_musicians.Length < 1)
            {
                o_error = @"JazzConcert.SetMusicians Number of elements of the input musicians array is less than one (1)";

                return false;
            }

            m_concert_musicians = i_concert_musicians;

            return true;

        } // SetMusicians

        /// <summary>Appends a JazzMusician object</summary>
        public bool AppendJazzMusician(JazzMusician i_musician_object, out string o_error)
        {
            o_error = @"";

            string error_msg = @"";
            if (!i_musician_object.CheckParameterValues(out error_msg))
            {
                o_error = @"JazzConcert.AppendJazzMusician JazzMusician.CheckParameterValues failed " + error_msg;

                return false;
            }

            JazzMusician[] append_musician_array = new JazzMusician[GetNumberOfMusicians() + 1];

            for (int index_musician = 0; index_musician < GetNumberOfMusicians(); index_musician++)
            {
                append_musician_array[index_musician] = m_concert_musicians[index_musician];
            }

            append_musician_array[GetNumberOfMusicians()] = i_musician_object;

            m_concert_musicians = append_musician_array;

            return true;

        } // AppendJazzMusician

        /// <summary>Deletes a JazzMusician object</summary>
        public bool DeleteJazzMusician(int i_musician_number, out string o_error)
        {
            o_error = @"";

            string error_msg = @"";

            if (!CheckMusicianNumber(i_musician_number, out error_msg))
            {
                o_error = @"JazzConcert.DeleteJazzMusician CheckMusicianNumber failed " + error_msg;

                return false;
            }
            JazzMusician[] delete_musician_array = new JazzMusician[GetNumberOfMusicians() - 1];

            int index_out = 0;

            for (int index_musician = 0; index_musician < GetNumberOfMusicians(); index_musician++)
            {
                if (index_musician + 1 != i_musician_number)
                {
                    delete_musician_array[index_out] = m_concert_musicians[index_musician];

                    index_out = index_out + 1;
                }
            }

            m_concert_musicians = delete_musician_array;

            return true;

        } // DeleteJazzMusician

        /// <summary>Returns true if the musician object number exists</summary>
        private bool CheckMusicianNumber(int i_musician_number, out string o_error)
        {
            o_error = @"";

            if (i_musician_number <= 0 || i_musician_number > GetNumberOfMusicians())
            {
                o_error = @"JazzConcert.CheckMusicianNumber i_musician_number not between 1 and " + GetNumberOfMusicians().ToString();

                return false;
            }

            return true;

        } // CheckMusicianNumber


        #endregion // Definition and functions for the array of musicians

        #region Get and set some member variables as integers

        /// <summary>Get and set concert year as integer</summary>
        public int ConcertYearInt { get { return JazzUtils.StringToInt(m_concert_year_str); } set { m_concert_year_str = value.ToString(); } }

        /// <summary>Get and set concert month as integer</summary>
        public int ConcertMonthInt { get { return JazzUtils.StringToInt(m_concert_month_str); } set { m_concert_month_str = value.ToString(); } }

        /// <summary>Get and set concert day as integer</summary>
        public int ConcertDayInt { get { return JazzUtils.StringToInt(m_concert_day_str); } set { m_concert_day_str = value.ToString(); } }

        /// <summary>Get and set concert start hour as integer</summary>
        public int ConcertTimeStartHourInt { get { return JazzUtils.StringToInt(m_concert_start_hour_str); } set { m_concert_start_hour_str = value.ToString(); } }

        /// <summary>Get and set concert start minute as integer</summary>
        public int ConcertTimeStartMinuteInt { get { return JazzUtils.StringToInt(m_concert_start_minute_str); } set { m_concert_start_minute_str = value.ToString(); } }

        /// <summary>Get and set concert end hour as integer</summary>
        public int ConcertTimeEndHourInt { get { return JazzUtils.StringToInt(m_concert_end_hour_str); } set { m_concert_end_hour_str = value.ToString(); } }

        /// <summary>Get and set concert end minute as integer</summary>
        public int ConcertTimeEndMinuteInt { get { return JazzUtils.StringToInt(m_concert_end_minute_str); } set { m_concert_end_minute_str = value.ToString(); } }



        #endregion // Get and set some member variables as integers

        #region Get and set some member variables as booleans

        /// <summary>Get and set the concert cancelled flag as boolean</summary>
        public bool ConcertCancelledBoolean
        {
            get
            {
                if (m_concert_cancelled_str.Equals("FALSE"))
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    m_concert_cancelled_str = "TRUE";
                else
                    m_concert_cancelled_str = "FALSE";
            }

        } // ConcertCancelledBoolean

        /// <summary>Get and set the flag telling if the flyer free text can be published as boolean</summary>
        public bool FlyerTextHomepagePublishBoolean
        {
            get
            {
                if (m_flyer_text_homepage_publish_str.Equals("FALSE"))
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    m_flyer_text_homepage_publish_str = "TRUE";
                else
                    m_flyer_text_homepage_publish_str = "FALSE";
            }

        } // FlyerTextHomepagePublishBoolean

        #endregion // Get and set some member variables as booleans

        #region Set to empty strings ("") for values not yet set "NotYetSetNodeValue"

        /// <summary>Set to empty strings ("") if values not yet are set, i.e. have the value "NotYetSetNodeValue"
        /// <para>The string "NotYetSetNodeValue" can be retrieved with JazzXml.GetUndefinedNodeValue()</para>
        /// <para>The function JazzSeasonUtil.EmptyStringIfValueNotYetSet is called for all member variables</para>
        /// </summary>
        public void SetToEmptyStringsForValuesNotYetSet()
        {
            ConcertDayName = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertDayName);
            ConcertYear = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertYear);
            ConcertMonth = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertMonth);
            ConcertDay = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertDay);
            ConcertTimeStartHour = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertTimeStartHour);
            ConcertTimeStartMinute = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertTimeStartMinute);
            ConcertTimeEndHour = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertTimeEndHour);
            ConcertTimeEndMinute = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertTimeEndMinute);
            ConcertPlace = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPlace);
            ConcertStreet = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertStreet);
            ConcertCity = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertCity);
            ConcertCancelled = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertCancelled);
            ConcertBandName = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertBandName);
            ConcertShortText = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertShortText);
            ConcertAdditionalText = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertAdditionalText);
            ConcertLabelAdditionalText = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertLabelAdditionalText);
            ConcertLabelFlyerText = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertLabelFlyerText);
            ConcertFlyerText = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertFlyerText);
            FlyerTextHomepagePublish = JazzSeasonUtil.EmptyStringIfValueNotYetSet(FlyerTextHomepagePublish);
            ConcertPosterBigSize = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPosterBigSize);
            ConcertPosterMidSize = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPosterMidSize);
            ConcertPosterSmallSize = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPosterSmallSize);
            ConcertSoundSample = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertSoundSample);
            ConcertBandWebsite = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertBandWebsite);
            ConcertSoundSampleQrCode = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertSoundSampleQrCode);
            ConcertBandWebsiteQrCode = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertBandWebsiteQrCode);
            ConcertPhotoGalleryOne = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPhotoGalleryOne);
            ConcertPhotoGalleryTwo = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPhotoGalleryTwo);
            ConcertPhotoGalleryOneZip = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPhotoGalleryOneZip);
            ConcertPhotoGalleryTwoZip = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertPhotoGalleryTwoZip);
            ConcertContactPerson = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertContactPerson);
            ConcertContactEmail = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertContactEmail);
            ConcertContactTelephone = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertContactTelephone);
            ConcertContactStreet = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertContactStreet);
            ConcertContactPostCode = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertContactPostCode);
            IbanNumber = JazzSeasonUtil.EmptyStringIfValueNotYetSet(IbanNumber);
            ContactRemark = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ContactRemark);
            ConcertContactCity = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertContactCity);
            ConcertLoginPassword = JazzSeasonUtil.EmptyStringIfValueNotYetSet(ConcertLoginPassword);

            for (int index_musician_empty=0; index_musician_empty < GetNumberOfMusicians(); index_musician_empty++)
            {
                JazzMusician jazz_musician_empty = m_concert_musicians[index_musician_empty];

                jazz_musician_empty.SetToEmptyStringsForValuesNotYetSet();

            }

        } // SetToEmptyStringsForValuesNotYetSet

        #endregion // Set to empty strings ("") for values not yet set "NotYetSetNodeValue"

        #region Set to values not yet set ("NotYetSetNodeValue") for empty values ("")

        /// <summary>Set to values not yet set ("NotYetSetNodeValue") for member parameters with empty strings ("")
        /// <para>The function JazzSeasonUtil.SetToValueNotYetSetIfEmptyString is called for all member variables</para>
        /// <para>The string "NotYetSetNodeValue" can be retrieved with JazzXml.GetUndefinedNodeValue()</para>
        /// </summary>
        public void SetToValuesNotYetSetForEmptyStrings()
        {

            ConcertDayName = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertDayName);
            ConcertYear = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertYear);
            ConcertMonth = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertMonth);
            ConcertDay = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertDay);
            ConcertTimeStartHour = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertTimeStartHour);
            ConcertTimeStartMinute = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertTimeStartMinute);
            ConcertTimeEndHour = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertTimeEndHour);
            ConcertTimeEndMinute = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertTimeEndMinute);
            ConcertPlace = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPlace);
            ConcertStreet = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertStreet);
            ConcertCity = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertCity);
            ConcertCancelled = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertCancelled);
            ConcertBandName = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertBandName);
            ConcertShortText = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertShortText);
            ConcertAdditionalText = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertAdditionalText);
            ConcertLabelAdditionalText = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertLabelAdditionalText);
            ConcertLabelFlyerText = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertLabelFlyerText);
            ConcertFlyerText = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertFlyerText);
            FlyerTextHomepagePublish = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(FlyerTextHomepagePublish);
            ConcertPosterBigSize = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPosterBigSize);
            ConcertPosterMidSize = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPosterMidSize);
            ConcertPosterSmallSize = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPosterSmallSize);
            ConcertBandWebsite = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertBandWebsite);
            ConcertSoundSample = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertSoundSample);
            ConcertBandWebsiteQrCode = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertBandWebsiteQrCode);
            ConcertSoundSampleQrCode = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertSoundSampleQrCode);
            ConcertPhotoGalleryOne = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPhotoGalleryOne);
            ConcertPhotoGalleryTwo = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPhotoGalleryTwo);
            ConcertPhotoGalleryOneZip = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPhotoGalleryOneZip);
            ConcertPhotoGalleryTwoZip = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertPhotoGalleryTwoZip);
            ConcertContactPerson = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertContactPerson);
            ConcertContactEmail = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertContactEmail);
            ConcertContactTelephone = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertContactTelephone);
            ConcertContactStreet = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertContactStreet);
            ConcertContactPostCode = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertContactPostCode);
            IbanNumber = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(IbanNumber);
            ContactRemark = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ContactRemark);
            ConcertContactCity = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertContactCity);
            ConcertLoginPassword = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(ConcertLoginPassword);

            for (int index_not_set = 0; index_not_set < GetNumberOfMusicians(); index_not_set++)
            {
                JazzMusician jazz_musician = m_concert_musicians[index_not_set];

                jazz_musician.SetToValuesNotYetSetForEmptyStrings();

            }

            return;

        } // SetToValuesNotYetSetForEmptyStrings

        #endregion // Set to values not yet set ("NotYetSetNodeValue") for empty values ("")

        #region Check functions

        /// <summary>Check the parameter values</summary>
        public bool CheckParameterValues(out string o_error)
        {
            o_error = @"";
            bool ret_bool = true;


            if (ConcertYear.Length != 4)
            {
                o_error = @"JazzConcert.CheckParameterValues Concert year string length is not four (4)";

                return false;
            }

            if (ConcertYearInt < 1996)
            {
                o_error = @"JazzConcert.CheckParameterValues ConcertYearInt value < 1996 ";

                return false;
            }

            if (ConcertMonth.Length < 1 || ConcertMonth.Length > 2)
            {
                o_error = @"JazzConcert.CheckParameterValues Month string length < 1 or > 2";
                return false;
            }

            if (ConcertMonthInt < 1 || ConcertMonthInt > 12)
            {
                o_error = @"JazzConcert.CheckParameterValues ConcertMonthInt value < 1 or > 12 ";
                return false;
            }

            if (ConcertDay.Length < 1 || ConcertDay.Length > 2)
            {
                o_error = @"JazzConcert.CheckParameterValues Day string length < 1 or > 2";
                return false;
            }

            if (ConcertDayInt < 1 || ConcertDayInt > 31)
            {
                o_error = @"JazzConcert.CheckParameterValues ConcertDayInt value < 1 or > 31 ";

                return false;
            }

            // ConcertTimeStartHour
            // ConcertTimeStartMinute
            // ConcertTimeEndHour
            // ConcertTimeEndMinute

            if (ConcertCancelled.Equals("TRUE") || ConcertCancelled.Equals("FALSE"))
            {
                ret_bool = true;
            }
            else
            {
                o_error = @"JazzConcert.CheckParameterValues ConcertCancelled is not equal to TRUE or FALSE";

                ret_bool = false;

                return ret_bool;
            }

            if (FlyerTextHomepagePublish.Equals("TRUE") || FlyerTextHomepagePublish.Equals("FALSE"))
            {
                ret_bool = true;
            }
            else
            {
                o_error = @"JazzConcert.CheckParameterValues FlyerTextHomepagePublish is not equal to TRUE or FALSE";

                ret_bool = false;

                return ret_bool;
            }

            string error_msg = @"";

            for (int index_musician_check = 0; index_musician_check < GetNumberOfMusicians(); index_musician_check++)
            {
                JazzMusician jazz_musician_check = m_concert_musicians[index_musician_check];

                if (!jazz_musician_check.CheckParameterValues(out error_msg))
                {
                    o_error = @"JazzConcert.CheckParameterValues Musician check failed " + error_msg;

                    ret_bool = false;

                    return ret_bool;
                }

            }

            return ret_bool;

        } // CheckParameterValues

        #endregion // Check functions

    } // JazzConcert

    /// <summary>Holds data for one musician
    /// <para></para>
    /// </summary>
    public class JazzMusician
    {
        #region Define, set and get member variables (as strings)

        /// <summary>Musician name</summary>
        private string m_musician_name = @"";
        /// <summary>Get and set musician name</summary>
        public string MusicianName { get { return m_musician_name; } set { m_musician_name = value; } }

        /// <summary>Musician instrument</summary>
        private string m_musician_instrument = @"";
        /// <summary>Get and set musician instrument</summary>
        public string MusicianInstrument { get { return m_musician_instrument; } set { m_musician_instrument = value; } }

        /// <summary>Musician text</summary>
        private string m_musician_text = @"";
        /// <summary>Get and set musician text</summary>
        public string MusicianText { get { return m_musician_text; } set { m_musician_text = value; } }

        /// <summary>Musician birth year</summary>
        private string m_musician_birth_year_str = @"";
        /// <summary>Get and set musician birth year</summary>
        public string MusicianBirthYear { get { return m_musician_birth_year_str; } set { m_musician_birth_year_str = value; } }

        /// <summary>Musician gender</summary>
        private string m_musician_gender = @"male";
        /// <summary>Get and set musician gender</summary>
        public string MusicianGender { get { return m_musician_gender; } set { m_musician_gender = value; } }

        #endregion // Define, set and get member variables (as strings)

        #region Get and set some member variables as integers

        /// <summary>Get and set musician birth year as integer</summary>
        public int MusicianBirthYearInt { get { return JazzUtils.StringToInt(m_musician_birth_year_str); } set { m_musician_birth_year_str = value.ToString(); } }

        #endregion // Get and set some member variables as integers

        #region Set to empty strings if member parameter values not yet are set

        /// <summary>Set to empty strings if values not yet are set</summary>
        public void SetToEmptyStringsForValuesNotYetSet()
        {
            MusicianName = JazzSeasonUtil.EmptyStringIfValueNotYetSet(MusicianName);
            MusicianInstrument = JazzSeasonUtil.EmptyStringIfValueNotYetSet(MusicianInstrument);
            MusicianText = JazzSeasonUtil.EmptyStringIfValueNotYetSet(MusicianText);
            MusicianGender = JazzSeasonUtil.EmptyStringIfValueNotYetSet(MusicianGender);
            MusicianBirthYear = JazzSeasonUtil.EmptyStringIfValueNotYetSet(MusicianBirthYear);

        } // SetToEmptyStringsForValuesNotYetSet

        #endregion // Set to empty strings "" for member parameter with value 

        #region Set to values not yet set ("NotYetSetNodeValue") for empty values ("") "NotYetSetNodeValue"

        /// <summary>Set to values not yet set ("NotYetSetNodeValue") for member parameters with empty strings ("")
        /// <para>The function JazzSeasonUtil.SetToValueNotYetSetIfEmptyString is called for all member variables</para>
        /// <para>The string "NotYetSetNodeValue" can be retrieved with JazzXml.GetUndefinedNodeValue()</para>
        /// </summary>
        public void SetToValuesNotYetSetForEmptyStrings()
        {
            MusicianName = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(MusicianName);
            MusicianInstrument = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(MusicianInstrument);
            MusicianText = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(MusicianText);
            MusicianGender = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(MusicianGender);
            MusicianBirthYear = JazzSeasonUtil.SetToValueNotYetSetIfEmptyString(MusicianBirthYear);

        } // SetToValuesNotYetSetForEmptyStrings

        #endregion // Set to values not yet set ("NotYetSetNodeValue") for empty values ("")

        #region Check functions

        /// <summary>Check the parameter values</summary>
        public bool CheckParameterValues(out string o_error)
        {
            o_error = @"";
            bool ret_bool = true;

            if (MusicianName.Trim().Length < 1)
            {
                o_error = @"JazzMusician.CheckParameterValues MusicianName string length < 1";
                return false;
            }

            if (MusicianBirthYear.Length != 0)
            {
                if (MusicianBirthYear.Length != 4)
                {
                    o_error = @"JazzMusician.CheckParameterValues Birth year string length is not four (4)";

                    return false;
                }

                if (MusicianBirthYearInt < 1900 || MusicianBirthYearInt > 2050)
                {
                    o_error = @"JazzMusician.CheckParameterValues MusicianBirthYearInt value < 1900 or > 2050 ";

                    return false;
                }
            }

 
            if (MusicianGender.Equals("male") || MusicianGender.Equals("female"))
            {
                ret_bool = true;
            }
            else
            {
                o_error = @"JazzMusician.CheckParameterValues MusicianGender is not equal to male or female";

                ret_bool = false;
            }


            return ret_bool;

        } // CheckParameterValues

        #endregion // Check functions

    } // JazzMusician

    /// <summary>Utility functions primarely for JazzSeason, JazzConcert and JazzMusician</summary>
    public static class JazzSeasonUtil
    {
        /// <summary>Returns empty string if value not yet is set</summary>
        public static string EmptyStringIfValueNotYetSet(string i_value)
        {
            string ret_string = i_value;

            if (!JazzXml.XmlNodeValueIsSet(i_value))
            {
                ret_string = @"";
            }

            return ret_string;

        } // EmptyStringIfValueNotYetSet

        /// <summary>Returns not yet set value if string is empty</summary>
        public static string SetToValueNotYetSetIfEmptyString(string i_value)
        {
            string ret_string = i_value;

            if (i_value.Length == 0)
            {
                ret_string = JazzXml.GetUndefinedNodeValue();
            }

            return ret_string;

        } // SetToValueNotYetSetIfEmptyString

    } //JazzSeasonUtil

} // namespace
