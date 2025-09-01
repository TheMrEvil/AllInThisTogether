using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleManifest.h")]
	public class AssetBundleManifest : Object
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002050 File Offset: 0x00000250
		private AssetBundleManifest()
		{
		}

		// Token: 0x06000054 RID: 84
		[NativeMethod("GetAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetBundles();

		// Token: 0x06000055 RID: 85
		[NativeMethod("GetAllAssetBundlesWithVariant")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetBundlesWithVariant();

		// Token: 0x06000056 RID: 86 RVA: 0x00002838 File Offset: 0x00000A38
		[NativeMethod("GetAssetBundleHash")]
		public Hash128 GetAssetBundleHash(string assetBundleName)
		{
			Hash128 result;
			this.GetAssetBundleHash_Injected(assetBundleName, out result);
			return result;
		}

		// Token: 0x06000057 RID: 87
		[NativeMethod("GetDirectDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetDirectDependencies(string assetBundleName);

		// Token: 0x06000058 RID: 88
		[NativeMethod("GetAllDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllDependencies(string assetBundleName);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAssetBundleHash_Injected(string assetBundleName, out Hash128 ret);
	}
}
