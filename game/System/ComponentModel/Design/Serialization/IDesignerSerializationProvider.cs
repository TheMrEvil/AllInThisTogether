using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that enables access to a serializer.</summary>
	// Token: 0x02000489 RID: 1161
	public interface IDesignerSerializationProvider
	{
		/// <summary>Gets a serializer using the specified attributes.</summary>
		/// <param name="manager">The serialization manager requesting the serializer.</param>
		/// <param name="currentSerializer">An instance of the current serializer of the specified type. This can be <see langword="null" /> if no serializer of the specified type exists.</param>
		/// <param name="objectType">The data type of the object to serialize.</param>
		/// <param name="serializerType">The data type of the serializer to create.</param>
		/// <returns>An instance of a serializer of the type requested, or <see langword="null" /> if the request cannot be satisfied.</returns>
		// Token: 0x0600252D RID: 9517
		object GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType);
	}
}
