using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200010C RID: 268
	[NativeHeader("Runtime/Utilities/PropertyName.h")]
	[NativeHeader("Runtime/Director/Core/ExposedPropertyTable.bindings.h")]
	public struct ExposedPropertyResolver
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00008D14 File Offset: 0x00006F14
		internal static Object ResolveReferenceInternal(IntPtr ptr, PropertyName name, out bool isValid)
		{
			bool flag = ptr == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException("Argument \"ptr\" can't be null.");
			}
			return ExposedPropertyResolver.ResolveReferenceBindingsInternal(ptr, name, out isValid);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00008D48 File Offset: 0x00006F48
		[FreeFunction("ExposedPropertyTableBindings::ResolveReferenceInternal")]
		private static Object ResolveReferenceBindingsInternal(IntPtr ptr, PropertyName name, out bool isValid)
		{
			return ExposedPropertyResolver.ResolveReferenceBindingsInternal_Injected(ptr, ref name, out isValid);
		}

		// Token: 0x0600067B RID: 1659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object ResolveReferenceBindingsInternal_Injected(IntPtr ptr, ref PropertyName name, out bool isValid);

		// Token: 0x04000384 RID: 900
		internal IntPtr table;
	}
}
