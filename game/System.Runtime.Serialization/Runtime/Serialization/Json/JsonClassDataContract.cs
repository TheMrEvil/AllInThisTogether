using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200016A RID: 362
	internal class JsonClassDataContract : JsonDataContract
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x00049EC3 File Offset: 0x000480C3
		[SecuritySafeCritical]
		public JsonClassDataContract(ClassDataContract traditionalDataContract) : base(new JsonClassDataContract.JsonClassDataContractCriticalHelper(traditionalDataContract))
		{
			this.helper = (base.Helper as JsonClassDataContract.JsonClassDataContractCriticalHelper);
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00049EE4 File Offset: 0x000480E4
		internal JsonFormatClassReaderDelegate JsonFormatReaderDelegate
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
							if (this.TraditionalClassDataContract.IsReadOnlyContract)
							{
								DataContract.ThrowInvalidDataContractException(this.TraditionalClassDataContract.DeserializationExceptionMessage, null);
							}
							JsonFormatClassReaderDelegate jsonFormatReaderDelegate = new JsonFormatReaderGenerator().GenerateClassReader(this.TraditionalClassDataContract);
							Thread.MemoryBarrier();
							this.helper.JsonFormatReaderDelegate = jsonFormatReaderDelegate;
						}
					}
				}
				return this.helper.JsonFormatReaderDelegate;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00049F80 File Offset: 0x00048180
		internal JsonFormatClassWriterDelegate JsonFormatWriterDelegate
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
							JsonFormatClassWriterDelegate jsonFormatWriterDelegate = new JsonFormatWriterGenerator().GenerateClassWriter(this.TraditionalClassDataContract);
							Thread.MemoryBarrier();
							this.helper.JsonFormatWriterDelegate = jsonFormatWriterDelegate;
						}
					}
				}
				return this.helper.JsonFormatWriterDelegate;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00049FFC File Offset: 0x000481FC
		internal XmlDictionaryString[] MemberNames
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.MemberNames;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0004A009 File Offset: 0x00048209
		internal override string TypeName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TypeName;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004A016 File Offset: 0x00048216
		private ClassDataContract TraditionalClassDataContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TraditionalClassDataContract;
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004A023 File Offset: 0x00048223
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			jsonReader.Read();
			object result = this.JsonFormatReaderDelegate(jsonReader, context, XmlDictionaryString.Empty, this.MemberNames);
			jsonReader.ReadEndElement();
			return result;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004A04A File Offset: 0x0004824A
		public override void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			jsonWriter.WriteAttributeString(null, "type", null, "object");
			this.JsonFormatWriterDelegate(jsonWriter, obj, context, this.TraditionalClassDataContract, this.MemberNames);
		}

		// Token: 0x04000988 RID: 2440
		[SecurityCritical]
		private JsonClassDataContract.JsonClassDataContractCriticalHelper helper;

		// Token: 0x0200016B RID: 363
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class JsonClassDataContractCriticalHelper : JsonDataContract.JsonDataContractCriticalHelper
		{
			// Token: 0x0600130C RID: 4876 RVA: 0x0004A078 File Offset: 0x00048278
			public JsonClassDataContractCriticalHelper(ClassDataContract traditionalDataContract) : base(traditionalDataContract)
			{
				this.typeName = (string.IsNullOrEmpty(traditionalDataContract.Namespace.Value) ? traditionalDataContract.Name.Value : (traditionalDataContract.Name.Value + ":" + XmlObjectSerializerWriteContextComplexJson.TruncateDefaultDataContractNamespace(traditionalDataContract.Namespace.Value)));
				this.traditionalClassDataContract = traditionalDataContract;
				this.CopyMembersAndCheckDuplicateNames();
			}

			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x0600130D RID: 4877 RVA: 0x0004A0E3 File Offset: 0x000482E3
			// (set) Token: 0x0600130E RID: 4878 RVA: 0x0004A0EB File Offset: 0x000482EB
			internal JsonFormatClassReaderDelegate JsonFormatReaderDelegate
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

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x0600130F RID: 4879 RVA: 0x0004A0F4 File Offset: 0x000482F4
			// (set) Token: 0x06001310 RID: 4880 RVA: 0x0004A0FC File Offset: 0x000482FC
			internal JsonFormatClassWriterDelegate JsonFormatWriterDelegate
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

			// Token: 0x17000427 RID: 1063
			// (get) Token: 0x06001311 RID: 4881 RVA: 0x0004A105 File Offset: 0x00048305
			internal XmlDictionaryString[] MemberNames
			{
				get
				{
					return this.memberNames;
				}
			}

			// Token: 0x17000428 RID: 1064
			// (get) Token: 0x06001312 RID: 4882 RVA: 0x0004A10D File Offset: 0x0004830D
			internal ClassDataContract TraditionalClassDataContract
			{
				get
				{
					return this.traditionalClassDataContract;
				}
			}

			// Token: 0x06001313 RID: 4883 RVA: 0x0004A118 File Offset: 0x00048318
			private void CopyMembersAndCheckDuplicateNames()
			{
				if (this.traditionalClassDataContract.MemberNames != null)
				{
					int num = this.traditionalClassDataContract.MemberNames.Length;
					Dictionary<string, object> dictionary = new Dictionary<string, object>(num);
					XmlDictionaryString[] array = new XmlDictionaryString[num];
					for (int i = 0; i < num; i++)
					{
						if (dictionary.ContainsKey(this.traditionalClassDataContract.MemberNames[i].Value))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Duplicate member, including '{1}', is found in JSON input, in type '{0}'.", new object[]
							{
								DataContract.GetClrTypeFullName(this.traditionalClassDataContract.UnderlyingType),
								this.traditionalClassDataContract.MemberNames[i].Value
							})));
						}
						dictionary.Add(this.traditionalClassDataContract.MemberNames[i].Value, null);
						array[i] = DataContractJsonSerializer.ConvertXmlNameToJsonName(this.traditionalClassDataContract.MemberNames[i]);
					}
					this.memberNames = array;
				}
			}

			// Token: 0x04000989 RID: 2441
			private JsonFormatClassReaderDelegate jsonFormatReaderDelegate;

			// Token: 0x0400098A RID: 2442
			private JsonFormatClassWriterDelegate jsonFormatWriterDelegate;

			// Token: 0x0400098B RID: 2443
			private XmlDictionaryString[] memberNames;

			// Token: 0x0400098C RID: 2444
			private ClassDataContract traditionalClassDataContract;

			// Token: 0x0400098D RID: 2445
			private string typeName;
		}
	}
}
