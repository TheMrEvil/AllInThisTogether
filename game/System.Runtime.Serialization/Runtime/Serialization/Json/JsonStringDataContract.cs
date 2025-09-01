using System;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000189 RID: 393
	internal class JsonStringDataContract : JsonDataContract
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x00049E98 File Offset: 0x00048098
		public JsonStringDataContract(StringDataContract traditionalStringDataContract) : base(traditionalStringDataContract)
		{
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0004C141 File Offset: 0x0004A341
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			if (context != null)
			{
				return JsonDataContract.HandleReadValue(jsonReader.ReadElementContentAsString(), context);
			}
			if (!JsonDataContract.TryReadNullAtTopLevel(jsonReader))
			{
				return jsonReader.ReadElementContentAsString();
			}
			return null;
		}
	}
}
