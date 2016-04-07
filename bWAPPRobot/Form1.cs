using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace bWAPPRobot
{
    public partial class Form1 : Form
    {

        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath +"\\bWAPP.xml");
            XmlNodeList colorList = doc.SelectNodes("select/option");
            foreach (XmlNode Name in colorList)
            {
                comboBox1.Items.Add(Name.InnerText);
            }

            comboBox1.SelectedIndex = 2;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            FillInjectionData();



        }
        private void FillInjectionData()
        {
            InjectionData.InjectionName = comboBox1.SelectedItem.ToString();
            InjectionData.InjectionNamePosition = comboBox1.SelectedIndex.ToString();
            // See if this file exists in the same directory.
            //MessageBox.Show(InjectionData.InjectionNamePosition);

            InjectionData.InjectionDescreption = InjectionData.InjectionName + ".PNG";

            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\Picture\\" + InjectionData.InjectionDescreption))
            {
                //MessageBox.Show(System.IO.Directory.GetCurrentDirectory() + "\\Picture\\" + InjectionData.InjectionDescreption);
                pictureBox1.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\Picture\\" + InjectionData.InjectionDescreption);
            }
            else
            {
                InjectionData.InjectionDescreption = null;
            }

            InjectionData.InjectionJson = InjectionData.InjectionName + ".json";
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\Json\\" + InjectionData.InjectionJson))
            {

                //MessageBox.Show(System.IO.Directory.GetCurrentDirectory() + "\\Json\\" + InjectionData.InjectionJson);
            }
            else
            {
                InjectionData.InjectionJson = null;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
                if ((InjectionData.InjectionJson != null)) { 
                InjectionData.driver = new ChromeDriver();
                InjectionData.baseURL = txtbaseurl.Text;
                InjectionData.LoginUserName = txtusername.Text;
                InjectionData.LoginPassword = txtpassword.Text;

                InjectionData.ExcuteInjection();
            }else
            {
                MessageBox.Show("No Json file exist for this Injection");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillInjectionData();
        }
    }
}
