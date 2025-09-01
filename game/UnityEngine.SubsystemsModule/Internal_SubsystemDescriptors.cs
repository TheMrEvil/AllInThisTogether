using System;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	internal static class Internal_SubsystemDescriptors
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002240 File Offset: 0x00000440
		[RequiredByNativeCode]
		internal static void Internal_AddDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDeprecatedDescriptor(descriptor);
		}
	}
}
