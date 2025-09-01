using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000C RID: 12
	[DebuggerTypeProxy(typeof(bool4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4 : IEquatable<bool4>
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0001DBA9 File Offset: 0x0001BDA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool y, bool z, bool w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool y, bool2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool2 yz, bool w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001DC18 File Offset: 0x0001BE18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001DC45 File Offset: 0x0001BE45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool2 xy, bool z, bool w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001DC6D File Offset: 0x0001BE6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool2 xy, bool2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001DC9F File Offset: 0x0001BE9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool3 xyz, bool w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001DCCC File Offset: 0x0001BECC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001DCFE File Offset: 0x0001BEFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001DD1C File Offset: 0x0001BF1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4(bool v)
		{
			return new bool4(v);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001DD24 File Offset: 0x0001BF24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001DD63 File Offset: 0x0001BF63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001DD8E File Offset: 0x0001BF8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(bool lhs, bool4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001DDBC File Offset: 0x0001BFBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001DE12 File Offset: 0x0001C012
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001DE49 File Offset: 0x0001C049
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(bool lhs, bool4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001DE80 File Offset: 0x0001C080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !(bool4 val)
		{
			return new bool4(!val.x, !val.y, !val.z, !val.w);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001DEAB File Offset: 0x0001C0AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator &(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001DEE6 File Offset: 0x0001C0E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator &(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x && rhs, lhs.y && rhs, lhs.z && rhs, lhs.w && rhs);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001DF0D File Offset: 0x0001C10D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator &(bool lhs, bool4 rhs)
		{
			return new bool4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001DF34 File Offset: 0x0001C134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator |(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001DF6F File Offset: 0x0001C16F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator |(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x || rhs, lhs.y || rhs, lhs.z || rhs, lhs.w || rhs);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001DF96 File Offset: 0x0001C196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator |(bool lhs, bool4 rhs)
		{
			return new bool4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001DFBD File Offset: 0x0001C1BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ^(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ^(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001E01F File Offset: 0x0001C21F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ^(bool lhs, bool4 rhs)
		{
			return new bool4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001E046 File Offset: 0x0001C246
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0001E065 File Offset: 0x0001C265
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001E084 File Offset: 0x0001C284
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001E0A3 File Offset: 0x0001C2A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001E0C2 File Offset: 0x0001C2C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001E0E1 File Offset: 0x0001C2E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001E100 File Offset: 0x0001C300
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001E11F File Offset: 0x0001C31F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001E13E File Offset: 0x0001C33E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001E15D File Offset: 0x0001C35D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001E17C File Offset: 0x0001C37C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001E19B File Offset: 0x0001C39B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001E1BA File Offset: 0x0001C3BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001E1D9 File Offset: 0x0001C3D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001E1F8 File Offset: 0x0001C3F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001E217 File Offset: 0x0001C417
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001E236 File Offset: 0x0001C436
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001E255 File Offset: 0x0001C455
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001E274 File Offset: 0x0001C474
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001E293 File Offset: 0x0001C493
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001E2B2 File Offset: 0x0001C4B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001E2D1 File Offset: 0x0001C4D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001E30F File Offset: 0x0001C50F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001E32E File Offset: 0x0001C52E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001E34D File Offset: 0x0001C54D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001E36C File Offset: 0x0001C56C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001E38B File Offset: 0x0001C58B
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001E3AA File Offset: 0x0001C5AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.w);
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001E3DC File Offset: 0x0001C5DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001E3FB File Offset: 0x0001C5FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001E41A File Offset: 0x0001C61A
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0001E439 File Offset: 0x0001C639
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.z);
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001E46B File Offset: 0x0001C66B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001E48A File Offset: 0x0001C68A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001E4A9 File Offset: 0x0001C6A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001E4E7 File Offset: 0x0001C6E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001E506 File Offset: 0x0001C706
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001E525 File Offset: 0x0001C725
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001E544 File Offset: 0x0001C744
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001E563 File Offset: 0x0001C763
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0001E582 File Offset: 0x0001C782
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.w);
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001E5D3 File Offset: 0x0001C7D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001E5F2 File Offset: 0x0001C7F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001E611 File Offset: 0x0001C811
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001E630 File Offset: 0x0001C830
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001E64F File Offset: 0x0001C84F
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0001E66E File Offset: 0x0001C86E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.y);
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001E6BF File Offset: 0x0001C8BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001E6DE File Offset: 0x0001C8DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0001E6FD File Offset: 0x0001C8FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001E71C File Offset: 0x0001C91C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001E73B File Offset: 0x0001C93B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001E75A File Offset: 0x0001C95A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001E779 File Offset: 0x0001C979
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001E798 File Offset: 0x0001C998
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001E7B7 File Offset: 0x0001C9B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.z);
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001E7E9 File Offset: 0x0001C9E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001E808 File Offset: 0x0001CA08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001E827 File Offset: 0x0001CA27
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001E846 File Offset: 0x0001CA46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.y);
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001E878 File Offset: 0x0001CA78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001E897 File Offset: 0x0001CA97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001E8B6 File Offset: 0x0001CAB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001E8D5 File Offset: 0x0001CAD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001E913 File Offset: 0x0001CB13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001E932 File Offset: 0x0001CB32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001E951 File Offset: 0x0001CB51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001E970 File Offset: 0x0001CB70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001E98F File Offset: 0x0001CB8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001E9AE File Offset: 0x0001CBAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001E9CD File Offset: 0x0001CBCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001EA0B File Offset: 0x0001CC0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001EA2A File Offset: 0x0001CC2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001EA49 File Offset: 0x0001CC49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001EA68 File Offset: 0x0001CC68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001EA87 File Offset: 0x0001CC87
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0001EAA6 File Offset: 0x0001CCA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.w);
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001EAF7 File Offset: 0x0001CCF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001EB16 File Offset: 0x0001CD16
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001EB35 File Offset: 0x0001CD35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.z);
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001EB67 File Offset: 0x0001CD67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001EB86 File Offset: 0x0001CD86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0001EBA5 File Offset: 0x0001CDA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001EBE3 File Offset: 0x0001CDE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001EC02 File Offset: 0x0001CE02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001EC21 File Offset: 0x0001CE21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001EC40 File Offset: 0x0001CE40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001EC5F File Offset: 0x0001CE5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001EC7E File Offset: 0x0001CE7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001EC9D File Offset: 0x0001CE9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001ECBC File Offset: 0x0001CEBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001ECDB File Offset: 0x0001CEDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001ECFA File Offset: 0x0001CEFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001ED19 File Offset: 0x0001CF19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001ED38 File Offset: 0x0001CF38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001ED57 File Offset: 0x0001CF57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001ED76 File Offset: 0x0001CF76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001ED95 File Offset: 0x0001CF95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001EDD3 File Offset: 0x0001CFD3
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0001EDF2 File Offset: 0x0001CFF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.w);
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001EE24 File Offset: 0x0001D024
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001EE43 File Offset: 0x0001D043
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001EE62 File Offset: 0x0001D062
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001EE81 File Offset: 0x0001D081
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001EEBF File Offset: 0x0001D0BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001EEDE File Offset: 0x0001D0DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001EEFD File Offset: 0x0001D0FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001EF1C File Offset: 0x0001D11C
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0001EF3B File Offset: 0x0001D13B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.x);
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001EF6D File Offset: 0x0001D16D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001EF8C File Offset: 0x0001D18C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001EFAB File Offset: 0x0001D1AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001EFCA File Offset: 0x0001D1CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0001EFE9 File Offset: 0x0001D1E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001F008 File Offset: 0x0001D208
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0001F027 File Offset: 0x0001D227
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.z);
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001F059 File Offset: 0x0001D259
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001F078 File Offset: 0x0001D278
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001F097 File Offset: 0x0001D297
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001F0B6 File Offset: 0x0001D2B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001F0D5 File Offset: 0x0001D2D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x0001F113 File Offset: 0x0001D313
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.x);
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001F145 File Offset: 0x0001D345
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001F164 File Offset: 0x0001D364
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001F183 File Offset: 0x0001D383
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001F1A2 File Offset: 0x0001D3A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001F1C1 File Offset: 0x0001D3C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001F1FF File Offset: 0x0001D3FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001F21E File Offset: 0x0001D41E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001F23D File Offset: 0x0001D43D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001F25C File Offset: 0x0001D45C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001F27B File Offset: 0x0001D47B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001F29A File Offset: 0x0001D49A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001F2B9 File Offset: 0x0001D4B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001F2D8 File Offset: 0x0001D4D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001F2F7 File Offset: 0x0001D4F7
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x0001F316 File Offset: 0x0001D516
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.w);
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001F348 File Offset: 0x0001D548
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0001F367 File Offset: 0x0001D567
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001F386 File Offset: 0x0001D586
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001F3A5 File Offset: 0x0001D5A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001F3E3 File Offset: 0x0001D5E3
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0001F402 File Offset: 0x0001D602
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.y);
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001F434 File Offset: 0x0001D634
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001F453 File Offset: 0x0001D653
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001F472 File Offset: 0x0001D672
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001F491 File Offset: 0x0001D691
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001F4CF File Offset: 0x0001D6CF
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0001F4EE File Offset: 0x0001D6EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.w);
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001F520 File Offset: 0x0001D720
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001F53F File Offset: 0x0001D73F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0001F55E File Offset: 0x0001D75E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001F57D File Offset: 0x0001D77D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0001F59C File Offset: 0x0001D79C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001F5BB File Offset: 0x0001D7BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0001F5DA File Offset: 0x0001D7DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001F5F9 File Offset: 0x0001D7F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001F618 File Offset: 0x0001D818
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0001F637 File Offset: 0x0001D837
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.x);
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001F669 File Offset: 0x0001D869
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001F688 File Offset: 0x0001D888
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001F6A7 File Offset: 0x0001D8A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001F6C6 File Offset: 0x0001D8C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0001F6E5 File Offset: 0x0001D8E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001F704 File Offset: 0x0001D904
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0001F723 File Offset: 0x0001D923
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001F742 File Offset: 0x0001D942
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001F761 File Offset: 0x0001D961
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001F780 File Offset: 0x0001D980
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0001F79F File Offset: 0x0001D99F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001F7BE File Offset: 0x0001D9BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0001F7DD File Offset: 0x0001D9DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001F7FC File Offset: 0x0001D9FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001F81B File Offset: 0x0001DA1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001F83A File Offset: 0x0001DA3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001F859 File Offset: 0x0001DA59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001F878 File Offset: 0x0001DA78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0001F897 File Offset: 0x0001DA97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0001F8B6 File Offset: 0x0001DAB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001F8D5 File Offset: 0x0001DAD5
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.y);
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001F926 File Offset: 0x0001DB26
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0001F945 File Offset: 0x0001DB45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001F964 File Offset: 0x0001DB64
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0001F983 File Offset: 0x0001DB83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.x);
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001F9B5 File Offset: 0x0001DBB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001F9F3 File Offset: 0x0001DBF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001FA12 File Offset: 0x0001DC12
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0001FA31 File Offset: 0x0001DC31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001FA50 File Offset: 0x0001DC50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0001FA6F File Offset: 0x0001DC6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001FA8E File Offset: 0x0001DC8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0001FAAD File Offset: 0x0001DCAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001FACC File Offset: 0x0001DCCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001FAEB File Offset: 0x0001DCEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0001FB0A File Offset: 0x0001DD0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001FB29 File Offset: 0x0001DD29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0001FB48 File Offset: 0x0001DD48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001FB67 File Offset: 0x0001DD67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0001FB86 File Offset: 0x0001DD86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001FBA5 File Offset: 0x0001DDA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0001FBE3 File Offset: 0x0001DDE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.z);
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0001FC15 File Offset: 0x0001DE15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001FC34 File Offset: 0x0001DE34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0001FC53 File Offset: 0x0001DE53
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0001FC72 File Offset: 0x0001DE72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.y);
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001FCC3 File Offset: 0x0001DEC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0001FCE2 File Offset: 0x0001DEE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001FD01 File Offset: 0x0001DF01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001FD20 File Offset: 0x0001DF20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001FD3F File Offset: 0x0001DF3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0001FD5E File Offset: 0x0001DF5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001FD7D File Offset: 0x0001DF7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0001FDBB File Offset: 0x0001DFBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.z);
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0001FDED File Offset: 0x0001DFED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001FE0C File Offset: 0x0001E00C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0001FE2B File Offset: 0x0001E02B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001FE4A File Offset: 0x0001E04A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001FE69 File Offset: 0x0001E069
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001FE88 File Offset: 0x0001E088
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001FEA7 File Offset: 0x0001E0A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.x);
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001FED9 File Offset: 0x0001E0D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001FF17 File Offset: 0x0001E117
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001FF36 File Offset: 0x0001E136
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001FF55 File Offset: 0x0001E155
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001FF74 File Offset: 0x0001E174
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001FF93 File Offset: 0x0001E193
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0001FFB2 File Offset: 0x0001E1B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001FFD1 File Offset: 0x0001E1D1
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.y);
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00020022 File Offset: 0x0001E222
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00020041 File Offset: 0x0001E241
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00020060 File Offset: 0x0001E260
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0002007F File Offset: 0x0001E27F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.x);
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

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000200B1 File Offset: 0x0001E2B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x000200D0 File Offset: 0x0001E2D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000200EF File Offset: 0x0001E2EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002010E File Offset: 0x0001E30E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002012D File Offset: 0x0001E32D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002014C File Offset: 0x0001E34C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002016B File Offset: 0x0001E36B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002018A File Offset: 0x0001E38A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000201A9 File Offset: 0x0001E3A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000201C8 File Offset: 0x0001E3C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000201E7 File Offset: 0x0001E3E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00020206 File Offset: 0x0001E406
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00020225 File Offset: 0x0001E425
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00020244 File Offset: 0x0001E444
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00020263 File Offset: 0x0001E463
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00020282 File Offset: 0x0001E482
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000202A1 File Offset: 0x0001E4A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x000202C0 File Offset: 0x0001E4C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000202DF File Offset: 0x0001E4DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000202FE File Offset: 0x0001E4FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002031D File Offset: 0x0001E51D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002033C File Offset: 0x0001E53C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002035B File Offset: 0x0001E55B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002037A File Offset: 0x0001E57A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00020399 File Offset: 0x0001E599
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x000203B8 File Offset: 0x0001E5B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000203D7 File Offset: 0x0001E5D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000203F6 File Offset: 0x0001E5F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002040F File Offset: 0x0001E60F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00020428 File Offset: 0x0001E628
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00020441 File Offset: 0x0001E641
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.w);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002045A File Offset: 0x0001E65A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00020473 File Offset: 0x0001E673
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002048C File Offset: 0x0001E68C
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x000204A5 File Offset: 0x0001E6A5
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000204CB File Offset: 0x0001E6CB
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x000204E4 File Offset: 0x0001E6E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002050A File Offset: 0x0001E70A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.x);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00020523 File Offset: 0x0001E723
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0002053C File Offset: 0x0001E73C
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00020562 File Offset: 0x0001E762
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.z);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002057B File Offset: 0x0001E77B
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00020594 File Offset: 0x0001E794
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000205BA File Offset: 0x0001E7BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.x);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000205D3 File Offset: 0x0001E7D3
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x000205EC File Offset: 0x0001E7EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00020612 File Offset: 0x0001E812
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0002062B File Offset: 0x0001E82B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00020651 File Offset: 0x0001E851
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.w);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002066A File Offset: 0x0001E86A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00020683 File Offset: 0x0001E883
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002069C File Offset: 0x0001E89C
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x000206B5 File Offset: 0x0001E8B5
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

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000206DB File Offset: 0x0001E8DB
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x000206F4 File Offset: 0x0001E8F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002071A File Offset: 0x0001E91A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00020733 File Offset: 0x0001E933
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002074C File Offset: 0x0001E94C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.z);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00020765 File Offset: 0x0001E965
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.w);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002077E File Offset: 0x0001E97E
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00020797 File Offset: 0x0001E997
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

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x000207BD File Offset: 0x0001E9BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.y);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x000207D6 File Offset: 0x0001E9D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.z);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000207EF File Offset: 0x0001E9EF
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00020808 File Offset: 0x0001EA08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002082E File Offset: 0x0001EA2E
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00020847 File Offset: 0x0001EA47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002086D File Offset: 0x0001EA6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.y);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00020886 File Offset: 0x0001EA86
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0002089F File Offset: 0x0001EA9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000208C5 File Offset: 0x0001EAC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.w);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000208DE File Offset: 0x0001EADE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.x);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000208F7 File Offset: 0x0001EAF7
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00020910 File Offset: 0x0001EB10
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00020936 File Offset: 0x0001EB36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.z);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002094F File Offset: 0x0001EB4F
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00020968 File Offset: 0x0001EB68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002098E File Offset: 0x0001EB8E
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x000209A7 File Offset: 0x0001EBA7
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000209CD File Offset: 0x0001EBCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.y);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000209E6 File Offset: 0x0001EBE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.z);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000209FF File Offset: 0x0001EBFF
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x00020A18 File Offset: 0x0001EC18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00020A3E File Offset: 0x0001EC3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.x);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00020A57 File Offset: 0x0001EC57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.y);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00020A70 File Offset: 0x0001EC70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.z);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00020A89 File Offset: 0x0001EC89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.w);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00020AA2 File Offset: 0x0001ECA2
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00020ABB File Offset: 0x0001ECBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00020AE1 File Offset: 0x0001ECE1
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00020AFA File Offset: 0x0001ECFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00020B20 File Offset: 0x0001ED20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.z);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00020B39 File Offset: 0x0001ED39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.w);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00020B52 File Offset: 0x0001ED52
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.x);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00020B6B File Offset: 0x0001ED6B
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00020B84 File Offset: 0x0001ED84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00020BAA File Offset: 0x0001EDAA
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00020BC3 File Offset: 0x0001EDC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00020BE9 File Offset: 0x0001EDE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.w);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00020C02 File Offset: 0x0001EE02
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00020C1B File Offset: 0x0001EE1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00020C41 File Offset: 0x0001EE41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.y);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00020C5A File Offset: 0x0001EE5A
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00020C73 File Offset: 0x0001EE73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00020C99 File Offset: 0x0001EE99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.w);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00020CB2 File Offset: 0x0001EEB2
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00020CCB File Offset: 0x0001EECB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00020CF1 File Offset: 0x0001EEF1
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00020D0A File Offset: 0x0001EF0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00020D30 File Offset: 0x0001EF30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.z);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00020D49 File Offset: 0x0001EF49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.w);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00020D62 File Offset: 0x0001EF62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.x);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00020D7B File Offset: 0x0001EF7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.y);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00020D94 File Offset: 0x0001EF94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.z);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00020DAD File Offset: 0x0001EFAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.w);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00020DC6 File Offset: 0x0001EFC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.x);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00020DD9 File Offset: 0x0001EFD9
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00020DEC File Offset: 0x0001EFEC
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00020E06 File Offset: 0x0001F006
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00020E19 File Offset: 0x0001F019
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00020E33 File Offset: 0x0001F033
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00020E46 File Offset: 0x0001F046
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00020E60 File Offset: 0x0001F060
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00020E73 File Offset: 0x0001F073
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00020E8D File Offset: 0x0001F08D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.y);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00020EB3 File Offset: 0x0001F0B3
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00020ECD File Offset: 0x0001F0CD
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00020EFA File Offset: 0x0001F0FA
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00020F0D File Offset: 0x0001F10D
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00020F27 File Offset: 0x0001F127
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00020F3A File Offset: 0x0001F13A
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

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00020F54 File Offset: 0x0001F154
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.z);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00020F67 File Offset: 0x0001F167
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x00020F7A File Offset: 0x0001F17A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00020F94 File Offset: 0x0001F194
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00020FC1 File Offset: 0x0001F1C1
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00020FD4 File Offset: 0x0001F1D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00020FEE File Offset: 0x0001F1EE
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00021001 File Offset: 0x0001F201
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002101B File Offset: 0x0001F21B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.w);
			}
		}

		// Token: 0x170001EA RID: 490
		public unsafe bool this[int index]
		{
			get
			{
				fixed (bool4* ptr = &this)
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

		// Token: 0x06000A89 RID: 2697 RVA: 0x00021061 File Offset: 0x0001F261
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000210A0 File Offset: 0x0001F2A0
		public override bool Equals(object o)
		{
			if (o is bool4)
			{
				bool4 rhs = (bool4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000210C5 File Offset: 0x0001F2C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000210D4 File Offset: 0x0001F2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x0400002E RID: 46
		[MarshalAs(UnmanagedType.U1)]
		public bool x;

		// Token: 0x0400002F RID: 47
		[MarshalAs(UnmanagedType.U1)]
		public bool y;

		// Token: 0x04000030 RID: 48
		[MarshalAs(UnmanagedType.U1)]
		public bool z;

		// Token: 0x04000031 RID: 49
		[MarshalAs(UnmanagedType.U1)]
		public bool w;

		// Token: 0x02000053 RID: 83
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002469 RID: 9321 RVA: 0x000674D0 File Offset: 0x000656D0
			public DebuggerProxy(bool4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x04000138 RID: 312
			public bool x;

			// Token: 0x04000139 RID: 313
			public bool y;

			// Token: 0x0400013A RID: 314
			public bool z;

			// Token: 0x0400013B RID: 315
			public bool w;
		}
	}
}
