using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000198 RID: 408
	internal class JsonFormatWriterInterpreter
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x00052165 File Offset: 0x00050365
		public JsonFormatWriterInterpreter(ClassDataContract classContract)
		{
			this.classContract = classContract;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005217B File Offset: 0x0005037B
		public JsonFormatWriterInterpreter(CollectionDataContract collectionContract)
		{
			this.collectionContract = collectionContract;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00052191 File Offset: 0x00050391
		private ClassDataContract classDataContract
		{
			get
			{
				return (ClassDataContract)this.dataContract;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0005219E File Offset: 0x0005039E
		private CollectionDataContract collectionDataContract
		{
			get
			{
				return (CollectionDataContract)this.dataContract;
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x000521AC File Offset: 0x000503AC
		public void WriteToJson(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, ClassDataContract dataContract, XmlDictionaryString[] memberNames)
		{
			this.writer = xmlWriter;
			this.obj = obj;
			this.context = context;
			this.dataContract = dataContract;
			this.memberNames = memberNames;
			this.InitArgs(this.classContract.UnderlyingType);
			if (this.classContract.IsReadOnlyContract)
			{
				DataContract.ThrowInvalidDataContractException(this.classContract.SerializationExceptionMessage, null);
			}
			this.WriteClass(this.classContract);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005221C File Offset: 0x0005041C
		public void WriteCollectionToJson(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, CollectionDataContract dataContract)
		{
			this.writer = xmlWriter;
			this.obj = obj;
			this.context = context;
			this.dataContract = dataContract;
			this.InitArgs(this.collectionContract.UnderlyingType);
			if (this.collectionContract.IsReadOnlyContract)
			{
				DataContract.ThrowInvalidDataContractException(this.collectionContract.SerializationExceptionMessage, null);
			}
			this.WriteCollection(this.collectionContract);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00052284 File Offset: 0x00050484
		private void InitArgs(Type objType)
		{
			if (objType == Globals.TypeOfDateTimeOffsetAdapter)
			{
				this.objLocal = DateTimeOffsetAdapter.GetDateTimeOffsetAdapter((DateTimeOffset)this.obj);
				return;
			}
			this.objLocal = CodeInterpreter.ConvertValue(this.obj, typeof(object), objType);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000522D8 File Offset: 0x000504D8
		private void InvokeOnSerializing(ClassDataContract classContract, object objSerialized, XmlObjectSerializerWriteContext context)
		{
			if (classContract.BaseContract != null)
			{
				this.InvokeOnSerializing(classContract.BaseContract, objSerialized, context);
			}
			if (classContract.OnSerializing != null)
			{
				classContract.OnSerializing.Invoke(objSerialized, new object[]
				{
					context.GetStreamingContext()
				});
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005232C File Offset: 0x0005052C
		private void InvokeOnSerialized(ClassDataContract classContract, object objSerialized, XmlObjectSerializerWriteContext context)
		{
			if (classContract.BaseContract != null)
			{
				this.InvokeOnSerialized(classContract.BaseContract, objSerialized, context);
			}
			if (classContract.OnSerialized != null)
			{
				classContract.OnSerialized.Invoke(objSerialized, new object[]
				{
					context.GetStreamingContext()
				});
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00052380 File Offset: 0x00050580
		private void WriteClass(ClassDataContract classContract)
		{
			this.InvokeOnSerializing(classContract, this.objLocal, this.context);
			if (classContract.IsISerializable)
			{
				this.context.WriteJsonISerializable(this.writer, (ISerializable)this.objLocal);
			}
			else if (classContract.HasExtensionData)
			{
				ExtensionDataObject extensionData = ((IExtensibleDataObject)this.objLocal).ExtensionData;
				this.context.WriteExtensionData(this.writer, extensionData, -1);
				this.WriteMembers(classContract, extensionData, classContract);
			}
			else
			{
				this.WriteMembers(classContract, null, classContract);
			}
			this.InvokeOnSerialized(classContract, this.objLocal, this.context);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005241C File Offset: 0x0005061C
		private void WriteCollection(CollectionDataContract collectionContract)
		{
			XmlDictionaryString collectionItemName = this.context.CollectionItemName;
			if (collectionContract.Kind == CollectionKind.Array)
			{
				Type itemType = collectionContract.ItemType;
				if (this.objLocal.GetType().GetElementType() != itemType)
				{
					throw new InvalidCastException(string.Format("Cannot cast array of {0} to array of {1}", this.objLocal.GetType().GetElementType(), itemType));
				}
				this.context.IncrementArrayCount(this.writer, (Array)this.objLocal);
				if (!this.TryWritePrimitiveArray(collectionContract.UnderlyingType, itemType, () => this.objLocal, collectionItemName))
				{
					this.WriteArrayAttribute();
					Array array = (Array)this.objLocal;
					int[] array2 = new int[1];
					for (int i = 0; i < array.Length; i++)
					{
						if (!this.TryWritePrimitive(itemType, null, null, new int?(i), collectionItemName, 0))
						{
							this.WriteStartElement(collectionItemName, 0);
							array2[0] = i;
							object value = array.GetValue(array2);
							this.WriteValue(itemType, value);
							this.WriteEndElement();
						}
					}
					return;
				}
			}
			else
			{
				if (!collectionContract.UnderlyingType.IsAssignableFrom(this.objLocal.GetType()))
				{
					throw new InvalidCastException(string.Format("Cannot cast {0} to {1}", this.objLocal.GetType(), collectionContract.UnderlyingType));
				}
				MethodInfo methodInfo = null;
				switch (collectionContract.Kind)
				{
				case CollectionKind.GenericDictionary:
					methodInfo = XmlFormatGeneratorStatics.IncrementCollectionCountGenericMethod.MakeGenericMethod(new Type[]
					{
						Globals.TypeOfKeyValuePair.MakeGenericType(collectionContract.ItemType.GetGenericArguments())
					});
					break;
				case CollectionKind.Dictionary:
				case CollectionKind.List:
				case CollectionKind.Collection:
					methodInfo = XmlFormatGeneratorStatics.IncrementCollectionCountMethod;
					break;
				case CollectionKind.GenericList:
				case CollectionKind.GenericCollection:
					methodInfo = XmlFormatGeneratorStatics.IncrementCollectionCountGenericMethod.MakeGenericMethod(new Type[]
					{
						collectionContract.ItemType
					});
					break;
				}
				if (methodInfo != null)
				{
					methodInfo.Invoke(this.context, new object[]
					{
						this.writer,
						this.objLocal
					});
				}
				bool flag = false;
				bool flag2 = false;
				Type[] typeArguments = null;
				Type type;
				if (collectionContract.Kind == CollectionKind.GenericDictionary)
				{
					flag2 = true;
					typeArguments = collectionContract.ItemType.GetGenericArguments();
					type = Globals.TypeOfGenericDictionaryEnumerator.MakeGenericType(typeArguments);
				}
				else if (collectionContract.Kind == CollectionKind.Dictionary)
				{
					flag = true;
					typeArguments = new Type[]
					{
						Globals.TypeOfObject,
						Globals.TypeOfObject
					};
					type = Globals.TypeOfDictionaryEnumerator;
				}
				else
				{
					type = collectionContract.GetEnumeratorMethod.ReturnType;
				}
				MethodInfo methodInfo2 = type.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
				MethodInfo methodInfo3 = type.GetMethod("get_Current", BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
				if (methodInfo2 == null || methodInfo3 == null)
				{
					if (type.IsInterface)
					{
						if (methodInfo2 == null)
						{
							methodInfo2 = JsonFormatGeneratorStatics.MoveNextMethod;
						}
						if (methodInfo3 == null)
						{
							methodInfo3 = JsonFormatGeneratorStatics.GetCurrentMethod;
						}
					}
					else
					{
						Type interfaceType = Globals.TypeOfIEnumerator;
						CollectionKind kind = collectionContract.Kind;
						if (kind == CollectionKind.GenericDictionary || kind == CollectionKind.GenericCollection || kind == CollectionKind.GenericEnumerable)
						{
							foreach (Type type2 in type.GetInterfaces())
							{
								if (type2.IsGenericType && type2.GetGenericTypeDefinition() == Globals.TypeOfIEnumeratorGeneric && type2.GetGenericArguments()[0] == collectionContract.ItemType)
								{
									interfaceType = type2;
									break;
								}
							}
						}
						if (methodInfo2 == null)
						{
							methodInfo2 = CollectionDataContract.GetTargetMethodWithName("MoveNext", type, interfaceType);
						}
						if (methodInfo3 == null)
						{
							methodInfo3 = CollectionDataContract.GetTargetMethodWithName("get_Current", type, interfaceType);
						}
					}
				}
				Type returnType = methodInfo3.ReturnType;
				object currentValue = null;
				IEnumerator enumerator = (IEnumerator)collectionContract.GetEnumeratorMethod.Invoke(this.objLocal, new object[0]);
				if (flag)
				{
					enumerator = (IEnumerator)type.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						Globals.TypeOfIDictionaryEnumerator
					}, null).Invoke(new object[]
					{
						enumerator
					});
				}
				else if (flag2)
				{
					Type type3 = Globals.TypeOfIEnumeratorGeneric.MakeGenericType(new Type[]
					{
						Globals.TypeOfKeyValuePair.MakeGenericType(typeArguments)
					});
					type.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						type3
					}, null);
					enumerator = (IEnumerator)Activator.CreateInstance(type, new object[]
					{
						enumerator
					});
				}
				bool flag3 = flag || flag2;
				bool flag4 = flag3 && this.context.UseSimpleDictionaryFormat;
				PropertyInfo memberInfo = null;
				PropertyInfo propertyInfo = null;
				if (flag3)
				{
					Type type4 = Globals.TypeOfKeyValue.MakeGenericType(typeArguments);
					memberInfo = type4.GetProperty("Key");
					propertyInfo = type4.GetProperty("Value");
				}
				if (flag4)
				{
					this.WriteObjectAttribute();
					object[] parameters = new object[0];
					while ((bool)methodInfo2.Invoke(enumerator, parameters))
					{
						currentValue = methodInfo3.Invoke(enumerator, parameters);
						object member = CodeInterpreter.GetMember(memberInfo, currentValue);
						object member2 = CodeInterpreter.GetMember(propertyInfo, currentValue);
						this.WriteStartElement(member, 0);
						this.WriteValue(propertyInfo.PropertyType, member2);
						this.WriteEndElement();
					}
					return;
				}
				this.WriteArrayAttribute();
				object[] parameters2 = new object[0];
				Func<object> <>9__1;
				while (enumerator != null && enumerator.MoveNext())
				{
					currentValue = methodInfo3.Invoke(enumerator, parameters2);
					if (methodInfo == null)
					{
						XmlFormatGeneratorStatics.IncrementItemCountMethod.Invoke(this.context, new object[]
						{
							1
						});
					}
					Type type5 = returnType;
					Func<object> value2;
					if ((value2 = <>9__1) == null)
					{
						value2 = (<>9__1 = (() => currentValue));
					}
					if (!this.TryWritePrimitive(type5, value2, null, null, collectionItemName, 0))
					{
						this.WriteStartElement(collectionItemName, 0);
						if (flag2 || flag)
						{
							DataContractJsonSerializer.WriteJsonValue(JsonDataContract.GetJsonDataContract(XmlObjectSerializerWriteContextComplexJson.GetRevisedItemContract(this.collectionDataContract.ItemContract)), this.writer, currentValue, this.context, currentValue.GetType().TypeHandle);
						}
						else
						{
							this.WriteValue(returnType, currentValue);
						}
						this.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00052A1C File Offset: 0x00050C1C
		private int WriteMembers(ClassDataContract classContract, ExtensionDataObject extensionData, ClassDataContract derivedMostClassContract)
		{
			int num = (classContract.BaseContract == null) ? 0 : this.WriteMembers(classContract.BaseContract, extensionData, derivedMostClassContract);
			this.context.IncrementItemCount(classContract.Members.Count);
			int i = 0;
			while (i < classContract.Members.Count)
			{
				DataMember dataMember = classContract.Members[i];
				Type memberType = dataMember.MemberType;
				object memberValue = null;
				if (dataMember.IsGetOnlyCollection)
				{
					this.context.StoreIsGetOnlyCollection();
				}
				bool flag = true;
				bool flag2 = false;
				if (!dataMember.EmitDefaultValue)
				{
					flag2 = true;
					memberValue = this.LoadMemberValue(dataMember);
					flag = !this.IsDefaultValue(memberType, memberValue);
				}
				if (flag)
				{
					bool flag3 = DataContractJsonSerializer.CheckIfXmlNameRequiresMapping(classContract.MemberNames[i]);
					if (flag3 || !this.TryWritePrimitive(memberType, flag2 ? (() => memberValue) : null, dataMember.MemberInfo, null, null, i + this.childElementIndex))
					{
						if (flag3)
						{
							XmlObjectSerializerWriteContextComplexJson.WriteJsonNameWithMapping(this.writer, this.memberNames, i + this.childElementIndex);
						}
						else
						{
							this.WriteStartElement(null, i + this.childElementIndex);
						}
						if (memberValue == null)
						{
							memberValue = this.LoadMemberValue(dataMember);
						}
						this.WriteValue(memberType, memberValue);
						this.WriteEndElement();
					}
					if (classContract.HasExtensionData)
					{
						this.context.WriteExtensionData(this.writer, extensionData, num);
					}
				}
				else if (!dataMember.EmitDefaultValue && dataMember.IsRequired)
				{
					XmlObjectSerializerWriteContext.ThrowRequiredMemberMustBeEmitted(dataMember.Name, classContract.UnderlyingType);
				}
				i++;
				num++;
			}
			this.typeIndex++;
			this.childElementIndex += classContract.Members.Count;
			return num;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00052BEC File Offset: 0x00050DEC
		internal bool IsDefaultValue(Type type, object value)
		{
			object defaultValue = this.GetDefaultValue(type);
			if (defaultValue != null)
			{
				return defaultValue.Equals(value);
			}
			return value == null;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00052C10 File Offset: 0x00050E10
		internal object GetDefaultValue(Type type)
		{
			if (type.IsValueType)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					return false;
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
					return 0;
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return 0L;
				case TypeCode.Single:
					return 0f;
				case TypeCode.Double:
					return 0.0;
				case TypeCode.Decimal:
					return 0m;
				case TypeCode.DateTime:
					return default(DateTime);
				}
			}
			return null;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00052CBC File Offset: 0x00050EBC
		private void WriteStartElement(object nameLocal, int nameIndex)
		{
			object obj = nameLocal ?? this.memberNames[nameIndex];
			if (nameLocal != null && nameLocal is string)
			{
				this.writer.WriteStartElement((string)obj, null);
				return;
			}
			if (obj is XmlDictionaryString)
			{
				this.writer.WriteStartElement((XmlDictionaryString)obj, null);
				return;
			}
			this.writer.WriteStartElement(obj.ToString(), null);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00052D22 File Offset: 0x00050F22
		private void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00052D2F File Offset: 0x00050F2F
		private void WriteArrayAttribute()
		{
			this.writer.WriteAttributeString(null, "type", string.Empty, "array");
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00052D4C File Offset: 0x00050F4C
		private void WriteObjectAttribute()
		{
			this.writer.WriteAttributeString(null, "type", null, "object");
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00052D68 File Offset: 0x00050F68
		private void WriteValue(Type memberType, object memberValue)
		{
			if (memberType.IsPointer)
			{
				Pointer pointer = (Pointer)JsonFormatGeneratorStatics.BoxPointer.Invoke(null, new object[]
				{
					memberValue,
					memberType
				});
			}
			bool flag = memberType.IsGenericType && memberType.GetGenericTypeDefinition() == Globals.TypeOfNullable;
			if (memberType.IsValueType && !flag)
			{
				PrimitiveDataContract primitiveDataContract = PrimitiveDataContract.GetPrimitiveDataContract(memberType);
				if (primitiveDataContract != null)
				{
					primitiveDataContract.XmlFormatContentWriterMethod.Invoke(this.writer, new object[]
					{
						memberValue
					});
					return;
				}
				this.InternalSerialize(XmlFormatGeneratorStatics.InternalSerializeMethod, () => memberValue, memberType, false);
				return;
			}
			else
			{
				bool flag2;
				if (flag)
				{
					memberValue = this.UnwrapNullableObject(() => memberValue, ref memberType, out flag2);
				}
				else
				{
					flag2 = (memberValue == null);
				}
				if (flag2)
				{
					XmlFormatGeneratorStatics.WriteNullMethod.Invoke(this.context, new object[]
					{
						this.writer,
						memberType,
						DataContract.IsTypeSerializable(memberType)
					});
					return;
				}
				PrimitiveDataContract primitiveDataContract2 = PrimitiveDataContract.GetPrimitiveDataContract(memberType);
				if (primitiveDataContract2 != null && primitiveDataContract2.UnderlyingType != Globals.TypeOfObject)
				{
					if (flag)
					{
						primitiveDataContract2.XmlFormatContentWriterMethod.Invoke(this.writer, new object[]
						{
							memberValue
						});
						return;
					}
					primitiveDataContract2.XmlFormatContentWriterMethod.Invoke(this.context, new object[]
					{
						this.writer,
						memberValue
					});
					return;
				}
				else
				{
					bool flag3 = false;
					if (memberType == Globals.TypeOfObject || memberType == Globals.TypeOfValueType || ((IList)Globals.TypeOfNullable.GetInterfaces()).Contains(memberType))
					{
						object memberValue2 = CodeInterpreter.ConvertValue(memberValue, memberType.GetType(), Globals.TypeOfObject);
						memberValue = memberValue2;
						flag3 = (memberValue == null);
					}
					if (flag3)
					{
						XmlFormatGeneratorStatics.WriteNullMethod.Invoke(this.context, new object[]
						{
							this.writer,
							memberType,
							DataContract.IsTypeSerializable(memberType)
						});
						return;
					}
					this.InternalSerialize(flag ? XmlFormatGeneratorStatics.InternalSerializeMethod : XmlFormatGeneratorStatics.InternalSerializeReferenceMethod, () => memberValue, memberType, false);
					return;
				}
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00052FA4 File Offset: 0x000511A4
		private void InternalSerialize(MethodInfo methodInfo, Func<object> memberValue, Type memberType, bool writeXsiType)
		{
			object obj = memberValue();
			bool flag = Type.GetTypeHandle(obj).Equals(CodeInterpreter.ConvertValue(obj, memberType, Globals.TypeOfObject));
			try
			{
				methodInfo.Invoke(this.context, new object[]
				{
					this.writer,
					(memberValue != null) ? obj : null,
					flag,
					writeXsiType,
					DataContract.GetId(memberType.TypeHandle),
					memberType.TypeHandle
				});
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00053054 File Offset: 0x00051254
		private object UnwrapNullableObject(Func<object> memberValue, ref Type memberType, out bool isNull)
		{
			object obj = memberValue();
			isNull = false;
			while (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == Globals.TypeOfNullable)
			{
				Type type = memberType.GetGenericArguments()[0];
				if ((bool)XmlFormatGeneratorStatics.GetHasValueMethod.MakeGenericMethod(new Type[]
				{
					type
				}).Invoke(null, new object[]
				{
					obj
				}))
				{
					obj = XmlFormatGeneratorStatics.GetNullableValueMethod.MakeGenericMethod(new Type[]
					{
						type
					}).Invoke(null, new object[]
					{
						obj
					});
				}
				else
				{
					isNull = true;
					obj = XmlFormatGeneratorStatics.GetDefaultValueMethod.MakeGenericMethod(new Type[]
					{
						memberType
					}).Invoke(null, new object[0]);
				}
				memberType = type;
			}
			return obj;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00053114 File Offset: 0x00051314
		private bool TryWritePrimitive(Type type, Func<object> value, MemberInfo memberInfo, int? arrayItemIndex, XmlDictionaryString name, int nameIndex)
		{
			PrimitiveDataContract primitiveDataContract = PrimitiveDataContract.GetPrimitiveDataContract(type);
			if (primitiveDataContract == null || primitiveDataContract.UnderlyingType == Globals.TypeOfObject)
			{
				return false;
			}
			List<object> list = new List<object>();
			object obj;
			if (type.IsValueType)
			{
				obj = this.writer;
			}
			else
			{
				obj = this.context;
				list.Add(this.writer);
			}
			if (value != null)
			{
				list.Add(value());
			}
			else if (memberInfo != null)
			{
				list.Add(CodeInterpreter.GetMember(memberInfo, this.objLocal));
			}
			else
			{
				list.Add(((Array)this.objLocal).GetValue(new int[]
				{
					arrayItemIndex.Value
				}));
			}
			if (name != null)
			{
				list.Add(name);
			}
			else
			{
				list.Add(this.memberNames[nameIndex]);
			}
			list.Add(null);
			primitiveDataContract.XmlFormatWriterMethod.Invoke(obj, list.ToArray());
			return true;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000531F8 File Offset: 0x000513F8
		private bool TryWritePrimitiveArray(Type type, Type itemType, Func<object> value, XmlDictionaryString itemName)
		{
			if (PrimitiveDataContract.GetPrimitiveDataContract(itemType) == null)
			{
				return false;
			}
			string text = null;
			TypeCode typeCode = Type.GetTypeCode(itemType);
			if (typeCode != TypeCode.Boolean)
			{
				switch (typeCode)
				{
				case TypeCode.Int32:
					text = "WriteJsonInt32Array";
					break;
				case TypeCode.Int64:
					text = "WriteJsonInt64Array";
					break;
				case TypeCode.Single:
					text = "WriteJsonSingleArray";
					break;
				case TypeCode.Double:
					text = "WriteJsonDoubleArray";
					break;
				case TypeCode.Decimal:
					text = "WriteJsonDecimalArray";
					break;
				case TypeCode.DateTime:
					text = "WriteJsonDateTimeArray";
					break;
				}
			}
			else
			{
				text = "WriteJsonBooleanArray";
			}
			if (text != null)
			{
				this.WriteArrayAttribute();
				MethodBase method = typeof(JsonWriterDelegator).GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
				{
					type,
					typeof(XmlDictionaryString),
					typeof(XmlDictionaryString)
				}, null);
				object obj = this.writer;
				object[] array = new object[3];
				array[0] = value();
				array[1] = itemName;
				method.Invoke(obj, array);
				return true;
			}
			return false;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000532E1 File Offset: 0x000514E1
		private object LoadMemberValue(DataMember member)
		{
			return CodeInterpreter.GetMember(member.MemberInfo, this.objLocal);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000532F4 File Offset: 0x000514F4
		[CompilerGenerated]
		private object <WriteCollection>b__22_0()
		{
			return this.objLocal;
		}

		// Token: 0x04000A53 RID: 2643
		private ClassDataContract classContract;

		// Token: 0x04000A54 RID: 2644
		private CollectionDataContract collectionContract;

		// Token: 0x04000A55 RID: 2645
		private XmlWriterDelegator writer;

		// Token: 0x04000A56 RID: 2646
		private object obj;

		// Token: 0x04000A57 RID: 2647
		private XmlObjectSerializerWriteContextComplexJson context;

		// Token: 0x04000A58 RID: 2648
		private DataContract dataContract;

		// Token: 0x04000A59 RID: 2649
		private object objLocal;

		// Token: 0x04000A5A RID: 2650
		private XmlDictionaryString[] memberNames;

		// Token: 0x04000A5B RID: 2651
		private int typeIndex = 1;

		// Token: 0x04000A5C RID: 2652
		private int childElementIndex;

		// Token: 0x02000199 RID: 409
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060014DD RID: 5341 RVA: 0x0000222F File Offset: 0x0000042F
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060014DE RID: 5342 RVA: 0x000532FC File Offset: 0x000514FC
			internal object <WriteCollection>b__1()
			{
				return this.currentValue;
			}

			// Token: 0x04000A5D RID: 2653
			public object currentValue;

			// Token: 0x04000A5E RID: 2654
			public Func<object> <>9__1;
		}

		// Token: 0x0200019A RID: 410
		[CompilerGenerated]
		private sealed class <>c__DisplayClass23_0
		{
			// Token: 0x060014DF RID: 5343 RVA: 0x0000222F File Offset: 0x0000042F
			public <>c__DisplayClass23_0()
			{
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x00053304 File Offset: 0x00051504
			internal object <WriteMembers>b__0()
			{
				return this.memberValue;
			}

			// Token: 0x04000A5F RID: 2655
			public object memberValue;
		}

		// Token: 0x0200019B RID: 411
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x060014E1 RID: 5345 RVA: 0x0000222F File Offset: 0x0000042F
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x060014E2 RID: 5346 RVA: 0x0005330C File Offset: 0x0005150C
			internal object <WriteValue>b__0()
			{
				return this.memberValue;
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x0005330C File Offset: 0x0005150C
			internal object <WriteValue>b__1()
			{
				return this.memberValue;
			}

			// Token: 0x060014E4 RID: 5348 RVA: 0x0005330C File Offset: 0x0005150C
			internal object <WriteValue>b__2()
			{
				return this.memberValue;
			}

			// Token: 0x04000A60 RID: 2656
			public object memberValue;
		}
	}
}
