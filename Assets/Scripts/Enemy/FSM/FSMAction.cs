using UnityEngine;

// Finite State Machine Action class.
// This class acts as a definition of
// all actions an enemy can take.
public abstract class FSMAction : MonoBehaviour
{
    public abstract void Act();

}
