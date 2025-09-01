using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.CrashReportHandler
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("CrashReporting::CrashReporter::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/CrashReporting/Public/CrashReporter.h")]
	public class CrashReportHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private CrashReportHandler()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2
		// (set) Token: 0x06000003 RID: 3
		[NativeProperty("Enabled")]
		public static extern bool enableCaptureExceptions { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		// (set) Token: 0x06000005 RID: 5
		[NativeThrows]
		public static extern uint logBufferSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000006 RID: 6
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetUserMetadata(string key);

		// Token: 0x06000007 RID: 7
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetUserMetadata(string key, string value);
	}
}
