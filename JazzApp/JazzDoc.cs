using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for a jazz document like for instance a season program, a concert information (program) or a poster
    /// <para>Instances of this class can be used to set or get data of XML doc objects (corresponding to files JazzDokumente_20XX_20YY.xml)</para>
    /// <para></para>
    /// <para>It is possible to use "direct" XML get and set functions, but for convinience reasons the calling application should use instances of this class.</para>
    /// <para>A typical use is for instance class JazzDocAll. Member variables of this class are arrays of JazzDoc objects</para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public class JazzDoc
    {
        /// <summary>Template name for the document</summary>
        private string m_template_name = @"";
        /// <summary>Template name for the document</summary>
        public string TemplateName { get { return m_template_name; } set { m_template_name = value; } }

        /// <summary>Path to the file/document</summary>
        private string m_file_path = @"";
        /// <summary>Path to the file/document</summary>
        public string FilePath { get { return m_file_path; } set { m_file_path = value; } }

        /// <summary>File name doc, i.e. a document for the application Word</summary>
        private string m_file_name_doc = @"";
        /// <summary>File name doc, i.e. a document for the application Word</summary>
        public string FileNameDoc { get { return m_file_name_doc; } set { m_file_name_doc = value; } }

        /// <summary>File name xls, i.e. a document for the application Excel</summary>
        private string m_file_name_xls = @"";
        /// <summary>File name xls, i.e. a document for the application Excel</summary>
        public string FileNameXls { get { return m_file_name_xls; } set { m_file_name_xls = value; } }

        /// <summary>File name pdf, i.e. a document for instance for the application Adobe Reader</summary>
        private string m_file_name_pdf = @"";
        /// <summary>File name pdf, i.e. a document for instance for the application Adobe Reader</summary>
        public string FileNamePdf { get { return m_file_name_pdf; } set { m_file_name_pdf = value; } }
         
        /// <summary>File name txt, i.e. a document for instance for the applications Notepad and Notepad++</summary>
        private string m_file_name_txt = @"";
        /// <summary>File name txt, i.e. a document for instance for the applications Notepad and Notepad++</summary>
        public string FileNameTxt { get { return m_file_name_txt; } set { m_file_name_txt = value; } }

        /// <summary>File name jpg, png, .. i.e. a photo for instance for the application Snagit</summary>
        private string m_file_name_img = @"";
        /// <summary>File name jpg, png, .. i.e. a photo for instance for the application Snagit</summary>
        public string FileNameImg { get { return m_file_name_img; } set { m_file_name_img = value; } }

        /// <summary>A flag telling if the document may be published</summary>
        private bool m_published = false;
        /// <summary>A flag telling if the document may be published</summary>
        public bool Published { get { return m_published; } set { m_published = value; } }

        /// <summary>Returns the member variable values as a string</summary>
        public string DebugMembers()
        {
            string ret_string = @"JazzDoc member variables:" + "\r\n";

            ret_string = ret_string + @"TemplateName= " + TemplateName + "\r\n";
            ret_string = ret_string + @"FilePath=    " + FilePath + "\r\n";
            ret_string = ret_string + @"FileNameDoc= " + FileNameDoc + "\r\n";
            ret_string = ret_string + @"FileNameXls= " + FileNameXls + "\r\n";
            ret_string = ret_string + @"FileNamePdf= " + FileNamePdf + "\r\n";
            ret_string = ret_string + @"FileNameTxt= " + FileNameTxt + "\r\n";
            ret_string = ret_string + @"FileNameImg= " + FileNameImg + "\r\n";
            ret_string = ret_string + @"Published=   " + Published.ToString() + "\r\n";
            ret_string = ret_string + "\r\n";

            return ret_string;

        } // DebugMembers

    } // JazzDoc

} // namespace
