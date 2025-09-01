using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000029 RID: 41
	[DebuggerTypeProxy(typeof(half2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half2 : IEquatable<half2>, IFormattable
	{
		// Token: 0x060015A2 RID: 5538 RVA: 0x0003E080 File Offset: 0x0003C280
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(half x, half y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0003E090 File Offset: 0x0003C290
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(half2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0003E0AA File Offset: 0x0003C2AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(half v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0003E0BA File Offset: 0x0003C2BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(float v)
		{
			this.x = (half)v;
			this.y = (half)v;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(float2 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(double v)
		{
			this.x = (half)v;
			this.y = (half)v;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0003E112 File Offset: 0x0003C312
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(double2 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0003E136 File Offset: 0x0003C336
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator half2(half v)
		{
			return new half2(v);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0003E13E File Offset: 0x0003C33E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(float v)
		{
			return new half2(v);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0003E146 File Offset: 0x0003C346
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(float2 v)
		{
			return new half2(v);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0003E14E File Offset: 0x0003C34E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(double v)
		{
			return new half2(v);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0003E156 File Offset: 0x0003C356
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(double2 v)
		{
			return new half2(v);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0003E15E File Offset: 0x0003C35E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(half2 lhs, half2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0003E187 File Offset: 0x0003C387
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(half2 lhs, half rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0003E1A6 File Offset: 0x0003C3A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(half lhs, half2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0003E1C5 File Offset: 0x0003C3C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(half2 lhs, half2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0003E1EE File Offset: 0x0003C3EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(half2 lhs, half rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0003E20D File Offset: 0x0003C40D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(half lhs, half2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0003E22C File Offset: 0x0003C42C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0003E24B File Offset: 0x0003C44B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0003E26A File Offset: 0x0003C46A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0003E289 File Offset: 0x0003C489
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0003E2C7 File Offset: 0x0003C4C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x0003E2E6 File Offset: 0x0003C4E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0003E305 File Offset: 0x0003C505
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0003E324 File Offset: 0x0003C524
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0003E343 File Offset: 0x0003C543
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0003E362 File Offset: 0x0003C562
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0003E381 File Offset: 0x0003C581
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0003E3A0 File Offset: 0x0003C5A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0003E3BF File Offset: 0x0003C5BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0003E3DE File Offset: 0x0003C5DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0003E3FD File Offset: 0x0003C5FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0003E41C File Offset: 0x0003C61C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0003E435 File Offset: 0x0003C635
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0003E44E File Offset: 0x0003C64E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0003E467 File Offset: 0x0003C667
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0003E480 File Offset: 0x0003C680
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0003E499 File Offset: 0x0003C699
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0003E4B2 File Offset: 0x0003C6B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0003E4CB File Offset: 0x0003C6CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0003E4E4 File Offset: 0x0003C6E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.x);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0003E4F7 File Offset: 0x0003C6F7
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x0003E50A File Offset: 0x0003C70A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0003E524 File Offset: 0x0003C724
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x0003E537 File Offset: 0x0003C737
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0003E551 File Offset: 0x0003C751
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.y);
			}
		}

		// Token: 0x170005E8 RID: 1512
		public unsafe half this[int index]
		{
			get
			{
				fixed (half2* ptr = &this)
				{
					return ((half*)ptr)[index];
				}
			}
			set
			{
				fixed (half* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0003E5AD File Offset: 0x0003C7AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0003E5D8 File Offset: 0x0003C7D8
		public override bool Equals(object o)
		{
			if (o is half2)
			{
				half2 rhs = (half2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0003E5FD File Offset: 0x0003C7FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0003E60A File Offset: 0x0003C80A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("half2({0}, {1})", this.x, this.y);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0003E62C File Offset: 0x0003C82C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("half2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x040000A3 RID: 163
		public half x;

		// Token: 0x040000A4 RID: 164
		public half y;

		// Token: 0x040000A5 RID: 165
		public static readonly half2 zero;

		// Token: 0x0200005A RID: 90
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002470 RID: 9328 RVA: 0x00067610 File Offset: 0x00065810
			public DebuggerProxy(half2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x0400014E RID: 334
			public half x;

			// Token: 0x0400014F RID: 335
			public half y;
		}
	}
}
