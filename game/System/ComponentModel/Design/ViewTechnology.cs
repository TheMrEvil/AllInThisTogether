using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for a set of technologies that designer hosts support.</summary>
	// Token: 0x02000479 RID: 1145
	public enum ViewTechnology
	{
		/// <summary>Represents a mode in which the view object is passed directly to the development environment.</summary>
		// Token: 0x04001485 RID: 5253
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Passthrough,
		/// <summary>Represents a mode in which a Windows Forms control object provides the display for the root designer.</summary>
		// Token: 0x04001486 RID: 5254
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		WindowsForms,
		/// <summary>Specifies the default view technology support.</summary>
		// Token: 0x04001487 RID: 5255
		Default
	}
}
