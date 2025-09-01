using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used by <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> and <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedAggregateAttribute" /> to indicate the serialization format of a user-defined type (UDT) or aggregate.</summary>
	// Token: 0x0200005E RID: 94
	public enum Format
	{
		/// <summary>The serialization format is unknown.</summary>
		// Token: 0x0400055F RID: 1375
		Unknown,
		/// <summary>The <see langword="Native" /> serialization format uses a very simple algorithm that enables SQL Server to store an efficient representation of the UDT on disk. Types marked for <see langword="Native" /> serialization can only have value types (structs in Microsoft Visual C# and structures in Microsoft Visual Basic .NET) as members. Members of reference types (such as classes in Visual C# and Visual Basic), either user-defined or those existing in the framework (such as <see cref="T:System.String" />), are not supported.</summary>
		// Token: 0x04000560 RID: 1376
		Native,
		/// <summary>The <see langword="UserDefined" /> serialization format gives the developer full control over the binary format through the <see cref="T:Microsoft.SqlServer.Server.IBinarySerialize" /><see langword=".Write" /> and <see cref="T:Microsoft.SqlServer.Server.IBinarySerialize" /><see langword=".Read" /> methods.</summary>
		// Token: 0x04000561 RID: 1377
		UserDefined
	}
}
