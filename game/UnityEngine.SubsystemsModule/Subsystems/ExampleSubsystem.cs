using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x02000011 RID: 17
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystem.h")]
	[UsedByNativeCode]
	public class ExampleSubsystem : IntegratedSubsystem<ExampleSubsystemDescriptor>
	{
		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PrintExample();

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool();

		// Token: 0x06000051 RID: 81 RVA: 0x000027DF File Offset: 0x000009DF
		public ExampleSubsystem()
		{
		}
	}
}
