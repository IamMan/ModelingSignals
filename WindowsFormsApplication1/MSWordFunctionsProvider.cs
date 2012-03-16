using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

using Word = Microsoft.Office.Interop.Word;

namespace WindowsFormsApplication1
{
    class MSWordFunctionsProvider
    {
        private static object missing = System.Reflection.Missing.Value;
        //private static Word.Application wordapp = new Word.Application();
        
        
        public static Document OpenDocument(Word.Application wordapp, string fileName)
        {
            wordapp.Visible = true;
            //Setting for open
            string FileName = fileName;
            bool ConfirmConversions = false;
            bool ReadOnly = false;
            bool AddToRecentFiles = false;
            object PasswordDocument = missing;
            object PasswordTemplate = missing;
            bool Revert = false;
            object WritePasswordDocument = missing;
            object WritePasswordTemplate = missing;
            object Format = missing;
            object Encoding = missing;
            bool Visible = true;
            bool OpenAndRepair = true;
            object DocumentDirection = missing;
            bool NoEncodingDialog = false;
            object XMLTransform = missing;
            //
            return  wordapp.Documents.Open(FileName, ConfirmConversions, ReadOnly, AddToRecentFiles,
                                                        PasswordDocument, PasswordTemplate, Revert, WritePasswordDocument,
                                                        WritePasswordTemplate, Format, Encoding, Visible, OpenAndRepair,
                                                        DocumentDirection = missing, NoEncodingDialog, XMLTransform);
        }

        public static bool FindAndRepalce(Range range, object FindText, object replaceText = null)
        {
            object missing = System.Reflection.Missing.Value;
            //Find parametrs
            object MatchCase = true;
            object MatchWholeWord = true;
            object MatchWildcards = false;
            object MatchSoundsLike = false;
            object MatchAllWordForms = false;
            object Forward = true;
            object Wrap = 1;
            object Format = false;
            object ReplaceWith = replaceText;
            object Replace = WdReplace.wdReplaceOne;
            object MatchKashida = false;
            object MatchDiacritics = false;
            object MatchAlefHamza = false;
            object MatchControl = false;
            //

            return range.Find.Execute(FindText, MatchCase, MatchWholeWord, MatchWildcards, MatchSoundsLike, MatchAllWordForms, Forward,
                                           Wrap, Format, ReplaceWith, Replace, MatchKashida, MatchDiacritics, MatchAlefHamza, MatchControl);
        }

        public static bool FindAndRepalce(Word.Application wordApp, object findText, object replaceText = null)
        {
            object missing = System.Reflection.Missing.Value;
            //Find parametrs
            object MatchCase = true;
            object MatchWholeWord = true;
            object MatchWildcards = false;
            object MatchSoundsLike = false;
            object MatchAllWordForms = false;
            object Forward = true;
            object Wrap = 1;
            object Format = false;
            object ReplaceWith = replaceText;
            object Replace = WdReplace.wdReplaceAll;
            object MatchKashida = false;
            object MatchDiacritics = false;
            object MatchAlefHamza = false;
            object MatchControl = false;
            //

            return wordApp.Selection.Find.Execute(findText, MatchCase, MatchWholeWord, MatchWildcards, MatchSoundsLike, MatchAllWordForms, Forward,
                                           Wrap, Format, ReplaceWith, Replace, MatchKashida, MatchDiacritics, MatchAlefHamza, MatchControl);
        }

        public static void InsertImageTo(string imgSrc, Document doc, string imgText)
        {
            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                Range range = paragraph.Range;
                if (FindAndRepalce(range, imgText, ""))
                {
                    Object oLinkToFile = false;  //default
                    Object oSaveWithDocument = true;//default
                    range.Document.InlineShapes.AddPicture(imgSrc, oLinkToFile, oSaveWithDocument, range);
                    break;
                }
            }
        }
    }
}
