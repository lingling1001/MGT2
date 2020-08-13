using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASMap
{
    private ASNode[,] _map;
    public void InitalMap(string lines)
    {


    }

    public ASNode GetNode(int x, int y)
    {

        return _map[x, y];
    }
}
