using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000119 RID: 281
	[BurstCompatible]
	public static class NativeListUnsafeUtility
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0001E971 File Offset: 0x0001CB71
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void* GetUnsafePtr<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData->Ptr;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001E971 File Offset: 0x0001CB71
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void* GetUnsafeReadOnlyPtr<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData->Ptr;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E97E File Offset: 0x0001CB7E
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void* GetInternalListDataPtrUnchecked<[IsUnmanaged] T>(ref NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData;
		}
	}
}
