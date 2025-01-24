using peak.core;
using VL.Devices.IDS.Advanced;
using Path = VL.Lib.IO.Path;

namespace VL.Devices.IDS
{
    [ProcessNode(Name = "ConfigReader")]
    public class FileConfigurationNode
    {
        IConfiguration? configuration;
        Path? file;

        [return: Pin(Name = "Output")]
        public IConfiguration Update(
            Path filePath,
            bool read)
        {
            if (read)
            {
                this.file = filePath;
                configuration = new FileConfiguration(filePath);
            }
            return configuration!;
        }
    }

    class FileConfiguration : IConfiguration
    {
        public Path File { get; }
        public FileConfiguration(Path file)
        {
            File = file;
        }

        public void Configure(NodeMap nodeMap)
        {
            if (File.Exists)
            {
                //nodeMap.FindNodeEnumeration("UserSetSelector").SetCurrentEntry("UserSet1");
                //nodeMap.InvalidateNodes();
                nodeMap.LoadFromFile(File.ToString());
                //nodeMap.FindNodeCommand("UserSetSave").Execute();
                //nodeMap.FindNodeCommand("UserSetSave").WaitUntilDone();



                //nodeMap.FindNodeString("UEyeParametersetPath").SetValue(File.ToString());
                //nodeMap.FindNodeCommand("UEyeParametersetLoad").Execute();
            }
        }
    }
}
