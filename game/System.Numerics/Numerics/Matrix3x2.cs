using System;
using System.Globalization;

namespace System.Numerics
{
	/// <summary>Represents a 3x2 matrix.</summary>
	// Token: 0x02000004 RID: 4
	public struct Matrix3x2 : IEquatable<Matrix3x2>
	{
		/// <summary>Gets the multiplicative identity matrix.</summary>
		/// <returns>The multiplicative identify matrix.</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020B9 File Offset: 0x000002B9
		public static Matrix3x2 Identity
		{
			get
			{
				return Matrix3x2._identity;
			}
		}

		/// <summary>Indicates whether the current matrix is the identity matrix.</summary>
		/// <returns>
		///   <see langword="true" /> if the current matrix is the identity matrix; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C0 File Offset: 0x000002C0
		public bool IsIdentity
		{
			get
			{
				return this.M11 == 1f && this.M22 == 1f && this.M12 == 0f && this.M21 == 0f && this.M31 == 0f && this.M32 == 0f;
			}
		}

		/// <summary>Gets or sets the translation component of this matrix.</summary>
		/// <returns>The translation component of the current instance.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000211D File Offset: 0x0000031D
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002130 File Offset: 0x00000330
		public Vector2 Translation
		{
			get
			{
				return new Vector2(this.M31, this.M32);
			}
			set
			{
				this.M31 = value.X;
				this.M32 = value.Y;
			}
		}

		/// <summary>Creates a 3x2 matrix from the specified components.</summary>
		/// <param name="m11">The value to assign to the first element in the first row.</param>
		/// <param name="m12">The value to assign to the second element in the first row.</param>
		/// <param name="m21">The value to assign to the first element in the second row.</param>
		/// <param name="m22">The value to assign to the second element in the second row.</param>
		/// <param name="m31">The value to assign to the first element in the third row.</param>
		/// <param name="m32">The value to assign to the second element in the third row.</param>
		// Token: 0x0600000F RID: 15 RVA: 0x0000214A File Offset: 0x0000034A
		public Matrix3x2(float m11, float m12, float m21, float m22, float m31, float m32)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M21 = m21;
			this.M22 = m22;
			this.M31 = m31;
			this.M32 = m32;
		}

		/// <summary>Creates a translation matrix from the specified 2-dimensional vector.</summary>
		/// <param name="position">The translation position.</param>
		/// <returns>The translation matrix.</returns>
		// Token: 0x06000010 RID: 16 RVA: 0x0000217C File Offset: 0x0000037C
		public static Matrix3x2 CreateTranslation(Vector2 position)
		{
			Matrix3x2 result;
			result.M11 = 1f;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M31 = position.X;
			result.M32 = position.Y;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified X and Y components.</summary>
		/// <param name="xPosition">The X position.</param>
		/// <param name="yPosition">The Y position.</param>
		/// <returns>The translation matrix.</returns>
		// Token: 0x06000011 RID: 17 RVA: 0x000021D4 File Offset: 0x000003D4
		public static Matrix3x2 CreateTranslation(float xPosition, float yPosition)
		{
			Matrix3x2 result;
			result.M11 = 1f;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M31 = xPosition;
			result.M32 = yPosition;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified X and Y components.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <returns>The scaling matrix.</returns>
		// Token: 0x06000012 RID: 18 RVA: 0x00002224 File Offset: 0x00000424
		public static Matrix3x2 CreateScale(float xScale, float yScale)
		{
			Matrix3x2 result;
			result.M11 = xScale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix that is offset by a given center point.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		// Token: 0x06000013 RID: 19 RVA: 0x00002274 File Offset: 0x00000474
		public static Matrix3x2 CreateScale(float xScale, float yScale, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			Matrix3x2 result;
			result.M11 = xScale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		// Token: 0x06000014 RID: 20 RVA: 0x000022D8 File Offset: 0x000004D8
		public static Matrix3x2 CreateScale(Vector2 scales)
		{
			Matrix3x2 result;
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale with an offset from the specified center point.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <param name="centerPoint">The center offset.</param>
		/// <returns>The scaling matrix.</returns>
		// Token: 0x06000015 RID: 21 RVA: 0x00002330 File Offset: 0x00000530
		public static Matrix3x2 CreateScale(Vector2 scales, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			Matrix3x2 result;
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a scaling matrix that scales uniformly with the given scale.</summary>
		/// <param name="scale">The uniform scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		// Token: 0x06000016 RID: 22 RVA: 0x000023A8 File Offset: 0x000005A8
		public static Matrix3x2 CreateScale(float scale)
		{
			Matrix3x2 result;
			result.M11 = scale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix that scales uniformly with the specified scale with an offset from the specified center.</summary>
		/// <param name="scale">The uniform scale to use.</param>
		/// <param name="centerPoint">The center offset.</param>
		/// <returns>The scaling matrix.</returns>
		// Token: 0x06000017 RID: 23 RVA: 0x000023F8 File Offset: 0x000005F8
		public static Matrix3x2 CreateScale(float scale, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			Matrix3x2 result;
			result.M11 = scale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a skew matrix from the specified angles in radians.</summary>
		/// <param name="radiansX">The X angle, in radians.</param>
		/// <param name="radiansY">The Y angle, in radians.</param>
		/// <returns>The skew matrix.</returns>
		// Token: 0x06000018 RID: 24 RVA: 0x0000245C File Offset: 0x0000065C
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY)
		{
			float m = MathF.Tan(radiansX);
			float m2 = MathF.Tan(radiansY);
			Matrix3x2 result;
			result.M11 = 1f;
			result.M12 = m2;
			result.M21 = m;
			result.M22 = 1f;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a skew matrix from the specified angles in radians and a center point.</summary>
		/// <param name="radiansX">The X angle, in radians.</param>
		/// <param name="radiansY">The Y angle, in radians.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The skew matrix.</returns>
		// Token: 0x06000019 RID: 25 RVA: 0x000024B8 File Offset: 0x000006B8
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY, Vector2 centerPoint)
		{
			float num = MathF.Tan(radiansX);
			float num2 = MathF.Tan(radiansY);
			float m = -centerPoint.Y * num;
			float m2 = -centerPoint.X * num2;
			Matrix3x2 result;
			result.M11 = 1f;
			result.M12 = num2;
			result.M21 = num;
			result.M22 = 1f;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a rotation matrix using the given rotation in radians.</summary>
		/// <param name="radians">The amount of rotation, in radians.</param>
		/// <returns>The rotation matrix.</returns>
		// Token: 0x0600001A RID: 26 RVA: 0x00002524 File Offset: 0x00000724
		public static Matrix3x2 CreateRotation(float radians)
		{
			radians = MathF.IEEERemainder(radians, 6.2831855f);
			float num;
			float num2;
			if (radians > -1.7453294E-05f && radians < 1.7453294E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.5708138f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.1415753f || radians > 3.1415753f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.5708138f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			Matrix3x2 result;
			result.M11 = num;
			result.M12 = num2;
			result.M21 = -num2;
			result.M22 = num;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a rotation matrix using the specified rotation in radians and a center point.</summary>
		/// <param name="radians">The amount of rotation, in radians.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		// Token: 0x0600001B RID: 27 RVA: 0x00002600 File Offset: 0x00000800
		public static Matrix3x2 CreateRotation(float radians, Vector2 centerPoint)
		{
			radians = MathF.IEEERemainder(radians, 6.2831855f);
			float num;
			float num2;
			if (radians > -1.7453294E-05f && radians < 1.7453294E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.5708138f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.1415753f || radians > 3.1415753f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.5708138f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			Matrix3x2 result;
			result.M11 = num;
			result.M12 = num2;
			result.M21 = -num2;
			result.M22 = num;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Calculates the determinant for this matrix.</summary>
		/// <returns>The determinant.</returns>
		// Token: 0x0600001C RID: 28 RVA: 0x00002704 File Offset: 0x00000904
		public float GetDeterminant()
		{
			return this.M11 * this.M22 - this.M21 * this.M12;
		}

		/// <summary>Inverts the specified matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <param name="result">When this method returns, contains the inverted matrix if the operation succeeded.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="matrix" /> was converted successfully; otherwise,  <see langword="false" />.</returns>
		// Token: 0x0600001D RID: 29 RVA: 0x00002724 File Offset: 0x00000924
		public static bool Invert(Matrix3x2 matrix, out Matrix3x2 result)
		{
			float num = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;
			if (MathF.Abs(num) < 1E-45f)
			{
				result = new Matrix3x2(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num2 = 1f / num;
			result.M11 = matrix.M22 * num2;
			result.M12 = -matrix.M12 * num2;
			result.M21 = -matrix.M21 * num2;
			result.M22 = matrix.M11 * num2;
			result.M31 = (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * num2;
			result.M32 = (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * num2;
			return true;
		}

		/// <summary>Performs a linear interpolation from one matrix to a second matrix based on a value that specifies the weighting of the second matrix.</summary>
		/// <param name="matrix1">The first matrix.</param>
		/// <param name="matrix2">The second matrix.</param>
		/// <param name="amount">The relative weighting of <paramref name="matrix2" />.</param>
		/// <returns>The interpolated matrix.</returns>
		// Token: 0x0600001E RID: 30 RVA: 0x00002810 File Offset: 0x00000A10
		public static Matrix3x2 Lerp(Matrix3x2 matrix1, Matrix3x2 matrix2, float amount)
		{
			Matrix3x2 result;
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		// Token: 0x0600001F RID: 31 RVA: 0x000028CC File Offset: 0x00000ACC
		public static Matrix3x2 Negate(Matrix3x2 value)
		{
			Matrix3x2 result;
			result.M11 = -value.M11;
			result.M12 = -value.M12;
			result.M21 = -value.M21;
			result.M22 = -value.M22;
			result.M31 = -value.M31;
			result.M32 = -value.M32;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values of <paramref name="value1" /> and <paramref name="value2" />.</returns>
		// Token: 0x06000020 RID: 32 RVA: 0x00002930 File Offset: 0x00000B30
		public static Matrix3x2 Add(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		// Token: 0x06000021 RID: 33 RVA: 0x000029B8 File Offset: 0x00000BB8
		public static Matrix3x2 Subtract(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		// Token: 0x06000022 RID: 34 RVA: 0x00002A40 File Offset: 0x00000C40
		public static Matrix3x2 Multiply(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		// Token: 0x06000023 RID: 35 RVA: 0x00002B28 File Offset: 0x00000D28
		public static Matrix3x2 Multiply(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		// Token: 0x06000024 RID: 36 RVA: 0x00002B90 File Offset: 0x00000D90
		public static Matrix3x2 operator -(Matrix3x2 value)
		{
			Matrix3x2 result;
			result.M11 = -value.M11;
			result.M12 = -value.M12;
			result.M21 = -value.M21;
			result.M22 = -value.M22;
			result.M31 = -value.M31;
			result.M32 = -value.M32;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values.</returns>
		// Token: 0x06000025 RID: 37 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public static Matrix3x2 operator +(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		// Token: 0x06000026 RID: 38 RVA: 0x00002C7C File Offset: 0x00000E7C
		public static Matrix3x2 operator -(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		// Token: 0x06000027 RID: 39 RVA: 0x00002D04 File Offset: 0x00000F04
		public static Matrix3x2 operator *(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		// Token: 0x06000028 RID: 40 RVA: 0x00002DEC File Offset: 0x00000FEC
		public static Matrix3x2 operator *(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result;
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000029 RID: 41 RVA: 0x00002E54 File Offset: 0x00001054
		public static bool operator ==(Matrix3x2 value1, Matrix3x2 value2)
		{
			return value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M31 == value2.M31 && value1.M32 == value2.M32;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are not equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600002A RID: 42 RVA: 0x00002EB8 File Offset: 0x000010B8
		public static bool operator !=(Matrix3x2 value1, Matrix3x2 value2)
		{
			return value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M31 != value2.M31 || value1.M32 != value2.M32;
		}

		/// <summary>Returns a value that indicates whether this instance and another 3x2 matrix are equal.</summary>
		/// <param name="other">The other matrix.</param>
		/// <returns>
		///   <see langword="true" /> if the two matrices are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600002B RID: 43 RVA: 0x00002F20 File Offset: 0x00001120
		public bool Equals(Matrix3x2 other)
		{
			return this.M11 == other.M11 && this.M22 == other.M22 && this.M12 == other.M12 && this.M21 == other.M21 && this.M31 == other.M31 && this.M32 == other.M32;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		// Token: 0x0600002C RID: 44 RVA: 0x00002F83 File Offset: 0x00001183
		public override bool Equals(object obj)
		{
			return obj is Matrix3x2 && this.Equals((Matrix3x2)obj);
		}

		/// <summary>Returns a string that represents this matrix.</summary>
		/// <returns>The string representation of this matrix.</returns>
		// Token: 0x0600002D RID: 45 RVA: 0x00002F9C File Offset: 0x0000119C
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}", new object[]
			{
				this.M11.ToString(currentCulture),
				this.M12.ToString(currentCulture),
				this.M21.ToString(currentCulture),
				this.M22.ToString(currentCulture),
				this.M31.ToString(currentCulture),
				this.M32.ToString(currentCulture)
			});
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		// Token: 0x0600002E RID: 46 RVA: 0x0000301C File Offset: 0x0000121C
		public override int GetHashCode()
		{
			return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003070 File Offset: 0x00001270
		// Note: this type is marked as 'beforefieldinit'.
		static Matrix3x2()
		{
		}

		/// <summary>The first element of the first row.</summary>
		// Token: 0x0400003E RID: 62
		public float M11;

		/// <summary>The second element of the first row.</summary>
		// Token: 0x0400003F RID: 63
		public float M12;

		/// <summary>The first element of the second row.</summary>
		// Token: 0x04000040 RID: 64
		public float M21;

		/// <summary>The second element of the second row.</summary>
		// Token: 0x04000041 RID: 65
		public float M22;

		/// <summary>The first element of the third row.</summary>
		// Token: 0x04000042 RID: 66
		public float M31;

		/// <summary>The second element of the third row.</summary>
		// Token: 0x04000043 RID: 67
		public float M32;

		// Token: 0x04000044 RID: 68
		private static readonly Matrix3x2 _identity = new Matrix3x2(1f, 0f, 0f, 1f, 0f, 0f);
	}
}
