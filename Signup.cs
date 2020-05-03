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

		public  Signup(string inputNetID, string inputPassword, string[] inputTargets, string[] inputDrops) {
			netID = inputNetID;
			password = inputPassword;
			targets = inputTargets;
			drops = inputDrops;

			FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("src", "geckodriver");

			//IWebDriver driver = new FirefoxDriver(null);
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