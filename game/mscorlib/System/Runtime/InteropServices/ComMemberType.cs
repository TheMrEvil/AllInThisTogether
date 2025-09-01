using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the type of a COM member.</summary>
	// Token: 0x020006D3 RID: 1747
	public enum ComMemberType
	{
		/// <summary>The member is a normal method.</summary>
		// Token: 0x04002A19 RID: 10777
		Method,
		/// <summary>The member gets properties.</summary>
		// Token: 0x04002A1A RID: 10778
		PropGet,
		/// <summary>The member sets properties.</summary>
		// Token: 0x04002A1B RID: 10779
		PropSet
	}
}
