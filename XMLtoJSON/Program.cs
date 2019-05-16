using System;
using System.IO;
using System.Linq;

namespace XMLtoJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFile = null;
            // try open file and read xml as string
            try
            {
                using (StreamReader sr = new StreamReader("input.xml"))
                {
                    Console.WriteLine("Reading file 'input.xml'");
                    xmlFile = sr.ReadToEnd();
                    // remove unwanted characters
                    xmlFile = new string(xmlFile.Where(c => !char.IsControl(c)).ToArray());
                }
            }
            catch (IOException)
            {
                Console.Error.WriteLine("File 'input.xml' not found");
                Environment.Exit(-1);
            }

            Console.WriteLine("Creating objects list from xml tree");
            // convert xml to json
            string outputJSON = Convert.ConvertFunction(xmlFile);
            
            try
            {
                Console.WriteLine("Writing JSON file");
                File.WriteAllText("output.json", outputJSON.ToString());
                Console.WriteLine("Output: 'output.json'");
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("Unexpected error creating output file");
                Console.Error.Write(e.Message);
            }
        }
    }
}
