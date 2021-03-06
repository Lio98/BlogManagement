using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BlogManagement.Core
{
    /// <summary>
    /// Excel导入操作
    /// </summary>
    public class ExcelImportHelper
    {
        /// <summary>   
        /// 从Excel中获取数据到DataTable   
        /// </summary>   
        /// <param name="filePath">Excel文件全路径(服务器路径)</param>   
        /// <param name="SheetIndex">要获取数据的工作表序号(从0开始)</param>   
        /// <param name="headerRowIndex">工作表标题行所在行号(从0开始)</param>   
        /// <returns></returns>   
        public static DataTable ToDataTable(string filePath, int SheetIndex = 0, int headerRowIndex = 0)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = null;
                if (filePath.IndexOf(".xlsx") == -1)//2003
                {
                    workbook = new HSSFWorkbook(file);
                }
                else
                {
                    workbook = new XSSFWorkbook(file);
                }
                string SheetName = workbook.GetSheetName(SheetIndex);
                return ToDataTable(workbook, SheetName, headerRowIndex);
            }
        }
        /// <summary>   
        /// 从Excel中获取数据到DataTable   
        /// </summary>   
        /// <param name="workbook">要处理的工作薄</param>   
        /// <param name="sheetName">要获取数据的工作表名称</param>   
        /// <param name="headerRowIndex">工作表标题行所在行号(从0开始)</param>   
        /// <returns></returns>   
        public static DataTable ToDataTable(IWorkbook workbook, string sheetName, int headerRowIndex)
        {
            ISheet sheet = workbook.GetSheet(sheetName);
            DataTable table = new DataTable();
            try
            {
                IRow headerRow = sheet.GetRow(headerRowIndex);
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                int rowCount = sheet.LastRowNum;
                if (rowCount > 0)
                {
                    #region 循环各行各列,写入数据到DataTable
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);
                            if (cell == null)
                            {
                                dataRow[j] = null;
                            }
                            else
                            {
                                if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                                {
                                    dataRow[j] = cell.DateCellValue.ToString("yyyy/MM/dd").Trim();
                                }
                                else
                                {
                                    dataRow[j] = cell.ToString().Trim();
                                }
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                table.Clear();
                table.Columns.Clear();
                table.Columns.Add("出错了");
                DataRow dr = table.NewRow();
                dr[0] = ex.Message;
                table.Rows.Add(dr);
                return table;
            }
            finally
            {
                //sheet.Dispose();   
                workbook = null;
                sheet = null;
            }
            #region 清除最后的空行
            for (int i = table.Rows.Count - 1; i > 0; i--)
            {
                bool isnull = true;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (table.Rows[i][j] != null)
                    {
                        if (table.Rows[i][j].ToString() != "")
                        {
                            isnull = false;
                            break;
                        }
                    }
                }
                if (isnull)
                {
                    table.Rows[i].Delete();
                }
            }
            #endregion

            return table;
        }
    }
}
