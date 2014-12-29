#define _DEBUG

using System.Text.RegularExpressions;
using System.Linq;

namespace WindowsAP_CS
{

    class myString
    {
        /* Region
         * To declare variable
         */
        const string _NULL_STRING = "";
        const char _DELIMITER = ' ';
        
        ////4. get the LCS length"
        public int getLCSLength(string lsComparePattern1, string lsComparePattern2, out string[,] ArrayOfLCSResult)
        {
            string[] lsComparePattern1_Array;
            string[] lsComparePattern2_Array;
            int LCS_TABLE_WIDTH_;
            int LCS_TABLE_HEIGHT_;
            int[,] LCS_LENGTH_ARRAY;  //''Table for calculating the length
            string[,] LCS_FLAG_ARRAY; //''Table for storing the flag when retrieving the LCS string
            string[] lsString1ToCompare;
            string[] lsString2ToCompare;

            try
            {
                lsComparePattern1_Array = lsComparePattern1.Split(_DELIMITER);
                lsComparePattern2_Array = lsComparePattern2.Split(_DELIMITER);

                LCS_TABLE_WIDTH_ = lsComparePattern1_Array.GetLength(0) + 1;
                LCS_TABLE_HEIGHT_ = lsComparePattern2_Array.GetLength(0) + 1;

                LCS_LENGTH_ARRAY = new int[LCS_TABLE_WIDTH_, LCS_TABLE_HEIGHT_];
                LCS_FLAG_ARRAY = new string[LCS_TABLE_WIDTH_, LCS_TABLE_HEIGHT_];
                lsString1ToCompare = new string[LCS_TABLE_WIDTH_];
                lsString2ToCompare = new string[LCS_TABLE_HEIGHT_];

                //setting the a and b string starting from 1 to m and n
                for (int i = 0; i <= lsComparePattern1_Array.GetLength(0) - 1; i++)
                    lsString1ToCompare[i + 1] = lsComparePattern1_Array[i];

                for (int j = 0; j <= lsComparePattern2_Array.GetLength(0) - 1; j++)
                    lsString2ToCompare[j + 1] = lsComparePattern2_Array[j];

                //''初始化結果陣列
                for (int i = 0; i <= lsString1ToCompare.GetLength(0) - 1; i++)
                {
                    LCS_LENGTH_ARRAY[i, 0] = 0;
                    LCS_FLAG_ARRAY[i, 0] = "SKIPX";
                }

                for (int j = 0; j <= lsString2ToCompare.GetLength(0) - 1; j++)
                {
                    LCS_LENGTH_ARRAY[0, j] = 0;
                    LCS_FLAG_ARRAY[0, j] = "SKIPY";
                }
                //''初始化結果陣列

                for (int ii = 1; ii <= lsString1ToCompare.GetLength(0) - 1; ii++)
                {
                    for (int jj = 1; jj <= lsString2ToCompare.GetLength(0) - 1; jj++)
                    {
                        if (lsString1ToCompare[ii] == lsString2ToCompare[jj])
                        {
                            LCS_LENGTH_ARRAY[ii, jj] = LCS_LENGTH_ARRAY[ii - 1, jj - 1] + 1;
                            LCS_FLAG_ARRAY[ii, jj] = "addXY";
                        }
                        else if (LCS_LENGTH_ARRAY[ii - 1, jj] >= LCS_LENGTH_ARRAY[ii, jj - 1])
                        {
                            LCS_LENGTH_ARRAY[ii, jj] = LCS_LENGTH_ARRAY[ii, jj - 1];
                            LCS_FLAG_ARRAY[ii, jj] = "SKIPX";
                        }
                        else
                        {
                            LCS_LENGTH_ARRAY[ii, jj] = LCS_LENGTH_ARRAY[ii, jj - 1];
                            LCS_FLAG_ARRAY[ii, jj] = "SKIPY";
                        }
                    }
                }

                ArrayOfLCSResult = LCS_FLAG_ARRAY;
                return LCS_LENGTH_ARRAY[LCS_TABLE_WIDTH_ - 1, LCS_TABLE_HEIGHT_ - 1];
            }
            catch (System.Exception ex)
            {
                ArrayOfLCSResult = null;
                return -1;
                throw ex;
            }
        }

        ////    5. get the Longest Common Substring (LCS) from two strings with space seperated (without ordering)"
        ////BACDB & BDCB => BCB
        public string getLCS(string lsComparePattern1, string lsComparePattern2)
        {
            //'' Param
            //'' lsComparePattern1 : the 1st string as the input parameter
            //'' lsComparePattern2 : the 2nd string as the input parameter
            string LCS = _NULL_STRING;
            string[] lsComparePattern1_Array;
            string[] lsComparePattern2_Array;
            string[,] LCS_FLAG_ARRAY;  //''Table for storing the flag when retrieving the LCS string
            int i;
            int j;

            try
            {
                lsComparePattern1_Array = lsComparePattern1.Trim().Split(_DELIMITER);
                lsComparePattern2_Array = lsComparePattern2.Trim().Split(_DELIMITER);

                //'' Call getLCSLength with a variable to capture the returned value
                //''Primarily to fill matrix LCS_FLAG_ARRAY
                getLCSLength(lsComparePattern1.Trim(), lsComparePattern2.Trim(), out LCS_FLAG_ARRAY);

                i = LCS_FLAG_ARRAY.GetLength(0) - 1;    //''長  列數
                j = LCS_FLAG_ARRAY.GetLength(1) - 1;    //''寬  欄數

                while ((i != 0) && (j != 0))
                {
                    switch (LCS_FLAG_ARRAY[i, j])
                    {
                        case "addXY":
                            {
                                LCS = string.Concat(lsComparePattern2_Array[j - 1], " ", LCS);
                                i--;
                                j--;
                                break;
                            }
                        case "SKIPX":
                            {
                                i--;
                                break;
                            }
                        case "SKIPY":
                            {
                                j--;
                                break;
                            }
                    }
                }
                return LCS.Trim();

            }
            catch (System.Exception ex)
            {
                return LCS;
                throw ex;
            }
        }
    }
}