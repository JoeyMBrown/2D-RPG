using System;

// For each state we will have the ability to perform
// different actions and transitions.
[Serializable]
public class FSMState
{
    public string ID;
    // This defines the different actions this state can perform
    public FSMAction[] Actions;
    // Transitions define what states we transition to based on a set
    // of rules being true or false.
    public FSMTransition[] Transitions;

    public void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }

    private void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if (Transitions == null || Transitions.Length <= 0) return;

        for (int i = 0; i < Transitions.Length; i++)
        {
            bool value = Transitions[i].Decision.Decide();

            if (value)
            {
                enemyBrain.ChangeState(Transitions[i].TrueState);
            }
            else
            {
                enemyBrain.ChangeState(Transitions[i].FalseState);
            }
        }
    }
}
