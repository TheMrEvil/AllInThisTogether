using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000056 RID: 86
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class PropertyTooltipAttribute : Attribute
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00003507 File Offset: 0x00001707
		public PropertyTooltipAttribute(string tooltip)
		{
			this.Tooltip = tooltip;
		}

		// Token: 0x040000F3 RID: 243
		public string Tooltip;
	}
}
