using UnityEngine;

namespace Yudiz.ShootingGame.Base
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            Instance = this as T;
        }
    }
}