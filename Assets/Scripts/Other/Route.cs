using UnityEngine;

public class Route 
{
    public Vector3 Target { get; private set; }
    public Vector3 Stock { get; private set; }
    public Vector3 Entry { get; private set; }
    public Vector3 Exit { get; private set; }
    
    public Route(Vector3 target, Vector3 stock, Vector3 entry, Vector3 exit)
    {
        Target = target;
        Stock = stock;
        Entry = entry;
        Exit = exit;
    }
}