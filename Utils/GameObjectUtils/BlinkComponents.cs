using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TotojGameDev
{
    /**
     * Blink components by enabling/disabling them
     * Just disable this component to stop blink (will disable all components)
     */
    public class BlinkComponents : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _components = new List<MonoBehaviour>();
        
        [Header("Times every second")]
        [Min(0.1f)]
        [SerializeField] private float _blinkFrequency = 1f;

        private void OnEnable()
        {
            _components.ForEach(component => component.enabled = false);
            StartCoroutine(Co_BlinkComponents());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _components.ForEach(component => component.enabled = false);
        }

        private IEnumerator Co_BlinkComponents()
        {
            float frequency = 1 / _blinkFrequency;
            while (true)
            {
                _components.ForEach(component => component.enabled = true);
                yield return new WaitForSeconds(frequency);
                _components.ForEach(component => component.enabled = false);
                yield return new WaitForSeconds(frequency);
            }
        }
    }
}