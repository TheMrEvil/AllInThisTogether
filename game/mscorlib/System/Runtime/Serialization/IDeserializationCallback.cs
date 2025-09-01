using System;

namespace System.Runtime.Serialization
{
	/// <summary>Indicates that a class is to be notified when deserialization of the entire object graph has been completed. Note that this interface is not called when deserializing with the XmlSerializer (System.Xml.Serialization.XmlSerializer).</summary>
	// Token: 0x02000647 RID: 1607
	public interface IDeserializationCallback
	{
		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06003C3B RID: 15419
		void OnDeserialization(object sender);
	}
}
