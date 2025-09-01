using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000103 RID: 259
	internal class FloatDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x000372A6 File Offset: 0x000354A6
		internal FloatDataContract() : base(typeof(float), DictionaryGlobals.FloatLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000372C2 File Offset: 0x000354C2
		internal override string WriteMethodName
		{
			get
			{
				return "WriteFloat";
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000372C9 File Offset: 0x000354C9
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsFloat";
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000372D0 File Offset: 0x000354D0
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteFloat((float)obj);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000372DE File Offset: 0x000354DE
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsFloat(), context);
			}
			return reader.ReadElementContentAsFloat();
		}
	}
}
