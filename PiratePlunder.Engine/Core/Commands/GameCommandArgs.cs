using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Core.Commands;

public class GameCommandArgs
{
    public object Sender { get; set; }
    public MainGame Game { get; set; }
    public GameState SenderGameState { get; set; }
}
