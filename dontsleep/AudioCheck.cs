using NAudio.CoreAudioApi;
using System.Runtime.InteropServices;

namespace dontsleep
{
    internal class AudioCheck
    //IsAudioPlaying() ile kontrol edilerek 
    //pauseSleep() ile ekranın uykuya geçmesini durdurabilir, 
    //resumeSleep() ile ekranın uykuya girebilmesine izin verebilirsiniz.
    


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

        protected static bool IsAudioPlaying()
        {
            return defaultDevice.AudioMeterInformation.MasterPeakValue > 0.0001; // Adjust threshold as needed
        }
        protected static void pauseSleep()
        {
           SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }
        protected static void resumeSleep()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
    
}
