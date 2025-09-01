using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
	// Token: 0x020002D2 RID: 722
	[AttributeUsage(AttributeTargets.Field)]
	public class XmlEnumAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class.</summary>
		// Token: 0x06001BF8 RID: 7160 RVA: 0x000021EA File Offset: 0x000003EA
		public XmlEnumAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class, and specifies the XML value that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates or recognizes (when it serializes or deserializes the enumeration, respectively).</summary>
		/// <param name="name">The overriding name of the enumeration member. </param>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x0009E6FE File Offset: 0x0009C8FE
		public XmlEnumAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration, or the value recognized when it deserializes the enumeration member.</summary>
		/// <returns>The value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes the enumeration, or the value recognized when it is deserializes the enumeration member.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0009E70D File Offset: 0x0009C90D
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x0009E715 File Offset: 0x0009C915
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040019F3 RID: 6643
		private string name;
	}
}
