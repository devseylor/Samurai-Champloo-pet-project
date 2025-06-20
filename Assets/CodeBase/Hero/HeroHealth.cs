using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator Animator;
        private State _state;

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if(_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;
                    HealthChanged?.Invoke();
                }

            }
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
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