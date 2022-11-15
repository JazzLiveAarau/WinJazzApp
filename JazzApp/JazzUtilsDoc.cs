using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzApp
{
    /// <summary>Utility functions for the jazz admimn documents application.
    /// <para></para>
    /// </summary>
    static public partial class JazzUtils
    {

        /// <summary>Returns an int array of start years for existing XML document files on the server
        /// <para>There is a limit of files after current year (m_number_of_possible_xmls_after_current_year)</para>
        /// <para>Start years without a corresponding non-corrupt XML file on the server are removed</para>
        /// </summary>
        public static int[] GetSeasonStartYearsForExistingXmlDocumentsFiles(string i_url_xml_doc_files_folder, int i_documents_start_year)
        {
            int[] ret_start_years = null;
          
            int current_end_year = GetCurrentSeasonStartYear() + 1;
            int size_possible_years = current_end_year - i_documents_start_year + MaxNumberOfNewSeasonPrograms;

            int[] possible_years = new int[size_possible_years];

            for (int i_init = 0; i_init < size_possible_years; i_init++)
            {
                possible_years[i_init] = i_documents_start_year + i_init;
            }

            int n_exists = 0;
            for (int i_exist = 0; i_exist < possible_years.Length; i_exist++)
            {
                int test_year = possible_years[i_exist];

                String url_season_file_name = JazzXml.GetSeasonDocumentsFileName(test_year, i_url_xml_doc_files_folder);

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

                String url_season_file_name = JazzXml.GetSeasonDocumentsFileName(test_year_add, i_url_xml_doc_files_folder);

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

        } // GetSeasonStartYearsForExistingXmlDocumentsFiles




    } // JazzUtils
} // namespace
