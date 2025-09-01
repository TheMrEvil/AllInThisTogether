using System;

namespace System.Runtime.Serialization
{
	/// <summary>Allows users to control class loading and mandate what class to load.</summary>
	// Token: 0x02000655 RID: 1621
	[Serializable]
	public abstract class SerializationBinder
	{
		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <param name="serializedType">The type of the object the formatter creates a new instance of.</param>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object.</param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object.</param>
		// Token: 0x06003CA2 RID: 15522 RVA: 0x000D1AE7 File Offset: 0x000CFCE7
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}

		/// <summary>When overridden in a derived class, controls the binding of a serialized object to a type.</summary>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object.</param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object.</param>
		/// <returns>The type of the object the formatter creates a new instance of.</returns>
		// Token: 0x06003CA3 RID: 15523
		public abstract Type BindToType(string assemblyName, string typeName);

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> class.</summary>
		// Token: 0x06003CA4 RID: 15524 RVA: 0x0000259F File Offset: 0x0000079F
		protected SerializationBinder()
		{
		}
	}
}
