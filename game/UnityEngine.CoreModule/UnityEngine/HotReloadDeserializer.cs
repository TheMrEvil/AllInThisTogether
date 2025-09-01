using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001B3 RID: 435
	[NativeConditional("HOT_RELOAD_AVAILABLE")]
	[NativeType(Header = "Runtime/Export/HotReload/HotReload.bindings.h")]
	internal static class HotReloadDeserializer
	{
		// Token: 0x06001336 RID: 4918
		[FreeFunction("HotReload::Prepare")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void PrepareHotReload();

		// Token: 0x06001337 RID: 4919
		[FreeFunction("HotReload::Finish")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FinishHotReload(Type[] typesToReset);

		// Token: 0x06001338 RID: 4920
		[FreeFunction("HotReload::CreateEmptyAsset")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object CreateEmptyAsset(Type type);

		// Token: 0x06001339 RID: 4921
		[FreeFunction("HotReload::DeserializeAsset")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DeserializeAsset(Object asset, byte[] data);

		// Token: 0x0600133A RID: 4922
		[FreeFunction("HotReload::RemapInstanceIds")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemapInstanceIds(Object editorAsset, int[] editorToPlayerInstanceIdMapKeys, int[] editorToPlayerInstanceIdMapValues);

		// Token: 0x0600133B RID: 4923 RVA: 0x0001AF7B File Offset: 0x0001917B
		internal static void RemapInstanceIds(Object editorAsset, Dictionary<int, int> editorToPlayerInstanceIdMap)
		{
			HotReloadDeserializer.RemapInstanceIds(editorAsset, editorToPlayerInstanceIdMap.Keys.ToArray<int>(), editorToPlayerInstanceIdMap.Values.ToArray<int>());
		}

		// Token: 0x0600133C RID: 4924
		[FreeFunction("HotReload::FinalizeAssetCreation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FinalizeAssetCreation(Object asset);

		// Token: 0x0600133D RID: 4925
		[FreeFunction("HotReload::GetDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object[] GetDependencies(Object asset);

		// Token: 0x0600133E RID: 4926
		[FreeFunction("HotReload::GetNullDependencies")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int[] GetNullDependencies(Object asset);
	}
}
