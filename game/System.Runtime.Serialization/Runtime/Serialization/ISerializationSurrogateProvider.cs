using System;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the methods needed to construct a serialization surrogate that extends the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />. A serialization surrogate is used during serialization and deserialization to substitute one type for another.</summary>
	// Token: 0x020000A9 RID: 169
	public interface ISerializationSurrogateProvider
	{
		/// <summary>During serialization, deserialization, and schema import and export, returns a data contract type that substitutes the specified type.</summary>
		/// <param name="type">The type to substitute.</param>
		/// <returns>The <see cref="T:System.Type" /> to substitute for the <paramref name="type" /> value.</returns>
		// Token: 0x06000905 RID: 2309
		Type GetSurrogateType(Type type);

		/// <summary>During serialization, returns an object that substitutes the specified object.</summary>
		/// <param name="obj">The object to substitute.</param>
		/// <param name="targetType">The <see cref="T:System.Type" /> that the substituted object should be assigned to.</param>
		/// <returns>The substituted object that will be serialized.</returns>
		// Token: 0x06000906 RID: 2310
		object GetObjectToSerialize(object obj, Type targetType);

		/// <summary>During deserialization, returns an object that is a substitute for the specified object.</summary>
		/// <param name="obj">The deserialized object to be substituted.</param>
		/// <param name="targetType">The <see cref="T:System.Type" /> that the substituted object should be assigned to.</param>
		/// <returns>The substituted deserialized object.</returns>
		// Token: 0x06000907 RID: 2311
		object GetDeserializedObject(object obj, Type targetType);
	}
}
