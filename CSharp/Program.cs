using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Vintasoft.WinTwain;

namespace GetInfoAboutCustomDeviceCapabilities
{
    class Program
    {

        static bool _isUiClosed;



        static void Main(string[] args)
        {
            Vintasoft.Twain.TwainGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");

            try
            {
                // create TWAIN device manager
                using (DeviceManager deviceManager = new DeviceManager())
                {
                    // open TWAIN device manager
                    deviceManager.Open();

                    // show the TWAIN device selection dialog and select the device
                    deviceManager.ShowDefaultDeviceSelectionDialog();

                    // get the selected device
                    Device device = deviceManager.DefaultDevice;

                    // open the device
                    device.Open();
                    using (FileStream fs = new FileStream("caps1.xml", FileMode.Create))
                    {
                        // save information about device capabilities BEFORE capabilities changes
                        device.Capabilities.Save(fs);
                    }

                    // subscribe to the UserInterfaceClosed event
                    device.UserInterfaceClosed += new EventHandler(device_UserInterfaceClosed);
                    // show the device setup dialog
                    device.ShowSetupDialog();
                    // wait while the device setup dialog will not be closed
                    while (!_isUiClosed)
                    {
                        Application.DoEvents();
                    }
                    // unsubscribe from the UserInterfaceClosed event
                    device.UserInterfaceClosed -= new EventHandler(device_UserInterfaceClosed);

                    using (FileStream fs = new FileStream("caps2.xml", FileMode.Create))
                    {
                        // save information about device capabilities AFTER capabilities changes
                        device.Capabilities.Save(fs);
                    }

                    // close the device
                    device.Close();

                    // close TWAIN device manager
                    deviceManager.Close();
                }

                // get readable information about capabilities BEFORE capabilities changes
                ConvertXmlToReadableForm("caps1.xml", "caps1.txt");
                // get readable information about capabilities AFTER capabilities changes
                ConvertXmlToReadableForm("caps2.xml", "caps2.txt");
            }
            catch (LicenseException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void device_UserInterfaceClosed(object sender, EventArgs e)
        {
            _isUiClosed = true;
        }

        /// <summary>
        /// Converts XML text (1 line) into readable text file.
        /// </summary>
        static void ConvertXmlToReadableForm(string xmlFileName, string textFileName)
        {
            StringBuilder sb = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(xmlFileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.IsEmptyElement)
                            sb.Append(string.Format("<{0}/>", reader.Name));
                        else
                        {
                            sb.Append(string.Format("<{0}> ", reader.Name));
                            reader.Read();
                            if (reader.IsStartElement())
                                sb.Append(string.Format("\r\n<{0}>", reader.Name));
                            sb.AppendLine(reader.ReadString());
                        }
                    }
                }
            }
            File.WriteAllText(textFileName, sb.ToString());
        }

    }
}
