using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.CoreAudioApi;

class Program
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_CONTINUOUS = 0x80000000,
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_AWAYMODE_REQUIRED = 0x00000040
    }

    private static MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
    private static MMDevice defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

    static void Main()
    {
        Console.WriteLine("Press Enter to exit.");

        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            bool isAudioPlaying = IsAudioPlaying();

            if (isAudioPlaying)
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
            }
            else
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }

            Thread.Sleep(1000);
        }
    }

    private static bool IsAudioPlaying()
    {
        return defaultDevice.AudioMeterInformation.MasterPeakValue > 0.1; // Adjust threshold as needed
    }
}
