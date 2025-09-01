using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the template can be bound one way or two ways.</summary>
	// Token: 0x02000384 RID: 900
	public enum BindingDirection
	{
		/// <summary>The template can only accept property values. Used with a generic <see cref="T:System.Web.UI.ITemplate" />.</summary>
		// Token: 0x04000EE7 RID: 3815
		OneWay,
		/// <summary>The template can accept and expose property values. Used with an <see cref="T:System.Web.UI.IBindableTemplate" />.</summary>
		// Token: 0x04000EE8 RID: 3816
		TwoWay
	}
}
