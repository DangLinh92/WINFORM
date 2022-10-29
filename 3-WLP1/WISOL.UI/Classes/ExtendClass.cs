using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES;

namespace Wisol
{
    public static class ExtendClass
    {
        public static string Translation(this string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code) == true) { return string.Empty; }
                if (Consts.GLOSSARY == null || Consts.GLOSSARY.Rows.Count == 0) { return code; }
                if (code.Contains("'")) { return code; }

                DataRow[] glossary = Consts.GLOSSARY.Select(string.Format("GLSR = '{0}'", code));
                string temp = code;
                if (glossary.Length > 0)
                {
                    switch (Consts.USER_INFO.Language)
                    {
                        case "KOR":
                            code = glossary[0]["KOR"].NullString();
                            break;
                        case "ENG":
                            code = glossary[0]["ENG"].NullString();
                            break;
                        case "CHN":
                            code = glossary[0]["CHN"].NullString();
                            break;
                        case "VTN":
                            code = glossary[0]["VTN"].NullString();
                            break;
                    }
                }
                if (code.Trim() == string.Empty) { code = temp; }

                return code.Replace("\\r", "\r").Replace("\\n", "\n");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
                return string.Empty;
            }
        }

        public static string Translation(this string code, string language)
        {
            try
            {
                if (String.IsNullOrEmpty(code) == true) return String.Empty;
                if (Consts.GLOSSARY == null || Consts.GLOSSARY.Rows.Count == 0) return code;
                if (code.Contains("'")) return code;

                var glossary = Consts.GLOSSARY.AsEnumerable().FirstOrDefault(x => x["GLSR"].NullString() == code);

                if (glossary == null) return String.Empty;

                switch (language)
                {
                    case "KOR":
                        code = glossary["KOR"].NullString();
                        break;
                    case "ENG":
                        code = glossary["ENG"].NullString();
                        break;
                    case "CHN":
                        code = glossary["CHN"].NullString();
                        break;
                    case "VTN":
                        code = glossary["VTN"].NullString();
                        break;
                }
                return code.Replace("\\r", "\r").Replace("\\n", "\n");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
                return String.Empty;
            }
        }

        public static void SetColumnLanguage(GridView gridView, string stdColumn, string Column)
        {
            if (gridView.Columns[stdColumn] == null)
            {
                return;
            }
            if (gridView.Columns[Column] == null)
            {
                return;
            }
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.GetDataRow(i)[Column] = gridView.GetDataRow(i)[stdColumn].NullString().Translation();
            }
        }

        public static void SetStyleFormatCondition(GridView gridView, string Columns, object value1, object value2 = null, Color fontColor = default(Color), Color backColor = default(Color), FontStyle fontStyle = default(FontStyle))
        {
            try
            {
                var styleFormatCondition = new StyleFormatCondition();
                var ConditionFont = gridView.Appearance.Row.Font.Clone() as Font;
                styleFormatCondition.Appearance.Options.UseFont = true;

                if (fontStyle == default(FontStyle))
                    styleFormatCondition.Appearance.Font = new Font(ConditionFont.FontFamily, ConditionFont.Size, ConditionFont.Style);
                else
                    styleFormatCondition.Appearance.Font = new Font(ConditionFont.FontFamily, ConditionFont.Size, fontStyle);

                styleFormatCondition.Appearance.Options.UseForeColor = true;
                styleFormatCondition.Appearance.Options.UseBackColor = true;

                if (fontColor != null || fontColor != default(Color))
                    styleFormatCondition.Appearance.ForeColor = fontColor;

                if (backColor != null || backColor != default(Color))
                    styleFormatCondition.Appearance.BackColor = backColor;

                styleFormatCondition.ApplyToRow = true;
                styleFormatCondition.Column = gridView.Columns[Columns];
                styleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                styleFormatCondition.Value1 = value1;
                styleFormatCondition.Value2 = value2;

                gridView.FormatConditions.Add(styleFormatCondition);
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
