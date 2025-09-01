using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002B RID: 43
	[DebuggerTypeProxy(typeof(half4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half4 : IEquatable<half4>, IFormattable
	{
		// Token: 0x06001675 RID: 5749 RVA: 0x0003F89A File Offset: 0x0003DA9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half y, half z, half w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0003F8B9 File Offset: 0x0003DAB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half y, half2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0003F8E1 File Offset: 0x0003DAE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half2 yz, half w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0003F909 File Offset: 0x0003DB09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0003F936 File Offset: 0x0003DB36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half2 xy, half z, half w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0003F95E File Offset: 0x0003DB5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half2 xy, half2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0003F990 File Offset: 0x0003DB90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half3 xyz, half w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0003F9BD File Offset: 0x0003DBBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0003F9EF File Offset: 0x0003DBEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0003FA0D File Offset: 0x0003DC0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(float v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
			this.w = (half)v;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0003FA40 File Offset: 0x0003DC40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(float4 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
			this.w = (half)v.w;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0003FA91 File Offset: 0x0003DC91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(double v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
			this.w = (half)v;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0003FAC4 File Offset: 0x0003DCC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(double4 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
			this.w = (half)v.w;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0003FB15 File Offset: 0x0003DD15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator half4(half v)
		{
			return new half4(v);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0003FB1D File Offset: 0x0003DD1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(float v)
		{
			return new half4(v);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0003FB25 File Offset: 0x0003DD25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(float4 v)
		{
			return new half4(v);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0003FB2D File Offset: 0x0003DD2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(double v)
		{
			return new half4(v);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0003FB35 File Offset: 0x0003DD35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(double4 v)
		{
			return new half4(v);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0003FB40 File Offset: 0x0003DD40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(half4 lhs, half4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0003FB96 File Offset: 0x0003DD96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(half4 lhs, half rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0003FBCD File Offset: 0x0003DDCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(half lhs, half4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0003FC04 File Offset: 0x0003DE04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(half4 lhs, half4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0003FC5A File Offset: 0x0003DE5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(half4 lhs, half rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0003FC91 File Offset: 0x0003DE91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(half lhs, half4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0003FCC8 File Offset: 0x0003DEC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0003FCE7 File Offset: 0x0003DEE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0003FD06 File Offset: 0x0003DF06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0003FD25 File Offset: 0x0003DF25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0003FD44 File Offset: 0x0003DF44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0003FD63 File Offset: 0x0003DF63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0003FD82 File Offset: 0x0003DF82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0003FDA1 File Offset: 0x0003DFA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0003FDC0 File Offset: 0x0003DFC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0003FDDF File Offset: 0x0003DFDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0003FDFE File Offset: 0x0003DFFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0003FE1D File Offset: 0x0003E01D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0003FE3C File Offset: 0x0003E03C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0003FE5B File Offset: 0x0003E05B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0003FE7A File Offset: 0x0003E07A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0003FE99 File Offset: 0x0003E099
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0003FEB8 File Offset: 0x0003E0B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0003FED7 File Offset: 0x0003E0D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0003FEF6 File Offset: 0x0003E0F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0003FF15 File Offset: 0x0003E115
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0003FF34 File Offset: 0x0003E134
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0003FF53 File Offset: 0x0003E153
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0003FF72 File Offset: 0x0003E172
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0003FF91 File Offset: 0x0003E191
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0003FFB0 File Offset: 0x0003E1B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0003FFCF File Offset: 0x0003E1CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0003FFEE File Offset: 0x0003E1EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0004000D File Offset: 0x0003E20D
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0004002C File Offset: 0x0003E22C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.w);
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

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0004005E File Offset: 0x0003E25E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0004007D File Offset: 0x0003E27D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0004009C File Offset: 0x0003E29C
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x000400BB File Offset: 0x0003E2BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.z);
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

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000400ED File Offset: 0x0003E2ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0004010C File Offset: 0x0003E30C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0004012B File Offset: 0x0003E32B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0004014A File Offset: 0x0003E34A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00040169 File Offset: 0x0003E369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00040188 File Offset: 0x0003E388
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000401A7 File Offset: 0x0003E3A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000401C6 File Offset: 0x0003E3C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000401E5 File Offset: 0x0003E3E5
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x00040204 File Offset: 0x0003E404
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.w);
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

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00040236 File Offset: 0x0003E436
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x00040255 File Offset: 0x0003E455
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00040274 File Offset: 0x0003E474
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00040293 File Offset: 0x0003E493
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000402B2 File Offset: 0x0003E4B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000402D1 File Offset: 0x0003E4D1
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x000402F0 File Offset: 0x0003E4F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.y);
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

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00040322 File Offset: 0x0003E522
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00040341 File Offset: 0x0003E541
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00040360 File Offset: 0x0003E560
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0004037F File Offset: 0x0003E57F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x0004039E File Offset: 0x0003E59E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000403BD File Offset: 0x0003E5BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000403DC File Offset: 0x0003E5DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x000403FB File Offset: 0x0003E5FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0004041A File Offset: 0x0003E61A
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00040439 File Offset: 0x0003E639
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.z);
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

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0004046B File Offset: 0x0003E66B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0004048A File Offset: 0x0003E68A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000404A9 File Offset: 0x0003E6A9
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x000404C8 File Offset: 0x0003E6C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.y);
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

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x000404FA File Offset: 0x0003E6FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00040519 File Offset: 0x0003E719
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00040538 File Offset: 0x0003E738
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00040557 File Offset: 0x0003E757
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00040576 File Offset: 0x0003E776
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00040595 File Offset: 0x0003E795
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000405B4 File Offset: 0x0003E7B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x000405D3 File Offset: 0x0003E7D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x000405F2 File Offset: 0x0003E7F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00040611 File Offset: 0x0003E811
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00040630 File Offset: 0x0003E830
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0004064F File Offset: 0x0003E84F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0004066E File Offset: 0x0003E86E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0004068D File Offset: 0x0003E88D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x000406AC File Offset: 0x0003E8AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000406CB File Offset: 0x0003E8CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x000406EA File Offset: 0x0003E8EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00040709 File Offset: 0x0003E909
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x00040728 File Offset: 0x0003E928
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.w);
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

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0004075A File Offset: 0x0003E95A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00040779 File Offset: 0x0003E979
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00040798 File Offset: 0x0003E998
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x000407B7 File Offset: 0x0003E9B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.z);
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

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x000407E9 File Offset: 0x0003E9E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00040808 File Offset: 0x0003EA08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00040827 File Offset: 0x0003EA27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00040846 File Offset: 0x0003EA46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00040865 File Offset: 0x0003EA65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00040884 File Offset: 0x0003EA84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x000408A3 File Offset: 0x0003EAA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x000408C2 File Offset: 0x0003EAC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x000408E1 File Offset: 0x0003EAE1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00040900 File Offset: 0x0003EB00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0004091F File Offset: 0x0003EB1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0004093E File Offset: 0x0003EB3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0004095D File Offset: 0x0003EB5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0004097C File Offset: 0x0003EB7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0004099B File Offset: 0x0003EB9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x000409BA File Offset: 0x0003EBBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000409D9 File Offset: 0x0003EBD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x000409F8 File Offset: 0x0003EBF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00040A17 File Offset: 0x0003EC17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00040A36 File Offset: 0x0003EC36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00040A55 File Offset: 0x0003EC55
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x00040A74 File Offset: 0x0003EC74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.w);
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

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00040AA6 File Offset: 0x0003ECA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00040AC5 File Offset: 0x0003ECC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00040AE4 File Offset: 0x0003ECE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00040B03 File Offset: 0x0003ED03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00040B22 File Offset: 0x0003ED22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00040B41 File Offset: 0x0003ED41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00040B60 File Offset: 0x0003ED60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00040B7F File Offset: 0x0003ED7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00040B9E File Offset: 0x0003ED9E
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x00040BBD File Offset: 0x0003EDBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.x);
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

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00040BEF File Offset: 0x0003EDEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00040C0E File Offset: 0x0003EE0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00040C2D File Offset: 0x0003EE2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00040C4C File Offset: 0x0003EE4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00040C6B File Offset: 0x0003EE6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00040C8A File Offset: 0x0003EE8A
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x00040CA9 File Offset: 0x0003EEA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.z);
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

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00040CDB File Offset: 0x0003EEDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00040CFA File Offset: 0x0003EEFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00040D19 File Offset: 0x0003EF19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00040D38 File Offset: 0x0003EF38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00040D57 File Offset: 0x0003EF57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00040D76 File Offset: 0x0003EF76
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x00040D95 File Offset: 0x0003EF95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.x);
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

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00040DC7 File Offset: 0x0003EFC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00040DE6 File Offset: 0x0003EFE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00040E05 File Offset: 0x0003F005
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00040E24 File Offset: 0x0003F024
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x00040E43 File Offset: 0x0003F043
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00040E62 File Offset: 0x0003F062
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00040E81 File Offset: 0x0003F081
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00040EA0 File Offset: 0x0003F0A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x00040EBF File Offset: 0x0003F0BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00040EDE File Offset: 0x0003F0DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00040EFD File Offset: 0x0003F0FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x00040F1C File Offset: 0x0003F11C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00040F3B File Offset: 0x0003F13B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00040F5A File Offset: 0x0003F15A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00040F79 File Offset: 0x0003F179
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00040F98 File Offset: 0x0003F198
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.w);
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

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00040FCA File Offset: 0x0003F1CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00040FE9 File Offset: 0x0003F1E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00041008 File Offset: 0x0003F208
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00041027 File Offset: 0x0003F227
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00041046 File Offset: 0x0003F246
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00041065 File Offset: 0x0003F265
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x00041084 File Offset: 0x0003F284
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.y);
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

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x000410B6 File Offset: 0x0003F2B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x000410D5 File Offset: 0x0003F2D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000410F4 File Offset: 0x0003F2F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x00041113 File Offset: 0x0003F313
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00041132 File Offset: 0x0003F332
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00041151 File Offset: 0x0003F351
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00041170 File Offset: 0x0003F370
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.w);
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

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000411A2 File Offset: 0x0003F3A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000411C1 File Offset: 0x0003F3C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000411E0 File Offset: 0x0003F3E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x000411FF File Offset: 0x0003F3FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0004121E File Offset: 0x0003F41E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0004123D File Offset: 0x0003F43D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0004125C File Offset: 0x0003F45C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0004127B File Offset: 0x0003F47B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0004129A File Offset: 0x0003F49A
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x000412B9 File Offset: 0x0003F4B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.x);
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

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000412EB File Offset: 0x0003F4EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0004130A File Offset: 0x0003F50A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00041329 File Offset: 0x0003F529
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00041348 File Offset: 0x0003F548
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00041367 File Offset: 0x0003F567
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00041386 File Offset: 0x0003F586
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x000413A5 File Offset: 0x0003F5A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x000413C4 File Offset: 0x0003F5C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x000413E3 File Offset: 0x0003F5E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x00041402 File Offset: 0x0003F602
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00041421 File Offset: 0x0003F621
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00041440 File Offset: 0x0003F640
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0004145F File Offset: 0x0003F65F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0004147E File Offset: 0x0003F67E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0004149D File Offset: 0x0003F69D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x000414BC File Offset: 0x0003F6BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x000414DB File Offset: 0x0003F6DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x000414FA File Offset: 0x0003F6FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00041519 File Offset: 0x0003F719
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00041538 File Offset: 0x0003F738
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00041557 File Offset: 0x0003F757
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x00041576 File Offset: 0x0003F776
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.y);
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

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x000415A8 File Offset: 0x0003F7A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x000415C7 File Offset: 0x0003F7C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x000415E6 File Offset: 0x0003F7E6
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x00041605 File Offset: 0x0003F805
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.x);
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

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00041637 File Offset: 0x0003F837
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00041656 File Offset: 0x0003F856
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00041675 File Offset: 0x0003F875
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00041694 File Offset: 0x0003F894
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000416B3 File Offset: 0x0003F8B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000416D2 File Offset: 0x0003F8D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000416F1 File Offset: 0x0003F8F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00041710 File Offset: 0x0003F910
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0004172F File Offset: 0x0003F92F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0004174E File Offset: 0x0003F94E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0004176D File Offset: 0x0003F96D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0004178C File Offset: 0x0003F98C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x000417AB File Offset: 0x0003F9AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x000417CA File Offset: 0x0003F9CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x000417E9 File Offset: 0x0003F9E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00041808 File Offset: 0x0003FA08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00041827 File Offset: 0x0003FA27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00041846 File Offset: 0x0003FA46
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x00041865 File Offset: 0x0003FA65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.z);
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

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00041897 File Offset: 0x0003FA97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000418B6 File Offset: 0x0003FAB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000418D5 File Offset: 0x0003FAD5
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x000418F4 File Offset: 0x0003FAF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.y);
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

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00041926 File Offset: 0x0003FB26
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00041945 File Offset: 0x0003FB45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00041964 File Offset: 0x0003FB64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00041983 File Offset: 0x0003FB83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x000419A2 File Offset: 0x0003FBA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x000419C1 File Offset: 0x0003FBC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x000419E0 File Offset: 0x0003FBE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000419FF File Offset: 0x0003FBFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00041A1E File Offset: 0x0003FC1E
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x00041A3D File Offset: 0x0003FC3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.z);
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

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00041A6F File Offset: 0x0003FC6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00041A8E File Offset: 0x0003FC8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00041AAD File Offset: 0x0003FCAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00041ACC File Offset: 0x0003FCCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00041AEB File Offset: 0x0003FCEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00041B0A File Offset: 0x0003FD0A
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x00041B29 File Offset: 0x0003FD29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.x);
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

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00041B5B File Offset: 0x0003FD5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00041B7A File Offset: 0x0003FD7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x00041B99 File Offset: 0x0003FD99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x00041BB8 File Offset: 0x0003FDB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00041BD7 File Offset: 0x0003FDD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00041BF6 File Offset: 0x0003FDF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00041C15 File Offset: 0x0003FE15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00041C34 File Offset: 0x0003FE34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00041C53 File Offset: 0x0003FE53
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00041C72 File Offset: 0x0003FE72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.y);
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

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00041CA4 File Offset: 0x0003FEA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00041CC3 File Offset: 0x0003FEC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00041CE2 File Offset: 0x0003FEE2
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x00041D01 File Offset: 0x0003FF01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.x);
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

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00041D33 File Offset: 0x0003FF33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00041D52 File Offset: 0x0003FF52
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00041D71 File Offset: 0x0003FF71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00041D90 File Offset: 0x0003FF90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00041DAF File Offset: 0x0003FFAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00041DCE File Offset: 0x0003FFCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00041DED File Offset: 0x0003FFED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x00041E0C File Offset: 0x0004000C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00041E2B File Offset: 0x0004002B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x00041E4A File Offset: 0x0004004A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00041E69 File Offset: 0x00040069
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00041E88 File Offset: 0x00040088
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00041EA7 File Offset: 0x000400A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00041EC6 File Offset: 0x000400C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00041EE5 File Offset: 0x000400E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00041F04 File Offset: 0x00040104
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00041F23 File Offset: 0x00040123
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00041F42 File Offset: 0x00040142
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00041F61 File Offset: 0x00040161
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00041F80 File Offset: 0x00040180
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00041F9F File Offset: 0x0004019F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00041FBE File Offset: 0x000401BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00041FDD File Offset: 0x000401DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00041FFC File Offset: 0x000401FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0004201B File Offset: 0x0004021B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0004203A File Offset: 0x0004023A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00042059 File Offset: 0x00040259
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x00042078 File Offset: 0x00040278
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00042091 File Offset: 0x00040291
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000420AA File Offset: 0x000402AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000420C3 File Offset: 0x000402C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.w);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000420DC File Offset: 0x000402DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000420F5 File Offset: 0x000402F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0004210E File Offset: 0x0004030E
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x00042127 File Offset: 0x00040327
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0004214D File Offset: 0x0004034D
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x00042166 File Offset: 0x00040366
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0004218C File Offset: 0x0004038C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000421A5 File Offset: 0x000403A5
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x000421BE File Offset: 0x000403BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000421E4 File Offset: 0x000403E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000421FD File Offset: 0x000403FD
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00042216 File Offset: 0x00040416
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0004223C File Offset: 0x0004043C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.x);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x00042255 File Offset: 0x00040455
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0004226E File Offset: 0x0004046E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x00042294 File Offset: 0x00040494
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x000422AD File Offset: 0x000404AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000422D3 File Offset: 0x000404D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.w);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000422EC File Offset: 0x000404EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00042305 File Offset: 0x00040505
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0004231E File Offset: 0x0004051E
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x00042337 File Offset: 0x00040537
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0004235D File Offset: 0x0004055D
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00042376 File Offset: 0x00040576
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0004239C File Offset: 0x0004059C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000423B5 File Offset: 0x000405B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000423CE File Offset: 0x000405CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000423E7 File Offset: 0x000405E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.w);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00042400 File Offset: 0x00040600
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x00042419 File Offset: 0x00040619
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0004243F File Offset: 0x0004063F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00042458 File Offset: 0x00040658
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00042471 File Offset: 0x00040671
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x0004248A File Offset: 0x0004068A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000424B0 File Offset: 0x000406B0
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x000424C9 File Offset: 0x000406C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000424EF File Offset: 0x000406EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.y);
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00042508 File Offset: 0x00040708
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x00042521 File Offset: 0x00040721
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00042547 File Offset: 0x00040747
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.w);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00042560 File Offset: 0x00040760
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00042579 File Offset: 0x00040779
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x00042592 File Offset: 0x00040792
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000425B8 File Offset: 0x000407B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000425D1 File Offset: 0x000407D1
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x000425EA File Offset: 0x000407EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00042610 File Offset: 0x00040810
		// (set) Token: 0x060017D8 RID: 6104 RVA: 0x00042629 File Offset: 0x00040829
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0004264F File Offset: 0x0004084F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00042668 File Offset: 0x00040868
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00042681 File Offset: 0x00040881
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x0004269A File Offset: 0x0004089A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x000426C0 File Offset: 0x000408C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x000426D9 File Offset: 0x000408D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x000426F2 File Offset: 0x000408F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0004270B File Offset: 0x0004090B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.w);
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00042724 File Offset: 0x00040924
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x0004273D File Offset: 0x0004093D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00042763 File Offset: 0x00040963
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x0004277C File Offset: 0x0004097C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x000427A2 File Offset: 0x000409A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.z);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x000427BB File Offset: 0x000409BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.w);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000427D4 File Offset: 0x000409D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.x);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x000427ED File Offset: 0x000409ED
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x00042806 File Offset: 0x00040A06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0004282C File Offset: 0x00040A2C
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x00042845 File Offset: 0x00040A45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0004286B File Offset: 0x00040A6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.w);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00042884 File Offset: 0x00040A84
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0004289D File Offset: 0x00040A9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x000428C3 File Offset: 0x00040AC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.y);
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000428DC File Offset: 0x00040ADC
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x000428F5 File Offset: 0x00040AF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0004291B File Offset: 0x00040B1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.w);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00042934 File Offset: 0x00040B34
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0004294D File Offset: 0x00040B4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00042973 File Offset: 0x00040B73
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0004298C File Offset: 0x00040B8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x000429B2 File Offset: 0x00040BB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.z);
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000429CB File Offset: 0x00040BCB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.w);
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000429E4 File Offset: 0x00040BE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.x);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x000429FD File Offset: 0x00040BFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.y);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00042A16 File Offset: 0x00040C16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.z);
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00042A2F File Offset: 0x00040C2F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.w);
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x00042A48 File Offset: 0x00040C48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.x);
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00042A5B File Offset: 0x00040C5B
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00042A6E File Offset: 0x00040C6E
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

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00042A88 File Offset: 0x00040C88
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x00042A9B File Offset: 0x00040C9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00042AB5 File Offset: 0x00040CB5
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x00042AC8 File Offset: 0x00040CC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00042AE2 File Offset: 0x00040CE2
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00042AF5 File Offset: 0x00040CF5
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00042B0F File Offset: 0x00040D0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.y);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00042B22 File Offset: 0x00040D22
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x00042B35 File Offset: 0x00040D35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00042B4F File Offset: 0x00040D4F
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x00042B62 File Offset: 0x00040D62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00042B7C File Offset: 0x00040D7C
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x00042B8F File Offset: 0x00040D8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00042BA9 File Offset: 0x00040DA9
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x00042BBC File Offset: 0x00040DBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00042BD6 File Offset: 0x00040DD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.z);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00042BE9 File Offset: 0x00040DE9
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x00042BFC File Offset: 0x00040DFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00042C16 File Offset: 0x00040E16
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x00042C29 File Offset: 0x00040E29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00042C43 File Offset: 0x00040E43
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x00042C56 File Offset: 0x00040E56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00042C70 File Offset: 0x00040E70
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x00042C83 File Offset: 0x00040E83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00042C9D File Offset: 0x00040E9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.w);
			}
		}

		// Token: 0x170007AF RID: 1967
		public unsafe half this[int index]
		{
			get
			{
				fixed (half4* ptr = &this)
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

		// Token: 0x0600181B RID: 6171 RVA: 0x00042CFC File Offset: 0x00040EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00042D58 File Offset: 0x00040F58
		public override bool Equals(object o)
		{
			if (o is half4)
			{
				half4 rhs = (half4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00042D7D File Offset: 0x00040F7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00042D8C File Offset: 0x00040F8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("half4({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00042DE4 File Offset: 0x00040FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("half4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000AA RID: 170
		public half x;

		// Token: 0x040000AB RID: 171
		public half y;

		// Token: 0x040000AC RID: 172
		public half z;

		// Token: 0x040000AD RID: 173
		public half w;

		// Token: 0x040000AE RID: 174
		public static readonly half4 zero;

		// Token: 0x0200005C RID: 92
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002472 RID: 9330 RVA: 0x0006765C File Offset: 0x0006585C
			public DebuggerProxy(half4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x04000153 RID: 339
			public half x;

			// Token: 0x04000154 RID: 340
			public half y;

			// Token: 0x04000155 RID: 341
			public half z;

			// Token: 0x04000156 RID: 342
			public half w;
		}
	}
}
