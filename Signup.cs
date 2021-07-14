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
		string[] targets; //targets[0] is the name of the class, targets[i > 0] are the CRNs to check
		string[] drops;
		FirefoxDriver driver;
		FirefoxDriverService service;
		IWebElement element1;
		//SelectElement dropdown; //For dropdown menus in picking the results per page
		readonly int LARGE_SLEEP_TIME = 10 * 1000; //n * 1000 == n seconds of time to wait inbetween tasks
		readonly int SLEEP_TIME = 5 * 1000;
		readonly int SMALL_SLEEP_TIME = 2 * 1000;
		int i = 0; //Generic for loop

		/*
		Constructor that initilaizes final variables
		@param inputNetId: the netId
		@param inputPassword: the user's password
		@param inputTargets: array of names and CRNs for the target classes
		@param inputDrops: array of CRNs for the classes to drop in order to make room in the schedule
		*/
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
			try {
				service = FirefoxDriverService.CreateDefaultService("src", "geckodriver");
			}
			catch (System.Exception e) {
				service = FirefoxDriverService.CreateDefaultService("src", "geckodriver.exe");
			}
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

			driver.Navigate().GoToUrl("https://banner.apps.uillinois.edu/StudentRegistrationSSB/?mepCode=1UIUC");
			System.Threading.Thread.Sleep(SLEEP_TIME); //At screen to click "Register for Classes"
			driver.FindElementById("registerLink").Click();
			System.Threading.Thread.Sleep(SLEEP_TIME);
			driver.FindElementByClassName("select2-choice").Click();
			System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
			driver.FindElementById("120208").Click();
			System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
			driver.FindElementById("term-go").Click();
			System.Threading.Thread.Sleep(LARGE_SLEEP_TIME); //At screen for picking classes now
			driver.FindElementById("txt_courseTitle").SendKeys(targets[0]);
			System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
			driver.FindElementById("search-go").Click(); //At screen with list of desired classes now
			System.Threading.Thread.Sleep(LARGE_SLEEP_TIME);
			System.Console.WriteLine("Setup complete");
		}

		/*
		Signs up for the classes and drops classes as necessary
		@return true if classes were signed up for, false otherwise
		*/
		public bool performSignup() {
			System.Console.WriteLine("Nothing interesting happens");
			driver.FindElementById("search-again-button").Click(); //At search screen for classes
			System.Threading.Thread.Sleep(SLEEP_TIME);
			for (i = 0; i < 30; i++) {
				//Backspace 30 times
				driver.FindElementById("txt_courseTitle").SendKeys(Keys.Backspace);
			}
			driver.FindElementById("txt_courseTitle").SendKeys(targets[0]);
			System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
			driver.FindElementById("search-go").Click(); //At screen with list of desired classes now
			System.Threading.Thread.Sleep(LARGE_SLEEP_TIME);
			driver.FindElementByClassName("page-size-select").SendKeys("50" + Keys.Enter); //Set 50 results per page
			System.Threading.Thread.Sleep(LARGE_SLEEP_TIME);
			if (isAvailable()) {
				//The classes have spots open!
				System.Console.WriteLine("All spots open, attempting signup");
				for (i = 1; i < targets.Length; i++) {
					//Iterate through indeces of targets CRNs
					driver.FindElementById("addSection120208" + targets[i]).Click();
					System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
				}
				for (i = 0; i < drops.Length; i++) {
					//Iterate through indeces of CRNs to drop
					driver.FindElementById("s2id_action-" + drops[i] + "-ddl").Click();
					System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
					driver.FindElementById("DW").Click();
					System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
				}
				driver.FindElementById("saveButton").Click();
				System.Threading.Thread.Sleep(SLEEP_TIME);
				return true;
			}
			return false;
		}

		/*
		Checks to see if there is a spot in the classes
		@return true if there is a spot in each target class, false otherwise
		*/
		public bool isAvailable() {
			for (i = 1; i < targets.Length; i++) {
				//Iterate through the indeces of targets corresponding to CRNs
				if (!driver.FindElementById("addSection120208" + targets[i]).Enabled) {
					//The class is full 37431
					return false;
				}
			}
			return true;
		}

		/*
		Closes the web browser and sets it up again
		*/
		public void refresh() {
			finish();
			System.Threading.Thread.Sleep(SLEEP_TIME);
			getLoggedIn();
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