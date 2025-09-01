using System;

namespace System.Drawing.Design
{
	/// <summary>Exposes a collection of toolbox items.</summary>
	// Token: 0x02000122 RID: 290
	public interface IToolboxItemProvider
	{
		/// <summary>Gets a collection of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects.</summary>
		/// <returns>A collection of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D78 RID: 3448
		ToolboxItemCollection Items { get; }
	}
}
