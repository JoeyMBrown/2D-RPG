using UnityEngine;

// This class allows us to basicailly still inherit MonoBehavior,
// while also instantiating a "Instance" of whatever class inherits
// this one automatically in the awake method, and assigning that
// instance to a public Instance variable.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
