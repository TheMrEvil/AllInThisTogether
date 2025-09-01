using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C7 RID: 455
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[Il2CppEagerStaticClassConstruction]
	[NativeType(Header = "Runtime/Math/Quaternion.h")]
	[UsedByNativeCode]
	public struct Quaternion : IEquatable<Quaternion>, IFormattable
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x0001F090 File Offset: 0x0001D290
		[FreeFunction("FromToQuaternionSafe", IsThreadSafe = true)]
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion result;
			Quaternion.FromToRotation_Injected(ref fromDirection, ref toDirection, out result);
			return result;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0001F0AC File Offset: 0x0001D2AC
		[FreeFunction(IsThreadSafe = true)]
		public static Quaternion Inverse(Quaternion rotation)
		{
			Quaternion result;
			Quaternion.Inverse_Injected(ref rotation, out result);
			return result;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		[FreeFunction("QuaternionScripting::Slerp", IsThreadSafe = true)]
		public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.Slerp_Injected(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		[FreeFunction("QuaternionScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		[FreeFunction("QuaternionScripting::Lerp", IsThreadSafe = true)]
		public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.Lerp_Injected(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0001F118 File Offset: 0x0001D318
		[FreeFunction("QuaternionScripting::LerpUnclamped", IsThreadSafe = true)]
		public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.LerpUnclamped_Injected(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0001F134 File Offset: 0x0001D334
		[FreeFunction("EulerToQuaternion", IsThreadSafe = true)]
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			Quaternion result;
			Quaternion.Internal_FromEulerRad_Injected(ref euler, out result);
			return result;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0001F14C File Offset: 0x0001D34C
		[FreeFunction("QuaternionScripting::ToEuler", IsThreadSafe = true)]
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			Vector3 result;
			Quaternion.Internal_ToEulerRad_Injected(ref rotation, out result);
			return result;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0001F163 File Offset: 0x0001D363
		[FreeFunction("QuaternionScripting::ToAxisAngle", IsThreadSafe = true)]
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0001F170 File Offset: 0x0001D370
		[FreeFunction("QuaternionScripting::AngleAxis", IsThreadSafe = true)]
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			Quaternion result;
			Quaternion.AngleAxis_Injected(angle, ref axis, out result);
			return result;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0001F188 File Offset: 0x0001D388
		[FreeFunction("QuaternionScripting::LookRotation", IsThreadSafe = true)]
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			Quaternion result;
			Quaternion.LookRotation_Injected(ref forward, ref upwards, out result);
			return result;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			return Quaternion.LookRotation(forward, Vector3.up);
		}

		// Token: 0x17000424 RID: 1060
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0001F279 File Offset: 0x0001D479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0001F279 File Offset: 0x0001D479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0001F29C File Offset: 0x0001D49C
		public static Quaternion identity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.identityQuaternion;
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result;
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsEqualUsingDot(float dot)
		{
			return dot > 0.999999f;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0001F514 File Offset: 0x0001D714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0001F530 File Offset: 0x0001D730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0001F57C File Offset: 0x0001D77C
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0001F599 File Offset: 0x0001D799
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation(view, up);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Quaternion a, Quaternion b)
		{
			float num = Mathf.Min(Mathf.Abs(Quaternion.Dot(a, b)), 1f);
			return Quaternion.IsEqualUsingDot(num) ? 0f : (Mathf.Acos(num) * 2f * 57.29578f);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		private static Vector3 Internal_MakePositive(Vector3 euler)
		{
			float num = -0.005729578f;
			float num2 = 360f + num;
			bool flag = euler.x < num;
			if (flag)
			{
				euler.x += 360f;
			}
			else
			{
				bool flag2 = euler.x > num2;
				if (flag2)
				{
					euler.x -= 360f;
				}
			}
			bool flag3 = euler.y < num;
			if (flag3)
			{
				euler.y += 360f;
			}
			else
			{
				bool flag4 = euler.y > num2;
				if (flag4)
				{
					euler.y -= 360f;
				}
			}
			bool flag5 = euler.z < num;
			if (flag5)
			{
				euler.z += 360f;
			}
			else
			{
				bool flag6 = euler.z > num2;
				if (flag6)
				{
					euler.z -= 360f;
				}
			}
			return euler;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0001F704 File Offset: 0x0001D904
		public Vector3 eulerAngles
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this = Quaternion.Internal_FromEulerRad(value * 0.017453292f);
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0001F720 File Offset: 0x0001D920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0001F74C File Offset: 0x0001D94C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0001F76E File Offset: 0x0001D96E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0001F789 File Offset: 0x0001D989
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0001F79C File Offset: 0x0001D99C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			bool flag = num == 0f;
			Quaternion result;
			if (flag)
			{
				result = to;
			}
			else
			{
				result = Quaternion.SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / num));
			}
			return result;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Normalize(Quaternion q)
		{
			float num = Mathf.Sqrt(Quaternion.Dot(q, q));
			bool flag = num < Mathf.Epsilon;
			Quaternion result;
			if (flag)
			{
				result = Quaternion.identity;
			}
			else
			{
				result = new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
			}
			return result;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0001F834 File Offset: 0x0001DA34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			this = Quaternion.Normalize(this);
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0001F848 File Offset: 0x0001DA48
		public Quaternion normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.Normalize(this);
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0001F868 File Offset: 0x0001DA68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Quaternion);
			return !flag && this.Equals((Quaternion)other);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Quaternion other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0001F944 File Offset: 0x0001DB44
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0001F960 File Offset: 0x0001DB60
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0001F97C File Offset: 0x0001DB7C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F5";
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

		// Token: 0x06001481 RID: 5249 RVA: 0x0001FA04 File Offset: 0x0001DC04
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0001FA24 File Offset: 0x0001DC24
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0001FA3C File Offset: 0x0001DC3C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0001FA52 File Offset: 0x0001DC52
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0001FA64 File Offset: 0x0001DC64
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0001FA84 File Offset: 0x0001DC84
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0001FABC File Offset: 0x0001DCBC
		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0001FACD File Offset: 0x0001DCCD
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new Vector3(x, y, z));
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0001FADF File Offset: 0x0001DCDF
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerAngles(Vector3 euler)
		{
			this = Quaternion.EulerRotation(euler);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0001FB08 File Offset: 0x0001DD08
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0001FB25 File Offset: 0x0001DD25
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0001FB38 File Offset: 0x0001DD38
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion AxisAngle(Vector3 axis, float angle)
		{
			return Quaternion.AngleAxis(57.29578f * angle, axis);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0001FB57 File Offset: 0x0001DD57
		// Note: this type is marked as 'beforefieldinit'.
		static Quaternion()
		{
		}

		// Token: 0x06001490 RID: 5264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromToRotation_Injected(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion ret);

		// Token: 0x06001491 RID: 5265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected(ref Quaternion rotation, out Quaternion ret);

		// Token: 0x06001492 RID: 5266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001493 RID: 5267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001494 RID: 5268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001495 RID: 5269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001496 RID: 5270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_FromEulerRad_Injected(ref Vector3 euler, out Quaternion ret);

		// Token: 0x06001497 RID: 5271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToEulerRad_Injected(ref Quaternion rotation, out Vector3 ret);

		// Token: 0x06001498 RID: 5272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToAxisAngleRad_Injected(ref Quaternion q, out Vector3 axis, out float angle);

		// Token: 0x06001499 RID: 5273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AngleAxis_Injected(float angle, ref Vector3 axis, out Quaternion ret);

		// Token: 0x0600149A RID: 5274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookRotation_Injected(ref Vector3 forward, [DefaultValue("Vector3.up")] ref Vector3 upwards, out Quaternion ret);

		// Token: 0x04000773 RID: 1907
		public float x;

		// Token: 0x04000774 RID: 1908
		public float y;

		// Token: 0x04000775 RID: 1909
		public float z;

		// Token: 0x04000776 RID: 1910
		public float w;

		// Token: 0x04000777 RID: 1911
		private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

		// Token: 0x04000778 RID: 1912
		public const float kEpsilon = 1E-06f;
	}
}
