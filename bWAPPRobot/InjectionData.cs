using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bWAPPRobot
{
    class InjectionData
    {
        public static String InjectionName;
        public static String InjectionNamePosition;
        public static String InjectionDescreption;
        public static String InjectionJson;

        public static IWebDriver driver;
        private StringBuilder verificationErrors;
        public static string baseURL;
        public static string LoginUserName;
        public static string LoginPassword;
        public static int SleepTimemilliseconds = 3000;


        public static void Login()
        {
            InjectionData.driver.Navigate().GoToUrl(InjectionData.baseURL + "/login.php");
            InjectionData.driver.Manage().Window.Maximize();
            Thread.Sleep(1000);
            InjectionData.driver.FindElement(By.Id("login")).Clear();
            InjectionData.driver.FindElement(By.Id("login")).SendKeys(InjectionData.LoginUserName);
            InjectionData.driver.FindElement(By.Id("password")).Clear();
            InjectionData.driver.FindElement(By.Id("password")).SendKeys(InjectionData.LoginPassword);
            Thread.Sleep(1000);
            InjectionData.driver.FindElement(By.XPath(".//*[@id='main']/form/button")).Click();


            Thread.Sleep(2000);
            SetInjectioncombobox(InjectionData.InjectionNamePosition);


        }

        public static void SetInjectioncombobox(String Position)
        {
            SelectElement se = new SelectElement(InjectionData.driver.FindElement(By.XPath(".//*[@id='bug']/form/select")));
            se.SelectByValue(Position);
            Thread.Sleep(2000);
            InjectionData.driver.FindElement(By.XPath(".//*[@id='bug']/form/button")).Click();
        }

        public static IWebElement GetWebElement(String locatortype, String locatorvalue)
        {
            Thread.Sleep(500);
            switch (locatortype)
            {
                case "id":
                    return InjectionData.driver.FindElement(By.Id(locatorvalue));

                case "xpath":
                    return InjectionData.driver.FindElement(By.XPath(locatorvalue));

                case "name":
                    return InjectionData.driver.FindElement(By.Name(locatorvalue));

                case "css":
                    return InjectionData.driver.FindElement(By.CssSelector(locatorvalue));

                case "LinkText":
                    return InjectionData.driver.FindElement(By.LinkText(locatorvalue));

                case "link text":
                    return InjectionData.driver.FindElement(By.LinkText(locatorvalue));

                case "classname":
                    return InjectionData.driver.FindElement(By.ClassName(locatorvalue));

                case "tagname":
                    return InjectionData.driver.FindElement(By.TagName(locatorvalue));

                default:
                    return null;


            }
        }

        public static void ExcuteInjection() {
            Login();



            String JsonString = File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\Json\\" + InjectionJson);
            JsonSeleniumHelper selenium = JsonConvert.DeserializeObject<JsonSeleniumHelper>(JsonString);


            IWebElement webelement;


            #region    foreach Json Steps
           
            foreach (object element in selenium.steps)
            {
                JsonStepsHelper step = JsonConvert.DeserializeObject<JsonStepsHelper>(element.ToString());
                //if (step.Type != "get" & step.Type != "verifyTextPresent")
                #region switch
                {
                    Thread.Sleep(SleepTimemilliseconds);
                    switch (step.Type.ToString())
                    {
                        case "get":
                            Thread.Sleep(1500);

                            if (InjectionData.driver.Url.ToString().Contains("robots.txt"))
                            {
                                string temp = InjectionData.driver.Url.ToString();
                                string anotherString =temp.Replace("robots.txt", "index.php");
                                InjectionData.driver.Navigate().GoToUrl(anotherString);
                                //MessageBox.Show(anotherString);
                            }

                            else if (InjectionData.driver.Url.ToString().Contains("message=test"))
                            {
                                string temp = InjectionData.driver.Url.ToString();
                                string anotherString = temp.Replace("test", "phpinfo()");
                                InjectionData.driver.Navigate().GoToUrl(anotherString);
                                //MessageBox.Show(anotherString);
                            }
                            else
                            {
                                InjectionData.driver.Navigate().GoToUrl(InjectionData.driver.Url + step.url.ToString());
                            }
                            Thread.Sleep(1000);
                            break;

                        case "setElementText":
                            webelement = GetWebElement(step.locator.Type.ToString(), step.locator.value.ToString());
                            webelement.Clear();
                            webelement.SendKeys(step.text.ToString());
                            Thread.Sleep(SleepTimemilliseconds);
                            break;
                        case "clickElement":
                            webelement = GetWebElement(step.locator.Type.ToString(), step.locator.value.ToString());
                            webelement.Click();
                            Thread.Sleep(SleepTimemilliseconds);
                            break;

                        case "confirm":
                            Thread.Sleep(1000);
                            InjectionData.driver.SwitchTo().Alert().Accept();
                            Thread.Sleep(SleepTimemilliseconds);
                            break;

                        case "goBack":
                            Thread.Sleep(1000);
                            InjectionData.driver.Navigate().Back();

                            Thread.Sleep(SleepTimemilliseconds);
                            break;

                        case "verifyTextPresent":
                            Thread.Sleep(SleepTimemilliseconds);
                           
                            break;

                        case "storeTitle":
                            Thread.Sleep(SleepTimemilliseconds);
                            
                            break;

                        case "setElementSelected":
                            step.locator.value.ToString();
                            //String b = step.locator.value.ToString().Substring(step.locator.value.ToString().IndexOf("["), step.locator.value.ToString().IndexOf("]"));
                            //MessageBox.Show(b);                            
                            Thread.Sleep(SleepTimemilliseconds);
                            
                            break;

                        case "assertTextPresent":
                            Thread.Sleep(SleepTimemilliseconds);
                           
                            break;

                        case "switchToFrame":
                            //driver.SwitchTo().Window("windowName");
                           

                            break;

                        case "dragAndDrop":
                           
                            break;


                    }

                }
                #endregion switch
            }
          

            #endregion foreach Json Steps


           
        }
    }
}
