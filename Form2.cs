using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

using System.Net;
using System.IO;

namespace SSGStockMonitor
{
    public partial class Form2 : Form
    {
        public decimal SIChange;

        public Form2(string StoclSymbol)
        {
            InitializeComponent();
            txtSymbol.Text = StoclSymbol;
            pictureBox1.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol.Text);
            comboBox1.Items.Add("1 Day Chart");
            comboBox1.Items.Add("5 Day Chart");
            comboBox1.Items.Add("3 Month Chart");
            comboBox1.Items.Add("6 Month Chart");
            comboBox1.Items.Add("1 Year Chart");
            comboBox1.Items.Add("2 Year Chart");
            comboBox1.Items.Add("5 Year Chart");
            comboBox1.Items.Add("Max Chart");
            label1.Text = txtSymbol.Text;
            label2.Text = DateTime.Today.ToString("MM/dd/yy");
            label3.Text = DateTime.Now.ToString("HH:mm:ss");
            label4.Text = "Price: ";
            label5.Text = "Volume: ";
            label6.Text = "Open: ";
            label7.Text = txtSymbol.Text;
            label8.Text = DateTime.Today.ToString("MM/dd/yy");
            label9.Text = DateTime.Now.ToString("HH:mm:ss");
            label10.Text = "AHT: ";
            label11.Text = "Volume: ";
            label12.Text = "Open: ";
            label13.Text = "P/E Ratio:";
            label14.Text = "Dividens:";
            GetData();

        }

         

        private string GetWebResponse(string url)
        {
            // Make a WebClient.
            WebClient web_client = new WebClient();

            // Get the indicated URL.
            Stream response = web_client.OpenRead(url);
            //Stream ChartResponse = web_client.OpenRead(Gra);
            // Read the result.
            using (StreamReader stream_reader = new StreamReader(response))
            {
                // Get the results.
                string result = stream_reader.ReadToEnd();

                // Close the stream reader and its underlying stream.
                stream_reader.Close();

                // Return the result.
                return result;
            }
        }

        private void btnGetInfo_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();


            Random num = new Random(1000);
            // Build the URL.
            string url = "";  char[] seperatedata = { ',',','};//new char[] { '\r', '\n' }
            if (txtSymbol.Text != "") url += txtSymbol.Text + "+";
            {
                if (url != "")
                {
                   

                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd"; 
                    url = base_url.Replace("@", url);


                    try
                    {
                        string result = GetWebResponse(url);
                        Console.WriteLine(result.Replace("\\r\\", "\r\n"));
                        
                        // Pull out the current prices.
                        string[] lines = result.Split(seperatedata , StringSplitOptions.RemoveEmptyEntries);

                        foreach (string d in lines)
                        {
                            Console.WriteLine(d);
                        }

                        label1.Text = txtSymbol.Text;
                        label2.Text = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                        label3.Text = "Time:" + DateTime.Now.ToString();
                        label4.Text = "Price:" + lines[1].ToString();
                        label5.Text = "Change:" + lines[4].ToString();
                        label6.Text = "Day's High:" + lines[5].ToString();
                        label7.Text = "Day's Low:" + lines[6].ToString();
                        label8.Text = "Volume:" + lines[7].ToString();
                        label9.Text = "Bid:" + lines[8].ToString();
                        label10.Text = "Ask:" + lines[9].ToString();
                        label11.Text = "Change%:" + lines[10].Trim(new char[] {'"'});
                        label12.Text = "Open:" + lines[11].ToString();
                        label13.Text = "P/E Ratio:" + lines[13].ToString();
                        label14.Text = "Dividens:" + lines[14].ToString();

                        SIChange = decimal.Parse(lines[4]);

                        if (SIChange > 0)
                        {
                            label5.ForeColor = System.Drawing.Color.Green;
                            label11.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (SIChange < 0)
                        {
                            label5.ForeColor = System.Drawing.Color.Red;
                            label11.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            label5.ForeColor = System.Drawing.Color.Black;
                            label11.ForeColor = System.Drawing.Color.Black;
                        }




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Read Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    this.Cursor = Cursors.Default;
                }
            }
        }

        void GetData()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();


            Random num = new Random(1000);
            // Build the URL.
            string url = ""; char[] seperatedata = { ',', ',' };//new char[] { '\r', '\n' }
            if (txtSymbol.Text != "") url += txtSymbol.Text + "+";
            {
                if (url != "")
                {


                    // Remove the trailing plus sign.
                    url = url.Substring(0, url.Length - 1);

                    // Prepend the base URL.
                    const string base_url =
                        "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                    url = base_url.Replace("@", url);


                    try
                    {
                        string result = GetWebResponse(url);
                        Console.WriteLine(result.Replace("\\r\\", "\r\n"));

                        // Pull out the current prices.
                        string[] lines = result.Split(seperatedata, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string d in lines)
                        {
                            Console.WriteLine(d);
                        }

                        label1.Text = txtSymbol.Text;
                        label2.Text = "Today's Date:" + DateTime.Today.ToString("MM/dd/yy");
                        label3.Text = "Time:" + DateTime.Now.ToString();
                        label4.Text = "Price:" + lines[1].ToString();
                        label5.Text = "Change:" + lines[4].ToString();
                        label6.Text = "Day's High:" + lines[5].ToString();
                        label7.Text = "Day's Low:" + lines[6].ToString();
                        label8.Text = "Volume:" + lines[7].ToString();
                        label9.Text = "Bid:" + lines[8].ToString();
                        label10.Text = "Ask:" + lines[9].ToString();
                        label11.Text = "Change%:" + lines[10].Trim(new char[] { '"' });
                        label12.Text = "Open:" + lines[11].ToString();
                        label13.Text = "P/E Ratio:" + lines[13].ToString();
                        label14.Text = "Dividens:" + lines[14].ToString();

                        SIChange = decimal.Parse(lines[4]);

                        if (SIChange > 0)
                        {
                            label5.ForeColor = System.Drawing.Color.Green;
                            label11.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (SIChange < 0)
                        {
                            label5.ForeColor = System.Drawing.Color.Red;
                            label11.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            label5.ForeColor = System.Drawing.Color.Black;
                            label11.ForeColor = System.Drawing.Color.Black;
                        }





                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Read Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnChangeCharts_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol.Text);

            }
            if (comboBox1.SelectedIndex == 1)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol.Text);

            }
            if (comboBox1.SelectedIndex == 2)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol.Text);

            }
            if (comboBox1.SelectedIndex == 3)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol.Text);

            }
            if (comboBox1.SelectedIndex == 4)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol.Text);
            }
                if (comboBox1.SelectedIndex == 5)
                {

                    pictureBox1.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol.Text);

                }
                if (comboBox1.SelectedIndex == 6)
                {

                    pictureBox1.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol.Text);

                }
                if (comboBox1.SelectedIndex == 7)
                {

                    pictureBox1.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol.Text);

                }




                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

       

       
    }

