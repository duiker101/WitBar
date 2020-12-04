using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App
{
    class ScriptExecuter
    {
        private String file;

        public ScriptExecuter(String file)
        {
            this.file = file;
        }

        public RootEntry ExecuteCommand()
        {

            var output = new StringBuilder();

            var processInfo = new ProcessStartInfo("cmd.exe", "/Q /c " + this.file);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(file);

            var process = Process.Start(processInfo);

            int currentDepth = 0;

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                output.AppendLine(e.Data);
            };

            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Debug.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Debug.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();

            RootEntry res = OutputParser.parse(output.ToString());

            return res;
        }
    }
}
