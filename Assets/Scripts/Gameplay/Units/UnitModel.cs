using System;
using UnityEngine;

namespace TurnBasedCombat.Gamplay
{
    public class UnitModel
    {
        public string Id { get; }
        public string Name { get; }
        public int MaxHp { get; }
        public int CurrentHp { get; private set; }
        public Vector2Int GridPosition { get; private set; }


        public event Action<int> OnHpChanged;
        public event Action<Vector2Int> OnPositionChanged;
        public event Action OnUnitDied;

        public UnitModel(string id, string name, int maxHp, Vector2Int startingPosition) 
        {
            Id = id;
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            GridPosition = startingPosition;
        }

        public void SetPosition(Vector2Int newPosition) 
        {
            if(GridPosition == newPosition) return;

            GridPosition = newPosition;
            OnPositionChanged?.Invoke(newPosition);
        }

        public void TakeDamage(int amount) 
        {
            if(CurrentHp <= 0) return;

            CurrentHp = Math.Max(0, CurrentHp - amount);
            OnHpChanged?.Invoke(CurrentHp);

            if (CurrentHp == 0)
            {
                OnUnitDied?.Invoke();
            }
        }
    }
}
