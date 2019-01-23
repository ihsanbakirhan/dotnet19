using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Students
{
    public partial class Browser : Form
    {
        public Browser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }
        string anasayfa = "https://www.google.com.tr";
        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(anasayfa);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            textBox3.Text = webBrowser1.Url.ToString();
            this.Text = webBrowser1.DocumentTitle;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Enter)
            {
                try
                {
                    if(!(textBox3.Text.StartsWith("http://")|| (textBox3.Text.StartsWith("https://"))))
                    {
                        textBox3.Text = "http://" + textBox3.Text;
                    }
                    webBrowser1.Navigate(textBox3.Text);
                }
                catch 
                {

                    
                }
            }
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if ( e.MaximumProgress>0 && e.CurrentProgress>=0 && e.CurrentProgress<=e.MaximumProgress
                ) 

            {
                
                

                progressBar1.Value = Convert.ToInt32((e.CurrentProgress / e.MaximumProgress)*100);


            }
            
        }
    }
}
