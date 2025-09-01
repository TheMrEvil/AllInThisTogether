using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleRecompressOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRecompressOperation : AsyncOperation
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005A RID: 90
		public extern string humanReadableResult { [NativeMethod("GetResultStr")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005B RID: 91
		public extern string inputPath { [NativeMethod("GetInputPath")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005C RID: 92
		public extern string outputPath { [NativeMethod("GetOutputPath")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005D RID: 93
		public extern AssetBundleLoadResult result { [NativeMethod("GetResult")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94
		public extern bool success { [NativeMethod("GetSuccess")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600005F RID: 95 RVA: 0x000027B8 File Offset: 0x000009B8
		public AssetBundleRecompressOperation()
		{
		}
	}
}
