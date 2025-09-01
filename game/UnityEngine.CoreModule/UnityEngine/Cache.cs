using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F0 RID: 240
	[NativeHeader("Runtime/Misc/Cache.h")]
	[StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	public struct Cache : IEquatable<Cache>
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00007008 File Offset: 0x00005208
		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00007020 File Offset: 0x00005220
		public static bool operator ==(Cache lhs, Cache rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00007044 File Offset: 0x00005244
		public static bool operator !=(Cache lhs, Cache rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000706C File Offset: 0x0000526C
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00007084 File Offset: 0x00005284
		public override bool Equals(object other)
		{
			return other is Cache && this.Equals((Cache)other);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000070B0 File Offset: 0x000052B0
		public bool Equals(Cache other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000070D4 File Offset: 0x000052D4
		public bool valid
		{
			get
			{
				return Cache.Cache_IsValid(this.m_Handle);
			}
		}

		// Token: 0x06000455 RID: 1109
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsValid(int handle);

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000070F4 File Offset: 0x000052F4
		public bool ready
		{
			get
			{
				return Cache.Cache_IsReady(this.m_Handle);
			}
		}

		// Token: 0x06000457 RID: 1111
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsReady(int handle);

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00007114 File Offset: 0x00005314
		public bool readOnly
		{
			get
			{
				return Cache.Cache_IsReadonly(this.m_Handle);
			}
		}

		// Token: 0x06000459 RID: 1113
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsReadonly(int handle);

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00007134 File Offset: 0x00005334
		public string path
		{
			get
			{
				return Cache.Cache_GetPath(this.m_Handle);
			}
		}

		// Token: 0x0600045B RID: 1115
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Cache_GetPath(int handle);

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00007154 File Offset: 0x00005354
		public int index
		{
			get
			{
				return Cache.Cache_GetIndex(this.m_Handle);
			}
		}

		// Token: 0x0600045D RID: 1117
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetIndex(int handle);

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00007174 File Offset: 0x00005374
		public long spaceFree
		{
			get
			{
				return Cache.Cache_GetSpaceFree(this.m_Handle);
			}
		}

		// Token: 0x0600045F RID: 1119
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetSpaceFree(int handle);

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00007194 File Offset: 0x00005394
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x000071B1 File Offset: 0x000053B1
		public long maximumAvailableStorageSpace
		{
			get
			{
				return Cache.Cache_GetMaximumDiskSpaceAvailable(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetMaximumDiskSpaceAvailable(this.m_Handle, value);
			}
		}

		// Token: 0x06000462 RID: 1122
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetMaximumDiskSpaceAvailable(int handle);

		// Token: 0x06000463 RID: 1123
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000071C4 File Offset: 0x000053C4
		public long spaceOccupied
		{
			get
			{
				return Cache.Cache_GetCachingDiskSpaceUsed(this.m_Handle);
			}
		}

		// Token: 0x06000465 RID: 1125
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long Cache_GetCachingDiskSpaceUsed(int handle);

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000071E4 File Offset: 0x000053E4
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00007201 File Offset: 0x00005401
		public int expirationDelay
		{
			get
			{
				return Cache.Cache_GetExpirationDelay(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetExpirationDelay(this.m_Handle, value);
			}
		}

		// Token: 0x06000468 RID: 1128
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetExpirationDelay(int handle);

		// Token: 0x06000469 RID: 1129
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		// Token: 0x0600046A RID: 1130 RVA: 0x00007214 File Offset: 0x00005414
		public bool ClearCache()
		{
			return Cache.Cache_ClearCache(this.m_Handle);
		}

		// Token: 0x0600046B RID: 1131
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_ClearCache(int handle);

		// Token: 0x0600046C RID: 1132 RVA: 0x00007234 File Offset: 0x00005434
		public bool ClearCache(int expiration)
		{
			return Cache.Cache_ClearCache_Expiration(this.m_Handle, expiration);
		}

		// Token: 0x0600046D RID: 1133
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_ClearCache_Expiration(int handle, int expiration);

		// Token: 0x04000329 RID: 809
		private int m_Handle;
	}
}
