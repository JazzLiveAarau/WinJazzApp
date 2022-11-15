using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for a photo gallery, i.e. data about nine photos, photographer, date, ...
    /// <para></para>
    /// </summary>
    public class JazzPhoto
    {
        #region Define, set and get member variables (as strings)

        /// <summary>Band name</summary>
        private string m_band_name = @"";
        /// <summary>Get and set band name</summary>
        public string BandName { get { return m_band_name; } set { m_band_name = value; } }

        /// <summary>Year as string</summary>
        private string m_year_str = @"";
        /// <summary>Get and set year as string</summary>
        public string Year { get { return m_year_str; } set { m_year_str = value; } }

        /// <summary>Month as string</summary>
        private string m_month_str = @"";
        /// <summary>Get and set month as string</summary>
        public string Month { get { return m_month_str; } set { m_month_str = value; } }

        /// <summary>Day as string</summary>
        private string m_day_str = @"";
        /// <summary>Get and set day as string</summary>
        public string Day { get { return m_day_str; } set { m_day_str = value; } }

        /// <summary>Gallery name</summary>
        private string m_gallery_name = @"";
        /// <summary>Get and set gallery name</summary>
        public string GalleryName { get { return m_gallery_name; } set { m_gallery_name = value; } }

        /// <summary>Text one</summary>
        private string m_text_one = @"";
        /// <summary>Get and set text one</summary>
        public string TextOne { get { return m_text_one; } set { m_text_one = value; } }

        /// <summary>Text two</summary>
        private string m_text_two = @"";
        /// <summary>Get and set text two</summary>
        public string TextTwo { get { return m_text_two; } set { m_text_two = value; } }

        /// <summary>Text three</summary>
        private string m_text_three = @"";
        /// <summary>Get and set text three</summary>
        public string TextThree { get { return m_text_three; } set { m_text_three = value; } }

        /// <summary>Text four</summary>
        private string m_text_four = @"";
        /// <summary>Get and set text four</summary>
        public string TextFour { get { return m_text_four; } set { m_text_four = value; } }

        /// <summary>Text five</summary>
        private string m_text_five = @"";
        /// <summary>Get and set text five</summary>
        public string TextFive { get { return m_text_five; } set { m_text_five = value; } }

        /// <summary>Text six</summary>
        private string m_text_six = @"";
        /// <summary>Get and set text six</summary>
        public string TextSix { get { return m_text_six; } set { m_text_six = value; } }

        /// <summary>Text seven</summary>
        private string m_text_seven = @"";
        /// <summary>Get and set text seven</summary>
        public string TextSeven { get { return m_text_seven; } set { m_text_seven = value; } }

        /// <summary>Text eight</summary>
        private string m_text_eight = @"";
        /// <summary>Get and set text eight</summary>
        public string TextEight { get { return m_text_eight; } set { m_text_eight = value; } }

        /// <summary>Text nine</summary>
        private string m_text_nine = @"";
        /// <summary>Get and set text nine</summary>
        public string TextNine { get { return m_text_nine; } set { m_text_nine = value; } }

        /// <summary>Photographer name</summary>
        private string m_photographer_name = @"";
        /// <summary>Get and set photographer name</summary>
        public string PhotographerName { get { return m_photographer_name; } set { m_photographer_name = value; } }

        /// <summary>Zip file name</summary>
        private string m_zip_name = @"";
        /// <summary>Get and set zip file name</summary>
        public string ZipName { get { return m_zip_name; } set { m_zip_name = value; } }

        /// <summary>Concert number as string</summary>
        private string m_concert_number_str = @"";
        /// <summary>Get and set concert number as string</summary>
        public string ConcertNumber { get { return m_concert_number_str; } set { m_concert_number_str = value; } }

        #endregion // Define, set and get member variables (as strings)

        #region Get and set some member variables as integers

        /// <summary>Get and set year as integer</summary>
        public int YearInt { get { return JazzUtils.StringToInt(m_year_str); } set { m_year_str = value.ToString(); } }

        /// <summary>Get and set month as integer</summary>
        public int MonthInt { get { return JazzUtils.StringToInt(m_month_str); } set { m_month_str = value.ToString(); } }

        /// <summary>Get and set day as integer</summary>
        public int DayInt { get { return JazzUtils.StringToInt(m_day_str); } set { m_day_str = value.ToString(); } }

        /// <summary>Get and set concert number as integer</summary>
        public int ConcertNumberInt { get { return JazzUtils.StringToInt(m_concert_number_str); } set { m_concert_number_str = value.ToString(); } }

        /// <summary>Get and set gallery number as integer</summary>
        public int GalleryNumberInt { get { return GetGalleryNumberInt(); } set { SetGalleryNumberInt(value); } }

        /// <summary>Returns the gallery number as int</summary>
        private int GetGalleryNumberInt()
        {
            if (GalleryName.Length < 3 || GalleryName.Length > 4)
            {
                return -99;
            }

            if (!GalleryName.Substring(0,1).Equals(@"G"))
            {
                return -98;
            }

            string number_str = GalleryName.Substring(1);

            // G023  G02

            string hundert_str = @""; // Can also be ten_str

            hundert_str = number_str.Substring(0, 1);
            if (!hundert_str.Equals("0"))
            {
                return JazzUtils.StringToInt(number_str);
            }

            number_str = number_str.Substring(1);
            string ten_str = number_str.Substring(0, 1); // Can be one_string

            if (!ten_str.Equals("0"))
            {
                return JazzUtils.StringToInt(number_str);
            }

            number_str = number_str.Substring(0, 1);

            return JazzUtils.StringToInt(number_str);

        } // GetGalleryNumberInt

        /// <summary>Returns the gallery number as int</summary>
        private void SetGalleryNumberInt(int i_gallery_number)
        {
            if (i_gallery_number <= 0 || i_gallery_number > 999)
            {
                return;
            }

            if (i_gallery_number >= 100)
            {
                GalleryName = @"G" + i_gallery_number.ToString();
                return;
            }

            else if (i_gallery_number >= 10)
            {
                GalleryName = @"G0" + i_gallery_number.ToString();
                return;
            }
            else
            {
                GalleryName = @"G00" + i_gallery_number.ToString();
                return;
            }

        } // SetGalleryNumberInt

        #endregion // Get and set some member variables as integers

        #region Gallery text file parameters

        /// <summary>Flags telling if a big photo is in the local gallery directory</summary>
        private bool[] m_big_pictures = { false, false, false, false, false, false, false, false, false };

        /// <summary>Flags telling if a small photo is in the local gallery directory</summary>
        private bool[] m_small_pictures = { false, false, false, false, false, false, false, false, false };

        /// <summary>Set flag telling if a big photo is in the local gallery directory</summary>
        public void SetBigPicture(int i_big_picture_number, bool i_big_picture_flag)
        {
            if (i_big_picture_number >= 1 && i_big_picture_number <= 9)
            {
                m_big_pictures[i_big_picture_number - 1] = i_big_picture_flag;
            }
        } // SetBigPicture

        /// <summary>Set flag telling if a small photo is in the local gallery directory</summary>
        public void SetSmallPicture(int i_small_picture_number, bool i_small_picture_flag)
        {
            if (i_small_picture_number >= 1 && i_small_picture_number <= 9)
            {
                m_small_pictures[i_small_picture_number - 1] = i_small_picture_flag;
            }
        } // SetSmallPicture

        /// <summary>Returns true if the big picture has been set</summary>
        public bool BigPictureIsSet(int i_big_picture_number)
        {
            if (i_big_picture_number >= 1 && i_big_picture_number <= 9)
            {
                return m_big_pictures[i_big_picture_number - 1];
            }

            return false;

        } // BigPictureIsSet

        /// <summary>Returns true if the small picture has been set</summary>
        public bool SmallPictureIsSet(int i_small_picture_number)
        {
            if (i_small_picture_number >= 1 && i_small_picture_number <= 9)
            {
                return m_small_pictures[i_small_picture_number - 1];
            }

            return false;

        } // SmallPictureIsSet

        #endregion // Gallery text file parameters

        #region Gallery text file and directory names

        /// <summary>Get the name of the local gallery two directory
        /// <para></para>
        /// </summary>
        /// <param name="o_directory_name">Name of the local gallery directory</param>
        /// <param name="o_error">Error message</param>
        public bool GalleryTwoDirectoryName(out string o_directory_name, out string o_error)
        {
            o_error = @"";
            o_directory_name = @"";

            if (!CheckParameterValues(out o_error))
            {
                o_error = @"JazzPhoto.LocalGalleryDirectoryName CheckParameterValues failed " + o_error;
                return false;
            }

            o_directory_name = o_directory_name + @"Konzert.";

            o_directory_name = o_directory_name + Year + @".";

            if (Month.Length == 1)
            {
                o_directory_name = o_directory_name + @"0" + Month + @".";
            }
            else
            {
                o_directory_name = o_directory_name + Month + @".";
            }

            if (Day.Length == 1)
            {
                o_directory_name = o_directory_name + @"0" + Day;
            }
            else
            {
                o_directory_name = o_directory_name + Day;
            }

            return true;

        } // LocalGalleryDirectoryName

        /// <summary>Get the name of the TXT file for the local gallery</summary>
        private string GalleryTxtFileName { get { return m_gallery_name + @".txt"; } }

        /// <summary>Get data from the local gallery directory text file
        /// <para>Set text file name to input path plus GalleryTxtFileName</para>
        /// <para>Create text file if it is missing. Call of CreateLocalGalleryDirectoryTxtFile and return</para>
        /// <para>Read data from the file</para>
        /// <para></para>
        /// </summary>
        /// <param name="i_full_directory_name">Name and path to the local gallery directory</param>
        /// <param name="o_error">Error message</param>
        public bool GetDataFromLocalGalleryDirectory(string i_full_directory_name, out string o_error)
        {
            o_error = @"";

            string txt_file_name = i_full_directory_name + GalleryTxtFileName;

            if (!File.Exists(txt_file_name))
            {
                if (!CreateLocalGalleryDirectoryTxtFile(txt_file_name, out o_error))
                {
                    o_error = @"JazzPhoto.GetDataFromLocalGalleryDirectory CreateLocalGalleryDirectoryTxtFile failed " + o_error;
                    return false;
                }

                return true;
            }

            string[] file_content = new string[27];

            try
            {
                using (FileStream file_stream = new FileStream(txt_file_name, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader stream_reader = new StreamReader(file_stream))
                {
                    int index_content = 0;
                    while (stream_reader.Peek() >= 0)
                    {
                        string current_row = stream_reader.ReadLine();

                        file_content[index_content] = current_row;

                        index_content = index_content + 1;
                        if (index_content == 27)
                        {
                            break;
                        }

                    } // while

                    if (index_content != 27)
                    {
                        o_error = @"JazzPhoto.GetDataFromLocalGalleryDirectory Gallery text file not OK index_content= " + index_content.ToString();
                        return false;
                    }

                } // using
            } // try


            catch (FileNotFoundException) { o_error = "File not found"; return false; }
            catch (DirectoryNotFoundException) { o_error = "Directory not found"; return false; }
            catch (InvalidOperationException) { o_error = "Invalid operation"; return false; }
            catch (InvalidCastException) { o_error = "invalid cast"; return false; }
            catch (Exception e)
            {
                o_error = "JazzPhoto.GetDataFromLocalGalleryDirectory Unhandled Exception " + e.GetType() + " occurred at " + DateTime.Now + "!";
                return false;
            }

            for (int index_big=0; index_big<m_big_pictures.Length; index_big++)
            {
                int index_content_big = index_big;
                if (file_content[index_content_big].Contains(@"true"))
                {
                    m_big_pictures[index_big] = true;
                }
                else
                {
                    m_big_pictures[index_big] = false;
                }

            } // index_big

            for (int index_small = 0; index_small < m_small_pictures.Length; index_small++)
            {
                int index_content_small = index_small + 9;
                if (file_content[index_content_small].Contains(@"true"))
                {
                    m_small_pictures[index_small] = true;
                }
                else
                {
                    m_small_pictures[index_small] = false;
                }

            } // index_small

            TextOne = file_content[18].Substring(JazzXml.GetTagPhotoTextOne().Length);
            TextTwo = file_content[19].Substring(JazzXml.GetTagPhotoTextTwo().Length);
            TextThree = file_content[20].Substring(JazzXml.GetTagPhotoTextThree().Length);
            TextFour = file_content[21].Substring(JazzXml.GetTagPhotoTextFour().Length);
            TextFive = file_content[22].Substring(JazzXml.GetTagPhotoTextFive().Length);
            TextSix = file_content[23].Substring(JazzXml.GetTagPhotoTextSix().Length);
            TextSeven = file_content[24].Substring(JazzXml.GetTagPhotoTextSeven().Length);
            TextEight = file_content[25].Substring(JazzXml.GetTagPhotoTextEight().Length);
            TextNine = file_content[26].Substring(JazzXml.GetTagPhotoTextNine().Length);

            TextOne = TextOne.Trim();
            TextTwo = TextTwo.Trim();
            TextThree = TextThree.Trim();
            TextFour = TextFour.Trim();
            TextFive = TextFive.Trim();
            TextSix = TextSix.Trim();
            TextSeven = TextSeven.Trim();
            TextEight = TextEight.Trim();
            TextNine = TextNine.Trim();

            return true;

        } // GetDataFromLocalGalleryDirectory

        /// <summary>Write local gallery text file
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_local_gallery_dir">Path to the local gallery directory</param>
        /// <param name="o_error">Error message</param>
        public bool WriteLocalGalleryDirectoryTxtFile(string i_local_gallery_dir, out string o_error)
        {
            o_error = @"";

            string full_file_name = i_local_gallery_dir + GalleryTxtFileName;

            string[] file_content = new string[27];

            for (int index_big = 0; index_big < m_big_pictures.Length; index_big++)
            {
                int index_content_big = index_big;
                if (m_big_pictures[index_big])
                {
                    file_content[index_content_big] = @"PhotoBig_" + (index_big + 1).ToString() + @" true";
                }
                else
                {
                    file_content[index_content_big] = @"PhotoBig_" + (index_big + 1).ToString() + @" false";
                }
            } // index_big

            for (int index_small = 0; index_small < m_small_pictures.Length; index_small++)
            {
                int index_content_small = index_small + 9;
                if (m_big_pictures[index_small])
                {
                    file_content[index_content_small] = @"PhotoSmall_" + (index_small + 1).ToString() + @" true";
                }
                else
                {
                    file_content[index_content_small] = @"PhotoSmall_" + (index_small + 1).ToString() + @" false";
                }

            } // index_small


            file_content[18] = (JazzXml.GetTagPhotoTextOne() + @" " + TextOne).Trim();
            file_content[19] = (JazzXml.GetTagPhotoTextTwo() + @" " + TextTwo).Trim();
            file_content[20] = (JazzXml.GetTagPhotoTextThree() + @" " + TextThree).Trim();
            file_content[21] = (JazzXml.GetTagPhotoTextFour() + @" " + TextFour).Trim();
            file_content[22] = (JazzXml.GetTagPhotoTextFive() + @" " + TextFive).Trim();
            file_content[23] = (JazzXml.GetTagPhotoTextSix() + @" " + TextSix).Trim();
            file_content[24] = (JazzXml.GetTagPhotoTextSeven() + @" " + TextSeven).Trim();
            file_content[25] = (JazzXml.GetTagPhotoTextEight() + @" " + TextEight).Trim();
            file_content[26] = (JazzXml.GetTagPhotoTextNine() + @" " + TextNine).Trim();

            FileStream file_stream = null;

            try
            {

                file_stream = new FileStream(full_file_name, FileMode.Create);
                int buffer_size = 512;
                using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(file_stream, Encoding.UTF8, buffer_size))
                {
                    for (int index_str = 0; index_str < file_content.Length; index_str++)
                    {
                        outfile.Write(file_content[index_str]);
                        outfile.Write(System.Environment.NewLine);
                    }
                }
            } // try

            catch (Exception e)
            {
                o_error = " Unhandled Exception " + e.GetType() + " occurred at " + DateTime.Now + "!";
                return false;
            }
            finally
            {
                if (file_stream != null)
                    file_stream.Dispose();
            }


            return true;

        } // WriteLocalGalleryDirectoryTxtFile

        /// <summary>Create local gallery text file
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="i_full_file_name">Full file name of i_full_file_name</param>
        /// <param name="o_error">Error message</param>
        private bool CreateLocalGalleryDirectoryTxtFile(string i_full_file_name, out string o_error)
        {
            o_error = @"";

            string[] file_content = new string[27];

            file_content[0] = @"PhotoBig_1 false";
            file_content[1] = @"PhotoBig_2 false";
            file_content[2] = @"PhotoBig_3 false";
            file_content[3] = @"PhotoBig_4 false";
            file_content[4] = @"PhotoBig_5 false";
            file_content[5] = @"PhotoBig_6 false";
            file_content[6] = @"PhotoBig_7 false";
            file_content[7] = @"PhotoBig_8 false";
            file_content[8] = @"PhotoBig_9 false";

            file_content[9] = @"PhotoSmall_1 false";
            file_content[10] = @"PhotoSmall_2 false";
            file_content[11] = @"PhotoSmall_3 false";
            file_content[12] = @"PhotoSmall_4 false";
            file_content[13] = @"PhotoSmall_5 false";
            file_content[14] = @"PhotoSmall_6 false";
            file_content[15] = @"PhotoSmall_7 false";
            file_content[16] = @"PhotoSmall_8 false";
            file_content[17] = @"PhotoSmall_9 false";

            file_content[18] = JazzXml.GetTagPhotoTextOne();
            file_content[19] = JazzXml.GetTagPhotoTextTwo();
            file_content[20] = JazzXml.GetTagPhotoTextThree();
            file_content[21] = JazzXml.GetTagPhotoTextFour();
            file_content[22] = JazzXml.GetTagPhotoTextFive();
            file_content[23] = JazzXml.GetTagPhotoTextSix();
            file_content[24] = JazzXml.GetTagPhotoTextSeven();
            file_content[25] = JazzXml.GetTagPhotoTextEight();
            file_content[26] = JazzXml.GetTagPhotoTextNine();


            FileStream file_stream = null;

            try
            {

                file_stream = new FileStream(i_full_file_name, FileMode.Create);
                int buffer_size = 512;
                using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(file_stream, Encoding.UTF8, buffer_size))
                {
                    for (int index_str = 0; index_str < file_content.Length; index_str++)
                    {
                        outfile.Write(file_content[index_str]);
                        outfile.Write(System.Environment.NewLine);
                    }
                }
            } // try

            catch (Exception e)
            {
                o_error = " Unhandled Exception " + e.GetType() + " occurred at " + DateTime.Now + "!";
                return false;
            }
            finally
            {
                if (file_stream != null)
                    file_stream.Dispose();
            }


            return true;

        } // CreateLocalGalleryDirectoryTxtFile


        /// <summary>Get the name for the gallery directory picture file
        /// <para>A name is only returned if m_big_pictures/m_small_pictures is true for the given picture number</para>
        /// <para></para>
        /// </summary>
        /// <param name="i_b_big">Flag telling if it is a big or small picture</param>
        /// <param name="i_picture_number">Picture number</param>
        /// <param name="o_file_name">Output file name</param>
        /// <param name="o_error">Error message</param>
        public bool GalleryPhotoName(bool i_b_big, int i_picture_number, out string o_file_name, out string o_error)
        {
            o_error = @"";
            o_file_name = @"";

            if (i_picture_number < 1 || i_picture_number > 9)
            {
                o_error = @"JazzPhoto.GalleryPhotoName Unvalid i_picture_number= " + i_picture_number.ToString();
                return false;
            }

            if (i_b_big)
            {
                if (!m_big_pictures[i_picture_number - 1])
                {
                    o_file_name = @"";
                    return true;
                }
            }
            else
            {
                if (!m_small_pictures[i_picture_number - 1])
                {
                    o_file_name = @"";
                    return true;
                }
            }

            // JazzBild_G118_01_LowRes.jpg
            // JazzBild_G118_01_small.jpg

            o_file_name = o_file_name + @"JazzBild_" + GalleryName + @"_0" + i_picture_number.ToString() + @"_";

            if (i_b_big)
            {
                o_file_name = o_file_name + @"LowRes.jpg";
            }
            else
            {
                o_file_name = o_file_name + @"small.jpg";
            }


            return true;

        } // GalleryPhotoName

        #endregion // Gallery text file and directory names

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

        /// <summary>Set to empty strings if values not yet are set</summary>
        public void SetToEmptyStringsForValuesNotYetSet()
        {
            BandName = EmptyStringIfValueNotYetSet(BandName);
            Year = EmptyStringIfValueNotYetSet(Year);
            Month = EmptyStringIfValueNotYetSet(Month);
            Day = EmptyStringIfValueNotYetSet(Day);
            GalleryName = EmptyStringIfValueNotYetSet(GalleryName);
            TextOne = EmptyStringIfValueNotYetSet(TextOne);
            TextTwo = EmptyStringIfValueNotYetSet(TextTwo);
            TextThree = EmptyStringIfValueNotYetSet(TextThree);
            TextFour = EmptyStringIfValueNotYetSet(TextFour);
            TextFive = EmptyStringIfValueNotYetSet(TextFive);
            TextSix = EmptyStringIfValueNotYetSet(TextSix);
            TextSeven = EmptyStringIfValueNotYetSet(TextSeven);
            TextEight = EmptyStringIfValueNotYetSet(TextEight);
            TextNine = EmptyStringIfValueNotYetSet(TextNine);
            PhotographerName = EmptyStringIfValueNotYetSet(PhotographerName);
            ZipName = EmptyStringIfValueNotYetSet(ZipName);
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

        /// <summary>Returns a JazzPhoto object where empty strings have been replaced by JazzXml.GetUndefinedNodeValue()</summary>
        public JazzPhoto GetObjectWithUndefinedNodeValues(JazzPhoto i_jazz_photo)
        {
            JazzPhoto ret_jazz_photo = i_jazz_photo;

            BandName = SetToValueNotYetSetIfEmptyString(BandName);
            Year = SetToValueNotYetSetIfEmptyString(Year);
            Month = SetToValueNotYetSetIfEmptyString(Month);
            Day = SetToValueNotYetSetIfEmptyString(Day);
            GalleryName = SetToValueNotYetSetIfEmptyString(GalleryName);
            TextOne = SetToValueNotYetSetIfEmptyString(TextOne);
            TextTwo = SetToValueNotYetSetIfEmptyString(TextTwo);
            TextThree = SetToValueNotYetSetIfEmptyString(TextThree);
            TextFour = SetToValueNotYetSetIfEmptyString(TextFour);
            TextFive = SetToValueNotYetSetIfEmptyString(TextFive);
            TextSix = SetToValueNotYetSetIfEmptyString(TextSix);
            TextSeven = SetToValueNotYetSetIfEmptyString(TextSeven);
            TextEight = SetToValueNotYetSetIfEmptyString(TextEight);
            TextNine = SetToValueNotYetSetIfEmptyString(TextNine);
            PhotographerName = SetToValueNotYetSetIfEmptyString(PhotographerName);
            ZipName = SetToValueNotYetSetIfEmptyString(ZipName);
            ConcertNumber = SetToValueNotYetSetIfEmptyString(ConcertNumber);

            return ret_jazz_photo;

        } // GetObjectWithUndefinedNodeValues

        #endregion // Set to empty strings if member parameter values not yet are set

        #region Check functions

        /// <summary>Check the parameter values</summary>
        public bool CheckParameterValues(out string o_error)
        {
            o_error = @"";
            bool ret_bool = true;

            if (BandName.Trim().Length < 1)
            {
                o_error = @"JazzPhoto.CheckParameterValues BandName string length < 1";
                return false;
            }

            if (Day.Length < 1 || Day.Length > 2)
            {
                o_error = @"JazzPhoto.CheckParameterValues Day string length < 1 or > 2";
                return false;
            }

            if (DayInt < 1 || DayInt > 31)
            {
                o_error = @"JazzPhoto.CheckParameterValues DayInt value < 1 or > 31 ";
                return false;
            }

            if (Month.Length < 1 || Month.Length > 2)
            {
                o_error = @"JazzPhoto.CheckParameterValues Month string length < 1 or > 2";
                return false;
            }

            if (MonthInt < 1 || MonthInt > 12)
            {
                o_error = @"JazzPhoto.CheckParameterValues MonthInt value < 1 or > 12 ";
                return false;
            }

            if (Year.Length != 4)
            {
                o_error = @"JazzPhoto.CheckParameterValues Year string length is not equal to four (4)";
                return false;
            }

            if (YearInt < 1996)
            {
                o_error = @"JazzPhoto.CheckParameterValues YearInt value < 1996 ";
                return false;
            }

            if (PhotographerName.Trim().Length < 1)
            {
                o_error = @"JazzPhoto.CheckParameterValues PhotographerName string length < 1";
                return false;
            }

            if (ConcertNumber.Length < 1)
            {
                o_error = @"JazzPhoto.CheckParameterValues ConcertNumber length < 1";
                return false;
            }

            if (ConcertNumberInt < 1 || ConcertNumberInt > 20) // Actually twelve, but ...
            {
                o_error = @"JazzPhoto.CheckParameterValues ConcertNumberInt value < 1 or > 20 ";
                return false;
            }

            if (GalleryName.Length < 3 || GalleryName.Length > 4)
            {
                o_error = @"JazzPhoto.CheckParameterValues GalleryName string length is not 3 or 4";
                return false;
            }

            if (!GalleryName.Substring(0,1).Equals("G"))
            {
                o_error = @"JazzPhoto.CheckParameterValues First character of GalleryName is not G";
                return false;
            }

            if (!CheckNumberString(GalleryName.Substring(1, 1), out o_error))
            {
                o_error = @"JazzPhoto.CheckParameterValues Second character of GalleryName is not a number " + o_error;
                return false;
            }

            if (!CheckNumberString(GalleryName.Substring(2, 1), out o_error))
            {
                o_error = @"JazzPhoto.CheckParameterValues Third character of GalleryName is not a number " + o_error;
                return false;
            }

            if (GalleryName.Length == 4)
            {
                if (!CheckNumberString(GalleryName.Substring(3, 1), out o_error))
                {
                    o_error = @"JazzPhoto.CheckParameterValues Fourth character of GalleryName is not a number " + o_error;
                    return false;
                }
            }
 
            return ret_bool;

        } // CheckParameterValues

        /// <summary>Check number value</summary>
        public bool CheckNumberString(string i_number_str, out string o_error)
        {
            o_error = @"";
  
            if (i_number_str.Length != 1)
            {
                o_error = @"JazzPhoto.CheckNumberString Input string length is not one (1)";
                return false;
            }

            if (i_number_str.Equals("0"))
            {
                return true;
            }
            else if (i_number_str.Equals("1"))
            {
                return true;
            }
            else if (i_number_str.Equals("2"))
            {
                return true;
            }
            else if (i_number_str.Equals("3"))
            {
                return true;
            }
            else if (i_number_str.Equals("4"))
            {
                return true;
            }
            else if (i_number_str.Equals("5"))
            {
                return true;
            }
            else if (i_number_str.Equals("6"))
            {
                return true;
            }
            else if (i_number_str.Equals("7"))
            {
                return true;
            }
            else if (i_number_str.Equals("8"))
            {
                return true;
            }
            else if (i_number_str.Equals("9"))
            {
                return true;
            }
            else
            {
                o_error = @"JazzPhoto.CheckNumberString Input string is not 0, 1, 2, 3,.... or 9 ";
                return false;
            }

        } // CheckNumberString

        #endregion // Check functions

    } // JazzPhoto

} // namespace
