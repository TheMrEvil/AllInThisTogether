using System;
using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	/// <summary>Represents a vector with three  single-precision floating-point values.</summary>
	// Token: 0x0200000B RID: 11
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		/// <summary>Gets a vector whose 3 elements are equal to zero.</summary>
		/// <returns>A vector whose three elements are equal to zero (that is, it returns the vector <c>(0,0,0)</c>.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000094C4 File Offset: 0x000076C4
		public static Vector3 Zero
		{
			get
			{
				return default(Vector3);
			}
		}

		/// <summary>Gets a vector whose 3 elements are equal to one.</summary>
		/// <returns>A vector whose three elements are equal to one (that is, it returns the vector <c>(1,1,1)</c>.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000094DA File Offset: 0x000076DA
		public static Vector3 One
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		/// <summary>Gets the vector (1,0,0).</summary>
		/// <returns>The vector <c>(1,0,0)</c>.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000094F0 File Offset: 0x000076F0
		public static Vector3 UnitX
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		/// <summary>Gets the vector (0,1,0).</summary>
		/// <returns>The vector <c>(0,1,0)</c>.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00009506 File Offset: 0x00007706
		public static Vector3 UnitY
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		/// <summary>Gets the vector (0,0,1).</summary>
		/// <returns>The vector <c>(0,0,1)</c>.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000951C File Offset: 0x0000771C
		public static Vector3 UnitZ
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		// Token: 0x060000D2 RID: 210 RVA: 0x00009532 File Offset: 0x00007732
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode()), this.Z.GetHashCode());
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		// Token: 0x060000D3 RID: 211 RVA: 0x0000955F File Offset: 0x0000775F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Vector3 && this.Equals((Vector3)obj);
		}

		/// <summary>Returns the string representation of the current instance using default formatting.</summary>
		/// <returns>The string representation of the current instance.</returns>
		// Token: 0x060000D4 RID: 212 RVA: 0x00009577 File Offset: 0x00007777
		public override string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <returns>The string representation of the current instance.</returns>
		// Token: 0x060000D5 RID: 213 RVA: 0x00009589 File Offset: 0x00007789
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance.</returns>
		// Token: 0x060000D6 RID: 214 RVA: 0x00009598 File Offset: 0x00007798
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(((IFormattable)this.X).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)this.Y).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)this.Z).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		/// <summary>Returns the length of this vector object.</summary>
		/// <returns>The vector's length.</returns>
		// Token: 0x060000D7 RID: 215 RVA: 0x0000963C File Offset: 0x0000783C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Vector3.Dot(this, this));
			}
			return MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
		}

		/// <summary>Returns the length of the vector squared.</summary>
		/// <returns>The vector's length squared.</returns>
		// Token: 0x060000D8 RID: 216 RVA: 0x00009698 File Offset: 0x00007898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(this, this);
			}
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		/// <summary>Computes the Euclidean distance between the two given points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance.</returns>
		// Token: 0x060000D9 RID: 217 RVA: 0x000096E8 File Offset: 0x000078E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return MathF.Sqrt(Vector3.Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return MathF.Sqrt(num * num + num2 * num2 + num3 * num3);
		}

		/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance squared.</returns>
		// Token: 0x060000DA RID: 218 RVA: 0x00009748 File Offset: 0x00007948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return Vector3.Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return num * num + num2 * num2 + num3 * num3;
		}

		/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
		/// <param name="value">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		// Token: 0x060000DB RID: 219 RVA: 0x000097A0 File Offset: 0x000079A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float value2 = value.Length();
				return value / value2;
			}
			float num = MathF.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
			return new Vector3(value.X / num, value.Y / num, value.Z / num);
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The cross product.</returns>
		// Token: 0x060000DC RID: 220 RVA: 0x00009810 File Offset: 0x00007A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			return new Vector3(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);
		}

		/// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of the surface being reflected off.</param>
		/// <returns>The reflected vector.</returns>
		// Token: 0x060000DD RID: 221 RVA: 0x00009874 File Offset: 0x00007A74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float right = Vector3.Dot(vector, normal);
				Vector3 right2 = normal * right * 2f;
				return vector - right2;
			}
			float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			float num2 = normal.X * num * 2f;
			float num3 = normal.Y * num * 2f;
			float num4 = normal.Z * num * 2f;
			return new Vector3(vector.X - num2, vector.Y - num3, vector.Z - num4);
		}

		/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
		/// <param name="value1">The vector to restrict.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The restricted vector.</returns>
		// Token: 0x060000DE RID: 222 RVA: 0x00009924 File Offset: 0x00007B24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			float num3 = value1.Z;
			num3 = ((num3 > max.Z) ? max.Z : num3);
			num3 = ((num3 < min.Z) ? min.Z : num3);
			return new Vector3(num, num2, num3);
		}

		/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
		/// <returns>The interpolated vector.</returns>
		// Token: 0x060000DF RID: 223 RVA: 0x000099C0 File Offset: 0x00007BC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 left = value1 * (1f - amount);
				Vector3 right = value2 * amount;
				return left + right;
			}
			return new Vector3(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount);
		}

		/// <summary>Transforms a vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		// Token: 0x060000E0 RID: 224 RVA: 0x00009A38 File Offset: 0x00007C38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector3(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
		}

		/// <summary>Transforms a vector normal by the given 4x4 matrix.</summary>
		/// <param name="normal">The source vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The transformed vector.</returns>
		// Token: 0x060000E1 RID: 225 RVA: 0x00009ADC File Offset: 0x00007CDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
		{
			return new Vector3(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
		}

		/// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		// Token: 0x060000E2 RID: 226 RVA: 0x00009B6C File Offset: 0x00007D6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector3(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		// Token: 0x060000E3 RID: 227 RVA: 0x00009C83 File Offset: 0x00007E83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Add(Vector3 left, Vector3 right)
		{
			return left + right;
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The difference vector.</returns>
		// Token: 0x060000E4 RID: 228 RVA: 0x00009C8C File Offset: 0x00007E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Subtract(Vector3 left, Vector3 right)
		{
			return left - right;
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		// Token: 0x060000E5 RID: 229 RVA: 0x00009C95 File Offset: 0x00007E95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, Vector3 right)
		{
			return left * right;
		}

		/// <summary>Multiplies a vector by a specified scalar.</summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		// Token: 0x060000E6 RID: 230 RVA: 0x00009C9E File Offset: 0x00007E9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, float right)
		{
			return left * right;
		}

		/// <summary>Multiplies a scalar value by a specified vector.</summary>
		/// <param name="left">The scaled value.</param>
		/// <param name="right">The vector.</param>
		/// <returns>The scaled vector.</returns>
		// Token: 0x060000E7 RID: 231 RVA: 0x00009CA7 File Offset: 0x00007EA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(float left, Vector3 right)
		{
			return left * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		// Token: 0x060000E8 RID: 232 RVA: 0x00009CB0 File Offset: 0x00007EB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, Vector3 right)
		{
			return left / right;
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="divisor">The scalar value.</param>
		/// <returns>The vector that results from the division.</returns>
		// Token: 0x060000E9 RID: 233 RVA: 0x00009CB9 File Offset: 0x00007EB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, float divisor)
		{
			return left / divisor;
		}

		/// <summary>Negates a specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		// Token: 0x060000EA RID: 234 RVA: 0x00009CC2 File Offset: 0x00007EC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Negate(Vector3 value)
		{
			return -value;
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Vector3" /> object whose three elements have the same value.</summary>
		/// <param name="value">The value to assign to all three elements.</param>
		// Token: 0x060000EB RID: 235 RVA: 0x00009CCA File Offset: 0x00007ECA
		[Intrinsic]
		public Vector3(float value)
		{
			this = new Vector3(value, value, value);
		}

		/// <summary>Creates a   new <see cref="T:System.Numerics.Vector3" /> object from the specified <see cref="T:System.Numerics.Vector2" /> object and the specified value.</summary>
		/// <param name="value">The vector with two elements.</param>
		/// <param name="z">The additional value to assign to the <see cref="F:System.Numerics.Vector3.Z" /> field.</param>
		// Token: 0x060000EC RID: 236 RVA: 0x00009CD5 File Offset: 0x00007ED5
		public Vector3(Vector2 value, float z)
		{
			this = new Vector3(value.X, value.Y, z);
		}

		/// <summary>Creates a vector whose elements have the specified values.</summary>
		/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector3.X" /> field.</param>
		/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector3.Y" /> field.</param>
		/// <param name="z">The value to assign to the <see cref="F:System.Numerics.Vector3.Z" /> field.</param>
		// Token: 0x060000ED RID: 237 RVA: 0x00009CEA File Offset: 0x00007EEA
		[Intrinsic]
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>Copies the elements of the vector to a specified array.</summary>
		/// <param name="array">The destination array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060000EE RID: 238 RVA: 0x00009D01 File Offset: 0x00007F01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
		/// <param name="array">The destination array.</param>
		/// <param name="index">The index at which to copy the first element of the vector.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the array length.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		// Token: 0x060000EF RID: 239 RVA: 0x00009D0C File Offset: 0x00007F0C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException("The method was called with a null array argument.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.Format("Index was out of bounds:", index));
			}
			if (array.Length - index < 3)
			{
				throw new ArgumentException(SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
			array[index + 2] = this.Z;
		}

		/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
		/// <param name="other">The other vector.</param>
		/// <returns>
		///   <see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000F0 RID: 240 RVA: 0x00009D89 File Offset: 0x00007F89
		[Intrinsic]
		public bool Equals(Vector3 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		/// <summary>Returns the dot product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The dot product.</returns>
		// Token: 0x060000F1 RID: 241 RVA: 0x00009DB7 File Offset: 0x00007FB7
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The minimized vector.</returns>
		// Token: 0x060000F2 RID: 242 RVA: 0x00009DE4 File Offset: 0x00007FE4
		[Intrinsic]
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z);
		}

		/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The maximized vector.</returns>
		// Token: 0x060000F3 RID: 243 RVA: 0x00009E4C File Offset: 0x0000804C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z);
		}

		/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value vector.</returns>
		// Token: 0x060000F4 RID: 244 RVA: 0x00009EB2 File Offset: 0x000080B2
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Abs(Vector3 value)
		{
			return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
		}

		/// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The square root vector.</returns>
		// Token: 0x060000F5 RID: 245 RVA: 0x00009EDA File Offset: 0x000080DA
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SquareRoot(Vector3 value)
		{
			return new Vector3(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		// Token: 0x060000F6 RID: 246 RVA: 0x00009F02 File Offset: 0x00008102
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		// Token: 0x060000F7 RID: 247 RVA: 0x00009F30 File Offset: 0x00008130
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		// Token: 0x060000F8 RID: 248 RVA: 0x00009F5E File Offset: 0x0000815E
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		/// <summary>Multiples the specified vector by the specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		// Token: 0x060000F9 RID: 249 RVA: 0x00009F8C File Offset: 0x0000818C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 left, float right)
		{
			return left * new Vector3(right);
		}

		/// <summary>Multiples the scalar value by the specified vector.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		// Token: 0x060000FA RID: 250 RVA: 0x00009F9A File Offset: 0x0000819A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float left, Vector3 right)
		{
			return new Vector3(left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
		// Token: 0x060000FB RID: 251 RVA: 0x00009FA8 File Offset: 0x000081A8
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="value1">The vector.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		// Token: 0x060000FC RID: 252 RVA: 0x00009FD6 File Offset: 0x000081D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 value1, float value2)
		{
			return value1 / new Vector3(value2);
		}

		/// <summary>Negates the specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		// Token: 0x060000FD RID: 253 RVA: 0x00009FE4 File Offset: 0x000081E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 value)
		{
			return Vector3.Zero - value;
		}

		/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000FE RID: 254 RVA: 0x00009D89 File Offset: 0x00007F89
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000FF RID: 255 RVA: 0x00009FF1 File Offset: 0x000081F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 left, Vector3 right)
		{
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		/// <summary>The X component of the vector.</summary>
		// Token: 0x04000064 RID: 100
		public float X;

		/// <summary>The Y component of the vector.</summary>
		// Token: 0x04000065 RID: 101
		public float Y;

		/// <summary>The Z component of the vector.</summary>
		// Token: 0x04000066 RID: 102
		public float Z;
	}
}
