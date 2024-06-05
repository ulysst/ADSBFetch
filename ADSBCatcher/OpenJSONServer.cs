using System.Diagnostics;
using System;

namespace SeleniumWebDriverDemo;

public class OpenJSONServer
{
    public void StartJsonServer()
    {
        try
        {
            // Get the current base directory and ensure it points to the correct working directory
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string workingDirectory = baseDirectory;

            // Create a new process start info
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C json-server --watch output.json --port 3001",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory
            };

            // Start the process
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.ErrorDataReceived += (sender, args) => Console.WriteLine("ERROR: " + args.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    
    public void ClosePortActivity(int portNumber)
    {
        try
        {
            // Create a new process start info for netstat command
            ProcessStartInfo netstatStartInfo = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = $"-aon | findstr :{portNumber}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the netstat process
            using (Process netstatProcess = new Process())
            {
                netstatProcess.StartInfo = netstatStartInfo;
                netstatProcess.Start();

                string output = netstatProcess.StandardOutput.ReadToEnd();

                // Parse the output to get the PID
                string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.Contains("LISTENING"))
                    {
                        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int pid = int.Parse(parts[parts.Length - 1]);

                        // Terminate the process using taskkill
                        Process.Start("taskkill", $"/PID {pid} /F").WaitForExit();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

}