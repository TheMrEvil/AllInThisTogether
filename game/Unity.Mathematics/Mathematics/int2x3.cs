using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002E RID: 46
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2x3 : IEquatable<int2x3>, IFormattable
	{
		// Token: 0x060018D6 RID: 6358 RVA: 0x000442FD File Offset: 0x000424FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(int2 c0, int2 c1, int2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00044314 File Offset: 0x00042514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(int m00, int m01, int m02, int m10, int m11, int m12)
		{
			this.c0 = new int2(m00, m10);
			this.c1 = new int2(m01, m11);
			this.c2 = new int2(m02, m12);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00044340 File Offset: 0x00042540
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00044368 File Offset: 0x00042568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(bool v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v);
			this.c1 = math.select(new int2(0), new int2(1), v);
			this.c2 = math.select(new int2(0), new int2(1), v);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000443C0 File Offset: 0x000425C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(bool2x3 v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v.c0);
			this.c1 = math.select(new int2(0), new int2(1), v.c1);
			this.c2 = math.select(new int2(0), new int2(1), v.c2);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00044424 File Offset: 0x00042624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(uint v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0004444A File Offset: 0x0004264A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(uint2x3 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0004447F File Offset: 0x0004267F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(float v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000444A5 File Offset: 0x000426A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(float2x3 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000444DA File Offset: 0x000426DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(double v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00044500 File Offset: 0x00042700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(double2x3 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00044535 File Offset: 0x00042735
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2x3(int v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0004453D File Offset: 0x0004273D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(bool v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00044545 File Offset: 0x00042745
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(bool2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0004454D File Offset: 0x0004274D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(uint v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00044555 File Offset: 0x00042755
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(uint2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0004455D File Offset: 0x0004275D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(float v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00044565 File Offset: 0x00042765
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(float2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0004456D File Offset: 0x0004276D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(double v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00044575 File Offset: 0x00042775
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(double2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0004457D File Offset: 0x0004277D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator *(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000445B7 File Offset: 0x000427B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator *(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000445E2 File Offset: 0x000427E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator *(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0004460D File Offset: 0x0004280D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00044647 File Offset: 0x00042847
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00044672 File Offset: 0x00042872
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0004469D File Offset: 0x0004289D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000446D7 File Offset: 0x000428D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00044702 File Offset: 0x00042902
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0004472D File Offset: 0x0004292D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator /(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00044767 File Offset: 0x00042967
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator /(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00044792 File Offset: 0x00042992
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator /(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000447BD File Offset: 0x000429BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator %(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000447F7 File Offset: 0x000429F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator %(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00044822 File Offset: 0x00042A22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator %(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00044850 File Offset: 0x00042A50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ++(int2x3 val)
		{
			int2 @int = ++val.c0;
			val.c0 = @int;
			int2 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			int2 int3 = @int;
			@int = ++val.c2;
			val.c2 = @int;
			return new int2x3(int2, int3, @int);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000448B0 File Offset: 0x00042AB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator --(int2x3 val)
		{
			int2 @int = --val.c0;
			val.c0 = @int;
			int2 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			int2 int3 = @int;
			@int = --val.c2;
			val.c2 = @int;
			return new int2x3(int2, int3, @int);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00044910 File Offset: 0x00042B10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0004494A File Offset: 0x00042B4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00044975 File Offset: 0x00042B75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000449A0 File Offset: 0x00042BA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000449DA File Offset: 0x00042BDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00044A05 File Offset: 0x00042C05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00044A30 File Offset: 0x00042C30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00044A6A File Offset: 0x00042C6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00044A95 File Offset: 0x00042C95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00044AC0 File Offset: 0x00042CC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00044AFA File Offset: 0x00042CFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00044B25 File Offset: 0x00042D25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00044B50 File Offset: 0x00042D50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int2x3 val)
		{
			return new int2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00044B78 File Offset: 0x00042D78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int2x3 val)
		{
			return new int2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00044BA0 File Offset: 0x00042DA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator <<(int2x3 x, int n)
		{
			return new int2x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00044BCB File Offset: 0x00042DCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator >>(int2x3 x, int n)
		{
			return new int2x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00044BF6 File Offset: 0x00042DF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00044C30 File Offset: 0x00042E30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00044C5B File Offset: 0x00042E5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00044C86 File Offset: 0x00042E86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00044CC0 File Offset: 0x00042EC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00044CEB File Offset: 0x00042EEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00044D16 File Offset: 0x00042F16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ~(int2x3 val)
		{
			return new int2x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00044D3E File Offset: 0x00042F3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator &(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00044D78 File Offset: 0x00042F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator &(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00044DA3 File Offset: 0x00042FA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator &(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00044DCE File Offset: 0x00042FCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator |(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00044E08 File Offset: 0x00043008
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator |(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00044E33 File Offset: 0x00043033
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator |(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00044E5E File Offset: 0x0004305E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ^(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00044E98 File Offset: 0x00043098
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ^(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00044EC3 File Offset: 0x000430C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ^(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x170007CE RID: 1998
		public unsafe int2 this[int index]
		{
			get
			{
				fixed (int2x3* ptr = &this)
				{
					return ref *(int2*)(ptr + (IntPtr)index * (IntPtr)sizeof(int2) / (IntPtr)sizeof(int2x3));
				}
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00044F0B File Offset: 0x0004310B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00044F48 File Offset: 0x00043148
		public override bool Equals(object o)
		{
			if (o is int2x3)
			{
				int2x3 rhs = (int2x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00044F6D File Offset: 0x0004316D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00044F7C File Offset: 0x0004317C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0004500C File Offset: 0x0004320C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000B6 RID: 182
		public int2 c0;

		// Token: 0x040000B7 RID: 183
		public int2 c1;

		// Token: 0x040000B8 RID: 184
		public int2 c2;

		// Token: 0x040000B9 RID: 185
		public static readonly int2x3 zero;
	}
}
