using System;
using System.Security;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200016C RID: 364
	internal class JsonCollectionDataContract : JsonDataContract
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x0004A1F5 File Offset: 0x000483F5
		[SecuritySafeCritical]
		public JsonCollectionDataContract(CollectionDataContract traditionalDataContract) : base(new JsonCollectionDataContract.JsonCollectionDataContractCriticalHelper(traditionalDataContract))
		{
			this.helper = (base.Helper as JsonCollectionDataContract.JsonCollectionDataContractCriticalHelper);
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0004A214 File Offset: 0x00048414
		internal JsonFormatCollectionReaderDelegate JsonFormatReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.JsonFormatReaderDelegate == null)
				{
					lock (this)
					{
						if (this.helper.JsonFormatReaderDelegate == null)
						{
							if (this.TraditionalCollectionDataContract.IsReadOnlyContract)
							{
								DataContract.ThrowInvalidDataContractException(this.TraditionalCollectionDataContract.DeserializationExceptionMessage, null);
							}
							JsonFormatCollectionReaderDelegate jsonFormatReaderDelegate = new JsonFormatReaderGenerator().GenerateCollectionReader(this.TraditionalCollectionDataContract);
							Thread.MemoryBarrier();
							this.helper.JsonFormatReaderDelegate = jsonFormatReaderDelegate;
						}
					}
				}
				return this.helper.JsonFormatReaderDelegate;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004A2B0 File Offset: 0x000484B0
		internal JsonFormatGetOnlyCollectionReaderDelegate JsonFormatGetOnlyReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.JsonFormatGetOnlyReaderDelegate == null)
				{
					lock (this)
					{
						if (this.helper.JsonFormatGetOnlyReaderDelegate == null)
						{
							CollectionKind kind = this.TraditionalCollectionDataContract.Kind;
							if (base.TraditionalDataContract.UnderlyingType.IsInterface && (kind == CollectionKind.Enumerable || kind == CollectionKind.Collection || kind == CollectionKind.GenericEnumerable))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("On type '{0}', get-only collection must have an Add method.", new object[]
								{
									DataContract.GetClrTypeFullName(base.TraditionalDataContract.UnderlyingType)
								})));
							}
							if (this.TraditionalCollectionDataContract.IsReadOnlyContract)
							{
								DataContract.ThrowInvalidDataContractException(this.TraditionalCollectionDataContract.DeserializationExceptionMessage, null);
							}
							JsonFormatGetOnlyCollectionReaderDelegate jsonFormatGetOnlyReaderDelegate = new JsonFormatReaderGenerator().GenerateGetOnlyCollectionReader(this.TraditionalCollectionDataContract);
							Thread.MemoryBarrier();
							this.helper.JsonFormatGetOnlyReaderDelegate = jsonFormatGetOnlyReaderDelegate;
						}
					}
				}
				return this.helper.JsonFormatGetOnlyReaderDelegate;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004A3A8 File Offset: 0x000485A8
		internal JsonFormatCollectionWriterDelegate JsonFormatWriterDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.JsonFormatWriterDelegate == null)
				{
					lock (this)
					{
						if (this.helper.JsonFormatWriterDelegate == null)
						{
							JsonFormatCollectionWriterDelegate jsonFormatWriterDelegate = new JsonFormatWriterGenerator().GenerateCollectionWriter(this.TraditionalCollectionDataContract);
							Thread.MemoryBarrier();
							this.helper.JsonFormatWriterDelegate = jsonFormatWriterDelegate;
						}
					}
				}
				return this.helper.JsonFormatWriterDelegate;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0004A424 File Offset: 0x00048624
		private CollectionDataContract TraditionalCollectionDataContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TraditionalCollectionDataContract;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004A434 File Offset: 0x00048634
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			jsonReader.Read();
			object result = null;
			if (context.IsGetOnlyCollection)
			{
				context.IsGetOnlyCollection = false;
				this.JsonFormatGetOnlyReaderDelegate(jsonReader, context, XmlDictionaryString.Empty, JsonGlobals.itemDictionaryString, this.TraditionalCollectionDataContract);
			}
			else
			{
				result = this.JsonFormatReaderDelegate(jsonReader, context, XmlDictionaryString.Empty, JsonGlobals.itemDictionaryString, this.TraditionalCollectionDataContract);
			}
			jsonReader.ReadEndElement();
			return result;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004A49D File Offset: 0x0004869D
		public override void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			context.IsGetOnlyCollection = false;
			this.JsonFormatWriterDelegate(jsonWriter, obj, context, this.TraditionalCollectionDataContract);
		}

		// Token: 0x0400098E RID: 2446
		[SecurityCritical]
		private JsonCollectionDataContract.JsonCollectionDataContractCriticalHelper helper;

		// Token: 0x0200016D RID: 365
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class JsonCollectionDataContractCriticalHelper : JsonDataContract.JsonDataContractCriticalHelper
		{
			// Token: 0x0600131B RID: 4891 RVA: 0x0004A4BA File Offset: 0x000486BA
			public JsonCollectionDataContractCriticalHelper(CollectionDataContract traditionalDataContract) : base(traditionalDataContract)
			{
				this.traditionalCollectionDataContract = traditionalDataContract;
			}

			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x0600131C RID: 4892 RVA: 0x0004A4CA File Offset: 0x000486CA
			// (set) Token: 0x0600131D RID: 4893 RVA: 0x0004A4D2 File Offset: 0x000486D2
			internal JsonFormatCollectionReaderDelegate JsonFormatReaderDelegate
			{
				get
				{
					return this.jsonFormatReaderDelegate;
				}
				set
				{
					this.jsonFormatReaderDelegate = value;
				}
			}

			// Token: 0x1700042E RID: 1070
			// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004A4DB File Offset: 0x000486DB
			// (set) Token: 0x0600131F RID: 4895 RVA: 0x0004A4E3 File Offset: 0x000486E3
			internal JsonFormatGetOnlyCollectionReaderDelegate JsonFormatGetOnlyReaderDelegate
			{
				get
				{
					return this.jsonFormatGetOnlyReaderDelegate;
				}
				set
				{
					this.jsonFormatGetOnlyReaderDelegate = value;
				}
			}

			// Token: 0x1700042F RID: 1071
			// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004A4EC File Offset: 0x000486EC
			// (set) Token: 0x06001321 RID: 4897 RVA: 0x0004A4F4 File Offset: 0x000486F4
			internal JsonFormatCollectionWriterDelegate JsonFormatWriterDelegate
			{
				get
				{
					return this.jsonFormatWriterDelegate;
				}
				set
				{
					this.jsonFormatWriterDelegate = value;
				}
			}

			// Token: 0x17000430 RID: 1072
			// (get) Token: 0x06001322 RID: 4898 RVA: 0x0004A4FD File Offset: 0x000486FD
			internal CollectionDataContract TraditionalCollectionDataContract
			{
				get
				{
					return this.traditionalCollectionDataContract;
				}
			}

			// Token: 0x0400098F RID: 2447
			private JsonFormatCollectionReaderDelegate jsonFormatReaderDelegate;

			// Token: 0x04000990 RID: 2448
			private JsonFormatGetOnlyCollectionReaderDelegate jsonFormatGetOnlyReaderDelegate;

			// Token: 0x04000991 RID: 2449
			private JsonFormatCollectionWriterDelegate jsonFormatWriterDelegate;

			// Token: 0x04000992 RID: 2450
			private CollectionDataContract traditionalCollectionDataContract;
		}
	}
}
