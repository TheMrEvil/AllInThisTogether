using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object as encoded SOAP XML.</summary>
	// Token: 0x020002B2 RID: 690
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class SoapIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapIncludeAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type of the object to include. </param>
		// Token: 0x060019F3 RID: 6643 RVA: 0x00094D55 File Offset: 0x00092F55
		public SoapIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to use when serializing or deserializing an object.</summary>
		/// <returns>The type of the object to include.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x00094D64 File Offset: 0x00092F64
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x00094D6C File Offset: 0x00092F6C
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04001951 RID: 6481
		private Type type;
	}
}
