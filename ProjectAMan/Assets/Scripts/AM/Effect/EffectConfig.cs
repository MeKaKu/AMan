using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AM.Effect
{
    [CreateAssetMenu(menuName = "AM/EffectConfig")]
    public class EffectConfig : ScriptableObject
    {
        public List<EffectArticle> effects;
        [System.Serializable]
        public class EffectArticle{
            public int id;
            public GameObject prefab;
        }
        Dictionary<int, EffectArticle> dict;
        public void Init(){
            dict = new Dictionary<int, EffectArticle>();
            foreach(var effect in effects){
                dict.Add(effect.id, effect);
            }
        }
        public GameObject GetEffectPrefab(int id){
            return dict[id].prefab;
        }
    }
}