using System;
using System.Linq.Expressions;
using UnityEngine;

namespace Addon
{
    public struct Range<T> where T : IEquatable<T>, IComparable<T>
    {

        public T min;
        public T max;
        public Number<T> Min => Number(min);
        public Number<T> Max => Number(max);
        public Number<T> Number(T number) => new Number<T>(number);

        public Range(T min, T max)
        {
            this.max = max;
            this.min = min;
        }
        public bool InRange(T value) => Number(value) >= min && Number(value) <= max;
        public bool Equals(Range<T> other) => Number(other.min) == min && Number(other.max) == max;
        public Range<T> Flipped() => new Range<T>(max, min);
        public void Flip()
        {
            T tempMin = min;
            min = max;
            max = tempMin;
        }
    }
    #region Same method since unit doesnt accept generic serialization
    [Serializable]
    public struct RangeInt
    {

        public int min;
        public int max;

        public RangeInt(int min, int max)
        {
            this.max = max;
            this.min = min;
        }
        public bool InRange(int value) => value >= min && value <= max;
        public bool Equals(RangeInt other) => other.min == min && other.max == max;
        public RangeInt Flipped() => new RangeInt(max, min);
        public void Flip()
        {
            int tempMin = min;
            min = max;
            max = tempMin;
        }
    }
    [Serializable]
    public struct RangeFloat
    {

        public float min;
        public float max;

        public RangeFloat(float min, float max)
        {
            this.max = max;
            this.min = min;
        }
        public bool InRange(float value) => value >= min && value <= max;
        public bool Equals(RangeFloat other) => other.min == min && other.max == max;
        public RangeFloat Flipped() => new RangeFloat(max, min);
        public void Flip()
        {
            float tempMin = min;
            min = max;
            max = tempMin;
        }
    }
    #endregion
    [Serializable]
    public struct RangeVector2 : IEquatable<RangeVector2>

    {
        public Vector2 min;
        public Vector2 max;

        public RangeVector2(Vector2 min, Vector2 max)
        {
            this.max = max;
            this.min = min;
        }
        public bool InRange(Vector2 value) => (value.x >= min.x && value.x <= max.x) && (value.y >= min.y && value.y <= max.y);
        public bool Equals(RangeVector2 other) => other.min == min && other.max == max;
        public RangeVector2 Flipped() => new RangeVector2(max, min);
        public void Flip()
        {
            Vector2 tempMin = min;
            min = max;
            max = tempMin;
        }
    }
    [Serializable]
    public struct RangeVector3 : IEquatable<RangeVector3>

    {
        public Vector3 min;
        public Vector3 max;

        public RangeVector3(Vector3 min, Vector3 max)
        {
            this.max = max;
            this.min = min;
        }
        public bool InRange(Vector3 value) => ((value.x >= min.x && value.x <= max.x) && (value.y >= min.y && value.y <= max.y)) && (value.z >= min.z && value.z <= max.z);
        public bool Equals(RangeVector3 other) => other.min == min && other.max == max;
        public RangeVector3 Flipped() => new RangeVector3(max, min);
        public void Flip()
        {
            Vector3 tempMin = min;
            min = max;
            max = tempMin;
        }

    }

}