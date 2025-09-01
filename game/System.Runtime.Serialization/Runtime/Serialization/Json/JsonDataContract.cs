using System;
using System.Collections.Generic;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200016E RID: 366
	internal class JsonDataContract
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x0004A505 File Offset: 0x00048705
		[SecuritySafeCritical]
		protected JsonDataContract(DataContract traditionalDataContract)
		{
			this.helper = new JsonDataContract.JsonDataContractCriticalHelper(traditionalDataContract);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004A519 File Offset: 0x00048719
		[SecuritySafeCritical]
		protected JsonDataContract(JsonDataContract.JsonDataContractCriticalHelper helper)
		{
			this.helper = helper;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0001BF43 File Offset: 0x0001A143
		internal virtual string TypeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004A528 File Offset: 0x00048728
		protected JsonDataContract.JsonDataContractCriticalHelper Helper
		{
			[SecurityCritical]
			get
			{
				return this.helper;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0004A530 File Offset: 0x00048730
		protected DataContract TraditionalDataContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TraditionalDataContract;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004A53D File Offset: 0x0004873D
		private Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.KnownDataContracts;
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004A54A File Offset: 0x0004874A
		[SecuritySafeCritical]
		public static JsonDataContract GetJsonDataContract(DataContract traditionalDataContract)
		{
			return JsonDataContract.JsonDataContractCriticalHelper.GetJsonDataContract(traditionalDataContract);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004A552 File Offset: 0x00048752
		public object ReadJsonValue(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			this.PushKnownDataContracts(context);
			object result = this.ReadJsonValueCore(jsonReader, context);
			this.PopKnownDataContracts(context);
			return result;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004A56A File Offset: 0x0004876A
		public virtual object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			return this.TraditionalDataContract.ReadXmlValue(jsonReader, context);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004A579 File Offset: 0x00048779
		public void WriteJsonValue(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			this.PushKnownDataContracts(context);
			this.WriteJsonValueCore(jsonWriter, obj, context, declaredTypeHandle);
			this.PopKnownDataContracts(context);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004A594 File Offset: 0x00048794
		public virtual void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			this.TraditionalDataContract.WriteXmlValue(jsonWriter, obj, context);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004A5A4 File Offset: 0x000487A4
		protected static object HandleReadValue(object obj, XmlObjectSerializerReadContext context)
		{
			context.AddNewObject(obj);
			return obj;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004A5AE File Offset: 0x000487AE
		protected static bool TryReadNullAtTopLevel(XmlReaderDelegator reader)
		{
			if (!reader.MoveToAttribute("type") || !(reader.Value == "null"))
			{
				reader.MoveToElement();
				return false;
			}
			reader.Skip();
			reader.MoveToElement();
			return true;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004A5E8 File Offset: 0x000487E8
		protected void PopKnownDataContracts(XmlObjectSerializerContext context)
		{
			if (this.KnownDataContracts != null)
			{
				context.scopedKnownTypes.Pop();
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004A5FD File Offset: 0x000487FD
		protected void PushKnownDataContracts(XmlObjectSerializerContext context)
		{
			if (this.KnownDataContracts != null)
			{
				context.scopedKnownTypes.Push(this.KnownDataContracts);
			}
		}

		// Token: 0x04000993 RID: 2451
		[SecurityCritical]
		private JsonDataContract.JsonDataContractCriticalHelper helper;

		// Token: 0x0200016F RID: 367
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal class JsonDataContractCriticalHelper
		{
			// Token: 0x06001332 RID: 4914 RVA: 0x0004A618 File Offset: 0x00048818
			internal JsonDataContractCriticalHelper(DataContract traditionalDataContract)
			{
				this.traditionalDataContract = traditionalDataContract;
				this.AddCollectionItemContractsToKnownDataContracts();
				this.typeName = (string.IsNullOrEmpty(traditionalDataContract.Namespace.Value) ? traditionalDataContract.Name.Value : (traditionalDataContract.Name.Value + ":" + XmlObjectSerializerWriteContextComplexJson.TruncateDefaultDataContractNamespace(traditionalDataContract.Namespace.Value)));
			}

			// Token: 0x17000435 RID: 1077
			// (get) Token: 0x06001333 RID: 4915 RVA: 0x0004A682 File Offset: 0x00048882
			internal Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
			{
				get
				{
					return this.knownDataContracts;
				}
			}

			// Token: 0x17000436 RID: 1078
			// (get) Token: 0x06001334 RID: 4916 RVA: 0x0004A68A File Offset: 0x0004888A
			internal DataContract TraditionalDataContract
			{
				get
				{
					return this.traditionalDataContract;
				}
			}

			// Token: 0x17000437 RID: 1079
			// (get) Token: 0x06001335 RID: 4917 RVA: 0x0004A692 File Offset: 0x00048892
			internal virtual string TypeName
			{
				get
				{
					return this.typeName;
				}
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x0004A69C File Offset: 0x0004889C
			public static JsonDataContract GetJsonDataContract(DataContract traditionalDataContract)
			{
				int id = JsonDataContract.JsonDataContractCriticalHelper.GetId(traditionalDataContract.UnderlyingType.TypeHandle);
				JsonDataContract jsonDataContract = JsonDataContract.JsonDataContractCriticalHelper.dataContractCache[id];
				if (jsonDataContract == null)
				{
					jsonDataContract = JsonDataContract.JsonDataContractCriticalHelper.CreateJsonDataContract(id, traditionalDataContract);
					JsonDataContract.JsonDataContractCriticalHelper.dataContractCache[id] = jsonDataContract;
				}
				return jsonDataContract;
			}

			// Token: 0x06001337 RID: 4919 RVA: 0x0004A6D8 File Offset: 0x000488D8
			internal static int GetId(RuntimeTypeHandle typeHandle)
			{
				object obj = JsonDataContract.JsonDataContractCriticalHelper.cacheLock;
				int value;
				lock (obj)
				{
					JsonDataContract.JsonDataContractCriticalHelper.typeHandleRef.Value = typeHandle;
					IntRef intRef;
					if (!JsonDataContract.JsonDataContractCriticalHelper.typeToIDCache.TryGetValue(JsonDataContract.JsonDataContractCriticalHelper.typeHandleRef, out intRef))
					{
						int num = JsonDataContract.JsonDataContractCriticalHelper.dataContractID++;
						if (num >= JsonDataContract.JsonDataContractCriticalHelper.dataContractCache.Length)
						{
							int num2 = (num < 1073741823) ? (num * 2) : int.MaxValue;
							if (num2 <= num)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("An internal error has occurred. DataContract cache overflow.")));
							}
							Array.Resize<JsonDataContract>(ref JsonDataContract.JsonDataContractCriticalHelper.dataContractCache, num2);
						}
						intRef = new IntRef(num);
						try
						{
							JsonDataContract.JsonDataContractCriticalHelper.typeToIDCache.Add(new TypeHandleRef(typeHandle), intRef);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
						}
					}
					value = intRef.Value;
				}
				return value;
			}

			// Token: 0x06001338 RID: 4920 RVA: 0x0004A7D0 File Offset: 0x000489D0
			private static JsonDataContract CreateJsonDataContract(int id, DataContract traditionalDataContract)
			{
				object obj = JsonDataContract.JsonDataContractCriticalHelper.createDataContractLock;
				JsonDataContract result;
				lock (obj)
				{
					JsonDataContract jsonDataContract = JsonDataContract.JsonDataContractCriticalHelper.dataContractCache[id];
					if (jsonDataContract == null)
					{
						Type type = traditionalDataContract.GetType();
						if (type == typeof(ObjectDataContract))
						{
							jsonDataContract = new JsonObjectDataContract(traditionalDataContract);
						}
						else if (type == typeof(StringDataContract))
						{
							jsonDataContract = new JsonStringDataContract((StringDataContract)traditionalDataContract);
						}
						else if (type == typeof(UriDataContract))
						{
							jsonDataContract = new JsonUriDataContract((UriDataContract)traditionalDataContract);
						}
						else if (type == typeof(QNameDataContract))
						{
							jsonDataContract = new JsonQNameDataContract((QNameDataContract)traditionalDataContract);
						}
						else if (type == typeof(ByteArrayDataContract))
						{
							jsonDataContract = new JsonByteArrayDataContract((ByteArrayDataContract)traditionalDataContract);
						}
						else if (traditionalDataContract.IsPrimitive || traditionalDataContract.UnderlyingType == Globals.TypeOfXmlQualifiedName)
						{
							jsonDataContract = new JsonDataContract(traditionalDataContract);
						}
						else if (type == typeof(ClassDataContract))
						{
							jsonDataContract = new JsonClassDataContract((ClassDataContract)traditionalDataContract);
						}
						else if (type == typeof(EnumDataContract))
						{
							jsonDataContract = new JsonEnumDataContract((EnumDataContract)traditionalDataContract);
						}
						else if (type == typeof(GenericParameterDataContract) || type == typeof(SpecialTypeDataContract))
						{
							jsonDataContract = new JsonDataContract(traditionalDataContract);
						}
						else if (type == typeof(CollectionDataContract))
						{
							jsonDataContract = new JsonCollectionDataContract((CollectionDataContract)traditionalDataContract);
						}
						else
						{
							if (!(type == typeof(XmlDataContract)))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("traditionalDataContract", SR.GetString("Type '{0}' is not suppotred by DataContractJsonSerializer.", new object[]
								{
									traditionalDataContract.UnderlyingType
								}));
							}
							jsonDataContract = new JsonXmlDataContract((XmlDataContract)traditionalDataContract);
						}
					}
					result = jsonDataContract;
				}
				return result;
			}

			// Token: 0x06001339 RID: 4921 RVA: 0x0004A9D8 File Offset: 0x00048BD8
			private void AddCollectionItemContractsToKnownDataContracts()
			{
				if (this.traditionalDataContract.KnownDataContracts != null)
				{
					foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in this.traditionalDataContract.KnownDataContracts)
					{
						if (keyValuePair != null)
						{
							DataContract itemContract;
							for (CollectionDataContract collectionDataContract = keyValuePair.Value as CollectionDataContract; collectionDataContract != null; collectionDataContract = (itemContract as CollectionDataContract))
							{
								itemContract = collectionDataContract.ItemContract;
								if (this.knownDataContracts == null)
								{
									this.knownDataContracts = new Dictionary<XmlQualifiedName, DataContract>();
								}
								if (!this.knownDataContracts.ContainsKey(itemContract.StableName))
								{
									this.knownDataContracts.Add(itemContract.StableName, itemContract);
								}
								if (collectionDataContract.ItemType.IsGenericType && collectionDataContract.ItemType.GetGenericTypeDefinition() == typeof(KeyValue<, >))
								{
									DataContract dataContract = DataContract.GetDataContract(Globals.TypeOfKeyValuePair.MakeGenericType(collectionDataContract.ItemType.GetGenericArguments()));
									if (!this.knownDataContracts.ContainsKey(dataContract.StableName))
									{
										this.knownDataContracts.Add(dataContract.StableName, dataContract);
									}
								}
								if (!(itemContract is CollectionDataContract))
								{
									break;
								}
							}
						}
					}
				}
			}

			// Token: 0x0600133A RID: 4922 RVA: 0x0004AB20 File Offset: 0x00048D20
			// Note: this type is marked as 'beforefieldinit'.
			static JsonDataContractCriticalHelper()
			{
			}

			// Token: 0x04000994 RID: 2452
			private static object cacheLock = new object();

			// Token: 0x04000995 RID: 2453
			private static object createDataContractLock = new object();

			// Token: 0x04000996 RID: 2454
			private static JsonDataContract[] dataContractCache = new JsonDataContract[32];

			// Token: 0x04000997 RID: 2455
			private static int dataContractID = 0;

			// Token: 0x04000998 RID: 2456
			private static TypeHandleRef typeHandleRef = new TypeHandleRef();

			// Token: 0x04000999 RID: 2457
			private static Dictionary<TypeHandleRef, IntRef> typeToIDCache = new Dictionary<TypeHandleRef, IntRef>(new TypeHandleRefEqualityComparer());

			// Token: 0x0400099A RID: 2458
			private Dictionary<XmlQualifiedName, DataContract> knownDataContracts;

			// Token: 0x0400099B RID: 2459
			private DataContract traditionalDataContract;

			// Token: 0x0400099C RID: 2460
			private string typeName;
		}
	}
}
