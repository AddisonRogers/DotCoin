using System;
using System.Diagnostics;
using System.Threading;

namespace DotCoin3;

public class Events
{
    //The purpose of this class is to detect if there is an event that can be an issue
    //If there is an issue then it will call the discord + sounds class to send a message to discord and play a sound
    public void Main()
    {
        Console.WriteLine("Starting Events, {0}, {1}, {2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.IsBackground);
        //Call the functions to see if anything has gone wrong
        //https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread?view=net-6.0
        
        
        
    }
}