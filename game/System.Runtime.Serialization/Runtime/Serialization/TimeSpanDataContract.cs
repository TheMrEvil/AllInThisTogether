using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200011E RID: 286
	internal class TimeSpanDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x000376F0 File Offset: 0x000358F0
		internal TimeSpanDataContract() : this(DictionaryGlobals.TimeSpanLocalName, DictionaryGlobals.SerializationNamespace)
		{
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00037702 File Offset: 0x00035902
		internal TimeSpanDataContract(XmlDictionaryString name, XmlDictionaryString ns) : base(typeof(TimeSpan), name, ns)
		{
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00037716 File Offset: 0x00035916
		internal override string WriteMethodName
		{
			get
			{
				return "WriteTimeSpan";
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0003771D File Offset: 0x0003591D
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsTimeSpan";
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00037724 File Offset: 0x00035924
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteTimeSpan((TimeSpan)obj);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00037732 File Offset: 0x00035932
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsTimeSpan(), context);
			}
			return reader.ReadElementContentAsTimeSpan();
		}
	}
}
