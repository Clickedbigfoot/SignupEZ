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
		readonly int SLEEP_TIME = 10 * 1000; //n * 1000 == n seconds of time to wait inbetween tasks

		public  Signup(string inputNetID, string inputPassword, string[] inputTargets, string[] inputDrops) {
			netID = inputNetID;
			password = inputPassword;
			targets = inputTargets;
			drops = inputDrops;

			FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("src", "geckodriver");
			driver = new FirefoxDriver(service);
			System.Threading.Thread.Sleep(SLEEP_TIME);
			System.Console.WriteLine("Done!");
			driver.Close();
		}

		/*
		Attempts to log into the course registration site
		@return 1 if successful
		*/
		public int getLoggedIn() {



			return 1;
		}
	}





}