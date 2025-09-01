using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C6 RID: 454
	[NativeClass("Vector3f")]
	[NativeHeader("Runtime/Math/Vector3.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeType(Header = "Runtime/Math/Vector3.h")]
	[Il2CppEagerStaticClassConstruction]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x0001DF6C File Offset: 0x0001C16C
		[FreeFunction("VectorScripting::Slerp", IsThreadSafe = true)]
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Vector3 result;
			Vector3.Slerp_Injected(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0001DF88 File Offset: 0x0001C188
		[FreeFunction("VectorScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			Vector3 result;
			Vector3.SlerpUnclamped_Injected(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06001413 RID: 5139
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		// Token: 0x06001414 RID: 5140 RVA: 0x0001DFA2 File Offset: 0x0001C1A2
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.OrthoNormalize2(ref normal, ref tangent);
		}

		// Token: 0x06001415 RID: 5141
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		// Token: 0x06001416 RID: 5142 RVA: 0x0001DFAD File Offset: 0x0001C1AD
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		[FreeFunction(IsThreadSafe = true)]
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			Vector3 result;
			Vector3.RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out result);
			return result;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0001E03C File Offset: 0x0001C23C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0001E098 File Offset: 0x0001C298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			bool flag = num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta);
			Vector3 result;
			if (flag)
			{
				result = target;
			}
			else
			{
				float num5 = (float)Math.Sqrt((double)num4);
				result = new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
			}
			return result;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0001E144 File Offset: 0x0001C344
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0001E168 File Offset: 0x0001C368
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0001E194 File Offset: 0x0001C394
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			float num6 = current.z - target.z;
			Vector3 vector = target;
			float num7 = maxSpeed * smoothTime;
			float num8 = num7 * num7;
			float num9 = num4 * num4 + num5 * num5 + num6 * num6;
			bool flag = num9 > num8;
			if (flag)
			{
				float num10 = (float)Math.Sqrt((double)num9);
				num4 = num4 / num10 * num7;
				num5 = num5 / num10 * num7;
				num6 = num6 / num10 * num7;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			target.z = current.z - num6;
			float num11 = (currentVelocity.x + num * num4) * deltaTime;
			float num12 = (currentVelocity.y + num * num5) * deltaTime;
			float num13 = (currentVelocity.z + num * num6) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num11) * num3;
			currentVelocity.y = (currentVelocity.y - num * num12) * num3;
			currentVelocity.z = (currentVelocity.z - num * num13) * num3;
			float num14 = target.x + (num4 + num11) * num3;
			float num15 = target.y + (num5 + num12) * num3;
			float num16 = target.z + (num6 + num13) * num3;
			float num17 = vector.x - current.x;
			float num18 = vector.y - current.y;
			float num19 = vector.z - current.z;
			float num20 = num14 - vector.x;
			float num21 = num15 - vector.y;
			float num22 = num16 - vector.z;
			bool flag2 = num17 * num20 + num18 * num21 + num19 * num22 > 0f;
			if (flag2)
			{
				num14 = vector.x;
				num15 = vector.y;
				num16 = vector.z;
				currentVelocity.x = (num14 - vector.x) / deltaTime;
				currentVelocity.y = (num15 - vector.y) / deltaTime;
				currentVelocity.z = (num16 - vector.z) / deltaTime;
			}
			return new Vector3(num14, num15, num16);
		}

		// Token: 0x17000415 RID: 1045
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0001E4EC File Offset: 0x0001C6EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0001E52A File Offset: 0x0001C72A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0001E568 File Offset: 0x0001C768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0001E60C File Offset: 0x0001C80C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3);
			return !flag && this.Equals((Vector3)other);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0001E640 File Offset: 0x0001C840
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0001E680 File Offset: 0x0001C880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			float num = -2f * Vector3.Dot(inNormal, inDirection);
			return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			bool flag = num > 1E-05f;
			Vector3 result;
			if (flag)
			{
				result = value / num;
			}
			else
			{
				result = Vector3.zero;
			}
			return result;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0001E708 File Offset: 0x0001C908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0001E750 File Offset: 0x0001C950
		public Vector3 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0001E770 File Offset: 0x0001C970
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 result;
			if (flag)
			{
				result = Vector3.zero;
			}
			else
			{
				float num2 = Vector3.Dot(vector, onNormal);
				result = new Vector3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
			}
			return result;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0001E808 File Offset: 0x0001CA08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			float num = Vector3.Dot(planeNormal, planeNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 result;
			if (flag)
			{
				result = vector;
			}
			else
			{
				float num2 = Vector3.Dot(vector, planeNormal);
				result = new Vector3(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num, vector.z - planeNormal.z * num2 / num);
			}
			return result;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0001E874 File Offset: 0x0001CA74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Vector3 from, Vector3 to)
		{
			float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = num < 1E-15f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float num2 = Mathf.Clamp(Vector3.Dot(from, to) / num, -1f, 1f);
				result = (float)Math.Acos((double)num2) * 57.29578f;
			}
			return result;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float num = Vector3.Angle(from, to);
			float num2 = from.y * to.z - from.z * to.y;
			float num3 = from.z * to.x - from.x * to.z;
			float num4 = from.x * to.y - from.y * to.x;
			float num5 = Mathf.Sign(axis.x * num2 + axis.y * num3 + axis.z * num4);
			return num * num5;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0001E970 File Offset: 0x0001CB70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 a, Vector3 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			float num3 = a.z - b.z;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector3 result;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				float num4 = vector.z / num;
				result = new Vector3(num2 * maxLength, num3 * maxLength, num4 * maxLength);
			}
			else
			{
				result = vector;
			}
			return result;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0001EA24 File Offset: 0x0001CC24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector3 vector)
		{
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0001EA68 File Offset: 0x0001CC68
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0001EAAC File Offset: 0x0001CCAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0001EB24 File Offset: 0x0001CD24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0001EB70 File Offset: 0x0001CD70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0001EBBC File Offset: 0x0001CDBC
		public static Vector3 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.zeroVector;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
		public static Vector3 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.oneVector;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		public static Vector3 forward
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.forwardVector;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0001EC04 File Offset: 0x0001CE04
		public static Vector3 back
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.backVector;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		public static Vector3 up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.upVector;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0001EC34 File Offset: 0x0001CE34
		public static Vector3 down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.downVector;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0001EC4C File Offset: 0x0001CE4C
		public static Vector3 left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.leftVector;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0001EC64 File Offset: 0x0001CE64
		public static Vector3 right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.rightVector;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		public static Vector3 positiveInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.positiveInfinityVector;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0001EC94 File Offset: 0x0001CE94
		public static Vector3 negativeInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.negativeInfinityVector;
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0001ED58 File Offset: 0x0001CF58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0001ED88 File Offset: 0x0001CF88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			return num4 < 9.9999994E-11f;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0001EE3C File Offset: 0x0001D03C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0001EE58 File Offset: 0x0001D058
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0001EE74 File Offset: 0x0001D074
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0001EE90 File Offset: 0x0001D090
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
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0001EF08 File Offset: 0x0001D108
		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0001EF30 File Offset: 0x0001D130
		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return (float)Math.Acos((double)Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0001EF6C File Offset: 0x0001D16C
		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return Vector3.ProjectOnPlane(fromThat, excludeThis);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0001EF88 File Offset: 0x0001D188
		// Note: this type is marked as 'beforefieldinit'.
		static Vector3()
		{
		}

		// Token: 0x06001453 RID: 5203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001454 RID: 5204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001455 RID: 5205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateTowards_Injected(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);

		// Token: 0x04000764 RID: 1892
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000765 RID: 1893
		public const float kEpsilonNormalSqrt = 1E-15f;

		// Token: 0x04000766 RID: 1894
		public float x;

		// Token: 0x04000767 RID: 1895
		public float y;

		// Token: 0x04000768 RID: 1896
		public float z;

		// Token: 0x04000769 RID: 1897
		private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

		// Token: 0x0400076A RID: 1898
		private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

		// Token: 0x0400076B RID: 1899
		private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

		// Token: 0x0400076C RID: 1900
		private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

		// Token: 0x0400076D RID: 1901
		private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

		// Token: 0x0400076E RID: 1902
		private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

		// Token: 0x0400076F RID: 1903
		private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

		// Token: 0x04000770 RID: 1904
		private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

		// Token: 0x04000771 RID: 1905
		private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x04000772 RID: 1906
		private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
