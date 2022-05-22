using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Atlanta.Positions
{
    public class Worker
    {
        IWebDriver Driver;

        public void Do()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("test-type");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("no-sandbox");
            //options.AddArgument("disable-infobars");
            //options.AddArgument("--headless"); //hide browser
            //options.AddArgument("--start-maximized");
            options.AddArgument("--window-size=1100,500");
            // options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

            // options.AddArgument(@"user-data-dir=C:\Users\pwujczyk\AppData\Local\Google\Chrome\User Data");
            this.Driver = new ChromeDriver(options);

            Driver.Url = "https://grow.googleplex.com/jobs/search?query=engineering%20manager%20jt:%22full_time_job%22%20loc:%22United%20States%22%20jl:%226%22%20pm:%22true%22";
            Console.ReadLine();
            Console.Write("Perfect");

            List<string> result = new List<string>();
            Dictionary<string, int> resultCity = new Dictionary<string, int>();
            resultCity.Add("Kirkland", 0);
            resultCity.Add("Sunnyvale", 0);
            resultCity.Add("Irvine", 0);
            resultCity.Add("San Francisco", 0);
            resultCity.Add("Seattle", 0);
            resultCity.Add("Austin", 0);
            resultCity.Add("Chicago", 0);
            resultCity.Add("New york", 0);
            while (true)
            {

                var inputs = Driver.FindElements(By.TagName("grow-job-search-result"));
                foreach (var input in inputs)
                {
                    var box = input.Text;
                    string[] ar = box.Split('\n');

                    string line = ar[ar.Length - 3];
                    Console.Write(line);
                    bool added = false;
                    foreach (var item in resultCity)
                    {
                        if(line.Contains(item.Key))
                        {
                            resultCity[item.Key] = resultCity[item.Key] + 1;
                            added = true;
                        }
                    }
                    if (added == false)
                    {
                        result.Add(line);
                    }
                }
                var next = Driver.FindElement(By.ClassName("next-page"));
                Actions actions = new Actions(this.Driver);
                actions.MoveToElement(next).Click().Build().Perform();
            }

        }
    }
}
