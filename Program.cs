/*
Signup Script. Reads in the user's net ID, password, and the CRNs of the target classes and the classes to drop if a spot opens.
@TODO
1) DONE Make this read in data
2) DONE Open a web browser
3) Perform goal
4) Set expiry code
5) Compile for windows and export
*/

namespace SignupEZ
{
    class Program
    {
    	readonly static string CREDENTIALS_LOCATION = "loginCredentials.txt";
    	readonly static string TARGET_CLASSES_LOCATION = "targetCRN.txt";
    	readonly static string DROP_CLASSES_LOCATION = "dropCRN.txt";
    	readonly static string errorCredentials = @"Error: Ensure your netID is in line one of loginCredentials.txt and your password is in line two";
    	readonly static string errorTargetCRN = @"Error: Ensure there is the name of the class and at least one CRN in targetCRN.txt";


        static void Main(string[] args)
        {
            string[] credentials = System.IO.File.ReadAllLines(CREDENTIALS_LOCATION); //[0] is the netID while [1] is the password
            string[] targets = System.IO.File.ReadAllLines(TARGET_CLASSES_LOCATION); //Array of each target class' name and CRN
            string[] drops = System.IO.File.ReadAllLines(DROP_CLASSES_LOCATION); //Array of each class' CRN to drop

            //Check for issues
            if (credentials.Length < 2) {
            	System.Console.WriteLine(errorCredentials);
            	return;
            }
            else if (targets.Length < 2) {
            	System.Console.WriteLine(errorTargetCRN);
            	return;
            }

            Signup signup = new Signup(credentials[0], credentials[1], targets, drops);
            signup.getLoggedIn();
            bool isSuccessful = false;
            int iterations = 1;
            while (!isSuccessful) {
            	if (iterations % 21 == 0) {
            		//This is the ~21st iteration
            		signup.refresh();
            	}

            	System.Console.WriteLine("This is iteration " + iterations);
            	System.Console.WriteLine(System.DateTime.Now);
            	isSuccessful = signup.performSignup(); //Checking and signing up if able

            	if (isSuccessful) {
            		//Success!
            		signup.finish();
                    System.Console.WriteLine("Success!");
            		break;
            	}
            	iterations++;
            }
            
        }
    }
}
