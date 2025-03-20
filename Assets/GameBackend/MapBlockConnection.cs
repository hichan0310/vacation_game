using UnityEngine;

namespace GameBackend
{
    public class MapBlockConnection:MonoBehaviour
    {
        public int type;
        public bool opened { get; private set; } = false;

        public void open()
        {
            opened = true;
        }
    }
}