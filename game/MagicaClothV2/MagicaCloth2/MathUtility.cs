using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000DF RID: 223
	public static class MathUtility
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0001ECB3 File Offset: 0x0001CEB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp1(float a)
		{
			return math.clamp(a, -1f, 1f);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001ECC5 File Offset: 0x0001CEC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 Project(in float3 v, in float3 n)
		{
			return math.dot(v, n) * n;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001ECE3 File Offset: 0x0001CEE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ProjectOnPlane(in float3 v, in float3 n)
		{
			return v - math.dot(v, n) * n;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(in float3 v1, in float3 v2)
		{
			float num = math.length(v1);
			float num2 = math.length(v2);
			return math.acos(MathUtility.Clamp1(math.dot(v1, v2) / (num * num2)));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001ED50 File Offset: 0x0001CF50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClampVector(float3 v, float minlength, float maxlength)
		{
			float num = math.length(v);
			if (num > 1E-09f)
			{
				if (num > maxlength)
				{
					v *= maxlength / num;
				}
				else if (num < minlength)
				{
					v *= minlength / num;
				}
			}
			return v;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001ED90 File Offset: 0x0001CF90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClampVector(float3 v, float maxlength)
		{
			float num = math.length(v);
			if (num > 1E-09f && num > maxlength)
			{
				v *= maxlength / num;
			}
			return v;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClampDistance(float3 from, float3 to, float maxlength)
		{
			float num = math.distance(from, to);
			if (num <= maxlength)
			{
				return to;
			}
			float s = maxlength / num;
			return math.lerp(from, to, s);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001EDE4 File Offset: 0x0001CFE4
		public static bool ClampAngle(in float3 dir, in float3 basedir, float maxAngle, out float3 outdir)
		{
			float3 @float = math.normalize(dir);
			float3 y = math.normalize(basedir);
			float num = MathUtility.Clamp1(math.dot(@float, y));
			float num2 = math.acos(num);
			if (num2 <= maxAngle)
			{
				outdir = dir;
				return false;
			}
			float num3 = (num2 - maxAngle) / num2;
			float3 x = math.cross(@float, y);
			if (math.abs(1f + num) < 1E-06f)
			{
				num2 = 3.1415927f;
				if (@float.x > @float.y && @float.x > @float.z)
				{
					x = math.cross(@float, new float3(0f, 1f, 0f));
				}
				else
				{
					x = math.cross(@float, new float3(1f, 0f, 0f));
				}
			}
			else if (math.abs(1f - num) < 1E-06f)
			{
				outdir = dir;
				return false;
			}
			quaternion q = quaternion.AxisAngle(math.normalize(x), num2 * num3);
			outdir = math.mul(q, dir);
			return true;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		public static quaternion FromToRotation(in float3 from, in float3 to, float t = 1f)
		{
			float3 @float = math.normalize(from);
			float3 y = math.normalize(to);
			float num = MathUtility.Clamp1(math.dot(@float, y));
			float num2 = math.acos(num);
			float3 x = math.cross(@float, y);
			if (math.abs(1f + num) < 1E-06f)
			{
				num2 = 3.1415927f;
				if (@float.x > @float.y && @float.x > @float.z)
				{
					x = math.cross(@float, new float3(0f, 1f, 0f));
				}
				else
				{
					x = math.cross(@float, new float3(1f, 0f, 0f));
				}
			}
			else if (math.abs(1f - num) < 1E-06f)
			{
				return quaternion.identity;
			}
			return quaternion.AxisAngle(math.normalize(x), num2 * t);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001EFD3 File Offset: 0x0001D1D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion FromToRotation(in quaternion from, in quaternion to)
		{
			return math.mul(to, math.inverse(from));
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(in quaternion a, in quaternion b)
		{
			float num = math.dot(a, b);
			if (math.abs(num) >= 0.9999f)
			{
				return 0f;
			}
			float num2 = math.acos(MathUtility.Clamp1(num)) * 2f;
			if (num2 <= 3.1415927f)
			{
				return num2;
			}
			return 6.2831855f - num2;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001F044 File Offset: 0x0001D244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion ClampAngle(quaternion from, quaternion to, float maxAngle)
		{
			float num = MathUtility.Angle(from, to);
			if (num <= maxAngle)
			{
				return to;
			}
			float t = maxAngle / num;
			return math.slerp(from, to, t);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001F06D File Offset: 0x0001D26D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion ToRotation(in float3 nor, in float3 tan)
		{
			return quaternion.LookRotation(tan, nor);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001F080 File Offset: 0x0001D280
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToNormalTangent(in quaternion rot, out float3 nor, out float3 tan)
		{
			nor = math.mul(rot, math.up());
			tan = math.mul(rot, math.forward());
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001F0AE File Offset: 0x0001D2AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ToNormal(in quaternion rot)
		{
			return math.mul(rot, math.up());
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001F0C0 File Offset: 0x0001D2C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ToTangent(in quaternion rot)
		{
			return math.mul(rot, math.forward());
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001F0D2 File Offset: 0x0001D2D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ToBinormal(in quaternion rot)
		{
			return math.mul(rot, math.right());
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 Binormal(in float3 nor, in float3 tan)
		{
			return math.cross(nor, tan);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001F0F8 File Offset: 0x0001D2F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 AxisToEuler(in float3 axis)
		{
			float y = math.atan2(axis.x, axis.z);
			return new float3(math.atan2(-axis.y, math.length(axis - new float3(0f, axis.y, 0f))), y, 0f);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001F153 File Offset: 0x0001D353
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion AxisQuaternion(float3 dir)
		{
			return quaternion.Euler(MathUtility.AxisToEuler(dir), math.RotationOrder.ZXY);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001F164 File Offset: 0x0001D364
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToAngleAxis(in quaternion q, out float angle, out float3 axis)
		{
			float num = (math.abs(q.value.w) < 0.9999f) ? math.acos(q.value.w) : 0f;
			angle = 2f * num;
			float num2 = math.sin(num);
			if (math.abs(num2) < 1E-06f)
			{
				axis = 0;
				return;
			}
			float4 value = q.value;
			axis = value.xyz / num2;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ClosestPtPointSegmentRatio(in float3 c, in float3 a, in float3 b)
		{
			float3 @float = b - a;
			return math.saturate(math.dot(c - a, @float) / math.dot(@float, @float));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001F228 File Offset: 0x0001D428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ClosestPtPointSegmentRatioNoClamp(float3 c, float3 a, float3 b)
		{
			float3 @float = b - a;
			return math.dot(c - a, @float) / math.dot(@float, @float);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001F254 File Offset: 0x0001D454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClosestPtPointSegment(float3 c, float3 a, float3 b)
		{
			float3 @float = b - a;
			float num = math.dot(c - a, @float) / math.dot(@float, @float);
			num = math.saturate(num);
			return a + num * @float;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001F294 File Offset: 0x0001D494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClosestPtPointSegmentNoClamp(float3 c, float3 a, float3 b)
		{
			float3 @float = b - a;
			float lhs = math.dot(c - a, @float) / math.dot(@float, @float);
			return a + lhs * @float;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001F2CC File Offset: 0x0001D4CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ClosestPtSegmentSegment(in float3 p1, in float3 q1, in float3 p2, in float3 q2, out float s, out float t, out float3 c1, out float3 c2)
		{
			float3 @float = q1 - p1;
			float3 float2 = q2 - p2;
			float3 y = p1 - p2;
			float num = math.dot(@float, @float);
			float num2 = math.dot(float2, float2);
			float num3 = math.dot(float2, y);
			if (num <= 1E-08f && num2 <= 1E-08f)
			{
				s = (t = 0f);
				c1 = p1;
				c2 = p2;
				return math.dot(c1 - c2, c1 - c2);
			}
			if (num <= 1E-08f)
			{
				s = 0f;
				t = math.saturate(num3 / num2);
			}
			else
			{
				float num4 = math.dot(@float, y);
				if (num2 <= 1E-08f)
				{
					t = 0f;
					s = math.saturate(-num4 / num);
				}
				else
				{
					float num5 = math.dot(@float, float2);
					float num6 = num * num2 - num5 * num5;
					if (num6 != 0f)
					{
						s = math.saturate((num5 * num3 - num4 * num2) / num6);
					}
					else
					{
						s = 0f;
					}
					t = (num5 * s + num3) / num2;
					if (t < 0f)
					{
						t = 0f;
						s = math.saturate(-num4 / num);
					}
					else if (t > 1f)
					{
						t = 1f;
						s = math.saturate((num5 - num4) / num);
					}
				}
			}
			c1 = p1 + @float * s;
			c2 = p2 + float2 * t;
			return math.dot(c1 - c2, c1 - c2);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClosestPtPointTriangle(in float3 p, in float3 a, in float3 b, in float3 c, out float3 uvw)
		{
			uvw = 0;
			float3 @float = b - a;
			float3 float2 = c - a;
			float3 y = p - a;
			float num = math.dot(@float, y);
			float num2 = math.dot(float2, y);
			if (num <= 0f && num2 <= 0f)
			{
				uvw.x = 1f;
				return a;
			}
			float3 y2 = p - b;
			float num3 = math.dot(@float, y2);
			float num4 = math.dot(float2, y2);
			if (num3 >= 0f && num4 <= num3)
			{
				uvw.y = 1f;
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			float num6;
			if (num5 <= 0f && num >= 0f && num3 <= 0f)
			{
				num6 = num / (num - num3);
				uvw = new float3(1f - num6, num6, 0f);
				return a + num6 * @float;
			}
			float3 y3 = p - c;
			float num7 = math.dot(@float, y3);
			float num8 = math.dot(float2, y3);
			if (num8 >= 0f && num7 <= num8)
			{
				uvw.z = 1f;
				return c;
			}
			float num9 = num7 * num2 - num * num8;
			float num10;
			if (num9 <= 0f && num2 >= 0f && num8 <= 0f)
			{
				num10 = num2 / (num2 - num8);
				uvw = new float3(1f - num10, 0f, num10);
				return a + num10 * float2;
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num11 <= 0f && num4 - num3 >= 0f && num7 - num8 >= 0f)
			{
				num10 = (num4 - num3) / (num4 - num3 + (num7 - num8));
				uvw = new float3(0f, 1f - num10, num10);
				return b + num10 * (c - b);
			}
			float num12 = 1f / (num11 + num9 + num5);
			num6 = num9 * num12;
			num10 = num5 * num12;
			uvw = new float3(1f - num6 - num10, num6, num10);
			return a + @float * num6 + float2 * num10;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001F77F File Offset: 0x0001D97F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool PointInTriangleUVW(float3 uvw)
		{
			return math.all(uvw);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001F787 File Offset: 0x0001D987
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TriangleCenter(in float3 p0, in float3 p1, in float3 p2)
		{
			return (p0 + p1 + p2) / 3f;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001F7AF File Offset: 0x0001D9AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TriangleNormal(in float3 p0, in float3 p1, in float3 p2)
		{
			return math.normalize(math.cross(p1 - p0, p2 - p0));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001F7DD File Offset: 0x0001D9DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TriangleArea(in float3 p0, in float3 p1, in float3 p2)
		{
			return math.length(math.cross(p1 - p0, p2 - p0));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001F80B File Offset: 0x0001DA0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSafeTriangle(in float3 p0, in float3 p1, in float3 p2)
		{
			return MathUtility.TriangleArea(p0, p1, p2) > 1E-06f;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001F81C File Offset: 0x0001DA1C
		public static float3 TriangleTangent(in float3 p0, in float3 p1, in float3 p2, in float2 uv0, in float2 uv1, in float2 uv2)
		{
			float3 @float = p1 - p0;
			float3 float2 = p2 - p0;
			float2 float3 = uv1 - uv0;
			float2 float4 = uv2 - uv0;
			float num = float3.x * float4.y - float3.y * float4.x;
			0;
			if (num == 0f)
			{
				Debug.LogWarning(string.Format("Calc tangent area = 0!\np0:{0},p1:{1},p2:{2}\nuv0:{3},uv1:{4},uv2:{5}", new object[]
				{
					p0,
					p1,
					p2,
					uv0,
					uv1,
					uv2
				}));
				num = 1f;
			}
			float rhs = 1f / num;
			return -(new float3(@float.x * float4.y + float2.x * -float3.y, @float.y * float4.y + float2.y * -float3.y, @float.z * float4.y + float2.z * -float3.y) * rhs);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001F984 File Offset: 0x0001DB84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion TriangleRotation(float3 p0, float3 p1, float3 p2)
		{
			float3 up = MathUtility.TriangleNormal(p0, p1, p2);
			float3 rhs = MathUtility.TriangleCenter(p0, p1, p2);
			return quaternion.LookRotation(math.normalize(p0 - rhs), up);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001F9BC File Offset: 0x0001DBBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion TriangleCenterRotation(float3 p0, float3 p1, float3 p2, float3 p3)
		{
			float3 lhs = MathUtility.TriangleNormal(p0, p2, p3);
			float3 rhs = MathUtility.TriangleNormal(p1, p3, p2);
			float3 up = (lhs + rhs) * 0.5f;
			return quaternion.LookRotation(math.normalize(p3 - p2), up);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001FA04 File Offset: 0x0001DC04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TriangleAngle(in float3 v0, in float3 v1, in float3 v2, in float3 v3)
		{
			float3 @float = v1 - v0;
			float3 x = v2 - v0;
			float3 y = v3 - v0;
			float3 float2 = math.cross(x, @float);
			float3 float3 = math.cross(@float, y);
			return MathUtility.Angle(float2, float3);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001FA60 File Offset: 0x0001DC60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceTriangleCenter(float3 p, float3 p0, float3 p1, float3 p2)
		{
			float3 y = (p0 + p1 + p2) / 3f;
			return math.distance(p, y);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DirectionPointTriangle(float3 p, float3 a, float3 b, float3 c)
		{
			float3 x = b - a;
			float3 y = c - a;
			float3 x2 = p - a;
			float3 y2 = math.cross(x, y);
			return math.sign(math.dot(x2, y2));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
		public static int2 GetRestTriangleVertex(int3 tri1, int3 tri2, int2 edge)
		{
			int2 result = -1;
			for (int i = 0; i < 3; i++)
			{
				int num = tri1[i];
				if (num != edge.x && num != edge.x && num != edge.y && num != edge.y)
				{
					result[0] = num;
					break;
				}
			}
			for (int j = 0; j < 3; j++)
			{
				int num2 = tri2[j];
				if (num2 != edge.x && num2 != edge.x && num2 != edge.y && num2 != edge.y)
				{
					result[1] = num2;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001FB68 File Offset: 0x0001DD68
		public static int2 GetCommonEdgeFromTrianglePair(int3 tri1, int3 tri2)
		{
			int2 @int = 0;
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				if (tri1[i] == tri2.x || tri1[i] == tri2.y || tri1[i] == tri2.z)
				{
					@int[num] = tri1[i];
					num++;
				}
			}
			if (num != 2)
			{
				Debug.LogError("Common edge nothing!");
				return 0;
			}
			if (@int.x > @int.y)
			{
				int x = @int.x;
				@int.x = @int.y;
				@int.y = x;
			}
			return @int;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001FC10 File Offset: 0x0001DE10
		public static int4 GetTrianglePairIndices(int3 tri1, int3 tri2)
		{
			int2 commonEdgeFromTrianglePair = MathUtility.GetCommonEdgeFromTrianglePair(tri1, tri2);
			int4 result = new int4(0, 0, commonEdgeFromTrianglePair.x, commonEdgeFromTrianglePair.y);
			for (int i = 0; i < 3; i++)
			{
				if (tri1[i] != commonEdgeFromTrianglePair.x && tri1[i] != commonEdgeFromTrianglePair.y)
				{
					result[0] = tri1[i];
				}
				if (tri2[i] != commonEdgeFromTrianglePair.x && tri2[i] != commonEdgeFromTrianglePair.y)
				{
					result[1] = tri2[i];
				}
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001FCA8 File Offset: 0x0001DEA8
		public static int GetUnuseTriangleIndex(int3 tri, int2 edge)
		{
			if (tri.x != edge.x && tri.x != edge.y)
			{
				return tri.x;
			}
			if (tri.y != edge.x && tri.y != edge.y)
			{
				return tri.y;
			}
			if (tri.z != edge.x && tri.z != edge.y)
			{
				return tri.z;
			}
			return -1;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001FD20 File Offset: 0x0001DF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetTrianglePairAngle(float3 pos0, float3 pos1, float3 pos2, float3 pos3)
		{
			float3 @float = pos3 - pos2;
			float3 y = pos0 - pos2;
			float3 x = pos1 - pos2;
			float3 float2 = math.cross(@float, y);
			float3 float3 = math.cross(x, @float);
			return MathUtility.Angle(float2, float3);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 FlipTriangle(in int3 tri)
		{
			int3 @int = tri;
			return @int.xzy;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001FD78 File Offset: 0x0001DF78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GetTriangleSphere(float3 pos0, float3 pos1, float3 pos2, out float3 sc, out float sr)
		{
			float3 @float = math.max(math.max(pos0, pos1), pos2);
			float3 float2 = math.min(math.min(pos0, pos1), pos2);
			sc = (float2 + @float) * 0.5f;
			sr = math.distance(float2, @float) * 0.5f;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 LocalToWorldMatrix(in float3 wpos, in quaternion wrot, in float3 wscl)
		{
			return Matrix4x4.TRS(wpos, wrot, wscl);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001FDF5 File Offset: 0x0001DFF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 WorldToLocalMatrix(in float3 wpos, in quaternion wrot, in float3 wscl)
		{
			return math.inverse(Matrix4x4.TRS(wpos, wrot, wscl));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001FE27 File Offset: 0x0001E027
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformPoint(in float3 pos, in float4x4 localToWorldMatrix)
		{
			return math.transform(localToWorldMatrix, pos);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001FE3C File Offset: 0x0001E03C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformVector(in float3 vec, in float4x4 localToWorldMatrix)
		{
			return math.mul(localToWorldMatrix, new float4(vec, 0f)).xyz;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001FE6C File Offset: 0x0001E06C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformDirection(in float3 dir, in float4x4 localToWorldMatrix)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(MathUtility.TransformVector(dir, localToWorldMatrix)) * num;
			}
			return dir;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TransformDistance(in float dist, in float4x4 localToWorldMatrix)
		{
			return math.csum(math.mul(localToWorldMatrix, new float4(dist, dist, dist, 0f)).xyz) / 3f;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TransformPositionNormalTangent(in float3 tpos, in quaternion trot, in float3 tscl, ref float3 pos, ref float3 nor, ref float3 tan)
		{
			pos *= tscl;
			pos = math.mul(trot, pos);
			pos += tpos;
			nor = math.mul(trot, nor);
			tan = math.mul(trot, tan);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001FF68 File Offset: 0x0001E168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TransformLength(float length, in float4x4 matrix)
		{
			return math.length(math.mul(matrix, new float4(length, length, length, 0f)).xyz) / 1.73205f;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001FE27 File Offset: 0x0001E027
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformPoint(in float3 pos, in float4x4 worldToLocalMatrix)
		{
			return math.transform(worldToLocalMatrix, pos);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformVector(in float3 vec, in float4x4 worldToLocalMatrix)
		{
			return math.mul(worldToLocalMatrix, new float4(vec, 0f)).xyz;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformVector(in float3 vec, in quaternion rot)
		{
			return math.mul(math.inverse(rot), vec);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformDirection(in float3 dir, in float4x4 worldToLocalMatrix)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(MathUtility.InverseTransformVector(dir, worldToLocalMatrix)) * num;
			}
			return dir;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00020022 File Offset: 0x0001E222
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Transform(in float4x4 fromLocalToWorldMatrix, in float4x4 toWorldToLocalMatrix)
		{
			return math.mul(toWorldToLocalMatrix, fromLocalToWorldMatrix);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00020038 File Offset: 0x0001E238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CompareMatrix(in float4x4 m1, in float4x4 m2)
		{
			bool4x4 bool4x = m1 == m2;
			return math.all(bool4x.c0) && math.all(bool4x.c1) && math.all(bool4x.c2) && math.all(bool4x.c3);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0002008C File Offset: 0x0001E28C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CompareTransform(in float3 pos1, in quaternion rot1, in float3 scl1, in float3 pos2, in quaternion rot2, in float3 scl2)
		{
			float3 @float = pos1;
			if (@float.Equals(pos2))
			{
				quaternion quaternion = rot1;
				if (quaternion.Equals(rot2))
				{
					@float = scl1;
					return @float.Equals(scl2);
				}
			}
			return false;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000200E0 File Offset: 0x0001E2E0
		public static bool IntersectSegmentTriangle(in float3 p, in float3 q, float3 a, float3 b, float3 c, bool doubleSide, out float u, out float v, out float w, out float t)
		{
			t = 0f;
			u = 0f;
			v = 0f;
			w = 0f;
			float3 x = b - a;
			float3 @float = c - a;
			float3 x2 = p - q;
			float3 float2 = math.cross(x, @float);
			float num = math.dot(x2, float2);
			if (math.abs(num) < 1E-09f)
			{
				return false;
			}
			if (!doubleSide)
			{
				if (num <= 0f)
				{
					return false;
				}
			}
			else if (num < 0f)
			{
				float2 = -float2;
				float3 float3 = b;
				b = c;
				c = float3;
				x = b - a;
				@float = c - a;
				num = math.dot(x2, float2);
			}
			float3 float4 = p - a;
			t = math.dot(float4, float2);
			if (t < 0f)
			{
				return false;
			}
			if (t > num)
			{
				return false;
			}
			float3 y = math.cross(x2, float4);
			v = math.dot(@float, y);
			if (v < 0f || v > num)
			{
				return false;
			}
			w = -math.dot(x, y);
			if (w < 0f || v + w > num)
			{
				return false;
			}
			float num2 = 1f / num;
			t *= num2;
			v *= num2;
			w *= num2;
			u = 1f - v - w;
			return true;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00020240 File Offset: 0x0001E440
		public static bool IntersectSegmentTriangle(in float3 p, in float3 q, float3 a, float3 b, float3 c)
		{
			float3 x = b - a;
			float3 @float = c - a;
			float3 x2 = p - q;
			float3 float2 = math.cross(x, @float);
			float num = math.dot(x2, float2);
			if (math.abs(num) < 1E-09f)
			{
				return false;
			}
			if (num < 0f)
			{
				float2 = -float2;
				float3 float3 = b;
				b = c;
				c = float3;
				x = b - a;
				@float = c - a;
				num = math.dot(x2, float2);
			}
			float3 float4 = p - a;
			float num2 = math.dot(float4, float2);
			if (num2 < 0f)
			{
				return false;
			}
			if (num2 > num)
			{
				return false;
			}
			float3 y = math.cross(x2, float4);
			float num3 = math.dot(@float, y);
			if (num3 < 0f || num3 > num)
			{
				return false;
			}
			float num4 = -math.dot(x, y);
			return num4 >= 0f && num3 + num4 <= num;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00020334 File Offset: 0x0001E534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IntersectPointPlaneDist(in float3 planePos, in float3 planeDir, in float3 pos, out float3 outPos)
		{
			float3 y = pos - planePos;
			float3 @float = MathUtility.Project(y, planeDir);
			float num = math.length(@float);
			if (math.dot(planeDir, y) < 0f)
			{
				outPos = pos - @float;
				return -num;
			}
			outPos = pos;
			return num;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00020398 File Offset: 0x0001E598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IntersectRaySphere(in float3 p, in float3 d, in float3 sc, in float sr, ref float t, ref float3 q)
		{
			float3 @float = p - sc;
			float num = math.dot(@float, d);
			float num2 = math.dot(@float, @float) - sr * sr;
			if (num2 > 0f && num > 0f)
			{
				return false;
			}
			float num3 = num * num - num2;
			if (num3 < 0f)
			{
				return false;
			}
			t = -num - math.sqrt(num3);
			if (t < 0f)
			{
				t = 0f;
			}
			q = p + t * d;
			return true;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00020434 File Offset: 0x0001E634
		public static float SqDistPointSegment(Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 vector3 = c - b;
			float num = Vector3.Dot(vector2, vector);
			if (num <= 0f)
			{
				return Vector3.Dot(vector2, vector2);
			}
			float num2 = Vector3.Dot(vector, vector);
			if (num >= num2)
			{
				return Vector3.Dot(vector3, vector3);
			}
			return Vector3.Dot(vector2, vector2) - num * num / num2;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00020495 File Offset: 0x0001E695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(float3 v)
		{
			return math.any(math.isnan(v));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000204A2 File Offset: 0x0001E6A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(float4 v)
		{
			return math.any(math.isnan(v));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000204AF File Offset: 0x0001E6AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(quaternion q)
		{
			return math.any(math.isnan(q.value));
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000204C4 File Offset: 0x0001E6C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcMass(float depth)
		{
			float num = 1f - depth;
			return 1f + num * num * 5f;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000204E8 File Offset: 0x0001E6E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcInverseMass(float friction)
		{
			float num = 1f + friction * 3f;
			return 1f / num;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0002050C File Offset: 0x0001E70C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcInverseMass(float friction, float depth)
		{
			float num = 1f;
			num += friction * 3f;
			float num2 = 1f - depth;
			num += num2 * num2 * 5f;
			return 1f / num;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00020544 File Offset: 0x0001E744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcInverseMass(float friction, float depth, bool fix)
		{
			if (!fix)
			{
				return MathUtility.CalcInverseMass(friction, depth);
			}
			return 0.01f;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00020558 File Offset: 0x0001E758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcSelfCollisionInverseMass(float friction, bool fix, float clothMass)
		{
			float num = fix ? 100f : (1f + friction * 10f);
			num += clothMass * 50f;
			return 1f / num;
		}
	}
}
