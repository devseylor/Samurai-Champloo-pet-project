using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        public HeroAnimator Animator;
        private State _state;

        public float Current 
        {
            get => _state.CurrentHp; 
            set => _state.CurrentHp = value; 
        }
        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = Current;
            progress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            Animator.PlayHit();
        }
    }
}