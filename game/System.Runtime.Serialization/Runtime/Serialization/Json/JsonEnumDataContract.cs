using System;
using System.Security;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000172 RID: 370
	internal class JsonEnumDataContract : JsonDataContract
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x0004B419 File Offset: 0x00049619
		[SecuritySafeCritical]
		public JsonEnumDataContract(EnumDataContract traditionalDataContract) : base(new JsonEnumDataContract.JsonEnumDataContractCriticalHelper(traditionalDataContract))
		{
			this.helper = (base.Helper as JsonEnumDataContract.JsonEnumDataContractCriticalHelper);
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0004B438 File Offset: 0x00049638
		public bool IsULong
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsULong;
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004B448 File Offset: 0x00049648
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			object obj;
			if (this.IsULong)
			{
				obj = Enum.ToObject(base.TraditionalDataContract.UnderlyingType, jsonReader.ReadElementContentAsUnsignedLong());
			}
			else
			{
				obj = Enum.ToObject(base.TraditionalDataContract.UnderlyingType, jsonReader.ReadElementContentAsLong());
			}
			if (context != null)
			{
				context.AddNewObject(obj);
			}
			return obj;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0004B498 File Offset: 0x00049698
		public override void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			if (this.IsULong)
			{
				jsonWriter.WriteUnsignedLong(((IConvertible)obj).ToUInt64(null));
				return;
			}
			jsonWriter.WriteLong(((IConvertible)obj).ToInt64(null));
		}

		// Token: 0x040009B4 RID: 2484
		[SecurityCritical]
		private JsonEnumDataContract.JsonEnumDataContractCriticalHelper helper;

		// Token: 0x02000173 RID: 371
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class JsonEnumDataContractCriticalHelper : JsonDataContract.JsonDataContractCriticalHelper
		{
			// Token: 0x06001361 RID: 4961 RVA: 0x0004B4C7 File Offset: 0x000496C7
			public JsonEnumDataContractCriticalHelper(EnumDataContract traditionalEnumDataContract) : base(traditionalEnumDataContract)
			{
				this.isULong = traditionalEnumDataContract.IsULong;
			}

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x06001362 RID: 4962 RVA: 0x0004B4DC File Offset: 0x000496DC
			public bool IsULong
			{
				get
				{
					return this.isULong;
				}
			}

			// Token: 0x040009B5 RID: 2485
			private bool isULong;
		}
	}
}
