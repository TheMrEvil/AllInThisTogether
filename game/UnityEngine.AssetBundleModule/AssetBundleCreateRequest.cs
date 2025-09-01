using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromAsyncOperation.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000048 RID: 72
		public extern AssetBundle assetBundle { [NativeMethod("GetAssetBundleBlocking")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000049 RID: 73
		[NativeMethod("SetEnableCompatibilityChecks")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEnableCompatibilityChecks(bool set);

		// Token: 0x0600004A RID: 74 RVA: 0x000027AD File Offset: 0x000009AD
		internal void DisableCompatibilityChecks()
		{
			this.SetEnableCompatibilityChecks(false);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000027B8 File Offset: 0x000009B8
		public AssetBundleCreateRequest()
		{
		}
	}
}
