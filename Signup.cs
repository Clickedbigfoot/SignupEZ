/*
Signup script
Checks whether or not there is a spot open and signs up the user automatically if there is.
Drops any course necessary as specified by the user.
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SignupEZ {

	class Signup {

		string netID;
		string password;
		string[] targets;
		string[] drops;
		FirefoxDriver driver;
		FirefoxDriverService service;
		readonly int SLEEP_TIME = 10 * 1000; //n * 1000 == n seconds of time to wait inbetween tasks
		readonly int SMALL_SLEEP_TIME = 5 * 1000;

		public  Signup(string inputNetID, string inputPassword, string[] inputTargets, string[] inputDrops) {
			netID = inputNetID;
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
			driver.Navigate().GoToUrl("https://courses.illinois.edu/");

			System.Console.WriteLine("Setup complete");
			System.Threading.Thread.Sleep(SLEEP_TIME);
		}

		/*
		Closes the web browser and sets it up again
		*/
		public void refresh() {
			this.finish();
			System.Threading.Thread.Sleep(SMALL_SLEEP_TIME);
			this.getLoggedIn();
		}

		/*
		Closes the web browser
		*/
		public void finish() {
			System.Console.WriteLine("Exiting");
			driver.Close();
		}
	}





}