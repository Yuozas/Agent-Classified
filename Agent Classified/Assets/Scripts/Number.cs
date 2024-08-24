using System;
using System.Linq.Expressions;

public static class Calculator<T>
{
    static Calculator()
    {
        Add = CreateDelegate<T>(Expression.AddChecked, "Addition", true);
        Subtract = CreateDelegate<T>(Expression.SubtractChecked, "Substraction", true);
        Multiply = CreateDelegate<T>(Expression.MultiplyChecked, "Multiply", true);
        Divide = CreateDelegate<T>(Expression.Divide, "Divide", true);
        Modulo = CreateDelegate<T>(Expression.Modulo, "Modulus", true);
        Negate = CreateDelegate(Expression.NegateChecked, "Negate", true);
        Plus = CreateDelegate(Expression.UnaryPlus, "Plus", true);
        Increment = CreateDelegate(Expression.Increment, "Increment", true);
        Decrement = CreateDelegate(Expression.Decrement, "Decrement", true);
        LeftShift = CreateDelegate<int>(Expression.LeftShift, "LeftShift", false);
        RightShift = CreateDelegate<int>(Expression.RightShift, "RightShift", false);
        OnesComplement = CreateDelegate(Expression.OnesComplement, "OnesComplement", false);
        And = CreateDelegate<T>(Expression.And, "BitwiseAnd", false);
        Or = CreateDelegate<T>(Expression.Or, "BitwiseOr", false);
        Xor = CreateDelegate<T>(Expression.ExclusiveOr, "ExclusiveOr", false);
    }

    static private Func<T, T2, T> CreateDelegate<T2>(Func<Expression, Expression, Expression> @operator, string operatorName, bool isChecked)
    {
        try
        {
            Type convertToTypeA = ConvertTo(typeof(T));
            Type convertToTypeB = ConvertTo(typeof(T2));
            ParameterExpression parameterA = Expression.Parameter(typeof(T), "a");
            ParameterExpression parameterB = Expression.Parameter(typeof(T2), "b");
            Expression valueA = (convertToTypeA != null) ? Expression.Convert(parameterA, convertToTypeA) : (Expression)parameterA;
            Expression valueB = (convertToTypeB != null) ? Expression.Convert(parameterB, convertToTypeB) : (Expression)parameterB;
            Expression body = @operator(valueA, valueB);
            if (convertToTypeA != null)
            {
                if (isChecked)
                    body = Expression.ConvertChecked(body, typeof(T));
                else
                    body = Expression.Convert(body, typeof(T));
            }
            return Expression.Lambda<Func<T, T2, T>>(body, parameterA, parameterB).Compile();
        }
        catch
        {
            return (a, b) =>
            {
                throw new InvalidOperationException("Operator " + operatorName + " is not supported by type " + typeof(T).FullName + ".");
            };
        }
    }

    static private Func<T, T> CreateDelegate(Func<Expression, Expression> @operator, string operatorName, bool isChecked)
    {
        try
        {
            Type convertToType = ConvertTo(typeof(T));
            ParameterExpression parameter = Expression.Parameter(typeof(T), "a");
            Expression value = (convertToType != null) ? Expression.Convert(parameter, convertToType) : (Expression)parameter;
            Expression body = @operator(value);
            if (convertToType != null)
            {
                if (isChecked)
                    body = Expression.ConvertChecked(body, typeof(T));
                else
                    body = Expression.Convert(body, typeof(T));
            }
            return Expression.Lambda<Func<T, T>>(body, parameter).Compile();
        }
        catch
        {
            return (a) =>
            {
                throw new InvalidOperationException("Operator " + operatorName + " is not supported by type " + typeof(T).FullName + ".");
            };
        }
    }

    static private Type ConvertTo(Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Char:
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
                return typeof(int);
        }
        return null;
    }

    /// <summary>
    /// Adds two values of the same type.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Add;

    /// <summary>
    /// Subtracts two values of the same type.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Subtract;

    /// <summary>
    /// Multiplies two values of the same type.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Multiply;

    /// <summary>
    /// Divides two values of the same type.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Divide;

    /// <summary>
    /// Divides two values of the same type and returns the remainder.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Modulo;

    /// <summary>
    /// Gets the negative value of T.
    /// Supported by: All numeric values, but will throw an OverflowException on unsigned values which are not 0.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T> Negate;

    /// <summary>
    /// Gets the negative value of T.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T> Plus;

    /// <summary>
    /// Gets the negative value of T.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T> Increment;

    /// <summary>
    /// Gets the negative value of T.
    /// Supported by: All numeric values.
    /// </summary>
    /// <exception cref="OverflowException"/>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T> Decrement;

    /// <summary>
    /// Shifts the number to the left.
    /// Supported by: All integral types.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, int, T> LeftShift;

    /// <summary>
    /// Shifts the number to the right.
    /// Supported by: All integral types.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, int, T> RightShift;

    /// <summary>
    /// Inverts all bits inside the value.
    /// Supported by: All integral types.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T> OnesComplement;

    /// <summary>
    /// Performs a bitwise OR.
    /// Supported by: All integral types.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Or;

    /// <summary>
    /// Performs a bitwise AND
    /// Supported by: All integral types.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> And;

    /// <summary>
    /// Performs a bitwise Exclusive OR.
    /// Supported by: All integral types.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public static readonly Func<T, T, T> Xor;
}

public struct Number<T>
where T : IComparable<T>, IEquatable<T>
{
    private readonly T _Value;

    public Number(T value)
    {
        _Value = value;
    }

    #region Comparison

    public bool Equals(Number<T> other)
    {
        return _Value.Equals(other._Value);
    }

    public bool Equals(T other)
    {
        return _Value.Equals(other);
    }

    public int CompareTo(Number<T> other)
    {
        return _Value.CompareTo(other._Value);
    }

    public int CompareTo(T other)
    {
        return _Value.CompareTo(other);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (obj is T)
            return _Value.Equals((T)obj);
        if (obj is Number<T>)
            return _Value.Equals(((Number<T>)obj)._Value);
        return false;
    }

    public override int GetHashCode()
    {
        return (_Value == null) ? 0 : _Value.GetHashCode();
    }

    static public bool operator ==(Number<T> a, Number<T> b)
    {
        return a._Value.Equals(b._Value);
    }

    static public bool operator !=(Number<T> a, Number<T> b)
    {
        return !a._Value.Equals(b._Value);
    }

    static public bool operator <(Number<T> a, Number<T> b)
    {
        return a._Value.CompareTo(b._Value) < 0;
    }

    static public bool operator <=(Number<T> a, Number<T> b)
    {
        return a._Value.CompareTo(b._Value) <= 0;
    }

    static public bool operator >(Number<T> a, Number<T> b)
    {
        return a._Value.CompareTo(b._Value) > 0;
    }

    static public bool operator >=(Number<T> a, Number<T> b)
    {
        return a._Value.CompareTo(b._Value) >= 0;
    }

    static public Number<T> operator !(Number<T> a)
    {
        return new Number<T>(Calculator<T>.Negate(a._Value));
    }

    #endregion Comparison

    #region Arithmatic operations

    static public Number<T> operator +(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Add(a._Value, b._Value));
    }

    static public Number<T> operator -(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Subtract(a._Value, b._Value));
    }

    static public Number<T> operator *(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Multiply(a._Value, b._Value));
    }

    static public Number<T> operator /(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Divide(a._Value, b._Value));
    }

    static public Number<T> operator %(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Modulo(a._Value, b._Value));
    }

    static public Number<T> operator -(Number<T> a)
    {
        return new Number<T>(Calculator<T>.Negate(a._Value));
    }

    static public Number<T> operator +(Number<T> a)
    {
        return new Number<T>(Calculator<T>.Plus(a._Value));
    }

    static public Number<T> operator ++(Number<T> a)
    {
        return new Number<T>(Calculator<T>.Increment(a._Value));
    }

    static public Number<T> operator --(Number<T> a)
    {
        return new Number<T>(Calculator<T>.Decrement(a._Value));
    }

    #endregion Arithmatic operations

    #region Bitwise operations

    static public Number<T> operator <<(Number<T> a, int b)
    {
        return new Number<T>(Calculator<T>.LeftShift(a._Value, b));
    }

    static public Number<T> operator >>(Number<T> a, int b)
    {
        return new Number<T>(Calculator<T>.RightShift(a._Value, b));
    }

    static public Number<T> operator &(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.And(a._Value, b._Value));
    }

    static public Number<T> operator |(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Or(a._Value, b._Value));
    }

    static public Number<T> operator ^(Number<T> a, Number<T> b)
    {
        return new Number<T>(Calculator<T>.Xor(a._Value, b._Value));
    }

    static public Number<T> operator ~(Number<T> a)
    {
        return new Number<T>(Calculator<T>.OnesComplement(a._Value));
    }

    #endregion Bitwise operations

    #region Casts

    static public implicit operator Number<T>(T value)
    {
        return new Number<T>(value);
    }

    static public explicit operator T(Number<T> value)
    {
        return value._Value;
    }

    #endregion Casts

    #region Other members

    public override string ToString()
    {
        return (_Value == null) ? string.Empty : _Value.ToString();
    }

    #endregion Other members
}