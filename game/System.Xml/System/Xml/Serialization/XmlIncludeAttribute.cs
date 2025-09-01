using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object.</summary>
	// Token: 0x020002D4 RID: 724
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class XmlIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlIncludeAttribute" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to include. </param>
		// Token: 0x06001BFD RID: 7165 RVA: 0x0009E71E File Offset: 0x0009C91E
		public XmlIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to include.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object to include.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0009E72D File Offset: 0x0009C92D
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0009E735 File Offset: 0x0009C935
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

		// Token: 0x040019F4 RID: 6644
		private Type type;
	}
}
