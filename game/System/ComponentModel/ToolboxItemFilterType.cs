using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x020003F1 RID: 1009
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x04000FED RID: 4077
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string.</summary>
		// Token: 0x04000FEE RID: 4078
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed.</summary>
		// Token: 0x04000FEF RID: 4079
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled.</summary>
		// Token: 0x04000FF0 RID: 4080
		Require
	}
}
