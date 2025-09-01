using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002A RID: 42
	[DebuggerTypeProxy(typeof(half3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half3 : IEquatable<half3>, IFormattable
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x0003E652 File Offset: 0x0003C852
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half x, half y, half z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003E669 File Offset: 0x0003C869
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half x, half2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0003E68A File Offset: 0x0003C88A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half2 xy, half z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0003E6AB File Offset: 0x0003C8AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0003E6D1 File Offset: 0x0003C8D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(float v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0003E70E File Offset: 0x0003C90E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(float3 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0003E743 File Offset: 0x0003C943
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(double v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0003E769 File Offset: 0x0003C969
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(double3 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0003E79E File Offset: 0x0003C99E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator half3(half v)
		{
			return new half3(v);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0003E7A6 File Offset: 0x0003C9A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(float v)
		{
			return new half3(v);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0003E7AE File Offset: 0x0003C9AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(float3 v)
		{
			return new half3(v);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0003E7B6 File Offset: 0x0003C9B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(double v)
		{
			return new half3(v);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0003E7BE File Offset: 0x0003C9BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(double3 v)
		{
			return new half3(v);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0003E7C6 File Offset: 0x0003C9C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(half3 lhs, half3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0003E800 File Offset: 0x0003CA00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(half3 lhs, half rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0003E82B File Offset: 0x0003CA2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(half lhs, half3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0003E856 File Offset: 0x0003CA56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(half3 lhs, half3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0003E890 File Offset: 0x0003CA90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(half3 lhs, half rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0003E8BB File Offset: 0x0003CABB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(half lhs, half3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0003E8E6 File Offset: 0x0003CAE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0003E905 File Offset: 0x0003CB05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0003E924 File Offset: 0x0003CB24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0003E943 File Offset: 0x0003CB43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0003E962 File Offset: 0x0003CB62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0003E981 File Offset: 0x0003CB81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0003E9A0 File Offset: 0x0003CBA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0003E9BF File Offset: 0x0003CBBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0003E9DE File Offset: 0x0003CBDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0003E9FD File Offset: 0x0003CBFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0003EA1C File Offset: 0x0003CC1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0003EA3B File Offset: 0x0003CC3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0003EA5A File Offset: 0x0003CC5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0003EA79 File Offset: 0x0003CC79
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0003EA98 File Offset: 0x0003CC98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0003EAB7 File Offset: 0x0003CCB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0003EAD6 File Offset: 0x0003CCD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0003EAF5 File Offset: 0x0003CCF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0003EB14 File Offset: 0x0003CD14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x0003EB33 File Offset: 0x0003CD33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0003EB52 File Offset: 0x0003CD52
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x0003EB71 File Offset: 0x0003CD71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0003EB90 File Offset: 0x0003CD90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0003EBAF File Offset: 0x0003CDAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0003EBCE File Offset: 0x0003CDCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0003EBED File Offset: 0x0003CDED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0003EC0C File Offset: 0x0003CE0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0003EC2B File Offset: 0x0003CE2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0003EC4A File Offset: 0x0003CE4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0003EC69 File Offset: 0x0003CE69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0003EC88 File Offset: 0x0003CE88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0003ECA7 File Offset: 0x0003CEA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0003ECC6 File Offset: 0x0003CEC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0003ECE5 File Offset: 0x0003CEE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0003ED04 File Offset: 0x0003CF04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0003ED23 File Offset: 0x0003CF23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0003ED42 File Offset: 0x0003CF42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0003ED61 File Offset: 0x0003CF61
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0003ED80 File Offset: 0x0003CF80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0003ED9F File Offset: 0x0003CF9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0003EDBE File Offset: 0x0003CFBE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0003EDDD File Offset: 0x0003CFDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0003EDFC File Offset: 0x0003CFFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0003EE1B File Offset: 0x0003D01B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0003EE3A File Offset: 0x0003D03A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0003EE59 File Offset: 0x0003D059
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0003EE78 File Offset: 0x0003D078
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0003EE97 File Offset: 0x0003D097
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0003EEB6 File Offset: 0x0003D0B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0003EED5 File Offset: 0x0003D0D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0003EEF4 File Offset: 0x0003D0F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0003EF13 File Offset: 0x0003D113
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0003EF32 File Offset: 0x0003D132
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0003EF51 File Offset: 0x0003D151
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0003EF70 File Offset: 0x0003D170
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0003EF8F File Offset: 0x0003D18F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0003EFAE File Offset: 0x0003D1AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0003EFCD File Offset: 0x0003D1CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0003EFEC File Offset: 0x0003D1EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0003F00B File Offset: 0x0003D20B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0003F02A File Offset: 0x0003D22A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0003F049 File Offset: 0x0003D249
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0003F068 File Offset: 0x0003D268
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0003F087 File Offset: 0x0003D287
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0003F0A6 File Offset: 0x0003D2A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0003F0C5 File Offset: 0x0003D2C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0003F0E4 File Offset: 0x0003D2E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0003F103 File Offset: 0x0003D303
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0003F122 File Offset: 0x0003D322
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0003F141 File Offset: 0x0003D341
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0003F160 File Offset: 0x0003D360
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0003F17F File Offset: 0x0003D37F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0003F19E File Offset: 0x0003D39E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0003F1BD File Offset: 0x0003D3BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0003F1DC File Offset: 0x0003D3DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0003F1FB File Offset: 0x0003D3FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0003F21A File Offset: 0x0003D41A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0003F239 File Offset: 0x0003D439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0003F258 File Offset: 0x0003D458
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0003F277 File Offset: 0x0003D477
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0003F296 File Offset: 0x0003D496
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0003F2B5 File Offset: 0x0003D4B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0003F2CE File Offset: 0x0003D4CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0003F2E7 File Offset: 0x0003D4E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0003F300 File Offset: 0x0003D500
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0003F319 File Offset: 0x0003D519
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0003F332 File Offset: 0x0003D532
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0003F34B File Offset: 0x0003D54B
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

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0003F371 File Offset: 0x0003D571
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0003F38A File Offset: 0x0003D58A
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0003F3A3 File Offset: 0x0003D5A3
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

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0003F3C9 File Offset: 0x0003D5C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0003F3E2 File Offset: 0x0003D5E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0003F3FB File Offset: 0x0003D5FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0003F414 File Offset: 0x0003D614
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x0003F42D File Offset: 0x0003D62D
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

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0003F453 File Offset: 0x0003D653
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0003F46C File Offset: 0x0003D66C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0003F485 File Offset: 0x0003D685
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0003F49E File Offset: 0x0003D69E
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0003F4B7 File Offset: 0x0003D6B7
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

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0003F4DD File Offset: 0x0003D6DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.y);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0003F4F6 File Offset: 0x0003D6F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0003F50F File Offset: 0x0003D70F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.x);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0003F528 File Offset: 0x0003D728
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x0003F541 File Offset: 0x0003D741
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

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0003F567 File Offset: 0x0003D767
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.z);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0003F580 File Offset: 0x0003D780
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0003F599 File Offset: 0x0003D799
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

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0003F5BF File Offset: 0x0003D7BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0003F5D8 File Offset: 0x0003D7D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0003F5F1 File Offset: 0x0003D7F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0003F60A File Offset: 0x0003D80A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0003F623 File Offset: 0x0003D823
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0003F63C File Offset: 0x0003D83C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.x);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0003F64F File Offset: 0x0003D84F
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x0003F662 File Offset: 0x0003D862
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

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0003F67C File Offset: 0x0003D87C
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x0003F68F File Offset: 0x0003D88F
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

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0003F6A9 File Offset: 0x0003D8A9
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x0003F6BC File Offset: 0x0003D8BC
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

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0003F6D6 File Offset: 0x0003D8D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.y);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0003F6E9 File Offset: 0x0003D8E9
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0003F6FC File Offset: 0x0003D8FC
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

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0003F716 File Offset: 0x0003D916
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0003F729 File Offset: 0x0003D929
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

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0003F743 File Offset: 0x0003D943
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0003F756 File Offset: 0x0003D956
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

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0003F770 File Offset: 0x0003D970
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.z);
			}
		}

		// Token: 0x1700065E RID: 1630
		public unsafe half this[int index]
		{
			get
			{
				fixed (half3* ptr = &this)
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

		// Token: 0x06001670 RID: 5744 RVA: 0x0003F7CD File Offset: 0x0003D9CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0003F808 File Offset: 0x0003DA08
		public override bool Equals(object o)
		{
			if (o is half3)
			{
				half3 rhs = (half3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0003F82D File Offset: 0x0003DA2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0003F83A File Offset: 0x0003DA3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("half3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0003F867 File Offset: 0x0003DA67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("half3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x040000A6 RID: 166
		public half x;

		// Token: 0x040000A7 RID: 167
		public half y;

		// Token: 0x040000A8 RID: 168
		public half z;

		// Token: 0x040000A9 RID: 169
		public static readonly half3 zero;

		// Token: 0x0200005B RID: 91
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002471 RID: 9329 RVA: 0x00067630 File Offset: 0x00065830
			public DebuggerProxy(half3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000150 RID: 336
			public half x;

			// Token: 0x04000151 RID: 337
			public half y;

			// Token: 0x04000152 RID: 338
			public half z;
		}
	}
}
