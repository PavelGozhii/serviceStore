using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ServiceStore.Services
{
    class PdfConvert
    {
        //public static BaseFont baseFont = BaseFont.CreateFont(@"..\WINDOWS\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        public static BaseFont baseFont = BaseFont.CreateFont("myFont.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true, ServiceStore.Properties.Resources.Royal_Arial_Regular, null);
        public static Font normalfon = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
        public static Font titlefon = new iTextSharp.text.Font(baseFont, 20, iTextSharp.text.Font.BOLD);
        private class Title : Paragraph
        {
            public Title(string name, Font font) : base(name, font)
            {
                Alignment = Element.ALIGN_CENTER;
                SpacingAfter = 20;
            }
        }
        static public void FullNotApp(DataGrid grid, string titlename)
        {
            PdfPTable table = new PdfPTable(grid.Columns.Count);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text file (*.pdf)|*.pdf;";
            saveFile.FileName = titlename;
            if (saveFile.ShowDialog() != true)
            {
                return;
            }
            Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate());
            table.WidthPercentage = 100f;
            table.PaddingTop = 20;
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(saveFile.FileName, System.IO.FileMode.Create));
            doc.Open();
            var title = new Title(titlename, titlefon);
            doc.Add(title);
            for (int j = 0; j < grid.Columns.Count; j++)
            {
                PdfPCell header = new PdfPCell(new Phrase(grid.Columns[j].Header.ToString(), normalfon));
                header.VerticalAlignment = 1;
                header.HorizontalAlignment = 1;
                header.Padding = 5;
                table.AddCell(header);
            }
            table.HeaderRows = 1;
            var itemsSource = grid.Items;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                        for (int i = 0; i < grid.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                var x = new PdfPCell(new Phrase(txt.Text.ToString(), normalfon));
                                x.HorizontalAlignment = 1;
                                x.VerticalAlignment = 1;
                                table.AddCell(x);
                            }
                        }
                    }
                }

                doc.Add(table);
                doc.Close();
            }
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }
            return null;
        }
    }
}
