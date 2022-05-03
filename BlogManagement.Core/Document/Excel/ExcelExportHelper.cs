﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace BlogManagement.Core.Document.Excel
{
    /// <summary>
    /// Excel导出操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelExportHelper<T>
    {
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excelConfig">导出设置包含文件名、标题、列设置</param>
        /// <param name="addFilePath"></param>
        public static void Export(List<T> list, ExcelConfig excelConfig, string addFilePath)
        {
            var file = ExportMemoryStream(list, excelConfig);
            ExportWrite(addFilePath, file);
        }

        public static void Export(List<Dictionary<string, object>> list, ExcelConfig excelConfig, string addFilePath, List<string> columnList)
        {
            var file = ExportMemoryStream(list, excelConfig, columnList);
            ExportWrite(addFilePath, file);
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excelConfig">导出设置包含文件名、标题、列设置</param>
        public static MemoryStream ToStream(List<T> list, ExcelConfig excelConfig)
        {
            return ExportMemoryStream(list, excelConfig);
        }
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="path">模板路径</param>
        /// <param name="addFilePath">保存文件名</param>
        public static void Export(List<ExcelTemplateModel> list, string path, string addFilePath)
        {
            var file = ExcelExportTemplate(list, path);
            ExportWrite(addFilePath, file);
        }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="memoryStream">文件流</param>
        public static void ExportWrite(string filePath, MemoryStream memoryStream)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = memoryStream.ToArray();//转化为byte格式存储
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                    buffer = null;
                }//使用using可以最后不用关闭fs 比较方便
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region List<T>导出到Excel模板

        /// <summary T="导出到Excel模板">
        /// List
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="filePath">模板路径</param>
        private static MemoryStream ExcelExportTemplate(List<ExcelTemplateModel> list, string filePath)
        {
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                ISheet sheet = null;
                if (filePath.IndexOf(".xls", StringComparison.Ordinal) == -1)//2003
                {
                    HSSFWorkbook hssfWorkbook = new HSSFWorkbook(fileStream);
                    sheet = hssfWorkbook.GetSheetAt(0);
                    SetPurchaseOrder(sheet, list);
                    sheet.ForceFormulaRecalculation = true;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        hssfWorkbook.Write(ms);
                        ms.Flush();
                        return ms;
                    }
                }
                else//2007
                {
                    XSSFWorkbook xssfWorkbook = new XSSFWorkbook(fileStream);
                    sheet = xssfWorkbook.GetSheetAt(0);
                    SetPurchaseOrder(sheet, list);
                    sheet.ForceFormulaRecalculation = true;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        xssfWorkbook.Write(ms);
                        ms.Flush();
                        return ms;
                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// <summary>
        /// 赋值单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="list"></param>
        private static void SetPurchaseOrder(ISheet sheet, List<ExcelTemplateModel> list)
        {
            try
            {
                foreach (var item in list)
                {
                    IRow row = null;
                    ICell cell = null;
                    row = sheet.GetRow(item.row);
                    if (row == null)
                    {
                        row = sheet.CreateRow(item.row);
                    }
                    cell = row.GetCell(item.cell);
                    if (cell == null)
                    {
                        cell = row.CreateCell(item.cell);
                    }
                    cell.SetCellValue(item.value);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
        #endregion

        #region  List<T>导出到Excel的MemoryStream
        /// <summary>
        /// List<T>导出到Excel的MemoryStream Export()
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="excelConfig">导出设置包含文件名、标题、列设置</param>
        private static MemoryStream ExportMemoryStream(List<T> list, ExcelConfig excelConfig)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();

                #region 右击文件 属性信息
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "NPOI";
                    workbook.DocumentSummaryInformation = dsi;
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "BlogManager"; //填加xls文件作者信息
                    si.ApplicationName = "BlogManager"; //填加xls文件创建程序信息
                    si.LastAuthor = "BlogManager"; //填加xls文件最后保存者信息
                    si.Comments = "BlogManager"; //填加xls文件作者信息
                    si.Title = "标题信息"; //填加xls文件标题信息
                    si.Subject = "主题信息";//填加文件主题信息
                    si.CreateDateTime = System.DateTime.Now;
                    workbook.SummaryInformation = si;
                }
                #endregion

                #region 设置标题样式
                ICellStyle headStyle = workbook.CreateCellStyle();
                int[] arrColWidth = new int[properties.Length];
                string[] arrColName = new string[properties.Length];//列名
                ICellStyle[] arrColumnStyle = new ICellStyle[properties.Length];//样式表
                                                                                //headStyle.BorderBottom = BorderStyle.Thin;
                                                                                //headStyle.BorderLeft = BorderStyle.Thin;
                                                                                //headStyle.BorderRight = BorderStyle.Thin;
                                                                                //headStyle.BorderTop = BorderStyle.Thin;
                headStyle.Alignment = HorizontalAlignment.Left;
                if (excelConfig.Background != new Color())
                {
                    if (excelConfig.Background != new Color())
                    {
                        headStyle.FillPattern = FillPattern.SolidForeground;
                        headStyle.FillForegroundColor = GetXlColour(workbook, excelConfig.Background);
                    }
                }
                IFont font = workbook.CreateFont();
                font.FontHeightInPoints = excelConfig.TitlePoint;
                if (excelConfig.ForeColor != new Color())
                {
                    font.Color = GetXlColour(workbook, excelConfig.ForeColor);
                }
                font.IsBold = true;
                headStyle.SetFont(font);
                #endregion

                #region 列头及样式
                ICellStyle cHeadStyle = workbook.CreateCellStyle();
                cHeadStyle.Alignment = HorizontalAlignment.Left;
                IFont cFont = workbook.CreateFont();
                cFont.FontHeightInPoints = excelConfig.HeadPoint;
                cHeadStyle.SetFont(cFont);
                #endregion

                #region 设置内容单元格样式
                int i = 0;
                foreach (PropertyInfo column in properties)
                {
                    ICellStyle columnStyle = workbook.CreateCellStyle();
                    columnStyle.Alignment = HorizontalAlignment.Left;
                    arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(column.Name).Length;
                    arrColName[i] = column.Name;

                    if (excelConfig.ColumnModel != null)
                    {
                        ExcelColumnModel columnModel = excelConfig.ColumnModel.Find(t => t.Column == column.Name);
                        if (columnModel != null)
                        {
                            arrColName[i] = columnModel.ExcelColumn;
                            if (columnModel.Width != 0)
                            {
                                arrColWidth[i] = columnModel.Width;
                            }
                            if (columnModel.Background != new Color())
                            {
                                if (columnModel.Background != new Color())
                                {
                                    columnStyle.FillPattern = FillPattern.SolidForeground;
                                    columnStyle.FillForegroundColor = GetXlColour(workbook, columnModel.Background);
                                }
                            }
                            if (columnModel.Font != null || columnModel.Point != 0 || columnModel.ForeColor != new Color())
                            {
                                IFont columnFont = workbook.CreateFont();
                                columnFont.FontHeightInPoints = 10;
                                if (columnModel.Font != null)
                                {
                                    columnFont.FontName = columnModel.Font;
                                }
                                if (columnModel.Point != 0)
                                {
                                    columnFont.FontHeightInPoints = columnModel.Point;
                                }
                                if (columnModel.ForeColor != new Color())
                                {
                                    columnFont.Color = GetXlColour(workbook, columnModel.ForeColor);
                                }
                                columnStyle.SetFont(font);
                            }
                        }
                    }
                    arrColumnStyle[i] = columnStyle;
                    i++;
                }
                #endregion

                #region 填充数据
                ICellStyle dateStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                int rowIndex = 0;
                #region 新建表，填充表头，填充列头，样式

                #region 表头及样式
                {
                    if (excelConfig.Title != null)
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        if (excelConfig.TitleHeight != 0)
                        {
                            headerRow.Height = (short)(excelConfig.TitleHeight * 20);
                        }
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(excelConfig.Title);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, excelConfig.ColumnModel.Count - 1)); // ------------------
                    }
                }
                #endregion

                #region 列头及样式
                {
                    var rownum = 0;
                    if (excelConfig.Title != null)
                    {
                        rowIndex = 2;
                        rownum = 1;
                    }
                    else
                    {
                        rowIndex = 1;
                    }
                    IRow headerRow = sheet.CreateRow(rownum);
                    #region 如果设置了列标题就按列标题定义列头，没定义直接按字段名输出
                    int headIndex = 0;
                    if (excelConfig.ColumnModel != null)
                        foreach (ExcelColumnModel excelColumnModel in excelConfig.ColumnModel)
                        {
                            headerRow.CreateCell(headIndex).SetCellValue(excelColumnModel.ExcelColumn);
                            headerRow.GetCell(headIndex).CellStyle = cHeadStyle;
                            //设置列宽
                            sheet.SetColumnWidth(headIndex, (arrColWidth[headIndex] + 1) * 256);
                            headIndex++;
                        }

                    #endregion
                }
                #endregion


                #endregion
                if (list != null)
                {
                    foreach (T item in list)
                    {
                        #region 填充内容
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        int ordinal = 0;
                        foreach (PropertyInfo column in properties)
                        {
                            if (excelConfig.ColumnModel != null)
                            {
                                ExcelColumnModel columnModel = excelConfig.ColumnModel.Find(t => t.Column == column.Name);
                                if (columnModel != null)
                                {
                                    ICell newCell = dataRow.CreateCell(ordinal);
                                    newCell.CellStyle = arrColumnStyle[ordinal];
                                    string drValue = column.GetValue(item, null) == null ? "" : column.GetValue(item, null)?.ToString();
                                    SetCell(newCell, dateStyle, column.PropertyType, drValue);
                                    ordinal++;
                                }
                            }
                        }
                        #endregion
                        rowIndex++;
                    }
                }
                #endregion

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    return ms;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region  List<Dictionary<string, object>>导出到Excel的MemoryStream

        private static MemoryStream ExportMemoryStream(List<Dictionary<string, object>> list, ExcelConfig excelConfig, List<string> columnList)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;
                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "BlogManager"; //填加xls文件作者信息
                si.ApplicationName = "BlogManager"; //填加xls文件创建程序信息
                si.LastAuthor = "BlogManager"; //填加xls文件最后保存者信息
                si.Comments = "BlogManager"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 设置标题样式
            ICellStyle headStyle = workbook.CreateCellStyle();
            int[] arrColWidth = new int[columnList.Count];
            string[] arrColName = new string[columnList.Count];//列名
            ICellStyle[] arryColumStyle = new ICellStyle[columnList.Count];//样式表
            //headStyle.BorderBottom = BorderStyle.Thin;
            //headStyle.BorderLeft = BorderStyle.Thin;
            //headStyle.BorderRight = BorderStyle.Thin;
            //headStyle.BorderTop = BorderStyle.Thin;
            headStyle.Alignment = HorizontalAlignment.Left;
            if (excelConfig.Background != new Color())
            {
                if (excelConfig.Background != new Color())
                {
                    headStyle.FillPattern = FillPattern.SolidForeground;
                    headStyle.FillForegroundColor = GetXlColour(workbook, excelConfig.Background);
                }
            }
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = excelConfig.TitlePoint;
            if (excelConfig.ForeColor != new Color())
            {
                font.Color = GetXlColour(workbook, excelConfig.ForeColor);
            }
            font.IsBold = true;
            headStyle.SetFont(font);
            #endregion

            #region 列头及样式
            ICellStyle cHeadStyle = workbook.CreateCellStyle();
            cHeadStyle.Alignment = HorizontalAlignment.Left;
            IFont cfont = workbook.CreateFont();
            cfont.FontHeightInPoints = excelConfig.HeadPoint;
            cHeadStyle.SetFont(cfont);
            #endregion

            #region 设置内容单元格样式
            int i = 0;

            foreach (var column in columnList)
            {
                ICellStyle columnStyle = workbook.CreateCellStyle();
                columnStyle.Alignment = HorizontalAlignment.Left;
                arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(column).Length;
                arrColName[i] = column;

                if (excelConfig.ColumnModel != null)
                {
                    ExcelColumnModel columnModel = excelConfig.ColumnModel.Find(t => t.ExcelColumn == column);
                    if (columnModel != null)
                    {
                        arrColName[i] = columnModel.ExcelColumn;
                        if (columnModel.Width != 0)
                        {
                            arrColWidth[i] = columnModel.Width;
                        }
                        if (columnModel.Background != new Color())
                        {
                            if (columnModel.Background != new Color())
                            {
                                columnStyle.FillPattern = FillPattern.SolidForeground;
                                columnStyle.FillForegroundColor = GetXlColour(workbook, columnModel.Background);
                            }
                        }
                        if (columnModel.Font != null || columnModel.Point != 0 || columnModel.ForeColor != new Color())
                        {
                            IFont columnFont = workbook.CreateFont();
                            columnFont.FontHeightInPoints = 10;
                            if (columnModel.Font != null)
                            {
                                columnFont.FontName = columnModel.Font;
                            }
                            if (columnModel.Point != 0)
                            {
                                columnFont.FontHeightInPoints = columnModel.Point;
                            }
                            if (columnModel.ForeColor != new Color())
                            {
                                columnFont.Color = GetXlColour(workbook, columnModel.ForeColor);
                            }
                            columnStyle.SetFont(font);
                        }
                    }
                }
                arryColumStyle[i] = columnStyle;
                i++;
            }

            #endregion

            #region 填充数据
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            int rowIndex = 0;
            #region 新建表，填充表头，填充列头，样式
            if (rowIndex == 65535 || rowIndex == 0)
            {
                if (rowIndex != 0)
                {
                    sheet = workbook.CreateSheet();
                }

                #region 表头及样式
                {
                    if (excelConfig.Title != null)
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        if (excelConfig.TitleHeight != 0)
                        {
                            headerRow.Height = (short)(excelConfig.TitleHeight * 20);
                        }
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(excelConfig.Title);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, excelConfig.ColumnModel.Count - 1)); // ------------------
                    }
                }
                #endregion

                #region 列头及样式
                {
                    var _rownum = 0;
                    if (excelConfig.Title != null)
                    {
                        rowIndex = 2;
                        _rownum = 1;
                    }
                    else
                    {
                        rowIndex = 1;
                    }
                    IRow headerRow = sheet.CreateRow(_rownum);
                    #region 如果设置了列标题就按列标题定义列头，没定义直接按字段名输出
                    int headIndex = 0;
                    foreach (ExcelColumnModel excelColumnModel in excelConfig.ColumnModel)
                    {
                        headerRow.CreateCell(headIndex).SetCellValue(excelColumnModel.ExcelColumn);
                        headerRow.GetCell(headIndex).CellStyle = cHeadStyle;
                        //设置列宽
                        sheet.SetColumnWidth(headIndex, (arrColWidth[headIndex] + 1) * 256);
                        headIndex++;
                    }
                    #endregion
                }
                #endregion


            }
            #endregion
            if (list != null)
            {
                foreach (Dictionary<string, object> item in list)
                {
                    #region 填充内容
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    int ordinal = 0;
                    foreach (var column in excelConfig.ColumnModel)
                    {
                        var colunValue = item.Where(i => i.Key.Contains(column.Column)).FirstOrDefault();
                        if (colunValue.Key != null && colunValue.Value != null)
                        {
                            ICell newCell = dataRow.CreateCell(ordinal);
                            newCell.CellStyle = arryColumStyle[ordinal];
                            string drValue = colunValue.Value.ToString();
                            SetCell(newCell, dateStyle, drValue.GetType(), drValue);
                            ordinal++;
                        }
                        else
                        {
                            ICell newCell = dataRow.CreateCell(ordinal);
                            newCell.CellStyle = arryColumStyle[ordinal];
                            string drValue = string.Empty;
                            SetCell(newCell, dateStyle, drValue.GetType(), drValue);
                            ordinal++;
                        }
                    }
                    foreach (var column in item)
                    {
                        ExcelColumnModel columnModel = excelConfig.ColumnModel.Find(t => t.Column == column.Key);
                        if (columnModel != null)
                        {

                        }
                    }
                    #endregion
                    rowIndex++;
                }
            }
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
        #endregion

        #region 设置表格内容
        private static void SetCell(ICell newCell, ICellStyle dateStyle, Type dataType, string drValue)
        {
            switch (dataType.ToString())
            {
                case "System.String"://字符串类型
                    newCell.SetCellValue(drValue);
                    break;
                case "System.DateTime"://日期类型
                    {
                        if (System.DateTime.TryParse(drValue, out var dateV))
                        {
                            newCell.SetCellValue(dateV);
                        }
                        else
                        {
                            newCell.SetCellValue("");
                        }
                        newCell.CellStyle = dateStyle;//格式化显示
                    }
                    break;
                case "System.Boolean"://布尔型
                    bool.TryParse(drValue, out var boolV);
                    newCell.SetCellValue(boolV);
                    break;
                case "System.Int16"://整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int.TryParse(drValue, out var intV);
                    newCell.SetCellValue(intV);
                    break;
                case "System.Decimal"://浮点型
                case "System.Double":
                    double.TryParse(drValue, out var dobV);
                    newCell.SetCellValue(dobV);
                    break;
                case "System.DBNull"://空值处理
                    newCell.SetCellValue("");
                    break;
                default:
                    newCell.SetCellValue("");
                    break;
            }
        }
        #endregion

        #region RGB颜色转NPOI颜色
        private static short GetXlColour(HSSFWorkbook workbook, Color systemColor)
        {
            short s = 0;
            HSSFPalette xlPalette = workbook.GetCustomPalette();
            NPOI.HSSF.Util.HSSFColor xlColour = xlPalette.FindColor(systemColor.R, systemColor.G,systemColor.B);
            if (xlColour == null)
            {
                xlColour = xlPalette.FindSimilarColor(systemColor.R, systemColor.G, systemColor.B);
                s = xlColour.Indexed;

            }
            else
                s = xlColour.Indexed;
            return s;
        }
        #endregion

        #region 类型转换
        object ValueType(Type t, string value)
        {
            object o = null;
            string str = "String";
            if (t.Name == "Nullable`1")
            {
                str = t.GetGenericArguments()[0].Name;
            }
            switch (str)
            {
                case "Decimal":
                    o = decimal.Parse(value);
                    break;
                case "Int":
                    o = int.Parse(value);
                    break;
                case "Float":
                    o = float.Parse(value);
                    break;
                case "DateTime":
                    o = DateTime.Parse(value);
                    break;
                default:
                    o = value;
                    break;
            }
            return o;
        }
        #endregion
    }
}
