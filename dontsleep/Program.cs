using System.Text;
using dontsleep;
class Program: AudioCheck
{
    string path = Directory.GetCurrentDirectory();
    private static StringBuilder sb = new StringBuilder();
    static void Main()
    {
        Console.WriteLine("Press Enter to exit.");

        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            bool isAudioPlaying = IsAudioPlaying();

            if (isAudioPlaying)
            {
                pauseSleep();
                sb.Append($"[{DateTime.Now}] screen sleep is stopped! \n");
            }
            else
            {
                resumeSleep();
                sb.Append($"[{DateTime.Now}] screen sleep is now working! \n");
                
            }

            Thread.Sleep(1000);

        }
        try
        {
            sb.Append($"[{DateTime.Now}] program closing now! \n");
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\log.txt", sb.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("An Error Has Been Occured!:" + e);
            sb.Append($"{DateTime.Now} +An Error Has Been Occured!:" + e);
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\log.txt", sb.ToString());
        }
        finally
        {
            sb.Clear();
        }     
    }
}
