using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000160 RID: 352
	internal class XmlFormatWriterInterpreter
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x000481D7 File Offset: 0x000463D7
		public XmlFormatWriterInterpreter(ClassDataContract classContract)
		{
			this.classContract = classContract;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000481ED File Offset: 0x000463ED
		public XmlFormatWriterInterpreter(CollectionDataContract collectionContract)
		{
			this.collectionContract = collectionContract;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00048203 File Offset: 0x00046403
		private ClassDataContract classDataContract
		{
			get
			{
				return (ClassDataContract)this.dataContract;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00048210 File Offset: 0x00046410
		private CollectionDataContract collectionDataContract
		{
			get
			{
				return (CollectionDataContract)this.dataContract;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00048220 File Offset: 0x00046420
		public void WriteToXml(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context, ClassDataContract dataContract)
		{
			this.writer = xmlWriter;
			this.obj = obj;
			this.ctx = context;
			this.dataContract = dataContract;
			this.InitArgs(this.classContract.UnderlyingType);
			if (this.classContract.IsReadOnlyContract)
			{
				DataContract.ThrowInvalidDataContractException(this.classContract.SerializationExceptionMessage, null);
			}
			this.WriteClass(this.classContract);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00048288 File Offset: 0x00046488
		public void WriteCollectionToXml(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context, CollectionDataContract collectionContract)
		{
			this.writer = xmlWriter;
			this.obj = obj;
			this.ctx = context;
			this.dataContract = collectionContract;
			this.InitArgs(collectionContract.UnderlyingType);
			if (collectionContract.IsReadOnlyContract)
			{
				DataContract.ThrowInvalidDataContractException(collectionContract.SerializationExceptionMessage, null);
			}
			this.WriteCollection(collectionContract);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000482E0 File Offset: 0x000464E0
		private void InitArgs(Type objType)
		{
			if (objType == Globals.TypeOfDateTimeOffsetAdapter)
			{
				this.objLocal = DateTimeOffsetAdapter.GetDateTimeOffsetAdapter((DateTimeOffset)this.obj);
				return;
			}
			this.objLocal = CodeInterpreter.ConvertValue(this.obj, typeof(object), objType);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00048334 File Offset: 0x00046534
		private void InvokeOnSerializing(ClassDataContract classContract, object objSerialized, XmlObjectSerializerWriteContext ctx)
		{
			if (classContract.BaseContract != null)
			{
				this.InvokeOnSerializing(classContract.BaseContract, objSerialized, ctx);
			}
			if (classContract.OnSerializing != null)
			{
				classContract.OnSerializing.Invoke(objSerialized, new object[]
				{
					ctx.GetStreamingContext()
				});
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00048388 File Offset: 0x00046588
		private void InvokeOnSerialized(ClassDataContract classContract, object objSerialized, XmlObjectSerializerWriteContext ctx)
		{
			if (classContract.BaseContract != null)
			{
				this.InvokeOnSerialized(classContract.BaseContract, objSerialized, ctx);
			}
			if (classContract.OnSerialized != null)
			{
				classContract.OnSerialized.Invoke(objSerialized, new object[]
				{
					ctx.GetStreamingContext()
				});
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000483DC File Offset: 0x000465DC
		private void WriteClass(ClassDataContract classContract)
		{
			this.InvokeOnSerializing(classContract, this.objLocal, this.ctx);
			if (classContract.IsISerializable)
			{
				this.ctx.WriteISerializable(this.writer, (ISerializable)this.objLocal);
			}
			else
			{
				if (classContract.ContractNamespaces.Length > 1)
				{
					this.contractNamespaces = this.classDataContract.ContractNamespaces;
				}
				this.memberNames = this.classDataContract.MemberNames;
				for (int i = 0; i < classContract.ChildElementNamespaces.Length; i++)
				{
					if (classContract.ChildElementNamespaces[i] != null)
					{
						this.childElementNamespaces = this.classDataContract.ChildElementNamespaces;
					}
				}
				if (classContract.HasExtensionData)
				{
					ExtensionDataObject extensionData = ((IExtensibleDataObject)this.objLocal).ExtensionData;
					this.ctx.WriteExtensionData(this.writer, extensionData, -1);
					this.WriteMembers(classContract, extensionData, classContract);
				}
				else
				{
					this.WriteMembers(classContract, null, classContract);
				}
			}
			this.InvokeOnSerialized(classContract, this.objLocal, this.ctx);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000484D8 File Offset: 0x000466D8
		private void WriteCollection(CollectionDataContract collectionContract)
		{
			XmlDictionaryString @namespace = this.dataContract.Namespace;
			XmlDictionaryString collectionItemName = this.collectionDataContract.CollectionItemName;
			if (collectionContract.ChildElementNamespace != null)
			{
				this.writer.WriteNamespaceDecl(this.collectionDataContract.ChildElementNamespace);
			}
			if (collectionContract.Kind == CollectionKind.Array)
			{
				Type itemType = collectionContract.ItemType;
				if (this.objLocal.GetType().GetElementType() != itemType)
				{
					throw new InvalidCastException(string.Format("Cannot cast array of {0} to array of {1}", this.objLocal.GetType().GetElementType(), itemType));
				}
				this.ctx.IncrementArrayCount(this.writer, (Array)this.objLocal);
				if (!this.TryWritePrimitiveArray(collectionContract.UnderlyingType, itemType, () => this.objLocal, collectionItemName, @namespace))
				{
					Array array = (Array)this.objLocal;
					int[] array2 = new int[1];
					for (int i = 0; i < array.Length; i++)
					{
						if (!this.TryWritePrimitive(itemType, null, null, new int?(i), @namespace, collectionItemName, 0))
						{
							this.WriteStartElement(itemType, collectionContract.Namespace, @namespace, collectionItemName, 0);
							array2[0] = i;
							object value = array.GetValue(array2);
							this.WriteValue(itemType, value, false);
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
					methodInfo.Invoke(this.ctx, new object[]
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
				MethodInfo left = type.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
				MethodInfo methodInfo2 = type.GetMethod("get_Current", BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
				if (left == null || methodInfo2 == null)
				{
					if (type.IsInterface)
					{
						if (left == null)
						{
							left = XmlFormatGeneratorStatics.MoveNextMethod;
						}
						if (methodInfo2 == null)
						{
							methodInfo2 = XmlFormatGeneratorStatics.GetCurrentMethod;
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
						if (left == null)
						{
							left = CollectionDataContract.GetTargetMethodWithName("MoveNext", type, interfaceType);
						}
						if (methodInfo2 == null)
						{
							methodInfo2 = CollectionDataContract.GetTargetMethodWithName("get_Current", type, interfaceType);
						}
					}
				}
				Type returnType = methodInfo2.ReturnType;
				object currentValue = null;
				IEnumerator enumerator = (IEnumerator)collectionContract.GetEnumeratorMethod.Invoke(this.objLocal, new object[0]);
				if (flag)
				{
					enumerator = new CollectionDataContract.DictionaryEnumerator((IDictionaryEnumerator)enumerator);
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
				object[] parameters = new object[0];
				while (enumerator != null && enumerator.MoveNext())
				{
					currentValue = methodInfo2.Invoke(enumerator, parameters);
					if (methodInfo == null)
					{
						XmlFormatGeneratorStatics.IncrementItemCountMethod.Invoke(this.ctx, new object[]
						{
							1
						});
					}
					if (!this.TryWritePrimitive(returnType, () => currentValue, null, null, @namespace, collectionItemName, 0))
					{
						this.WriteStartElement(returnType, collectionContract.Namespace, @namespace, collectionItemName, 0);
						if (flag2 || flag)
						{
							this.collectionDataContract.ItemContract.WriteXmlValue(this.writer, currentValue, this.ctx);
						}
						else
						{
							this.WriteValue(returnType, currentValue, false);
						}
						this.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00048A00 File Offset: 0x00046C00
		private int WriteMembers(ClassDataContract classContract, ExtensionDataObject extensionData, ClassDataContract derivedMostClassContract)
		{
			int num = (classContract.BaseContract == null) ? 0 : this.WriteMembers(classContract.BaseContract, extensionData, derivedMostClassContract);
			XmlDictionaryString xmlDictionaryString = (this.contractNamespaces == null) ? this.dataContract.Namespace : this.contractNamespaces[this.typeIndex - 1];
			this.ctx.IncrementItemCount(classContract.Members.Count);
			int i = 0;
			while (i < classContract.Members.Count)
			{
				DataMember dataMember = classContract.Members[i];
				Type memberType = dataMember.MemberType;
				object memberValue = null;
				if (dataMember.IsGetOnlyCollection)
				{
					this.ctx.StoreIsGetOnlyCollection();
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
					bool flag3 = this.CheckIfMemberHasConflict(dataMember, classContract, derivedMostClassContract);
					if (flag3 || !this.TryWritePrimitive(memberType, flag2 ? (() => memberValue) : null, dataMember.MemberInfo, null, xmlDictionaryString, null, i + this.childElementIndex))
					{
						this.WriteStartElement(memberType, classContract.Namespace, xmlDictionaryString, null, i + this.childElementIndex);
						if (classContract.ChildElementNamespaces[i + this.childElementIndex] != null)
						{
							this.writer.WriteNamespaceDecl(this.childElementNamespaces[i + this.childElementIndex]);
						}
						if (memberValue == null)
						{
							memberValue = this.LoadMemberValue(dataMember);
						}
						this.WriteValue(memberType, memberValue, flag3);
						this.WriteEndElement();
					}
					if (classContract.HasExtensionData)
					{
						this.ctx.WriteExtensionData(this.writer, extensionData, num);
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

		// Token: 0x06001295 RID: 4757 RVA: 0x00048C14 File Offset: 0x00046E14
		internal bool IsDefaultValue(Type type, object value)
		{
			object defaultValue = this.GetDefaultValue(type);
			if (defaultValue != null)
			{
				return defaultValue.Equals(value);
			}
			return value == null;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00048C38 File Offset: 0x00046E38
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

		// Token: 0x06001297 RID: 4759 RVA: 0x00048CE4 File Offset: 0x00046EE4
		private bool CheckIfMemberHasConflict(DataMember member, ClassDataContract classContract, ClassDataContract derivedMostClassContract)
		{
			if (this.CheckIfConflictingMembersHaveDifferentTypes(member))
			{
				return true;
			}
			string name = member.Name;
			string @namespace = classContract.StableName.Namespace;
			ClassDataContract classDataContract = derivedMostClassContract;
			while (classDataContract != null && classDataContract != classContract)
			{
				if (@namespace == classDataContract.StableName.Namespace)
				{
					List<DataMember> members = classDataContract.Members;
					for (int i = 0; i < members.Count; i++)
					{
						if (name == members[i].Name)
						{
							return this.CheckIfConflictingMembersHaveDifferentTypes(members[i]);
						}
					}
				}
				classDataContract = classDataContract.BaseContract;
			}
			return false;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00048D75 File Offset: 0x00046F75
		private bool CheckIfConflictingMembersHaveDifferentTypes(DataMember member)
		{
			while (member.ConflictingMember != null)
			{
				if (member.MemberType != member.ConflictingMember.MemberType)
				{
					return true;
				}
				member = member.ConflictingMember;
			}
			return false;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00048DA4 File Offset: 0x00046FA4
		private bool NeedsPrefix(Type type, XmlDictionaryString ns)
		{
			return type == Globals.TypeOfXmlQualifiedName && (ns != null && ns.Value != null) && ns.Value.Length > 0;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00048DD0 File Offset: 0x00046FD0
		private void WriteStartElement(Type type, XmlDictionaryString ns, XmlDictionaryString namespaceLocal, XmlDictionaryString nameLocal, int nameIndex)
		{
			bool flag = this.NeedsPrefix(type, ns);
			nameLocal = (nameLocal ?? this.memberNames[nameIndex]);
			if (flag)
			{
				this.writer.WriteStartElement("q", nameLocal, namespaceLocal);
				return;
			}
			this.writer.WriteStartElement(nameLocal, namespaceLocal);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00048E0F File Offset: 0x0004700F
		private void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00048E1C File Offset: 0x0004701C
		private void WriteValue(Type memberType, object memberValue, bool writeXsiType)
		{
			if (memberType.IsPointer)
			{
				Pointer pointer = (Pointer)XmlFormatGeneratorStatics.BoxPointer.Invoke(null, new object[]
				{
					memberValue,
					memberType
				});
			}
			bool flag = memberType.IsGenericType && memberType.GetGenericTypeDefinition() == Globals.TypeOfNullable;
			if (memberType.IsValueType && !flag)
			{
				PrimitiveDataContract primitiveDataContract = PrimitiveDataContract.GetPrimitiveDataContract(memberType);
				if (primitiveDataContract != null && !writeXsiType)
				{
					primitiveDataContract.XmlFormatContentWriterMethod.Invoke(this.writer, new object[]
					{
						memberValue
					});
					return;
				}
				bool isDeclaredType = Type.GetTypeHandle(memberValue).Equals(CodeInterpreter.ConvertValue(memberValue, memberType, Globals.TypeOfObject));
				this.ctx.InternalSerialize(this.writer, memberValue, isDeclaredType, writeXsiType, DataContract.GetId(memberType.TypeHandle), memberType.TypeHandle);
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
					XmlFormatGeneratorStatics.WriteNullMethod.Invoke(this.ctx, new object[]
					{
						this.writer,
						memberType,
						DataContract.IsTypeSerializable(memberType)
					});
					return;
				}
				PrimitiveDataContract primitiveDataContract2 = PrimitiveDataContract.GetPrimitiveDataContract(memberType);
				if (primitiveDataContract2 != null && primitiveDataContract2.UnderlyingType != Globals.TypeOfObject && !writeXsiType)
				{
					if (flag)
					{
						primitiveDataContract2.XmlFormatContentWriterMethod.Invoke(this.writer, new object[]
						{
							memberValue
						});
						return;
					}
					primitiveDataContract2.XmlFormatContentWriterMethod.Invoke(this.ctx, new object[]
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
						XmlFormatGeneratorStatics.WriteNullMethod.Invoke(this.ctx, new object[]
						{
							this.writer,
							memberType,
							DataContract.IsTypeSerializable(memberType)
						});
						return;
					}
					RuntimeTypeHandle typeHandle = Type.GetTypeHandle(memberValue);
					bool isDeclaredType2 = typeHandle.Equals(CodeInterpreter.ConvertValue(memberValue, memberType, Globals.TypeOfObject));
					if (flag)
					{
						this.ctx.InternalSerialize(this.writer, memberValue, isDeclaredType2, writeXsiType, DataContract.GetId(memberType.TypeHandle), memberType.TypeHandle);
						return;
					}
					if (memberType == Globals.TypeOfObject)
					{
						DataContract dataContract = DataContract.GetDataContract(memberValue.GetType());
						this.writer.WriteAttributeQualifiedName("i", DictionaryGlobals.XsiTypeLocalName, DictionaryGlobals.SchemaInstanceNamespace, dataContract.Name, dataContract.Namespace);
						this.ctx.InternalSerializeReference(this.writer, memberValue, false, false, -1, typeHandle);
						return;
					}
					this.ctx.InternalSerializeReference(this.writer, memberValue, isDeclaredType2, writeXsiType, DataContract.GetId(memberType.TypeHandle), memberType.TypeHandle);
					return;
				}
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00049160 File Offset: 0x00047360
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

		// Token: 0x0600129E RID: 4766 RVA: 0x00049220 File Offset: 0x00047420
		private bool TryWritePrimitive(Type type, Func<object> value, MemberInfo memberInfo, int? arrayItemIndex, XmlDictionaryString ns, XmlDictionaryString name, int nameIndex)
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
				obj = this.ctx;
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
			list.Add(ns);
			primitiveDataContract.XmlFormatWriterMethod.Invoke(obj, list.ToArray());
			return true;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00049304 File Offset: 0x00047504
		private bool TryWritePrimitiveArray(Type type, Type itemType, Func<object> value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
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
					text = "WriteInt32Array";
					break;
				case TypeCode.Int64:
					text = "WriteInt64Array";
					break;
				case TypeCode.Single:
					text = "WriteSingleArray";
					break;
				case TypeCode.Double:
					text = "WriteDoubleArray";
					break;
				case TypeCode.Decimal:
					text = "WriteDecimalArray";
					break;
				case TypeCode.DateTime:
					text = "WriteDateTimeArray";
					break;
				}
			}
			else
			{
				text = "WriteBooleanArray";
			}
			if (text != null)
			{
				typeof(XmlWriterDelegator).GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
				{
					type,
					typeof(XmlDictionaryString),
					typeof(XmlDictionaryString)
				}, null).Invoke(this.writer, new object[]
				{
					value(),
					itemName,
					itemNamespace
				});
				return true;
			}
			return false;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000493EC File Offset: 0x000475EC
		private object LoadMemberValue(DataMember member)
		{
			return CodeInterpreter.GetMember(member.MemberInfo, this.objLocal);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000493FF File Offset: 0x000475FF
		[CompilerGenerated]
		private object <WriteCollection>b__24_0()
		{
			return this.objLocal;
		}

		// Token: 0x04000961 RID: 2401
		private ClassDataContract classContract;

		// Token: 0x04000962 RID: 2402
		private CollectionDataContract collectionContract;

		// Token: 0x04000963 RID: 2403
		private XmlWriterDelegator writer;

		// Token: 0x04000964 RID: 2404
		private object obj;

		// Token: 0x04000965 RID: 2405
		private XmlObjectSerializerWriteContext ctx;

		// Token: 0x04000966 RID: 2406
		private DataContract dataContract;

		// Token: 0x04000967 RID: 2407
		private object objLocal;

		// Token: 0x04000968 RID: 2408
		private XmlDictionaryString[] contractNamespaces;

		// Token: 0x04000969 RID: 2409
		private XmlDictionaryString[] memberNames;

		// Token: 0x0400096A RID: 2410
		private XmlDictionaryString[] childElementNamespaces;

		// Token: 0x0400096B RID: 2411
		private int typeIndex = 1;

		// Token: 0x0400096C RID: 2412
		private int childElementIndex;

		// Token: 0x02000161 RID: 353
		[CompilerGenerated]
		private sealed class <>c__DisplayClass24_0
		{
			// Token: 0x060012A2 RID: 4770 RVA: 0x0000222F File Offset: 0x0000042F
			public <>c__DisplayClass24_0()
			{
			}

			// Token: 0x060012A3 RID: 4771 RVA: 0x00049407 File Offset: 0x00047607
			internal object <WriteCollection>b__1()
			{
				return this.currentValue;
			}

			// Token: 0x0400096D RID: 2413
			public object currentValue;
		}

		// Token: 0x02000162 RID: 354
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x060012A4 RID: 4772 RVA: 0x0000222F File Offset: 0x0000042F
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x060012A5 RID: 4773 RVA: 0x0004940F File Offset: 0x0004760F
			internal object <WriteMembers>b__0()
			{
				return this.memberValue;
			}

			// Token: 0x0400096E RID: 2414
			public object memberValue;
		}

		// Token: 0x02000163 RID: 355
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x060012A6 RID: 4774 RVA: 0x0000222F File Offset: 0x0000042F
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x060012A7 RID: 4775 RVA: 0x00049417 File Offset: 0x00047617
			internal object <WriteValue>b__0()
			{
				return this.memberValue;
			}

			// Token: 0x0400096F RID: 2415
			public object memberValue;
		}
	}
}
