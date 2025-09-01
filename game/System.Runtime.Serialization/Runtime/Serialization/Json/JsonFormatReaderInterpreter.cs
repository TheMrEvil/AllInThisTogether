using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000196 RID: 406
	internal class JsonFormatReaderInterpreter
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x0005105F File Offset: 0x0004F25F
		public JsonFormatReaderInterpreter(ClassDataContract classContract)
		{
			this.classContract = classContract;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005106E File Offset: 0x0004F26E
		public JsonFormatReaderInterpreter(CollectionDataContract collectionContract, bool isGetOnly)
		{
			this.collectionContract = collectionContract;
			this.is_get_only_collection = isGetOnly;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00051084 File Offset: 0x0004F284
		public object ReadFromJson(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContextComplexJson context, XmlDictionaryString emptyDictionaryString, XmlDictionaryString[] memberNames)
		{
			this.xmlReader = xmlReader;
			this.context = context;
			this.emptyDictionaryString = emptyDictionaryString;
			this.memberNames = memberNames;
			this.CreateObject(this.classContract);
			context.AddNewObject(this.objectLocal);
			this.InvokeOnDeserializing(this.classContract);
			if (this.classContract.IsISerializable)
			{
				this.ReadISerializable(this.classContract);
			}
			else
			{
				this.ReadClass(this.classContract);
			}
			if (Globals.TypeOfIDeserializationCallback.IsAssignableFrom(this.classContract.UnderlyingType))
			{
				((IDeserializationCallback)this.objectLocal).OnDeserialization(null);
			}
			this.InvokeOnDeserialized(this.classContract);
			if (!this.InvokeFactoryMethod(this.classContract) && this.classContract.UnderlyingType == Globals.TypeOfDateTimeOffsetAdapter)
			{
				this.objectLocal = DateTimeOffsetAdapter.GetDateTimeOffset((DateTimeOffsetAdapter)this.objectLocal);
			}
			return this.objectLocal;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00051173 File Offset: 0x0004F373
		public object ReadCollectionFromJson(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContextComplexJson context, XmlDictionaryString emptyDictionaryString, XmlDictionaryString itemName, CollectionDataContract collectionContract)
		{
			this.xmlReader = xmlReader;
			this.context = context;
			this.emptyDictionaryString = emptyDictionaryString;
			this.itemName = itemName;
			this.collectionContract = collectionContract;
			this.ReadCollection(collectionContract);
			return this.objectLocal;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000511A8 File Offset: 0x0004F3A8
		public void ReadGetOnlyCollectionFromJson(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContextComplexJson context, XmlDictionaryString emptyDictionaryString, XmlDictionaryString itemName, CollectionDataContract collectionContract)
		{
			this.xmlReader = xmlReader;
			this.context = context;
			this.emptyDictionaryString = emptyDictionaryString;
			this.itemName = itemName;
			this.collectionContract = collectionContract;
			this.ReadGetOnlyCollection(collectionContract);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000511D8 File Offset: 0x0004F3D8
		private void CreateObject(ClassDataContract classContract)
		{
			Type type = this.objectType = classContract.UnderlyingType;
			if (type.IsValueType && !classContract.IsNonAttributedType)
			{
				type = Globals.TypeOfValueType;
			}
			if (classContract.UnderlyingType == Globals.TypeOfDBNull)
			{
				this.objectLocal = DBNull.Value;
				return;
			}
			if (!classContract.IsNonAttributedType)
			{
				this.objectLocal = CodeInterpreter.ConvertValue(XmlFormatReaderGenerator.UnsafeGetUninitializedObject(DataContract.GetIdForInitialization(classContract)), Globals.TypeOfObject, type);
				return;
			}
			if (type.IsValueType)
			{
				this.objectLocal = FormatterServices.GetUninitializedObject(type);
				return;
			}
			this.objectLocal = classContract.GetNonAttributedTypeConstructor().Invoke(new object[0]);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0005127C File Offset: 0x0004F47C
		private void InvokeOnDeserializing(ClassDataContract classContract)
		{
			if (classContract.BaseContract != null)
			{
				this.InvokeOnDeserializing(classContract.BaseContract);
			}
			if (classContract.OnDeserializing != null)
			{
				classContract.OnDeserializing.Invoke(this.objectLocal, new object[]
				{
					this.context.GetStreamingContext()
				});
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000512D8 File Offset: 0x0004F4D8
		private void InvokeOnDeserialized(ClassDataContract classContract)
		{
			if (classContract.BaseContract != null)
			{
				this.InvokeOnDeserialized(classContract.BaseContract);
			}
			if (classContract.OnDeserialized != null)
			{
				classContract.OnDeserialized.Invoke(this.objectLocal, new object[]
				{
					this.context.GetStreamingContext()
				});
			}
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00047532 File Offset: 0x00045732
		private bool HasFactoryMethod(ClassDataContract classContract)
		{
			return Globals.TypeOfIObjectReference.IsAssignableFrom(classContract.UnderlyingType);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00051332 File Offset: 0x0004F532
		private bool InvokeFactoryMethod(ClassDataContract classContract)
		{
			if (this.HasFactoryMethod(classContract))
			{
				this.objectLocal = CodeInterpreter.ConvertValue(this.context.GetRealObject((IObjectReference)this.objectLocal, Globals.NewObjectId), Globals.TypeOfObject, classContract.UnderlyingType);
				return true;
			}
			return false;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00051374 File Offset: 0x0004F574
		private void ReadISerializable(ClassDataContract classContract)
		{
			ConstructorInfo constructor = classContract.UnderlyingType.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, JsonFormatGeneratorStatics.SerInfoCtorArgs, null);
			if (constructor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Constructor that takes SerializationInfo and StreamingContext is not found for '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(classContract.UnderlyingType)
				})));
			}
			this.context.ReadSerializationInfo(this.xmlReader, classContract.UnderlyingType);
			constructor.Invoke(this.objectLocal, new object[]
			{
				this.context.GetStreamingContext()
			});
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00051408 File Offset: 0x0004F608
		private void ReadClass(ClassDataContract classContract)
		{
			if (classContract.HasExtensionData)
			{
				ExtensionDataObject extensionDataObject = new ExtensionDataObject();
				this.ReadMembers(classContract, extensionDataObject);
				for (ClassDataContract classDataContract = classContract; classDataContract != null; classDataContract = classDataContract.BaseContract)
				{
					MethodInfo extensionDataSetMethod = classDataContract.ExtensionDataSetMethod;
					if (extensionDataSetMethod != null)
					{
						extensionDataSetMethod.Invoke(this.objectLocal, new object[]
						{
							extensionDataObject
						});
					}
				}
				return;
			}
			this.ReadMembers(classContract, null);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005146C File Offset: 0x0004F66C
		private void ReadMembers(ClassDataContract classContract, ExtensionDataObject extensionData)
		{
			int num = classContract.MemberNames.Length;
			this.context.IncrementItemCount(num);
			int memberIndex = -1;
			BitFlagsGenerator bitFlagsGenerator = new BitFlagsGenerator(num);
			byte[] requiredElements = new byte[bitFlagsGenerator.GetLocalCount()];
			this.SetRequiredElements(classContract, requiredElements);
			this.SetExpectedElements(bitFlagsGenerator, 0);
			while (XmlObjectSerializerReadContext.MoveToNextElement(this.xmlReader))
			{
				int jsonMemberIndex = this.context.GetJsonMemberIndex(this.xmlReader, this.memberNames, memberIndex, extensionData);
				if (num > 0)
				{
					this.ReadMembers(jsonMemberIndex, classContract, bitFlagsGenerator, ref memberIndex);
				}
			}
			if (!this.CheckRequiredElements(bitFlagsGenerator, requiredElements))
			{
				XmlObjectSerializerReadContextComplexJson.ThrowMissingRequiredMembers(this.objectLocal, this.memberNames, bitFlagsGenerator.LoadArray(), requiredElements);
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00051514 File Offset: 0x0004F714
		private int ReadMembers(int index, ClassDataContract classContract, BitFlagsGenerator expectedElements, ref int memberIndex)
		{
			int num = (classContract.BaseContract == null) ? 0 : this.ReadMembers(index, classContract.BaseContract, expectedElements, ref memberIndex);
			if (num <= index && index < num + classContract.Members.Count)
			{
				DataMember dataMember = classContract.Members[index - num];
				Type memberType = dataMember.MemberType;
				memberIndex = num;
				if (!expectedElements.Load(index))
				{
					XmlObjectSerializerReadContextComplexJson.ThrowDuplicateMemberException(this.objectLocal, this.memberNames, memberIndex);
				}
				if (dataMember.IsGetOnlyCollection)
				{
					object member = CodeInterpreter.GetMember(dataMember.MemberInfo, this.objectLocal);
					this.context.StoreCollectionMemberInfo(member);
					this.ReadValue(memberType, dataMember.Name);
				}
				else
				{
					object value = this.ReadValue(memberType, dataMember.Name);
					CodeInterpreter.SetMember(dataMember.MemberInfo, this.objectLocal, value);
				}
				memberIndex = index;
				this.ResetExpectedElements(expectedElements, index);
			}
			return num + classContract.Members.Count;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00051600 File Offset: 0x0004F800
		private bool CheckRequiredElements(BitFlagsGenerator expectedElements, byte[] requiredElements)
		{
			for (int i = 0; i < requiredElements.Length; i++)
			{
				if ((expectedElements.GetLocal(i) & requiredElements[i]) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005162C File Offset: 0x0004F82C
		private int SetRequiredElements(ClassDataContract contract, byte[] requiredElements)
		{
			int num = (contract.BaseContract == null) ? 0 : this.SetRequiredElements(contract.BaseContract, requiredElements);
			List<DataMember> members = contract.Members;
			int i = 0;
			while (i < members.Count)
			{
				if (members[i].IsRequired)
				{
					BitFlagsGenerator.SetBit(requiredElements, num);
				}
				i++;
				num++;
			}
			return num;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00051684 File Offset: 0x0004F884
		private void SetExpectedElements(BitFlagsGenerator expectedElements, int startIndex)
		{
			int bitCount = expectedElements.GetBitCount();
			for (int i = startIndex; i < bitCount; i++)
			{
				expectedElements.Store(i, true);
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000516AC File Offset: 0x0004F8AC
		private void ResetExpectedElements(BitFlagsGenerator expectedElements, int index)
		{
			expectedElements.Store(index, false);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000516B8 File Offset: 0x0004F8B8
		private object ReadValue(Type type, string name)
		{
			Type type2 = type;
			bool flag = false;
			int num = 0;
			while (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable)
			{
				num++;
				type = type.GetGenericArguments()[0];
			}
			PrimitiveDataContract primitiveDataContract = PrimitiveDataContract.GetPrimitiveDataContract(type);
			object obj;
			if ((primitiveDataContract != null && primitiveDataContract.UnderlyingType != Globals.TypeOfObject) || num != 0 || type.IsValueType)
			{
				this.context.ReadAttributes(this.xmlReader);
				string text = this.context.ReadIfNullOrRef(this.xmlReader, type, DataContract.IsTypeSerializable(type));
				if (text == null)
				{
					if (num != 0)
					{
						obj = Activator.CreateInstance(type2);
					}
					else
					{
						if (type.IsValueType)
						{
							throw new SerializationException(SR.GetString("ValueType '{0}' cannot be null.", new object[]
							{
								DataContract.GetClrTypeFullName(type)
							}));
						}
						obj = null;
					}
				}
				else if (text == string.Empty)
				{
					text = this.context.GetObjectId();
					if (type.IsValueType && !string.IsNullOrEmpty(text))
					{
						throw new SerializationException(SR.GetString("ValueType '{0}' cannot have id.", new object[]
						{
							DataContract.GetClrTypeFullName(type)
						}));
					}
					if (num != 0)
					{
						flag = true;
					}
					if (primitiveDataContract != null && primitiveDataContract.UnderlyingType != Globals.TypeOfObject)
					{
						obj = primitiveDataContract.XmlFormatReaderMethod.Invoke(this.xmlReader, new object[0]);
						if (!type.IsValueType)
						{
							this.context.AddNewObject(obj);
						}
					}
					else
					{
						obj = this.InternalDeserialize(type, name);
					}
				}
				else
				{
					if (type.IsValueType)
					{
						throw new SerializationException(SR.GetString("ValueType '{0}' cannot have ref to another object.", new object[]
						{
							DataContract.GetClrTypeFullName(type)
						}));
					}
					obj = CodeInterpreter.ConvertValue(this.context.GetExistingObject(text, type, name, string.Empty), Globals.TypeOfObject, type);
				}
				if (flag && text != null)
				{
					obj = this.WrapNullableObject(type, obj, type2, num);
				}
			}
			else
			{
				obj = this.InternalDeserialize(type, name);
			}
			return obj;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005189C File Offset: 0x0004FA9C
		private object InternalDeserialize(Type type, string name)
		{
			Type type2 = type.IsPointer ? Globals.TypeOfReflectionPointer : type;
			object obj = this.context.InternalDeserialize(this.xmlReader, DataContract.GetId(type2.TypeHandle), type2.TypeHandle, name, string.Empty);
			if (type.IsPointer)
			{
				return JsonFormatGeneratorStatics.UnboxPointer.Invoke(null, new object[]
				{
					obj
				});
			}
			return CodeInterpreter.ConvertValue(obj, Globals.TypeOfObject, type);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00051910 File Offset: 0x0004FB10
		private object WrapNullableObject(Type innerType, object innerValue, Type outerType, int nullables)
		{
			object obj = innerValue;
			for (int i = 1; i < nullables; i++)
			{
				Type type = Globals.TypeOfNullable.MakeGenericType(new Type[]
				{
					innerType
				});
				obj = Activator.CreateInstance(type, new object[]
				{
					obj
				});
				innerType = type;
			}
			return Activator.CreateInstance(outerType, new object[]
			{
				obj
			});
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00051968 File Offset: 0x0004FB68
		private void ReadCollection(CollectionDataContract collectionContract)
		{
			Type type = collectionContract.UnderlyingType;
			Type itemType = collectionContract.ItemType;
			bool flag = collectionContract.Kind == CollectionKind.Array;
			ConstructorInfo constructor = collectionContract.Constructor;
			if (type.IsInterface)
			{
				switch (collectionContract.Kind)
				{
				case CollectionKind.GenericDictionary:
					type = Globals.TypeOfDictionaryGeneric.MakeGenericType(itemType.GetGenericArguments());
					constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
					break;
				case CollectionKind.Dictionary:
					type = Globals.TypeOfHashtable;
					constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
					break;
				case CollectionKind.GenericList:
				case CollectionKind.GenericCollection:
				case CollectionKind.List:
				case CollectionKind.GenericEnumerable:
				case CollectionKind.Collection:
				case CollectionKind.Enumerable:
					type = itemType.MakeArrayType();
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (type.IsValueType)
				{
					this.objectLocal = FormatterServices.GetUninitializedObject(type);
				}
				else
				{
					this.objectLocal = constructor.Invoke(new object[0]);
					this.context.AddNewObject(this.objectLocal);
				}
			}
			if ((collectionContract.Kind == CollectionKind.Dictionary || collectionContract.Kind == CollectionKind.GenericDictionary) & this.context.UseSimpleDictionaryFormat)
			{
				this.ReadSimpleDictionary(collectionContract, itemType);
				return;
			}
			string objectId = this.context.GetObjectId();
			bool flag2 = false;
			bool flag3 = false;
			if (flag && this.TryReadPrimitiveArray(itemType, out flag3))
			{
				flag2 = true;
			}
			if (!flag2)
			{
				object obj = null;
				if (flag)
				{
					obj = Array.CreateInstance(itemType, 32);
				}
				int i;
				for (i = 0; i < 2147483647; i++)
				{
					if (this.IsStartElement(this.itemName, this.emptyDictionaryString))
					{
						this.context.IncrementItemCount(1);
						object value = this.ReadCollectionItem(collectionContract, itemType);
						if (flag)
						{
							obj = XmlFormatGeneratorStatics.EnsureArraySizeMethod.MakeGenericMethod(new Type[]
							{
								itemType
							}).Invoke(null, new object[]
							{
								obj,
								i
							});
							((Array)obj).SetValue(value, i);
						}
						else
						{
							this.StoreCollectionValue(this.objectLocal, itemType, value, collectionContract);
						}
					}
					else
					{
						if (this.IsEndElement())
						{
							break;
						}
						this.HandleUnexpectedItemInCollection(ref i);
					}
				}
				if (flag)
				{
					MethodInfo methodInfo = XmlFormatGeneratorStatics.TrimArraySizeMethod.MakeGenericMethod(new Type[]
					{
						itemType
					});
					this.objectLocal = methodInfo.Invoke(null, new object[]
					{
						obj,
						i
					});
					this.context.AddNewObjectWithId(objectId, this.objectLocal);
					return;
				}
			}
			else
			{
				this.context.AddNewObjectWithId(objectId, this.objectLocal);
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00051BC8 File Offset: 0x0004FDC8
		private void ReadSimpleDictionary(CollectionDataContract collectionContract, Type keyValueType)
		{
			Type[] genericArguments = keyValueType.GetGenericArguments();
			Type type = genericArguments[0];
			Type type2 = genericArguments[1];
			int num = 0;
			while (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable)
			{
				num++;
				type = type.GetGenericArguments()[0];
			}
			DataContract memberTypeContract = ((ClassDataContract)collectionContract.ItemContract).Members[0].MemberTypeContract;
			JsonFormatReaderInterpreter.KeyParseMode keyParseMode = JsonFormatReaderInterpreter.KeyParseMode.Fail;
			if (type == Globals.TypeOfString || type == Globals.TypeOfObject)
			{
				keyParseMode = JsonFormatReaderInterpreter.KeyParseMode.AsString;
			}
			else if (type.IsEnum)
			{
				keyParseMode = JsonFormatReaderInterpreter.KeyParseMode.UsingParseEnum;
			}
			else if (memberTypeContract.ParseMethod != null)
			{
				keyParseMode = JsonFormatReaderInterpreter.KeyParseMode.UsingCustomParse;
			}
			if (keyParseMode == JsonFormatReaderInterpreter.KeyParseMode.Fail)
			{
				this.ThrowSerializationException(SR.GetString("Key type '{1}' for collection type '{0}' cannot be parsed in simple dictionary.", new object[]
				{
					DataContract.GetClrTypeFullName(collectionContract.UnderlyingType),
					DataContract.GetClrTypeFullName(type)
				}), Array.Empty<object>());
				return;
			}
			XmlNodeType xmlNodeType;
			while ((xmlNodeType = this.xmlReader.MoveToContent()) != XmlNodeType.EndElement)
			{
				if (xmlNodeType != XmlNodeType.Element)
				{
					this.ThrowUnexpectedStateException(XmlNodeType.Element);
				}
				this.context.IncrementItemCount(1);
				string jsonMemberName = XmlObjectSerializerReadContextComplexJson.GetJsonMemberName(this.xmlReader);
				object obj = null;
				if (keyParseMode == JsonFormatReaderInterpreter.KeyParseMode.AsString)
				{
					obj = jsonMemberName;
				}
				else if (keyParseMode == JsonFormatReaderInterpreter.KeyParseMode.UsingParseEnum)
				{
					obj = Enum.Parse(type, jsonMemberName);
				}
				else if (keyParseMode == JsonFormatReaderInterpreter.KeyParseMode.UsingCustomParse)
				{
					obj = memberTypeContract.ParseMethod.Invoke(null, new object[]
					{
						jsonMemberName
					});
				}
				if (num > 0)
				{
					obj = this.WrapNullableObject(type, obj, type2, num);
				}
				object obj2 = this.ReadValue(type2, string.Empty);
				collectionContract.AddMethod.Invoke(this.objectLocal, new object[]
				{
					obj,
					obj2
				});
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00051D5C File Offset: 0x0004FF5C
		private void ReadGetOnlyCollection(CollectionDataContract collectionContract)
		{
			Type underlyingType = collectionContract.UnderlyingType;
			Type itemType = collectionContract.ItemType;
			bool flag = collectionContract.Kind == CollectionKind.Array;
			int num = 0;
			this.objectLocal = this.context.GetCollectionMember();
			if ((collectionContract.Kind != CollectionKind.Dictionary && collectionContract.Kind != CollectionKind.GenericDictionary) || !this.context.UseSimpleDictionaryFormat)
			{
				if (this.IsStartElement(this.itemName, this.emptyDictionaryString))
				{
					if (this.objectLocal == null)
					{
						XmlObjectSerializerReadContext.ThrowNullValueReturnedForGetOnlyCollectionException(underlyingType);
						return;
					}
					num = 0;
					if (flag)
					{
						num = ((Array)this.objectLocal).Length;
					}
					int i = 0;
					while (i < 2147483647)
					{
						if (this.IsStartElement(this.itemName, this.emptyDictionaryString))
						{
							this.context.IncrementItemCount(1);
							object value = this.ReadCollectionItem(collectionContract, itemType);
							if (flag)
							{
								if (num == i)
								{
									XmlObjectSerializerReadContext.ThrowArrayExceededSizeException(num, underlyingType);
								}
								else
								{
									((Array)this.objectLocal).SetValue(value, i);
								}
							}
							else
							{
								this.StoreCollectionValue(this.objectLocal, itemType, value, collectionContract);
							}
						}
						else
						{
							if (this.IsEndElement())
							{
								break;
							}
							this.HandleUnexpectedItemInCollection(ref i);
						}
					}
					this.context.CheckEndOfArray(this.xmlReader, num, this.itemName, this.emptyDictionaryString);
				}
				return;
			}
			if (this.objectLocal == null)
			{
				XmlObjectSerializerReadContext.ThrowNullValueReturnedForGetOnlyCollectionException(underlyingType);
				return;
			}
			this.ReadSimpleDictionary(collectionContract, itemType);
			this.context.CheckEndOfArray(this.xmlReader, num, this.itemName, this.emptyDictionaryString);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00051ED0 File Offset: 0x000500D0
		private bool TryReadPrimitiveArray(Type itemType, out bool readResult)
		{
			readResult = false;
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
					text = "TryReadInt32Array";
					break;
				case TypeCode.Int64:
					text = "TryReadInt64Array";
					break;
				case TypeCode.Single:
					text = "TryReadSingleArray";
					break;
				case TypeCode.Double:
					text = "TryReadDoubleArray";
					break;
				case TypeCode.Decimal:
					text = "TryReadDecimalArray";
					break;
				case TypeCode.DateTime:
					text = "TryReadJsonDateTimeArray";
					break;
				}
			}
			else
			{
				text = "TryReadBooleanArray";
			}
			if (text != null)
			{
				MethodInfo method = typeof(JsonReaderDelegator).GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				object[] array = new object[]
				{
					this.context,
					this.itemName,
					this.emptyDictionaryString,
					-1,
					this.objectLocal
				};
				readResult = (bool)method.Invoke((JsonReaderDelegator)this.xmlReader, array);
				this.objectLocal = array.Last<object>();
				return true;
			}
			return false;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00051FCC File Offset: 0x000501CC
		private object ReadCollectionItem(CollectionDataContract collectionContract, Type itemType)
		{
			if (collectionContract.Kind == CollectionKind.Dictionary || collectionContract.Kind == CollectionKind.GenericDictionary)
			{
				this.context.ResetAttributes();
				return CodeInterpreter.ConvertValue(DataContractJsonSerializer.ReadJsonValue(XmlObjectSerializerWriteContextComplexJson.GetRevisedItemContract(collectionContract.ItemContract), this.xmlReader, this.context), Globals.TypeOfObject, itemType);
			}
			return this.ReadValue(itemType, "item");
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005202C File Offset: 0x0005022C
		private void StoreCollectionValue(object collection, Type valueType, object value, CollectionDataContract collectionContract)
		{
			if (collectionContract.Kind == CollectionKind.GenericDictionary || collectionContract.Kind == CollectionKind.Dictionary)
			{
				ClassDataContract classDataContract = DataContract.GetDataContract(valueType) as ClassDataContract;
				DataMember dataMember = classDataContract.Members[0];
				DataMember dataMember2 = classDataContract.Members[1];
				object member = CodeInterpreter.GetMember(dataMember.MemberInfo, value);
				object member2 = CodeInterpreter.GetMember(dataMember2.MemberInfo, value);
				try
				{
					collectionContract.AddMethod.Invoke(collection, new object[]
					{
						member,
						member2
					});
					return;
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
			collectionContract.AddMethod.Invoke(collection, new object[]
			{
				value
			});
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000520E4 File Offset: 0x000502E4
		private void HandleUnexpectedItemInCollection(ref int iterator)
		{
			if (this.IsStartElement())
			{
				this.context.SkipUnknownElement(this.xmlReader);
				iterator--;
				return;
			}
			throw XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, this.xmlReader);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00052112 File Offset: 0x00050312
		private bool IsStartElement(XmlDictionaryString name, XmlDictionaryString ns)
		{
			return this.xmlReader.IsStartElement(name, ns);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00052121 File Offset: 0x00050321
		private bool IsStartElement()
		{
			return this.xmlReader.IsStartElement();
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0005212E File Offset: 0x0005032E
		private bool IsEndElement()
		{
			return this.xmlReader.NodeType == XmlNodeType.EndElement;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005213F File Offset: 0x0005033F
		private void ThrowUnexpectedStateException(XmlNodeType expectedState)
		{
			throw XmlObjectSerializerReadContext.CreateUnexpectedStateException(expectedState, this.xmlReader);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005214D File Offset: 0x0005034D
		private void ThrowSerializationException(string msg, params object[] values)
		{
			if (values != null && values.Length != 0)
			{
				msg = string.Format(msg, values);
			}
			throw new SerializationException(msg);
		}

		// Token: 0x04000A43 RID: 2627
		private bool is_get_only_collection;

		// Token: 0x04000A44 RID: 2628
		private ClassDataContract classContract;

		// Token: 0x04000A45 RID: 2629
		private CollectionDataContract collectionContract;

		// Token: 0x04000A46 RID: 2630
		private object objectLocal;

		// Token: 0x04000A47 RID: 2631
		private Type objectType;

		// Token: 0x04000A48 RID: 2632
		private XmlReaderDelegator xmlReader;

		// Token: 0x04000A49 RID: 2633
		private XmlObjectSerializerReadContextComplexJson context;

		// Token: 0x04000A4A RID: 2634
		private XmlDictionaryString[] memberNames;

		// Token: 0x04000A4B RID: 2635
		private XmlDictionaryString emptyDictionaryString;

		// Token: 0x04000A4C RID: 2636
		private XmlDictionaryString itemName;

		// Token: 0x04000A4D RID: 2637
		private XmlDictionaryString itemNamespace;

		// Token: 0x02000197 RID: 407
		private enum KeyParseMode
		{
			// Token: 0x04000A4F RID: 2639
			Fail,
			// Token: 0x04000A50 RID: 2640
			AsString,
			// Token: 0x04000A51 RID: 2641
			UsingParseEnum,
			// Token: 0x04000A52 RID: 2642
			UsingCustomParse
		}
	}
}
