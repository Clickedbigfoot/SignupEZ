/*
Signup script
Checks whether or not there is a spot open and signs up the user automatically if there is.
Drops any course necessary as specified by the user.
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SignupEZ {

	class Signup {

		string netId;
		string password;
		string[] targets;
		string[] drops;
		FirefoxDriver driver;
		FirefoxDriverService service;
		IWebElement element1;
		readonly int LARGE_SLEEP_TIME = 10 * 1000; //n * 1000 == n seconds of time to wait inbetween tasks
		readonly int SLEEP_TIME = 5 * 1000;
		readonly int SMALL_SLEEP_TIME = 2 * 1000;

		public  Signup(string inputNetId, string inputPassword, string[] inputTargets, string[] inputDrops) {
			netId = inputNetId;
			password = inputPassword;
			targets = inputTargets;
			drops = inputDrops;
		}

		/*
		Logs into the course registration site
		*/
		public void getLoggedIn() {
			service = FirefoxDriverService.CreateDefaultService("src", "geckodriver");
			driver = new FirefoxDriver(service);
			driver.Navigate().GoToUrl("https://webprod.admin.uillinois.edu/ssa/servlet/SelfServiceLogin?appName=edu.uillinois.aits.SelfServiceLogin&dad=BANPROD1&target=G");
			System.Threading.Thread.Sleep(SLEEP_TIME); //Now at login screen
			while (isPresent("netid")) {
				driver.FindElementById("netid").SendKeys(netId);
				System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
				driver.FindElementById("easpass").SendKeys(password);
				System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
				driver.FindElementByName("BTN_LOGIN").Click();
				System.Threading.Thread.Sleep(LARGE_SLEEP_TIME); //Should be at screen for selecting course registration
			}

			System.Console.WriteLine("Setup complete");
			System.Threading.Thread.Sleep(SLEEP_TIME);
		}

		/*
		Closes the web browser and sets it up again
		*/
		public void refresh() {
			this.finish();
			System.Threading.Thread.Sleep(SLEEP_TIME);
			this.getLoggedIn();
		}

		/*
		Closes the web browser
		*/
		public void finish() {
			System.Console.WriteLine("Exiting");
			driver.Close();
		}

		/*
		Determines if an element is present by its ID
		@param elementId: the id of the element being looked for
		@return true if the element is present, otherwise false
		*/
		private bool isPresent(string elementId) {
			try {
				element1 = driver.FindElementById(elementId);
				return true;
			}
			catch (System.Exception e) {
				return false;
			}
		}
	}
}