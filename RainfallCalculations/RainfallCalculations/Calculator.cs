//Name: Babatope Ayeni
//Date: 12/14/2021
//Exam: C# Exam – Rainfall Calculator


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainfallCalculations
{
    public partial class Calculator : Form
    {

        private static string logFilePath = @"C:\temp\rainfall2.txt";

        List<double> Record = new List<double>();
        List<double> bRecord = new List<double>();

        public Calculator()
        {
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        private void frmFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            DialogResult result = frmFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = frmFileDialog.FileName;
                try
                {
                    double rainfalreading;

                    int totalRecordCount = File.ReadAllLines(file).Length;

                    int totalBadRecords = (from line in File.ReadAllLines(file)
                                           where Double.TryParse(line, out rainfalreading) == false
                                           select line).Count();

                    List<string> list = (from line in File.ReadAllLines(file)
                                         where Double.TryParse(line, out rainfalreading) == true
                                         select line).ToList();

                    foreach (var line in list)
                    {
                        // Do something with the line.
                        if (Double.TryParse(line, out rainfalreading))
                        {
                            if (rainfalreading>=0)
                            {
                                Record.Add(rainfalreading);
                            }
                            else
                            {
                                bRecord.Add(rainfalreading);
                            }
                        }
                    }

                    lblMaxRain.Text = Record.Max().ToString();

                    lblTotalRecords.Text = totalRecordCount.ToString();

                    lblbadrecord.Text = bRecord.Count.ToString();

                    lblMinRain.Text = Record.Min().ToString();

                    lblAverage.Text= Record.Average().ToString();

                    LogFileWrite(Record);
                }
                catch (System.IO.IOException ex)
                {
                    lblmessage.Text = "File Selected Has Error(" + ex.Message + ")";
                }
            }
        }

        public  void LogFileWrite(List<double> message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists

                DirectoryInfo logDirInfo = null;

                FileInfo logFileInfo = new FileInfo(logFilePath);

                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)//check if the file exist
                {
                    fileStream = logFileInfo.Create();

                    lblmessage.Text = "File Name:rainfall2.txt Does not Exist in (" + logFilePath + ") thus will be created";
                }
                else
                {
                    File.Delete(logFilePath);

                    fileStream = new FileStream(logFilePath, FileMode.Append);

                    lblmessage.Text = "File Name:rainfall2.txt Exist in the path(" + logFilePath + ")";
                }
                streamWriter = new StreamWriter(fileStream);
                //streamWriter.WriteLine(message);
                foreach (var s in message.OrderBy(p => p))
                    streamWriter.WriteLine(s);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }

       private void lbi_Click(object sender, EventArgs e)
        {

       }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
