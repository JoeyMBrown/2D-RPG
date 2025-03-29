using System;
using UnityEngine;

public class AttributeButton : MonoBehaviour
{
    // Registering event, will use referecent to our Attribute class
    public static event Action<AttributeType> OnAttributeSelectedEvent;

    [Header("Config")]
    [SerializeField] private AttributeType attribute;

    public void SelectAttribute()
    {
        // It's important we check if the event is null here
        // in C# this event will always be null if it has no
        // listeners, so trying to invoke this event without
        // listeners will throw an exception crashing the game.
        OnAttributeSelectedEvent?.Invoke(attribute);
    }
}
