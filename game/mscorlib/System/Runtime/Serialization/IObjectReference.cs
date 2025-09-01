using System;

namespace System.Runtime.Serialization
{
	/// <summary>Indicates that the current interface implementer is a reference to another object.</summary>
	// Token: 0x02000649 RID: 1609
	public interface IObjectReference
	{
		/// <summary>Returns the real object that should be deserialized, rather than the object that the serialized stream specifies.</summary>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> from which the current object is deserialized.</param>
		/// <returns>The actual object that is put into the graph.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. The call will not work on a medium trusted server.</exception>
		// Token: 0x06003C4D RID: 15437
		object GetRealObject(StreamingContext context);
	}
}
