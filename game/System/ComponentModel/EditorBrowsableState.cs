using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the browsable state of a property or method from within an editor.</summary>
	// Token: 0x02000360 RID: 864
	public enum EditorBrowsableState
	{
		/// <summary>The property or method is always browsable from within an editor.</summary>
		// Token: 0x04000E8E RID: 3726
		Always,
		/// <summary>The property or method is never browsable from within an editor.</summary>
		// Token: 0x04000E8F RID: 3727
		Never,
		/// <summary>The property or method is a feature that only advanced users should see. An editor can either show or hide such properties.</summary>
		// Token: 0x04000E90 RID: 3728
		Advanced
	}
}
