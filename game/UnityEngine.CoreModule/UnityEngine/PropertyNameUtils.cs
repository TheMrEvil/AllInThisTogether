using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E3 RID: 483
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	internal class PropertyNameUtils
	{
		// Token: 0x060015EE RID: 5614 RVA: 0x000233F8 File Offset: 0x000215F8
		[FreeFunction(IsThreadSafe = true)]
		public static PropertyName PropertyNameFromString([Unmarshalled] string name)
		{
			PropertyName result;
			PropertyNameUtils.PropertyNameFromString_Injected(name, out result);
			return result;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00002072 File Offset: 0x00000272
		public PropertyNameUtils()
		{
		}

		// Token: 0x060015F0 RID: 5616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PropertyNameFromString_Injected(string name, out PropertyName ret);
	}
}
