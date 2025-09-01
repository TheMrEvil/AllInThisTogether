using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates the name by which an indexer is known in programming languages that do not support indexers directly.</summary>
	// Token: 0x020007F9 RID: 2041
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IndexerNameAttribute" /> class.</summary>
		/// <param name="indexerName">The name of the indexer, as shown to other languages.</param>
		// Token: 0x0600460C RID: 17932 RVA: 0x00002050 File Offset: 0x00000250
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
