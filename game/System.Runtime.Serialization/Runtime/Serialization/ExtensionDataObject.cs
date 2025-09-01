using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Stores data from a versioned data contract that has been extended by adding new members.</summary>
	// Token: 0x020000D4 RID: 212
	public sealed class ExtensionDataObject
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0000222F File Offset: 0x0000042F
		internal ExtensionDataObject()
		{
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00032BCE File Offset: 0x00030DCE
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00032BD6 File Offset: 0x00030DD6
		internal IList<ExtensionDataMember> Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x04000519 RID: 1305
		private IList<ExtensionDataMember> members;
	}
}
