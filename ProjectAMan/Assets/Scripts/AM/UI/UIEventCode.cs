using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AM.UI
{
    public class UIEventCode
    {
        /// <summary>
        /// int
        /// </summary>
        public const int SHOW_PAPER_CONTENT = 10;
        public const int SHOW_HIT_ENEMY_CURSOR = 11;
        public const int SHOW_KILL_ENEMY_CURSOR = 12;

        /// <summary>
        /// 参数：float (生命值增加的百分比)
        /// </summary>
        public const int ADD_PLAYER_HP = 13;
    }
}