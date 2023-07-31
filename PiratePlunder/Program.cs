using System.Linq;
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
        using var game = new PPGame(ConfigureLogger());

        game.Run();
    }

    protected ILogger ConfigureLogger()
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
        
        var logger = loggerConfig.CreateLogger();

        logger.Information("Logger configured.");
        return logger;
    }
}