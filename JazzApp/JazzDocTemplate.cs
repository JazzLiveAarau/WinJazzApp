using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Holds data for a jazz document template
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public class JazzDocTemplate
    {
        /// <summary>Template name</summary>
        private string m_template_name = @"";
        /// <summary>Template name</summary>
        public string TemplateName { get { return m_template_name; } set { m_template_name = value; } }

        /// <summary>Template extensions</summary>
        private string m_template_extensions = @"";
        /// <summary>Template extensions</summary>
        public string TemplateExtensions { get { return m_template_extensions; } set { m_template_extensions = value; } }

        /// <summary>Template description</summary>
        private string m_template_description = @"";
        /// <summary>Template description</summary>
        public string TemplateDescription { get { return m_template_description; } set { m_template_description = value; } }

        /// <summary>Template instructions</summary>
        private string[] m_template_instructions = null;
        /// <summary>Template instructions</summary>
        public string[] TemplateInstructions { get { return m_template_instructions; } set { m_template_instructions = value; } }

        /// <summary>Template document type: season, concert or other</summary>
        private string m_template_document_type = @"";
        /// <summary>Template document type: season, concert or other</summary>
        public string TemplateDocumentType { get { return m_template_document_type; } set { m_template_document_type = value; } }

        /// <summary>Template document dialog: Program, DocPdf, DocPdfImg or XlsPdf</summary>
        private string m_template_document_dialog = @"";
        /// <summary>Template document dialog: Program, DocPdf, DocPdfImg or XlsPdf</summary>
        public string TemplateDocumentDialog { get { return m_template_document_dialog; } set { m_template_document_dialog = value; } }

        /// <summary>Template document dialog title</summary>
        private string m_template_document_dialog_title = @"";
        /// <summary>Template document dialog title</summary>
        public string TemplateDocumentDialogTitle { get { return m_template_document_dialog_title; } set { m_template_document_dialog_title = value; } }

        /// <summary>Template file path description</summary>
        private string m_template_file_path_description = @"";
        /// <summary>Template file path description</summary>
        public string TemplateFilePathDescription { get { return m_template_file_path_description; } set { m_template_file_path_description = value; } }

        /// <summary>Template file name DOC description</summary>
        private string m_template_file_name_doc_description = @"";
        /// <summary>Template file name DOC description</summary>
        public string TemplateFileNameDocDescription { get { return m_template_file_name_doc_description; } set { m_template_file_name_doc_description = value; } }

        /// <summary>Template file name XLS description</summary>
        private string m_template_file_name_xls_description = @"";
        /// <summary>Template file name XLS description</summary>
        public string TemplateFileNameXlsDescription { get { return m_template_file_name_xls_description; } set { m_template_file_name_xls_description = value; } }

        /// <summary>Template file name PDF description</summary>
        private string m_template_file_name_pdf_description = @"";
        /// <summary>Template file name PDF description</summary>
        public string TemplateFileNamePdfDescription { get { return m_template_file_name_pdf_description; } set { m_template_file_name_pdf_description = value; } }

        /// <summary>Template file name TXT description</summary>
        private string m_template_file_name_txt_description = @"";
        /// <summary>Template file name TXT description</summary>
        public string TemplateFileNameTxtDescription { get { return m_template_file_name_txt_description; } set { m_template_file_name_txt_description = value; } }

        /// <summary>Template file name IMG description</summary>
        private string m_template_file_name_img_description = @"";
        /// <summary>Template file name IMG description</summary>
        public string TemplateFileNameImgDescription { get { return m_template_file_name_img_description; } set { m_template_file_name_img_description = value; } }

        /// <summary>Template published description</summary>
        private string m_template_published_description = @"";
        /// <summary>Template published description</summary>
        public string TemplatePublishedDescription { get { return m_template_published_description; } set { m_template_published_description = value; } }

        /// <summary>Template name description</summary>
        private string m_template_name_description = @"";
        /// <summary>Template name description</summary>
        public string TemplateNameDescription { get { return m_template_name_description; } set { m_template_name_description = value; } }


        /// <summary>Check the template data</summary>
        public bool CheckInput(out string o_error)
        {
            o_error = @"";

            bool ret_check_input = true;

            if (m_template_document_type.Equals("season") || m_template_document_type.Equals("concert") )
            {
                ret_check_input = true;
            }
            else if (m_template_document_type.Equals("NotYetSetNodeValue"))
            {
                o_error = o_error + @"JazzDocTemplate.CheckInput Document type (season or concert) is not set." + @" ";
                ret_check_input = false;
            }
            else
            {
                o_error = o_error + @"JazzDocTemplate.CheckInput Unknown document type: " + m_template_document_type + @" ";
                ret_check_input = false;
            }

            // Program, DocPdf, DocPdfImg or XlsPdf

            if (   m_template_document_dialog.Equals("Program") 
                || m_template_document_dialog.Equals("DocPdf")
                || m_template_document_dialog.Equals("DocPdfImg")
                || m_template_document_dialog.Equals("XlsPdf") )
            {
                ret_check_input = true;
            }
            else if (m_template_document_dialog.Equals("NotYetSetNodeValue"))
            {
                o_error = o_error + @"JazzDocTemplate.CheckInput Document dialog (Program, DocPdf, DocPdfImg or XlsPdf) is not set." + @" ";
                ret_check_input = false;
            }
            else
            {
                o_error = o_error + @"JazzDocTemplate.CheckInput Unknown document dialog: " + m_template_document_dialog + @" ";
                ret_check_input = false;
            }

            return ret_check_input;

        } // CheckInput

        /// <summary>Returns the member variable values as a string</summary>
        public string DebugMembers()
        {
            string ret_string = @"JazzDoc member variables:" + "\r\n";

            ret_string = ret_string + @"TemplateName= " + TemplateName + "\r\n";
            ret_string = ret_string + @"TemplateExtensions=    " + TemplateExtensions + "\r\n";
            ret_string = ret_string + @"TemplateDescription= " + TemplateDescription + "\r\n";
            ret_string = ret_string + @"TemplateDocumentType= " + TemplateDocumentType + "\r\n";
            ret_string = ret_string + @"TemplateDocumentDialog= " + TemplateDocumentDialog + "\r\n";
            ret_string = ret_string + @"TemplateDocumentDialogTitle= " + TemplateDocumentDialogTitle + "\r\n";
            ret_string = ret_string + @"TODO Other members " + "\r\n";
            ret_string = ret_string + "\r\n";

            return ret_string;

        } // DebugMembers

    } // JazzDocTemplate

} // namespace
