using System;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Allows access to field names and field types of objects that support the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.</summary>
	// Token: 0x0200067B RID: 1659
	public interface IFieldInfo
	{
		/// <summary>Gets or sets the field names of serialized objects.</summary>
		/// <returns>The field names of serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003DC6 RID: 15814
		// (set) Token: 0x06003DC7 RID: 15815
		string[] FieldNames { get; set; }

		/// <summary>Gets or sets the field types of the serialized objects.</summary>
		/// <returns>The field types of the serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003DC8 RID: 15816
		// (set) Token: 0x06003DC9 RID: 15817
		Type[] FieldTypes { get; set; }
	}
}
