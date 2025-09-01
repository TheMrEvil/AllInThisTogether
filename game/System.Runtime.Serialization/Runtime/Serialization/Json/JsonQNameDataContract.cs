using System;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000185 RID: 389
	internal class JsonQNameDataContract : JsonDataContract
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x00049E98 File Offset: 0x00048098
		public JsonQNameDataContract(QNameDataContract traditionalQNameDataContract) : base(traditionalQNameDataContract)
		{
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004BA39 File Offset: 0x00049C39
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			if (context != null)
			{
				return JsonDataContract.HandleReadValue(jsonReader.ReadElementContentAsQName(), context);
			}
			if (!JsonDataContract.TryReadNullAtTopLevel(jsonReader))
			{
				return jsonReader.ReadElementContentAsQName();
			}
			return null;
		}
	}
}
