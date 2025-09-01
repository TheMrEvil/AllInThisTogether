using System;

namespace System.Runtime.Serialization
{
	/// <summary>Enables serialization of custom exception data in security-transparent code.</summary>
	// Token: 0x0200066A RID: 1642
	public interface ISafeSerializationData
	{
		/// <summary>This method is called when the instance is deserialized.</summary>
		/// <param name="deserialized">An object that contains the state of the instance.</param>
		// Token: 0x06003D59 RID: 15705
		void CompleteDeserialization(object deserialized);
	}
}
