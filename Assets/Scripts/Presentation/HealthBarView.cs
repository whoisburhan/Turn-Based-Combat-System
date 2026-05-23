using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedCombat.Presentation
{
    public class HealthBarView : MonoBehaviour
    {
        [Header("UI Slider Binding")]
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _healthText;

        public void UpdateDisplay(int currentHp, int maxHp)
        {
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = 1;

            float fillAmount = maxHp > 0 ? (float)currentHp / maxHp : 0f;

            _healthSlider.value = fillAmount;
            _healthText.text = $"{currentHp} / {maxHp}";
        }
    }
}
