using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Common.Interfaces;

public interface IGameEntityContainer : IGameEntity
{
    protected ICollection<IGameEntity> RegisteredEntity { get; }

    Guid RegisterEntity(IGameEntity entity);
    void UnregisterEntity(Guid id);
}
