using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AM.Effect
{
    public class EffectMsg 
    {
        public int id;
        public Vector3 pos;
        public Vector3 rot;
        public EffectMsg(){}
        public EffectMsg(int id, Vector3 pos, Vector3 rot){
            SetMsg(id, pos, rot);
        }
        public void SetMsg(int id ,Vector3 pos){
            this.id = id;
            this.pos = pos;
        }
        public void SetMsg(int id, Vector3 pos, Vector3 rot){
            SetMsg(id, pos);
            this.rot = rot;
        }
    }
}