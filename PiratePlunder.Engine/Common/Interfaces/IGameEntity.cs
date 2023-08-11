using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Common.Interfaces;

public interface IGameEntity
{
    void Initialize();
    void LoadContent();
    void UnloadContent();
    void Update();
    void Draw();
}