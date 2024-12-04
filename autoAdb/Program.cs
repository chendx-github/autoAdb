// See https://aka.ms/new-console-template for more information
using autoAdb;
using System.Diagnostics;
using System.Text.RegularExpressions;

Console.WriteLine("start!");

List<string> configs = new List<string>();
configs = File.ReadAllLines("config.txt").ToList();
List<dev_process> dev_process1 = new List<dev_process>();
void 触发(string dev1)
{
    Console.WriteLine(dev1);
    foreach (var item in configs)
    {
        string[] strings = item.Split(' ');
        for (int i = 0; i < strings.Length; i++)
        {
            strings[i] = strings[i].Replace("%name%", dev1);
        }
        if (strings[0] == dev1 || strings[0] == "any")
        {
            Process process = new Process();
            process.StartInfo.FileName = strings[2];
            process.StartInfo.WorkingDirectory = strings[1];
            process.StartInfo.Arguments = string.Join(' ', strings[3..(strings.Length)]);
            process.Start();
            dev_process1.Add(new dev_process() { dev = dev1, pro1 = process });
        }
    }
    if (dev1 == "11")
    {
        //Process process = new Process();
        //process.StartInfo.FileName = @"D:\sof\adb\adb.exe";
        //process.StartInfo.WorkingDirectory = @"D:\sof\adb";
        //process.StartInfo.Arguments = "shell";
        //process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
        //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //process.StartInfo.RedirectStandardError = true;
        //process.StartInfo.RedirectStandardInput = true;
        //process.StartInfo.RedirectStandardOutput = true;
        //process.StartInfo.UseShellExecute = false;
        //process.OutputDataReceived += (s1, s2) =>
        //{
        //};
        //process.ErrorDataReceived += (s1, s2) =>
        //{
        //};
        //process.Start();
    }
}

void 断开(string dev1)
{
    Console.WriteLine("close " + dev1);
    foreach (var item in dev_process1.FindAll(s => s.dev == dev1))
    {
        try
        {
            item.pro1.Close();
            item.pro1.Kill();
            item.pro1.Dispose();
        }
        catch (Exception)
        {
        }
    }
}

string cmd_str = "adb devices";

List<Device> _Devices1 = new List<Device>();
AutoResetEvent auto1 = new AutoResetEvent(false);
while (true)
{
    cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
    {
        auto1.Set();
        string str = s1.Data;
        if (str != null && Regex.IsMatch(str, @"\tdevice$"))
        {
            string dev1 = str.Replace("\tdevice", "");
            var i01 = _Devices1.FindIndex(s => s.name == dev1);
            if (i01 > -1)
            {
                _Devices1[i01].lasttime = DateTime.Now;
            }
            else
            {
                _Devices1.Add(new Device() { name = dev1, lasttime = DateTime.Now });
                触发(dev1);
            }
        }
    });
    auto1.WaitOne();
    auto1.Reset();
    Thread.Sleep(1500);
    var de1 = _Devices1.FindAll(x => (DateTime.Now - x.lasttime).TotalMilliseconds > 1000 * 7);
    for (int i = 0; i < de1.Count; i++)
    {
        断开(de1[i].name);
        _Devices1.Remove(de1[i]);
    }
    Thread.Sleep(1500);
}

class Device
{
    public string name;
    public string description;
    public DateTime lasttime;
}
class dev_process
{
    public string dev;
    public Process pro1;
}
