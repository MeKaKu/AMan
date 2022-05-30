using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AM.UI
{
    public class HpPanel : PanelBase
    {
        #region 消息绑定
        private void Awake() {
            Bind(
                UIEventCode.ADD_PLAYER_HP
            );
        }
        public override void Execute(int eventCode, object arg) {
            switch(eventCode){
                case UIEventCode.ADD_PLAYER_HP:{
                    AddHp((float)arg);
                    break;
                }
                default:break;
            }
        }
        #endregion
        
        #region 子物体
        Image Img_Hp;
        float t = 10;
        float hp;
        private void Start() {//初始化
            Img_Hp = transform.Find("HP/RedHp").GetComponent<Image>();

            hp = 1;
        }
        #endregion
        
        #region 方法
        bool isDead;
        private void Update() {
            if(isDead) return;
            if(hp < 0){
                isDead = true;
                //TODO 玩家死亡
                //可以交由场景管理器处理
            }
            else{
                hp -= Time.deltaTime / t;
            }
            Img_Hp.transform.localScale = new Vector3(hp, 1, 1);
        }
        void AddHp(float percent){
            hp += percent;
            if(hp > 1) hp = 1; 
        }
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