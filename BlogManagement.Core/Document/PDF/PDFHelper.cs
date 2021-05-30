using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spire.Presentation;

namespace BlogManagement.Core
{
    public class PDFHelper
    {
        #region 转换PDF 需要在nuget获取FreeSpire.Office 注意：如果需要版权自行找官方购买这个组件授权
        /// <summary>
        /// Excel转换PDF
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public static void ExcelToPDF(string fileName)
        {
            try
            {
                if (!FileHelper.IsExistFile(fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf"))
                {
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(fileName, ExcelVersion.Version97to2003);
                    workbook.ConverterSetting.SheetFitToPage = true;//关键   自适应pdf
                    workbook.SaveToFile(fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf", Spire.Xls.FileFormat.PDF);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionEx.Throw(ex);
            }
        }
        /// <summary>
        /// Word转换PDF
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public static void WordToPDF(string fileName)
        {
            try
            {
                if (!FileHelper.IsExistFile(fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf"))
                {
                    Stream stream = File.OpenRead(fileName);
                    var document = new Spire.Doc.Document();
                    document.LoadFromFile(fileName);
                    document.SaveToFile(fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf", Spire.Doc.FileFormat.PDF);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionEx.Throw(ex);
            }
        }
        /// <summary>
        /// Aspose组件word转成pdf文件
        /// </summary>
        /// <param name="fileName">word文件路径</param>
        public static void AsposeWordToPDF(string fileName)
        {
            try
            {
                var pdfSavePath = fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf";
                if (!FileHelper.IsExistFile(pdfSavePath))
                {
                    var document = new Aspose.Words.Document(fileName);
                    if (document == null)
                    {
                        throw new Exception("Word文件无效");
                    }

                    document.Save(pdfSavePath, Aspose.Words.SaveFormat.Pdf);
                }
            }
            catch (Exception ex)
            {

                throw ExceptionEx.Throw(ex);
            }
        }



        /// <summary>
        /// PPT转换PDF
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public static void PPTToPDF(string fileName)
        {
            try
            {
                if (!FileHelper.IsExistFile(fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf"))
                {
                    Presentation presentation = new Presentation();
                    presentation.LoadFromFile(fileName);
                    presentation.SaveToFile(fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf", Spire.Presentation.FileFormat.PDF);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionEx.Throw(ex);
            }
        }
        #endregion
    }
}
