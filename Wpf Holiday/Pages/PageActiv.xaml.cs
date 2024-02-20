using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_Holiday.Classes;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Wpf_Holiday.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageActiv.xaml
    /// </summary>
    public partial class PageActiv : Page
    {
        public PageActiv()
        {
            InitializeComponent();
            dtgActiv.ItemsSource = HolidayEntities.GetContext().Activity.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddActiv(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddActiv((Activity)dtgActiv.SelectedItem));
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgActiv.ItemsSource = HolidayEntities.GetContext().Activity.ToList();
        }

        private void MenuDel_Click(object sender, RoutedEventArgs e)
        {
            var salesForRemoving = dtgActiv.SelectedItems.Cast<Activity>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {salesForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HolidayEntities.GetContext().Activity.RemoveRange(salesForRemoving);
                    HolidayEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    dtgActiv.ItemsSource = HolidayEntities.GetContext().Activity.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }


        private void MenuGlav_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageMainMenu());
        }

        private void MenuWord_Click(object sender, RoutedEventArgs e)
        {
            Activity salesForRemoving = (Activity)dtgActiv.SelectedItem;
            Word.Selection wrdSelection;
            Word.MailMerge wrdMailMerge;
            Word.MailMergeFields wrdMergeFields;
            string StrToAdd;

            // Create an instance of Word  and make it visible.
            wrdApp = new Word.Application();
            wrdApp.Visible = true;

            // Add a new document.
            wrdDoc = wrdApp.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            wrdDoc.Select();




            wrdSelection = wrdApp.Selection;
            wrdMailMerge = wrdDoc.MailMerge;

            // Create a MailMerge Data file.

            //CreateMailMergeDataFile();

            // Create a string and insert it into the document.

            StrToAdd = $"Конкурс {salesForRemoving.Event.NameEvent} ";
            wrdSelection.ParagraphFormat.Alignment =
                Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wrdSelection.TypeText(StrToAdd);

            InsertLines(4);

            // Insert merge data.
            wrdSelection.ParagraphFormat.Alignment =
                Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wrdMergeFields = wrdMailMerge.Fields;
            wrdMergeFields.Add(wrdSelection.Range, "Фамилия");
            wrdSelection.TypeText(" ");
            wrdMergeFields.Add(wrdSelection.Range, "Имя");
            wrdSelection.TypeParagraph();

            wrdMergeFields.Add(wrdSelection.Range, $"{salesForRemoving.ActivName}");
            wrdSelection.TypeParagraph();

            InsertLines(2);

            // Right justify the line and insert a date field
            // with the current date.
            wrdSelection.ParagraphFormat.Alignment =
                Word.WdParagraphAlignment.wdAlignParagraphRight;

            Object objDate = "dddd, MMMM dd, yyyy";
            wrdSelection.InsertDateTime(ref objDate, ref oFalse, ref oMissing,
            ref oMissing, ref oMissing);
            InsertLines(2);
           


            // Create a string and insert it into the document.
            StrToAdd = $"Поздравляем {salesForRemoving.Person.Fio} с заслуженной победой в конкурсе! " +
                $"Желаем не останавливаться на достигнутом, всегда двигаться вперед, покорять любые вершины. " +
                $"Пусть на пути к успеху Вам всегда светит счастливая звезда!";
            wrdSelection.TypeText(StrToAdd);
            wrdSelection.TypeParagraph();
            wrdSelection.TypeText("Подпись: ");
        }
        Word.Application wrdApp;
        Word._Document wrdDoc;
        Object oMissing = System.Reflection.Missing.Value;
        Object oFalse = false;
        private void InsertLines(int LineNum)
        {
            int iCount;

            // Insert "LineNum" blank lines. 
            for (iCount = 1; iCount <= LineNum; iCount++)
            {
                wrdApp.Selection.TypeParagraph();
            }
        }
        private void FillRow(Word._Document oDoc, int Row, string Text1, string Text2, string Text3, string Text4)
        {
            // Insert the data into the specific cell.
            oDoc.Tables[1].Cell(Row, 1).Range.InsertAfter(Text1);
            oDoc.Tables[1].Cell(Row, 2).Range.InsertAfter(Text2);
            oDoc.Tables[1].Cell(Row, 3).Range.InsertAfter(Text3);
            oDoc.Tables[1].Cell(Row, 4).Range.InsertAfter(Text4);
        }

        private void MenuExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook wb = excelApp.Workbooks.Open($"{Directory.GetCurrentDirectory()}\\Shablon.xlsx");
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
            ws.Cells[1, 3] = "Конкурсы";
            int indexRows = 2;
            //ячейка
            ws.Cells[1][indexRows] = "Событие";
            ws.Cells[2][indexRows] = "Победитель";

            //список пользователей из таблицы после фильтрации и поиска
            var printItems = dtgActiv.Items;
            //цикл по данным из списка для печати
            foreach (Activity item in printItems)
            {
                ws.Cells[1][indexRows + 1] = item.Event.NameEvent;
                ws.Cells[2][indexRows + 1] = item.Person.Fio;
                indexRows++;
            }
            excelApp.Visible = true;
        }
    }
}
