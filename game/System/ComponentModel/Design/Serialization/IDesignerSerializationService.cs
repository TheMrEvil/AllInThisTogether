using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can invoke serialization and deserialization.</summary>
	// Token: 0x0200048A RID: 1162
	public interface IDesignerSerializationService
	{
		/// <summary>Deserializes the specified serialization data object and returns a collection of objects represented by that data.</summary>
		/// <param name="serializationData">An object consisting of serialized data.</param>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of objects rebuilt from the specified serialization data object.</returns>
		// Token: 0x0600252E RID: 9518
		ICollection Deserialize(object serializationData);

		/// <summary>Serializes the specified collection of objects and stores them in a serialization data object.</summary>
		/// <param name="objects">A collection of objects to serialize.</param>
		/// <returns>An object that contains the serialized state of the specified collection of objects.</returns>
		// Token: 0x0600252F RID: 9519
		object Serialize(ICollection objects);
	}
}
