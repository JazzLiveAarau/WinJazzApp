using System;
using System.Linq;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Functions to get random photos defined in an XML files. Photos and XML files are on the server
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzPhotosXml
    {
        /// <summary>Gets random jazz photos</summary>
        static private RandomPicture m_random_picture = new RandomPicture();

        /// <summary>The XML DOM object created from the photos XML file on the server</summary>
        static private XDocument m_photos_document = null;

        /// <summary>Tag name for the photo</summary>
        static private String m_tag_name_photo = "PathPhoto";

        /// <summary>URL start path to a photo XML file in folder XML/Photos on the server</summary>
        static private string m_path_photo_xml = "http://www.jazzliveaarau.ch/XML/Photos/SmallPhotos_";

        /// <summary>Flags telling if photos have been displayed</summary>
        static private Boolean[] m_photo_is_displayed = null;

        /// <summary>The number of photo nodes</summary>
        static private int m_number_photo_nodes = -12345;

        /// <summary>Flag telling if the photos XML object is set, i.e. if function CreateAndLoadXmlDocument has been called</summary>
        static private bool m_photos_document_initialized = false;

        /// <summary>Returns true if the season XML objects are set, i.e. if function InitXmlAllSeasons has been called</summary>
        static public bool PhotosDocumentInitialized() { return m_photos_document_initialized; }

        /// <summary>Initialization: Create the XML Document object with the photos XML file on the server</summary>
        static public void CreateAndLoadXmlDocument()
        {
            // m_number_photo_nodes = concertNodes.getLength();

            try
            {
                JazzOsUtils.LoadXmlDocument(GetPhotosFileName(), 4, -12345);

                m_number_photo_nodes = GetNumberPhotosInXmlDocument();

                if (m_number_photo_nodes <= 0)
                    return;

                m_photo_is_displayed = new Boolean[m_number_photo_nodes];

                InitPhotoDisplayedFlags();

                m_photos_document_initialized = true;
            }
            catch (Exception e)
            {
                String exc_msg = e.ToString();
                return;
            }
            finally
            {

            } // finally


        } // CreateAndLoadXmlDocument

        /// <summary>Set photos XML document</summary>
        static public void SetPhotosDocument(XDocument i_photos_document) { m_photos_document = i_photos_document; }


        /// <summary>Returns the number of defined photos in the selected XML document</summary>
        static private int GetNumberPhotosInXmlDocument()
        {
            int ret_number_photos = 0;
            if (null == m_photos_document)
                return -1;

            foreach (XElement element_concert in m_photos_document.Descendants(m_tag_name_photo))
            {
                ret_number_photos = ret_number_photos + 1;
            }

            return ret_number_photos;
        }

        /// <summary>Returns the URL for a random photo from the selected XML photos document</summary>
        static public String GetPhotoRandom()
        {
            String ret_photo_url = "Error GetPhotoRandom: XML Document is not created";
            if (null == m_photos_document)
                return ret_photo_url;
            if (m_number_photo_nodes <= 1)
                return ret_photo_url;

            int random_index = m_random_picture.randomUniformInt(m_number_photo_nodes - 1);

            random_index = SetDisplayedFlag(random_index);

            String ret_inner_text = "";

            int current_number = 0;
            foreach (XElement element_photo in m_photos_document.Descendants(m_tag_name_photo))
            {

                if (random_index == current_number)
                {
                    ret_inner_text = element_photo.Value;

                    return ret_inner_text;
                }

                current_number = current_number + 1;
            }

            return "Error GetPhotoRandom: Photo not found";

        } // GetPhotoRandom

        /// <summary>Sets displayed flag. If input index already is used will an unused index be returned </summary>
        private static int SetDisplayedFlag(int i_index_picture)
        {
            int ret_index_picture = i_index_picture;

            if (null == m_photo_is_displayed)
            {
                if (i_index_picture < 0)
                    ret_index_picture = 0; // Programming error
                return ret_index_picture;
            }

            if (i_index_picture < 0 || i_index_picture >= m_number_photo_nodes)
            {
                ret_index_picture = 0; // Programming error
                return ret_index_picture;
            }

            if (false == m_photo_is_displayed[i_index_picture])
            {
                m_photo_is_displayed[i_index_picture] = true;
                return ret_index_picture;
            }


            for (int index_flag = 0; index_flag < m_number_photo_nodes; index_flag++)
            {
                if (false == m_photo_is_displayed[index_flag])
                {
                    m_photo_is_displayed[index_flag] = true;
                    ret_index_picture = index_flag;
                    return ret_index_picture;
                }
            }

            // All pictures have been displayed
            InitPhotoDisplayedFlags();

            m_photo_is_displayed[i_index_picture] = true;
            return ret_index_picture;

        } // SetDisplayedFlag


        /// <summary>Initialization of the photos is displayed flags</summary>
        private static void InitPhotoDisplayedFlags()
        {
            if (null == m_photo_is_displayed)
                return;

            for (int index_flag = 0; index_flag < m_number_photo_nodes; index_flag++)
            {
                m_photo_is_displayed[index_flag] = false;
            }

        } // InitPhotoDisplayedFlags

        /// <summary>Returns the full name of a randomly selected XML photos file</summary>
        static private String GetPhotosFileName()
        {
            int xml_file_number = m_random_picture.randomUniformInt(9) + 1;

            String str_xml_file_number = "";
            if (xml_file_number < 10)
                str_xml_file_number = "0" + xml_file_number.ToString();
            else
                str_xml_file_number = xml_file_number.ToString();

            String ret_string = m_path_photo_xml + str_xml_file_number + ".xml";

            return ret_string;
        } // GetPhotosFileName


    } // JazzPhotosXml

}