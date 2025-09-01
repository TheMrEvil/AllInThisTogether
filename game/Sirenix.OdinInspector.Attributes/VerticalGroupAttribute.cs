using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000081 RID: 129
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class VerticalGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000247E File Offset: 0x0000067E
		public VerticalGroupAttribute(string groupId, float order = 0f) : base(groupId, order)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000413B File Offset: 0x0000233B
		public VerticalGroupAttribute(float order = 0f) : this("_DefaultVerticalGroup", order)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000414C File Offset: 0x0000234C
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			VerticalGroupAttribute verticalGroupAttribute = other as VerticalGroupAttribute;
			if (verticalGroupAttribute != null)
			{
				if (verticalGroupAttribute.PaddingTop != 0f)
				{
					this.PaddingTop = verticalGroupAttribute.PaddingTop;
				}
				if (verticalGroupAttribute.PaddingBottom != 0f)
				{
					this.PaddingBottom = verticalGroupAttribute.PaddingBottom;
				}
			}
		}

		// Token: 0x04000261 RID: 609
		public float PaddingTop;

		// Token: 0x04000262 RID: 610
		public float PaddingBottom;
	}
}
