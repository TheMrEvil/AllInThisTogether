using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200005D RID: 93
	[IncludeMyAttributes]
	[ShowInInspector]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class ResponsiveButtonGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000036E7 File Offset: 0x000018E7
		public ResponsiveButtonGroupAttribute(string group = "_DefaultResponsiveButtonGroup") : base(group)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000036F8 File Offset: 0x000018F8
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			ResponsiveButtonGroupAttribute responsiveButtonGroupAttribute = other as ResponsiveButtonGroupAttribute;
			if (other == null)
			{
				return;
			}
			if (responsiveButtonGroupAttribute.DefaultButtonSize != ButtonSizes.Medium)
			{
				this.DefaultButtonSize = responsiveButtonGroupAttribute.DefaultButtonSize;
			}
			else if (this.DefaultButtonSize != ButtonSizes.Medium)
			{
				responsiveButtonGroupAttribute.DefaultButtonSize = this.DefaultButtonSize;
			}
			this.UniformLayout = (this.UniformLayout || responsiveButtonGroupAttribute.UniformLayout);
		}

		// Token: 0x04000104 RID: 260
		public ButtonSizes DefaultButtonSize = ButtonSizes.Medium;

		// Token: 0x04000105 RID: 261
		public bool UniformLayout;
	}
}
