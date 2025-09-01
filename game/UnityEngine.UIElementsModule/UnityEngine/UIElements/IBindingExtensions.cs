using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000129 RID: 297
	public static class IBindingExtensions
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x000285E0 File Offset: 0x000267E0
		public static bool IsBound(this IBindable control)
		{
			return ((control != null) ? control.binding : null) != null;
		}
	}
}
