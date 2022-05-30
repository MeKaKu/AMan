using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AM.Effect
{
    public class DirtyEffect : MonoBehaviour
    {
        public float duration = .8f;
        void Start()
        {
            Invoke("DestroyMe", duration);
        }

        void DestroyMe(){
            Destroy(gameObject);
        }
    }
}