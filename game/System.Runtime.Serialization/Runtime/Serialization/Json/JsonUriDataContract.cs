using System;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200018A RID: 394
	internal class JsonUriDataContract : JsonDataContract
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x00049E98 File Offset: 0x00048098
		public JsonUriDataContract(UriDataContract traditionalUriDataContract) : base(traditionalUriDataContract)
		{
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0004C163 File Offset: 0x0004A363
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			if (context != null)
			{
				return JsonDataContract.HandleReadValue(jsonReader.ReadElementContentAsUri(), context);
			}
			if (!JsonDataContract.TryReadNullAtTopLevel(jsonReader))
			{
				return jsonReader.ReadElementContentAsUri();
			}
			return null;
		}
	}
}
