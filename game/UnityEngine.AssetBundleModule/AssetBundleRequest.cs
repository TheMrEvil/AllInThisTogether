using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRequest : ResourceRequest
	{
		// Token: 0x06000060 RID: 96
		[NativeMethod("GetLoadedAsset")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern Object GetResult();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002850 File Offset: 0x00000A50
		public new Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98
		public extern Object[] allAssets { [NativeMethod("GetAllLoadedAssets")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002868 File Offset: 0x00000A68
		public AssetBundleRequest()
		{
		}
	}
}
