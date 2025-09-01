using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides functionality for formatting serialized objects.</summary>
	// Token: 0x0200065E RID: 1630
	[ComVisible(true)]
	public interface IFormatter
	{
		/// <summary>Deserializes the data on the provided stream and reconstitutes the graph of objects.</summary>
		/// <param name="serializationStream">The stream that contains the data to deserialize.</param>
		/// <returns>The top object of the deserialized graph.</returns>
		// Token: 0x06003CDA RID: 15578
		object Deserialize(Stream serializationStream);

		/// <summary>Serializes an object, or graph of objects with the given root to the provided stream.</summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. This stream can reference a variety of backing stores (such as files, network, memory, and so on).</param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized.</param>
		// Token: 0x06003CDB RID: 15579
		void Serialize(Stream serializationStream, object graph);

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by this formatter.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003CDC RID: 15580
		// (set) Token: 0x06003CDD RID: 15581
		ISurrogateSelector SurrogateSelector { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003CDE RID: 15582
		// (set) Token: 0x06003CDF RID: 15583
		SerializationBinder Binder { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003CE0 RID: 15584
		// (set) Token: 0x06003CE1 RID: 15585
		StreamingContext Context { get; set; }
	}
}
