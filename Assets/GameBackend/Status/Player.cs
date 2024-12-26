using System.Collections.Generic;

namespace GameBackend.Status
{
    public class Player : Entity
    {
        Player()
        {
            status = new PlayerStatus(10000, 1000, 1000);
            eventListener = new List<IEntityEventListener>();
        }
    }
}