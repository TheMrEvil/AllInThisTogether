using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000018 RID: 24
	[DebuggerTypeProxy(typeof(double4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4 : IEquatable<double4>, IFormattable
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00028E31 File Offset: 0x00027031
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00028E50 File Offset: 0x00027050
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double y, double2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00028E78 File Offset: 0x00027078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double2 yz, double w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00028EA0 File Offset: 0x000270A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00028ECD File Offset: 0x000270CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double2 xy, double z, double w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00028EF5 File Offset: 0x000270F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double2 xy, double2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00028F27 File Offset: 0x00027127
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double3 xyz, double w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00028F54 File Offset: 0x00027154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00028F86 File Offset: 0x00027186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00028FA4 File Offset: 0x000271A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(bool v)
		{
			this.x = (v ? 1.0 : 0.0);
			this.y = (v ? 1.0 : 0.0);
			this.z = (v ? 1.0 : 0.0);
			this.w = (v ? 1.0 : 0.0);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00029028 File Offset: 0x00027228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(bool4 v)
		{
			this.x = (v.x ? 1.0 : 0.0);
			this.y = (v.y ? 1.0 : 0.0);
			this.z = (v.z ? 1.0 : 0.0);
			this.w = (v.w ? 1.0 : 0.0);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000290BD File Offset: 0x000272BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(int v)
		{
			this.x = (double)v;
			this.y = (double)v;
			this.z = (double)v;
			this.w = (double)v;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000290DF File Offset: 0x000272DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(int4 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
			this.z = (double)v.z;
			this.w = (double)v.w;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00029115 File Offset: 0x00027315
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002913B File Offset: 0x0002733B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(uint4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00029175 File Offset: 0x00027375
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000291A8 File Offset: 0x000273A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(half4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000291F9 File Offset: 0x000273F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(float v)
		{
			this.x = (double)v;
			this.y = (double)v;
			this.z = (double)v;
			this.w = (double)v;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002921B File Offset: 0x0002741B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(float4 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
			this.z = (double)v.z;
			this.w = (double)v.w;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00029251 File Offset: 0x00027451
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(double v)
		{
			return new double4(v);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00029259 File Offset: 0x00027459
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4(bool v)
		{
			return new double4(v);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00029261 File Offset: 0x00027461
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4(bool4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00029269 File Offset: 0x00027469
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(int v)
		{
			return new double4(v);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00029271 File Offset: 0x00027471
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(int4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00029279 File Offset: 0x00027479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(uint v)
		{
			return new double4(v);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00029281 File Offset: 0x00027481
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(uint4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00029289 File Offset: 0x00027489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(half v)
		{
			return new double4(v);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00029291 File Offset: 0x00027491
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(half4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00029299 File Offset: 0x00027499
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(float v)
		{
			return new double4(v);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000292A1 File Offset: 0x000274A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(float4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000292A9 File Offset: 0x000274A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator *(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000292E4 File Offset: 0x000274E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator *(double4 lhs, double rhs)
		{
			return new double4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002930B File Offset: 0x0002750B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator *(double lhs, double4 rhs)
		{
			return new double4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00029332 File Offset: 0x00027532
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002936D File Offset: 0x0002756D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double4 lhs, double rhs)
		{
			return new double4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00029394 File Offset: 0x00027594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double lhs, double4 rhs)
		{
			return new double4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000293BB File Offset: 0x000275BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000293F6 File Offset: 0x000275F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double4 lhs, double rhs)
		{
			return new double4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002941D File Offset: 0x0002761D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double lhs, double4 rhs)
		{
			return new double4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00029444 File Offset: 0x00027644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator /(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002947F File Offset: 0x0002767F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator /(double4 lhs, double rhs)
		{
			return new double4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000294A6 File Offset: 0x000276A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator /(double lhs, double4 rhs)
		{
			return new double4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000294CD File Offset: 0x000276CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator %(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00029508 File Offset: 0x00027708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator %(double4 lhs, double rhs)
		{
			return new double4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002952F File Offset: 0x0002772F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator %(double lhs, double4 rhs)
		{
			return new double4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00029558 File Offset: 0x00027758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator ++(double4 val)
		{
			double num = val.x + 1.0;
			val.x = num;
			double num2 = num;
			num = val.y + 1.0;
			val.y = num;
			double num3 = num;
			num = val.z + 1.0;
			val.z = num;
			double num4 = num;
			num = val.w + 1.0;
			val.w = num;
			return new double4(num2, num3, num4, num);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000295C8 File Offset: 0x000277C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator --(double4 val)
		{
			double num = val.x - 1.0;
			val.x = num;
			double num2 = num;
			num = val.y - 1.0;
			val.y = num;
			double num3 = num;
			num = val.z - 1.0;
			val.z = num;
			double num4 = num;
			num = val.w - 1.0;
			val.w = num;
			return new double4(num2, num3, num4, num);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00029636 File Offset: 0x00027836
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00029675 File Offset: 0x00027875
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(double4 lhs, double rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x000296A0 File Offset: 0x000278A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(double lhs, double4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000296CC File Offset: 0x000278CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00029722 File Offset: 0x00027922
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(double4 lhs, double rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00029759 File Offset: 0x00027959
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(double lhs, double4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00029790 File Offset: 0x00027990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000297CF File Offset: 0x000279CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(double4 lhs, double rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000297FA File Offset: 0x000279FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(double lhs, double4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00029828 File Offset: 0x00027A28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002987E File Offset: 0x00027A7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(double4 lhs, double rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000298B5 File Offset: 0x00027AB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(double lhs, double4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000298EC File Offset: 0x00027AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double4 val)
		{
			return new double4(-val.x, -val.y, -val.z, -val.w);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002990F File Offset: 0x00027B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double4 val)
		{
			return new double4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002992E File Offset: 0x00027B2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002996D File Offset: 0x00027B6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(double4 lhs, double rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00029998 File Offset: 0x00027B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(double lhs, double4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000299C4 File Offset: 0x00027BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00029A1A File Offset: 0x00027C1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(double4 lhs, double rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00029A51 File Offset: 0x00027C51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(double lhs, double4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00029A88 File Offset: 0x00027C88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00029AA7 File Offset: 0x00027CA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00029AC6 File Offset: 0x00027CC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00029AE5 File Offset: 0x00027CE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00029B04 File Offset: 0x00027D04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00029B23 File Offset: 0x00027D23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00029B42 File Offset: 0x00027D42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00029B61 File Offset: 0x00027D61
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00029B80 File Offset: 0x00027D80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00029B9F File Offset: 0x00027D9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00029BBE File Offset: 0x00027DBE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00029BDD File Offset: 0x00027DDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00029BFC File Offset: 0x00027DFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00029C1B File Offset: 0x00027E1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00029C3A File Offset: 0x00027E3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00029C59 File Offset: 0x00027E59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00029C78 File Offset: 0x00027E78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00029C97 File Offset: 0x00027E97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00029CB6 File Offset: 0x00027EB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00029CD5 File Offset: 0x00027ED5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00029CF4 File Offset: 0x00027EF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00029D13 File Offset: 0x00027F13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00029D32 File Offset: 0x00027F32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00029D51 File Offset: 0x00027F51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00029D70 File Offset: 0x00027F70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00029D8F File Offset: 0x00027F8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00029DAE File Offset: 0x00027FAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00029DCD File Offset: 0x00027FCD
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x00029DEC File Offset: 0x00027FEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00029E1E File Offset: 0x0002801E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00029E3D File Offset: 0x0002803D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00029E5C File Offset: 0x0002805C
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x00029E7B File Offset: 0x0002807B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00029EAD File Offset: 0x000280AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00029ECC File Offset: 0x000280CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00029EEB File Offset: 0x000280EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00029F0A File Offset: 0x0002810A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00029F29 File Offset: 0x00028129
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00029F48 File Offset: 0x00028148
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00029F67 File Offset: 0x00028167
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00029F86 File Offset: 0x00028186
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00029FA5 File Offset: 0x000281A5
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00029FC4 File Offset: 0x000281C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00029FF6 File Offset: 0x000281F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0002A015 File Offset: 0x00028215
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0002A034 File Offset: 0x00028234
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0002A053 File Offset: 0x00028253
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0002A072 File Offset: 0x00028272
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0002A091 File Offset: 0x00028291
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0002A0B0 File Offset: 0x000282B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0002A0E2 File Offset: 0x000282E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0002A101 File Offset: 0x00028301
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0002A120 File Offset: 0x00028320
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0002A13F File Offset: 0x0002833F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0002A15E File Offset: 0x0002835E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0002A17D File Offset: 0x0002837D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002A19C File Offset: 0x0002839C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0002A1BB File Offset: 0x000283BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0002A1DA File Offset: 0x000283DA
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0002A1F9 File Offset: 0x000283F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0002A22B File Offset: 0x0002842B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0002A24A File Offset: 0x0002844A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0002A269 File Offset: 0x00028469
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0002A288 File Offset: 0x00028488
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0002A2BA File Offset: 0x000284BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0002A2D9 File Offset: 0x000284D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0002A2F8 File Offset: 0x000284F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0002A317 File Offset: 0x00028517
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0002A336 File Offset: 0x00028536
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0002A355 File Offset: 0x00028555
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0002A374 File Offset: 0x00028574
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0002A393 File Offset: 0x00028593
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0002A3B2 File Offset: 0x000285B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0002A3D1 File Offset: 0x000285D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0002A3F0 File Offset: 0x000285F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0002A40F File Offset: 0x0002860F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002A42E File Offset: 0x0002862E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0002A44D File Offset: 0x0002864D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002A46C File Offset: 0x0002866C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0002A48B File Offset: 0x0002868B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0002A4AA File Offset: 0x000286AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0002A4C9 File Offset: 0x000286C9
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0002A4E8 File Offset: 0x000286E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0002A51A File Offset: 0x0002871A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0002A539 File Offset: 0x00028739
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0002A558 File Offset: 0x00028758
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x0002A577 File Offset: 0x00028777
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0002A5A9 File Offset: 0x000287A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0002A5C8 File Offset: 0x000287C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0002A5E7 File Offset: 0x000287E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0002A606 File Offset: 0x00028806
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0002A625 File Offset: 0x00028825
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0002A644 File Offset: 0x00028844
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002A663 File Offset: 0x00028863
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0002A682 File Offset: 0x00028882
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002A6A1 File Offset: 0x000288A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0002A6C0 File Offset: 0x000288C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0002A6DF File Offset: 0x000288DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0002A6FE File Offset: 0x000288FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0002A71D File Offset: 0x0002891D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0002A73C File Offset: 0x0002893C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0002A75B File Offset: 0x0002895B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0002A77A File Offset: 0x0002897A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0002A799 File Offset: 0x00028999
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0002A7B8 File Offset: 0x000289B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0002A7D7 File Offset: 0x000289D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0002A7F6 File Offset: 0x000289F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002A815 File Offset: 0x00028A15
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x0002A834 File Offset: 0x00028A34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002A866 File Offset: 0x00028A66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0002A885 File Offset: 0x00028A85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0002A8A4 File Offset: 0x00028AA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0002A8C3 File Offset: 0x00028AC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0002A8E2 File Offset: 0x00028AE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0002A901 File Offset: 0x00028B01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0002A920 File Offset: 0x00028B20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0002A93F File Offset: 0x00028B3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0002A95E File Offset: 0x00028B5E
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x0002A97D File Offset: 0x00028B7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0002A9AF File Offset: 0x00028BAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0002A9CE File Offset: 0x00028BCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0002A9ED File Offset: 0x00028BED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0002AA0C File Offset: 0x00028C0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0002AA2B File Offset: 0x00028C2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0002AA4A File Offset: 0x00028C4A
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x0002AA69 File Offset: 0x00028C69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0002AA9B File Offset: 0x00028C9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002AABA File Offset: 0x00028CBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0002AAD9 File Offset: 0x00028CD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002AAF8 File Offset: 0x00028CF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0002AB17 File Offset: 0x00028D17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0002AB36 File Offset: 0x00028D36
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0002AB55 File Offset: 0x00028D55
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0002AB87 File Offset: 0x00028D87
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002ABA6 File Offset: 0x00028DA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0002ABC5 File Offset: 0x00028DC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0002ABE4 File Offset: 0x00028DE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0002AC03 File Offset: 0x00028E03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0002AC22 File Offset: 0x00028E22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0002AC41 File Offset: 0x00028E41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0002AC60 File Offset: 0x00028E60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0002AC7F File Offset: 0x00028E7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0002AC9E File Offset: 0x00028E9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0002ACBD File Offset: 0x00028EBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0002ACDC File Offset: 0x00028EDC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002ACFB File Offset: 0x00028EFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0002AD1A File Offset: 0x00028F1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0002AD39 File Offset: 0x00028F39
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x0002AD58 File Offset: 0x00028F58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002AD8A File Offset: 0x00028F8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0002ADA9 File Offset: 0x00028FA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0002ADC8 File Offset: 0x00028FC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0002ADE7 File Offset: 0x00028FE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002AE06 File Offset: 0x00029006
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0002AE25 File Offset: 0x00029025
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0002AE44 File Offset: 0x00029044
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0002AE76 File Offset: 0x00029076
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0002AE95 File Offset: 0x00029095
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0002AEB4 File Offset: 0x000290B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0002AED3 File Offset: 0x000290D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0002AEF2 File Offset: 0x000290F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0002AF11 File Offset: 0x00029111
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0002AF30 File Offset: 0x00029130
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0002AF62 File Offset: 0x00029162
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0002AF81 File Offset: 0x00029181
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0002AFA0 File Offset: 0x000291A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0002AFBF File Offset: 0x000291BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0002AFDE File Offset: 0x000291DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002AFFD File Offset: 0x000291FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002B01C File Offset: 0x0002921C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0002B03B File Offset: 0x0002923B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0002B05A File Offset: 0x0002925A
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0002B079 File Offset: 0x00029279
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0002B0AB File Offset: 0x000292AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0002B0CA File Offset: 0x000292CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0002B0E9 File Offset: 0x000292E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0002B108 File Offset: 0x00029308
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0002B127 File Offset: 0x00029327
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002B146 File Offset: 0x00029346
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002B165 File Offset: 0x00029365
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0002B184 File Offset: 0x00029384
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0002B1A3 File Offset: 0x000293A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0002B1C2 File Offset: 0x000293C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0002B1E1 File Offset: 0x000293E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0002B200 File Offset: 0x00029400
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0002B21F File Offset: 0x0002941F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0002B23E File Offset: 0x0002943E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0002B25D File Offset: 0x0002945D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0002B27C File Offset: 0x0002947C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0002B29B File Offset: 0x0002949B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0002B2BA File Offset: 0x000294BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0002B2D9 File Offset: 0x000294D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0002B2F8 File Offset: 0x000294F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0002B317 File Offset: 0x00029517
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0002B336 File Offset: 0x00029536
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0002B368 File Offset: 0x00029568
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0002B387 File Offset: 0x00029587
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0002B3A6 File Offset: 0x000295A6
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0002B3C5 File Offset: 0x000295C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0002B3F7 File Offset: 0x000295F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0002B416 File Offset: 0x00029616
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0002B435 File Offset: 0x00029635
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0002B454 File Offset: 0x00029654
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0002B473 File Offset: 0x00029673
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0002B492 File Offset: 0x00029692
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0002B4B1 File Offset: 0x000296B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0002B4D0 File Offset: 0x000296D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0002B4EF File Offset: 0x000296EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0002B50E File Offset: 0x0002970E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0002B52D File Offset: 0x0002972D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0002B54C File Offset: 0x0002974C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002B56B File Offset: 0x0002976B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002B58A File Offset: 0x0002978A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0002B5A9 File Offset: 0x000297A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0002B5C8 File Offset: 0x000297C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0002B5E7 File Offset: 0x000297E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002B606 File Offset: 0x00029806
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x0002B625 File Offset: 0x00029825
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0002B657 File Offset: 0x00029857
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0002B676 File Offset: 0x00029876
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0002B695 File Offset: 0x00029895
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x0002B6B4 File Offset: 0x000298B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0002B6E6 File Offset: 0x000298E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0002B705 File Offset: 0x00029905
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0002B724 File Offset: 0x00029924
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0002B743 File Offset: 0x00029943
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0002B762 File Offset: 0x00029962
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0002B781 File Offset: 0x00029981
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0002B7A0 File Offset: 0x000299A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0002B7BF File Offset: 0x000299BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0002B7DE File Offset: 0x000299DE
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x0002B7FD File Offset: 0x000299FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0002B82F File Offset: 0x00029A2F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0002B84E File Offset: 0x00029A4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0002B86D File Offset: 0x00029A6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0002B88C File Offset: 0x00029A8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0002B8AB File Offset: 0x00029AAB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0002B8CA File Offset: 0x00029ACA
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x0002B8E9 File Offset: 0x00029AE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0002B91B File Offset: 0x00029B1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0002B93A File Offset: 0x00029B3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0002B959 File Offset: 0x00029B59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0002B978 File Offset: 0x00029B78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0002B997 File Offset: 0x00029B97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0002B9B6 File Offset: 0x00029BB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0002B9D5 File Offset: 0x00029BD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0002B9F4 File Offset: 0x00029BF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0002BA13 File Offset: 0x00029C13
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0002BA32 File Offset: 0x00029C32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0002BA64 File Offset: 0x00029C64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002BA83 File Offset: 0x00029C83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0002BAA2 File Offset: 0x00029CA2
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0002BAC1 File Offset: 0x00029CC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0002BAF3 File Offset: 0x00029CF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0002BB12 File Offset: 0x00029D12
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0002BB31 File Offset: 0x00029D31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0002BB50 File Offset: 0x00029D50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0002BB6F File Offset: 0x00029D6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0002BB8E File Offset: 0x00029D8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0002BBAD File Offset: 0x00029DAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0002BBCC File Offset: 0x00029DCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0002BBEB File Offset: 0x00029DEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0002BC0A File Offset: 0x00029E0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0002BC29 File Offset: 0x00029E29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0002BC48 File Offset: 0x00029E48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0002BC67 File Offset: 0x00029E67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0002BC86 File Offset: 0x00029E86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002BCA5 File Offset: 0x00029EA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0002BCC4 File Offset: 0x00029EC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002BCE3 File Offset: 0x00029EE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0002BD02 File Offset: 0x00029F02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002BD21 File Offset: 0x00029F21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0002BD40 File Offset: 0x00029F40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0002BD5F File Offset: 0x00029F5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0002BD7E File Offset: 0x00029F7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0002BD9D File Offset: 0x00029F9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0002BDBC File Offset: 0x00029FBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0002BDDB File Offset: 0x00029FDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0002BDFA File Offset: 0x00029FFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0002BE19 File Offset: 0x0002A019
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0002BE38 File Offset: 0x0002A038
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0002BE51 File Offset: 0x0002A051
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0002BE6A File Offset: 0x0002A06A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0002BE83 File Offset: 0x0002A083
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.w);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0002BE9C File Offset: 0x0002A09C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0002BEB5 File Offset: 0x0002A0B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0002BECE File Offset: 0x0002A0CE
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0002BEE7 File Offset: 0x0002A0E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0002BF0D File Offset: 0x0002A10D
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0002BF26 File Offset: 0x0002A126
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0002BF4C File Offset: 0x0002A14C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0002BF65 File Offset: 0x0002A165
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0002BF7E File Offset: 0x0002A17E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0002BFBD File Offset: 0x0002A1BD
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0002BFD6 File Offset: 0x0002A1D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0002C015 File Offset: 0x0002A215
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0002C02E File Offset: 0x0002A22E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0002C054 File Offset: 0x0002A254
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0002C06D File Offset: 0x0002A26D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0002C093 File Offset: 0x0002A293
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0002C0AC File Offset: 0x0002A2AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002C0C5 File Offset: 0x0002A2C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002C0DE File Offset: 0x0002A2DE
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0002C0F7 File Offset: 0x0002A2F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002C11D File Offset: 0x0002A31D
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x0002C136 File Offset: 0x0002A336
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0002C15C File Offset: 0x0002A35C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0002C175 File Offset: 0x0002A375
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0002C18E File Offset: 0x0002A38E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0002C1A7 File Offset: 0x0002A3A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.w);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0002C1D9 File Offset: 0x0002A3D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0002C1FF File Offset: 0x0002A3FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.y);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0002C218 File Offset: 0x0002A418
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.z);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002C231 File Offset: 0x0002A431
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0002C24A File Offset: 0x0002A44A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0002C270 File Offset: 0x0002A470
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0002C289 File Offset: 0x0002A489
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0002C2AF File Offset: 0x0002A4AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.y);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0002C2C8 File Offset: 0x0002A4C8
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0002C2E1 File Offset: 0x0002A4E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0002C307 File Offset: 0x0002A507
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.w);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0002C320 File Offset: 0x0002A520
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.x);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002C339 File Offset: 0x0002A539
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0002C352 File Offset: 0x0002A552
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002C378 File Offset: 0x0002A578
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.z);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0002C391 File Offset: 0x0002A591
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0002C3AA File Offset: 0x0002A5AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0002C3E9 File Offset: 0x0002A5E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0002C40F File Offset: 0x0002A60F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.y);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0002C428 File Offset: 0x0002A628
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.z);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0002C441 File Offset: 0x0002A641
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0002C45A File Offset: 0x0002A65A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0002C480 File Offset: 0x0002A680
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.x);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002C499 File Offset: 0x0002A699
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.y);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0002C4B2 File Offset: 0x0002A6B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.z);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0002C4CB File Offset: 0x0002A6CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.w);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0002C4FD File Offset: 0x0002A6FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0002C523 File Offset: 0x0002A723
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0002C53C File Offset: 0x0002A73C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0002C562 File Offset: 0x0002A762
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.z);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0002C57B File Offset: 0x0002A77B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.w);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0002C594 File Offset: 0x0002A794
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.x);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0002C5AD File Offset: 0x0002A7AD
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0002C5C6 File Offset: 0x0002A7C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002C5EC File Offset: 0x0002A7EC
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0002C605 File Offset: 0x0002A805
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002C62B File Offset: 0x0002A82B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.w);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0002C644 File Offset: 0x0002A844
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0002C65D File Offset: 0x0002A85D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0002C683 File Offset: 0x0002A883
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.y);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002C69C File Offset: 0x0002A89C
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0002C6B5 File Offset: 0x0002A8B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002C6DB File Offset: 0x0002A8DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.w);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0002C6F4 File Offset: 0x0002A8F4
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0002C70D File Offset: 0x0002A90D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002C733 File Offset: 0x0002A933
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0002C74C File Offset: 0x0002A94C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0002C772 File Offset: 0x0002A972
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.z);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0002C78B File Offset: 0x0002A98B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.w);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0002C7A4 File Offset: 0x0002A9A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.x);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0002C7BD File Offset: 0x0002A9BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.y);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0002C7D6 File Offset: 0x0002A9D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.z);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002C7EF File Offset: 0x0002A9EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.w);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0002C808 File Offset: 0x0002AA08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.x);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0002C81B File Offset: 0x0002AA1B
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x0002C82E File Offset: 0x0002AA2E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0002C848 File Offset: 0x0002AA48
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x0002C85B File Offset: 0x0002AA5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0002C875 File Offset: 0x0002AA75
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x0002C888 File Offset: 0x0002AA88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0002C8A2 File Offset: 0x0002AAA2
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x0002C8B5 File Offset: 0x0002AAB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0002C8CF File Offset: 0x0002AACF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.y);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0002C8E2 File Offset: 0x0002AAE2
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0002C8F5 File Offset: 0x0002AAF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0002C90F File Offset: 0x0002AB0F
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0002C922 File Offset: 0x0002AB22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0002C93C File Offset: 0x0002AB3C
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0002C94F File Offset: 0x0002AB4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002C969 File Offset: 0x0002AB69
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0002C97C File Offset: 0x0002AB7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002C996 File Offset: 0x0002AB96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.z);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0002C9A9 File Offset: 0x0002ABA9
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0002C9BC File Offset: 0x0002ABBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0002C9D6 File Offset: 0x0002ABD6
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0002C9E9 File Offset: 0x0002ABE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0002CA03 File Offset: 0x0002AC03
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0002CA16 File Offset: 0x0002AC16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002CA30 File Offset: 0x0002AC30
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0002CA43 File Offset: 0x0002AC43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0002CA5D File Offset: 0x0002AC5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.w);
			}
		}

		// Token: 0x170003D7 RID: 983
		public unsafe double this[int index]
		{
			get
			{
				fixed (double4* ptr = &this)
				{
					return ((double*)ptr)[index];
				}
			}
			set
			{
				fixed (double* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002CAE4 File Offset: 0x0002ACE4
		public override bool Equals(object o)
		{
			if (o is double4)
			{
				double4 rhs = (double4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002CB09 File Offset: 0x0002AD09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002CB18 File Offset: 0x0002AD18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002CB70 File Offset: 0x0002AD70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400005C RID: 92
		public double x;

		// Token: 0x0400005D RID: 93
		public double y;

		// Token: 0x0400005E RID: 94
		public double z;

		// Token: 0x0400005F RID: 95
		public double w;

		// Token: 0x04000060 RID: 96
		public static readonly double4 zero;

		// Token: 0x02000056 RID: 86
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600246C RID: 9324 RVA: 0x00067554 File Offset: 0x00065754
			public DebuggerProxy(double4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x04000141 RID: 321
			public double x;

			// Token: 0x04000142 RID: 322
			public double y;

			// Token: 0x04000143 RID: 323
			public double z;

			// Token: 0x04000144 RID: 324
			public double w;
		}
	}
}
