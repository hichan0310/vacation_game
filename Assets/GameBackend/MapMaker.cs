using System.Collections.Generic;
using GameFrontEnd;
using UnityEngine;

namespace GameBackend
{
    public class MapMaker:MonoBehaviour
    {
        public MapBlock startBlock;
        public MapBlock endBlock;
        public List<MapBlock> Blocks;
        private HashSet<MapBlock> mapBlocks = new HashSet<MapBlock>();

        public void makeMap(int value)
        {
            mapBlocks.Add(startBlock);
            // todo
            // startBlock, endBlock과 연결되게 랜덤한 Blocks 원소를 가져다가
            // connection.open() 하고 connection끼리 좌표 겹치게 연결
            // 그 상황에서 value도 최대한 비슷하게 해야 해서 신경쓸게 많음. 
            // 구현은 꽤 빡셀듯
        }
    }
}