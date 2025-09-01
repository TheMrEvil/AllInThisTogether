using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000039 RID: 57
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class HideReferenceObjectPickerAttribute : Attribute
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002102 File Offset: 0x00000302
		public HideReferenceObjectPickerAttribute()
		{
		}
	}
}
