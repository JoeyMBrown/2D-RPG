using System;

[Serializable]
public class FSMTransition
{
    public FSMDecision Decision; // PlayerInRangeOfAttack -> True or False
    public string TrueState; // If true, transition to "TrueState" or "AttackState"
    public string FalseState; // If false, transition to "FalseState" or "PatrolState"
}
