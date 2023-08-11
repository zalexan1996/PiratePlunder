using System.Linq;
using PiratePlunder.Engine.Core;
using PP.GameStates;
using Serilog;

namespace PiratePlunder;

public class Program
{
    private static readonly string debugFlag = "-DEBUG";
    public bool IsDebug { get; private set; } = false;
    
    public static void Main(string[] args)
    {
        var argsList = args.ToList();
        var program = new Program(argsList.Any(x => x.ToUpper() == debugFlag));
        program.Start();
    }

    public Program(bool isDebug)
    {
        IsDebug = isDebug;
    }

    public void Start()
    {
        ConfigureLogger();
        using var game = new PPGame(new PPGameState());

        game.Run();
    }

    protected void ConfigureLogger()
    {
        var loggerConfig = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.File("Saved/log-.txt", 
                rollingInterval: RollingInterval.Hour);

        if (IsDebug)
        {
            loggerConfig.MinimumLevel.Verbose();
        }
        else
        {
            loggerConfig.MinimumLevel.Warning();
        }
        
        Log.Logger = loggerConfig.CreateLogger();

        Log.Logger.Information("Logger configured.");
    }
}