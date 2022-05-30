using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.AM
{
    public class LoadSceneTrig : MonoBehaviour
    {
        public int sceneInd = 0;
        bool isLoading;
        private void OnCollisionEnter2D(Collision2D other) {
            if(isLoading) return;
            if(other.gameObject.tag == "Player"){
                SceneManager.LoadScene(sceneInd);
                isLoading = true;
            }
        }
    }
}