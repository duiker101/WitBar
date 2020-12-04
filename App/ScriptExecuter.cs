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
            var error = new StringBuilder();

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
           {
               error.AppendLine(e.Data);
           };

            process.BeginErrorReadLine();

            process.WaitForExit();

            RootEntry res;

            Debug.WriteLine(error.ToString());
            if (process.ExitCode == 0)
                res = OutputParser.parse(output.ToString());
            else
            {
                res = new RootEntry();
                res.error = true;
                res.errorMessage = error.ToString();
            }

            process.Close();

            return res;
        }
    }
}
