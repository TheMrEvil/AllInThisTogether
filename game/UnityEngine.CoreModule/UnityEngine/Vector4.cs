using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CD RID: 461
	[Il2CppEagerStaticClassConstruction]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Math/Vector4.h")]
	[NativeClass("Vector4f")]
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x1700044D RID: 1101
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				float result;
				switch (index)
				{
				case 0:
					result = this.x;
					break;
				case 1:
					result = this.y;
					break;
				case 2:
					result = this.z;
					break;
				case 3:
					result = this.w;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00022461 File Offset: 0x00020661
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00022481 File Offset: 0x00020681
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000224A4 File Offset: 0x000206A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00022461 File Offset: 0x00020661
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000224CC File Offset: 0x000206CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00022544 File Offset: 0x00020744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000225B4 File Offset: 0x000207B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = target.w - current.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			bool flag = num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta);
			Vector4 result;
			if (flag)
			{
				result = target;
			}
			else
			{
				float num6 = (float)Math.Sqrt((double)num5);
				result = new Vector4(current.x + num / num6 * maxDistanceDelta, current.y + num2 / num6 * maxDistanceDelta, current.z + num3 / num6 * maxDistanceDelta, current.w + num4 / num6 * maxDistanceDelta);
			}
			return result;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00022684 File Offset: 0x00020884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000226D0 File Offset: 0x000208D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0002272C File Offset: 0x0002092C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00022774 File Offset: 0x00020974
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector4);
			return !flag && this.Equals((Vector4)other);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000227A8 File Offset: 0x000209A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector4 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000227F8 File Offset: 0x000209F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 a)
		{
			float num = Vector4.Magnitude(a);
			bool flag = num > 1E-05f;
			Vector4 result;
			if (flag)
			{
				result = a / num;
			}
			else
			{
				result = Vector4.zero;
			}
			return result;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0002282C File Offset: 0x00020A2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float num = Vector4.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector4.zero;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00022874 File Offset: 0x00020A74
		public Vector4 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00022894 File Offset: 0x00020A94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000228E0 File Offset: 0x00020AE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Project(Vector4 a, Vector4 b)
		{
			return b * (Vector4.Dot(a, b) / Vector4.Dot(b, b));
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00022908 File Offset: 0x00020B08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 a, Vector4 b)
		{
			return Vector4.Magnitude(a - b);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00022928 File Offset: 0x00020B28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector4 a)
		{
			return (float)Math.Sqrt((double)Vector4.Dot(a, a));
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00022948 File Offset: 0x00020B48
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)Vector4.Dot(this, this));
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00022974 File Offset: 0x00020B74
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00022998 File Offset: 0x00020B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000229F4 File Offset: 0x00020BF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00022A50 File Offset: 0x00020C50
		public static Vector4 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.zeroVector;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00022A68 File Offset: 0x00020C68
		public static Vector4 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.oneVector;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00022A80 File Offset: 0x00020C80
		public static Vector4 positiveInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.positiveInfinityVector;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00022A98 File Offset: 0x00020C98
		public static Vector4 negativeInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.negativeInfinityVector;
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00022AB0 File Offset: 0x00020CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00022AFC File Offset: 0x00020CFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00022B48 File Offset: 0x00020D48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00022B7C File Offset: 0x00020D7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00022BB4 File Offset: 0x00020DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00022BEC File Offset: 0x00020DEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00022C24 File Offset: 0x00020E24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = lhs.w - rhs.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			return num5 < 9.9999994E-11f;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00022C8C File Offset: 0x00020E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00022CA8 File Offset: 0x00020EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00022CD8 File Offset: 0x00020ED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00022D04 File Offset: 0x00020F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00022D34 File Offset: 0x00020F34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00022D58 File Offset: 0x00020F58
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00022D74 File Offset: 0x00020F74
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00022D90 File Offset: 0x00020F90
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00022E18 File Offset: 0x00021018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00022E34 File Offset: 0x00021034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float SqrMagnitude()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00022E58 File Offset: 0x00021058
		// Note: this type is marked as 'beforefieldinit'.
		static Vector4()
		{
		}

		// Token: 0x0400079F RID: 1951
		public const float kEpsilon = 1E-05f;

		// Token: 0x040007A0 RID: 1952
		public float x;

		// Token: 0x040007A1 RID: 1953
		public float y;

		// Token: 0x040007A2 RID: 1954
		public float z;

		// Token: 0x040007A3 RID: 1955
		public float w;

		// Token: 0x040007A4 RID: 1956
		private static readonly Vector4 zeroVector = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040007A5 RID: 1957
		private static readonly Vector4 oneVector = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x040007A6 RID: 1958
		private static readonly Vector4 positiveInfinityVector = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040007A7 RID: 1959
		private static readonly Vector4 negativeInfinityVector = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
