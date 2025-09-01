using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies types that should be recognized by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> when serializing or deserializing a given type.</summary>
	// Token: 0x020000EC RID: 236
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
	public sealed class KnownTypeAttribute : Attribute
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x000254FF File Offset: 0x000236FF
		private KnownTypeAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.KnownTypeAttribute" /> class with the specified type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that is included as a known type when serializing or deserializing data.</param>
		// Token: 0x06000D7B RID: 3451 RVA: 0x00035C5F File Offset: 0x00033E5F
		public KnownTypeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.KnownTypeAttribute" /> class with the name of a method that returns an <see cref="T:System.Collections.IEnumerable" /> of known types.</summary>
		/// <param name="methodName">The name of the method that returns an <see cref="T:System.Collections.IEnumerable" /> of types used when serializing or deserializing data.</param>
		// Token: 0x06000D7C RID: 3452 RVA: 0x00035C6E File Offset: 0x00033E6E
		public KnownTypeAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		/// <summary>Gets the name of a method that will return a list of types that should be recognized during serialization or deserialization.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the method on the type defined by the <see cref="T:System.Runtime.Serialization.KnownTypeAttribute" /> class.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00035C7D File Offset: 0x00033E7D
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		/// <summary>Gets the type that should be recognized during serialization or deserialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is used during serialization or deserialization.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00035C85 File Offset: 0x00033E85
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000642 RID: 1602
		private string methodName;

		// Token: 0x04000643 RID: 1603
		private Type type;
	}
}
