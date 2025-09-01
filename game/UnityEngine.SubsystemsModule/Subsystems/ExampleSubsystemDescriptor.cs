using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x02000012 RID: 18
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystemDescriptor.h")]
	[UsedByNativeCode]
	public class ExampleSubsystemDescriptor : IntegratedSubsystemDescriptor<ExampleSubsystem>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82
		public extern bool supportsEditorMode { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83
		public extern bool disableBackbufferMSAA { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84
		public extern bool stereoscopicBackbuffer { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85
		public extern bool usePBufferEGL { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000056 RID: 86 RVA: 0x000027E8 File Offset: 0x000009E8
		public ExampleSubsystemDescriptor()
		{
		}
	}
}
