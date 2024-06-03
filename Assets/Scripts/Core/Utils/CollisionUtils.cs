using UnityEngine;

namespace Core.Utils
{
    public class CollisionUtils
    {
        public static bool CheckRectanglesIntersect(RotatedRectangle rect1, RotatedRectangle rect2)
        {
            var vertices1 = rect1.GetVertices();
            var vertices2 = rect2.GetVertices();

            var axes = new Vector2[4];
            axes[0] = GetEdgeNormal(vertices1[0], vertices1[1]);
            axes[1] = GetEdgeNormal(vertices1[1], vertices1[2]);
            axes[2] = GetEdgeNormal(vertices2[0], vertices2[1]);
            axes[3] = GetEdgeNormal(vertices2[1], vertices2[2]);

            foreach (var axis in axes)
            {
                if (!IsProjectionOverlapping(vertices1, vertices2, axis))
                {
                    return false;
                }
            }

            return true;
        }

        private static Vector2 GetEdgeNormal(Vector2 point1, Vector2 point2)
        {
            var edge = point2 - point1;
            return new Vector2(-edge.y, edge.x);
        }

        private static bool IsProjectionOverlapping(Vector2[] vertices1, Vector2[] vertices2, Vector2 axis)
        {
            float min1, max1, min2, max2;
            ProjectVertices(vertices1, axis, out min1, out max1);
            ProjectVertices(vertices2, axis, out min2, out max2);

            return !(max1 < min2 || max2 < min1);
        }

        private static void ProjectVertices(Vector2[] vertices, Vector2 axis, out float min, out float max)
        {
            min = Vector2.Dot(vertices[0], axis);
            max = min;

            for (var i = 1; i < vertices.Length; i++)
            {
                var projection = Vector2.Dot(vertices[i], axis);
                if (projection < min)
                {
                    min = projection;
                }
                else if (projection > max)
                {
                    max = projection;
                }
            }
        }

        public class RotatedRectangle
        {
            private float _height;
            private Vector2 _center;
            private float _rotation;
            private float _width;

            public RotatedRectangle(Vector2 center, float width, float height, float rotation)
            {
                _center = center;
                _width = width;
                _height = height;
                _rotation = rotation;
            }

            public Vector2[] GetVertices()
            {
                // Half dimensions
                var halfWidth = _width / 2;
                var halfHeight = _height / 2;

                // Rotation matrix components
                var cosTheta = Mathf.Cos(_rotation * Mathf.Deg2Rad);
                var sinTheta = Mathf.Sin(_rotation * Mathf.Deg2Rad);

                // Calculate the four corners
                var vertices = new Vector2[4];
                vertices[0] = _center + new Vector2(halfWidth * cosTheta - halfHeight * sinTheta,
                    halfWidth * sinTheta + halfHeight * cosTheta);
                vertices[1] = _center + new Vector2(-halfWidth * cosTheta - halfHeight * sinTheta,
                    -halfWidth * sinTheta + halfHeight * cosTheta);
                vertices[2] = _center + new Vector2(-halfWidth * cosTheta + halfHeight * sinTheta,
                    -halfWidth * sinTheta - halfHeight * cosTheta);
                vertices[3] = _center + new Vector2(halfWidth * cosTheta + halfHeight * sinTheta,
                    halfWidth * sinTheta - halfHeight * cosTheta);

                return vertices;
            }
        }
    }
}
