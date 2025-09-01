using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011A RID: 282
	[BurstCompatible]
	public static class NativeReferenceUnsafeUtility
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E986 File Offset: 0x0001CB86
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void* GetUnsafePtr<[IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return reference.m_Data;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E986 File Offset: 0x0001CB86
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void* GetUnsafeReadOnlyPtr<[IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return reference.m_Data;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E986 File Offset: 0x0001CB86
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void* GetUnsafePtrWithoutChecks<[IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return reference.m_Data;
		}
	}
}
