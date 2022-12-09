using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{
    public class nnode
    {
        int depth;
        bool walkable;

        GameObject waypoint = new GameObject();
        List<nnode> neighbor = new List<nnode>();

        public int Depth { get => depth; set => depth = value; }
        public bool Walkable { get => walkable; set => walkable = value; }
    }
}
