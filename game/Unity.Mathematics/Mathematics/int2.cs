using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002C RID: 44
	[DebuggerTypeProxy(typeof(int2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2 : IEquatable<int2>, IFormattable
	{
		// Token: 0x06001820 RID: 6176 RVA: 0x00042E41 File Offset: 0x00041041
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00042E51 File Offset: 0x00041051
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(int2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00042E6B File Offset: 0x0004106B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(int v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00042E7B File Offset: 0x0004107B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(bool v)
		{
			this.x = (v ? 1 : 0);
			this.y = (v ? 1 : 0);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00042E97 File Offset: 0x00041097
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(bool2 v)
		{
			this.x = (v.x ? 1 : 0);
			this.y = (v.y ? 1 : 0);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00042EBD File Offset: 0x000410BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(uint v)
		{
			this.x = (int)v;
			this.y = (int)v;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00042ECD File Offset: 0x000410CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(uint2 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00042EE7 File Offset: 0x000410E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(float v)
		{
			this.x = (int)v;
			this.y = (int)v;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00042EF9 File Offset: 0x000410F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(float2 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00042F15 File Offset: 0x00041115
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(double v)
		{
			this.x = (int)v;
			this.y = (int)v;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00042F27 File Offset: 0x00041127
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(double2 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00042F43 File Offset: 0x00041143
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2(int v)
		{
			return new int2(v);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00042F4B File Offset: 0x0004114B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(bool v)
		{
			return new int2(v);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00042F53 File Offset: 0x00041153
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(bool2 v)
		{
			return new int2(v);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00042F5B File Offset: 0x0004115B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(uint v)
		{
			return new int2(v);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00042F63 File Offset: 0x00041163
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(uint2 v)
		{
			return new int2(v);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00042F6B File Offset: 0x0004116B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(float v)
		{
			return new int2(v);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00042F73 File Offset: 0x00041173
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(float2 v)
		{
			return new int2(v);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00042F7B File Offset: 0x0004117B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(double v)
		{
			return new int2(v);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00042F83 File Offset: 0x00041183
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(double2 v)
		{
			return new int2(v);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00042F8B File Offset: 0x0004118B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00042FAC File Offset: 0x000411AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int2 lhs, int rhs)
		{
			return new int2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00042FC3 File Offset: 0x000411C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int lhs, int2 rhs)
		{
			return new int2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00042FDA File Offset: 0x000411DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00042FFB File Offset: 0x000411FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 lhs, int rhs)
		{
			return new int2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00043012 File Offset: 0x00041212
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int lhs, int2 rhs)
		{
			return new int2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00043029 File Offset: 0x00041229
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0004304A File Offset: 0x0004124A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 lhs, int rhs)
		{
			return new int2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00043061 File Offset: 0x00041261
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int lhs, int2 rhs)
		{
			return new int2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00043078 File Offset: 0x00041278
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00043099 File Offset: 0x00041299
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int2 lhs, int rhs)
		{
			return new int2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000430B0 File Offset: 0x000412B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int lhs, int2 rhs)
		{
			return new int2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000430C7 File Offset: 0x000412C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000430E8 File Offset: 0x000412E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int2 lhs, int rhs)
		{
			return new int2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000430FF File Offset: 0x000412FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int lhs, int2 rhs)
		{
			return new int2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00043118 File Offset: 0x00041318
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ++(int2 val)
		{
			int num = val.x + 1;
			val.x = num;
			int num2 = num;
			num = val.y + 1;
			val.y = num;
			return new int2(num2, num);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00043148 File Offset: 0x00041348
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator --(int2 val)
		{
			int num = val.x - 1;
			val.x = num;
			int num2 = num;
			num = val.y - 1;
			val.y = num;
			return new int2(num2, num);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00043178 File Offset: 0x00041378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0004319B File Offset: 0x0004139B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(int2 lhs, int rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000431B4 File Offset: 0x000413B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(int lhs, int2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000431CD File Offset: 0x000413CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000431F6 File Offset: 0x000413F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(int2 lhs, int rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00043215 File Offset: 0x00041415
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(int lhs, int2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00043234 File Offset: 0x00041434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00043257 File Offset: 0x00041457
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(int2 lhs, int rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00043270 File Offset: 0x00041470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(int lhs, int2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00043289 File Offset: 0x00041489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000432B2 File Offset: 0x000414B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(int2 lhs, int rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000432D1 File Offset: 0x000414D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(int lhs, int2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000432F0 File Offset: 0x000414F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 val)
		{
			return new int2(-val.x, -val.y);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00043305 File Offset: 0x00041505
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 val)
		{
			return new int2(val.x, val.y);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00043318 File Offset: 0x00041518
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator <<(int2 x, int n)
		{
			return new int2(x.x << n, x.y << n);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00043335 File Offset: 0x00041535
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator >>(int2 x, int n)
		{
			return new int2(x.x >> n, x.y >> n);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00043352 File Offset: 0x00041552
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00043375 File Offset: 0x00041575
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(int2 lhs, int rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0004338E File Offset: 0x0004158E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(int lhs, int2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000433A7 File Offset: 0x000415A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000433D0 File Offset: 0x000415D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(int2 lhs, int rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000433EF File Offset: 0x000415EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(int lhs, int2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0004340E File Offset: 0x0004160E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ~(int2 val)
		{
			return new int2(~val.x, ~val.y);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00043423 File Offset: 0x00041623
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator &(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00043444 File Offset: 0x00041644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator &(int2 lhs, int rhs)
		{
			return new int2(lhs.x & rhs, lhs.y & rhs);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0004345B File Offset: 0x0004165B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator &(int lhs, int2 rhs)
		{
			return new int2(lhs & rhs.x, lhs & rhs.y);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00043472 File Offset: 0x00041672
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator |(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00043493 File Offset: 0x00041693
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator |(int2 lhs, int rhs)
		{
			return new int2(lhs.x | rhs, lhs.y | rhs);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000434AA File Offset: 0x000416AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator |(int lhs, int2 rhs)
		{
			return new int2(lhs | rhs.x, lhs | rhs.y);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000434C1 File Offset: 0x000416C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ^(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000434E2 File Offset: 0x000416E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ^(int2 lhs, int rhs)
		{
			return new int2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000434F9 File Offset: 0x000416F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ^(int lhs, int2 rhs)
		{
			return new int2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00043510 File Offset: 0x00041710
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0004352F File Offset: 0x0004172F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0004354E File Offset: 0x0004174E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0004356D File Offset: 0x0004176D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0004358C File Offset: 0x0004178C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x000435AB File Offset: 0x000417AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x000435CA File Offset: 0x000417CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x000435E9 File Offset: 0x000417E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x00043608 File Offset: 0x00041808
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00043627 File Offset: 0x00041827
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00043646 File Offset: 0x00041846
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00043665 File Offset: 0x00041865
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00043684 File Offset: 0x00041884
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x000436A3 File Offset: 0x000418A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000436C2 File Offset: 0x000418C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x000436E1 File Offset: 0x000418E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00043700 File Offset: 0x00041900
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00043719 File Offset: 0x00041919
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00043732 File Offset: 0x00041932
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0004374B File Offset: 0x0004194B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00043764 File Offset: 0x00041964
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0004377D File Offset: 0x0004197D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00043796 File Offset: 0x00041996
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x000437AF File Offset: 0x000419AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x000437C8 File Offset: 0x000419C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.x);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x000437DB File Offset: 0x000419DB
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x000437EE File Offset: 0x000419EE
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

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00043808 File Offset: 0x00041A08
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0004381B File Offset: 0x00041A1B
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

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00043835 File Offset: 0x00041A35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.y);
			}
		}

		// Token: 0x170007CC RID: 1996
		public unsafe int this[int index]
		{
			get
			{
				fixed (int2* ptr = &this)
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

		// Token: 0x06001885 RID: 6277 RVA: 0x00043880 File Offset: 0x00041A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000438A0 File Offset: 0x00041AA0
		public override bool Equals(object o)
		{
			if (o is int2)
			{
				int2 rhs = (int2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000438C5 File Offset: 0x00041AC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000438D2 File Offset: 0x00041AD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2({0}, {1})", this.x, this.y);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000438F4 File Offset: 0x00041AF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x040000AF RID: 175
		public int x;

		// Token: 0x040000B0 RID: 176
		public int y;

		// Token: 0x040000B1 RID: 177
		public static readonly int2 zero;

		// Token: 0x0200005D RID: 93
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002473 RID: 9331 RVA: 0x00067694 File Offset: 0x00065894
			public DebuggerProxy(int2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x04000157 RID: 343
			public int x;

			// Token: 0x04000158 RID: 344
			public int y;
		}
	}
}
