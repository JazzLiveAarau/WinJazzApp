using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{

    /// <summary>Utility functions that are specific for the Windows operating systems</summary>
    public static class JazzOsUtils
    {

        /// <summary>Create an XML Document object with an XML file on the server
        /// <para>The function sets XML objects in JazzXml and JazzPhotosXml</para>
        /// </summary>
        /// <param name="i_xml_file_url">URL for the XML file on the server</param>
        /// <param name="i_case">Eq. 1: Application Eq. 2: Current season Eq. 3: Season Eq. 4: Photos Eq. 5: Documents Eq.6: Document templates Eq.7: Document requests</param>
        /// <param name="i_index">Season object index for i_case=3 and i_case=5</param>
        static public void LoadXmlDocument(string i_xml_file_url, int i_case, int i_index)
        {

            //QQ_SSL XDocument x_document = XDocument.Load(i_xml_file_url);

            string error_message = @"";

            XDocument x_document = DownloadXmlFileLoadXmlDocument(i_xml_file_url, out error_message);

            if (x_document != null)
            {
                if (1 == i_case)
                {
                    JazzXml.SetApplicationDocument(x_document);
                }
                else if (2 == i_case)
                {
                    JazzXml.SetCurrentSeasonDocument(x_document);
                }
                else if (3 == i_case)
                {
                    JazzXml.SetSeasonDocument(x_document, i_index);
                }
                else if (4 == i_case)
                {
                    JazzPhotosXml.SetPhotosDocument(x_document);
                }
                else if (5 == i_case)
                {
                    JazzXml.SetSeasonDocumentsXDocument(x_document, i_index);
                }
                else if (6 == i_case)
                {
                    JazzXml.SetXmlDocumentTemplates(x_document);
                }
                else if (7 == i_case)
                {
                    JazzXml.SetXmlDocumentReq(x_document);
                }
                else if (8 == i_case)
                {
                    JazzXml.SetXmlDocumentObjectOne(x_document);
                }
                else if (9 == i_case)
                {
                    JazzXml.SetXmlDocumentObjectTwo(x_document);
                }
                else if (10 == i_case)
                {
                    JazzXml.SetXmlDocumentNews(x_document);
                }
                else if (11 == i_case)
                {
                    JazzXml.SetXmlDocumentNewsletter(x_document);
                }
            }
            else
            {
                if (i_index <= 4)
                {
                    JazzXml.SetSeasonDocumentStatus(-3, i_index);
                }
                else if (5 == i_index)
                {
                    JazzXml.SetSeasonDocumentXDocumentStatus(-3, i_index);
                }

                if (6 == i_case)
                {
                    JazzXml.SetXmlDocumentTemplatesStatus(-1, "JazzOsUtils.LoadXmlDocument exception: " + error_message);
                }

                if (7 == i_case)
                {
                    JazzXml.SetXmlDocumentReqStatus(-1, "JazzOsUtils.LoadXmlDocument exception: " + error_message);
                }

                if (8 == i_case)
                {
                    JazzXml.SetXmlDocumentObjectPhotoOneStatus(-1, "JazzOsUtils.LoadXmlDocument exception: " + error_message);
                }

                if (9 == i_case)
                {
                    JazzXml.SetXmlDocumentObjectPhotoTwoStatus(-1, "JazzOsUtils.LoadXmlDocument exception: " + error_message);
                }

                if (11 == i_case)
                {
                    JazzXml.SetXmlDocumentNewsletterStatus(-1, "JazzOsUtils.LoadXmlDocument exception: " + error_message);
                }
            }

            return;

        } // LoadXmlDocument

        /// <summary>Downloads an XML file from the server, creates and returns an XML object
        /// <para>This function is necessary for an SSL homepage</para>
        /// </summary>
        /// <param name="i_xml_file_url">URL for the XML file on the server</param>
        /// <param name="o_error">Error message</param>
        static private XDocument DownloadXmlFileLoadXmlDocument(string i_xml_file_url, out string o_error)
        {
            XDocument ret_x_document = null;

            o_error = @"";

            JazzFtp.Input ftp_xml_download = new JazzFtp.Input(JazzXml.FtpHost, JazzXml.FtpUser, JazzXml.FtpPassword, JazzFtp.Input.Case.DownloadFile);

            string xml_local_sub_directory_path = Path.Combine(JazzXml.ExePath, "XML");

            if (!Directory.Exists(xml_local_sub_directory_path))
            {
                Directory.CreateDirectory(xml_local_sub_directory_path);
            }

            string xml_file_name = Path.GetFileName(i_xml_file_url);

            ftp_xml_download.ServerDirectory = "www/XML";
            ftp_xml_download.ServerFileName = xml_file_name;

            ftp_xml_download.LocalDirectory = xml_local_sub_directory_path;
            ftp_xml_download.LocalFileName = xml_file_name;

            JazzFtp.Result ftp_result = JazzFtp.Execute.Run(ftp_xml_download);

            if (!ftp_result.Status)
            {
                o_error = @"JazzXml.DownloadXmlFileLoadXmlDocument JazzFtp.Execute.Run failed " + ftp_result.ErrorMsg;

                return ret_x_document;
            }

            string local_xml_path_file = Path.Combine(xml_local_sub_directory_path, xml_file_name);

            try
            {
                ret_x_document = XDocument.Load(local_xml_path_file);
            }

            catch (Exception e)
            {
                o_error = e.ToString();

                return ret_x_document;

            } // catch

            finally
            {

            } // finally

            if (File.Exists(local_xml_path_file))
            {
                File.Delete(local_xml_path_file);
            }

            return ret_x_document;

        } // DownloadXmlFileLoadXmlDocument

        /// <summaryReturns false if the file not exists on the server</summary>
        /// <param name="i_url">URL for the file on the server</param>
        static public bool RemoteFileExists(string i_url)
        {

            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(i_url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        } // RemoteFileExists

    } // JazzOsUtils
} // namespace
