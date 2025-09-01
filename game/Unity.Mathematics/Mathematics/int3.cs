using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000030 RID: 48
	[DebuggerTypeProxy(typeof(int3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3 : IEquatable<int3>, IFormattable
	{
		// Token: 0x0600196C RID: 6508 RVA: 0x000462DD File Offset: 0x000444DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000462F4 File Offset: 0x000444F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int x, int2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00046315 File Offset: 0x00044515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int2 xy, int z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00046336 File Offset: 0x00044536
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0004635C File Offset: 0x0004455C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00046373 File Offset: 0x00044573
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(bool v)
		{
			this.x = (v ? 1 : 0);
			this.y = (v ? 1 : 0);
			this.z = (v ? 1 : 0);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0004639C File Offset: 0x0004459C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(bool3 v)
		{
			this.x = (v.x ? 1 : 0);
			this.y = (v.y ? 1 : 0);
			this.z = (v.z ? 1 : 0);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000463D4 File Offset: 0x000445D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(uint v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000463EB File Offset: 0x000445EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(uint3 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00046411 File Offset: 0x00044611
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(float v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0004642B File Offset: 0x0004462B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(float3 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00046454 File Offset: 0x00044654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(double v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0004646E File Offset: 0x0004466E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(double3 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00046497 File Offset: 0x00044697
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3(int v)
		{
			return new int3(v);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0004649F File Offset: 0x0004469F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(bool v)
		{
			return new int3(v);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000464A7 File Offset: 0x000446A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(bool3 v)
		{
			return new int3(v);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000464AF File Offset: 0x000446AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(uint v)
		{
			return new int3(v);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000464B7 File Offset: 0x000446B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(uint3 v)
		{
			return new int3(v);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000464BF File Offset: 0x000446BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(float v)
		{
			return new int3(v);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000464C7 File Offset: 0x000446C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(float3 v)
		{
			return new int3(v);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000464CF File Offset: 0x000446CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(double v)
		{
			return new int3(v);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000464D7 File Offset: 0x000446D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(double3 v)
		{
			return new int3(v);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000464DF File Offset: 0x000446DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator *(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0004650D File Offset: 0x0004470D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator *(int3 lhs, int rhs)
		{
			return new int3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0004652C File Offset: 0x0004472C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator *(int lhs, int3 rhs)
		{
			return new int3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0004654B File Offset: 0x0004474B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00046579 File Offset: 0x00044779
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int3 lhs, int rhs)
		{
			return new int3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00046598 File Offset: 0x00044798
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int lhs, int3 rhs)
		{
			return new int3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000465B7 File Offset: 0x000447B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000465E5 File Offset: 0x000447E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int3 lhs, int rhs)
		{
			return new int3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00046604 File Offset: 0x00044804
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int lhs, int3 rhs)
		{
			return new int3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00046623 File Offset: 0x00044823
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator /(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00046651 File Offset: 0x00044851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator /(int3 lhs, int rhs)
		{
			return new int3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00046670 File Offset: 0x00044870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator /(int lhs, int3 rhs)
		{
			return new int3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0004668F File Offset: 0x0004488F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator %(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000466BD File Offset: 0x000448BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator %(int3 lhs, int rhs)
		{
			return new int3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000466DC File Offset: 0x000448DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator %(int lhs, int3 rhs)
		{
			return new int3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000466FC File Offset: 0x000448FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ++(int3 val)
		{
			int num = val.x + 1;
			val.x = num;
			int num2 = num;
			num = val.y + 1;
			val.y = num;
			int num3 = num;
			num = val.z + 1;
			val.z = num;
			return new int3(num2, num3, num);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0004673C File Offset: 0x0004493C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator --(int3 val)
		{
			int num = val.x - 1;
			val.x = num;
			int num2 = num;
			num = val.y - 1;
			val.y = num;
			int num3 = num;
			num = val.z - 1;
			val.z = num;
			return new int3(num2, num3, num);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0004677B File Offset: 0x0004497B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000467AC File Offset: 0x000449AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(int3 lhs, int rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000467CE File Offset: 0x000449CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(int lhs, int3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000467F0 File Offset: 0x000449F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0004682A File Offset: 0x00044A2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(int3 lhs, int rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00046855 File Offset: 0x00044A55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(int lhs, int3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00046880 File Offset: 0x00044A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000468B1 File Offset: 0x00044AB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(int3 lhs, int rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000468D3 File Offset: 0x00044AD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(int lhs, int3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000468F5 File Offset: 0x00044AF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0004692F File Offset: 0x00044B2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(int3 lhs, int rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0004695A File Offset: 0x00044B5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(int lhs, int3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00046985 File Offset: 0x00044B85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int3 val)
		{
			return new int3(-val.x, -val.y, -val.z);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000469A1 File Offset: 0x00044BA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int3 val)
		{
			return new int3(val.x, val.y, val.z);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000469BA File Offset: 0x00044BBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator <<(int3 x, int n)
		{
			return new int3(x.x << n, x.y << n, x.z << n);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000469E2 File Offset: 0x00044BE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator >>(int3 x, int n)
		{
			return new int3(x.x >> n, x.y >> n, x.z >> n);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00046A0A File Offset: 0x00044C0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00046A3B File Offset: 0x00044C3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(int3 lhs, int rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00046A5D File Offset: 0x00044C5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(int lhs, int3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00046A7F File Offset: 0x00044C7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00046AB9 File Offset: 0x00044CB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(int3 lhs, int rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00046AE4 File Offset: 0x00044CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(int lhs, int3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00046B0F File Offset: 0x00044D0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ~(int3 val)
		{
			return new int3(~val.x, ~val.y, ~val.z);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00046B2B File Offset: 0x00044D2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator &(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00046B59 File Offset: 0x00044D59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator &(int3 lhs, int rhs)
		{
			return new int3(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00046B78 File Offset: 0x00044D78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator &(int lhs, int3 rhs)
		{
			return new int3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00046B97 File Offset: 0x00044D97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator |(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00046BC5 File Offset: 0x00044DC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator |(int3 lhs, int rhs)
		{
			return new int3(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00046BE4 File Offset: 0x00044DE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator |(int lhs, int3 rhs)
		{
			return new int3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00046C03 File Offset: 0x00044E03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ^(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00046C31 File Offset: 0x00044E31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ^(int3 lhs, int rhs)
		{
			return new int3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00046C50 File Offset: 0x00044E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ^(int lhs, int3 rhs)
		{
			return new int3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00046C6F File Offset: 0x00044E6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00046C8E File Offset: 0x00044E8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00046CAD File Offset: 0x00044EAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00046CCC File Offset: 0x00044ECC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00046CEB File Offset: 0x00044EEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00046D0A File Offset: 0x00044F0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x00046D29 File Offset: 0x00044F29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00046D48 File Offset: 0x00044F48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x00046D67 File Offset: 0x00044F67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00046D86 File Offset: 0x00044F86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00046DA5 File Offset: 0x00044FA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00046DC4 File Offset: 0x00044FC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00046DE3 File Offset: 0x00044FE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x00046E02 File Offset: 0x00045002
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00046E21 File Offset: 0x00045021
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00046E40 File Offset: 0x00045040
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00046E5F File Offset: 0x0004505F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00046E7E File Offset: 0x0004507E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00046E9D File Offset: 0x0004509D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00046EBC File Offset: 0x000450BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00046EDB File Offset: 0x000450DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00046EFA File Offset: 0x000450FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00046F19 File Offset: 0x00045119
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00046F38 File Offset: 0x00045138
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00046F57 File Offset: 0x00045157
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00046F76 File Offset: 0x00045176
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00046F95 File Offset: 0x00045195
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00046FB4 File Offset: 0x000451B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00046FD3 File Offset: 0x000451D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00046FF2 File Offset: 0x000451F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00047011 File Offset: 0x00045211
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00047030 File Offset: 0x00045230
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0004704F File Offset: 0x0004524F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0004706E File Offset: 0x0004526E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0004708D File Offset: 0x0004528D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x000470AC File Offset: 0x000452AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000470CB File Offset: 0x000452CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000470EA File Offset: 0x000452EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00047109 File Offset: 0x00045309
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00047128 File Offset: 0x00045328
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00047147 File Offset: 0x00045347
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00047166 File Offset: 0x00045366
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00047185 File Offset: 0x00045385
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x000471A4 File Offset: 0x000453A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000471C3 File Offset: 0x000453C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x000471E2 File Offset: 0x000453E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00047201 File Offset: 0x00045401
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00047220 File Offset: 0x00045420
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0004723F File Offset: 0x0004543F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0004725E File Offset: 0x0004545E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0004727D File Offset: 0x0004547D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0004729C File Offset: 0x0004549C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000472BB File Offset: 0x000454BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000472DA File Offset: 0x000454DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000472F9 File Offset: 0x000454F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00047318 File Offset: 0x00045518
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00047337 File Offset: 0x00045537
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00047356 File Offset: 0x00045556
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x00047375 File Offset: 0x00045575
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00047394 File Offset: 0x00045594
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x000473B3 File Offset: 0x000455B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x000473D2 File Offset: 0x000455D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x000473F1 File Offset: 0x000455F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00047410 File Offset: 0x00045610
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0004742F File Offset: 0x0004562F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0004744E File Offset: 0x0004564E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0004746D File Offset: 0x0004566D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0004748C File Offset: 0x0004568C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x000474AB File Offset: 0x000456AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000474CA File Offset: 0x000456CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000474E9 File Offset: 0x000456E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00047508 File Offset: 0x00045708
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00047527 File Offset: 0x00045727
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00047546 File Offset: 0x00045746
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00047565 File Offset: 0x00045765
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00047584 File Offset: 0x00045784
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x000475A3 File Offset: 0x000457A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x000475C2 File Offset: 0x000457C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000475E1 File Offset: 0x000457E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00047600 File Offset: 0x00045800
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0004761F File Offset: 0x0004581F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0004763E File Offset: 0x0004583E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00047657 File Offset: 0x00045857
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00047670 File Offset: 0x00045870
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00047689 File Offset: 0x00045889
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000476A2 File Offset: 0x000458A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000476BB File Offset: 0x000458BB
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x000476D4 File Offset: 0x000458D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x000476FA File Offset: 0x000458FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00047713 File Offset: 0x00045913
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x0004772C File Offset: 0x0004592C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00047752 File Offset: 0x00045952
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0004776B File Offset: 0x0004596B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.x);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00047784 File Offset: 0x00045984
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0004779D File Offset: 0x0004599D
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000477B6 File Offset: 0x000459B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000477DC File Offset: 0x000459DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x000477F5 File Offset: 0x000459F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0004780E File Offset: 0x00045A0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x00047827 File Offset: 0x00045A27
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00047840 File Offset: 0x00045A40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00047866 File Offset: 0x00045A66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0004787F File Offset: 0x00045A7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00047898 File Offset: 0x00045A98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000478B1 File Offset: 0x00045AB1
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x000478CA File Offset: 0x00045ACA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000478F0 File Offset: 0x00045AF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00047909 File Offset: 0x00045B09
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00047922 File Offset: 0x00045B22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x00047948 File Offset: 0x00045B48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00047961 File Offset: 0x00045B61
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0004797A File Offset: 0x00045B7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.x);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00047993 File Offset: 0x00045B93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.y);
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x000479AC File Offset: 0x00045BAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000479C5 File Offset: 0x00045BC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.x);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000479D8 File Offset: 0x00045BD8
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x000479EB File Offset: 0x00045BEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00047A05 File Offset: 0x00045C05
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x00047A18 File Offset: 0x00045C18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x00047A32 File Offset: 0x00045C32
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x00047A45 File Offset: 0x00045C45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00047A5F File Offset: 0x00045C5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.y);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x00047A72 File Offset: 0x00045C72
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x00047A85 File Offset: 0x00045C85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x00047A9F File Offset: 0x00045C9F
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x00047AB2 File Offset: 0x00045CB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00047ACC File Offset: 0x00045CCC
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x00047ADF File Offset: 0x00045CDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00047AF9 File Offset: 0x00045CF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.z);
			}
		}

		// Token: 0x17000845 RID: 2117
		public unsafe int this[int index]
		{
			get
			{
				fixed (int3* ptr = &this)
				{
					return ((int*)ptr)[index];
				}
			}
			set
			{
				fixed (int* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00047B44 File Offset: 0x00045D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00047B74 File Offset: 0x00045D74
		public override bool Equals(object o)
		{
			if (o is int3)
			{
				int3 rhs = (int3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00047B99 File Offset: 0x00045D99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00047BA6 File Offset: 0x00045DA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00047BD3 File Offset: 0x00045DD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x040000BF RID: 191
		public int x;

		// Token: 0x040000C0 RID: 192
		public int y;

		// Token: 0x040000C1 RID: 193
		public int z;

		// Token: 0x040000C2 RID: 194
		public static readonly int3 zero;

		// Token: 0x0200005E RID: 94
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002474 RID: 9332 RVA: 0x000676B4 File Offset: 0x000658B4
			public DebuggerProxy(int3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000159 RID: 345
			public int x;

			// Token: 0x0400015A RID: 346
			public int y;

			// Token: 0x0400015B RID: 347
			public int z;
		}
	}
}
