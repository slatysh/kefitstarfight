using System;
using System.Collections.Generic;
using Logic.Common;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Bullet
{
    public class BulletFactory : IBulletFactory
    {
        public Dictionary<BulletType, BulletSettings> bulletSettingsMap;

        public BulletFactory(List<CommonSettingsBulletSettingsKvp> settingsDict)
        {
            bulletSettingsMap = new Dictionary<BulletType, BulletSettings>();
            foreach (var setting in settingsDict)
            {
                bulletSettingsMap.Add(setting.BulletType, setting.BulletSettings);
            }
        }

        public BulletModel Create(BulletType type, Vector2 pos, float rotation)
        {
            var instSetting = bulletSettingsMap[type];
            switch (type)
            {
                case BulletType.Simple:
                    var instSimple = new BulletSimpleModel(pos, instSetting.Speed, rotation, instSetting.Lifetimer);
                    instSimple.SetMoveStrategy(new MoveDirectStrategy(instSimple));
                    return instSimple;
                case BulletType.Laser:
                    var instLaser = new BulletLaserModel(pos, instSetting.Speed, rotation, instSetting.Lifetimer);
                    instLaser.SetMoveStrategy(new MoveStayStrategy());
                    return instLaser;
            }

            throw new ArgumentException($"No bullet type for type: {type.ToString()}");
        }
    }
}
