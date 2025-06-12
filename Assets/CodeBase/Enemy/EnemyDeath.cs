using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public EnemyAnimator Animator;

        public GameObject DeathFx;

        public event Action Happend;

        private void Start() => 
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if(Health.Current <= 0)            
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;

            Animator.PlayDeath();

            SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            Happend?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(DeathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}