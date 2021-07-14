# SchoolSignup

Description: This program will constantly check to see if there are spots open in a class you are trying to get into. Upon seeing that there are available seats, it will sign you up for the class while dropping any classes necessary to make room in the schedule. In past experience, this generally signs up for the course before Coursicle even detects a spot is open, but I would obviously still recommend using Coursicle as a backup method. Best left running on a computer not being used for extended periods. 

Setup:
1) Requires firefox installed. 
2) Requires the download of geckodriver, which must be saved in the src folder. Geckodriver can be downloaded from https://github.com/mozilla/geckodriver/releases
3) In the same folder as the SignupEZ executable, there must be an src folder and three text files: loginCredentials.txt, targetCRN.txt, and dropCRN.txt.

Instructions:
1) In the targetCRN.txt file, write the exact name of the course you wish to sign up for on line one.
2) In the targetCRN.txt file, write on the lines 2+ the CRN of each section you wish to sign up for with each CRN in its own line.
	-Do not choose multiple lectures or labs, etc. because the program will try to sign up for every class section together. 
3) In the dropCRN.txt file, write the CRN of each class section that must be dropped in order to make space in the schedule, one CRN per line.
	-Start with line 1. There is no need to include any class names, only the CRN.
	-If your schedule can already accomodate the classes you wish to sign up for, feel free to leave drops.txt empty.
	-Please only use this if necessary. Every class that needs to be dropped adds a few seconds to the total time it takes to sign up for a class when it opens up.
4) In loginCredentials.txt, enter your netID on line 1 and your password on line 2.
5) Open command prompt (Or windows powershell) and make your way to the folder that holds this executable and run "./SignupEZ.exe"

It should print out various updates to the command prompt, such as how many checks it has performed and at what time. If it succeeds, the program will stop and the last thing it will print is "Success!". If the program stops and "Success!" is not printed, then something went wrong. If the program is finished running and you see "All spots open, attempting signup", but no "Success!" printed, please double check your schedule manually. 