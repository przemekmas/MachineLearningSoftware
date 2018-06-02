using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ObjectRecognitionSoftware.Common
{
    public static class ExecuteCMDCommands
    {
        public static DataReceivedEventHandler outputHandler;
        private static Process _process = new Process();
        private static StringBuilder _cmdOutput = new StringBuilder();        

        public static StringBuilder RunCommand(string arguments, bool readStream = true, bool isPythonEnvironment = false)
        {
            _process.StartInfo = GetStartInformation(arguments, isPythonEnvironment);
            _process.Start();
            _process.WaitForExit();
            return _cmdOutput;
        }

        public static StringBuilder RunMultipleCommands(List<string> commands, bool isPythonEnvironment = false, bool redirectOutput = false)
        {
            _process.StartInfo = GetStartInformation(string.Empty, isPythonEnvironment, redirectOutput);
            _process.OutputDataReceived += new DataReceivedEventHandler(outputHandler);
            _process.Start();

            using (var writer = _process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    foreach (var command in commands)
                    {
                        writer.WriteLine(command);
                    }
                }
            }
            
            _process.WaitForExit();
            return _cmdOutput;
        }

        private static void ReadOutput()
        {
            // Can only read output when it is not redirected
            _process.BeginOutputReadLine();
        }

        public static string GetCommandOutput(string arguments)
        {             
            _process.StartInfo = GetStartInformation(arguments);
            _process.Start();
            string output = _process.StandardOutput.ReadLine();
            _process.Kill();
            return output;
        }

        public static StringBuilder GetRealtimeCMD()
        {
            return _cmdOutput;
        }

        private static ProcessStartInfo GetStartInformation(string arguments, bool isPythonEnvironment = false, bool redirectOutput = false)
        {
            var startinfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = false,
                CreateNoWindow = true,
                UseShellExecute = false,
            };

            if (isPythonEnvironment)
            {
                var pythonScriptsPath = Python.GetLatestPythonScriptsDirectory();
                if (!string.IsNullOrEmpty(pythonScriptsPath))
                {
                    startinfo.EnvironmentVariables["Path"] = pythonScriptsPath;
                }               
            }    
            
            if (redirectOutput)
            {
                startinfo.RedirectStandardOutput = true;
            }

            if (!string.IsNullOrEmpty(arguments))
            {
                startinfo.Arguments = string.Format("/k {0}", arguments);
            }

            return startinfo;
        }  
    }
}
