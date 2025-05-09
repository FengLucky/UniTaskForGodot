﻿using System;
using System.Collections.Generic;
using Godot;

namespace Cysharp.Threading.Tasks.Internal
{
    internal static class GodotEqualityComparer
    {
        public static readonly IEqualityComparer<Vector2> Vector2 = new Vector2EqualityComparer();
        public static readonly IEqualityComparer<Vector3> Vector3 = new Vector3EqualityComparer();
        public static readonly IEqualityComparer<Vector4> Vector4 = new Vector4EqualityComparer();
        public static readonly IEqualityComparer<Color> Color = new ColorEqualityComparer();
        public static readonly IEqualityComparer<Rect2> Rect = new RectEqualityComparer();
        public static readonly IEqualityComparer<Quaternion> Quaternion = new QuaternionEqualityComparer();

        static readonly RuntimeTypeHandle vector2Type = typeof(Vector2).TypeHandle;
        static readonly RuntimeTypeHandle vector3Type = typeof(Vector3).TypeHandle;
        static readonly RuntimeTypeHandle vector4Type = typeof(Vector4).TypeHandle;
        static readonly RuntimeTypeHandle colorType = typeof(Color).TypeHandle;
        static readonly RuntimeTypeHandle rectType = typeof(Rect2).TypeHandle;
        static readonly RuntimeTypeHandle quaternionType = typeof(Quaternion).TypeHandle;
        
        public static readonly IEqualityComparer<Vector2I> Vector2Int = new Vector2IntEqualityComparer();
        public static readonly IEqualityComparer<Vector3I> Vector3Int = new Vector3IntEqualityComparer();
        public static readonly IEqualityComparer<Rect2I> RectInt = new RectIntEqualityComparer();

        static readonly RuntimeTypeHandle vector2IntType = typeof(Vector2I).TypeHandle;
        static readonly RuntimeTypeHandle vector3IntType = typeof(Vector3I).TypeHandle;
        static readonly RuntimeTypeHandle rectIntType = typeof(Rect2I).TypeHandle;

        static class Cache<T>
        {
            public static readonly IEqualityComparer<T> Comparer;

            static Cache()
            {
                var comparer = GetDefaultHelper(typeof(T));
                if (comparer == null)
                {
                    Comparer = EqualityComparer<T>.Default;
                }
                else
                {
                    Comparer = (IEqualityComparer<T>)comparer;
                }
            }
        }

        public static IEqualityComparer<T> GetDefault<T>()
        {
            return Cache<T>.Comparer;
        }

        static object GetDefaultHelper(Type type)
        {
            var t = type.TypeHandle;

            if (t.Equals(vector2Type)) return Vector2;
            if (t.Equals(vector3Type)) return Vector3;
            if (t.Equals(vector4Type)) return Vector4;
            if (t.Equals(colorType)) return Color;
            if (t.Equals(rectType)) return Rect;
            if (t.Equals(quaternionType)) return Quaternion;
            if (t.Equals(vector2IntType)) return Vector2Int;
            if (t.Equals(vector3IntType)) return Vector3Int;
            if (t.Equals(rectIntType)) return RectInt;

            return null;
        }

        sealed class Vector2EqualityComparer : IEqualityComparer<Vector2>
        {
            public bool Equals(Vector2 self, Vector2 vector)
            {
                return self.X.Equals(vector.X) && self.Y.Equals(vector.Y);
            }

            public int GetHashCode(Vector2 obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() << 2;
            }
        }

        sealed class Vector3EqualityComparer : IEqualityComparer<Vector3>
        {
            public bool Equals(Vector3 self, Vector3 vector)
            {
                return self.X.Equals(vector.X) && self.Y.Equals(vector.Y) && self.Z.Equals(vector.Z);
            }

            public int GetHashCode(Vector3 obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() << 2 ^ obj.Z.GetHashCode() >> 2;
            }
        }

        sealed class Vector4EqualityComparer : IEqualityComparer<Vector4>
        {
            public bool Equals(Vector4 self, Vector4 vector)
            {
                return self.X.Equals(vector.X) && self.Y.Equals(vector.Y) && self.Z.Equals(vector.Z) && self.W.Equals(vector.W);
            }

            public int GetHashCode(Vector4 obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() << 2 ^ obj.Z.GetHashCode() >> 2 ^ obj.W.GetHashCode() >> 1;
            }
        }

        sealed class ColorEqualityComparer : IEqualityComparer<Color>
        {
            public bool Equals(Color self, Color other)
            {
                return self.R.Equals(other.R) && self.G.Equals(other.G) && self.B.Equals(other.B) && self.A.Equals(other.A);
            }

            public int GetHashCode(Color obj)
            {
                return obj.R.GetHashCode() ^ obj.G.GetHashCode() << 2 ^ obj.B.GetHashCode() >> 2 ^ obj.A.GetHashCode() >> 1;
            }
        }

        sealed class RectEqualityComparer : IEqualityComparer<Rect2>
        {
            public bool Equals(Rect2 self, Rect2 other)
            {
                return self.Position.X.Equals(other.Position.X) && self.Size.X.Equals(other.Size.X) && 
                       self.Position.Y.Equals(other.Position.Y) && self.Size.Y.Equals(other.Size.Y);
            }

            public int GetHashCode(Rect2 obj)
            {
                return obj.Position.X.GetHashCode() ^ obj.Size.X.GetHashCode() << 2 ^ obj.Position.Y.GetHashCode() >> 2 ^ obj.Size.Y.GetHashCode() >> 1;
            }
        }

        sealed class QuaternionEqualityComparer : IEqualityComparer<Quaternion>
        {
            public bool Equals(Quaternion self, Quaternion vector)
            {
                return self.X.Equals(vector.X) && self.Y.Equals(vector.Y) && self.Z.Equals(vector.Z) && self.W.Equals(vector.W);
            }

            public int GetHashCode(Quaternion obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() << 2 ^ obj.Z.GetHashCode() >> 2 ^ obj.W.GetHashCode() >> 1;
            }
        }

        sealed class Vector2IntEqualityComparer : IEqualityComparer<Vector2I>
        {
            public bool Equals(Vector2I self, Vector2I vector)
            {
                return self.X.Equals(vector.X) && self.Y.Equals(vector.Y);
            }

            public int GetHashCode(Vector2I obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() << 2;
            }
        }

        sealed class Vector3IntEqualityComparer : IEqualityComparer<Vector3I>
        {
            public static readonly Vector3IntEqualityComparer Default = new Vector3IntEqualityComparer();

            public bool Equals(Vector3I self, Vector3I vector)
            {
                return self.X.Equals(vector.X) && self.Y.Equals(vector.Y) && self.Z.Equals(vector.Z);
            }

            public int GetHashCode(Vector3I obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() << 2 ^ obj.Z.GetHashCode() >> 2;
            }
        }
        
        sealed class RectIntEqualityComparer : IEqualityComparer<Rect2I>
        {
            public bool Equals(Rect2I self, Rect2I other)
            {
                return self.Position.X.Equals(other.Position.X) && self.Size.X.Equals(other.Size.X) && 
                       self.Position.Y.Equals(other.Position.Y) && self.Size.Y.Equals(other.Size.Y);
            }

            public int GetHashCode(Rect2I obj)
            {
                return obj.Position.X.GetHashCode() ^ obj.Size.X.GetHashCode() << 2 ^ obj.Position.Y.GetHashCode() >> 2 ^ obj.Size.Y.GetHashCode() >> 1;
            }
        }
    }
}
