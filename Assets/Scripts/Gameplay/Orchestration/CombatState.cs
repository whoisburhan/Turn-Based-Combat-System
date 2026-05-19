using UnityEngine;

namespace TurnBasedCombat.Gamplay
{
    public enum CombatState
    {
        Setup,
        PlayerTurn,
        EnemyTurn,
        CommandExecution,
        Victory,
        Defeat
    }
}
