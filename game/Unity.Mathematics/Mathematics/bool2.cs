using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000003 RID: 3
	[DebuggerTypeProxy(typeof(bool2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2 : IEquatable<bool2>
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2(bool x, bool y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2(bool2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002082 File Offset: 0x00000282
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2(bool v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002092 File Offset: 0x00000292
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2(bool v)
		{
			return new bool2(v);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000209A File Offset: 0x0000029A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020BD File Offset: 0x000002BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D6 File Offset: 0x000002D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(bool lhs, bool2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020EF File Offset: 0x000002EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002137 File Offset: 0x00000337
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(bool lhs, bool2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002156 File Offset: 0x00000356
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !(bool2 val)
		{
			return new bool2(!val.x, !val.y);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000216F File Offset: 0x0000036F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator &(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator &(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x && rhs, lhs.y && rhs);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021A7 File Offset: 0x000003A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator &(bool lhs, bool2 rhs)
		{
			return new bool2(lhs & rhs.x, lhs & rhs.y);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021BE File Offset: 0x000003BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator |(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021DF File Offset: 0x000003DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator |(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x || rhs, lhs.y || rhs);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F6 File Offset: 0x000003F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator |(bool lhs, bool2 rhs)
		{
			return new bool2(lhs | rhs.x, lhs | rhs.y);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000220D File Offset: 0x0000040D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ^(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000222E File Offset: 0x0000042E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ^(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002245 File Offset: 0x00000445
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ^(bool lhs, bool2 rhs)
		{
			return new bool2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000225C File Offset: 0x0000045C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000227B File Offset: 0x0000047B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000229A File Offset: 0x0000049A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022B9 File Offset: 0x000004B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022D8 File Offset: 0x000004D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022F7 File Offset: 0x000004F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002316 File Offset: 0x00000516
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002335 File Offset: 0x00000535
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002354 File Offset: 0x00000554
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002373 File Offset: 0x00000573
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002392 File Offset: 0x00000592
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023B1 File Offset: 0x000005B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023D0 File Offset: 0x000005D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023EF File Offset: 0x000005EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000240E File Offset: 0x0000060E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000242D File Offset: 0x0000062D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000244C File Offset: 0x0000064C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002465 File Offset: 0x00000665
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000247E File Offset: 0x0000067E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002497 File Offset: 0x00000697
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024B0 File Offset: 0x000006B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024C9 File Offset: 0x000006C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000024E2 File Offset: 0x000006E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024FB File Offset: 0x000006FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002514 File Offset: 0x00000714
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.x);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002527 File Offset: 0x00000727
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000253A File Offset: 0x0000073A
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002567 File Offset: 0x00000767
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002581 File Offset: 0x00000781
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.y);
			}
		}

		// Token: 0x1700001D RID: 29
		public unsafe bool this[int index]
		{
			get
			{
				fixed (bool2* ptr = &this)
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

		// Token: 0x06000036 RID: 54 RVA: 0x000025C5 File Offset: 0x000007C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025E8 File Offset: 0x000007E8
		public override bool Equals(object o)
		{
			if (o is bool2)
			{
				bool2 rhs = (bool2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000260D File Offset: 0x0000080D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000261A File Offset: 0x0000081A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2({0}, {1})", this.x, this.y);
		}

		// Token: 0x04000001 RID: 1
		[MarshalAs(UnmanagedType.U1)]
		public bool x;

		// Token: 0x04000002 RID: 2
		[MarshalAs(UnmanagedType.U1)]
		public bool y;

		// Token: 0x0200004D RID: 77
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002467 RID: 9319 RVA: 0x00067484 File Offset: 0x00065684
			public DebuggerProxy(bool2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x0400011E RID: 286
			public bool x;

			// Token: 0x0400011F RID: 287
			public bool y;
		}
	}
}
