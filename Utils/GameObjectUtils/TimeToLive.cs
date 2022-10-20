using System.ComponentModel;
using UnityEngine;

namespace Utils.GameObjectUtils
{
    public class TimeToLive : MonoBehaviour
    {
        [SerializeField] 
        [Description("In seconds")]
        private float _timeToLive;

        private void Start()
        {
            Destroy(gameObject, _timeToLive);
        }
    }
}