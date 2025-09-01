using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000008 RID: 8
	[DebuggerTypeProxy(typeof(bool3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3 : IEquatable<bool3>
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x0001BAA1 File Offset: 0x00019CA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool x, bool y, bool z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool x, bool2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001BAD9 File Offset: 0x00019CD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool2 xy, bool z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001BAFA File Offset: 0x00019CFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001BB20 File Offset: 0x00019D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001BB37 File Offset: 0x00019D37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3(bool v)
		{
			return new bool3(v);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001BB3F File Offset: 0x00019D3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001BB70 File Offset: 0x00019D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001BB92 File Offset: 0x00019D92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(bool lhs, bool3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001BBEE File Offset: 0x00019DEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001BC19 File Offset: 0x00019E19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(bool lhs, bool3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001BC44 File Offset: 0x00019E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !(bool3 val)
		{
			return new bool3(!val.x, !val.y, !val.z);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001BC66 File Offset: 0x00019E66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator &(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001BC94 File Offset: 0x00019E94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator &(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x && rhs, lhs.y && rhs, lhs.z && rhs);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001BCB3 File Offset: 0x00019EB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator &(bool lhs, bool3 rhs)
		{
			return new bool3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001BCD2 File Offset: 0x00019ED2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator |(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001BD00 File Offset: 0x00019F00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator |(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x || rhs, lhs.y || rhs, lhs.z || rhs);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001BD1F File Offset: 0x00019F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator |(bool lhs, bool3 rhs)
		{
			return new bool3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001BD3E File Offset: 0x00019F3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ^(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001BD6C File Offset: 0x00019F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ^(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001BD8B File Offset: 0x00019F8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ^(bool lhs, bool3 rhs)
		{
			return new bool3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001BDAA File Offset: 0x00019FAA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001BE07 File Offset: 0x0001A007
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001BE26 File Offset: 0x0001A026
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001BE45 File Offset: 0x0001A045
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001BE64 File Offset: 0x0001A064
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001BE83 File Offset: 0x0001A083
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001BEA2 File Offset: 0x0001A0A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001BEC1 File Offset: 0x0001A0C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001BEFF File Offset: 0x0001A0FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001BF1E File Offset: 0x0001A11E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001BF3D File Offset: 0x0001A13D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001BF5C File Offset: 0x0001A15C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001BF7B File Offset: 0x0001A17B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001BF9A File Offset: 0x0001A19A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001BFB9 File Offset: 0x0001A1B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001BFF7 File Offset: 0x0001A1F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001C016 File Offset: 0x0001A216
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001C035 File Offset: 0x0001A235
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001C054 File Offset: 0x0001A254
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001C073 File Offset: 0x0001A273
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001C092 File Offset: 0x0001A292
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001C0EF File Offset: 0x0001A2EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001C10E File Offset: 0x0001A30E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001C12D File Offset: 0x0001A32D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001C14C File Offset: 0x0001A34C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001C16B File Offset: 0x0001A36B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001C18A File Offset: 0x0001A38A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001C1A9 File Offset: 0x0001A3A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001C1E7 File Offset: 0x0001A3E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001C206 File Offset: 0x0001A406
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001C225 File Offset: 0x0001A425
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001C244 File Offset: 0x0001A444
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001C263 File Offset: 0x0001A463
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001C282 File Offset: 0x0001A482
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001C2A1 File Offset: 0x0001A4A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001C2DF File Offset: 0x0001A4DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001C2FE File Offset: 0x0001A4FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001C31D File Offset: 0x0001A51D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001C33C File Offset: 0x0001A53C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001C35B File Offset: 0x0001A55B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001C37A File Offset: 0x0001A57A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001C399 File Offset: 0x0001A599
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001C3D7 File Offset: 0x0001A5D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001C3F6 File Offset: 0x0001A5F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001C415 File Offset: 0x0001A615
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001C434 File Offset: 0x0001A634
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001C453 File Offset: 0x0001A653
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001C472 File Offset: 0x0001A672
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001C491 File Offset: 0x0001A691
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001C4CF File Offset: 0x0001A6CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001C4EE File Offset: 0x0001A6EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001C50D File Offset: 0x0001A70D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001C52C File Offset: 0x0001A72C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001C54B File Offset: 0x0001A74B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001C56A File Offset: 0x0001A76A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001C589 File Offset: 0x0001A789
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001C5C7 File Offset: 0x0001A7C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001C5E6 File Offset: 0x0001A7E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001C605 File Offset: 0x0001A805
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0001C624 File Offset: 0x0001A824
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001C643 File Offset: 0x0001A843
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001C662 File Offset: 0x0001A862
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001C681 File Offset: 0x0001A881
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001C6BF File Offset: 0x0001A8BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001C6DE File Offset: 0x0001A8DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0001C6FD File Offset: 0x0001A8FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001C71C File Offset: 0x0001A91C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001C73B File Offset: 0x0001A93B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001C75A File Offset: 0x0001A95A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001C779 File Offset: 0x0001A979
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0001C792 File Offset: 0x0001A992
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001C7AB File Offset: 0x0001A9AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001C7DD File Offset: 0x0001A9DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001C7F6 File Offset: 0x0001A9F6
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0001C80F File Offset: 0x0001AA0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001C835 File Offset: 0x0001AA35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001C84E File Offset: 0x0001AA4E
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0001C867 File Offset: 0x0001AA67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001C88D File Offset: 0x0001AA8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001C8A6 File Offset: 0x0001AAA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.x);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001C8BF File Offset: 0x0001AABF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001C917 File Offset: 0x0001AB17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001C930 File Offset: 0x0001AB30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001C949 File Offset: 0x0001AB49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001C962 File Offset: 0x0001AB62
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0001C97B File Offset: 0x0001AB7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001C9A1 File Offset: 0x0001ABA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001C9BA File Offset: 0x0001ABBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001C9D3 File Offset: 0x0001ABD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001C9EC File Offset: 0x0001ABEC
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0001CA05 File Offset: 0x0001AC05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001CA2B File Offset: 0x0001AC2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001CA44 File Offset: 0x0001AC44
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0001CA5D File Offset: 0x0001AC5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001CA83 File Offset: 0x0001AC83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.z);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001CAB5 File Offset: 0x0001ACB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.x);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001CACE File Offset: 0x0001ACCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.y);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0001CAE7 File Offset: 0x0001ACE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001CB00 File Offset: 0x0001AD00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.x);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001CB13 File Offset: 0x0001AD13
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0001CB26 File Offset: 0x0001AD26
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001CB40 File Offset: 0x0001AD40
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0001CB53 File Offset: 0x0001AD53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001CB6D File Offset: 0x0001AD6D
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0001CB80 File Offset: 0x0001AD80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001CB9A File Offset: 0x0001AD9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.y);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001CBAD File Offset: 0x0001ADAD
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001CBDA File Offset: 0x0001ADDA
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0001CBED File Offset: 0x0001ADED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001CC07 File Offset: 0x0001AE07
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001CC1A File Offset: 0x0001AE1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001CC34 File Offset: 0x0001AE34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.z);
			}
		}

		// Token: 0x17000096 RID: 150
		public unsafe bool this[int index]
		{
			get
			{
				fixed (bool3* ptr = &this)
				{
					return ((byte*)ptr)[index] != 0;
				}
			}
			set
			{
				fixed (bool* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001CC79 File Offset: 0x0001AE79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public override bool Equals(object o)
		{
			if (o is bool3)
			{
				bool3 rhs = (bool3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001CCCD File Offset: 0x0001AECD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001CCDA File Offset: 0x0001AEDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x04000022 RID: 34
		[MarshalAs(UnmanagedType.U1)]
		public bool x;

		// Token: 0x04000023 RID: 35
		[MarshalAs(UnmanagedType.U1)]
		public bool y;

		// Token: 0x04000024 RID: 36
		[MarshalAs(UnmanagedType.U1)]
		public bool z;

		// Token: 0x02000052 RID: 82
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002468 RID: 9320 RVA: 0x000674A4 File Offset: 0x000656A4
			public DebuggerProxy(bool3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000135 RID: 309
			public bool x;

			// Token: 0x04000136 RID: 310
			public bool y;

			// Token: 0x04000137 RID: 311
			public bool z;
		}
	}
}
