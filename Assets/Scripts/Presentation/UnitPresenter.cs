using System;
using TurnBasedCombat.Gamplay;
using Zenject;

namespace TurnBasedCombat.Presentation
{
    public class UnitPresenter : IInitializable, IDisposable
    {
        private readonly HealthBarView _view;
        private readonly UnitModel _model;

        public UnitPresenter(HealthBarView view, UnitModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.UpdateDisplay(_model.CurrentHp, _model.MaxHp);

            _model.OnHpChanged += HandleHpChanged;

        }

        private void HandleHpChanged(int currentHp)
        {
            _view.UpdateDisplay(currentHp, _model.MaxHp);
        }

        public void Dispose()
        {
            _model.OnHpChanged -= HandleHpChanged;
        }


    }
}
