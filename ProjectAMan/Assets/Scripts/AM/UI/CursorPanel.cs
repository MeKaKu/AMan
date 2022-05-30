using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts.AM.UI
{
    public class CursorPanel : PanelBase, IPointerEnterHandler, IPointerExitHandler
    {
        #region 消息绑定
        private void Awake() {
            Bind(
                UIEventCode.SHOW_HIT_ENEMY_CURSOR,
                UIEventCode.SHOW_KILL_ENEMY_CURSOR
            );
        }
        public override void Execute(int eventCode, object arg) {
            switch(eventCode){
                case UIEventCode.SHOW_HIT_ENEMY_CURSOR:{
                    IncreCursorSize(Random.Range(40,70));
                    break;
                }
                case UIEventCode.SHOW_KILL_ENEMY_CURSOR:{
                    IncreCursorSize(Random.Range(60, 100));
                    SetCornerColor(Color.red);
                    break;
                }
                default:break;
            }
        }
        #endregion
        
        #region 子物体
        RectTransform cursor;
        List<Image> corners;
        private void Start() {//初始化
            cursor = transform.Find("Cursor").GetComponent<RectTransform>();
            corners = new List<Image>();
            for(int i=0;i<4;i++){
                corners.Add(cursor.Find($"Img_Coner_{i}").GetComponent<Image>());
            }

            originSize = cursor.sizeDelta;
            increSize = 4f*originSize;

        }
        #endregion
        
        #region 方法
        void SetCornerColor(Color color){
            foreach(var img in corners){
                img.color = color;
            }
            redDuration = .5f;
        }
        Vector2 originSize;
        Vector2 increSize;
        float speed = 5;
        float redDuration;
        void Update() {
            cursor.sizeDelta = Vector2.Lerp(cursor.sizeDelta, originSize, Time.deltaTime * speed);
            if(redDuration > 0){
                redDuration -= Time.deltaTime;
                if(redDuration <= 0) SetCornerColor(Color.white);
            }   
            if(isEnter){
                cursor.anchoredPosition = Input.mousePosition;
            }
        }
        void IncreCursorSize(float sp){
            if(cursor.sizeDelta.x < increSize.x)
            cursor.sizeDelta += sp * Vector2.one;
        }
        bool isEnter;
        public void OnPointerEnter(PointerEventData eventData)
        {
            isEnter = true;
            Cursor.visible = false;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            isEnter = false;
            Cursor.visible = true;
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