using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F2 RID: 242
	[StaticAccessor("GetCachingManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Misc/CachingManager.h")]
	public sealed class Caching
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600046E RID: 1134
		// (set) Token: 0x0600046F RID: 1135
		public static extern bool compressionEnabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000470 RID: 1136
		public static extern bool ready { [NativeName("GetIsReady")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000471 RID: 1137
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool ClearCache();

		// Token: 0x06000472 RID: 1138 RVA: 0x00007254 File Offset: 0x00005454
		public static bool ClearCache(int expiration)
		{
			return Caching.ClearCache_Int(expiration);
		}

		// Token: 0x06000473 RID: 1139
		[NativeName("ClearCache")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ClearCache_Int(int expiration);

		// Token: 0x06000474 RID: 1140 RVA: 0x0000726C File Offset: 0x0000546C
		public static bool ClearCachedVersion(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersionInternal(assetBundleName, hash);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000729A File Offset: 0x0000549A
		[NativeName("ClearCachedVersion")]
		internal static bool ClearCachedVersionInternal(string assetBundleName, Hash128 hash)
		{
			return Caching.ClearCachedVersionInternal_Injected(assetBundleName, ref hash);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000072A4 File Offset: 0x000054A4
		public static bool ClearOtherCachedVersions(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, hash, true);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000072D4 File Offset: 0x000054D4
		public static bool ClearAllCachedVersions(string assetBundleName)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, default(Hash128), false);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000730B File Offset: 0x0000550B
		internal static bool ClearCachedVersions(string assetBundleName, Hash128 hash, bool keepInputVersion)
		{
			return Caching.ClearCachedVersions_Injected(assetBundleName, ref hash, keepInputVersion);
		}

		// Token: 0x06000479 RID: 1145
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Hash128[] GetCachedVersions(string assetBundleName);

		// Token: 0x0600047A RID: 1146 RVA: 0x00007318 File Offset: 0x00005518
		public static void GetCachedVersions(string assetBundleName, List<Hash128> outCachedVersions)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			bool flag2 = outCachedVersions == null;
			if (flag2)
			{
				throw new ArgumentNullException("Input outCachedVersions cannot be null.");
			}
			outCachedVersions.AddRange(Caching.GetCachedVersions(assetBundleName));
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000735C File Offset: 0x0000555C
		[Obsolete("Please use IsVersionCached with Hash128 instead.")]
		public static bool IsVersionCached(string url, int version)
		{
			return Caching.IsVersionCached(url, new Hash128(0U, 0U, 0U, (uint)version));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00007380 File Offset: 0x00005580
		public static bool IsVersionCached(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.IsVersionCached(url, "", hash);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000073B4 File Offset: 0x000055B4
		public static bool IsVersionCached(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.IsVersionCached("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000073F9 File Offset: 0x000055F9
		[NativeName("IsCached")]
		internal static bool IsVersionCached(string url, string assetBundleName, Hash128 hash)
		{
			return Caching.IsVersionCached_Injected(url, assetBundleName, ref hash);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00007404 File Offset: 0x00005604
		[Obsolete("Please use MarkAsUsed with Hash128 instead.")]
		public static bool MarkAsUsed(string url, int version)
		{
			return Caching.MarkAsUsed(url, new Hash128(0U, 0U, 0U, (uint)version));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00007428 File Offset: 0x00005628
		public static bool MarkAsUsed(string url, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(url);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle url cannot be null or empty.");
			}
			return Caching.MarkAsUsed(url, "", hash);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000745C File Offset: 0x0000565C
		public static bool MarkAsUsed(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.MarkAsUsed("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000074A1 File Offset: 0x000056A1
		internal static bool MarkAsUsed(string url, string assetBundleName, Hash128 hash)
		{
			return Caching.MarkAsUsed_Injected(url, assetBundleName, ref hash);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000074AC File Offset: 0x000056AC
		[Obsolete("This function is obsolete and will always return -1. Use IsVersionCached instead.")]
		public static int GetVersionFromCache(string url)
		{
			return -1;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x000074C0 File Offset: 0x000056C0
		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceUsed
		{
			get
			{
				return (int)Caching.spaceOccupied;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000485 RID: 1157
		[Obsolete("This property is only used for the current cache, use Cache.spaceOccupied to get used bytes per cache.")]
		public static extern long spaceOccupied { [NativeName("GetCachingDiskSpaceUsed")] [StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000074D8 File Offset: 0x000056D8
		[Obsolete("Please use use Cache.spaceOccupied to get used bytes per cache.")]
		public static int spaceAvailable
		{
			get
			{
				return (int)Caching.spaceFree;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000487 RID: 1159
		[Obsolete("This property is only used for the current cache, use Cache.spaceFree to get unused bytes per cache.")]
		public static extern long spaceFree { [NativeName("GetCachingDiskSpaceFree")] [StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000488 RID: 1160
		// (set) Token: 0x06000489 RID: 1161
		[Obsolete("This property is only used for the current cache, use Cache.maximumAvailableStorageSpace to access the maximum available storage space per cache.")]
		[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		public static extern long maximumAvailableDiskSpace { [NativeName("GetMaximumDiskSpaceAvailable")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetMaximumDiskSpaceAvailable")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600048A RID: 1162
		// (set) Token: 0x0600048B RID: 1163
		[StaticAccessor("GetCachingManager().GetCurrentCache()", StaticAccessorType.Dot)]
		[Obsolete("This property is only used for the current cache, use Cache.expirationDelay to access the expiration delay per cache.")]
		public static extern int expirationDelay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600048C RID: 1164 RVA: 0x000074F0 File Offset: 0x000056F0
		public static Cache AddCache(string cachePath)
		{
			bool flag = string.IsNullOrEmpty(cachePath);
			if (flag)
			{
				throw new ArgumentNullException("Cache path cannot be null or empty.");
			}
			bool isReadonly = false;
			bool flag2 = cachePath.Replace('\\', '/').StartsWith(Application.streamingAssetsPath);
			if (flag2)
			{
				isReadonly = true;
			}
			else
			{
				bool flag3 = !Directory.Exists(cachePath);
				if (flag3)
				{
					throw new ArgumentException("Cache path '" + cachePath + "' doesn't exist.");
				}
				bool flag4 = (File.GetAttributes(cachePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
				if (flag4)
				{
					isReadonly = true;
				}
			}
			bool valid = Caching.GetCacheByPath(cachePath).valid;
			if (valid)
			{
				throw new InvalidOperationException("Cache with path '" + cachePath + "' has already been added.");
			}
			return Caching.AddCache(cachePath, isReadonly);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000075A4 File Offset: 0x000057A4
		[NativeName("AddCachePath")]
		internal static Cache AddCache(string cachePath, bool isReadonly)
		{
			Cache result;
			Caching.AddCache_Injected(cachePath, isReadonly, out result);
			return result;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000075BC File Offset: 0x000057BC
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		[NativeName("Caching_GetCacheHandleAt")]
		public static Cache GetCacheAt(int cacheIndex)
		{
			Cache result;
			Caching.GetCacheAt_Injected(cacheIndex, out result);
			return result;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000075D4 File Offset: 0x000057D4
		[NativeName("Caching_GetCacheHandleByPath")]
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache GetCacheByPath(string cachePath)
		{
			Cache result;
			Caching.GetCacheByPath_Injected(cachePath, out result);
			return result;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000075EC File Offset: 0x000057EC
		public static void GetAllCachePaths(List<string> cachePaths)
		{
			cachePaths.Clear();
			for (int i = 0; i < Caching.cacheCount; i++)
			{
				cachePaths.Add(Caching.GetCacheAt(i).path);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000762C File Offset: 0x0000582C
		[NativeThrows]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeName("Caching_RemoveCacheByHandle")]
		public static bool RemoveCache(Cache cache)
		{
			return Caching.RemoveCache_Injected(ref cache);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00007635 File Offset: 0x00005835
		[NativeThrows]
		[NativeName("Caching_MoveCacheBeforeByHandle")]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static void MoveCacheBefore(Cache src, Cache dst)
		{
			Caching.MoveCacheBefore_Injected(ref src, ref dst);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00007640 File Offset: 0x00005840
		[NativeThrows]
		[NativeName("Caching_MoveCacheAfterByHandle")]
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static void MoveCacheAfter(Cache src, Cache dst)
		{
			Caching.MoveCacheAfter_Injected(ref src, ref dst);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000494 RID: 1172
		public static extern int cacheCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000764C File Offset: 0x0000584C
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache defaultCache
		{
			[NativeName("Caching_GetDefaultCacheHandle")]
			get
			{
				Cache result;
				Caching.get_defaultCache_Injected(out result);
				return result;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00007664 File Offset: 0x00005864
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00007679 File Offset: 0x00005879
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache currentCacheForWriting
		{
			[NativeName("Caching_GetCurrentCacheHandle")]
			get
			{
				Cache result;
				Caching.get_currentCacheForWriting_Injected(out result);
				return result;
			}
			[NativeName("Caching_SetCurrentCacheByHandle")]
			[NativeThrows]
			set
			{
				Caching.set_currentCacheForWriting_Injected(ref value);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00007684 File Offset: 0x00005884
		[Obsolete("This function is obsolete. Please use ClearCache.  (UnityUpgradable) -> ClearCache()")]
		public static bool CleanCache()
		{
			return Caching.ClearCache();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00002072 File Offset: 0x00000272
		public Caching()
		{
		}

		// Token: 0x0600049A RID: 1178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersionInternal_Injected(string assetBundleName, ref Hash128 hash);

		// Token: 0x0600049B RID: 1179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersions_Injected(string assetBundleName, ref Hash128 hash, bool keepInputVersion);

		// Token: 0x0600049C RID: 1180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsVersionCached_Injected(string url, string assetBundleName, ref Hash128 hash);

		// Token: 0x0600049D RID: 1181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool MarkAsUsed_Injected(string url, string assetBundleName, ref Hash128 hash);

		// Token: 0x0600049E RID: 1182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCache_Injected(string cachePath, bool isReadonly, out Cache ret);

		// Token: 0x0600049F RID: 1183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheAt_Injected(int cacheIndex, out Cache ret);

		// Token: 0x060004A0 RID: 1184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheByPath_Injected(string cachePath, out Cache ret);

		// Token: 0x060004A1 RID: 1185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveCache_Injected(ref Cache cache);

		// Token: 0x060004A2 RID: 1186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveCacheBefore_Injected(ref Cache src, ref Cache dst);

		// Token: 0x060004A3 RID: 1187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveCacheAfter_Injected(ref Cache src, ref Cache dst);

		// Token: 0x060004A4 RID: 1188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultCache_Injected(out Cache ret);

		// Token: 0x060004A5 RID: 1189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentCacheForWriting_Injected(out Cache ret);

		// Token: 0x060004A6 RID: 1190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_currentCacheForWriting_Injected(ref Cache value);
	}
}
