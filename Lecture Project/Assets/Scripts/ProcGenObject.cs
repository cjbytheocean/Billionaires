using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ProcGenObject
{
    public UnityEngine.Vector2 Position;
    public UnityEngine.Vector2 Direction;
    public float ChanceToChange;

    public ProcGenObject(UnityEngine.Vector2 pos, UnityEngine.Vector2 dir, float chanceToChange)
    {
        Position = pos;
        Direction = dir;
        ChanceToChange = chanceToChange;
    }
}