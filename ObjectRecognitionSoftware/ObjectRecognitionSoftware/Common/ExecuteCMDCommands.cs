using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ObjectRecognitionSoftware.Common
{
    public static class ExecuteCMDCommands
    {
        public static DataReceivedEventHandler outputHandler;
        private static Process m_Process = new Process();
        private static StringBuilder m_CmdOutput = new StringBuilder();        

        public static StringBuilder RunCommand(string arguments, bool readStream = true)
        {
            m_Process.StartInfo = GetStartInformation(arguments);
            m_Process.Start();
            ReadOutput();
            m_Process.WaitForExit();
            return m_CmdOutput;
        }

        public static StringBuilder RunMultipleCommands(List<string> commands)
        {
            m_Process.StartInfo = GetStartInformation(string.Empty);
            m_Process.OutputDataReceived += new DataReceivedEventHandler(outputHandler);
            m_Process.Start();

            using (var writer = m_Process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    foreach (var command in commands)
                    {
                        writer.WriteLine(command);
                    }
                }
            }
            
            ReadOutput();
            m_Process.WaitForExit();
            return m_CmdOutput;
        }

        private static void ReadOutput()
        {
            m_Process.BeginOutputReadLine();
        }

        public static string GetCommandOutput(string arguments)
        {             
            m_Process.StartInfo = GetStartInformation(arguments);
            m_Process.Start();
            string output = m_Process.StandardOutput.ReadLine();
            m_Process.Kill();
            return output;
        }

        public static StringBuilder GetRealtimeCMD()
        {
            return m_CmdOutput;
        }

        private static ProcessStartInfo GetStartInformation(string arguments)
        {
            var startinfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            if (!string.IsNullOrEmpty(arguments))
            {
                startinfo.Arguments = string.Format("/k {0}", arguments);
            }

            return startinfo;
        }  
    }
}
