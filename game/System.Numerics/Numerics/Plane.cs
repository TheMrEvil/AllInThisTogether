using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	/// <summary>Represents a plane in three-dimensional space.</summary>
	// Token: 0x02000008 RID: 8
	public struct Plane : IEquatable<Plane>
	{
		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object from the X, Y, and Z components of its normal, and its distance from the origin on that normal.</summary>
		/// <param name="x">The X component of the normal.</param>
		/// <param name="y">The Y component of the normal.</param>
		/// <param name="z">The Z component of the normal.</param>
		/// <param name="d">The distance of the plane along its normal from the origin.</param>
		// Token: 0x06000069 RID: 105 RVA: 0x0000741B File Offset: 0x0000561B
		public Plane(float x, float y, float z, float d)
		{
			this.Normal = new Vector3(x, y, z);
			this.D = d;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object from a specified normal and the distance along the normal from the origin.</summary>
		/// <param name="normal">The plane's normal vector.</param>
		/// <param name="d">The plane's distance from the origin along its normal vector.</param>
		// Token: 0x0600006A RID: 106 RVA: 0x00007433 File Offset: 0x00005633
		public Plane(Vector3 normal, float d)
		{
			this.Normal = normal;
			this.D = d;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object from a specified four-dimensional vector.</summary>
		/// <param name="value">A vector whose first three elements describe the normal vector, and whose <see cref="F:System.Numerics.Vector4.W" /> defines the distance along that normal from the origin.</param>
		// Token: 0x0600006B RID: 107 RVA: 0x00007443 File Offset: 0x00005643
		public Plane(Vector4 value)
		{
			this.Normal = new Vector3(value.X, value.Y, value.Z);
			this.D = value.W;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object that contains three specified points.</summary>
		/// <param name="point1">The first point defining the plane.</param>
		/// <param name="point2">The second point defining the plane.</param>
		/// <param name="point3">The third point defining the plane.</param>
		/// <returns>The plane containing the three points.</returns>
		// Token: 0x0600006C RID: 108 RVA: 0x00007470 File Offset: 0x00005670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = point2 - point1;
				Vector3 vector2 = point3 - point1;
				Vector3 vector3 = Vector3.Normalize(Vector3.Cross(vector, vector2));
				float d = -Vector3.Dot(vector3, point1);
				return new Plane(vector3, d);
			}
			float num = point2.X - point1.X;
			float num2 = point2.Y - point1.Y;
			float num3 = point2.Z - point1.Z;
			float num4 = point3.X - point1.X;
			float num5 = point3.Y - point1.Y;
			float num6 = point3.Z - point1.Z;
			float num7 = num2 * num6 - num3 * num5;
			float num8 = num3 * num4 - num * num6;
			float num9 = num * num5 - num2 * num4;
			float x = num7 * num7 + num8 * num8 + num9 * num9;
			float num10 = 1f / MathF.Sqrt(x);
			Vector3 vector4 = new Vector3(num7 * num10, num8 * num10, num9 * num10);
			return new Plane(vector4, -(vector4.X * point1.X + vector4.Y * point1.Y + vector4.Z * point1.Z));
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Plane" /> object whose normal vector is the source plane's normal vector normalized.</summary>
		/// <param name="value">The source plane.</param>
		/// <returns>The normalized plane.</returns>
		// Token: 0x0600006D RID: 109 RVA: 0x00007598 File Offset: 0x00005798
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Normalize(Plane value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Normal.LengthSquared();
				if (MathF.Abs(num - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num2 = MathF.Sqrt(num);
				return new Plane(value.Normal / num2, value.D / num2);
			}
			else
			{
				float num3 = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
				if (MathF.Abs(num3 - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num4 = 1f / MathF.Sqrt(num3);
				return new Plane(value.Normal.X * num4, value.Normal.Y * num4, value.Normal.Z * num4, value.D * num4);
			}
		}

		/// <summary>Transforms a normalized plane by a 4x4 matrix.</summary>
		/// <param name="plane">The normalized plane to transform.</param>
		/// <param name="matrix">The transformation matrix to apply to <paramref name="plane" />.</param>
		/// <returns>The transformed plane.</returns>
		// Token: 0x0600006E RID: 110 RVA: 0x00007690 File Offset: 0x00005890
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Matrix4x4 matrix)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Invert(matrix, out matrix4x);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;
			return new Plane(x * matrix4x.M11 + y * matrix4x.M12 + z * matrix4x.M13 + d * matrix4x.M14, x * matrix4x.M21 + y * matrix4x.M22 + z * matrix4x.M23 + d * matrix4x.M24, x * matrix4x.M31 + y * matrix4x.M32 + z * matrix4x.M33 + d * matrix4x.M34, x * matrix4x.M41 + y * matrix4x.M42 + z * matrix4x.M43 + d * matrix4x.M44);
		}

		/// <summary>Transforms a normalized plane by a Quaternion rotation.</summary>
		/// <param name="plane">The normalized plane to transform.</param>
		/// <param name="rotation">The Quaternion rotation to apply to the plane.</param>
		/// <returns>A new plane that results from applying the Quaternion rotation.</returns>
		// Token: 0x0600006F RID: 111 RVA: 0x00007768 File Offset: 0x00005968
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Quaternion rotation)
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
			float num13 = 1f - num10 - num12;
			float num14 = num8 - num6;
			float num15 = num9 + num5;
			float num16 = num8 + num6;
			float num17 = 1f - num7 - num12;
			float num18 = num11 - num4;
			float num19 = num9 - num5;
			float num20 = num11 + num4;
			float num21 = 1f - num7 - num10;
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			return new Plane(x * num13 + y * num14 + z * num15, x * num16 + y * num17 + z * num18, x * num19 + y * num20 + z * num21, plane.D);
		}

		/// <summary>Calculates the dot product of a plane and a 4-dimensional vector.</summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The four-dimensional vector.</param>
		/// <returns>The dot product.</returns>
		// Token: 0x06000070 RID: 112 RVA: 0x000078A8 File Offset: 0x00005AA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Plane plane, Vector4 value)
		{
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D * value.W;
		}

		/// <summary>Returns the dot product of a specified three-dimensional vector and the normal vector of this plane plus the distance (<see cref="F:System.Numerics.Plane.D" />) value of the plane.</summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The 3-dimensional vector.</param>
		/// <returns>The dot product.</returns>
		// Token: 0x06000071 RID: 113 RVA: 0x000078FC File Offset: 0x00005AFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotCoordinate(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value) + plane.D;
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D;
		}

		/// <summary>Returns the dot product of a specified three-dimensional vector and the <see cref="F:System.Numerics.Plane.Normal" /> vector of this plane.</summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The three-dimensional vector.</param>
		/// <returns>The dot product.</returns>
		// Token: 0x06000072 RID: 114 RVA: 0x00007964 File Offset: 0x00005B64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotNormal(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value);
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z;
		}

		/// <summary>Returns a value that indicates whether two planes are equal.</summary>
		/// <param name="value1">The first plane to compare.</param>
		/// <param name="value2">The second plane to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000073 RID: 115 RVA: 0x000079C0 File Offset: 0x00005BC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Plane value1, Plane value2)
		{
			return value1.Normal.X == value2.Normal.X && value1.Normal.Y == value2.Normal.Y && value1.Normal.Z == value2.Normal.Z && value1.D == value2.D;
		}

		/// <summary>Returns a value that indicates whether two planes are not equal.</summary>
		/// <param name="value1">The first plane to compare.</param>
		/// <param name="value2">The second plane to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000074 RID: 116 RVA: 0x00007A28 File Offset: 0x00005C28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Plane value1, Plane value2)
		{
			return value1.Normal.X != value2.Normal.X || value1.Normal.Y != value2.Normal.Y || value1.Normal.Z != value2.Normal.Z || value1.D != value2.D;
		}

		/// <summary>Returns a value that indicates whether this instance and another plane object are equal.</summary>
		/// <param name="other">The other plane.</param>
		/// <returns>
		///   <see langword="true" /> if the two planes are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000075 RID: 117 RVA: 0x00007A90 File Offset: 0x00005C90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Plane other)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return this.Normal.Equals(other.Normal) && this.D == other.D;
			}
			return this.Normal.X == other.Normal.X && this.Normal.Y == other.Normal.Y && this.Normal.Z == other.Normal.Z && this.D == other.D;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		// Token: 0x06000076 RID: 118 RVA: 0x00007B20 File Offset: 0x00005D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Plane && this.Equals((Plane)obj);
		}

		/// <summary>Returns the string representation of this plane object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Numerics.Plane" /> object.</returns>
		// Token: 0x06000077 RID: 119 RVA: 0x00007B38 File Offset: 0x00005D38
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{Normal:{0} D:{1}}}", this.Normal.ToString(), this.D.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		// Token: 0x06000078 RID: 120 RVA: 0x00007B73 File Offset: 0x00005D73
		public override int GetHashCode()
		{
			return this.Normal.GetHashCode() + this.D.GetHashCode();
		}

		/// <summary>The normal vector of the plane.</summary>
		// Token: 0x0400005C RID: 92
		public Vector3 Normal;

		/// <summary>The distance of the plane along its normal from the origin.</summary>
		// Token: 0x0400005D RID: 93
		public float D;
	}
}
