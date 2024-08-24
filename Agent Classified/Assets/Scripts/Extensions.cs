using UnityEngine;

public static class Extensions
{
    /* idea
    public static ref referencableType SetGet(this ref referencableType r, referencableType to)
    {
        r = to;
        return ref r;
    }
    */
    public static T RandomElement<T>(this T[] array) => array[Random.Range(0, array.Length)];

    #region Numbers
    public static void SetFalse(this ref bool boolean) => boolean = false;

    public static void SetTrue(this ref bool boolean) => boolean = true;

    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

    public static bool IsNull(this GameObject obj) => obj == null;

    public static bool NotNull(this GameObject obj) => obj != null;

    public static bool IsZero(this float number) => number == 0;

    public static bool IsZero(this int number) => number == 0;

    public static bool IsZero(this double number) => number == 0;

    public static bool IsZero(this decimal number) => number == 0;

    public static bool NotZero(this float number) => number != 0;

    public static bool NotZero(this int number) => number != 0;

    public static bool NotZero(this double number) => number != 0;

    public static bool NotZero(this decimal number) => number != 0;

    public static bool Modulus(this int to, int from, int result) => to % from == result;
    public static int GetModulus(this int to, int from) => to % from;
    public static void Round(this ref float f, int decimalAmount) => f = (float)System.Math.Round(f, decimalAmount);
    public static void Round(this ref double d, int decimalAmount) => d = System.Math.Round(d, decimalAmount);
    public static float GetRound(this float f, int decimalAmount) => (float)System.Math.Round(f, decimalAmount);
    public static double GetRound(this double d, int decimalAmount) => System.Math.Round(d, decimalAmount);
    #endregion
    #region Vector
    public static Vector3 RandomPointInBounds(this Bounds bounds)
    {
        return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
    }
    public static Vector3 Multiply(this Vector3 vector3, float multiplyBy) => vector3 * multiplyBy;

    public static Vector3 Multiply(this Vector3 vector3, float multiplyBy, int decimalAmount)
    {
        return new Vector3(
            (float)System.Math.Round(vector3.x * multiplyBy, decimalAmount),
            (float)System.Math.Round(vector3.y * multiplyBy, decimalAmount),
            (float)System.Math.Round(vector3.z * multiplyBy, decimalAmount)
            );
    }

    public static Vector3 Divide(this Vector3 vector3, float multiplyBy) => vector3 / multiplyBy;

    public static Vector3 Divide(this Vector3 vector3, float multiplyBy, int decimalAmount)
    {
        return new Vector3(
            (float)System.Math.Round(vector3.x / multiplyBy, decimalAmount),
            (float)System.Math.Round(vector3.y / multiplyBy, decimalAmount),
            (float)System.Math.Round(vector3.z / multiplyBy, decimalAmount)
            );
    }

    public static Vector3 Round(this Vector3 vector3, int decimalAmount)
    {
        return new Vector3(
            (float)System.Math.Round(vector3.x, decimalAmount),
            (float)System.Math.Round(vector3.y, decimalAmount),
            (float)System.Math.Round(vector3.z, decimalAmount)
            );
    }

    public static Vector3 Amend(this Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
        vector3.x = x ?? vector3.x;
        vector3.y = y ?? vector3.y;
        vector3.z = z ?? vector3.z;
        return vector3;
    }
    public static Vector3 Vector3(this Vector2 vector, float z = 0) => new Vector3(vector.x, vector.y, z);
    public static void AmendSelf(this ref Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
        vector3.x = x ?? vector3.x;
        vector3.y = y ?? vector3.y;
        vector3.z = z ?? vector3.z;
    }
    public static ref Vector3 AmendGetSelf(this ref Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
        vector3.x = x ?? vector3.x;
        vector3.y = y ?? vector3.y;
        vector3.z = z ?? vector3.z;
        return ref vector3;
    }
    public static void Velocity(this Rigidbody2D rb) => rb.velocity = UnityEngine.Vector2.zero;
    public static void Velocity(this Rigidbody2D rb, Vector2 vector2) => rb.velocity = vector2 * Time.deltaTime;
    public static void Velocity(this Rigidbody2D rb, Vector3 vector3) => rb.velocity = vector3 * Time.deltaTime;
    public static void VelocityPrepareWithForce(this ref Vector2 vector2, float x, float y,float force)
    {
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        vector2 *= force;
    }
    public static void VelocityPrepareWithForce(this ref Vector2 vector2, float force)
    {
        vector2.Normalize();
        vector2 *= force;
    }
    public static void VelocityPrepareWithForce(this ref Vector3 vector3, float force)
    {
        vector3.Normalize();
        vector3 *= force;
    }
    public static void VelocityPrepareWithForce(this ref Vector2 vector2, Vector2 to, float force) => vector2 = to.normalized * force;
    public static void VelocityPrepareWithForce(this ref Vector3 vector3, Vector3 to, float force) => vector3 = to.normalized * force;
    public static ref Vector2 VelocityPrepareGetWithForce(this ref Vector2 vector2, float force)
    {
        vector2.Normalize();
        vector2 *= force;
        return ref vector2;
    }
    public static ref Vector3 VelocityPrepareGetWithForce(this ref Vector3 vector3, float force)
    {
        vector3.Normalize();
        vector3 *= force;
        return ref vector3;
    }
    public static ref Vector2 VelocityPrepareGetWithForce(this ref Vector2 vector2, Vector2 to, float force)
    {   
        vector2 = to.normalized * force;
        return ref vector2;
    }
    public static ref Vector3 VelocityPrepareGetWithForce(this ref Vector3 vector3, Vector3 to, float force)
    {
        vector3 = to.normalized * force;
        return ref vector3;
    }
    public static void Position(this Transform transform, Vector2 vector2) => transform.position = vector2;
    public static void Position(this Transform transform, Vector3 vector3) => transform.position = vector3;
    public static void Scale(this Transform transform, Vector2 vector2) => transform.localScale = vector2;
    public static void Scale(this Transform transform, Vector3 vector3) => transform.localScale = vector3;
    public static void RectScale(this RectTransform transform, Vector2 vector2) => transform.localScale = vector2;
    public static void RectScale(this RectTransform transform, Vector3 vector3) => transform.localScale = vector3;
    public static Vector2 GetRandomDirection(float xMin = -1f, float xMax = 1f, float yMin = -1f, float yMax = 1f, float force = 1f) => new Vector2(Random.Range(xMin * force, xMax * force), Random.Range(yMin * force, yMax * force));
    public static void RandomDirection(this ref Vector2 vector2, float xMin = -1f, float xMax = 1f, float yMin = -1f, float yMax = 1f, float force = 1f) => vector2 = new Vector2(Random.Range(xMin * force, xMax * force), Random.Range(yMin * force, yMax * force));
    public static Vector2 GetDirection(this Vector2 from, Vector2 to) => (from - to).normalized;
    public static void SetDirection(this ref Vector2 from, Vector2 to) => from = (from - to).normalized;
    public static Vector3 GetDirection(this Vector3 from, Vector3 to) => (from - to).normalized;
    public static void SetDirection(this ref Vector3 from, Vector3 to) => from = (from - to).normalized;
    public static Vector2 GetDirection(this Vector3 from, Vector2 to) => from.Amend(x: from.x - to.x, y: from.y - to.y).normalized;
    public static void SetDirection(this ref Vector3 from, Vector2 to)
    {
        from.AmendGetSelf(x: from.x - to.x, y: from.y - to.y).Normalize();
    }
    public static Vector2 MoveTowards(this Vector2 from, Vector2 towards, float step = 1) => towards.GetDirection(from) * step + from;
    public static Vector3 MoveTowards(this Vector3 from, Vector3 towards, float step = 1) => towards.GetDirection(from) * step + from;
    public static Vector2 Vector2(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

    public static void MultiplySelf(this ref Vector2 vector2, float multiplyBy) => vector2 *= multiplyBy;

    public static Vector2 Multiply(this Vector2 vector2, float multiplyBy) => vector2 * multiplyBy;

    public static Vector2 Multiply(this Vector2 vector2, float multiplyBy, int decimalAmount)
    {
        return new Vector2(
            (float)System.Math.Round(vector2.x * multiplyBy, decimalAmount),
            (float)System.Math.Round(vector2.y * multiplyBy, decimalAmount)
            );
    }

    public static Vector2 Divide(this Vector2 vector2, float multiplyBy) => vector2 / multiplyBy;

    public static Vector2 Divide(this Vector2 vector2, float multiplyBy, int decimalAmount)
    {
        return new Vector2(
            (float)System.Math.Round(vector2.x / multiplyBy, decimalAmount),
            (float)System.Math.Round(vector2.y / multiplyBy, decimalAmount)
            );
    }

    public static Vector2 Round(this Vector2 vector2, int decimalAmount)
    {
        return new Vector2(
            (float)System.Math.Round(vector2.x, decimalAmount),
            (float)System.Math.Round(vector2.y, decimalAmount)
            );
    }

    public static Vector2 Amend(this Vector2 vector2, float? x = null, float? y = null)
    {
        vector2.x = x ?? vector2.x;
        vector2.y = y ?? vector2.y;
        return vector2;
    }

    public static void AmendSelf(this ref Vector2 vector2, float? x = null, float? y = null)
    {
        vector2.x = x ?? vector2.x;
        vector2.y = y ?? vector2.y;
    }
    public static ref Vector2 AmendGetSelf(this ref Vector2 vector2, float? x = null, float? y = null)
    {
        vector2.x = x ?? vector2.x;
        vector2.y = y ?? vector2.y;
        return ref vector2;
    }
    #endregion
    public static float RandomRange(this Addon.Range<float> range) => Random.Range(range.min, range.max);
    public static int RandomRange(this Addon.Range<int> range) => Random.Range(range.min, range.max);
    public static float RandomRange(this Addon.RangeFloat range) => Random.Range(range.min, range.max);
    public static int RandomRange(this Addon.RangeInt range) => Random.Range(range.min, range.max);

    public static GameObject SetGetActive(this GameObject gameObject)
    {
        gameObject.SetActive(true);
        return gameObject;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Type Safety", "UNT0014:Invalid type for call to GetComponent", Justification = "<Pending>")]
    public static bool TryGetComponent<T>(this Transform from, out T component)
    {
        component = from.GetComponent<T>();
        return component != null;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Type Safety", "UNT0014:Invalid type for call to GetComponent", Justification = "<Pending>")]
    public static bool TryGetComponent<T>(this GameObject from, out T component)
    {
        component = from.GetComponent<T>();
        return component != null;
    }
}
