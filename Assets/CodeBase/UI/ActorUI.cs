using CodeBase.Hero;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _health;


        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            if (health != null)
                Consctruct(health);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateHpBar;

        public void Consctruct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() => 
            HpBar.SetValue(_health.Current, _health.Max);
    }
}