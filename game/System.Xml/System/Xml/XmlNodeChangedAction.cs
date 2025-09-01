using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node change.</summary>
	// Token: 0x020001C7 RID: 455
	public enum XmlNodeChangedAction
	{
		/// <summary>A node is being inserted in the tree.</summary>
		// Token: 0x0400109C RID: 4252
		Insert,
		/// <summary>A node is being removed from the tree.</summary>
		// Token: 0x0400109D RID: 4253
		Remove,
		/// <summary>A node value is being changed.</summary>
		// Token: 0x0400109E RID: 4254
		Change
	}
}
