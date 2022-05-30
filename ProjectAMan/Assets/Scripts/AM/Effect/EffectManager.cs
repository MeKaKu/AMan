using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AM.MsgFramework.Effect;
using UnityEngine;

namespace Assets.Scripts.AM.Effect
{
    public class EffectManager : EffectBase
    {
        #region 消息绑定
        private void Awake() {
            Bind(
                0
            );
        }
        public override void Execute(int eventCode, object arg) {
            switch(eventCode){
                case 0:{
                    HandleEffectMsg(arg as EffectMsg);
                    break;
                }
                default:break;
            }
        }
        //GameObject muzzleEffect;
        void HandleEffectMsg(EffectMsg msg){
            GameObject effect = Instantiate<GameObject>(effectConfig.GetEffectPrefab(msg.id));
            switch(msg.id){
                case 0: //起跳灰尘
                case 1://子弹击中墙 灰尘
                    effect.transform.position = msg.pos;
                    effect.transform.eulerAngles = msg.rot;
                    break;
                case 2://Muzzle fire 枪火
                    effect.transform.position = msg.pos;
                    effect.transform.eulerAngles = msg.rot;
                    break;
                
            }
        }
        #endregion
        
        #region 子物体
        public EffectConfig effectConfig;
        private void Start() {//初始化
            effectConfig.Init();
        }
        #endregion
        
        #region 方法
        #endregion
        
        #region Test
        #if UNITY_EDITOR
        [ContextMenu("Test/Test")]
        void TEST_Test(){
            
        }
        #endif
        #endregion
    }
}