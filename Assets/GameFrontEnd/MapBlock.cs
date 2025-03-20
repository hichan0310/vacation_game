using System.Collections.Generic;
using System.Linq;
using GameBackend;
using UnityEngine;

namespace GameFrontEnd
{
    public class MapBlock:MonoBehaviour
    {
        public int value;
        public List<MapBlockConnection> connections;

        public bool isConnectable(int type)
        {
            foreach (var connection in connections)
            {
                if (connection.type==type) return true;
            }
            return false;
        }

        public List<int> getConnectableTypes()
        {
            return connections.Select(connection => connection.type).ToList();
        }
    }
}
