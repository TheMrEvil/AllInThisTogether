using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200018C RID: 396
	internal class JsonXmlDataContract : JsonDataContract
	{
		// Token: 0x060013D4 RID: 5076 RVA: 0x00049E98 File Offset: 0x00048098
		public JsonXmlDataContract(XmlDataContract traditionalXmlDataContract) : base(traditionalXmlDataContract)
		{
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004C6C8 File Offset: 0x0004A8C8
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			string s = jsonReader.ReadElementContentAsString();
			DataContractSerializer dataContractSerializer = new DataContractSerializer(base.TraditionalDataContract.UnderlyingType, this.GetKnownTypesFromContext(context, (context == null) ? null : context.SerializerKnownTypeList), 1, false, false, null);
			MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
			XmlDictionaryReaderQuotas readerQuotas = ((JsonReaderDelegator)jsonReader).ReaderQuotas;
			object obj;
			if (readerQuotas == null)
			{
				obj = dataContractSerializer.ReadObject(stream);
			}
			else
			{
				obj = dataContractSerializer.ReadObject(XmlDictionaryReader.CreateTextReader(stream, readerQuotas));
			}
			if (context != null)
			{
				context.AddNewObject(obj);
			}
			return obj;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004C74C File Offset: 0x0004A94C
		public override void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			XmlObjectSerializer xmlObjectSerializer = new DataContractSerializer(Type.GetTypeFromHandle(declaredTypeHandle), this.GetKnownTypesFromContext(context, (context == null) ? null : context.SerializerKnownTypeList), 1, false, false, null);
			MemoryStream memoryStream = new MemoryStream();
			xmlObjectSerializer.WriteObject(memoryStream, obj);
			memoryStream.Position = 0L;
			string value = new StreamReader(memoryStream).ReadToEnd();
			jsonWriter.WriteString(value);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
		private List<Type> GetKnownTypesFromContext(XmlObjectSerializerContext context, IList<Type> serializerKnownTypeList)
		{
			List<Type> list = new List<Type>();
			if (context != null)
			{
				List<XmlQualifiedName> list2 = new List<XmlQualifiedName>();
				Dictionary<XmlQualifiedName, DataContract>[] dataContractDictionaries = context.scopedKnownTypes.dataContractDictionaries;
				if (dataContractDictionaries != null)
				{
					foreach (Dictionary<XmlQualifiedName, DataContract> dictionary in dataContractDictionaries)
					{
						if (dictionary != null)
						{
							foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in dictionary)
							{
								if (!list2.Contains(keyValuePair.Key))
								{
									list2.Add(keyValuePair.Key);
									list.Add(keyValuePair.Value.UnderlyingType);
								}
							}
						}
					}
				}
				if (serializerKnownTypeList != null)
				{
					list.AddRange(serializerKnownTypeList);
				}
			}
			return list;
		}
	}
}
