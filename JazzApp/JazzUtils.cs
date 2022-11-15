using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace JazzApp
{
    /// <summary>Utility functions for the jazz application.
    /// <para>Holds also the static variables member and musician login flags</para>
    /// <para>Functions in this partial class are used for the mobile app application and the Windows JazzAdmin application</para>
    /// <para>The OS specific functions (see this region) must be defined separately also for the JazzAdmin application</para>
    /// </summary>
    static public partial class JazzUtils
    {
        #region Member variables
        /// <summary>Flag telling if a member is logged in</summary>
        private static Boolean m_member_login = false;

        /// <summary>Flag telling if a musician is logged in</summary>
        private static Boolean m_musician_login = false;

        /// <summary>The app finds all season XMLs but loads only XMLs that are from current year plus two (2) years</summary>
        private static int m_number_of_possible_xmls_after_current_year = 2;

        /// <summary>Returns the number of new season programs that may be added</summary>
        public static int MaxNumberOfNewSeasonPrograms { get { return m_number_of_possible_xmls_after_current_year; } }

        /// <summary>Caption string season. TODO Consider adding to JazzApplication.xml</summary>
        private static String m_caption_season = "Saison";

        /// <summary>Returns true if a member is logged in</summary>
        public static Boolean GetMemberLogin() { return m_member_login; }

        /// <summary>Set flag member login</summary>
        public static void SetMemberLogin(Boolean i_member_login) { m_member_login = i_member_login; }

        /// <summary>Returns true if a musician is logged in</summary>
        public static Boolean GetMusicianLogin() { return m_musician_login; }

        /// <summary>Set flag member login</summary>
        public static void SetMusicianLogin(Boolean i_musician_login) { m_musician_login = i_musician_login; }
        #endregion // Member variables

        #region Member login/logout

        #region OS specific functions

        /// <summary>Returns true if an http file exists</summary>
        public static Boolean FileExists(String i_filename_url)
        {
            return JazzOsUtils.RemoteFileExists(i_filename_url);
        }

        #endregion // OS specific functions

        /// <summary>Sets the member and musician login flag to true if user name exists and password is OK</summary>
        public static Boolean MemberLogin(String i_member_name, String i_member_password, out String o_error_message)
        {
            Boolean ret_status = true;
            o_error_message = "";

            if (m_member_login)
            {
                ret_status = false;
                o_error_message = "JazzUtils.MemberLogin: Member already logged in";
                return ret_status;
            }

            String[] member_names = JazzXml.GetMemberPropertyArray("Name");
            String[] member_passwords = JazzXml.GetMemberPropertyArray("Password");
            if (null == member_names || null == member_names)
            {
                ret_status = false;
                o_error_message = "JazzUtils.MemberLogin: Programming error";
                return ret_status;
            }

            for (int i_member = 0; i_member < member_names.Length; i_member++)
            {
                String member_name = member_names[i_member];
                String member_password = member_passwords[i_member];

                if (i_member_name.Equals(member_name))
                {
                    if (i_member_password.Equals(member_password))
                    {
                        m_member_login = true;
                        m_musician_login = true;

                        break;
                    }
                    else
                    {
                        ret_status = false;
                        o_error_message = JazzXml.GetLoginFailed();
                        break;
                    }
                }

                if (member_names.Length - 1 == i_member)
                {
                    // Input member name does not exist
                    ret_status = false;
                    o_error_message = JazzXml.GetLoginFailed(); // TODO Add error message for this case in JazzApplication.xml
                }

            } // i_member

            return ret_status;

        } // MemberLogin

        /// <summary>Sets the musician login flag to true if the password is OK</summary>
        public static Boolean MusicianLogin(String i_member_password, out String o_error_message)
        {
            Boolean ret_status = true;
            o_error_message = "";

            if (m_musician_login)
            {
                ret_status = false;
                o_error_message = "MusicianLogin.MemberLogin: Musician is already logged in";
                return ret_status;
            }

            if (!JazzUtils.LoginMusicianPossible(out o_error_message))
            {
                ret_status = false;
                return ret_status;
            }

            int number_concerts = JazzXml.GetNumberConcertsInCurrentDocument();

            for (int i_concert = 1; i_concert <= number_concerts; i_concert++)
            {
                String band_password = JazzXml.GetLoginPassword(i_concert);

                if (JazzXml.XmlNodeValueIsSet(band_password))
                {
                    if (i_member_password.Equals(band_password))
                    {
                        m_musician_login = true;
                        break;
                    }
                }

            } // i_concert

            if (!m_musician_login)
            {
                ret_status = false;
                o_error_message = JazzXml.GetLoginMusicianFailed();
            }

            return ret_status;

        } // MusicianLogin

        /// <summary>Returns true if login is possible for a musician, i.e. current season or next season is set</summary>
        public static Boolean LoginMusicianPossible(out String o_error)
        {
            Boolean ret_login_musician_possible = false;
            o_error = "";

            XDocument enter_activity_season_document = JazzXml.GetDocumentCurrent();
            if (null == enter_activity_season_document)
            {
                ret_login_musician_possible = false;
                o_error = "JazzUtils.LoginMusicianPossible  JazzXml.GetDocumentCurrent() is null. Programming error";
                return ret_login_musician_possible;
            }

            int current_season_start_year = JazzUtils.GetCurrentSeasonStartYear();

            int autumn_year = JazzXml.GetYearAutumnInt();

            if (autumn_year >= current_season_start_year)
                ret_login_musician_possible = true;

            if (!ret_login_musician_possible)
            {
                o_error = JazzXml.GetMsgLoginOnlySeason() +
                    current_season_start_year.ToString() + "-" + (current_season_start_year + 1).ToString() + " and " +
                    (current_season_start_year + 1).ToString() + "-" + (current_season_start_year + 2).ToString();
            }

            return ret_login_musician_possible;
        } // LoginMusicianPossible

        #endregion // Member login/logout

        /// <summary>Returns a string that is the current season name.</summary>
        public static String GetCurrentSeasonName()
        {
            // TODO Should perhaps check that there is a non-corrupt XML 
            // on the server corresponding to the returned season name
            return SeasonName(GetCurrentSeasonStartYear());
        } // GetCurrentSeasonName

        /// <summary>Returns a string array of season names: "Saison 1996-1997", "Saison 1997-1998",  ...
        /// <para>There is an XML file on the server for each season name</para>
        /// </summary>
        /// <param name="i_start_years">Array of start years</param>
        public static String[] GetSeasonNamesForExistingXmlFiles(int[] i_start_years)
        {
            String[] ret_strings = null;

            if (i_start_years.Length == 0)
            {
                return ret_strings; // null
            }

            ret_strings = new String[i_start_years.Length];

            for (int i_str = 0; i_str < i_start_years.Length; i_str++)
            {
                ret_strings[i_str] = SeasonName(i_start_years[i_str]);
            }

            return ret_strings;

        } // GetSeasonNamesForExistingXmlFiles

        /// <summary>Returns season name/summary>
        /// <param name="i_start_year">Start year</param>
        public static String SeasonName(int i_start_year)
        {
            return m_caption_season + " " + i_start_year.ToString() + "-" + (i_start_year + 1).ToString();
        } // SeasonName

        /// <summary>Returns an int array of start years for existing XML season program files (JazzProgramm_20XX_20YY.xml) on the server
        /// <para>There is a limit of files after current year (m_number_of_possible_xmls_after_current_year)</para>
        /// <para>Start years without a corresponding non-corrupt XML file on the server are removed</para>
        /// </summary>
        public static int[] GetSeasonStartYearsForExistingXmlFiles()
        {
            int[] ret_start_years = null;

            int start_year_club = 1996;
            int current_end_year = GetCurrentSeasonStartYear() + 1;
            int size_possible_years = current_end_year - start_year_club + MaxNumberOfNewSeasonPrograms;

            int[] possible_years = new int[size_possible_years];

            for (int i_init = 0; i_init < size_possible_years; i_init++)
            {
                possible_years[i_init] = 1996 + i_init;
            }

            possible_years = keepOnlySixYears(possible_years);

            int n_exists = 0;
            for (int i_exist = 0; i_exist < possible_years.Length; i_exist++)
            {
                int test_year = possible_years[i_exist];

                String url_season_file_name = JazzXml.GetSeasonFileName(test_year);

                Boolean b_exists = FileExists(url_season_file_name);

                if (b_exists)
                    n_exists = n_exists + 1;
            }

            if (n_exists == 0)
                return ret_start_years;

            ret_start_years = new int[n_exists];

            int n_exists_add = 0;
            for (int i_add = 0; i_add < possible_years.Length; i_add++)
            {
                int test_year_add = possible_years[i_add];

                String url_season_file_name = JazzXml.GetSeasonFileName(test_year_add);

                Boolean b_exists_add = FileExists(url_season_file_name);

                if (b_exists_add)
                {
                    n_exists_add = n_exists_add + 1;
                    if (n_exists_add - 1 < ret_start_years.Length) // Programming check
                    {
                        ret_start_years[n_exists_add - 1] = test_year_add;
                    }
                    else
                        return null;
                }
            }

            return ret_start_years;

        } // GetSeasonStartYearsForExistingXmlFiles

        /// <summary>
        /// The loading time for the XML files became a problem. With this limitation 
        /// it is no longer possible to edit old season programs. This is however not 
        /// such a big problem. There have been changes made (for example for photos)
        /// but these changes were made with a text editor. 2022-05-25
        /// </summary>
        /// <param name="i_possible_years"></param>
        /// <returns>Array with six the last years</returns>
        static private int[] keepOnlySixYears(int[] i_possible_years)
        {
            int[] ret_possible_years = new int[6];

            int n_possible = i_possible_years.Length;

            int start_index = n_possible - 6;

            int index_out = 0;

            for (int index_part = start_index; index_part < n_possible; index_part++)
            {
                ret_possible_years[index_out] = i_possible_years[index_part];

                index_out = index_out + 1;
            }

            return ret_possible_years;

        } // keepOnlySixYears

        /// <summary>Returns the start year for the inout season name
        /// <para></para>
        /// </summary>
        static public int GetSeasonStartYear(string i_season_name, out string o_error)
        {
            o_error = @"";
            int ret_start_year = -12345;

            int[] all_start_years = GetSeasonStartYearsForExistingXmlFiles();
            if (all_start_years == null)
            {
                o_error = @"JazzUtils.GetSeasonStartYear GetSeasonStartYearsForExistingXmlFiles failed"; 
                return -99;
            }

            for (int index_year = 0; index_year < all_start_years.Length; index_year++)
            {
                int current_start_year = all_start_years[index_year];

                string curren_season_name = SeasonName(current_start_year);

                if (curren_season_name.Equals(i_season_name))
                {
                    ret_start_year = current_start_year;
                    return ret_start_year;
                }

            } // index_year


            o_error = @"JazzUtils.GetSeasonStartYear failure finding start season year for i_season_name= " + i_season_name;
            ret_start_year = -98;

            return ret_start_year;

        } // GetSeasonStartYear


        /// <summary>Returns true if the set current season program is for the current season
        /// <para> Current season program can be set with function JazzXml.SetCurrentSeasonDocument</para>
        /// <para> The current (ongoing) season is defined by the current date</para>
        /// <para> This function returns true if JazzXml get functions (e.g. bandname) returns values for the ongoing season</para>
        /// </summary>
        static public bool IsSetSeasonProgramForCurrentSeason()
        {
            int current_season_start_year = JazzUtils.GetCurrentSeasonStartYear();

            int autumn_year = JazzXml.GetYearAutumnInt();

            if (autumn_year == current_season_start_year)
                return true;
            else
                return false;

        } // IsSetSeasonProgramForCurrentSeason

 
        /// <summary>Returns the start year of the current season.
        /// <para> Change of season is the first of May</para>
        /// </summary>
        static public int GetCurrentSeasonStartYear()
        {
            int ret_year = -12345;

            DateTime current_time = DateTime.Now;
            int current_year = current_time.Year;
            int current_month = current_time.Month;

            if (current_month < 4)
            {
                ret_year = current_year - 1;
            }
            else
            {
                ret_year = current_year;
            }

            return ret_year;

        } // GetCurrentSeasonStartYear

        /// <summary>Returns the current year</summary>
        static public int GetCurrentYear()
        {
            DateTime current_time = DateTime.Now;
            int ret_year = current_time.Year;

            return ret_year;

        } // GetCurrentYear

        /// <summary>Returns true if a concert is played</summary>
        static public Boolean ConcertIsPlayed(int i_concert)
        {
            Boolean ret_played = false;

            DateTime current_time = DateTime.Now;
            int current_year = current_time.Year;
            int current_month = current_time.Month;
            int current_day = current_time.Day;

            int concert_year = JazzXml.GetYearInt(i_concert);
            int concert_month = JazzXml.GetMonthInt(i_concert);
            int concert_day = JazzXml.GetDayInt(i_concert);

            if (concert_year < current_year)
                ret_played = true;
            else if (concert_month < current_month && concert_year == current_year)
                ret_played = true;
            else if (concert_day < current_day && concert_year == current_year && concert_month == current_month)
                ret_played = true;

            return ret_played;

        } // ConcertIsPlayed

        /// <summary>Convert string to integer</summary>
        public static int StringToInt(String i_number_str)
        {
            int ret_number = -1234;

            try
            {
                ret_number = System.Convert.ToInt32(i_number_str);
            }
            catch (Exception e)
            {
                string error_msg = e.ToString();
                return ret_number;
            }

            return ret_number;

        } // StringToInt

        /// <summary>Returns true if the set current season is 2015-2016 or later 
        /// <para>XML entities in saison programs were added starting with season 2015-2016</para>
        /// </summary>
        public static Boolean IsInCurrentDocumentXmlItemsAdded2015()
        {
            Boolean ret_available = false;

            int xml_entity_start_year = 2015;

            // Programming check
            XDocument enter_activity_season_document = JazzXml.GetDocumentCurrent();
            if (null == enter_activity_season_document)
            {
                return ret_available;
            }

            int autumn_year = JazzXml.GetYearAutumnInt();

            if (autumn_year >= xml_entity_start_year)
            {
                ret_available = true;
            }
            else
            {
                ret_available = false;
            }

            return ret_available;
        } // IsInCurrentDocumentXmlItemsAdded2015

        /// <summary>Returns true if Calendar shall be shown, i.e. date and time before the concert</summary>
        public static Boolean ShowCalendar(int i_year, int i_month, int i_day)
        {
            Boolean ret_show = true;

            DateTime current_time = DateTime.Now;
            int current_year = current_time.Year;
            int current_month = current_time.Month;
            int current_day = current_time.Day;

            if (i_year < current_year)
                ret_show = false;
            else if (i_year == current_year && i_month < current_month)
                ret_show = false;
            else if (i_year == current_year && i_month == current_month && i_day < current_day)
                ret_show = false;

            return ret_show;
        } // ShowCalendar


        /// <summary>Get number of members to show, i.e. the members that currently are active in the club</summary>
        private static int GetNumberOfMembersToShow()
        {
            int number_of_members = JazzXml.GetNumberOfMembers();

            int n_number_of_members_to_show = 0;

            for (int i_member = 1; i_member <= number_of_members; i_member++)
            {
                if (ShowMember(i_member))
                {
                    n_number_of_members_to_show = n_number_of_members_to_show + 1;
                }
            }

            return n_number_of_members_to_show;
        } // GetNumberOfMembersToShow

        /// <summary>Returns true if a member shall be shown</summary>
        private static Boolean ShowMember(int i_member)
        {
            Boolean ret_show_member = false;

            Boolean in_vorstand = JazzXml.MemberIsInVorstand(i_member);
            int id_number = JazzXml.GetMemberNumber(i_member);

            if (in_vorstand && id_number > 0)
            {
                ret_show_member = true;
            }
            else if (in_vorstand)
            {
                // TODO Check funcion in Tools
                // Utils.LogStr("DisplayMembersActivity.ShowMember i_member= " + Integer.toString(i_member) + " has no identity number");
            }

            return ret_show_member;
        } // ShowMember


        /// <summary>Create an array with member numbers for members to show, i.e. members that are active in the jazz club</summary>
        public static int[] CreateArrayMembersToShow()
        {
            // Output array with members that shall be shown defined with member numbers. 
            // The array is ordered with the member identity number
            int[] ret_members_to_show = null;

            int number_of_members_to_show = JazzUtils.GetNumberOfMembersToShow();

            if (number_of_members_to_show <= 0)
            {
                return ret_members_to_show;
            }

            int[] member_to_show_unsorted = new int[number_of_members_to_show];
            int[] member_id_to_show_unsorted = new int[number_of_members_to_show];

            int index_added = 0;
            int number_of_members = JazzXml.GetNumberOfMembers();
            for (int i_member = 1; i_member <= number_of_members; i_member++)
            {
                if (ShowMember(i_member))
                {
                    member_to_show_unsorted[index_added] = i_member;
                    member_id_to_show_unsorted[index_added] = JazzXml.GetMemberNumber(i_member);

                    index_added = index_added + 1;
                }
            }

            ret_members_to_show = SortAndSetArrayMembersToShow(number_of_members_to_show, member_to_show_unsorted, member_id_to_show_unsorted);

            return ret_members_to_show;

        } // CreateArrayMembersToShow

        /// <summary>Sort and set array  members to show</summary>
        private static int[] SortAndSetArrayMembersToShow(int i_number_of_members_to_show, int[] i_member_to_show_unsorted, int[] i_member_id_to_show_unsorted)
        {
            int[] ret_members_to_show = null;

            if (null == i_member_to_show_unsorted)
            {
                return ret_members_to_show;
            }
            if (null == i_member_id_to_show_unsorted)
            {
                return ret_members_to_show;
            }

            ret_members_to_show = new int[i_number_of_members_to_show];

            Boolean[] not_used_values = new Boolean[i_number_of_members_to_show];
            for (int index_used = 0; index_used < i_number_of_members_to_show; index_used++)
                not_used_values[index_used] = true;

            for (int index_sorted = 0; index_sorted < i_number_of_members_to_show; index_sorted++)
            {
                int minimum_id_number = 100;
                int number_min = -1;
                int index_member_min = -1;
                for (int index_member = 0; index_member < i_number_of_members_to_show; index_member++)
                {
                    int current_id_number = i_member_id_to_show_unsorted[index_member];

                    if (current_id_number < minimum_id_number && not_used_values[index_member])
                    {
                        minimum_id_number = current_id_number;
                        number_min = i_member_to_show_unsorted[index_member];
                        index_member_min = index_member;
                    }

                }  // index_member

                if (number_min > 0)
                {
                    ret_members_to_show[index_sorted] = number_min;
                    not_used_values[index_member_min] = false;
                }
                else
                {
                    ret_members_to_show[index_sorted] = -12345;
                    return ret_members_to_show;
                }
            }

            return ret_members_to_show;
        } // SortAndSetArrayMembersToShow


        /// <summary>Returns an integer array from an int list array</summary>
        public static int[] ConvertListIntToInt(List<int> i_list_int)
        {
            // http://stackoverflow.com/questions/1367504/converting-listint-to-int

            int[] int_array = i_list_int.ToArray();

            return int_array;
        }

        /// <summary>Returns a string array from a string list array</summary>
        public static String[] ConvertStringListToString(List<String> i_string_list)
        {
            String[] ret_string_array = i_string_list.ToArray();

            return ret_string_array;
        } // ConvertStringListToString

    } // JazzUtils

} // namespace