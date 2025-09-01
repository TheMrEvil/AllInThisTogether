using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012C RID: 300
	[BurstCompatible]
	internal static class UnsafePtrListTExtensions
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x000209E4 File Offset: 0x0001EBE4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static ref UnsafeList<IntPtr> ListData<[IsUnmanaged] T>(this UnsafePtrList<T> from) where T : struct, ValueType
		{
			return UnsafeUtility.As<UnsafePtrList<T>, UnsafeList<IntPtr>>(ref from);
		}
	}
}
