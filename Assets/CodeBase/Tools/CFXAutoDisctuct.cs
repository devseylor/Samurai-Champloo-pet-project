using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Tools
{
    public class CFXAutoDisctuct : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(DestroyEffect());

        }

        private IEnumerator DestroyEffect()
        {
            yield return new WaitForSeconds(3);

            Destroy(gameObject);
        }
    }
}