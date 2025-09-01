using System;

namespace System.CodeDom
{
	/// <summary>Specifies how the code type reference is to be resolved.</summary>
	// Token: 0x020002EC RID: 748
	[Flags]
	public enum CodeTypeReferenceOptions
	{
		/// <summary>Resolve the type from the root namespace.</summary>
		// Token: 0x04000D3D RID: 3389
		GlobalReference = 1,
		/// <summary>Resolve the type from the type parameter.</summary>
		// Token: 0x04000D3E RID: 3390
		GenericTypeParameter = 2
	}
}
