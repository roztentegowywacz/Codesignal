using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace analyzingLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                string[] dirs = Directory.GetFiles(@"/root/devops/logs", "*.log");
                
                var errorLogs = new Dictionary<string, int>();
                
                foreach (string dir in dirs) 
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(dir))
                        {
                            while (sr.Peek() >= 0)
                            {
                                var logLine = sr.ReadLine();
                                if (!String.IsNullOrEmpty(logLine))
                                {
                                    var logLineSplited = logLine.Split('|');
                                    if (logLineSplited[1] == "ERROR")
                                    {
                                        try
                                        {
                                            errorLogs.Add(logLineSplited[2], 1);
                                        }
                                        catch
                                        {
                                            errorLogs[logLineSplited[2]] ++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(e.Message);
                    } 
                }
                
                foreach (var log in errorLogs.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    Console.WriteLine($"{log.Key} {log.Value}");
                }
            } 
            catch (Exception e) 
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
