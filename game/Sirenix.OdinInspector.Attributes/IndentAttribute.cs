using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200003B RID: 59
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class IndentAttribute : Attribute
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000029CB File Offset: 0x00000BCB
		public IndentAttribute(int indentLevel = 1)
		{
			this.IndentLevel = indentLevel;
		}

		// Token: 0x04000072 RID: 114
		public int IndentLevel;
	}
}
