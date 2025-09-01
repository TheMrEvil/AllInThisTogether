using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AssetBundlePatching
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/AssetBundle/Public/AssetBundlePatching.h")]
	public static class AssetBundleUtility
	{
		// Token: 0x0600006C RID: 108
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PatchAssetBundles(AssetBundle[] bundles, string[] filenames);
	}
}
