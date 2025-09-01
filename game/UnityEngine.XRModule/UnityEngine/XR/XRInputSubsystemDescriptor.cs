using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000027 RID: 39
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystemDescriptor.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public class XRInputSubsystemDescriptor : IntegratedSubsystemDescriptor<XRInputSubsystem>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000130 RID: 304
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyInput { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000131 RID: 305 RVA: 0x000045D5 File Offset: 0x000027D5
		public XRInputSubsystemDescriptor()
		{
		}
	}
}
