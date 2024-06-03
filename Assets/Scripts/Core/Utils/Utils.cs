using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Utils
{
    public static class Utils
    {
        public static T RandomEnum<T>() where T : struct, Enum
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(Random.Range(0, v.Length));
        }

        public static Vector2 RandomVector2(Vector2 vector1, Vector2 vector2)
        {
            var resX = Random.Range(vector1.x, vector2.x);
            var resY = Random.Range(vector1.y, vector2.y);
            var res = new Vector2(resX, resY);
            return res;
        }

        public static Vector2 RotationZToVector(float rotationZ)
        {
            var sinCur = Mathf.Sin(Mathf.Deg2Rad * rotationZ);
            var cosCur = Mathf.Cos(Mathf.Deg2Rad * rotationZ);
            var res = new Vector2(cosCur, sinCur);
            return res;
        }

        //TODO Move to separate service?
        public static DirComplex CheckNearScreenBorder(Vector2 pos, float offset = 50.0f, Camera camera = null)
        {
            var cameraCur = camera ?? Camera.main;
            var screenPoint = cameraCur.WorldToScreenPoint(pos);
            var hDir = Dir.No;
            if (screenPoint.x < offset)
            {
                hDir = Dir.Left;
            }
            else if (screenPoint.x > Screen.width - offset)
            {
                hDir = Dir.Right;
            }

            var vDir = Dir.No;
            if (screenPoint.y < offset)
            {
                vDir = Dir.Top;
            }
            else if (screenPoint.y > Screen.height - offset)
            {
                vDir = Dir.Bottom;
            }

            return new DirComplex(hDir, vDir);
        }

        public static bool IsRectCollidesRect((Vector2, Vector2) box1, Vector2 pos1, float sourceRotation,
            (Vector2, Vector2) box2, Vector2 pos2, float targetRotation)
        {
            var collisionX = box1.Item1.x + pos1.x <= box2.Item2.x + pos2.x &&
                             box1.Item2.x + pos1.x >= box2.Item1.x + pos2.x;
            var collisionY = box1.Item1.y + pos1.y <= box2.Item2.y + pos2.y &&
                             box1.Item2.y + pos1.y >= box2.Item1.y + pos2.y;
            return collisionX && collisionY;
        }


        public enum Dir
        {
            No,
            Left,
            Top,
            Right,
            Bottom
        }

        public struct DirComplex
        {
            public Dir HDir;
            public Dir VDir;

            public DirComplex(Dir hDir, Dir vDir)
            {
                HDir = hDir;
                VDir = vDir;
            }
        }
    }
}
