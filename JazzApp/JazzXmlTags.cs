using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Defines XML tag names and some XML values
    /// <para></para>
	/// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>This class is a partial class making it possible to add write XML functions for the JazzAdmin application</para>
    /// </remarks>
    static public partial class JazzXml
    {
        #region Tags for the single elements in the application XML file

        /// <summary>Defines the text tags in the application XML file that can be written</summary>
        static private string[] m_text_tags_appl =
        {
            @"AboutUsHeader", // 0 Done
            @"AboutUsOne", // 1 Done
            @"AboutUsTwo", // 2 Done
            @"AboutUsThree", // 3 Done
            @"PremisesHeader", // 4 Done
            @"Premises", // 5 Done
            @"PremisesStreet", // 6 Done
            @"PremisesCity", // 7 Done
            @"PremisesWebsite", // 8 Done
            @"PremisesTelephone", // 9 Done
            @"PremisesPhoto", // 10 Done
            @"PremisesMap", // 11 Done
            @"ContactsHeader", // 12 Done
            @"MailHeader", // 13 Done
            @"EmailHeader", // 14 Done
            @"ReservationHeader", // 15 Done
            @"NewsletterHeader", // 16 Done
            @"WebmasterHeader", // 17 Done
            @"ClubName", // 18 Done
            @"MailAddress", // 19 Done
            @"EmailJazzLiveAarau", // 20 Done
            @"EmailReservation", // 21 Done
            @"ReservationSubject", // 22 Done
            @"ReservationText", // 23 Done
            @"NewsletterSubject", // 24 Done
            @"NewsletterText", // 25 Done
            @"TelephoneWebmaster", // 26 Done
            @"EmailWebmaster", // 27 Done
            @"ContactConcertMemberNumber", // 28 Done
            @"ContactConcertTelephone", // 29 Done
            @"ContactConcertEmail", // 30 Done
            @"UnloadStreet", // 31 Done
            @"UnloadCity", // 32 Done
            @"ParkingOne", // 33 Done
            @"ParkingTwo", // 34 Done
            @"PublishSeasonStartYear", // 35 Done
            @"RequestCaption", // 36
            @"RequestHeader", // 37
            @"RequestDatesDisplay", // 38
            @"RequestNoDatesText", // 39
            @"RequestDatesText", // 40
            @"RequestContentHeader", // 41
            @"RequestContentOne", // 42
            @"RequestContentTwo", // 43
            @"RequestContentThree", // 44
            @"RequestContentFour", // 45
            @"RequestContentFive", // 46
            @"RequestContentSix", // 47
            @"RequestContentSeven", // 48
            @"RequestContentEight", // 49
            @"RequestContentNine", // 50
            @"RequestEmailAddress", // 51
            @"RequestEmailTitle", // 52
            @"RequestEmailCaption", // 53
            @"RequestEmailRemark", // 54
            @"RequestEndParagraph", // 55
            @"ReservationNotAllowed", // 56
            @"ReservationNotAllowedText", // 57

        }; // m_text_tags_appl


        #endregion // Tags for the single elements in the application XML file       

        #region Get tags for the single elements in the application XML file

        static public string GetTagApplAboutUsHeader() { return m_text_tags_appl[0]; }
        static public string GetTagApplAboutUsOne() { return m_text_tags_appl[1]; }
        static public string GetTagApplAboutUsTwo() { return m_text_tags_appl[2]; }
        static public string GetTagApplAboutUsThree() { return m_text_tags_appl[3]; }
        static public string GetTagApplPremisesHeader() { return m_text_tags_appl[4]; }
        static public string GetTagApplPremises() { return m_text_tags_appl[5]; }
        static public string GetTagApplPremisesStreet() { return m_text_tags_appl[6]; }
        static public string GetTagApplPremisesCity() { return m_text_tags_appl[7]; }
        static public string GetTagApplPremisesWebsite() { return m_text_tags_appl[8]; }
        static public string GetTagApplPremisesTelephone() { return m_text_tags_appl[9]; }
        static public string GetTagApplPremisesPhoto() { return m_text_tags_appl[10]; }
        static public string GetTagApplPremisesMap() { return m_text_tags_appl[11]; }
        static public string GetTagApplContactsHeader() { return m_text_tags_appl[12]; }
        static public string GetTagApplMailHeader() { return m_text_tags_appl[13]; }
        static public string GetTagApplEmailHeader() { return m_text_tags_appl[14]; }
        static public string GetTagApplReservationHeader() { return m_text_tags_appl[15]; }
        static public string GetTagApplNewsletterHeader() { return m_text_tags_appl[16]; }
        static public string GetTagApplWebmasterHeader() { return m_text_tags_appl[17]; }
        static public string GetTagApplClubName() { return m_text_tags_appl[18]; }
        static public string GetTagApplMailAddress() { return m_text_tags_appl[19]; }
        static public string GetTagApplEmailJazzLiveAarau() { return m_text_tags_appl[10]; }
        static public string GetTagApplEmailReservation() { return m_text_tags_appl[21]; }
        static public string GetTagApplReservationSubject() { return m_text_tags_appl[22]; }
        static public string GetTagApplReservationText() { return m_text_tags_appl[23]; }
        static public string GetTagApplNewsletterSubject() { return m_text_tags_appl[24]; }
        static public string GetTagApplNewsletterText() { return m_text_tags_appl[25]; }
        static public string GetTagApplTelephoneWebmaster() { return m_text_tags_appl[26]; }
        static public string GetTagApplEmailWebmaster() { return m_text_tags_appl[27]; }
        static public string GetTagApplContactConcertMemberNumber() { return m_text_tags_appl[28]; }
        static public string GetTagApplContactConcertTelephone() { return m_text_tags_appl[29]; }
        static public string GetTagApplContactConcertEmail() { return m_text_tags_appl[30]; }
        static public string GetTagApplUnloadStreet() { return m_text_tags_appl[31]; }
        static public string GetTagApplUnloadCity() { return m_text_tags_appl[32]; }
        static public string GetTagApplParkingOneParkingOne() { return m_text_tags_appl[33]; }
        static public string GetTagApplParkingTwo() { return m_text_tags_appl[34]; }
        static public string GetTagPublishSeasonStartYear() { return m_text_tags_appl[35]; }
        static public string GetTagRequestCaption() { return m_text_tags_appl[36]; }
        static public string GetTagRequestHeader() { return m_text_tags_appl[37]; }
        static public string GetTagRequestDatesDisplay() { return m_text_tags_appl[38]; }
        static public string GetTagRequestNoDatesText() { return m_text_tags_appl[39]; }
        static public string GetTagRequestDatesText() { return m_text_tags_appl[40]; }
        static public string GetTagRequestContentHeader() { return m_text_tags_appl[41]; }
        static public string GetTagRequestContentOne() { return m_text_tags_appl[42]; }
        static public string GetTagRequestContentTwo() { return m_text_tags_appl[43]; }
        static public string GetTagRequestContentThree() { return m_text_tags_appl[44]; }
        static public string GetTagRequestContentFour() { return m_text_tags_appl[45]; }
        static public string GetTagRequestContentFive() { return m_text_tags_appl[46]; }
        static public string GetTagRequestContentSix() { return m_text_tags_appl[47]; }
        static public string GetTagRequestContentSeven() { return m_text_tags_appl[48]; }
        static public string GetTagRequestContentEight() { return m_text_tags_appl[49]; }
        static public string GetTagRequestContentNine() { return m_text_tags_appl[50]; }
        static public string GetTagRequestEmailAddress() { return m_text_tags_appl[51]; }
        static public string GetTagRequestEmailTitle() { return m_text_tags_appl[52]; }
        static public string GetTagRequestEmailCaption() { return m_text_tags_appl[53]; }
        static public string GetTagRequestEmailRemark() { return m_text_tags_appl[54]; }
        static public string GetTagRequestEndParagraph() { return m_text_tags_appl[55]; }
        static public string GetTagReservationNotAllowed() { return m_text_tags_appl[56]; }
        static public string GetTagReservationNotAllowedText() { return m_text_tags_appl[57]; }

        #endregion // Get tags for the single elements in the application XML file

        #region Tags for the member data elements in the application XML file

        /// <summary>Defines the member text tags in the application XML file</summary>
        static public string[] m_text_tags_member =
        {
            @"Name",  // 0 Done
            @"FamilyName", // 1 Done
            @"EmailAddress", // 2 Done
            @"Telephone", // 3 Done
            @"Street",  // 4 Done
            @"City",  // 5 Done
            @"PostCode",  // 6 Done
            @"PhotoMidSize",  // 7 Done
            @"PhotoSmallSize",  // 8 Done
            @"Tasks",  // 9 Done
            @"TasksShort",  // 10 Done
            @"Why",  // 11 Done
            @"StartYear",  // 12 Done
            @"EndYear",  // 13 Done
            @"Password",  // 14 Done
            @"Vorstand",  // 15 Done
            @"Number",  // 16 Done
            @"EmailPrivate", // 17 Done
            @"TelephoneFix", // 18 Done
        }; // m_text_tags_member

        #endregion // Tags for the member data elements in the application XML file

        #region Get tags for the member data elements in the application XML file

        static public string GetTagMemberName() { return m_text_tags_member[0]; }
        static public string GetTagMemberFamilyName() { return m_text_tags_member[1]; }
        static public string GetTagMemberEmailAddress() { return m_text_tags_member[2]; }
        static public string GetTagMemberTelephone() { return m_text_tags_member[3]; }
        static public string GetTagMemberStreet() { return m_text_tags_member[4]; }
        static public string GetTagMemberCity() { return m_text_tags_member[5]; }
        static public string GetTagMemberPostCode() { return m_text_tags_member[6]; }
        static public string GetTagMemberPhotoMidSize() { return m_text_tags_member[7]; }
        static public string GetTagMemberPhotoSmallSize() { return m_text_tags_member[8]; }
        static public string GetTagMemberTasks() { return m_text_tags_member[9]; }
        static public string GetTagMemberTasksShort() { return m_text_tags_member[10]; }
        static public string GetTagMemberWhy() { return m_text_tags_member[11]; }
        static public string GetTagMemberStartYear() { return m_text_tags_member[12]; }
        static public string GetTagMemberEndYear() { return m_text_tags_member[13]; }
        static public string GetTagMemberPassword() { return m_text_tags_member[14]; }
        static public string GetTagMemberVorstand() { return m_text_tags_member[15]; }
        static public string GetTagMemberNumber() { return m_text_tags_member[16]; }
        static public string GetTagMemberEmailPrivate() { return m_text_tags_member[17]; }
        static public string GetTagMemberTelephoneFix() { return m_text_tags_member[18]; }

        #endregion // Get tags for the member data elements in the application XML file

        #region Tags for single elements in the season XML files

        /// <summary>Defines the single XML elements in the season XML files</summary>
        static public string[] m_text_tags_season =
        {
            @"PublishProgram",  // 0
            @"YearAutum",  // 1
            @"YearSpring",  // 2
            
        }; // m_text_tags_season

        #endregion // Tags for single elements in the season XML files

        #region Get tags for single elements in the season XML files

        static public string GetTagSeasonPublishProgram() { return m_text_tags_season[0]; }
        static public string GetTagSeasonYearAutum() { return m_text_tags_season[1]; }
        static public string GetTagSeasonYearSpring() { return m_text_tags_season[2]; }

        #endregion // Get tags for single elements in the season XML files

        #region Tags for concert data elements in the season XML files

        /// <summary>Defines the concert text tags that can be written</summary>
        static public string[] m_text_tags_concert =
        {
            @"BandName",  // 0  Done
            @"Year", // 1   Done
            @"Month", // 2   Done
            @"Day", // 3    Done
            @"TimeStartHour",  // 4   Done
            @"TimeStartMinute",  // 5    Done
            @"TimeEndHour",  // 6   Done
            @"TimeEndMinute",  // 7 Done
            @"ShortText",  // 8   Done
            @"AdditionalText",  // 9 Done
            @"SoundSample",  // 10 Done
            @"BandWebsite",  // 11 Done
            @"PhotoGalleryOne",  // 12 Done
            @"PhotoGalleryTwo",  // 13  Done
            @"PhotoGalleryOneZip",  // 14 Done
            @"PhotoGalleryTwoZip",  // 15 Done
            @"ContactPerson",  // 16 Done
            @"ContactEmail",  // 17 Done
            @"ContactTelephone",  // 18 Done
            @"ContactStreet",  // 19 Done
            @"ContactPostCode",  // 20 Done
            @"ContactCity",  // 21 Done
            @"LoginPassword",  // 22 Done
            @"Place",  // 23 Done
            @"Street",  // 24 Done
            @"City",  // 25 Done
            @"PosterBigSize",  // 26 Done
            @"PosterMidSize",  // 27 Done
            @"PosterSmallSize",  // 28  Done
            @"DayName",  // 29  Done
            @"IbanNumber",  // 30  Done
            @"ContactRemark",  // 31  Done
            @"LabelAdditionalText",  // 32  Done
            @"LabelFlyerText",  // 33  Done
            @"FlyerText",  // 34  Done
            @"ConcertCancelled",  // 35  Done
            @"FlyerTextHomepagePublish",  // 36  Done
            @"SoundSampleQrCode",  // 37  Done
            @"BandWebsiteQrCode",  // 38  Done
            

        }; // m_text_tags_concert

        #endregion // Tags for concert data elements in the season XML files

        #region Get tag functions for concert data elements in the season XML files 

        static public string GetTagSeason() { return "Saison"; }
        static public string GetTagConcert() { return "Concert"; }
        static public string GetTagConcertBandName() { return m_text_tags_concert[0]; }
        static public string GetTagConcertYear() { return m_text_tags_concert[1]; }
        static public string GetTagConcertMonth() { return m_text_tags_concert[2]; }
        static public string GetTagConcertDay() { return m_text_tags_concert[3]; }
        static public string GetTagConcertTimeStartHour() { return m_text_tags_concert[4]; }
        static public string GetTagConcertTimeStartMinute() { return m_text_tags_concert[5]; }
        static public string GetTagConcertTimeEndHour() { return m_text_tags_concert[6]; }
        static public string GetTagConcertTimeEndMinute() { return m_text_tags_concert[7]; }
        static public string GetTagConcertShortText() { return m_text_tags_concert[8]; }
        static public string GetTagConcertAdditionalText() { return m_text_tags_concert[9]; }
        static public string GetTagConcertSoundSample() { return m_text_tags_concert[10]; }
        static public string GetTagConcertBandWebsite() { return m_text_tags_concert[11]; }
        static public string GetTagConcertPhotoGalleryOne() { return m_text_tags_concert[12]; }
        static public string GetTagConcertPhotoGalleryTwo() { return m_text_tags_concert[13]; }
        static public string GetTagConcertPhotoGalleryOneZip() { return m_text_tags_concert[14]; }
        static public string GetTagConcertPhotoGalleryTwoZip() { return m_text_tags_concert[15]; }
        static public string GetTagConcertContactPerson() { return m_text_tags_concert[16]; }
        static public string GetTagConcertContactEmail() { return m_text_tags_concert[17]; }
        static public string GetTagConcertContactTelephone() { return m_text_tags_concert[18]; }
        static public string GetTagConcertContactStreet() { return m_text_tags_concert[19]; }
        static public string GetTagConcertContactPostCode() { return m_text_tags_concert[20]; }
        static public string GetTagConcertContactCity() { return m_text_tags_concert[21]; }
        static public string GetTagConcertLoginPassword() { return m_text_tags_concert[22]; }
        static public string GetTagConcertPlace() { return m_text_tags_concert[23]; }
        static public string GetTagConcertStreet() { return m_text_tags_concert[24]; }
        static public string GetTagConcertCity() { return m_text_tags_concert[25]; }
        static public string GetTagConcertPosterBigSize() { return m_text_tags_concert[26]; }
        static public string GetTagConcertPosterMidSize() { return m_text_tags_concert[27]; }
        static public string GetTagConcertPosterSmallSize() { return m_text_tags_concert[28]; }
        static public string GetTagConcertDayName() { return m_text_tags_concert[29]; }
        static public string GetTagIbanNumber() { return m_text_tags_concert[30]; }
        static public string GetTagContactRemark() { return m_text_tags_concert[31]; }
        static public string GetTagConcertLabelAdditionalText() { return m_text_tags_concert[32]; }
        static public string GetTagConcertLabelFlyerText() { return m_text_tags_concert[33]; }
        static public string GetTagConcertFlyerText() { return m_text_tags_concert[34]; }
        static public string GetTagConcertCancelled() { return m_text_tags_concert[35]; }
        static public string GetTagFlyerTextHomepagePublish() { return m_text_tags_concert[36]; }
        static public string GetTagConcertSoundSampleQrCode() { return m_text_tags_concert[37]; }
        static public string GetTagConcertBandWebsiteQrCode() { return m_text_tags_concert[38]; }

        #endregion // Get tag functions for concert data elements in the season XML files

        #region Tags for musician data elements in the season XML files

        /// <summary>Defines the musician text tags in the season XML files</summary>
        static public string[] m_text_tags_musician =
        {
            @"Name",  // 0
            @"Instrument", // 1
            @"Text", // 2
            @"BirthYear", // 3
            @"Gender",  // 4
        }; // m_text_tags_musician

        #endregion // Tags for musician data elements in the season XML files

        #region Get functions musician data elements tags in the season XML files

        static public string GetTagMusicians() { return "Musicians"; }
        static public string GetTagMusician() { return "Musician"; }
        static public string GetTagMusicianName() { return m_text_tags_musician[0]; }
        static public string GetTagMusicianInstrument() { return m_text_tags_musician[1]; }
        static public string GetTagMusicianText() { return m_text_tags_musician[2]; }
        static public string GetTagMusicianBirthYear() { return m_text_tags_musician[3]; }
        static public string GetTagMusicianGender() { return m_text_tags_musician[4]; }

        #endregion // Get functions musician data elements tags in the season XML files

        #region Definition of single element tags in the documents (templates) XML file

        /// <summary>Defines the text tags in the templates XML file (m_xdocument_templates)</summary>
        static private string[] m_text_tags_templates =
        {
            @"Description", // 0 
            @"TemplatesPath", // 1
            @"Templates", // 2
            @"Template", // 3

        }; // m_text_tags_templates

        #endregion // Definition of single element tags in the documents (templates) XML file

        #region Get single element tags in the documents (templates) XML file

        static public string GetTagTemplsDescription() { return m_text_tags_templates[0]; }
        static public string GetTagTemplsTemplatesPath() { return m_text_tags_templates[1]; }
        static public string GetTagTemplsTemplates() { return m_text_tags_templates[2]; }
        static public string GetTagTemplsTemplate() { return m_text_tags_templates[3]; }

        #endregion // Get single element tags in the documents (templates) XML file

        #region Definition of template data tags in the documents (templates) XML file

        /// <summary>Defines the text tags for one template in the templates XML file</summary>
        static private string[] m_text_tags_template =
        {
            @"TemplateName", // 0 
            @"TemplateExtensions", // 1
            @"TemplateDescription", // 2
            @"TemplateInstructions", // 3
            @"TemplateInstruction", // 4
            @"FilePathDescription", // 5
            @"FileNameDocDescription", // 6
            @"FileNameXlsDescription", // 7
            @"FileNamePdfDescription", // 8
            @"FileNameTxtDescription", // 9
            @"FileNameImgDescription", // 10
            @"PublishedDescription", // 11
            @"TemplateNameDescription", // 12
            @"DocumentType", // 13
            @"DocumentDialog", // 14
            @"DocumentDialogTitle", // 15

        }; // m_text_tags_template

        #endregion // Definition of template data tags in the documents (templates) XML file

        #region Get template data tags in the documents (templates) XML file

        static public string GetTagTemplTemplateName() { return m_text_tags_template[0]; }
        static public string GetTagTemplTemplateExtensions() { return m_text_tags_template[1]; }
        static public string GetTagTemplTemplateDescription() { return m_text_tags_template[2]; }
        static public string GetTagTemplTemplateInstructions() { return m_text_tags_template[3]; }
        static public string GetTagTemplTemplateInstruction() { return m_text_tags_template[4]; }
        static public string GetTagTemplFilePathDescription() { return m_text_tags_template[5]; }
        static public string GetTagTemplFileNameDocDescription() { return m_text_tags_template[6]; }
        static public string GetTagTemplFileNameXlsDescription() { return m_text_tags_template[7]; }
        static public string GetTagTemplFileNamePdfDescription() { return m_text_tags_template[8]; }
        static public string GetTagTemplFileNameTxtDescription() { return m_text_tags_template[9]; }
        static public string GetTagTemplFileNameImgDescription() { return m_text_tags_template[10]; }
        static public string GetTagTemplPublishedDescription() { return m_text_tags_template[11]; }
        static public string GetTagTemplTemplateNameDescription() { return m_text_tags_template[12]; }
        static public string GetTagTemplDocumentType() { return m_text_tags_template[13]; }
        static public string GetTagTemplDocumentDialog() { return m_text_tags_template[14]; }
        static public string GetTagTemplDocumentDialogTitle() { return m_text_tags_template[15]; }

        #endregion // Get template data tags in the documents (templates) XML file

        #region Definition of single element tags in the season documents XML file

        /// <summary>Defines the text tags in the season XML document file (m_xdocument_active_doc)</summary>
        static private string[] m_text_tags_doc_season =
        {
            @"SeasonYears", // 0 
            @"DocumentsPath", // 1
            @"DocumentsPathUsed", // 2
            @"Concerts", // 3
            @"Concert", // 4
            @"SeasonDocument", // 5

        }; // m_text_tags_doc_season

        #endregion // Definition of single element tags in the season documents XML file

        #region Get single element tags in the season documents XML file

        static public string GetTagDocSeasonYears() { return m_text_tags_doc_season[0]; }
        static public string GetTagDocDocumentsPath() { return m_text_tags_doc_season[1]; }
        static public string GetTagDocDocumentsPathUsed() { return m_text_tags_doc_season[2]; }
        static public string GetTagDocConcerts() { return m_text_tags_doc_season[3]; }
        static public string GetTagDocConcert() { return m_text_tags_doc_season[4]; }
        static public string GetTagDocSeasonDocument() { return m_text_tags_doc_season[5]; }

        #endregion // Get single element tags in the season documents XML file

        #region Definition of concert data tags in the season documents XML file

        /// <summary>Defines the text tags for one concert or a season document XML file</summary>
        static private string[] m_text_tags_doc_concert =
        {
            @"BandName", // 0 
            @"Documents", // 1
            @"Document", // 2

        }; // m_text_tags_doc_concert

        #endregion // Definition of concert data tags in the season documents XML file

        #region Get concert data tags in the season documents XML file

        static public string GetTagDocConcertBandName() { return m_text_tags_doc_concert[0]; }
        static public string GetTagDocConcertDocuments() { return m_text_tags_doc_concert[1]; }
        static public string GetTagDocConcertDocument() { return m_text_tags_doc_concert[2]; }

        #endregion // Get concert data tags in the season documents XML file

        #region Definition of document data tags in the season documents XML file and in the templates XML file

        /// <summary>Defines the text tags for one concert document or a season template document</summary>
        static private string[] m_text_tags_doc_season_concert =
        {
            @"TemplateName", // 0
            @"FilePath", // 1
            @"FileNameDoc", // 2
            @"FileNameXls", // 3
            @"FileNamePdf", // 4
            @"FileNameTxt", // 5
            @"FileNameImg", // 6
            @"Published", // 7

        }; // m_text_tags_season_concert

        #endregion // Definition of document data tags in the season documents XML file and in the templates XML file

        #region Get document data tags in the season documents XML file and in the templates XML file

        static public string GetTagDocSeasonConcertTemplateName() { return m_text_tags_doc_season_concert[0]; }
        static public string GetTagDocSeasonConcertFilePath() { return m_text_tags_doc_season_concert[1]; }
        static public string GetTagDocSeasonConcertFileNameDoc() { return m_text_tags_doc_season_concert[2]; }
        static public string GetTagDocSeasonConcertFileNameXls() { return m_text_tags_doc_season_concert[3]; }
        static public string GetTagDocSeasonConcertFileNamePdf() { return m_text_tags_doc_season_concert[4]; }
        static public string GetTagDocSeasonConcertFileNameTxt() { return m_text_tags_doc_season_concert[5]; }
        static public string GetTagDocSeasonConcertFileNameImg() { return m_text_tags_doc_season_concert[6]; }
        static public string GetTagDocSeasonConcertPublished() { return m_text_tags_doc_season_concert[7]; }

        #endregion // Get document data tags in the season documents XML file and in the templates XML file

        #region Definition of values and get functions for template data in the documents (templates) XML file

        /// <summary>Defines the values for template names</summary>
        static private string[] m_text_values_doc_template_name =
        {
            @"Programm_", // 0
            @"PATH_Billet", // 1
            @"PATH_Start_Flyer", // 2
            @"PATH_Poster", // 3
            @"PATH_Flyer_Front", // 4
            @"PATH_Flyer_Reverse", // 5
            @"PATH_Flyer_Start", // 6
            @"Brief_", // 7
            @"SupporterFront_", // 8
            @"SupporterReverse_", // 9
            @"SupporterLetter_", // 10
            @"FreeEntrance_", // 11
            @"Budget_", // 12
            @"Kassenbuch_", // 13
            @"Betriebsrechnung_", // 14
            @"Bilanz_", // 15
            @"Revisorenbericht_", // 16
            @"PATH_Contract", // 17
            @"SuisaAbrechnung_", // 18
            @"SuisaBrief_", // 19
            @"GesuchAarau_", // 20
            @"GesuchKuratorium_", // 21
            @"BescheidAarau_", // 22
            @"BescheidKuratorium_", // 23
            @"PATH_Concert_Info", // 24
            @"PATH_Flyer_Info", // 25
            @"PATH_Poster_Internet", // 26
            @"PATH_Flyer_Printshop", // 27
            
        }; // m_text_values_doc_template_name

        /// <summary>Get the template name for the season program</summary>
        static public string GetTemplateNameSeasonProgram() { return m_text_values_doc_template_name[0]; }

        /// <summary>Get the template name for the concert ticket</summary>
        static public string GetTemplateNameConcertTicket() { return m_text_values_doc_template_name[1]; }

        /// <summary>Get the template name for the start flyer</summary>
        static public string GetTemplateNameStartFlyer() { return m_text_values_doc_template_name[2]; }

        /// <summary>Get the template name for the concert poster</summary>
        static public string GetTemplateNamePoster() { return m_text_values_doc_template_name[3]; }

        /// <summary>Get the template name for the flyer</summary>
        static public string GetTemplateNameFlyerFront() { return m_text_values_doc_template_name[4]; }

        /// <summary>Get the template name for the flyer back</summary>
        static public string GetTemplateNameFlyerReverse() { return m_text_values_doc_template_name[5]; }

        /// <summary>Get the template name for the flyer start</summary>
        static public string GetTemplateNameFlyerStart() { return m_text_values_doc_template_name[6]; }

        /// <summary>Get the template name for the season letter</summary>
        static public string GetTemplateNameSeasonLetter() { return m_text_values_doc_template_name[7]; }

        /// <summary>Get the template name for the supporter card, front side</summary>
        static public string GetTemplateNameSupporterFront() { return m_text_values_doc_template_name[8]; }

        /// <summary>Get the template name for the supporter card, reverse side</summary>
        static public string GetTemplateNameSupporterReverse() { return m_text_values_doc_template_name[9]; }

        /// <summary>Get the template name for the supporter letter</summary>
        static public string GetTemplateNameSupporterLetter() { return m_text_values_doc_template_name[10]; }

        /// <summary>Get the template name for the free entrance ticket</summary>
        static public string GetTemplateNameFreeEntrance() { return m_text_values_doc_template_name[11]; }

        /// <summary>Get the template name for the "Budget"</summary>
        static public string GetTemplateNameBudget() { return m_text_values_doc_template_name[12]; }

        /// <summary>Get the template name for the "Kassenbuch"</summary>
        static public string GetTemplateNameKassenbuch() { return m_text_values_doc_template_name[13]; }

        /// <summary>Get the template name for the "Betriebsrechnung"</summary>
        static public string GetTemplateNameBetriebsrechnung() { return m_text_values_doc_template_name[14]; }

        /// <summary>Get the template name for the "Bilanz"</summary>
        static public string GetTemplateNameBilanz() { return m_text_values_doc_template_name[15]; }

        /// <summary>Get the template name for the "Revisorenbericht"</summary>
        static public string GetTemplateNameRevisorenbericht() { return m_text_values_doc_template_name[16]; }

        /// <summary>Get the template name for the contract</summary>
        static public string GetTemplateNameContract() { return m_text_values_doc_template_name[17]; }

        /// <summary>Get the template name for the SUISA report</summary>
        static public string GetTemplateNameSuisaReport() { return m_text_values_doc_template_name[18]; }

        /// <summary>Get the template name for the SUISA letter</summary>
        static public string GetTemplateNameSuisaLetter() { return m_text_values_doc_template_name[19]; }

        /// <summary>Get the template name for the Aarau request</summary>
        static public string GetTemplateNameAarauRequest() { return m_text_values_doc_template_name[20]; }

        /// <summary>Get the template name for the Kuratorium request</summary>
        static public string GetTemplateNameKuratoriumRequest() { return m_text_values_doc_template_name[21]; }

        /// <summary>Get the template name for the Aarau answer</summary>
        static public string GetTemplateNameAarauAnswer() { return m_text_values_doc_template_name[22]; }

        /// <summary>Get the template name for the Kuratorium answer</summary>
        static public string GetTemplateNameKuratoriumAnswer() { return m_text_values_doc_template_name[23]; }

        /// <summary>Get the template name for the concert information</summary>
        static public string GetTemplateNameConcertInformation() { return m_text_values_doc_template_name[24]; }

        /// <summary>Get the template name for the flyer info</summary>
        static public string GetTemplateNameFlyerInfo() { return m_text_values_doc_template_name[25]; }

        /// <summary>Get the template name for the internet poster</summary>
        static public string GetTemplateNamePosterInternet() { return m_text_values_doc_template_name[26]; }

        /// <summary>Get the template name for the flyer printshop</summary>
        static public string GetTemplateNameFlyerPrintshop() { return m_text_values_doc_template_name[27]; }


        #endregion // Definition of values and get functions for template data in the documents (templates) XML file

        #region Definition of single element tags in the requests XML document file (JazzAnfrage.xml)

        /// <summary>Defines the text tags in the requests XML document file (JazzAnfrage.xml)</summary>
        static private string[] m_text_tags_req_single =
        {
            @"LastRegNumber", // 0 
            @"Request", // 1

        }; // m_text_tags_req_single


        #endregion // Definition of single element tags in the requests XML document file (JazzAnfrage.xml)

        #region Get single element tags in the requests XML document file (JazzAnfrage.xml)

        static public string GetTagReqLastReqNumber() { return m_text_tags_req_single[0]; }
        static public string GetTagReqRequest() { return m_text_tags_req_single[1]; }

        #endregion // Get single element tags in the requests XML document file (JazzAnfrage.xml)

        #region Definition of request (band) data tags in the requests XML document file (JazzAnfrage.xml)

        /// <summary>Defines the text tags for a request</summary>
        static private string[] m_text_tags_req_band =
        {
            @"RegNumber", // 0
            @"RegDay", // 1
            @"RegMonth", // 2
            @"RegYear", // 3
            @"BandName", // 4
            @"Comments", // 5
            @"PrivateNotes", // 6
            @"BandWebsite", // 7
            @"SoundSample", // 8
            @"AudioOne", // 9
            @"AudioTwo", // 10
            @"AudioThree", // 11
            @"AudioOneCd", // 12
            @"AudioTwoCd", // 13
            @"AudioThreeCd", // 14
            @"ToBeEvaluated", // 15
            @"InfoOne", // 16
            @"InfoTwo", // 17
            @"InfoThree", // 18

            @"LinkOne", // 19
            @"LinkTwo", // 20
            @"LinkThree", // 21
            @"LinkFour", // 22
            @"LinkFive", // 23
            @"LinkSix", // 24
            @"LinkSeven", // 25
            @"LinkEight", // 26
            @"LinkNine", // 27
            @"LinkOneType", // 28
            @"LinkTwoType", // 29
            @"LinkThreeType", // 30
            @"LinkFourType", // 31
            @"LinkFiveType", // 32
            @"LinkSixType", // 33
            @"LinkSevenType", // 34
            @"LinkEightType", // 35
            @"LinkNineType", // 36
            @"LinkOneText", // 37
            @"LinkTwoText", // 38
            @"LinkThreeText", // 39
            @"LinkFourText", // 40
            @"LinkFiveText", // 41
            @"LinkSixText", // 42
            @"LinkSevenText", // 43
            @"LinkEightText", // 44
            @"LinkNineText", // 45
            @"PhotoOne", // 46
            @"PhotoTwo", // 47
            @"PhotoThree", // 48
            @"PhotoFour", // 49
            @"PhotoFive", // 50
            @"PhotoSix", // 51
            @"PhotoSeven", // 52
            @"PhotoEight", // 53
            @"PhotoNine", // 54
            @"ConcertNumber", // 55
            

        }; // m_text_tags_req_band

        #endregion Definition of request (band) data tags in the requests XML document file (JazzAnfrage.xml)

        #region Get functions for request (band) data tags in the requests XML document file (JazzAnfrage.xml)

        static public string GetTagReqRegNumber() { return m_text_tags_req_band[0]; }
        static public string GetTagReqRegDay() { return m_text_tags_req_band[1]; }
        static public string GetTagReqRegMonth() { return m_text_tags_req_band[2]; }
        static public string GetTagReqRegYear() { return m_text_tags_req_band[3]; }
        static public string GetTagReqBandName() { return m_text_tags_req_band[4]; }
        static public string GetTagReqComments() { return m_text_tags_req_band[5]; }
        static public string GetTagReqPrivateNotes() { return m_text_tags_req_band[6]; }
        static public string GetTagReqBandWebsite() { return m_text_tags_req_band[7]; }
        static public string GetTagReqSoundSample() { return m_text_tags_req_band[8]; }
        static public string GetTagReqAudioOne() { return m_text_tags_req_band[9]; }
        static public string GetTagReqAudioTwo() { return m_text_tags_req_band[10]; }
        static public string GetTagReqAudioThree() { return m_text_tags_req_band[11]; }
        static public string GetTagReqAudioOneCd() { return m_text_tags_req_band[12]; }
        static public string GetTagReqAudioTwoCd() { return m_text_tags_req_band[13]; }
        static public string GetTagReqAudioThreeCd() { return m_text_tags_req_band[14]; }
        static public string GetTagReqToBeEvaluated() { return m_text_tags_req_band[15]; }
        static public string GetTagReqInfoOne() { return m_text_tags_req_band[16]; }
        static public string GetTagReqInfoTwo() { return m_text_tags_req_band[17]; }
        static public string GetTagReqInfoThree() { return m_text_tags_req_band[18]; }
        static public string GetTagReqLinkOne() { return m_text_tags_req_band[19]; }
        static public string GetTagReqLinkTwo() { return m_text_tags_req_band[20]; }
        static public string GetTagReqLinkThree() { return m_text_tags_req_band[21]; }
        static public string GetTagReqLinkFour() { return m_text_tags_req_band[22]; }
        static public string GetTagReqLinkFive() { return m_text_tags_req_band[23]; }
        static public string GetTagReqLinkSix() { return m_text_tags_req_band[24]; }
        static public string GetTagReqLinkSeven() { return m_text_tags_req_band[25]; }
        static public string GetTagReqLinkEight() { return m_text_tags_req_band[26]; }
        static public string GetTagReqLinkNine() { return m_text_tags_req_band[27]; }
        static public string GetTagReqLinkOneType() { return m_text_tags_req_band[28]; }
        static public string GetTagReqLinkTwoType() { return m_text_tags_req_band[29]; }
        static public string GetTagReqLinkThreeType() { return m_text_tags_req_band[30]; }
        static public string GetTagReqLinkFourType() { return m_text_tags_req_band[31]; }
        static public string GetTagReqLinkFiveType() { return m_text_tags_req_band[32]; }
        static public string GetTagReqLinkSixType() { return m_text_tags_req_band[33]; }
        static public string GetTagReqLinkSevenType() { return m_text_tags_req_band[34]; }
        static public string GetTagReqLinkEightType() { return m_text_tags_req_band[35]; }
        static public string GetTagReqLinkNineType() { return m_text_tags_req_band[36]; }
        static public string GetTagReqLinkOneText() { return m_text_tags_req_band[37]; }
        static public string GetTagReqLinkTwoText() { return m_text_tags_req_band[38]; }
        static public string GetTagReqLinkThreeText() { return m_text_tags_req_band[39]; }
        static public string GetTagReqLinkFourText() { return m_text_tags_req_band[40]; }
        static public string GetTagReqLinkFiveText() { return m_text_tags_req_band[41]; }
        static public string GetTagReqLinkSixText() { return m_text_tags_req_band[42]; }
        static public string GetTagReqLinkSevenText() { return m_text_tags_req_band[43]; }
        static public string GetTagReqLinkEightText() { return m_text_tags_req_band[44]; }
        static public string GetTagReqLinkNineText() { return m_text_tags_req_band[45]; }
        static public string GetTagReqPhotoOne() { return m_text_tags_req_band[46]; }
        static public string GetTagReqPhotoTwo() { return m_text_tags_req_band[47]; }
        static public string GetTagReqPhotoThree() { return m_text_tags_req_band[48]; }
        static public string GetTagReqPhotoFour() { return m_text_tags_req_band[49]; }
        static public string GetTagReqPhotoFive() { return m_text_tags_req_band[50]; }
        static public string GetTagReqPhotoSix() { return m_text_tags_req_band[51]; }
        static public string GetTagReqPhotoSeven() { return m_text_tags_req_band[52]; }
        static public string GetTagReqPhotoEight() { return m_text_tags_req_band[53]; }
        static public string GetTagReqPhotoNine() { return m_text_tags_req_band[54]; }
        static public string GetTagReqConcertNumber() { return m_text_tags_req_band[55]; }

        #endregion // Get functions for request (band) data tags in the requests XML document file (JazzAnfrage.xml)

        #region Definition of single element tags in the news XML document file (JazzNews.xml)

        /// <summary>Defines the text tags in the news XML document file (JazzNews.xml)</summary>
        static private string[] m_text_tags_news_single =
        {
            @"NewsBackgroundColor", // 0 
            @"NewsTextColor", // 1
            @"CurrentNews", // 2
            @"ConcertNews", // 3

        }; // m_text_tags_news_single


        #endregion // Definition of single element tags in the news XML document file (JazzNews.xml)

        #region Get single element tags in the news XML document file (JazzNews.xml)

        static public string GetTagNewsBackgroundColor() { return m_text_tags_news_single[0]; }
        static public string GetTagNewsTextColor() { return m_text_tags_news_single[1]; }
        static public string GetTagCurrentNews() { return m_text_tags_news_single[2]; }
        static public string GetTagConcertNews() { return m_text_tags_news_single[3]; }

        #endregion // Get single element tags in the news XML document file (JazzNews.xml)

        #region Definition of news tags in the news XML document file (JazzNews.xml)

        /// <summary>Defines the text tags for a request</summary>
        static private string[] m_text_tags_current_news =
        {
            @"NewsHeader", // 0
            @"NewsContent", // 1
            @"NewsImage", // 2
            @"NewsImageWidth", // 3
            @"NewsImageTitle", // 4
            @"NewsImageCaption", // 5
            @"NewsLink", // 6
            @"NewsLinkCaption", // 7
            @"NewsEmailSubject", // 8
            @"NewsEmailText", // 9
            @"NewsEmailCaption", // 10
            @"NewsStartYear", // 11
            @"NewsStartMonth", // 12
            @"NewsStartDay", // 13
            @"NewsEndYear", // 14
            @"NewsEndMonth", // 15
            @"NewsEndDay", // 16
            @"NewsTestFlag", // 17
            @"ConcertNewsNumber", // 18
            @"ConcertNewsHeader", // 19
            @"ConcertNewsContent", // 20
            @"ConcertNewsTestFlag", // 21
            @"ConcertNewsCancelledFlag", // 22

        }; // m_text_tags_current_news

        #endregion // Definition of news tags in the news XML document file (JazzNews.xml)

        #region Get news tags functions for the news XML document file (JazzNews.xml)

        static public string GetTagNewsHeader() { return m_text_tags_current_news[0]; }
        static public string GetTagNewsContent() { return m_text_tags_current_news[1]; }
        static public string GetTagNewsImage() { return m_text_tags_current_news[2]; }
        static public string GetTagNewsImageWidth() { return m_text_tags_current_news[3]; }
        static public string GetTagNewsImageTitle() { return m_text_tags_current_news[4]; }
        static public string GetTagNewsImageCaption() { return m_text_tags_current_news[5]; }
        static public string GetTagNewsLink() { return m_text_tags_current_news[6]; }
        static public string GetTagNewsLinkCaption() { return m_text_tags_current_news[7]; }
        static public string GetTagNewsEmailSubject() { return m_text_tags_current_news[8]; }
        static public string GetTagNewsEmailText() { return m_text_tags_current_news[9]; }
        static public string GetTagNewsEmailCaption() { return m_text_tags_current_news[10]; }
        static public string GetTagNewsStartYear() { return m_text_tags_current_news[11]; }
        static public string GetTagNewsStartMonth() { return m_text_tags_current_news[12]; }
        static public string GetTagNewsStartDay() { return m_text_tags_current_news[13]; }
        static public string GetTagNewsEndYear() { return m_text_tags_current_news[14]; }
        static public string GetTagNewsEndMonth() { return m_text_tags_current_news[15]; }
        static public string GetTagNewsEndDay() { return m_text_tags_current_news[16]; }
        static public string GetTagNewsTestFlag() { return m_text_tags_current_news[17]; }
        static public string GetTagConcertNewsNumber() { return m_text_tags_current_news[18]; }
        static public string GetTagConcertNewsHeader() { return m_text_tags_current_news[19]; }
        static public string GetTagConcertNewsContent() { return m_text_tags_current_news[20]; }
        static public string GetTagConcertNewsTestFlag() { return m_text_tags_current_news[21]; }
        static public string GetTagConcertNewsCancelledFlag() { return m_text_tags_current_news[22]; }

        #endregion // Get news tags functions for the news XML document file (JazzNews.xml)

        #region Definition of single element tags in the newsletter XML document file (JazzNewsletter.xml)

        /// <summary>Defines the text tags in the newsletter XML document file (JazzNewsletter.xml)</summary>
        static private string[] m_text_tags_newsletter_single =
        {
            @"JazzNewsletter", // 0 

        }; // m_text_tags_newsletter_single


        #endregion // Definition of single element tags in the newsletter XML document file (JazzNewsletter.xml)

        #region Get single element tags in the newsletter XML document file (JazzNewsletter.xml)

        static public string GetTagNewsletter() { return m_text_tags_newsletter_single[0]; }

        #endregion // Get single element tags in the newsletter XML document file (JazzNewsletter.xml)


        #region Definition of newsletter tags in the newsletter XML document file (JazzNewsletter.xml)

        /// <summary>Defines the text tags for a request</summary>
        static private string[] m_text_tags_newsletter =
        {
            @"JazzNewsletterEmlPath", // 0
            @"JazzNewsletterEmlFile", // 1
            @"JazzNewsletterYear", // 2
            @"JazzNewsletterMonth", // 3
            @"JazzNewsletterDay", // 4
            @"JazzNewsletterSubject", // 5
            @"JazzNewsletterFrom", // 6
            @"JazzNewsletterMsgHtml", // 7
            @"JazzNewsletterImagePath", // 8
            @"JazzNewsletterImageFile", // 9
            @"JazzNewsletterEmbeddedFlag", // 10
            @"JazzNewsletterAttachmentPath", // 11
            @"JazzNewsletterAttachmentFile", // 12

        }; // m_text_tags_newsletter

        #endregion Definition of newsletter tags in the newsletter XML document file (JazzNewsletter.xml)

        #region Get newsletter tags functions for the newsletter tags in the newsletter XML document file (JazzNewsletter.xml)

        static public string GetTagNewsletterEmlPath() { return m_text_tags_newsletter[0]; }
        static public string GetTagNewsletterEmlFile() { return m_text_tags_newsletter[1]; }
        static public string GetTagNewsletterYear() { return m_text_tags_newsletter[2]; }
        static public string GetTagNewsletterMonth() { return m_text_tags_newsletter[3]; }
        static public string GetTagNewsletterDay() { return m_text_tags_newsletter[4]; }
        static public string GetTagNewsletterSubject() { return m_text_tags_newsletter[5]; }
        static public string GetTagNewsletterFrom() { return m_text_tags_newsletter[6]; }
        static public string GetTagNewsletterMsgHtml() { return m_text_tags_newsletter[7]; }
        static public string GetTagNewsletterImagePath() { return m_text_tags_newsletter[8]; }
        static public string GetTagNewsletterImageFile() { return m_text_tags_newsletter[9]; }
        static public string GetTagNewsletterEmbeddedFlag() { return m_text_tags_newsletter[10]; }
        static public string GetTagNewsletterAttachmentPath() { return m_text_tags_newsletter[11]; }
        static public string GetTagNewsletterAttachmentFile() { return m_text_tags_newsletter[12]; }

        #endregion // Get newsletter tags functions for the newsletter tags in the newsletter XML document file (JazzNewsletter.xml)


        #region Definition of single element tags in photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        /// <summary>Defines the text tags in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)</summary>
        static private string[] m_text_tags_photo_single =
        {
            @"PhotosSeason", // 0 
            @"PhotoStartYearSeason", // 1 
            @"PhotoConcert", // 2
            @"GalleryOne", // 3
            @"GalleryTwo", // 4

        }; // m_text_tags_photo_single


        #endregion // Definition of single element tags in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        #region Get single element tags in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        static public string GetTagPhotosSeason() { return m_text_tags_photo_single[0]; }
        static public string GetTagPhotoStartYearSeason() { return m_text_tags_photo_single[1]; }
        static public string GetTagPhotoConcert() { return m_text_tags_photo_single[2]; }
        static public string GetTagGalleryOne() { return m_text_tags_photo_single[3]; }
        static public string GetTagGalleryTwo() { return m_text_tags_photo_single[4]; }

        #endregion // Definition of single element tags in photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        #region Definition of data tags in the in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        /// <summary>Defines the text tags for a request</summary>
        static private string[] m_text_tags_photo_concert =
        {
            @"PhotoBandName", // 0
            @"PhotoYear", // 1
            @"PhotoMonth", // 2
            @"PhotoDay", // 3
            @"GalleryName", // 4
            @"PhotoTextOne", // 5
            @"PhotoTextTwo", // 6
            @"PhotoTextThree", // 7
            @"PhotoTextFour", // 8
            @"PhotoTextFive", // 9
            @"PhotoTextSix", // 10
            @"PhotoTextSeven", // 11
            @"PhotoTextEight", // 12
            @"PhotoTextNine", // 13
            @"PhotographerName", // 14
            @"PhotoZipName", // 15
            @"PhotoConcertNumber", // 16


        }; // m_text_tags_photo_concert


        #endregion // Definition of data tags in the in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        #region Get functions for data tags in the in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        static public string GetTagPhotoBandName() { return m_text_tags_photo_concert[0]; }
        static public string GetTagPhotoYear() { return m_text_tags_photo_concert[1]; }
        static public string GetTagPhotoMonth() { return m_text_tags_photo_concert[2]; }
        static public string GetTagPhotoDay() { return m_text_tags_photo_concert[3]; }
        static public string GetTagGalleryName() { return m_text_tags_photo_concert[4]; }
        static public string GetTagPhotoTextOne() { return m_text_tags_photo_concert[5]; }
        static public string GetTagPhotoTextTwo() { return m_text_tags_photo_concert[6]; }
        static public string GetTagPhotoTextThree() { return m_text_tags_photo_concert[7]; }
        static public string GetTagPhotoTextFour() { return m_text_tags_photo_concert[8]; }
        static public string GetTagPhotoTextFive() { return m_text_tags_photo_concert[9]; }
        static public string GetTagPhotoTextSix() { return m_text_tags_photo_concert[10]; }
        static public string GetTagPhotoTextSeven() { return m_text_tags_photo_concert[11]; }
        static public string GetTagPhotoTextEight() { return m_text_tags_photo_concert[12]; }
        static public string GetTagPhotoTextNine() { return m_text_tags_photo_concert[13]; }
        static public string GetTagPhotographerName() { return m_text_tags_photo_concert[14]; }
        static public string GetTagPhotoZipName() { return m_text_tags_photo_concert[15]; }
        static public string GetTagPhotoConcertNumber() { return m_text_tags_photo_concert[16]; }

        #endregion // Get functions for data tags in the in the photo gallery XML document files (JazzGalerieEin.xml and JazzGalerieZwei.xml)

        #region Check if tag name exists

        /// <summary>Returns true if single node tag (m_text_tags_appl) is defined for the application XML file</summary>
        static public bool CheckTagApplSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_appl.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_appl[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagAppl

        /// <summary>Returns true if single node tag (m_text_tags_season) is defined for a season program XML file</summary>
        static public bool CheckTagSeasonProgramSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_season.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_season[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagSeasonProgramSingle

        /// <summary>Returns true if single node tag (m_text_tags_templates) is defined for the documents (templates) XML file</summary>
        static public bool CheckTagTemplateSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_templates.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_templates[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagTemplateSingle

        /// <summary>Returns true if single node tag (m_text_tags_req_single) is defined for the requests XML file</summary>
        static public bool CheckTagReqSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_req_single.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_req_single[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagReqSingle

        /// <summary>Returns true if node tag (m_text_tags_req_band) is defined for the requests XML file</summary>
        static public bool CheckTagReq(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_req_band.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_req_band[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagReq

        /// <summary>Returns true if single node tag (m_text_tags_news_single) is defined for the news XML file</summary>
        static public bool CheckTagNewsSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_news_single.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_news_single[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagNewsSingle

        /// <summary>Returns true if node tag (m_text_tags_current_news) is defined for the news XML file</summary>
        static public bool CheckTagCurrentNews(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_current_news.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_current_news[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagCurrentNews

        /// <summary>Returns true if node tag (m_text_tags_newsletter) is defined for the newsletter XML file</summary>
        static public bool CheckTagNewsletter(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_newsletter.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_newsletter[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagNewsletter

        /// <summary>Returns true if single node tag (m_text_tags_photo_single) is defined for the photo gallery XML file</summary>
        static public bool CheckTagPhotoSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_photo_single.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_photo_single[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagPhotoSingle

        /// <summary>Returns true if node tag (m_text_tags_photo_concert) is defined for the photo gallery XML file</summary>
        static public bool CheckTagPhoto(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_photo_concert.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_photo_concert[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagPhoto

        /// <summary>Returns true if single node tag (m_text_tags_doc_season) is defined in a season documents XML file</summary>
        static public bool CheckTagSeasonDocumentsSingle(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_doc_season.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_doc_season[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagSeasonDocumentsSingle


        /// <summary>Returns true if single node tag (m_text_tags_member) is defined in a a season program XML file</summary>
        static public bool CheckTagMember(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_member.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_member[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagMember

        /// <summary>Returns true if a concert data tag (m_text_tags_concert) is defined in a a season program XML file</summary>
        static public bool CheckTagConcert(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_concert.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_concert[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagConcert

        /// <summary>Returns true if a template data tag (m_text_tags_doc_season_concert) is defined in the documents (templates) XML file</summary>
        static public bool CheckTagTemplateData(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_doc_season_concert.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_doc_season_concert[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagTemplateData

        /// <summary>Returns true if a concert document tag (m_text_tags_doc_season_concert) is defined</summary>
        static public bool CheckTagDocConcert(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_doc_season_concert.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_doc_season_concert[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagDocConcert

        /// <summary>Returns true if the musician tag (m_text_tags_musician) is defined</summary>
        static public bool CheckTagMusician(String i_tag_name)
        {
            for (int index_tag_name = 0; index_tag_name < m_text_tags_musician.Length; index_tag_name++)
            {
                if (i_tag_name.Equals(m_text_tags_musician[index_tag_name]))
                    return true;
            }

            return false;

        } // CheckTagMusician

        #endregion // Check if tag name exists

    } // JazzXml

} // namespace
