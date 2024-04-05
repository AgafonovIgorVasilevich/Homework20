using UnityEngine;

public class Route 
{
    public Vector3 Target { get; private set; }
    public Vector3 Factory { get; private set; }
    public Vector3 Entry { get; private set; }
    public Vector3 Exit { get; private set; }
    
    public Route(Vector3 target, Vector3 factory, Vector3 entry, Vector3 exit)
    {
        Target = target;
        Factory = factory;
        Entry = entry;
        Exit = exit;
    }
}