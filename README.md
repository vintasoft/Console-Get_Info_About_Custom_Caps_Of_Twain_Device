# VintaSoft Console Get_Info_About_Custom_Caps_Of_Twain_Device
TWAIN device parameters can be changed using the device capabilities. List of standard device capabilities is specified in TWAIN specification. Also TWAIN specification allows the device vendors to create the custom device capabilities.

Only Kodak provides good public information about the custom device capabilities of their scanners. Information about all capabilities of Kodak scanners can be found in Kodak TWAIN Integrator Kit. The Kodak TWAIN Integrator Kit can request on Kodak's web site.

The user interface of TWAIN driver provides many features, for example, Canon scanners allow to set the color mode parameter and detect the color mode automatically. You need to change the standard and custom device capabilities if the color mode parameter need to be used programmatically without driver UI. Unfortunately, Canon does not provide public information about the custom device capabilities and at this point the main problem occurs - we need to know which device capabilities to change for changing the color mode programmatically!

For getting information about the device capabilities, which must be changed, we need save information about the device capabilities, open the device UI, change the necessary parameter (for example "color mode") in device UI, save information about device capabilities once again, compare saved information and determine which device capabilities were changed!

Here are necessary steps:
- Run the GetInfoAboutCustomDeviceCapabilities application.
- Select necessary device, for example, "Canon DR-2510"
- Application will open the device and save information about device capabilities in file "caps1.txt". After this the device UI will appear.
- In device UI change the parameter, which you want to explore, for example, change value of the "Color mode" from "Black-white" to "Automatic".
- Close the device UI.
- Application will save information about device capabilities in file "caps2.txt" and will close the device.
- Application will be finished and you will have files "caps1.txt" and "caps2.txt".
- Compare files "caps1.txt" and "caps2.txt", for example, using SVN, and get information which device capabilities must be changed and how for changing the "Color mode" from "Black-white" to "Automatic".

This .NET console project uses <a href="https://www.vintasoft.com/vstwain-dotnet-index.html">VintaSoft TWAIN .NET SDK</a> and demonstrates how to get information about custom capabilities of TWAIN device.


## Usage
1. Get the 30 day free evaluation license for <a href="https://www.vintasoft.com/vstwain-dotnet-index.html" target="_blank">VintaSoft TWAIN .NET SDK</a> as described here: <a href="https://www.vintasoft.com/docs/vstwain-dotnet/Licensing-Twain-Evaluation.html" target="_blank">https://www.vintasoft.com/docs/vstwain-dotnet/Licensing-Twain-Evaluation.html</a>

2. Update the evaluation license in "CSharp\Program.cs" file:
   ```
   Vintasoft.Twain.TwainGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");
   ```

3. Build the project ("GetInfoAboutCustomDeviceCapabilities.Net4.csproj" file) in Visual Studio or using .NET CLI:
   ```
   dotnet build GetInfoAboutCustomDeviceCapabilities.Net4.csproj
   ```

4. Run compiled application.


## Documentation
VintaSoft TWAIN .NET SDK on-line User Guide and API Reference for .NET developer is available here: https://www.vintasoft.com/docs/vstwain-dotnet/


## Support
Please visit our <a href="https://myaccount.vintasoft.com/">online support center</a> if you have any question or problem.
