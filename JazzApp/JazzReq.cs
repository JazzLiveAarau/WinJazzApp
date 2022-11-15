using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for a request, i.e. data about a band that has applied for a gig in the jazz club
    /// <para></para>
    /// </summary>
    public class JazzReq
    {
        #region Define, set and get member variables (as strings)

        /// <summary>Request registration number as string</summary>
        private string m_reg_number_str = @"";
        /// <summary>Get and set request registration number as string</summary>
        public string RegNumber { get { return m_reg_number_str; } set { m_reg_number_str = value; } }

        /// <summary>Request registration day as string</summary>
        private string m_reg_day_str = @"";
        /// <summary>Get and set request registration day as string</summary>
        public string RegDay { get { return m_reg_day_str; } set { m_reg_day_str = value; } }

        /// <summary>Request registration month as string</summary>
        private string m_reg_month_str = @"";
        /// <summary>Get and set request registration month as string</summary>
        public string RegMonth { get { return m_reg_month_str; } set { m_reg_month_str = value; } }

        /// <summary>Request registration year as string</summary>
        private string m_reg_year_str = @"";
        /// <summary>Get and set request registration year as string</summary>
        public string RegYear { get { return m_reg_year_str; } set { m_reg_year_str = value; } }

        /// <summary>Band name</summary>
        private string m_band_name = @"";
        /// <summary>Get and set band name</summary>
        public string BandName { get { return m_band_name; } set { m_band_name = value; } }

        /// <summary>Comments about the request</summary>
        private string m_comments = @"";
        /// <summary>Get and set comments about the request</summary>
        public string Comments { get { return m_comments; } set { m_comments = value; } }

        /// <summary>Private notes about the request</summary>
        private string m_private_notes = @"";
        /// <summary>Get and set private notes about the request</summary>
        public string PrivateNotes { get { return m_private_notes; } set { m_private_notes = value; } }

        /// <summary>Website for the band</summary>
        private string m_web_site = @"";
        /// <summary>Get and set website for the band</summary>
        public string BandWebsite { get { return m_web_site; } set { m_web_site = value; } }

        /// <summary>Sound sample URL (often Youtube)</summary>
        private string m_sound_sample = @"";
        /// <summary>Get and set sound sample URL (often Youtube)</summary>
        public string SoundSample { get { return m_sound_sample; } set { m_sound_sample = value; } }

        /// <summary>Server directory one with (mp3) audio files, i.e. normally a CD</summary>
        private string m_audio_one = @"";
        /// <summary>Get and set server directory one with (mp3) audio files, i.e. normally a CD</summary>
        public string AudioOne { get { return m_audio_one; } set { m_audio_one = value; } }

        /// <summary>Server directory two with (mp3) audio files, i.e. normally a CD</summary>
        private string m_audio_two = @"";
        /// <summary>Get and set server directory two with (mp3) audio files, i.e. normally a CD</summary>
        public string AudioTwo { get { return m_audio_two; } set { m_audio_two = value; } }

        /// <summary>Server directory three with (mp3) audio files, i.e. normally a CD</summary>
        private string m_audio_three = @"";
        /// <summary>Get and set server directory three with (mp3) audio files, i.e. normally a CD</summary>
        public string AudioThree { get { return m_audio_three; } set { m_audio_three = value; } }

        /// <summary>The name of the audio one CD</summary>
        private string m_audio_one_cd = @"";
        /// <summary>Get and set the name of the audio one CD</summary>
        public string AudioOneCd { get { return m_audio_one_cd; } set { m_audio_one_cd = value; } }

        /// <summary>The name of the audio two CD</summary>
        private string m_audio_two_cd = @"";
        /// <summary>Get and set the name of the audio two CD</summary>
        public string AudioTwoCd { get { return m_audio_two_cd; } set { m_audio_two_cd = value; } }

        /// <summary>The name of the audio three CD</summary>
        private string m_audio_three_cd = @"";
        /// <summary>Get and set the name of the audio three CD</summary>
        public string AudioThreeCd { get { return m_audio_three_cd; } set { m_audio_three_cd = value; } }

        /// <summary>Flag telling if the request/band shall be evaluated (at the next meeting)</summary>
        private string m_to_be_evaluated = @"true";
        /// <summary>Get and set the flag telling if the request/band shall be evaluated (at the next meeting)</summary>
        public string ToBeEvaluated { get { return m_to_be_evaluated; } set { m_to_be_evaluated = value; } }

        /// <summary>Info one file name</summary>
        private string m_info_one = @"";
        /// <summary>Get and set the info one file name</summary>
        public string InfoOne { get { return m_info_one; } set { m_info_one = value; } }

        /// <summary>Info two file name</summary>
        private string m_info_two = @"";
        /// <summary>Get and set the info two file name</summary>
        public string InfoTwo { get { return m_info_two; } set { m_info_two = value; } }

        /// <summary>Info three file name</summary>
        private string m_info_three = @"";
        /// <summary>Get and set the info three file name</summary>
        public string InfoThree { get { return m_info_three; } set { m_info_three = value; } }

        /// <summary>Link 1</summary>
        private string m_link_one = @"";
        /// <summary>Get and set link 1</summary>
        public string LinkOne { get { return m_link_one; } set { m_link_one = value; } }

        /// <summary>Link 2</summary>
        private string m_link_two = @"";
        /// <summary>Get and set link 2</summary>
        public string LinkTwo { get { return m_link_two; } set { m_link_two = value; } }

        /// <summary>Link 3</summary>
        private string m_link_three = @"";
        /// <summary>Get and set link 3</summary>
        public string LinkThree { get { return m_link_three; } set { m_link_three = value; } }

        /// <summary>Link 4</summary>
        private string m_link_four = @"";
        /// <summary>Get and set link 4</summary>
        public string LinkFour { get { return m_link_four; } set { m_link_four = value; } }

        /// <summary>Link 5</summary>
        private string m_link_five = @"";
        /// <summary>Get and set link 5</summary>
        public string LinkFive { get { return m_link_five; } set { m_link_five = value; } }

        /// <summary>Link 6</summary>
        private string m_link_six = @"";
        /// <summary>Get and set link 6</summary>
        public string LinkSix { get { return m_link_six; } set { m_link_six = value; } }

        /// <summary>Link 7</summary>
        private string m_link_seven = @"";
        /// <summary>Get and set link 7</summary>
        public string LinkSeven { get { return m_link_seven; } set { m_link_seven = value; } }

        /// <summary>Link 8</summary>
        private string m_link_eight = @"";
        /// <summary>Get and set link 8</summary>
        public string LinkEight { get { return m_link_eight; } set { m_link_eight = value; } }

        /// <summary>Link 9</summary>
        private string m_link_nine = @"";
        /// <summary>Get and set link 9</summary>
        public string LinkNine { get { return m_link_nine; } set { m_link_nine = value; } }

        /// <summary>Link type 1</summary>
        private string m_link_type_one = @"";
        /// <summary>Get and set link type 1</summary>
        public string LinkTypeOne { get { return m_link_type_one; } set { m_link_type_one = value; } }

        /// <summary>Link type 2</summary>
        private string m_link_type_two = @"";
        /// <summary>Get and set link type 2</summary>
        public string LinkTypeTwo { get { return m_link_type_two; } set { m_link_type_two = value; } }

        /// <summary>Link type 3</summary>
        private string m_link_type_three = @"";
        /// <summary>Get and set link type 3</summary>
        public string LinkTypeThree { get { return m_link_type_three; } set { m_link_type_three = value; } }

        /// <summary>Link type 4</summary>
        private string m_link_type_four = @"";
        /// <summary>Get and set link type 4</summary>
        public string LinkTypeFour { get { return m_link_type_four; } set { m_link_type_four = value; } }

        /// <summary>Link type 5</summary>
        private string m_link_type_five = @"";
        /// <summary>Get and set link type 5</summary>
        public string LinkTypeFive { get { return m_link_type_five; } set { m_link_type_five = value; } }

        /// <summary>Link type 6</summary>
        private string m_link_type_six = @"";
        /// <summary>Get and set link type 6</summary>
        public string LinkTypeSix { get { return m_link_type_six; } set { m_link_type_six = value; } }

        /// <summary>Link type 7</summary>
        private string m_link_type_seven = @"";
        /// <summary>Get and set link type 7</summary>
        public string LinkTypeSeven { get { return m_link_type_seven; } set { m_link_type_seven = value; } }

        /// <summary>Link type 8</summary>
        private string m_link_type_eight = @"";
        /// <summary>Get and set link type 8</summary>
        public string LinkTypeEight { get { return m_link_type_eight; } set { m_link_type_eight = value; } }

        /// <summary>Link type 9</summary>
        private string m_link_type_nine = @"";
        /// <summary>Get and set link type 9</summary>
        public string LinkTypeNine { get { return m_link_type_nine; } set { m_link_type_nine = value; } }

        /// <summary>Link text 1</summary>
        private string m_link_text_one = @"";
        /// <summary>Get and set Link text 1</summary>
        public string LinkTextOne { get { return m_link_text_one; } set { m_link_text_one = value; } }

        /// <summary>Link text 2</summary>
        private string m_link_text_two = @"";
        /// <summary>Get and set Link text 2</summary>
        public string LinkTextTwo { get { return m_link_text_two; } set { m_link_text_two = value; } }

        /// <summary>Link text 3</summary>
        private string m_link_text_three = @"";
        /// <summary>Get and set Link text 3</summary>
        public string LinkTextThree { get { return m_link_text_three; } set { m_link_text_three = value; } }

        /// <summary>Link text 4</summary>
        private string m_link_text_four = @"";
        /// <summary>Get and set Link text 4</summary>
        public string LinkTextFour { get { return m_link_text_four; } set { m_link_text_four = value; } }

        /// <summary>Link text 5</summary>
        private string m_link_text_five = @"";
        /// <summary>Get and set Link text 5</summary>
        public string LinkTextFive { get { return m_link_text_five; } set { m_link_text_five = value; } }

        /// <summary>Link text 6</summary>
        private string m_link_text_six = @"";
        /// <summary>Get and set Link text 6</summary>
        public string LinkTextSix { get { return m_link_text_six; } set { m_link_text_six = value; } }

        /// <summary>Link text 7</summary>
        private string m_link_text_seven = @"";
        /// <summary>Get and set Link text 7</summary>
        public string LinkTextSeven { get { return m_link_text_seven; } set { m_link_text_seven = value; } }

        /// <summary>Link text 8</summary>
        private string m_link_text_eight = @"";
        /// <summary>Get and set Link text 8</summary>
        public string LinkTextEight { get { return m_link_text_eight; } set { m_link_text_eight = value; } }

        /// <summary>Link text 9</summary>
        private string m_link_text_nine = @"";
        /// <summary>Get and set Link text 9</summary>
        public string LinkTextNine { get { return m_link_text_nine; } set { m_link_text_nine = value; } }

        /// <summary>Photo 1</summary>
        private string m_photo_one = @"";
        /// <summary>Get and set photo 1</summary>
        public string PhotoOne { get { return m_photo_one; } set { m_photo_one = value; } }

        /// <summary>Photo 2</summary>
        private string m_photo_two = @"";
        /// <summary>Get and set photo 2</summary>
        public string PhotoTwo { get { return m_photo_two; } set { m_photo_two = value; } }

        /// <summary>Photo 3</summary>
        private string m_photo_three = @"";
        /// <summary>Get and set photo 3</summary>
        public string PhotoThree { get { return m_photo_three; } set { m_photo_three = value; } }

        /// <summary>Photo 4</summary>
        private string m_photo_four = @"";
        /// <summary>Get and set photo 4</summary>
        public string PhotoFour { get { return m_photo_four; } set { m_photo_four = value; } }

        /// <summary>Photo 5</summary>
        private string m_photo_five = @"";
        /// <summary>Get and set photo 5</summary>
        public string PhotoFive { get { return m_photo_five; } set { m_photo_five = value; } }

        /// <summary>Photo 6</summary>
        private string m_photo_six = @"";
        /// <summary>Get and set photo 6</summary>
        public string PhotoSix { get { return m_photo_six; } set { m_photo_six = value; } }

        /// <summary>Photo 7</summary>
        private string m_photo_seven = @"";
        /// <summary>Get and set photo 7</summary>
        public string PhotoSeven { get { return m_photo_seven; } set { m_photo_seven = value; } }

        /// <summary>Photo 8</summary>
        private string m_photo_eight = @"";
        /// <summary>Get and set photo 8</summary>
        public string PhotoEight { get { return m_photo_eight; } set { m_photo_eight = value; } }

        /// <summary>Photo 9</summary>
        private string m_photo_nine = @"";
        /// <summary>Get and set photo 9</summary>
        public string PhotoNine { get { return m_photo_nine; } set { m_photo_nine = value; } }

        /// <summary>Concert number</summary>
        private string m_concert_number = @"";
        /// <summary>Get and set concert number</summary>
        public string ConcertNumber { get { return m_concert_number; } set { m_concert_number = value; } }


        #endregion // Define, set and get member variables (as strings)

        #region Get and set some member variables as integers and booleans

        /// <summary>Get and set request registration number as integer</summary>
        public int RegNumberInt { get { return JazzUtils.StringToInt(RegNumber); } set { m_reg_number_str = value.ToString(); } }

        /// <summary>Get and set request registration day as integer</summary>
        public int RegDayInt { get { return JazzUtils.StringToInt(m_reg_day_str); } set { m_reg_day_str = value.ToString(); } }

        /// <summary>Get and set request registration month as integer</summary>
        public int RegMonthInt { get { return JazzUtils.StringToInt(m_reg_month_str); } set { m_reg_month_str = value.ToString(); } }

        /// <summary>Get and set request registration year as integer</summary>
        public int RegYearInt { get { return JazzUtils.StringToInt(m_reg_year_str); } set { m_reg_year_str = value.ToString(); } }

        /// <summary>Get and set concert number as integer</summary>
        public int ConcertNumberInt
        {
            get
            {
                if (ConcertNumber.Length == 0)
                    return 0;
                return JazzUtils.StringToInt(ConcertNumber);
            }
            set
            {
                m_concert_number = value.ToString();
            }
        } // ConcertNumberInt

        /// <summary>Get and set link type 1 as integer</summary>
        public int LinkTypeOneInt { get { return JazzUtils.StringToInt(LinkTypeOne); } set { m_link_type_one = value.ToString(); } }

        /// <summary>Get and set link type 2 as integer</summary>
        public int LinkTypeTwoInt { get { return JazzUtils.StringToInt(LinkTypeTwo); } set { m_link_type_two = value.ToString(); } }

        /// <summary>Get and set link type 3 as integer</summary>
        public int LinkTypeThreeInt { get { return JazzUtils.StringToInt(LinkTypeThree); } set { m_link_type_three = value.ToString(); } }

        /// <summary>Get and set link type 4 as integer</summary>
        public int LinkTypeFourInt { get { return JazzUtils.StringToInt(LinkTypeFour); } set { m_link_type_four = value.ToString(); } }

        /// <summary>Get and set link type 5 as integer</summary>
        public int LinkTypeFiveInt { get { return JazzUtils.StringToInt(LinkTypeFive); } set { m_link_type_five = value.ToString(); } }

        /// <summary>Get and set link type 6 as integer</summary>
        public int LinkTypeSixInt { get { return JazzUtils.StringToInt(LinkTypeSix); } set { m_link_type_six = value.ToString(); } }

        /// <summary>Get and set link type 7 as integer</summary>
        public int LinkTypeSevenInt { get { return JazzUtils.StringToInt(LinkTypeSeven); } set { m_link_type_seven = value.ToString(); } }

        /// <summary>Get and set link type 8 as integer</summary>
        public int LinkTypeEightInt { get { return JazzUtils.StringToInt(LinkTypeEight); } set { m_link_type_eight = value.ToString(); } }

        /// <summary>Get and set link type 9 as integer</summary>
        public int LinkTypeNineInt { get { return JazzUtils.StringToInt(LinkTypeNine); } set { m_link_type_nine = value.ToString(); } }

        /// <summary>Get and set the evaluation flag as boolean</summary>
        public bool ToBeEvaluatedBoolean {
            get
            {
                if (ToBeEvaluated.Equals("false"))
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    m_to_be_evaluated = "true";
                else
                    m_to_be_evaluated = "false";
            }
        } // ToBeEvaluatedBoolean

        #endregion // Get and set some member variables as integers and booleans

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
            Comments = EmptyStringIfValueNotYetSet(Comments);
            PrivateNotes = EmptyStringIfValueNotYetSet(PrivateNotes);
            BandWebsite = EmptyStringIfValueNotYetSet(BandWebsite);
            SoundSample = EmptyStringIfValueNotYetSet(SoundSample);
            AudioOne = EmptyStringIfValueNotYetSet(AudioOne);
            AudioTwo = EmptyStringIfValueNotYetSet(AudioTwo);
            AudioThree = EmptyStringIfValueNotYetSet(AudioThree);
            AudioOneCd = EmptyStringIfValueNotYetSet(AudioOneCd);
            AudioTwoCd = EmptyStringIfValueNotYetSet(AudioTwoCd);
            AudioThreeCd = EmptyStringIfValueNotYetSet(AudioThreeCd);
            InfoOne = EmptyStringIfValueNotYetSet(InfoOne);
            InfoTwo = EmptyStringIfValueNotYetSet(InfoTwo);
            InfoThree = EmptyStringIfValueNotYetSet(InfoThree);

            LinkOne = EmptyStringIfValueNotYetSet(LinkOne);
            LinkTwo = EmptyStringIfValueNotYetSet(LinkTwo);
            LinkThree = EmptyStringIfValueNotYetSet(LinkThree);
            LinkFour = EmptyStringIfValueNotYetSet(LinkFour);
            LinkFive = EmptyStringIfValueNotYetSet(LinkFive);
            LinkSix = EmptyStringIfValueNotYetSet(LinkSix);
            LinkSeven = EmptyStringIfValueNotYetSet(LinkSeven);
            LinkEight = EmptyStringIfValueNotYetSet(LinkEight);
            LinkNine = EmptyStringIfValueNotYetSet(LinkNine);

            LinkTypeOne = EmptyStringIfValueNotYetSet(LinkTypeOne);
            LinkTypeTwo = EmptyStringIfValueNotYetSet(LinkTypeTwo);
            LinkTypeThree = EmptyStringIfValueNotYetSet(LinkTypeThree);
            LinkTypeFour = EmptyStringIfValueNotYetSet(LinkTypeFour);
            LinkTypeFive = EmptyStringIfValueNotYetSet(LinkTypeFive);
            LinkTypeSix = EmptyStringIfValueNotYetSet(LinkTypeSix);
            LinkTypeSeven = EmptyStringIfValueNotYetSet(LinkTypeSeven);
            LinkTypeEight = EmptyStringIfValueNotYetSet(LinkTypeEight);
            LinkTypeNine = EmptyStringIfValueNotYetSet(LinkTypeNine);

            LinkTextOne = EmptyStringIfValueNotYetSet(LinkTextOne);
            LinkTextTwo = EmptyStringIfValueNotYetSet(LinkTextTwo);
            LinkTextThree = EmptyStringIfValueNotYetSet(LinkTextThree);
            LinkTextFour = EmptyStringIfValueNotYetSet(LinkTextFour);
            LinkTextFive = EmptyStringIfValueNotYetSet(LinkTextFive);
            LinkTextSix = EmptyStringIfValueNotYetSet(LinkTextSix);
            LinkTextSeven = EmptyStringIfValueNotYetSet(LinkTextSeven);
            LinkTextEight = EmptyStringIfValueNotYetSet(LinkTextEight);
            LinkTextNine = EmptyStringIfValueNotYetSet(LinkTextNine);

            PhotoOne = EmptyStringIfValueNotYetSet(PhotoOne);
            PhotoTwo = EmptyStringIfValueNotYetSet(PhotoTwo);
            PhotoThree = EmptyStringIfValueNotYetSet(PhotoThree);
            PhotoFour = EmptyStringIfValueNotYetSet(PhotoFour);
            PhotoFive = EmptyStringIfValueNotYetSet(PhotoFive);
            PhotoSix = EmptyStringIfValueNotYetSet(PhotoSix);
            PhotoSeven = EmptyStringIfValueNotYetSet(PhotoSeven);
            PhotoEight = EmptyStringIfValueNotYetSet(PhotoEight);
            PhotoNine = EmptyStringIfValueNotYetSet(PhotoNine);

            ConcertNumber = EmptyStringIfValueNotYetSet(ConcertNumber);

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

        /// <summary>Returns a JazzReq object where empty strings have been replaced by JazzXml.GetUndefinedNodeValue()</summary>
        public JazzReq GetObjectWithUndefinedNodeValues(JazzReq i_jazz_req)
        {
            JazzReq ret_jazz_req = i_jazz_req;

            Comments = SetToValueNotYetSetIfEmptyString(Comments);
            PrivateNotes = SetToValueNotYetSetIfEmptyString(PrivateNotes);
            BandWebsite = SetToValueNotYetSetIfEmptyString(BandWebsite);
            SoundSample = SetToValueNotYetSetIfEmptyString(SoundSample);
            AudioOne = SetToValueNotYetSetIfEmptyString(AudioOne);
            AudioTwo = SetToValueNotYetSetIfEmptyString(AudioTwo);
            AudioThree = SetToValueNotYetSetIfEmptyString(AudioThree);
            AudioOneCd = SetToValueNotYetSetIfEmptyString(AudioOneCd);
            AudioTwoCd = SetToValueNotYetSetIfEmptyString(AudioTwoCd);
            AudioThreeCd = SetToValueNotYetSetIfEmptyString(AudioThreeCd);
            InfoOne = SetToValueNotYetSetIfEmptyString(InfoOne);
            InfoTwo = SetToValueNotYetSetIfEmptyString(InfoTwo);
            InfoThree = SetToValueNotYetSetIfEmptyString(InfoThree);

            LinkOne = SetToValueNotYetSetIfEmptyString(LinkOne);
            LinkTwo = SetToValueNotYetSetIfEmptyString(LinkTwo);
            LinkThree = SetToValueNotYetSetIfEmptyString(LinkThree);
            LinkFour = SetToValueNotYetSetIfEmptyString(LinkFour);
            LinkFive = SetToValueNotYetSetIfEmptyString(LinkFive);
            LinkSix = SetToValueNotYetSetIfEmptyString(LinkSix);
            LinkSeven = SetToValueNotYetSetIfEmptyString(LinkSeven);
            LinkEight = SetToValueNotYetSetIfEmptyString(LinkEight);
            LinkNine = SetToValueNotYetSetIfEmptyString(LinkNine);

            LinkTypeOne = SetToValueNotYetSetIfEmptyString(LinkTypeOne);
            LinkTypeTwo = SetToValueNotYetSetIfEmptyString(LinkTypeTwo);
            LinkTypeThree = SetToValueNotYetSetIfEmptyString(LinkTypeThree);
            LinkTypeFour = SetToValueNotYetSetIfEmptyString(LinkTypeFour);
            LinkTypeFive = SetToValueNotYetSetIfEmptyString(LinkTypeFive);
            LinkTypeSix = SetToValueNotYetSetIfEmptyString(LinkTypeSix);
            LinkTypeSeven = SetToValueNotYetSetIfEmptyString(LinkTypeSeven);
            LinkTypeEight = SetToValueNotYetSetIfEmptyString(LinkTypeEight);
            LinkTypeNine = SetToValueNotYetSetIfEmptyString(LinkTypeNine);

            LinkTextOne = SetToValueNotYetSetIfEmptyString(LinkTextOne);
            LinkTextTwo = SetToValueNotYetSetIfEmptyString(LinkTextTwo);
            LinkTextThree = SetToValueNotYetSetIfEmptyString(LinkTextThree);
            LinkTextFour = SetToValueNotYetSetIfEmptyString(LinkTextFour);
            LinkTextFive = SetToValueNotYetSetIfEmptyString(LinkTextFive);
            LinkTextSix = SetToValueNotYetSetIfEmptyString(LinkTextSix);
            LinkTextSeven = SetToValueNotYetSetIfEmptyString(LinkTextSeven);
            LinkTextEight = SetToValueNotYetSetIfEmptyString(LinkTextEight);
            LinkTextNine = SetToValueNotYetSetIfEmptyString(LinkTextNine);

            PhotoOne = SetToValueNotYetSetIfEmptyString(PhotoOne);
            PhotoTwo = SetToValueNotYetSetIfEmptyString(PhotoTwo);
            PhotoThree = SetToValueNotYetSetIfEmptyString(PhotoThree);
            PhotoFour = SetToValueNotYetSetIfEmptyString(PhotoFour);
            PhotoFive = SetToValueNotYetSetIfEmptyString(PhotoFive);
            PhotoSix = SetToValueNotYetSetIfEmptyString(PhotoSix);
            PhotoSeven = SetToValueNotYetSetIfEmptyString(PhotoSeven);
            PhotoEight = SetToValueNotYetSetIfEmptyString(PhotoEight);
            PhotoNine = SetToValueNotYetSetIfEmptyString(PhotoNine);

            ConcertNumber = SetToValueNotYetSetIfEmptyString(ConcertNumber);


            return ret_jazz_req;

        } // GetObjectWithUndefinedNodeValues

        #endregion // Set to empty strings if member parameter values not yet are set

        #region Check parameter values

        /// <summary>Check the parameter values</summary>
        public bool CheckParameterValues(out string o_error)
        {
            o_error = @"";
            bool ret_bool = true;

            if (RegNumber.Length < 1)
            {
                o_error = @"JazzReq.CheckParameterValues RegNumber string length < 1";
                return false;
            }

            if (RegNumberInt < 1)
            {
                o_error = @"JazzReq.CheckParameterValues RegNumberInt value < 1 ";
                return false;
            }

            if (BandName.Trim().Length < 1)
            {
                o_error = @"JazzReq.CheckParameterValues BandName string length < 1";
                return false;
            }

            if (RegDay.Length < 1 || RegDay.Length > 2)
            {
                o_error = @"JazzReq.CheckParameterValues RegDay string length < 1 or > 2";
                return false;
            }

            if (RegDayInt < 1 || RegDayInt > 31)
            {
                o_error = @"JazzReq.CheckParameterValues RegDayInt value < 1 or > 31 ";
                return false;
            }

            if (RegMonth.Length < 1 || RegMonth.Length > 2)
            {
                o_error = @"JazzReq.CheckParameterValues RegMonth string length < 1 or > 2";
                return false;
            }

            if (RegMonthInt < 1 || RegMonthInt > 12)
            {
                o_error = @"JazzReq.CheckParameterValues RegMonthInt value < 1 or > 12 ";
                return false;
            }

            if (RegYear.Length != 4)
            {
                o_error = @"JazzReq.CheckParameterValues RegYear string length is not equal to four (4)";
                return false;
            }

            if (RegYearInt < 2017)
            {
                o_error = @"JazzReq.CheckParameterValues RegYearInt value < 2017 ";
                return false;
            }

            if (ToBeEvaluated.Equals("false") || ToBeEvaluated.Equals("true"))
            {
                ret_bool = true;
            }
            else
            {
                o_error = @"JazzReq.CheckParameterValues ToBeEvaluated is not false or true but " + ToBeEvaluated;
                return false;
            }

            return ret_bool;

        } // CheckParameterValues

        #endregion // Check parameter values

        #region Strings for registration number and date

        /// <summary>Returns the registration number as a name that for instance can be used for a file name (for private notes).</summary>
        public string RegNumberName()
        {
            string reg_number_name = @"REG";

            if (RegNumber.Length == 1)
            {
                reg_number_name = reg_number_name + @"0000";
            }
            else if (RegNumber.Length == 2)
            {
                reg_number_name = reg_number_name + @"000";
            }
            else if (RegNumber.Length == 3)
            {
                reg_number_name = reg_number_name + @"00";
            }
            else if (RegNumber.Length == 4)
            {
                reg_number_name = reg_number_name + @"0";
            }

            reg_number_name = reg_number_name + RegNumber;

            return reg_number_name;
        } // RegNumberName

        /// <summary>Returns the registration date as a string (year-month-day).</summary>
        public string RegDate()
        {
            return RegYear + "-" + DateIntToString(RegMonthInt) + "-" + DateIntToString(RegDayInt);
        } // RegDate

        /// <summary>Returns date and time as a string with a '0' added if input number is less that ten (10)</summary>
        private static string DateIntToString(int i_int)
        {
            string time_text = i_int.ToString();

            if (i_int <= 9)
            {
                time_text = "0" + time_text;
            }

            return time_text;

        }  // DateIntToString

        #endregion // Strings for registration number and date

    } // JazzReq

} // namespace
