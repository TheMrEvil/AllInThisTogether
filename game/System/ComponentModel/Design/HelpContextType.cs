using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate information about the context in which a request for Help information originated.</summary>
	// Token: 0x02000453 RID: 1107
	public enum HelpContextType
	{
		/// <summary>A general context.</summary>
		// Token: 0x040010C7 RID: 4295
		Ambient,
		/// <summary>A window.</summary>
		// Token: 0x040010C8 RID: 4296
		Window,
		/// <summary>A selection.</summary>
		// Token: 0x040010C9 RID: 4297
		Selection,
		/// <summary>A tool window selection.</summary>
		// Token: 0x040010CA RID: 4298
		ToolWindowSelection
	}
}
