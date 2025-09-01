using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000008 RID: 8
	[UsedByNativeCode]
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h")]
	[NativeHeader("UnityWebRequestScriptingClasses.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequestAsyncOperation : AsyncOperation
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000035D8 File Offset: 0x000017D8
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000035E0 File Offset: 0x000017E0
		public UnityWebRequest webRequest
		{
			[CompilerGenerated]
			get
			{
				return this.<webRequest>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<webRequest>k__BackingField = value;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035E9 File Offset: 0x000017E9
		public UnityWebRequestAsyncOperation()
		{
		}

		// Token: 0x04000020 RID: 32
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private UnityWebRequest <webRequest>k__BackingField;
	}
}
