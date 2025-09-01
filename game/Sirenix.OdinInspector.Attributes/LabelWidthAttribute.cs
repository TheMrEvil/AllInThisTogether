using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000041 RID: 65
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class LabelWidthAttribute : Attribute
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002C10 File Offset: 0x00000E10
		public LabelWidthAttribute(float width)
		{
			this.Width = width;
		}

		// Token: 0x04000092 RID: 146
		public float Width;
	}
}
