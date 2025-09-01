using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	public class XRDisplaySubsystemDescriptor : IntegratedSubsystemDescriptor<XRDisplaySubsystem>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011B RID: 283
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyVr { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011C RID: 284
		[NativeConditional("ENABLE_XR")]
		public extern bool enableBackBufferMSAA { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600011D RID: 285
		[NativeMethod("TryGetAvailableMirrorModeCount")]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetAvailableMirrorBlitModeCount();

		// Token: 0x0600011E RID: 286
		[NativeConditional("ENABLE_XR")]
		[NativeMethod("TryGetMirrorModeByIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetMirrorBlitModeByIndex(int index, out XRMirrorViewBlitModeDesc mode);

		// Token: 0x0600011F RID: 287 RVA: 0x000043A9 File Offset: 0x000025A9
		public XRDisplaySubsystemDescriptor()
		{
		}
	}
}
