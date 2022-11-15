using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JazzApp
{
    /// <summary>Search functions
    /// <para>The below listed changes should also be made in the JazzSearch.cs file for JazzMobile(the original/copy)</para>
    /// <para>SetResultBatchSize(int i_return_n_results)</para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public static class JazzSearch
    {
        #region Member variables

        /// <summary>Results: Indices seasons</summary>
        static private int[] m_season_indices = null;

        /// <summary>Array list corresponding to  m_season_indices</summary>
        static private List<int> m_season_indices_list = new List<int>();

        /// <summary>Results: Concert numbers</summary>
        static private int[] m_concert_numbers = null;

        /// <summary>Array list corresponding to  m_concert_numbers</summary>
        static private List<int> m_concert_numbers_list = new List<int>();

        /// <summary>Defines the number of results in a batch that shall be returned, i.e. the maximum size of the returned  arrays</summary>
        static private int m_return_n_results = 7;
        /// <summary>Sets the number of results in a batch that shall be returned, i.e. the maximum size of the returned  arrays</summary>
        static public void SetResultBatchSize(int i_return_n_results) { m_return_n_results = i_return_n_results; }

        #endregion // Member variables

        #region Execution functions

        /// <summary>Execute search. Returns the number of batch results</summary>
        static public int Execute(String i_search, Boolean i_musician, Boolean i_band, Boolean i_texts, Boolean i_member_login)
        {
            int ret_number_batches = 0;

            // Empty the array lists
            m_season_indices_list.Clear();
            m_concert_numbers_list.Clear();

            XDocument current_season_document = JazzXml.GetDocumentCurrent();
            if (null == current_season_document) return ret_number_batches;

            XDocument[] season_documents = JazzXml.GetAvailableSeasonDocuments(i_member_login);
            if (null == season_documents) return ret_number_batches;
            if (0 == season_documents.Length) return ret_number_batches;

            for (int i_doc_index = season_documents.Length - 1; i_doc_index >= 0; i_doc_index--)
            {
                JazzXml.SetDocumentCurrent(season_documents[i_doc_index]);

                for (int i_concert = 12; i_concert >= 1; i_concert--)
                {
                    if (i_musician)
                        SearchMusicianNames(i_search, i_doc_index, i_concert);

                    if (i_band)
                        SearchBand(i_search, i_doc_index, i_concert);

                    if (i_texts)
                        SearchTexts(i_search, i_doc_index, i_concert);

                } // i_concert

            } // i_doc_index

            JazzXml.SetDocumentCurrent(current_season_document);

            m_season_indices = JazzUtils.ConvertListIntToInt(m_season_indices_list);
            m_concert_numbers = JazzUtils.ConvertListIntToInt(m_concert_numbers_list);

            int number_results = GetNumberResults();

            if (0 == number_results)
                return number_results;

            double n_batch_double = (double)number_results / (double)m_return_n_results;

            // For the cases that number_results = n * m_return_n_results, where n = 1, 2, 3, ...
            n_batch_double = n_batch_double - 0.000001;

            ret_number_batches = (int)n_batch_double + 1;

            return ret_number_batches;
        } // Execute


        /// <summary>Search musician names</summary>
        static private void SearchMusicianNames(String i_search, int i_doc_index, int i_concert)
        {
            int n_musicians = JazzXml.GetNumberMusicians(i_concert);
            if (0 == n_musicians)
                return;

            for (int i_musician = 1; i_musician <= n_musicians; i_musician++)
            {
                String musician_name = JazzXml.GetMusicianName(i_concert, i_musician);
                if (JazzXml.XmlNodeValueIsSet(musician_name))
                {
                    CompareStringsAddOutput(musician_name, i_search, i_doc_index, i_concert);
                }
            }

        } // SearchMusicianNames


        /// <summary>Search musician texts</summary>
        static private void SearchMusicianTexts(String i_search, int i_doc_index, int i_concert)
        {
            int n_musicians = JazzXml.GetNumberMusicians(i_concert);           
            if (0 == n_musicians)
                return;

            for (int i_musician = 1; i_musician <= n_musicians; i_musician++)
            {
                String musician_text = JazzXml.GetMusicianText(i_concert, i_musician);
                if (JazzXml.XmlNodeValueIsSet(musician_text))
                {
                    CompareStringsAddOutput(musician_text, i_search, i_doc_index, i_concert);
                }
            }

        } // SearchMusicianTexts

        /// <summary>Search band</summary>
        static private void SearchBand(String i_search, int i_doc_index, int i_concert)
        {
            String band_name = JazzXml.GetBandName(i_concert);

            CompareStringsAddOutput(band_name, i_search, i_doc_index, i_concert);

        } // SearchBand

        /// <summary>Search texts</summary>
        static private void SearchTexts(String i_search, int i_doc_index, int i_concert)
        {
            String short_text = JazzXml.GetShortText(i_concert);

            CompareStringsAddOutput(short_text, i_search, i_doc_index, i_concert);

            SearchMusicianTexts(i_search, i_doc_index, i_concert);

        } // SearchTexts



        #endregion // Execution functions

        #region Get functions

        /// <summary>Returns the number of results in a result batch,i.e. the maximum size of a returned array</summary>
        static public int GetNumberResultsInBatch() { return m_return_n_results; }

        /// <summary>Returns a batch of result strings: Date Year Band name</summary>
        static public String[] GetBatchStrings(int i_index_batch)
        {
            String[] ret_strings = null;

            int[] batch_seasons = GetBatchSeasons(i_index_batch);
            if (null == batch_seasons) return ret_strings;
            int[] batch_concerts = GetBatchConcerts(i_index_batch);
            if (null == batch_concerts) return ret_strings;

            if (batch_seasons.Length != batch_concerts.Length) return ret_strings;

            int n_string_array = batch_seasons.Length;
            if (0 == n_string_array) return ret_strings;

            XDocument current_season_document = JazzXml.GetDocumentCurrent();
            if (null == current_season_document) return ret_strings;

            XDocument[] season_documents = JazzXml.GetSeasonDocuments();
            if (null == season_documents) return ret_strings;
            if (0 == season_documents.Length) return ret_strings;

            ret_strings = new String[n_string_array];

            for (int i_str = 0; i_str < n_string_array; i_str++)
            {
                int index_season = batch_seasons[i_str];
                int concert_number = batch_concerts[i_str];

                JazzXml.SetDocumentCurrent(season_documents[index_season]);

                String day = JazzXml.GetDay(concert_number);
                String month = JazzXml.GetMonth(concert_number);
                String year = JazzXml.GetYear(concert_number);
                String band = JazzXml.GetBandName(concert_number);

                String out_str = day + "/" + month + "-" + year + " " + band;

                ret_strings[i_str] = out_str;
            }

            JazzXml.SetDocumentCurrent(current_season_document);

            return ret_strings;
        } // GetBatchStrings

        /// <summary>Returns a batch of result season indices</summary>
        static public int[] GetBatchSeasons(int i_index_batch)
        {
            return GetBatchArray(m_season_indices_list, i_index_batch);
        } // GetBatchSeasons

        /// <summary>Returns a batch of result concert numbers</summary>
        static public int[] GetBatchConcerts(int i_index_batch)
        {
            return GetBatchArray(m_concert_numbers_list, i_index_batch);
        } // GetBatchConcerts

        /// <summary>Returns a batch integer array. Input is an integer list</summary>
        static private int[] GetBatchArray(List<int> i_integer_list, int i_index_batch)
        {
            int[] ret_integer_array = null;

            int start_index = GetBatchStartIndex(i_index_batch);
            if (start_index < 0) return ret_integer_array;

            int end_index = GetBatchEndIndex(start_index);
            if (end_index < 0) return ret_integer_array;

            int size_array = end_index - start_index + 1;

            if (size_array < 1)
            {
                return ret_integer_array;
            }

            if (start_index < 0 || start_index >= i_integer_list.Count())
            {
                return ret_integer_array;
            }

            if (end_index < 0 || end_index >= i_integer_list.Count())
            {
                return ret_integer_array;
            }

            ret_integer_array = new int[size_array];

            int i_out = 0;
            for (int i_get = start_index; i_get <= end_index; i_get++)
            {
                if (i_out >= ret_integer_array.Length)
                {
                    return null;
                }

                ret_integer_array[i_out] = i_integer_list[i_get];
                i_out = i_out + 1;
            }

            return ret_integer_array;
        } // GetBatchArray

        /// <summary>Returns the start index of the batch</summary>
        static private int GetBatchStartIndex(int i_index_batch)
        {
            int start_index = i_index_batch * m_return_n_results;
            int number_results = GetNumberResults();
            if (number_results < 0) return -1;
            if (start_index > number_results - 1)
                return -2;

            return start_index;
        } // GetBatchStartIndex

        /// <summary>Returns the end index of the batch</summary>
        static private int GetBatchEndIndex(int i_start_index)
        {

            int number_results = GetNumberResults();
            if (number_results < 0) return -1;

            int end_index = i_start_index + m_return_n_results - 1;
            if (end_index > number_results - 1)
            {
                end_index = number_results - 1;
            }

            return end_index;
        } // GetBatchEndIndex

        /// <summary>Return the number of results defined by the size of arrays  m_season_indices and m_concert_numbers</summary>
        static private int GetNumberResults()
        {
            int ret_number_results = -12345;

            if (null == m_season_indices || null == m_concert_numbers)
            {
                 return -1;
            }

            int n_seasons = m_season_indices.Length;
            int n_concerts = m_concert_numbers.Length;
            if (n_seasons != n_concerts)
            {
                return -2;
            }

            ret_number_results = n_seasons;

            return ret_number_results;
        } // GetNumberResults

        #endregion // Get functions

        #region Execution help functions

        /// <summary>Search texts</summary>
        static private void CompareStringsAddOutput(String i_string_to_search, String i_search, int i_doc_index, int i_concert)
        {
            if (null == i_string_to_search) return;
            if (0 == i_string_to_search.Length) return;
            if (null == i_search) return;
            if (0 == i_search.Length) return;

            // http://stackoverflow.com/questions/8494703/find-a-substring-in-a-case-insensitive-way-c-sharp
            Boolean b_contains_string = i_string_to_search.IndexOf(i_search, StringComparison.OrdinalIgnoreCase) >= 0;


            if (b_contains_string)
            {
                AddConcertIfNotExisting(i_doc_index, i_concert);
            }

        } // CompareStringsAddOutput

        /// <summary> Add concert if not already existing in the output arrays</summary>
        static private void AddConcertIfNotExisting(int i_doc_index, int i_concert)
        {
            int number_results = m_season_indices_list.Count(); // TODO
            if (number_results < 0) return;

            Boolean b_add_to_arrays = true;
            for (int i_elem = 0; i_elem < number_results; i_elem++)
            {
                int doc_index = m_season_indices_list[i_elem];        // TODO Check 
                int concert_number = m_concert_numbers_list[i_elem];
                if (i_doc_index == doc_index && i_concert == concert_number)
                {
                    b_add_to_arrays = false;
                    break;
                }
            }

            if (b_add_to_arrays)
            {
                m_season_indices_list.Add(i_doc_index);
                m_concert_numbers_list.Add(i_concert);
            }
        } // AddConcertIfNotExisting

        #endregion // Execution help functions


    } // JazzSearch
} // namespace
