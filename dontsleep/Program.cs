﻿using dontsleep;
using NLog;

class Program : AudioCheck
{
    private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();
    static void Main()
    {
        //TODO - Kullanıcı arayüzü geliştir.
        Console.WriteLine("Press Enter to exit.");

        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            bool isAudioPlaying = IsAudioPlaying();
            double soundLevel = defaultDevice.AudioMeterInformation.MasterPeakValue;
            if (isAudioPlaying)
            {
                pauseSleep();
                logger.Trace($"Screen sleep has stopped! Sound level is: [{Math.Round(soundLevel, 2)}]\n");
            }
            else
            {
                resumeSleep();
                logger.Trace($"Screen sleep is now working!  Sound level is: [{Math.Round(soundLevel,2)}]\n");

            }

            Thread.Sleep(1000);

        }
        try
        {
            logger.Warn($"program closing! \n");
            LogManager.Shutdown();
        }
        catch (Exception e)
        {
            logger.Error($"An Error Has Been Occured!:" + e);
            LogManager.Shutdown();
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}
