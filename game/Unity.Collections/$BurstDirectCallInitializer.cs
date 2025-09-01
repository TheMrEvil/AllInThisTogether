using System;
using Unity.Collections;
using UnityEngine;

// Token: 0x0200015A RID: 346
internal static class $BurstDirectCallInitializer
{
	// Token: 0x06000C27 RID: 3111 RVA: 0x00024431 File Offset: 0x00022631
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Initialize()
	{
		AllocatorManager.Initialize$StackAllocator_Try_00000A45$BurstDirectCall();
		AllocatorManager.Initialize$SlabAllocator_Try_00000A53$BurstDirectCall();
		RewindableAllocator.Try_00000756$BurstDirectCall.Initialize();
		xxHash3.Hash64Long_0000078D$BurstDirectCall.Initialize();
		xxHash3.Hash128Long_00000794$BurstDirectCall.Initialize();
	}
}
