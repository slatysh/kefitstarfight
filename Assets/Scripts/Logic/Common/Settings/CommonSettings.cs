using System;
using System.Collections.Generic;
using Logic.Bullet;
using Logic.Enemy;
using Logic.Player;
using UnityEngine;

namespace Logic.Common
{
    [CreateAssetMenu(fileName = "CommonSettings", menuName = "Settings/CommonSettings", order = 1)]
    public class CommonSettings : ScriptableObject
    {
        public List<CommonSettingsBulletSettingsKvp> BulletSettingsDict = new List<CommonSettingsBulletSettingsKvp>();
        public PlayerAttackSettings PlayerAttackSettings = null;
        public PlayerMoveSettings PlayerMoveSettings = null;
        public EnemySpawnSettings EnemySpawnSettings = null;
    }


    [Serializable]
    public class CommonSettingsBulletSettingsKvp
    {
        public BulletType BulletType;
        public BulletSettings BulletSettings;
    }
}
