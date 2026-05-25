using Cysharp.Threading.Tasks;
using System.Threading;
using TurnBasedCombat.Core;
using TurnBasedCombat.Gamplay;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Presentation
{
    public class UnitFactory
    {
        private readonly UnitVisualFactory _unitVisualFactory;
        private readonly HealthBarFactory _uiFactory;

        private readonly Transform _playerHudParent;
        private readonly Transform _enemyHudParent;


        
        public UnitFactory(
            UnitVisualFactory unitVisualFactory, 
            HealthBarFactory uiFactory, 
            [Inject(Id = "PlayerHudParent")] Transform playerHudParent, 
            [Inject(Id = "EnemyHudParent")] Transform enemyHudParent)
        {
            _unitVisualFactory = unitVisualFactory;
            _uiFactory = uiFactory;
            _playerHudParent = playerHudParent;
            _enemyHudParent = enemyHudParent;
        }

        public async UniTask<(UnitVisual visual, UnitPresenter presenter)> CreateUnitContextAsync(
            string visualKey,
            string uiKey,
            Vector2Int spawnGridPos,
            UnitModel underlyingModel,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Transform targetUiParent = underlyingModel.Faction == UnitFactions.Player ? _playerHudParent : _enemyHudParent;

            var visual = _unitVisualFactory.CreateVisualAsync(visualKey, spawnGridPos, ct);
            var ui = _uiFactory.CreateHealthBarAsync(uiKey, targetUiParent, ct);

            var (unitVisual, healthBarView) = await UniTask.WhenAll(visual, ui);

            UnitPresenter presenter = new UnitPresenter(healthBarView, underlyingModel);
            presenter.Initialize();

            return (unitVisual, presenter);
        }
    }
}
