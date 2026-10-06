using System.Collections.Generic;
using UnityEngine;

namespace _Bludoku.Scripts.Boards
{
    public enum ClearShapeType
    {
        Row,
        Column,
        Box
    }

    public class ClearedShape
    {
        public ClearShapeType Type;
        public Vector3 Center;
        public Vector2 Size;
    }

    public class ClearResult
    {
        public int ClearedCount;
        public int FiguresRemovedCount;
        public List<Vector3> ClearedPositions;
        public List<ClearedShape> ClearedShapes;
    }
}
