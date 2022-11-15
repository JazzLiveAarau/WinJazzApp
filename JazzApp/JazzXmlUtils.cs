using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Low level functions for reading and writing XML values
    /// <para></para>
	/// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {
        #region Set values for single nodes

        /// <summary>Sets the value (the inner text) of a single node in the application XML document</summary>
        static private void SetInnerTextForApplicationNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagApplSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_document_application, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextForApplicationNode

        /// <summary>Sets the value (the inner text) of a single node in a season program XML document</summary>
        static private void SetInnerTextForConcertsNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagSeasonProgramSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_document_current, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextForConcertsNode

        /// <summary>Sets the inner text (the value) of a single node in the templates XML document</summary>
        static private void SetInnerTextTemplatesNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagTemplateSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_xdocument_templates, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextTemplatesNode

        /// <summary>Sets the inner text (the value) of a single node in the requests XML document</summary>
        static private void SetInnerTextReqSingleNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagReqSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_xdocument_req, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextReqSingleNode

        /// <summary>Sets the inner text (the value) of a single node in the news XML document</summary>
        static private void SetInnerTextNewsSingleNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagNewsSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_xdocument_news, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextNewsSingleNode

        /// <summary>Sets the inner text (the value) of a single node in the photo gallery one (JazzFotoGalerieEin.xml) XML document</summary>
        static private void SetInnerTextPhotoOneSingleNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_xdocument_photo_one, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextPhotoOneSingleNode

        /// <summary>Sets the inner text (the value) of a single node in the photo gallery two (JazzFotoGalerieZwei.xml) XML document</summary>
        static private void SetInnerTextPhotoTwoSingleNode(String i_tag_name, string i_inner_text)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_xdocument_photo_two, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextPhotoTwoSingleNode

        /// <summary>Sets the inner text (the value) of a single node in a season document XML file</summary>
        static private void SetInnerTextDocSeasonNode(string i_tag_name, string i_inner_text)
        {
            if (!CheckTagSeasonDocumentsSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetInnerTextForSingleNode(m_xdocument_active_doc, i_tag_name, i_inner_text, out error_message);

        } // SetInnerTextDocSeasonNode

        /// <summary>Sets the value (the inner text) of a single element node
        /// <para>The value of the first found node with the input tag name will be set</para>
        /// <para>(i.e. the function assumes that there is only one node with that name)</para>
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        /// <param name="i_x_document">XDocument corresponding to the application, season concerts, templates or season documents XML file</param>
        /// <param name="i_tag_name">Tag name</param>
        /// <param name="i_inner_text">Value (inner text) that shall be set</param>
        static private bool SetInnerTextForSingleNode(XDocument i_x_document, String i_tag_name, string i_inner_text, out string o_error)
        {
            o_error = @"";

            if (null == i_x_document)
            {
                o_error = @"JazzXml.SetInnerTextForSingleNode Input XDocument is null";
                return false;
            }

            try
            {
                XElement root_element = i_x_document.Root;


                XElement first_element = (from element in root_element.Descendants(i_tag_name)
                                          select element).First();

                if (null == first_element)
                {
                    o_error = @"JazzXml.SetInnerTextForSingleNode There is no node with tag name " + i_tag_name;
                    return false;
                }

                //QQ 2020-02-28 first_element.Value = i_inner_text;
                //QQ 2019-08-19 first_element.Value = ModifyWriteXml(i_inner_text);
                first_element.Value = SetUndefinedValueForEmptyString(i_inner_text);
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.SetInnerTextForSingleNode " + e.ToString();
                return false;
            }

            return true;

        } // SetInnerTextForSingleNode

        #endregion // Set values for single nodes

        #region Get values for single nodes

        /// <summary>Returns the value (the inner text) of a single node in the templates XML document</summary>
        static private String GetInnerTextTemplatesNode(String i_tag_name)
        {
            if (!CheckTagTemplateSingle(i_tag_name))
                return @"JazzXml.GetInnerTextTemplatesNode Not a defined tag name " + i_tag_name;

            return GetInnerTextForSingleNode(m_xdocument_templates, i_tag_name);

        } // GetInnerTextTemplatesNode

        /// <summary>Returns the value (the inner text) of a single node in the requests XML document</summary>
        static private String GetInnerTextReqSingleNode(String i_tag_name)
        {
            if (!CheckTagReqSingle(i_tag_name))
                return @"JazzXml.GetInnerTextReqSingleNode Not a defined tag name " + i_tag_name;

            return GetInnerTextForSingleNode(m_xdocument_req, i_tag_name);

        } // GetInnerTextReqSingleNode

        /// <summary>Returns the value (the inner text) of a single node in the news XML document</summary>
        static private String GetInnerTextNewsSingleNode(String i_tag_name)
        {
            if (!CheckTagNewsSingle(i_tag_name))
                return @"JazzXml.GetInnerTextNewsSingleNode Not a defined tag name " + i_tag_name;

            return GetInnerTextForSingleNode(m_xdocument_news, i_tag_name);

        } // GetInnerTextNewsSingleNode

        /// <summary>Returns the value (the inner text) of a single node in the photo gallery one XML document</summary>
        static private String GetInnerTextPhotoOneSingleNode(String i_tag_name)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return @"JazzXml.GetInnerTextPhotoOneSingleNode Not a defined tag name " + i_tag_name;

            return GetInnerTextForSingleNode(m_xdocument_photo_one, i_tag_name);

        } // GetInnerTextPhotoOneSingleNode

        /// <summary>Returns the value (the inner text) of a single node in the photo gallery two XML document</summary>
        static private String GetInnerTextPhotoTwoSingleNode(String i_tag_name)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return @"JazzXml.GetInnerTextPhotoTwoSingleNode Not a defined tag name " + i_tag_name;

            return GetInnerTextForSingleNode(m_xdocument_photo_two, i_tag_name);

        } // GetInnerTextPhotoTwoSingleNode

        /// <summary>Returns the value (the inner text) of a single node in a season documents XML file</summary>
        static private string GetInnerTextDocSeasonNode(string i_tag_name)
        {
            if (!CheckTagSeasonDocumentsSingle(i_tag_name))
                return @"JazzXml.GetInnerTextDocSeasonNode Not a defined tag name " + i_tag_name;

            return GetInnerTextForSingleNode(m_xdocument_active_doc, i_tag_name);

        } // GetInnerTextDocSeasonNode


        /// <summary>Gets the value (the inner text) of a single element node
        /// <para>The value of the first found node with the input tag name will be returned</para>
        /// <para>(i.e. the function assumes that there is only one node with that name)</para>
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        /// <param name="i_x_document">XDocument corresponding to the application, season concerts, templates or season documents XML file</param>
        /// <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextForSingleNode(XDocument i_x_document, String i_tag_name)
        {
            String ret_inner_text = "";

            if (null == i_x_document)
                return @"JazzXml.GetInnerTextForSingleNode Input XDocument is null";

            try
            {
                XElement root_element = i_x_document.Root;

                XElement first_element = (from element in root_element.Descendants(i_tag_name)
                                          select element).First();

                ret_inner_text = first_element.Value;

                //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);
            }
            catch (Exception e)
            {
                return @"JazzXml.GetInnerTextForSingleNode " + e.ToString();
            }

            return ret_inner_text;

        } // GetInnerTextForSingleNode

        #endregion // Get values for single nodes

        #region Set values for first level elements

        /// <summary>Sets the member inner text for a given tag name</summary>
        ///  <param name="i_member">Member number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_member_data">Member data (inner text) to set</param>
        static private void SetMemberInnerText(int i_member, String i_tag_name, String i_member_data)
        {
            if (!CheckTagMember(i_tag_name))
                return;

            // TODO Define @"Member" as tag name

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_document_application, @"Member", i_member, i_tag_name, i_member_data, out error_message);

        } // SetMemberInnerText

        /// <summary>Sets the value (inner text) of a season concert node</summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private void SetInnerTextForNode(int i_concert, String i_tag_name, string i_concert_data)
        {
            if (!CheckTagConcert(i_tag_name))
                return;

            // TODO Define @"Concert" as tag name

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_document_current, @"Concert", i_concert, i_tag_name, i_concert_data, out error_message);

        } // SetInnerTextForNode


        /// <summary>Sets the inner text of the node for the current template in the templates XML document</summary>
        ///  <param name="i_template">Template number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_template_data">Data (value) that shall be set</param>
        static private void SetInnerTextTemplateNode(int i_template, String i_tag_name, string i_template_data)
        {
            if (!CheckTagTemplateData(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_templates, GetTagTemplsTemplate(), i_template, i_tag_name, i_template_data, out error_message);

        } // SetInnerTextTemplateNode

        /// <summary>Sets the inner text of a request node for the requests XML document (corresponding to XML file JazzAnfrage.xml)</summary>
        ///  <param name="i_req">Request number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_req_data">Data (value) that shall be set</param>
        static private void SetInnerTextReqNode(int i_req, String i_tag_name, string i_req_data)
        {
            if (!CheckTagReq(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_req, GetTagReqRequest(), i_req, i_tag_name, i_req_data, out error_message);

        } // SetInnerTextReqNode

        /// <summary>Sets the inner text of a newsletter node for the newsletters XML document (corresponding to XML file JazzNewsletter.xml)</summary>
        ///  <param name="i_newsletter">Newsletter number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_newsletter_data">Data (value) that shall be set</param>
        static private void SetInnerTextNewsletterNode(int i_newsletter, String i_tag_name, string i_newsletter_data)
        {
            if (!CheckTagNewsletter(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_newsletter, GetTagNewsletter(), i_newsletter, i_tag_name, i_newsletter_data, out error_message);

        } // SetInnerTextNewsletterNode

        /// <summary>Sets the inner text of a news node for the news XML document (corresponding to XML file JazzNews.xml)</summary>
        ///  <param name="i_news_number">Current news number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_news_data">Data (value) that shall be set</param>
        static private void SetInnerTextCurrentNewsNode(int i_news_number, String i_tag_name, string i_news_data)
        {
            if (!CheckTagCurrentNews(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_news, GetTagCurrentNews(), i_news_number, i_tag_name, i_news_data, out error_message);

        } // SetInnerTextCurrentNewsNode

        /// <summary>Sets the inner text of a news node for the news XML document (corresponding to XML file JazzNews.xml)</summary>
        ///  <param name="i_news_number">Concert news number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_news_data">Data (value) that shall be set</param>
        static private void SetInnerTextConcertNewsNode(int i_news_number, String i_tag_name, string i_news_data)
        {
            if (!CheckTagCurrentNews(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_news, GetTagConcertNews(), i_news_number, i_tag_name, i_news_data, out error_message);

        } // SetInnerTextConcertNewsNode

        /// <summary>Sets the inner text of a request node for the photo gallery one XML document (corresponding to XML file JazzFotoGalerieEin.xml)</summary>
        ///  <param name="i_season">Season number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_photo_data">Data (value) that shall be set</param>
        static private void SetInnerTextPhotoOneNode(int i_season, String i_tag_name, string i_photo_data)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_photo_one, GetTagPhotosSeason(), i_season, i_tag_name, i_photo_data, out error_message);

        } // SetInnerTextPhotoOneNode

        /// <summary>Sets the inner text of a request node for the photo gallery two XML document (corresponding to XML file JazzFotoGalerieZwei.xml)</summary>
        ///  <param name="i_season">Season number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_photo_data">Data (value) that shall be set</param>
        static private void SetInnerTextPhotoTwoNode(int i_season, String i_tag_name, string i_photo_data)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_photo_two, GetTagPhotosSeason(), i_season, i_tag_name, i_photo_data, out error_message);

        } // SetInnerTextPhotoTwoNode

        /// <summary>Sets the inner text of the node for the input concert in the current season XML document</summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_concert_doc_data">Data (value) that shall be set</param>
        static private void SetInnerTextDocConcertNode(int i_concert, String i_tag_name, string i_concert_doc_data)
        {
            if (!CheckTagDocConcert(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetFirstLevelInnerText(m_xdocument_active_doc, GetTagDocConcert(), i_concert, i_tag_name, i_concert_doc_data, out error_message);

        } // SetInnerTextDocConcertNode


        /// <summary>Sets the inner text of the node for the input concert in the current season XML document</summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_concert_doc_data">Data (value) that shall be set</param>
        static private void SetInnerTextDocConcertDocumentNode(int i_concert, int i_object, String i_tag_name, string i_concert_doc_data)
        {
            if (!CheckTagDocConcert(i_tag_name))
                return;

            string error_message = @"";

            bool b_set_inner_text = SetSecondLevelInnerText(m_xdocument_active_doc, GetTagDocConcert(), GetTagDocConcertDocument(), i_concert, i_object, i_tag_name, i_concert_doc_data, out error_message);

        } // SetInnerTextDocConcertDocumentNode

        /// <summary>Sets the first level value (inner text) for a given tag name
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        /// <param name="i_x_document">XDocument corresponding to the application, season concerts, templates or season documents XML file</param>
        /// <param name="i_first_level_tag_name">First level tag name</param>
        ///  <param name="i_element_number">Element number (1, 2, 3, ....)</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_value">Value (inner text) to be set</param>
        static private bool SetFirstLevelInnerText(XDocument i_x_document, string i_first_level_tag_name, int i_element_number, String i_tag_name, String i_value, out string o_error)
        {
            o_error = @"";

            if (null == i_x_document)
            {
                o_error = @"JazzXml.SetFirstLevelInnerText Input XDocument is null";
                return false;
            }

            if (i_element_number <= 0)
            {
                o_error = @"JazzXml.SetFirstLevelInnerText Element number <= 0: i_element_number= " + i_element_number.ToString();
                return false;
            }

            try
            {
                int current_element_number = 0;
                foreach (XElement element_member in i_x_document.Descendants(i_first_level_tag_name))
                {
                    current_element_number = current_element_number + 1;
                    if (i_element_number == current_element_number)
                    {
                        XElement first_element = (from el in element_member.Descendants(i_tag_name)
                                                  select el).First();

                        //QQ 2020-02-28 first_element.Value = i_value;
                        //QQ 2019-08-19 first_element.Value = ModifyWriteXml(i_value);
                        first_element.Value = SetUndefinedValueForEmptyString(i_value);

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.SetFirstLevelInnerText " + e.ToString();
                return false;
            }

            o_error = @"JazzXml.SetFirstLevelInnerText No tag name " + i_tag_name;
            return false;

        } // SetFirstLevelInnerText

        #endregion // Set values for first level elements

        #region Get values for first level elements

        /// <summary>Returns the value (inner text) of a first level node in the templates XML document</summary>
        ///  <param name="i_template">Template number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextTemplateNode(int i_template, String i_tag_name)
        {
            // Temporary if (!CheckTagTemplateData(i_tag_name))
                //Temporary return "JazzXml.GetInnerTextTemplateNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_templates, GetTagTemplsTemplate(), i_template, i_tag_name);

        } // GetInnerTextTemplateNode

        /// <summary>Returns the value (inner text) of a first level node in the requests XML document (corresponding to file JazzAnfrage.xml)</summary>
        ///  <param name="i_req">Request number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextReqNode(int i_req, String i_tag_name)
        {
            if (!CheckTagReq(i_tag_name))
                return "JazzXml.GetInnerTextReqNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_req, GetTagReqRequest(), i_req, i_tag_name);

        } // GetInnerTextReqNode

        /// <summary>Returns the value (inner text) of a first level node in the newsletter XML document (corresponding to file JazzNewsletter.xml)</summary>
        ///  <param name="i_newsletter">Request number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextNewsletterNode(int i_newsletter, String i_tag_name)
        {
            if (!CheckTagNewsletter(i_tag_name))
                return "JazzXml.GetInnerTextNewsletterNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_newsletter, GetTagNewsletter(), i_newsletter, i_tag_name);

        } // GetInnerTextNewsletterNode

        /// <summary>Returns the value (inner text) of a first level node in the news XML document (corresponding to file JazzNews.xml)</summary>
        ///  <param name="i_news_number">Current news number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextCurrentNewsNode(int i_news_number, String i_tag_name)
        {
            if (!CheckTagCurrentNews(i_tag_name))
                return "JazzXml.GetInnerTextCurrentNewsNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_news, GetTagCurrentNews(), i_news_number, i_tag_name);

        } // GetInnerTextCurrentNewsNode

        /// <summary>Returns the value (inner text) of a first level node in the news XML document (corresponding to file JazzNews.xml)</summary>
        ///  <param name="i_news_number">Concert news number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextConcertNewsNode(int i_news_number, String i_tag_name)
        {
            if (!CheckTagCurrentNews(i_tag_name))
                return "JazzXml.GetInnerTextConcertNewsNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_news, GetTagConcertNews(), i_news_number, i_tag_name);

        } // GetInnerTextConcertNewsNode

        /// <summary>Returns the value (inner text) of a first level node in the photo gallery one XML document (corresponding to file JazzFotoGalerieEin.xml)</summary>
        ///  <param name="i_season">Season number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextPhotoOneNode(int i_season, String i_tag_name)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return "JazzXml.GetInnerTextPhotoOneNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_photo_one, GetTagPhotosSeason(), i_season, i_tag_name);

        } // GetInnerTextPhotoOneNode

        /// <summary>Returns the value (inner text) of a first level node in the photo gallery two XML document (corresponding to file JazzFotoGalerieZwei.xml)</summary>
        ///  <param name="i_season">Season number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextPhotoTwoNode(int i_season, String i_tag_name)
        {
            if (!CheckTagPhotoSingle(i_tag_name))
                return "JazzXml.GetInnerTextPhotoTwoNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_photo_two, GetTagPhotosSeason(), i_season, i_tag_name);

        } // GetInnerTextPhotoTwoNode

        /// <summary>Returns the value (inner text) of a season document node</summary>
        ///  <param name="i_season">Season document number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextSeasonNode(int i_season, String i_tag_name)
        {
            if (!CheckTagDocConcert(i_tag_name))
                return "JazzXml.GetInnerTextSeasonNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_active_doc, GetTagDocSeasonDocument(), i_season, i_tag_name);

        } // GetInnerTextSeasonNode


        /// <summary>Returns the value (inner text) of a first level node in the season documents XML file</summary>
        ///  <param name="i_concert">Concert number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextDocConcertNode(int i_concert, String i_tag_name)
        {
            // TODO if (!CheckTagDocConcert(i_tag_name))
                //TODO return "JazzXml.GetInnerTextDocConcertNode Not a defined tag name " + i_tag_name;

            return GetFirstLevelInnerText(m_xdocument_active_doc, GetTagDocConcert(), i_concert, i_tag_name);

        } // GetInnerTextDocConcertNode

        /// <summary>Returns the number of templates</summary>
        public static int GetNumberOfTemplates(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_templates, GetTagTemplsTemplate(), out o_error);
        } // GetNumberOfTemplates

        /// <summary>Returns the number of documents</summary>
        public static int GetNumberOfDocuments(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_active_doc, GetTagDocConcert(), out o_error);
        } // GetNumberOfDocuments

        /// <summary>Returns the number of season documents</summary>
        public static int GetNumberOfSeasonDocuments(out string o_error)
        {
            return GetNumberOfFirstLevelElements(m_xdocument_active_doc, GetTagDocSeasonDocument(), out o_error);
        } // GetNumberOfSeasonDocuments

        /// <summary>Gets the first level value (inner text) for a given tag name
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        /// <param name="i_x_document">XDocument corresponding to the application, season concerts, templates or season documents XML file</param>
        /// <param name="i_first_level_tag_name">First level tag name</param>
        ///  <param name="i_element_number">Element number (1, 2, 3, ....)</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetFirstLevelInnerText(XDocument i_x_document, string i_first_level_tag_name, int i_element_number, String i_tag_name)
        {
            string ret_inner_text = @"";

            if (null == i_x_document)
                return "JazzXml.GetFirstLevelInnerText Input XDocument is null";

            if (i_element_number <= 0)
                return "JazzXml.GetFirstLevelInnerText Element number " + i_element_number.ToString() + " <=0";

            try
            {
                int current_element_number = 0;
                foreach (XElement element_template in i_x_document.Descendants(i_first_level_tag_name))
                {
                    current_element_number = current_element_number + 1;
                    if (i_element_number == current_element_number)
                    {
                        XElement first_element = (from el in element_template.Descendants(i_tag_name)
                                                  select el).First();

                        ret_inner_text = first_element.Value;

                        //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

                        return ret_inner_text;

                    } // i_element_number == current_element_number

                } // Loop all first level elements
            }
            catch (Exception e)
            {
                return @"JazzXml.GetFirstLevelInnerText " + e.ToString();
            }

            return "JazzXml.GetFirstLevelInnerText Not existing element number " + i_element_number.ToString();

        } // GetFirstLevelInnerText

        /// <summary>Returns the number of first level elements</summary>
        private static int GetNumberOfFirstLevelElements(XDocument i_x_document, string i_first_level_tag_name, out string o_error)
        {
            o_error = @"";

            int ret_number_first_level_elements = 0;

            if (null == i_x_document)
            {
                o_error = @"JazzXml.GetNumberOfFirstLevelElements Input XDocument is null";
                return -1;
            }

            try
            {
                foreach (XElement element_first_level in i_x_document.Descendants(i_first_level_tag_name))
                {
                    ret_number_first_level_elements = ret_number_first_level_elements + 1;

                } // First level elements
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.GetNumberOfFirstLevelElements " + e.ToString();
                return -3;
            }

            return ret_number_first_level_elements;

        } // GetNumberOfFirstLevelElements

        #endregion // Get values for first level elements

        #region Set values for second level elements

        /// <summary>Sets the value (inner text) for a musician element node</summary>
        ///  <param name="i_concert">Concert number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_musician">Musician number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_musician_data">Text data that shall be set</param>
        static private void SetMusicianInnerText(int i_concert, int i_musician, String i_tag_name, string i_musician_data)
        {
            if (!CheckTagMusician(i_tag_name))
                return;

            // TODO Define tag names "Concert" and "Musician"
            string error_message = @"";
            bool b_set_inner_text = SetSecondLevelInnerText(m_document_current, @"Concert", @"Musician", i_concert, i_musician, i_tag_name, i_musician_data, out error_message);

        } // SetMusicianInnerText

        /// <summary>Sets the value (inner text) for a document element node</summary>
        ///  <param name="i_concert">Concert number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_document">Document number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_document_data">Text data that shall be set</param>
        static private void SetDocumentInnerText(int i_concert, int i_document, String i_tag_name, string i_document_data)
        {
            if (!CheckTagMusician(i_tag_name))
                return;

            // TODO Define tag name "Document"
            string error_message = @"";
            bool b_set_inner_text = SetSecondLevelInnerText(m_xdocument_active_doc, GetTagDocConcert(), @"Document", i_concert, i_document, i_tag_name, i_document_data, out error_message);

        } // SetDocumentInnerText

        /// <summary>Sets the value (inner text) for a photo gallery one (JazzFotoGalerieEin.xml) element node</summary>
        ///  <param name="i_season">Season number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_concert">Concert number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_photo_data">Text data that shall be set</param>
        static private void SetPhotoOneInnerText(int i_season, int i_concert, String i_tag_name, string i_photo_data)
        {
            if (!CheckTagPhoto(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetSecondLevelInnerText(m_xdocument_photo_one, GetTagPhotosSeason(), GetTagPhotoConcert(), i_season, i_concert, i_tag_name, i_photo_data, out error_message);

        } // SetPhotoOneInnerText

        /// <summary>Sets the value (inner text) for a photo gallery two (JazzFotoGalerieZwei.xml) element node</summary>
        ///  <param name="i_season">Season number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_concert">Concert number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_photo_data">Text data that shall be set</param>
        static private void SetPhotoTwoInnerText(int i_season, int i_concert, String i_tag_name, string i_photo_data)
        {
            if (!CheckTagPhoto(i_tag_name))
                return;

            string error_message = @"";
            bool b_set_inner_text = SetSecondLevelInnerText(m_xdocument_photo_two, GetTagPhotosSeason(), GetTagPhotoConcert(), i_season, i_concert, i_tag_name, i_photo_data, out error_message);

        } // SetPhotoTwoInnerText

        /// <summary>Sets the value (inner text) for a second level node
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_first_level_number">First level number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_second_level_number">Second level number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_value">Value (inner text) that shall be set</param>
        ///  <param name="o_error">Error message</param>
        static private bool SetSecondLevelInnerText(XDocument i_x_document, string i_first_level_tag_name, string i_second_level_tag_name, int i_first_level_number, int i_second_level_number, String i_tag_name, string i_value, out string o_error)
        {
            o_error = @"";

            if (null == i_x_document)
            {
                o_error = @"JazzXml.SetSecondLevelInnerText Input XDocument is null";
                return false;
            }

            if (i_first_level_number <= 0)
            {
                o_error = @"JazzXml.SetSecondLevelInnerText First level number <= 0";
                return false;
            }

            if (i_second_level_number <= 0)
            {
                o_error = @"JazzXml.SetSecondLevelInnerText Second level number <= 0";
                return false;
            }

            try
            {
                int current_first_level_number = 0;
                int current_second_level_number = 0;
                foreach (XElement element_first_level in i_x_document.Descendants(i_first_level_tag_name))
                {
                    current_first_level_number = current_first_level_number + 1;
                    if (i_first_level_number == current_first_level_number)
                    {
                        foreach (XElement element_second_level in element_first_level.Descendants(i_second_level_tag_name))
                        {
                            current_second_level_number = current_second_level_number + 1;
                            if (i_second_level_number == current_second_level_number)
                            {
                                XElement first_element = (from el in element_second_level.Descendants(i_tag_name)
                                                          select el).First();

                                //QQ 2020-02-28 first_element.Value = i_value;
                                //QQ 2019-08-19 first_element.Value = ModifyWriteXml(i_value);
                                first_element.Value = SetUndefinedValueForEmptyString(i_value);

                                return true;

                            } // Second level number exists

                        } // element_musician
                    } // First level number exists
                } // element_concert
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.SetSecondLevelInnerText " + e.ToString();
                return false;
            }

            o_error = @"JazzXml.SetSecondLevelInnerText No tag name " + i_tag_name;
            return false;

        } // SetSecondLevelInnerText

        #endregion // Set values for second level elements

        #region Get values for second level elements

        /// <summary>Returns the value (inner text) for a document element node</summary>
        ///  <param name="i_concert">Concert number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_document">Document number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetDocumentInnerText(int i_concert, int i_document, String i_tag_name)
        {
            if (!CheckTagDocConcert(i_tag_name))
                return "JazzXml.GetDocumentInnerText Not a defined tag name " + i_tag_name;

            // TODO Define tag name "Document"
            return GetSecondLevelInnerText(m_xdocument_active_doc, GetTagDocConcert(), @"Document", i_concert, i_document, i_tag_name);

        } // GetDocumentInnerText

        /// <summary>Returns the number of documents in the active XML document object</summary>
        public static int GetNumberOfDocuments(int i_concert, out string o_error)
        {
            return GetNumberOfSecondLevelElements(m_xdocument_active_doc, GetTagDocConcert(), @"Document", i_concert, out o_error);
        } // GetNumberOfDocuments


        /// <summary>Returns the value (inner text) for a second level node
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_first_level_number">First level number 1, 2, 3, 4, 5, ....</param>
        ///  <param name="i_second_level_number">Second level number 1, 2, 3, ....</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetSecondLevelInnerText(XDocument i_x_document, string i_first_level_tag_name, string i_second_level_tag_name, int i_first_level_number, int i_second_level_number, String i_tag_name)
        {
            string ret_inner_text = @"";

            if (null == i_x_document)
            {
                return @"JazzXml.GetSecondLevelInnerText Input XDocument is null";
            }

            if (i_first_level_number <= 0)
            {
                return @"JazzXml.GetSecondLevelInnerText First level number <= 0";
            }

            if (i_second_level_number <= 0)
            {
                return @"JazzXml.GetSecondLevelInnerText Second level number <= 0";
            }

            try
            {
                int current_first_level_number = 0;
                int current_second_level_number = 0;
                foreach (XElement element_first_level in i_x_document.Descendants(i_first_level_tag_name))
                {
                    current_first_level_number = current_first_level_number + 1;
                    if (i_first_level_number == current_first_level_number)
                    {
                        foreach (XElement element_second_level in element_first_level.Descendants(i_second_level_tag_name))
                        {
                            current_second_level_number = current_second_level_number + 1;
                            if (i_second_level_number == current_second_level_number)
                            {
                                XElement first_element = (from el in element_second_level.Descendants(i_tag_name)
                                                          select el).First();

                                ret_inner_text = first_element.Value;

                                //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

                                return ret_inner_text;

                            } // Second level number exists

                        } // element_musician
                    } // First level number exists
                } // element_concert
            }
            catch (Exception e)
            {
                return @"JazzXml.SetSecondLevelInnerText " + e.ToString();
            }

            return @"JazzXml.GetSecondLevelInnerText No tag name " + i_tag_name;

        } // GetSecondLevelInnerText

        /// <summary>Returns the number of second level elements</summary>
        private static int GetNumberOfSecondLevelElements(XDocument i_x_document, string i_first_level_tag_name, string i_second_level_tag_name, int i_first_level_number, out string o_error)
        {
            o_error = @"";

            int n_number_first_elements = GetNumberOfFirstLevelElements(i_x_document, i_first_level_tag_name, out o_error);
            if (n_number_first_elements < 0)
            {
                o_error = @"JazzXml.GetNumberOfSecondLevelElements Number of first elements is " + n_number_first_elements.ToString() + @" < 0";
                return -4;
            }
            if (n_number_first_elements == 0)
            {
                o_error = @"JazzXml.GetNumberOfSecondLevelElements Number of first elements is " + n_number_first_elements.ToString();
                return -5;
            }

            if (i_first_level_number <= 0 || i_first_level_number > n_number_first_elements)
            {
                o_error = @"JazzXml.GetNumberOfSecondLevelElements Number of first elements " + n_number_first_elements.ToString() + @" is <= 0 or > n_number_first_elements= " + n_number_first_elements.ToString();
                return -6;
            }

            int ret_number_second_level_elements = 0;

            if (null == i_x_document)
            {
                o_error = @"JazzXml.GetNumberOfSecondLevelElements Input XDocument is null";
                return -1;
            }

            try
            {
                int current_first_level_number = 0;
                foreach (XElement element_first_level in i_x_document.Descendants(i_first_level_tag_name))
                {
                    current_first_level_number = current_first_level_number + 1;
                    if (i_first_level_number == current_first_level_number)
                    {
                        foreach (XElement element_second_level in element_first_level.Descendants(i_second_level_tag_name))
                        {
                            ret_number_second_level_elements = ret_number_second_level_elements + 1;
                        }

                        return ret_number_second_level_elements;
                    }

                } // First level element
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.GetNumberOfSecondLevelElements " + e.ToString();
                return -3;
            }

            o_error = @"JazzXml.GetNumberOfSecondLevelElements No first level tag name or no second level elements";
            return -2;

        } // GetNumberOfSecondLevelElements

        #endregion // Get values for second level elements

        #region Remove first level element

        /// <summary>Remove a first level element defined by the element number</summary>
        private static bool RemoveFirstLevelElement(XDocument i_x_document, string i_first_level_tag_name, int i_delete_element_number, out string o_error)
        {
            o_error = @"";

            int n_first_elements = GetNumberOfFirstLevelElements(i_x_document, i_first_level_tag_name, out o_error);
            if (n_first_elements < 0)
            {
                o_error = @"JazzXml.RemoveFirstLevelElement GetNumberOfFirstLevelElements failed " + o_error;

                return false;
            }

            if (i_delete_element_number <= 0 || i_delete_element_number > n_first_elements)
            {
                o_error = @"JazzXml.RemoveFirstLevelElement Input element number " + i_delete_element_number.ToString() + " is not between 1 and " + n_first_elements.ToString();

                return false;
            }


            int current_element_number = 0;

            try
            {
                foreach (XElement element_first_level in i_x_document.Descendants(i_first_level_tag_name))
                {
                    current_element_number = current_element_number + 1;

                    if (current_element_number == i_delete_element_number)
                    {
                        element_first_level.Remove();

                        return true;
                    }

                } // Loop first level elements
            }
            catch (Exception e)
            {
                o_error = @"JazzXml.RemoveFirstLevelElement " + e.ToString();

                return false;
            }

            o_error = @"JazzXml.RemoveFirstLevelElement Delete of element failed";

            return false;

        } // RemoveFirstLevelElement

        #endregion // Remove first level element

        #region Add first level element
 
        /// <summary>Add a first level element</summary>
        private static bool AddFirstLevelElement(XDocument i_x_document, XElement i_first_level_element, out string o_error)
        {
            o_error = @"";

            if (null == i_x_document)
            {
                o_error = @"JazzXml.AddFirstLevelElement Input XML document is null";

                return false;
            }

            if (null == i_first_level_element)
            {
                o_error = @"JazzXml.AddFirstLevelElement Input XML element is null";

                return false;
            }

            XElement root_element = i_x_document.Root;

            root_element.Add(i_first_level_element);

            return true;

        } // AddFirstLevelElement

        #endregion // Add first level element


        #region Get and set help document template functions





        #endregion //  Get and set help document template functions

        #region Get and set help season document functions

        /// <summary>Gets the inner text of the node for the input season document number in the active season XML document
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_season_doc_number">Season document number</param>
        ///  <param name="i_tag_name">Tag name</param>
        static private string GetInnerTextDocSeasonDocumentNode(int i_season_doc_number, String i_tag_name)
        {
            string ret_inner_text = @"";

            if (null == m_xdocument_active_doc)
                return "GetInnerTextDocSeasonDocumentNode Programming error m_xdocument_active_doc=0";

            if (i_season_doc_number <= 0)
                return "GetInnerTextDocSeasonDocumentNode Programming error i_template=0 " + i_season_doc_number.ToString() + " <=0";

            int current_season_document_number = 0;
            foreach (XElement element_season_doc in m_xdocument_active_doc.Descendants(GetTagDocSeasonDocument()))
            {
                current_season_document_number = current_season_document_number + 1;
                if (i_season_doc_number == current_season_document_number)
                {
                    XElement first_element = (from el in element_season_doc.Descendants(i_tag_name)
                                              select el).First();

                    ret_inner_text = first_element.Value;

                    //QQ 2019-08-19 ret_inner_text = ModifyReadXml(ret_inner_text);

                    return ret_inner_text;

                } // i_season_doc_number == current_season_document_number

            } // Loop all season document elements

            return "GetInnerTextDocSeasonDocumentNode Programming error Not existing season document number i_season_doc_number= " + i_season_doc_number.ToString();

        } // GetInnerTextDocSeasonDocumentNode


        /// <summary>Sets the inner text of the node for the input season document number in the active season XML document
        /// <para>Please note that the called function XElement.Value escapes &, <, > and "</para>
        /// </summary>
        ///  <param name="i_season_doc_number">Season document number</param>
        ///  <param name="i_tag_name">Tag name</param>
        ///  <param name="i_season_doc_value">Value to set</param>
        static private void SetInnerTextDocSeasonDocumentNode(int i_season_doc_number, String i_tag_name, string i_season_doc_value)
        {
            if (null == m_xdocument_active_doc)
                return;

            if (i_season_doc_number <= 0)
                return;

            int current_season_document_number = 0;
            foreach (XElement element_season_doc in m_xdocument_active_doc.Descendants(GetTagDocSeasonDocument()))
            {
                current_season_document_number = current_season_document_number + 1;
                if (i_season_doc_number == current_season_document_number)
                {
                    XElement first_element = (from el in element_season_doc.Descendants(i_tag_name)
                                              select el).First();

                    //QQ 2020-02-28 first_element.Value = i_season_doc_value;
                    //QQ 2019-08-19 first_element.Value = ModifyWriteXml(i_season_doc_value);
                    first_element.Value = SetUndefinedValueForEmptyString(i_season_doc_value);

                    break;

                } // i_season_doc_number == current_season_document_number

            } // Loop all season document elements

        } // SetInnerTextDocSeasonDocumentNode

        #endregion //  Get and set help season document functions

    } // JazzXml

} // namespace
