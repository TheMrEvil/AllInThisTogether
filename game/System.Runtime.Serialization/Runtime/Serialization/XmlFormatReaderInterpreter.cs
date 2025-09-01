using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200015F RID: 351
	internal class XmlFormatReaderInterpreter
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x00047241 File Offset: 0x00045441
		public XmlFormatReaderInterpreter(ClassDataContract classContract)
		{
			this.classContract = classContract;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00047250 File Offset: 0x00045450
		public XmlFormatReaderInterpreter(CollectionDataContract collectionContract, bool isGetOnly)
		{
			this.collectionContract = collectionContract;
			this.is_get_only_collection = isGetOnly;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00047268 File Offset: 0x00045468
		public object ReadFromXml(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context, XmlDictionaryString[] memberNames, XmlDictionaryString[] memberNamespaces)
		{
			this.xmlReader = xmlReader;
			this.context = context;
			this.memberNames = memberNames;
			this.memberNamespaces = memberNamespaces;
			this.CreateObject(this.classContract);
			context.AddNewObject(this.objectLocal);
			this.InvokeOnDeserializing(this.classContract);
			string text = null;
			if (this.HasFactoryMethod(this.classContract))
			{
				text = context.GetObjectId();
			}
			if (this.classContract.IsISerializable)
			{
				this.ReadISerializable(this.classContract);
			}
			else
			{
				this.ReadClass(this.classContract);
			}
			bool flag = this.InvokeFactoryMethod(this.classContract, text);
			if (Globals.TypeOfIDeserializationCallback.IsAssignableFrom(this.classContract.UnderlyingType))
			{
				((IDeserializationCallback)this.objectLocal).OnDeserialization(null);
			}
			this.InvokeOnDeserialized(this.classContract);
			if ((text == null || !flag) && this.classContract.UnderlyingType == Globals.TypeOfDateTimeOffsetAdapter)
			{
				this.objectLocal = DateTimeOffsetAdapter.GetDateTimeOffset((DateTimeOffsetAdapter)this.objectLocal);
			}
			return this.objectLocal;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00047374 File Offset: 0x00045574
		public object ReadCollectionFromXml(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, CollectionDataContract collectionContract)
		{
			this.xmlReader = xmlReader;
			this.context = context;
			this.itemName = itemName;
			this.itemNamespace = itemNamespace;
			this.collectionContract = collectionContract;
			this.ReadCollection(collectionContract);
			return this.objectLocal;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000473A9 File Offset: 0x000455A9
		public void ReadGetOnlyCollectionFromXml(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, CollectionDataContract collectionContract)
		{
			this.xmlReader = xmlReader;
			this.context = context;
			this.itemName = itemName;
			this.itemNamespace = itemNamespace;
			this.collectionContract = collectionContract;
			this.ReadGetOnlyCollection(collectionContract);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000473D8 File Offset: 0x000455D8
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

		// Token: 0x06001273 RID: 4723 RVA: 0x0004747C File Offset: 0x0004567C
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

		// Token: 0x06001274 RID: 4724 RVA: 0x000474D8 File Offset: 0x000456D8
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

		// Token: 0x06001275 RID: 4725 RVA: 0x00047532 File Offset: 0x00045732
		private bool HasFactoryMethod(ClassDataContract classContract)
		{
			return Globals.TypeOfIObjectReference.IsAssignableFrom(classContract.UnderlyingType);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00047544 File Offset: 0x00045744
		private bool InvokeFactoryMethod(ClassDataContract classContract, string objectId)
		{
			if (this.HasFactoryMethod(classContract))
			{
				this.objectLocal = CodeInterpreter.ConvertValue(this.context.GetRealObject((IObjectReference)this.objectLocal, objectId), Globals.TypeOfObject, classContract.UnderlyingType);
				return true;
			}
			return false;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00047580 File Offset: 0x00045780
		private void ReadISerializable(ClassDataContract classContract)
		{
			MethodBase iserializableConstructor = classContract.GetISerializableConstructor();
			SerializationInfo serializationInfo = this.context.ReadSerializationInfo(this.xmlReader, classContract.UnderlyingType);
			iserializableConstructor.Invoke(this.objectLocal, new object[]
			{
				serializationInfo,
				this.context.GetStreamingContext()
			});
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000475D4 File Offset: 0x000457D4
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

		// Token: 0x06001279 RID: 4729 RVA: 0x00047638 File Offset: 0x00045838
		private void ReadMembers(ClassDataContract classContract, ExtensionDataObject extensionData)
		{
			int num = classContract.MemberNames.Length;
			this.context.IncrementItemCount(num);
			int memberIndex = -1;
			int num2;
			bool[] requiredMembers = this.GetRequiredMembers(classContract, out num2);
			bool flag = num2 < num;
			int num3 = flag ? num2 : num;
			while (XmlObjectSerializerReadContext.MoveToNextElement(this.xmlReader))
			{
				int index;
				if (flag)
				{
					index = this.context.GetMemberIndexWithRequiredMembers(this.xmlReader, this.memberNames, this.memberNamespaces, memberIndex, num3, extensionData);
				}
				else
				{
					index = this.context.GetMemberIndex(this.xmlReader, this.memberNames, this.memberNamespaces, memberIndex, extensionData);
				}
				if (num > 0)
				{
					this.ReadMembers(index, classContract, requiredMembers, ref memberIndex, ref num3);
				}
			}
			if (flag && num3 < num)
			{
				XmlObjectSerializerReadContext.ThrowRequiredMemberMissingException(this.xmlReader, memberIndex, num3, this.memberNames);
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00047700 File Offset: 0x00045900
		private int ReadMembers(int index, ClassDataContract classContract, bool[] requiredMembers, ref int memberIndex, ref int requiredIndex)
		{
			int num = (classContract.BaseContract == null) ? 0 : this.ReadMembers(index, classContract.BaseContract, requiredMembers, ref memberIndex, ref requiredIndex);
			if (num <= index && index < num + classContract.Members.Count)
			{
				DataMember dataMember = classContract.Members[index - num];
				Type memberType = dataMember.MemberType;
				if (dataMember.IsRequired)
				{
					int num2 = index + 1;
					while (num2 < requiredMembers.Length && !requiredMembers[num2])
					{
						num2++;
					}
					requiredIndex = num2;
				}
				if (dataMember.IsGetOnlyCollection)
				{
					object member = CodeInterpreter.GetMember(dataMember.MemberInfo, this.objectLocal);
					this.context.StoreCollectionMemberInfo(member);
					this.ReadValue(memberType, dataMember.Name, classContract.StableName.Namespace);
				}
				else
				{
					object value = this.ReadValue(memberType, dataMember.Name, classContract.StableName.Namespace);
					CodeInterpreter.SetMember(dataMember.MemberInfo, this.objectLocal, value);
				}
				memberIndex = index;
			}
			return num + classContract.Members.Count;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00047800 File Offset: 0x00045A00
		private bool[] GetRequiredMembers(ClassDataContract contract, out int firstRequiredMember)
		{
			int num = contract.MemberNames.Length;
			bool[] array = new bool[num];
			this.GetRequiredMembers(contract, array);
			firstRequiredMember = 0;
			while (firstRequiredMember < num && !array[firstRequiredMember])
			{
				firstRequiredMember++;
			}
			return array;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00047840 File Offset: 0x00045A40
		private int GetRequiredMembers(ClassDataContract contract, bool[] requiredMembers)
		{
			int num = (contract.BaseContract == null) ? 0 : this.GetRequiredMembers(contract.BaseContract, requiredMembers);
			List<DataMember> members = contract.Members;
			int i = 0;
			while (i < members.Count)
			{
				requiredMembers[num] = members[i].IsRequired;
				i++;
				num++;
			}
			return num;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00047894 File Offset: 0x00045A94
		private object ReadValue(Type type, string name, string ns)
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
						obj = this.InternalDeserialize(type, name, ns);
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
					obj = CodeInterpreter.ConvertValue(this.context.GetExistingObject(text, type, name, ns), Globals.TypeOfObject, type);
				}
				if (flag && text != null)
				{
					obj = this.WrapNullableObject(type, obj, type2, num);
				}
			}
			else
			{
				obj = this.InternalDeserialize(type, name, ns);
			}
			return obj;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00047A74 File Offset: 0x00045C74
		private object InternalDeserialize(Type type, string name, string ns)
		{
			Type type2 = type.IsPointer ? Globals.TypeOfReflectionPointer : type;
			object obj = this.context.InternalDeserialize(this.xmlReader, DataContract.GetId(type2.TypeHandle), type2.TypeHandle, name, ns);
			if (type.IsPointer)
			{
				return XmlFormatGeneratorStatics.UnboxPointer.Invoke(null, new object[]
				{
					obj
				});
			}
			return CodeInterpreter.ConvertValue(obj, Globals.TypeOfObject, type);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00047AE4 File Offset: 0x00045CE4
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

		// Token: 0x06001280 RID: 4736 RVA: 0x00047B3C File Offset: 0x00045D3C
		private void ReadCollection(CollectionDataContract collectionContract)
		{
			Type type = collectionContract.UnderlyingType;
			Type itemType = collectionContract.ItemType;
			bool flag = collectionContract.Kind == CollectionKind.Array;
			ConstructorInfo constructorInfo = collectionContract.Constructor;
			if (type.IsInterface)
			{
				switch (collectionContract.Kind)
				{
				case CollectionKind.GenericDictionary:
					type = Globals.TypeOfDictionaryGeneric.MakeGenericType(itemType.GetGenericArguments());
					constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
					break;
				case CollectionKind.Dictionary:
					type = Globals.TypeOfHashtable;
					constructorInfo = XmlFormatGeneratorStatics.HashtableCtor;
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
			string text = collectionContract.ItemName;
			string @namespace = collectionContract.StableName.Namespace;
			if (!flag)
			{
				if (type.IsValueType)
				{
					this.objectLocal = FormatterServices.GetUninitializedObject(type);
				}
				else
				{
					this.objectLocal = constructorInfo.Invoke(new object[0]);
					this.context.AddNewObject(this.objectLocal);
				}
			}
			int arraySize = this.context.GetArraySize();
			string objectId = this.context.GetObjectId();
			bool flag2 = false;
			bool flag3 = false;
			if (flag && this.TryReadPrimitiveArray(type, itemType, arraySize, out flag3))
			{
				flag2 = true;
			}
			if (!flag3)
			{
				if (arraySize == -1)
				{
					object obj = null;
					if (flag)
					{
						obj = Array.CreateInstance(itemType, 32);
					}
					int i;
					for (i = 0; i < 2147483647; i++)
					{
						if (this.IsStartElement(this.itemName, this.itemNamespace))
						{
							this.context.IncrementItemCount(1);
							object value = this.ReadCollectionItem(collectionContract, itemType, text, @namespace);
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
					}
				}
				else
				{
					this.context.IncrementItemCount(arraySize);
					if (flag)
					{
						this.objectLocal = Array.CreateInstance(itemType, arraySize);
						this.context.AddNewObject(this.objectLocal);
					}
					for (int j = 0; j < arraySize; j++)
					{
						if (this.IsStartElement(this.itemName, this.itemNamespace))
						{
							object value2 = this.ReadCollectionItem(collectionContract, itemType, text, @namespace);
							if (flag)
							{
								((Array)this.objectLocal).SetValue(value2, j);
							}
							else
							{
								this.StoreCollectionValue(this.objectLocal, itemType, value2, collectionContract);
							}
						}
						else
						{
							this.HandleUnexpectedItemInCollection(ref j);
						}
					}
					this.context.CheckEndOfArray(this.xmlReader, arraySize, this.itemName, this.itemNamespace);
				}
			}
			if (flag2)
			{
				this.context.AddNewObjectWithId(objectId, this.objectLocal);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00047E54 File Offset: 0x00046054
		private void ReadGetOnlyCollection(CollectionDataContract collectionContract)
		{
			Type underlyingType = collectionContract.UnderlyingType;
			Type itemType = collectionContract.ItemType;
			bool flag = collectionContract.Kind == CollectionKind.Array;
			string text = collectionContract.ItemName;
			string @namespace = collectionContract.StableName.Namespace;
			this.objectLocal = this.context.GetCollectionMember();
			if (this.IsStartElement(this.itemName, this.itemNamespace))
			{
				if (this.objectLocal == null)
				{
					XmlObjectSerializerReadContext.ThrowNullValueReturnedForGetOnlyCollectionException(underlyingType);
					return;
				}
				int num = 0;
				if (flag)
				{
					num = ((Array)this.objectLocal).Length;
				}
				this.context.AddNewObject(this.objectLocal);
				int i = 0;
				while (i < 2147483647)
				{
					if (this.IsStartElement(this.itemName, this.itemNamespace))
					{
						this.context.IncrementItemCount(1);
						object value = this.ReadCollectionItem(collectionContract, itemType, text, @namespace);
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
				this.context.CheckEndOfArray(this.xmlReader, num, this.itemName, this.itemNamespace);
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00047F98 File Offset: 0x00046198
		private bool TryReadPrimitiveArray(Type type, Type itemType, int size, out bool readResult)
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
					text = "TryReadDateTimeArray";
					break;
				}
			}
			else
			{
				text = "TryReadBooleanArray";
			}
			if (text != null)
			{
				MethodInfo method = typeof(XmlReaderDelegator).GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				object[] array = new object[]
				{
					this.context,
					this.itemName,
					this.itemNamespace,
					size,
					this.objectLocal
				};
				readResult = (bool)method.Invoke(this.xmlReader, array);
				this.objectLocal = array.Last<object>();
				return true;
			}
			return false;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00048090 File Offset: 0x00046290
		private object ReadCollectionItem(CollectionDataContract collectionContract, Type itemType, string itemName, string itemNs)
		{
			if (collectionContract.Kind == CollectionKind.Dictionary || collectionContract.Kind == CollectionKind.GenericDictionary)
			{
				this.context.ResetAttributes();
				return CodeInterpreter.ConvertValue(collectionContract.ItemContract.ReadXmlValue(this.xmlReader, this.context), Globals.TypeOfObject, itemType);
			}
			return this.ReadValue(itemType, itemName, itemNs);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000480E8 File Offset: 0x000462E8
		private void StoreCollectionValue(object collection, Type valueType, object value, CollectionDataContract collectionContract)
		{
			if (collectionContract.Kind == CollectionKind.GenericDictionary || collectionContract.Kind == CollectionKind.Dictionary)
			{
				ClassDataContract classDataContract = DataContract.GetDataContract(valueType) as ClassDataContract;
				DataMember dataMember = classDataContract.Members[0];
				DataMember dataMember2 = classDataContract.Members[1];
				object member = CodeInterpreter.GetMember(dataMember.MemberInfo, value);
				object member2 = CodeInterpreter.GetMember(dataMember2.MemberInfo, value);
				collectionContract.AddMethod.Invoke(collection, new object[]
				{
					member,
					member2
				});
				return;
			}
			collectionContract.AddMethod.Invoke(collection, new object[]
			{
				value
			});
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004817C File Offset: 0x0004637C
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

		// Token: 0x06001286 RID: 4742 RVA: 0x000481AA File Offset: 0x000463AA
		private bool IsStartElement(XmlDictionaryString name, XmlDictionaryString ns)
		{
			return this.xmlReader.IsStartElement(name, ns);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000481B9 File Offset: 0x000463B9
		private bool IsStartElement()
		{
			return this.xmlReader.IsStartElement();
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000481C6 File Offset: 0x000463C6
		private bool IsEndElement()
		{
			return this.xmlReader.NodeType == XmlNodeType.EndElement;
		}

		// Token: 0x04000956 RID: 2390
		private bool is_get_only_collection;

		// Token: 0x04000957 RID: 2391
		private ClassDataContract classContract;

		// Token: 0x04000958 RID: 2392
		private CollectionDataContract collectionContract;

		// Token: 0x04000959 RID: 2393
		private object objectLocal;

		// Token: 0x0400095A RID: 2394
		private Type objectType;

		// Token: 0x0400095B RID: 2395
		private XmlReaderDelegator xmlReader;

		// Token: 0x0400095C RID: 2396
		private XmlObjectSerializerReadContext context;

		// Token: 0x0400095D RID: 2397
		private XmlDictionaryString[] memberNames;

		// Token: 0x0400095E RID: 2398
		private XmlDictionaryString[] memberNamespaces;

		// Token: 0x0400095F RID: 2399
		private XmlDictionaryString itemName;

		// Token: 0x04000960 RID: 2400
		private XmlDictionaryString itemNamespace;
	}
}
