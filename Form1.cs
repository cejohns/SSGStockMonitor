using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;

namespace SSGStockMonitor
{
    public partial class Form1 : Form
    {
        public decimal SIChangeOne;
        public decimal SIChangeTwo;
        public decimal SIChangeThree;
        public decimal SIChangeFour;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("1 Day Chart");
            comboBox1.Items.Add("5 Day Chart");
            comboBox1.Items.Add("3 Month Chart");
            comboBox1.Items.Add("6 Month Chart");
            comboBox1.Items.Add("1 Year Chart");
            comboBox1.Items.Add("2 Year Chart");
            comboBox1.Items.Add("5 Year Chart");
            comboBox1.Items.Add("Max Chart");
            GetData();
            
        }

        // Get the stock prices.
        private void btnGetPrices_Click(object sender, EventArgs e)
        {
            GetData();
        }

        // Get a web response.
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

        void GetData()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();


            Random num = new Random(1000);
            // Build the URL.
            string url = "";
            if (txtSymbol1.Text != "") url += txtSymbol1.Text + "+";
            if (txtSymbol2.Text != "") url += txtSymbol2.Text + "+";
            if (txtSymbol3.Text != "") url += txtSymbol3.Text + "+";
            if (txtSymbol4.Text != "") url += txtSymbol4.Text + "+";
            if (url != "")
            {
                // Remove the trailing plus sign.
                url = url.Substring(0, url.Length - 1);

                // Prepend the base URL.
                const string base_url =
                    "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1hgvbap2oc3rd";
                url = base_url.Replace("@", url);



                // Get the response.
                try
                {
                    // Get the web response.
                    string result = GetWebResponse(url);
                    Console.WriteLine(result.Replace("\\r\\n", "\r\n"));

                    // Pull out the current prices.
                    string[] lines = result.Split(
                        new char[] { '\r', '\n' },
                        StringSplitOptions.RemoveEmptyEntries);
                    label1.Text = decimal.Parse(lines[0].Split(',')[1]).ToString("C3");
                    label4.Text = decimal.Parse(lines[1].Split(',')[1]).ToString("C3");
                    label7.Text = decimal.Parse(lines[2].Split(',')[1]).ToString("C3");
                    label10.Text = decimal.Parse(lines[3].Split(',')[1]).ToString("C3");


                    SIChangeOne = decimal.Parse(lines[0].Split(',')[4]);
                    SIChangeTwo = decimal.Parse(lines[1].Split(',')[4]);
                    SIChangeThree = decimal.Parse(lines[2].Split(',')[4]);
                    SIChangeFour = decimal.Parse(lines[3].Split(',')[4]);



                    label2.Text = SIChangeOne.ToString();
                    label5.Text = SIChangeTwo.ToString();
                    label8.Text = SIChangeThree.ToString();
                    label11.Text = SIChangeFour.ToString();

                    label3.Text = lines[0].Split(',')[5].Trim(new char[] { '"' });
                    label6.Text = lines[1].Split(',')[5].Trim(new char[] { '"' });
                    label9.Text = lines[2].Split(',')[5].Trim(new char[] { '"' });
                    label12.Text = lines[3].Split(',')[5].Trim(new char[] { '"' });


                    if (SIChangeOne > 0)
                    {
                        label2.ForeColor = System.Drawing.Color.Green;
                        label3.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (SIChangeOne < 0)
                    {
                        label2.ForeColor = System.Drawing.Color.Red;
                        label3.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label3.ForeColor = System.Drawing.Color.Black;
                    }

                    if (SIChangeTwo > 0)
                    {
                        label5.ForeColor = System.Drawing.Color.Green;
                        label6.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (SIChangeTwo < 0)
                    {
                        label5.ForeColor = System.Drawing.Color.Red;
                        label6.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        label5.ForeColor = System.Drawing.Color.Black;
                        label6.ForeColor = System.Drawing.Color.Black;
                    }

                    if (SIChangeThree > 0)
                    {
                        label8.ForeColor = System.Drawing.Color.Green;
                        label9.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (SIChangeThree < 0)
                    {
                        label8.ForeColor = System.Drawing.Color.Red;
                        label9.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        label8.ForeColor = System.Drawing.Color.Black;
                        label9.ForeColor = System.Drawing.Color.Black;
                    }

                    if (SIChangeFour > 0)
                    {
                        label11.ForeColor = System.Drawing.Color.Green;
                        label12.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (SIChangeFour < 0)
                    {
                        label11.ForeColor = System.Drawing.Color.Red;
                        label12.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        label11.ForeColor = System.Drawing.Color.Black;
                        label12.ForeColor = System.Drawing.Color.Black;
                    }


                    pictureBox1.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol1.Text);
                    pictureBox2.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol2.Text);
                    pictureBox3.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol3.Text);
                    pictureBox4.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol4.Text);

                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Read Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (comboBox1.SelectedIndex == 0)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 1)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 2)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 3)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 4)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 5)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 6)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 7)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol4.Text);
            }




            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

        }

       

        private void btnGetPrices_Click_1(object sender, EventArgs e)
        {
            GetData();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/b?s=" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 1)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/w?s=" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 2)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/3m/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 3)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/6m/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 4)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/1y/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 5)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/2y/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 6)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/5y/" + txtSymbol4.Text);
            }
            if (comboBox1.SelectedIndex == 7)
            {

                pictureBox1.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol1.Text);
                pictureBox2.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol2.Text);
                pictureBox3.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol3.Text);
                pictureBox4.Load("http://chart.finance.yahoo.com/c/my/" + txtSymbol4.Text);
            }




            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(txtSymbol1.Text);
            form2.Show();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(txtSymbol2.Text);
            form2.Show();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(txtSymbol3.Text);
            form2.Show();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(txtSymbol4.Text);
            form2.Show();
        }
    }
}
