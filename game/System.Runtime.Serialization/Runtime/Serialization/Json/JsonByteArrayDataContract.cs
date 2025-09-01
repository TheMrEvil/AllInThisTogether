using System;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000169 RID: 361
	internal class JsonByteArrayDataContract : JsonDataContract
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x00049E98 File Offset: 0x00048098
		public JsonByteArrayDataContract(ByteArrayDataContract traditionalByteArrayDataContract) : base(traditionalByteArrayDataContract)
		{
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00049EA1 File Offset: 0x000480A1
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			if (context != null)
			{
				return JsonDataContract.HandleReadValue(jsonReader.ReadElementContentAsBase64(), context);
			}
			if (!JsonDataContract.TryReadNullAtTopLevel(jsonReader))
			{
				return jsonReader.ReadElementContentAsBase64();
			}
			return null;
		}
	}
}
