using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200001C RID: 28
	[Conditional("UNITY_EDITOR")]
	public sealed class DoNotDrawAsReferenceAttribute : Attribute
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002102 File Offset: 0x00000302
		public DoNotDrawAsReferenceAttribute()
		{
		}
	}
}
