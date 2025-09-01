using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000CE RID: 206
	internal static class AsnSerializer
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00014D4F File Offset: 0x00012F4F
		private static AsnSerializer.Deserializer TryOrFail<T>(AsnSerializer.TryDeserializer<T> tryDeserializer)
		{
			return delegate(AsnReader reader)
			{
				T t;
				if (tryDeserializer(reader, out t))
				{
					return t;
				}
				throw new CryptographicException("ASN1 corrupted data.");
			};
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00014D68 File Offset: 0x00012F68
		private static FieldInfo[] GetOrderedFields(Type typeT)
		{
			return AsnSerializer.s_orderedFields.GetOrAdd(typeT, delegate(Type t)
			{
				FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (fields.Length == 0)
				{
					return Array.Empty<FieldInfo>();
				}
				try
				{
					int metadataToken = fields[0].MetadataToken;
				}
				catch (InvalidOperationException)
				{
					return fields;
				}
				Array.Sort<FieldInfo>(fields, (FieldInfo x, FieldInfo y) => x.MetadataToken.CompareTo(y.MetadataToken));
				return fields;
			});
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00014D94 File Offset: 0x00012F94
		private static ChoiceAttribute GetChoiceAttribute(Type typeT)
		{
			ChoiceAttribute customAttribute = typeT.GetCustomAttribute(false);
			if (customAttribute == null)
			{
				return null;
			}
			if (customAttribute.AllowNull && !AsnSerializer.CanBeNull(typeT))
			{
				throw new AsnSerializationConstraintException(SR.Format("[Choice].AllowNull=true is not valid because type '{0}' cannot have a null value.", typeT.FullName));
			}
			return customAttribute;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00014DD5 File Offset: 0x00012FD5
		private static bool CanBeNull(Type t)
		{
			return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00014E00 File Offset: 0x00013000
		private static void PopulateChoiceLookup(Dictionary<ValueTuple<TagClass, int>, LinkedList<FieldInfo>> lookup, Type typeT, LinkedList<FieldInfo> currentSet)
		{
			foreach (FieldInfo fieldInfo in AsnSerializer.GetOrderedFields(typeT))
			{
				Type type = fieldInfo.FieldType;
				if (!AsnSerializer.CanBeNull(type))
				{
					throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on [Choice] type '{1}' can not be assigned a null value.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
				}
				type = AsnSerializer.UnpackIfNullable(type);
				if (currentSet.Contains(fieldInfo))
				{
					throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on [Choice] type '{1}' has introduced a type chain cycle.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
				}
				LinkedListNode<FieldInfo> node = new LinkedListNode<FieldInfo>(fieldInfo);
				currentSet.AddLast(node);
				if (AsnSerializer.GetChoiceAttribute(type) != null)
				{
					AsnSerializer.PopulateChoiceLookup(lookup, type, currentSet);
				}
				else
				{
					AsnSerializer.SerializerFieldData serializerFieldData;
					AsnSerializer.GetFieldInfo(type, fieldInfo, out serializerFieldData);
					if (serializerFieldData.DefaultContents != null)
					{
						throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on [Choice] type '{1}' has a default value, which is not permitted.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
					}
					ValueTuple<TagClass, int> key = new ValueTuple<TagClass, int>(serializerFieldData.ExpectedTag.TagClass, serializerFieldData.ExpectedTag.TagValue);
					LinkedList<FieldInfo> linkedList;
					if (lookup.TryGetValue(key, out linkedList))
					{
						FieldInfo value = linkedList.Last.Value;
						throw new AsnSerializationConstraintException(SR.Format("The tag ({0} {1}) for field '{2}' on type '{3}' already is associated in this context with field '{4}' on type '{5}'.", new object[]
						{
							serializerFieldData.ExpectedTag.TagClass,
							serializerFieldData.ExpectedTag.TagValue,
							fieldInfo.Name,
							fieldInfo.DeclaringType.FullName,
							value.Name,
							value.DeclaringType.FullName
						}));
					}
					lookup.Add(key, new LinkedList<FieldInfo>(currentSet));
				}
				currentSet.RemoveLast();
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00014FA0 File Offset: 0x000131A0
		private static void SerializeChoice(Type typeT, object value, AsnWriter writer)
		{
			Dictionary<ValueTuple<TagClass, int>, LinkedList<FieldInfo>> lookup = new Dictionary<ValueTuple<TagClass, int>, LinkedList<FieldInfo>>();
			LinkedList<FieldInfo> currentSet = new LinkedList<FieldInfo>();
			AsnSerializer.PopulateChoiceLookup(lookup, typeT, currentSet);
			FieldInfo fieldInfo = null;
			object value2 = null;
			if (value == null)
			{
				if (AsnSerializer.GetChoiceAttribute(typeT).AllowNull)
				{
					writer.WriteNull();
					return;
				}
			}
			else
			{
				foreach (FieldInfo fieldInfo2 in AsnSerializer.GetOrderedFields(typeT))
				{
					object value3 = fieldInfo2.GetValue(value);
					if (value3 != null)
					{
						if (fieldInfo != null)
						{
							throw new AsnSerializationConstraintException(SR.Format("Fields '{0}' and '{1}' on type '{2}' are both non-null when only one value is permitted.", fieldInfo2.Name, fieldInfo.Name, typeT.FullName));
						}
						fieldInfo = fieldInfo2;
						value2 = value3;
					}
				}
			}
			if (fieldInfo == null)
			{
				throw new AsnSerializationConstraintException(SR.Format("An instance of [Choice] type '{0}' has no non-null fields.", typeT.FullName));
			}
			AsnSerializer.GetSerializer(fieldInfo.FieldType, fieldInfo)(value2, writer);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001506C File Offset: 0x0001326C
		private static object DeserializeChoice(AsnReader reader, Type typeT)
		{
			Dictionary<ValueTuple<TagClass, int>, LinkedList<FieldInfo>> dictionary = new Dictionary<ValueTuple<TagClass, int>, LinkedList<FieldInfo>>();
			LinkedList<FieldInfo> currentSet = new LinkedList<FieldInfo>();
			AsnSerializer.PopulateChoiceLookup(dictionary, typeT, currentSet);
			Asn1Tag left = reader.PeekTag();
			if (left == Asn1Tag.Null)
			{
				if (AsnSerializer.GetChoiceAttribute(typeT).AllowNull)
				{
					reader.ReadNull();
					return null;
				}
				throw new CryptographicException("ASN1 corrupted data.");
			}
			else
			{
				ValueTuple<TagClass, int> key = new ValueTuple<TagClass, int>(left.TagClass, left.TagValue);
				LinkedList<FieldInfo> linkedList;
				if (dictionary.TryGetValue(key, out linkedList))
				{
					LinkedListNode<FieldInfo> linkedListNode = linkedList.Last;
					FieldInfo value = linkedListNode.Value;
					object obj = Activator.CreateInstance(value.DeclaringType);
					object value2 = AsnSerializer.GetDeserializer(value.FieldType, value)(reader);
					value.SetValue(obj, value2);
					while (linkedListNode.Previous != null)
					{
						linkedListNode = linkedListNode.Previous;
						value = linkedListNode.Value;
						object obj2 = Activator.CreateInstance(value.DeclaringType);
						value.SetValue(obj2, obj);
						obj = obj2;
					}
					return obj;
				}
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001516C File Offset: 0x0001336C
		private static void SerializeCustomType(Type typeT, object value, AsnWriter writer, Asn1Tag tag)
		{
			writer.PushSequence(tag);
			foreach (FieldInfo fieldInfo in typeT.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				AsnSerializer.GetSerializer(fieldInfo.FieldType, fieldInfo)(fieldInfo.GetValue(value), writer);
			}
			writer.PopSequence(tag);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000151BC File Offset: 0x000133BC
		private static object DeserializeCustomType(AsnReader reader, Type typeT, Asn1Tag expectedTag)
		{
			object obj = Activator.CreateInstance(typeT);
			AsnReader asnReader = reader.ReadSequence(expectedTag);
			foreach (FieldInfo fieldInfo in typeT.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				AsnSerializer.Deserializer deserializer = AsnSerializer.GetDeserializer(fieldInfo.FieldType, fieldInfo);
				try
				{
					fieldInfo.SetValue(obj, deserializer(asnReader));
				}
				catch (Exception inner)
				{
					throw new CryptographicException(SR.Format("Unable to set field {0} on type {1}.", fieldInfo.Name, fieldInfo.DeclaringType.FullName), inner);
				}
			}
			asnReader.ThrowIfNotEmpty();
			return obj;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015254 File Offset: 0x00013454
		private static AsnSerializer.Deserializer ExplicitValueDeserializer(AsnSerializer.Deserializer valueDeserializer, Asn1Tag expectedTag)
		{
			return (AsnReader reader) => AsnSerializer.ExplicitValueDeserializer(reader, valueDeserializer, expectedTag);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00015274 File Offset: 0x00013474
		private static object ExplicitValueDeserializer(AsnReader reader, AsnSerializer.Deserializer valueDeserializer, Asn1Tag expectedTag)
		{
			AsnReader asnReader = reader.ReadSequence(expectedTag);
			object result = valueDeserializer(asnReader);
			asnReader.ThrowIfNotEmpty();
			return result;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015296 File Offset: 0x00013496
		private static AsnSerializer.Deserializer DefaultValueDeserializer(AsnSerializer.Deserializer valueDeserializer, bool isOptional, byte[] defaultContents, Asn1Tag? expectedTag)
		{
			return (AsnReader reader) => AsnSerializer.DefaultValueDeserializer(reader, expectedTag, valueDeserializer, defaultContents, isOptional);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000152C4 File Offset: 0x000134C4
		private static object DefaultValueDeserializer(AsnReader reader, Asn1Tag? expectedTag, AsnSerializer.Deserializer valueDeserializer, byte[] defaultContents, bool isOptional)
		{
			if (reader.HasData)
			{
				Asn1Tag asn1Tag = reader.PeekTag();
				if (expectedTag == null || asn1Tag.AsPrimitive() == expectedTag.Value.AsPrimitive())
				{
					return valueDeserializer(reader);
				}
			}
			if (isOptional)
			{
				return null;
			}
			if (defaultContents != null)
			{
				return AsnSerializer.DefaultValue(defaultContents, valueDeserializer);
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001532C File Offset: 0x0001352C
		private unsafe static AsnSerializer.Serializer GetSerializer(Type typeT, FieldInfo fieldInfo)
		{
			byte[] defaultContents;
			Asn1Tag? explicitTag;
			bool flag;
			AsnSerializer.Serializer literalValueSerializer = AsnSerializer.GetSimpleSerializer(typeT, fieldInfo, out defaultContents, out flag, out explicitTag);
			AsnSerializer.Serializer serializer = literalValueSerializer;
			if (flag)
			{
				serializer = delegate(object obj, AsnWriter writer)
				{
					if (obj != null)
					{
						literalValueSerializer(obj, writer);
					}
				};
			}
			else if (defaultContents != null)
			{
				serializer = delegate(object obj, AsnWriter writer)
				{
					AsnReader asnReader;
					using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
					{
						literalValueSerializer(obj, asnWriter);
						asnReader = new AsnReader(asnWriter.Encode(), AsnEncodingRules.DER);
					}
					ReadOnlySpan<byte> span = asnReader.GetEncodedValue().Span;
					bool flag2 = false;
					if (span.Length == defaultContents.Length)
					{
						flag2 = true;
						for (int i = 0; i < span.Length; i++)
						{
							if (*span[i] != defaultContents[i])
							{
								flag2 = false;
								break;
							}
						}
					}
					if (!flag2)
					{
						literalValueSerializer(obj, writer);
					}
				};
			}
			if (explicitTag != null)
			{
				return delegate(object obj, AsnWriter writer)
				{
					using (AsnWriter asnWriter = new AsnWriter(writer.RuleSet))
					{
						serializer(obj, asnWriter);
						if (asnWriter.Encode().Length != 0)
						{
							writer.PushSequence(explicitTag.Value);
							serializer(obj, writer);
							writer.PopSequence(explicitTag.Value);
						}
					}
				};
			}
			return serializer;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000153B8 File Offset: 0x000135B8
		private static AsnSerializer.Serializer GetSimpleSerializer(Type typeT, FieldInfo fieldInfo, out byte[] defaultContents, out bool isOptional, out Asn1Tag? explicitTag)
		{
			if (!typeT.IsSealed || typeT.ContainsGenericParameters)
			{
				throw new AsnSerializationConstraintException(SR.Format("Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.", typeT.FullName));
			}
			AsnSerializer.SerializerFieldData fieldData;
			AsnSerializer.GetFieldInfo(typeT, fieldInfo, out fieldData);
			defaultContents = fieldData.DefaultContents;
			isOptional = fieldData.IsOptional;
			typeT = AsnSerializer.UnpackIfNullable(typeT);
			bool flag = AsnSerializer.GetChoiceAttribute(typeT) != null;
			Asn1Tag tag;
			if (fieldData.HasExplicitTag)
			{
				explicitTag = new Asn1Tag?(fieldData.ExpectedTag);
				tag = new Asn1Tag(fieldData.TagType.GetValueOrDefault(), false);
			}
			else
			{
				explicitTag = null;
				tag = fieldData.ExpectedTag;
			}
			if (typeT.IsPrimitive)
			{
				return AsnSerializer.GetPrimitiveSerializer(typeT, tag);
			}
			if (typeT.IsEnum)
			{
				if (typeT.GetCustomAttributes(typeof(FlagsAttribute), false).Length != 0)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteNamedBitList(tag, value);
					};
				}
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteEnumeratedValue(tag, value);
				};
			}
			else if (typeT == typeof(string))
			{
				UniversalTagNumber? tagType = fieldData.TagType;
				UniversalTagNumber universalTagNumber = UniversalTagNumber.ObjectIdentifier;
				if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteObjectIdentifier(tag, (string)value);
					};
				}
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteCharacterString(tag, fieldData.TagType.Value, (string)value);
				};
			}
			else if (typeT == typeof(ReadOnlyMemory<byte>) && !fieldData.IsCollection)
			{
				if (fieldData.IsAny)
				{
					if (fieldData.SpecifiedTag && !fieldData.HasExplicitTag)
					{
						return delegate(object value, AsnWriter writer)
						{
							ReadOnlyMemory<byte> preEncodedValue = (ReadOnlyMemory<byte>)value;
							Asn1Tag asn1Tag;
							int num;
							if (!Asn1Tag.TryParse(preEncodedValue.Span, out asn1Tag, out num) || asn1Tag.AsPrimitive() != fieldData.ExpectedTag.AsPrimitive())
							{
								throw new CryptographicException("ASN1 corrupted data.");
							}
							writer.WriteEncodedValue(preEncodedValue);
						};
					}
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteEncodedValue((ReadOnlyMemory<byte>)value);
					};
				}
				else
				{
					UniversalTagNumber? tagType = fieldData.TagType;
					UniversalTagNumber universalTagNumber = UniversalTagNumber.BitString;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.WriteBitString(tag, ((ReadOnlyMemory<byte>)value).Span, 0);
						};
					}
					tagType = fieldData.TagType;
					universalTagNumber = UniversalTagNumber.OctetString;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.WriteOctetString(tag, ((ReadOnlyMemory<byte>)value).Span);
						};
					}
					tagType = fieldData.TagType;
					universalTagNumber = UniversalTagNumber.Integer;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.WriteInteger(tag, ((ReadOnlyMemory<byte>)value).Span);
						};
					}
					throw new CryptographicException();
				}
			}
			else
			{
				if (typeT == typeof(Oid))
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteObjectIdentifier(fieldData.ExpectedTag, (Oid)value);
					};
				}
				if (typeT.IsArray)
				{
					if (typeT.GetArrayRank() != 1)
					{
						throw new AsnSerializationConstraintException(SR.Format("Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.", typeT.FullName));
					}
					Type elementType = typeT.GetElementType();
					if (elementType.IsArray)
					{
						throw new AsnSerializationConstraintException(SR.Format("Type '{0}' cannot be serialized or deserialized because it is an array of arrays.", typeT.FullName));
					}
					AsnSerializer.Serializer serializer = AsnSerializer.GetSerializer(elementType, null);
					UniversalTagNumber? tagType = fieldData.TagType;
					UniversalTagNumber universalTagNumber = UniversalTagNumber.Set;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.PushSetOf(tag);
							foreach (object value2 in ((Array)value))
							{
								serializer(value2, writer);
							}
							writer.PopSetOf(tag);
						};
					}
					return delegate(object value, AsnWriter writer)
					{
						writer.PushSequence(tag);
						foreach (object value2 in ((Array)value))
						{
							serializer(value2, writer);
						}
						writer.PopSequence(tag);
					};
				}
				else if (typeT == typeof(DateTimeOffset))
				{
					UniversalTagNumber? tagType = fieldData.TagType;
					UniversalTagNumber universalTagNumber = UniversalTagNumber.UtcTime;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.WriteUtcTime(tag, (DateTimeOffset)value);
						};
					}
					tagType = fieldData.TagType;
					universalTagNumber = UniversalTagNumber.GeneralizedTime;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.WriteGeneralizedTime(tag, (DateTimeOffset)value, fieldData.DisallowGeneralizedTimeFractions.Value);
						};
					}
					throw new CryptographicException();
				}
				else
				{
					if (typeT == typeof(BigInteger))
					{
						return delegate(object value, AsnWriter writer)
						{
							writer.WriteInteger(tag, (BigInteger)value);
						};
					}
					if (typeT.IsLayoutSequential)
					{
						if (flag)
						{
							return delegate(object value, AsnWriter writer)
							{
								AsnSerializer.SerializeChoice(typeT, value, writer);
							};
						}
						UniversalTagNumber? tagType = fieldData.TagType;
						UniversalTagNumber universalTagNumber = UniversalTagNumber.Sequence;
						if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
						{
							return delegate(object value, AsnWriter writer)
							{
								AsnSerializer.SerializeCustomType(typeT, value, writer, tag);
							};
						}
					}
					throw new AsnSerializationConstraintException(SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
				}
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015884 File Offset: 0x00013A84
		private static AsnSerializer.Deserializer GetDeserializer(Type typeT, FieldInfo fieldInfo)
		{
			AsnSerializer.SerializerFieldData serializerFieldData;
			AsnSerializer.Deserializer deserializer = AsnSerializer.GetSimpleDeserializer(typeT, fieldInfo, out serializerFieldData);
			if (serializerFieldData.HasExplicitTag)
			{
				deserializer = AsnSerializer.ExplicitValueDeserializer(deserializer, serializerFieldData.ExpectedTag);
			}
			if (serializerFieldData.IsOptional || serializerFieldData.DefaultContents != null)
			{
				Asn1Tag? expectedTag = null;
				if (serializerFieldData.SpecifiedTag || serializerFieldData.TagType != null)
				{
					expectedTag = new Asn1Tag?(serializerFieldData.ExpectedTag);
				}
				deserializer = AsnSerializer.DefaultValueDeserializer(deserializer, serializerFieldData.IsOptional, serializerFieldData.DefaultContents, expectedTag);
			}
			return deserializer;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015900 File Offset: 0x00013B00
		private static AsnSerializer.Deserializer GetSimpleDeserializer(Type typeT, FieldInfo fieldInfo, out AsnSerializer.SerializerFieldData fieldData)
		{
			if (!typeT.IsSealed || typeT.ContainsGenericParameters)
			{
				throw new AsnSerializationConstraintException(SR.Format("Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.", typeT.FullName));
			}
			AsnSerializer.GetFieldInfo(typeT, fieldInfo, out fieldData);
			AsnSerializer.SerializerFieldData localFieldData = fieldData;
			typeT = AsnSerializer.UnpackIfNullable(typeT);
			if (fieldData.IsAny)
			{
				if (!(typeT == typeof(ReadOnlyMemory<byte>)))
				{
					throw new AsnSerializationConstraintException(SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
				}
				Asn1Tag matchTag = fieldData.ExpectedTag;
				if (fieldData.HasExplicitTag || !fieldData.SpecifiedTag)
				{
					return (AsnReader reader) => reader.GetEncodedValue();
				}
				return delegate(AsnReader reader)
				{
					Asn1Tag asn1Tag = reader.PeekTag();
					if (matchTag.TagClass != asn1Tag.TagClass || matchTag.TagValue != asn1Tag.TagValue)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					return reader.GetEncodedValue();
				};
			}
			else
			{
				if (AsnSerializer.GetChoiceAttribute(typeT) != null)
				{
					return (AsnReader reader) => AsnSerializer.DeserializeChoice(reader, typeT);
				}
				Asn1Tag expectedTag = fieldData.HasExplicitTag ? new Asn1Tag(fieldData.TagType.Value, false) : fieldData.ExpectedTag;
				if (typeT.IsPrimitive)
				{
					return AsnSerializer.GetPrimitiveDeserializer(typeT, expectedTag);
				}
				if (typeT.IsEnum)
				{
					if (typeT.GetCustomAttributes(typeof(FlagsAttribute), false).Length != 0)
					{
						return (AsnReader reader) => reader.GetNamedBitListValue(expectedTag, typeT);
					}
					return (AsnReader reader) => reader.GetEnumeratedValue(expectedTag, typeT);
				}
				else if (typeT == typeof(string))
				{
					UniversalTagNumber? tagType = fieldData.TagType;
					UniversalTagNumber universalTagNumber = UniversalTagNumber.ObjectIdentifier;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return (AsnReader reader) => reader.ReadObjectIdentifierAsString(expectedTag);
					}
					return (AsnReader reader) => reader.GetCharacterString(expectedTag, localFieldData.TagType.Value);
				}
				else if (typeT == typeof(ReadOnlyMemory<byte>) && !fieldData.IsCollection)
				{
					UniversalTagNumber? tagType = fieldData.TagType;
					UniversalTagNumber universalTagNumber = UniversalTagNumber.BitString;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(AsnReader reader)
						{
							int num;
							ReadOnlyMemory<byte> readOnlyMemory;
							if (reader.TryGetPrimitiveBitStringValue(expectedTag, out num, out readOnlyMemory))
							{
								return readOnlyMemory;
							}
							int length = reader.PeekEncodedValue().Length;
							byte[] array = ArrayPool<byte>.Shared.Rent(length);
							object result;
							try
							{
								int length2;
								if (!reader.TryCopyBitStringBytes(expectedTag, array, out num, out length2))
								{
									throw new CryptographicException();
								}
								result = new ReadOnlyMemory<byte>(array.AsSpan(0, length2).ToArray());
							}
							finally
							{
								Array.Clear(array, 0, length);
								ArrayPool<byte>.Shared.Return(array, false);
							}
							return result;
						};
					}
					tagType = fieldData.TagType;
					universalTagNumber = UniversalTagNumber.OctetString;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return delegate(AsnReader reader)
						{
							ReadOnlyMemory<byte> readOnlyMemory;
							if (reader.TryGetPrimitiveOctetStringBytes(expectedTag, out readOnlyMemory))
							{
								return readOnlyMemory;
							}
							int length = reader.PeekEncodedValue().Length;
							byte[] array = ArrayPool<byte>.Shared.Rent(length);
							object result;
							try
							{
								int length2;
								if (!reader.TryCopyOctetStringBytes(expectedTag, array, out length2))
								{
									throw new CryptographicException();
								}
								result = new ReadOnlyMemory<byte>(array.AsSpan(0, length2).ToArray());
							}
							finally
							{
								Array.Clear(array, 0, length);
								ArrayPool<byte>.Shared.Return(array, false);
							}
							return result;
						};
					}
					tagType = fieldData.TagType;
					universalTagNumber = UniversalTagNumber.Integer;
					if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
					{
						return (AsnReader reader) => reader.GetIntegerBytes(expectedTag);
					}
					throw new CryptographicException();
				}
				else
				{
					if (typeT == typeof(Oid))
					{
						bool skipFriendlyName = !fieldData.PopulateOidFriendlyName.GetValueOrDefault();
						return (AsnReader reader) => reader.ReadObjectIdentifier(expectedTag, skipFriendlyName);
					}
					if (typeT.IsArray)
					{
						if (typeT.GetArrayRank() != 1)
						{
							throw new AsnSerializationConstraintException(SR.Format("Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.", typeT.FullName));
						}
						Type baseType = typeT.GetElementType();
						if (baseType.IsArray)
						{
							throw new AsnSerializationConstraintException(SR.Format("Type '{0}' cannot be serialized or deserialized because it is an array of arrays.", typeT.FullName));
						}
						return delegate(AsnReader reader)
						{
							LinkedList<object> linkedList = new LinkedList<object>();
							UniversalTagNumber? tagType2 = localFieldData.TagType;
							UniversalTagNumber universalTagNumber2 = UniversalTagNumber.Set;
							AsnReader asnReader;
							if (tagType2.GetValueOrDefault() == universalTagNumber2 & tagType2 != null)
							{
								asnReader = reader.ReadSetOf(expectedTag, false);
							}
							else
							{
								asnReader = reader.ReadSequence(expectedTag);
							}
							AsnSerializer.Deserializer deserializer = AsnSerializer.GetDeserializer(baseType, null);
							while (asnReader.HasData)
							{
								LinkedListNode<object> node = new LinkedListNode<object>(deserializer(asnReader));
								linkedList.AddLast(node);
							}
							object[] array = linkedList.ToArray<object>();
							Array array2 = Array.CreateInstance(baseType, array.Length);
							Array.Copy(array, array2, array.Length);
							return array2;
						};
					}
					else if (typeT == typeof(DateTimeOffset))
					{
						UniversalTagNumber? tagType = fieldData.TagType;
						UniversalTagNumber universalTagNumber = UniversalTagNumber.UtcTime;
						if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
						{
							if (fieldData.TwoDigitYearMax != null)
							{
								return (AsnReader reader) => reader.GetUtcTime(expectedTag, localFieldData.TwoDigitYearMax.Value);
							}
							return (AsnReader reader) => reader.GetUtcTime(expectedTag, 2049);
						}
						else
						{
							tagType = fieldData.TagType;
							universalTagNumber = UniversalTagNumber.GeneralizedTime;
							if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
							{
								bool disallowFractions = fieldData.DisallowGeneralizedTimeFractions.Value;
								return (AsnReader reader) => reader.GetGeneralizedTime(expectedTag, disallowFractions);
							}
							throw new CryptographicException();
						}
					}
					else
					{
						if (typeT == typeof(BigInteger))
						{
							return (AsnReader reader) => reader.GetInteger(expectedTag);
						}
						if (typeT.IsLayoutSequential)
						{
							UniversalTagNumber? tagType = fieldData.TagType;
							UniversalTagNumber universalTagNumber = UniversalTagNumber.Sequence;
							if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
							{
								return (AsnReader reader) => AsnSerializer.DeserializeCustomType(reader, typeT, expectedTag);
							}
						}
						throw new AsnSerializationConstraintException(SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
					}
				}
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015DAC File Offset: 0x00013FAC
		private static object DefaultValue(byte[] defaultContents, AsnSerializer.Deserializer valueDeserializer)
		{
			object result;
			try
			{
				AsnReader asnReader = new AsnReader(defaultContents, AsnEncodingRules.DER);
				object obj = valueDeserializer(asnReader);
				if (asnReader.HasData)
				{
					throw new AsnSerializerInvalidDefaultException();
				}
				result = obj;
			}
			catch (AsnSerializerInvalidDefaultException)
			{
				throw;
			}
			catch (CryptographicException innerException)
			{
				throw new AsnSerializerInvalidDefaultException(innerException);
			}
			return result;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015E04 File Offset: 0x00014004
		private static void GetFieldInfo(Type typeT, FieldInfo fieldInfo, out AsnSerializer.SerializerFieldData serializerFieldData)
		{
			serializerFieldData = default(AsnSerializer.SerializerFieldData);
			object[] array = ((fieldInfo != null) ? fieldInfo.GetCustomAttributes(typeof(AsnTypeAttribute), false) : null) ?? Array.Empty<object>();
			if (array.Length > 1)
			{
				throw new AsnSerializationConstraintException(SR.Format(fieldInfo.Name, fieldInfo.DeclaringType.FullName, typeof(AsnTypeAttribute).FullName));
			}
			Type type = AsnSerializer.UnpackIfNullable(typeT);
			if (array.Length == 1)
			{
				object obj = array[0];
				serializerFieldData.WasCustomized = true;
				Type[] array2;
				if (obj is AnyValueAttribute)
				{
					serializerFieldData.IsAny = true;
					array2 = new Type[]
					{
						typeof(ReadOnlyMemory<byte>)
					};
				}
				else if (obj is IntegerAttribute)
				{
					array2 = new Type[]
					{
						typeof(ReadOnlyMemory<byte>)
					};
					serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Integer);
				}
				else if (obj is BitStringAttribute)
				{
					array2 = new Type[]
					{
						typeof(ReadOnlyMemory<byte>)
					};
					serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.BitString);
				}
				else if (obj is OctetStringAttribute)
				{
					array2 = new Type[]
					{
						typeof(ReadOnlyMemory<byte>)
					};
					serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.OctetString);
				}
				else
				{
					ObjectIdentifierAttribute objectIdentifierAttribute = obj as ObjectIdentifierAttribute;
					if (objectIdentifierAttribute != null)
					{
						serializerFieldData.PopulateOidFriendlyName = new bool?(objectIdentifierAttribute.PopulateFriendlyName);
						array2 = new Type[]
						{
							typeof(Oid),
							typeof(string)
						};
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.ObjectIdentifier);
						if (objectIdentifierAttribute.PopulateFriendlyName && type == typeof(string))
						{
							throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on type '{1}' has [ObjectIdentifier].PopulateFriendlyName set to true, which is not applicable to a string.  Change the field to '{2}' or set PopulateFriendlyName to false.", fieldInfo.Name, fieldInfo.DeclaringType.FullName, typeof(Oid).FullName));
						}
					}
					else if (obj is BMPStringAttribute)
					{
						array2 = new Type[]
						{
							typeof(string)
						};
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.BMPString);
					}
					else if (obj is IA5StringAttribute)
					{
						array2 = new Type[]
						{
							typeof(string)
						};
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.IA5String);
					}
					else if (obj is UTF8StringAttribute)
					{
						array2 = new Type[]
						{
							typeof(string)
						};
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.UTF8String);
					}
					else if (obj is PrintableStringAttribute)
					{
						array2 = new Type[]
						{
							typeof(string)
						};
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.PrintableString);
					}
					else if (obj is VisibleStringAttribute)
					{
						array2 = new Type[]
						{
							typeof(string)
						};
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.VisibleString);
					}
					else if (obj is SequenceOfAttribute)
					{
						serializerFieldData.IsCollection = true;
						array2 = null;
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Sequence);
					}
					else if (obj is SetOfAttribute)
					{
						serializerFieldData.IsCollection = true;
						array2 = null;
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Set);
					}
					else
					{
						UtcTimeAttribute utcTimeAttribute = obj as UtcTimeAttribute;
						if (utcTimeAttribute != null)
						{
							array2 = new Type[]
							{
								typeof(DateTimeOffset)
							};
							serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.UtcTime);
							if (utcTimeAttribute.TwoDigitYearMax != 0)
							{
								serializerFieldData.TwoDigitYearMax = new int?(utcTimeAttribute.TwoDigitYearMax);
								int? twoDigitYearMax = serializerFieldData.TwoDigitYearMax;
								int num = 99;
								if (twoDigitYearMax.GetValueOrDefault() < num & twoDigitYearMax != null)
								{
									throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on type '{1}' has a [UtcTime] TwoDigitYearMax value ({2}) smaller than the minimum (99).", fieldInfo.Name, fieldInfo.DeclaringType.FullName, serializerFieldData.TwoDigitYearMax));
								}
							}
						}
						else
						{
							GeneralizedTimeAttribute generalizedTimeAttribute = obj as GeneralizedTimeAttribute;
							if (generalizedTimeAttribute == null)
							{
								throw new CryptographicException();
							}
							array2 = new Type[]
							{
								typeof(DateTimeOffset)
							};
							serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.GeneralizedTime);
							serializerFieldData.DisallowGeneralizedTimeFractions = new bool?(generalizedTimeAttribute.DisallowFractions);
						}
					}
				}
				if (!serializerFieldData.IsCollection && Array.IndexOf<Type>(array2, type) < 0)
				{
					string resourceFormat = "Field '{0}' of type '{1}' has an effective type of '{2}' when one of ({3}) was expected.";
					object[] array3 = new object[4];
					array3[0] = fieldInfo.Name;
					array3[1] = fieldInfo.DeclaringType.Namespace;
					array3[2] = type.FullName;
					array3[3] = string.Join(", ", from t in array2
					select t.FullName);
					throw new AsnSerializationConstraintException(SR.Format(resourceFormat, array3));
				}
			}
			DefaultValueAttribute defaultValueAttribute = (fieldInfo != null) ? fieldInfo.GetCustomAttribute(false) : null;
			serializerFieldData.DefaultContents = ((defaultValueAttribute != null) ? defaultValueAttribute.EncodedBytes : null);
			if (serializerFieldData.TagType == null && !serializerFieldData.IsAny)
			{
				if (type == typeof(bool))
				{
					serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Boolean);
				}
				else if (type == typeof(sbyte) || type == typeof(byte) || type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong) || type == typeof(BigInteger))
				{
					serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Integer);
				}
				else if (type.IsLayoutSequential)
				{
					serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Sequence);
				}
				else
				{
					if (type == typeof(ReadOnlyMemory<byte>) || type == typeof(string) || type == typeof(DateTimeOffset))
					{
						throw new AsnAmbiguousFieldTypeException(fieldInfo, type);
					}
					if (type == typeof(Oid))
					{
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.ObjectIdentifier);
					}
					else if (type.IsArray)
					{
						serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Sequence);
					}
					else if (type.IsEnum)
					{
						if (typeT.GetCustomAttributes(typeof(FlagsAttribute), false).Length != 0)
						{
							serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.BitString);
						}
						else
						{
							serializerFieldData.TagType = new UniversalTagNumber?(UniversalTagNumber.Enumerated);
						}
					}
					else if (fieldInfo != null)
					{
						throw new AsnSerializationConstraintException();
					}
				}
			}
			serializerFieldData.IsOptional = (((fieldInfo != null) ? fieldInfo.GetCustomAttribute(false) : null) != null);
			if (serializerFieldData.IsOptional && !AsnSerializer.CanBeNull(typeT))
			{
				throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on type '{1}' is declared [OptionalValue], but it can not be assigned a null value.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
			}
			bool flag = AsnSerializer.GetChoiceAttribute(typeT) != null;
			ExpectedTagAttribute expectedTagAttribute = (fieldInfo != null) ? fieldInfo.GetCustomAttribute(false) : null;
			if (expectedTagAttribute == null)
			{
				if (flag)
				{
					serializerFieldData.TagType = null;
				}
				serializerFieldData.SpecifiedTag = false;
				serializerFieldData.HasExplicitTag = false;
				serializerFieldData.ExpectedTag = new Asn1Tag(serializerFieldData.TagType.GetValueOrDefault(), false);
				return;
			}
			if (flag && !expectedTagAttribute.ExplicitTag)
			{
				throw new AsnSerializationConstraintException(SR.Format("Field '{0}' on type '{1}' has specified an implicit tag value via [ExpectedTag] for [Choice] type '{2}'. ExplicitTag must be true, or the [ExpectedTag] attribute removed.", fieldInfo.Name, fieldInfo.DeclaringType.FullName, typeT.FullName));
			}
			serializerFieldData.ExpectedTag = new Asn1Tag(expectedTagAttribute.TagClass, expectedTagAttribute.TagValue, false);
			serializerFieldData.HasExplicitTag = expectedTagAttribute.ExplicitTag;
			serializerFieldData.SpecifiedTag = true;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001655F File Offset: 0x0001475F
		private static Type UnpackIfNullable(Type typeT)
		{
			return Nullable.GetUnderlyingType(typeT) ?? typeT;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001656C File Offset: 0x0001476C
		private static AsnSerializer.Deserializer GetPrimitiveDeserializer(Type typeT, Asn1Tag tag)
		{
			if (typeT == typeof(bool))
			{
				return (AsnReader reader) => reader.ReadBoolean(tag);
			}
			if (typeT == typeof(int))
			{
				return AsnSerializer.TryOrFail<int>(delegate(AsnReader reader, out int value)
				{
					return reader.TryReadInt32(tag, out value);
				});
			}
			if (typeT == typeof(uint))
			{
				return AsnSerializer.TryOrFail<uint>(delegate(AsnReader reader, out uint value)
				{
					return reader.TryReadUInt32(tag, out value);
				});
			}
			if (typeT == typeof(short))
			{
				return AsnSerializer.TryOrFail<short>(delegate(AsnReader reader, out short value)
				{
					return reader.TryReadInt16(tag, out value);
				});
			}
			if (typeT == typeof(ushort))
			{
				return AsnSerializer.TryOrFail<ushort>(delegate(AsnReader reader, out ushort value)
				{
					return reader.TryReadUInt16(tag, out value);
				});
			}
			if (typeT == typeof(byte))
			{
				return AsnSerializer.TryOrFail<byte>(delegate(AsnReader reader, out byte value)
				{
					return reader.TryReadUInt8(tag, out value);
				});
			}
			if (typeT == typeof(sbyte))
			{
				return AsnSerializer.TryOrFail<sbyte>(delegate(AsnReader reader, out sbyte value)
				{
					return reader.TryReadInt8(tag, out value);
				});
			}
			if (typeT == typeof(long))
			{
				return AsnSerializer.TryOrFail<long>(delegate(AsnReader reader, out long value)
				{
					return reader.TryReadInt64(tag, out value);
				});
			}
			if (typeT == typeof(ulong))
			{
				return AsnSerializer.TryOrFail<ulong>(delegate(AsnReader reader, out ulong value)
				{
					return reader.TryReadUInt64(tag, out value);
				});
			}
			throw new AsnSerializationConstraintException(SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000166DC File Offset: 0x000148DC
		private static AsnSerializer.Serializer GetPrimitiveSerializer(Type typeT, Asn1Tag primitiveTag)
		{
			if (typeT == typeof(bool))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteBoolean(primitiveTag, (bool)value);
				};
			}
			if (typeT == typeof(int))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)((int)value));
				};
			}
			if (typeT == typeof(uint))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)((ulong)((uint)value)));
				};
			}
			if (typeT == typeof(short))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)((short)value));
				};
			}
			if (typeT == typeof(ushort))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)((ulong)((ushort)value)));
				};
			}
			if (typeT == typeof(byte))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)((ulong)((byte)value)));
				};
			}
			if (typeT == typeof(sbyte))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)((sbyte)value));
				};
			}
			if (typeT == typeof(long))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)value);
				};
			}
			if (typeT == typeof(ulong))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (ulong)value);
				};
			}
			throw new AsnSerializationConstraintException(SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016824 File Offset: 0x00014A24
		public static T Deserialize<T>(ReadOnlyMemory<byte> source, AsnEncodingRules ruleSet)
		{
			AsnSerializer.Deserializer deserializer = AsnSerializer.GetDeserializer(typeof(T), null);
			AsnReader asnReader = new AsnReader(source, ruleSet);
			T result = (T)((object)deserializer(asnReader));
			asnReader.ThrowIfNotEmpty();
			return result;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001685C File Offset: 0x00014A5C
		public static T Deserialize<T>(ReadOnlyMemory<byte> source, AsnEncodingRules ruleSet, out int bytesRead)
		{
			AsnSerializer.Deserializer deserializer = AsnSerializer.GetDeserializer(typeof(T), null);
			AsnReader asnReader = new AsnReader(source, ruleSet);
			ReadOnlyMemory<byte> readOnlyMemory = asnReader.PeekEncodedValue();
			T result = (T)((object)deserializer(asnReader));
			bytesRead = readOnlyMemory.Length;
			return result;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001689C File Offset: 0x00014A9C
		public static AsnWriter Serialize<T>(T value, AsnEncodingRules ruleSet)
		{
			AsnWriter asnWriter = new AsnWriter(ruleSet);
			AsnWriter result;
			try
			{
				AsnSerializer.Serialize<T>(value, asnWriter);
				result = asnWriter;
			}
			catch
			{
				asnWriter.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000168D8 File Offset: 0x00014AD8
		public static void Serialize<T>(T value, AsnWriter existingWriter)
		{
			if (existingWriter == null)
			{
				throw new ArgumentNullException("existingWriter");
			}
			AsnSerializer.GetSerializer(typeof(T), null)(value, existingWriter);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016904 File Offset: 0x00014B04
		// Note: this type is marked as 'beforefieldinit'.
		static AsnSerializer()
		{
		}

		// Token: 0x04000388 RID: 904
		private const BindingFlags FieldFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000389 RID: 905
		private static readonly ConcurrentDictionary<Type, FieldInfo[]> s_orderedFields = new ConcurrentDictionary<Type, FieldInfo[]>();

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x06000543 RID: 1347
		private delegate void Serializer(object value, AsnWriter writer);

		// Token: 0x020000D0 RID: 208
		// (Invoke) Token: 0x06000547 RID: 1351
		private delegate object Deserializer(AsnReader reader);

		// Token: 0x020000D1 RID: 209
		// (Invoke) Token: 0x0600054B RID: 1355
		private delegate bool TryDeserializer<T>(AsnReader reader, out T value);

		// Token: 0x020000D2 RID: 210
		private struct SerializerFieldData
		{
			// Token: 0x0400038A RID: 906
			internal bool WasCustomized;

			// Token: 0x0400038B RID: 907
			internal UniversalTagNumber? TagType;

			// Token: 0x0400038C RID: 908
			internal bool? PopulateOidFriendlyName;

			// Token: 0x0400038D RID: 909
			internal bool IsAny;

			// Token: 0x0400038E RID: 910
			internal bool IsCollection;

			// Token: 0x0400038F RID: 911
			internal byte[] DefaultContents;

			// Token: 0x04000390 RID: 912
			internal bool HasExplicitTag;

			// Token: 0x04000391 RID: 913
			internal bool SpecifiedTag;

			// Token: 0x04000392 RID: 914
			internal bool IsOptional;

			// Token: 0x04000393 RID: 915
			internal int? TwoDigitYearMax;

			// Token: 0x04000394 RID: 916
			internal Asn1Tag ExpectedTag;

			// Token: 0x04000395 RID: 917
			internal bool? DisallowGeneralizedTimeFractions;
		}

		// Token: 0x020000D3 RID: 211
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0<T>
		{
			// Token: 0x0600054E RID: 1358 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x00016910 File Offset: 0x00014B10
			internal object <TryOrFail>b__0(AsnReader reader)
			{
				T t;
				if (this.tryDeserializer(reader, out t))
				{
					return t;
				}
				throw new CryptographicException("ASN1 corrupted data.");
			}

			// Token: 0x04000396 RID: 918
			public AsnSerializer.TryDeserializer<T> tryDeserializer;
		}

		// Token: 0x020000D4 RID: 212
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000550 RID: 1360 RVA: 0x0001693E File Offset: 0x00014B3E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x00002145 File Offset: 0x00000345
			public <>c()
			{
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x0001694C File Offset: 0x00014B4C
			internal FieldInfo[] <GetOrderedFields>b__6_0(Type t)
			{
				FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (fields.Length == 0)
				{
					return Array.Empty<FieldInfo>();
				}
				try
				{
					int metadataToken = fields[0].MetadataToken;
				}
				catch (InvalidOperationException)
				{
					return fields;
				}
				Array.Sort<FieldInfo>(fields, (FieldInfo x, FieldInfo y) => x.MetadataToken.CompareTo(y.MetadataToken));
				return fields;
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x000169B8 File Offset: 0x00014BB8
			internal int <GetOrderedFields>b__6_1(FieldInfo x, FieldInfo y)
			{
				return x.MetadataToken.CompareTo(y.MetadataToken);
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x000169D9 File Offset: 0x00014BD9
			internal void <GetSimpleSerializer>b__19_5(object value, AsnWriter writer)
			{
				writer.WriteEncodedValue((ReadOnlyMemory<byte>)value);
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x000169E7 File Offset: 0x00014BE7
			internal object <GetSimpleDeserializer>b__21_12(AsnReader reader)
			{
				return reader.GetEncodedValue();
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x000169F4 File Offset: 0x00014BF4
			internal string <GetFieldInfo>b__23_0(Type t)
			{
				return t.FullName;
			}

			// Token: 0x04000397 RID: 919
			public static readonly AsnSerializer.<>c <>9 = new AsnSerializer.<>c();

			// Token: 0x04000398 RID: 920
			public static Comparison<FieldInfo> <>9__6_1;

			// Token: 0x04000399 RID: 921
			public static Func<Type, FieldInfo[]> <>9__6_0;

			// Token: 0x0400039A RID: 922
			public static AsnSerializer.Serializer <>9__19_5;

			// Token: 0x0400039B RID: 923
			public static AsnSerializer.Deserializer <>9__21_12;

			// Token: 0x0400039C RID: 924
			public static Func<Type, string> <>9__23_0;
		}

		// Token: 0x020000D5 RID: 213
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06000557 RID: 1367 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x000169FC File Offset: 0x00014BFC
			internal object <ExplicitValueDeserializer>b__0(AsnReader reader)
			{
				return AsnSerializer.ExplicitValueDeserializer(reader, this.valueDeserializer, this.expectedTag);
			}

			// Token: 0x0400039D RID: 925
			public AsnSerializer.Deserializer valueDeserializer;

			// Token: 0x0400039E RID: 926
			public Asn1Tag expectedTag;
		}

		// Token: 0x020000D6 RID: 214
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x06000559 RID: 1369 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x00016A10 File Offset: 0x00014C10
			internal object <DefaultValueDeserializer>b__0(AsnReader reader)
			{
				return AsnSerializer.DefaultValueDeserializer(reader, this.expectedTag, this.valueDeserializer, this.defaultContents, this.isOptional);
			}

			// Token: 0x0400039F RID: 927
			public Asn1Tag? expectedTag;

			// Token: 0x040003A0 RID: 928
			public AsnSerializer.Deserializer valueDeserializer;

			// Token: 0x040003A1 RID: 929
			public byte[] defaultContents;

			// Token: 0x040003A2 RID: 930
			public bool isOptional;
		}

		// Token: 0x020000D7 RID: 215
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x0600055B RID: 1371 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x00016A30 File Offset: 0x00014C30
			internal void <GetSerializer>b__0(object obj, AsnWriter writer)
			{
				if (obj != null)
				{
					this.literalValueSerializer(obj, writer);
				}
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x00016A44 File Offset: 0x00014C44
			internal unsafe void <GetSerializer>b__1(object obj, AsnWriter writer)
			{
				AsnReader asnReader;
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
				{
					this.literalValueSerializer(obj, asnWriter);
					asnReader = new AsnReader(asnWriter.Encode(), AsnEncodingRules.DER);
				}
				ReadOnlySpan<byte> span = asnReader.GetEncodedValue().Span;
				bool flag = false;
				if (span.Length == this.defaultContents.Length)
				{
					flag = true;
					for (int i = 0; i < span.Length; i++)
					{
						if (*span[i] != this.defaultContents[i])
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					this.literalValueSerializer(obj, writer);
				}
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x00016AF8 File Offset: 0x00014CF8
			internal void <GetSerializer>b__2(object obj, AsnWriter writer)
			{
				using (AsnWriter asnWriter = new AsnWriter(writer.RuleSet))
				{
					this.serializer(obj, asnWriter);
					if (asnWriter.Encode().Length != 0)
					{
						writer.PushSequence(this.explicitTag.Value);
						this.serializer(obj, writer);
						writer.PopSequence(this.explicitTag.Value);
					}
				}
			}

			// Token: 0x040003A3 RID: 931
			public AsnSerializer.Serializer literalValueSerializer;

			// Token: 0x040003A4 RID: 932
			public byte[] defaultContents;

			// Token: 0x040003A5 RID: 933
			public AsnSerializer.Serializer serializer;

			// Token: 0x040003A6 RID: 934
			public Asn1Tag? explicitTag;
		}

		// Token: 0x020000D8 RID: 216
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x0600055F RID: 1375 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x00016B74 File Offset: 0x00014D74
			internal void <GetSimpleSerializer>b__0(object value, AsnWriter writer)
			{
				writer.WriteNamedBitList(this.tag, value);
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x00016B83 File Offset: 0x00014D83
			internal void <GetSimpleSerializer>b__1(object value, AsnWriter writer)
			{
				writer.WriteEnumeratedValue(this.tag, value);
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x00016B92 File Offset: 0x00014D92
			internal void <GetSimpleSerializer>b__2(object value, AsnWriter writer)
			{
				writer.WriteObjectIdentifier(this.tag, (string)value);
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x00016BA6 File Offset: 0x00014DA6
			internal void <GetSimpleSerializer>b__3(object value, AsnWriter writer)
			{
				writer.WriteCharacterString(this.tag, this.fieldData.TagType.Value, (string)value);
			}

			// Token: 0x06000564 RID: 1380 RVA: 0x00016BCC File Offset: 0x00014DCC
			internal void <GetSimpleSerializer>b__4(object value, AsnWriter writer)
			{
				ReadOnlyMemory<byte> preEncodedValue = (ReadOnlyMemory<byte>)value;
				Asn1Tag asn1Tag;
				int num;
				if (!Asn1Tag.TryParse(preEncodedValue.Span, out asn1Tag, out num) || asn1Tag.AsPrimitive() != this.fieldData.ExpectedTag.AsPrimitive())
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				writer.WriteEncodedValue(preEncodedValue);
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x00016C24 File Offset: 0x00014E24
			internal void <GetSimpleSerializer>b__6(object value, AsnWriter writer)
			{
				writer.WriteBitString(this.tag, ((ReadOnlyMemory<byte>)value).Span, 0);
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x00016C4C File Offset: 0x00014E4C
			internal void <GetSimpleSerializer>b__7(object value, AsnWriter writer)
			{
				writer.WriteOctetString(this.tag, ((ReadOnlyMemory<byte>)value).Span);
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00016C74 File Offset: 0x00014E74
			internal void <GetSimpleSerializer>b__8(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.tag, ((ReadOnlyMemory<byte>)value).Span);
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00016C9B File Offset: 0x00014E9B
			internal void <GetSimpleSerializer>b__9(object value, AsnWriter writer)
			{
				writer.WriteObjectIdentifier(this.fieldData.ExpectedTag, (Oid)value);
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x00016CB4 File Offset: 0x00014EB4
			internal void <GetSimpleSerializer>b__10(object value, AsnWriter writer)
			{
				writer.WriteUtcTime(this.tag, (DateTimeOffset)value);
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x00016CC8 File Offset: 0x00014EC8
			internal void <GetSimpleSerializer>b__11(object value, AsnWriter writer)
			{
				writer.WriteGeneralizedTime(this.tag, (DateTimeOffset)value, this.fieldData.DisallowGeneralizedTimeFractions.Value);
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00016CEC File Offset: 0x00014EEC
			internal void <GetSimpleSerializer>b__12(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.tag, (BigInteger)value);
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x00016D00 File Offset: 0x00014F00
			internal void <GetSimpleSerializer>b__13(object value, AsnWriter writer)
			{
				AsnSerializer.SerializeChoice(this.typeT, value, writer);
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x00016D0F File Offset: 0x00014F0F
			internal void <GetSimpleSerializer>b__14(object value, AsnWriter writer)
			{
				AsnSerializer.SerializeCustomType(this.typeT, value, writer, this.tag);
			}

			// Token: 0x040003A7 RID: 935
			public Asn1Tag tag;

			// Token: 0x040003A8 RID: 936
			public AsnSerializer.SerializerFieldData fieldData;

			// Token: 0x040003A9 RID: 937
			public Type typeT;
		}

		// Token: 0x020000D9 RID: 217
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_1
		{
			// Token: 0x0600056E RID: 1390 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass19_1()
			{
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x00016D24 File Offset: 0x00014F24
			internal void <GetSimpleSerializer>b__15(object value, AsnWriter writer)
			{
				writer.PushSetOf(this.CS$<>8__locals1.tag);
				foreach (object value2 in ((Array)value))
				{
					this.serializer(value2, writer);
				}
				writer.PopSetOf(this.CS$<>8__locals1.tag);
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x00016DA0 File Offset: 0x00014FA0
			internal void <GetSimpleSerializer>b__16(object value, AsnWriter writer)
			{
				writer.PushSequence(this.CS$<>8__locals1.tag);
				foreach (object value2 in ((Array)value))
				{
					this.serializer(value2, writer);
				}
				writer.PopSequence(this.CS$<>8__locals1.tag);
			}

			// Token: 0x040003AA RID: 938
			public AsnSerializer.Serializer serializer;

			// Token: 0x040003AB RID: 939
			public AsnSerializer.<>c__DisplayClass19_0 CS$<>8__locals1;
		}

		// Token: 0x020000DA RID: 218
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x06000571 RID: 1393 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x00016E1C File Offset: 0x0001501C
			internal object <GetSimpleDeserializer>b__0(AsnReader reader)
			{
				return AsnSerializer.DeserializeChoice(reader, this.typeT);
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x00016E2A File Offset: 0x0001502A
			internal object <GetSimpleDeserializer>b__1(AsnReader reader)
			{
				return reader.GetNamedBitListValue(this.expectedTag, this.typeT);
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00016E3E File Offset: 0x0001503E
			internal object <GetSimpleDeserializer>b__2(AsnReader reader)
			{
				return reader.GetEnumeratedValue(this.expectedTag, this.typeT);
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x00016E52 File Offset: 0x00015052
			internal object <GetSimpleDeserializer>b__3(AsnReader reader)
			{
				return reader.ReadObjectIdentifierAsString(this.expectedTag);
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x00016E60 File Offset: 0x00015060
			internal object <GetSimpleDeserializer>b__4(AsnReader reader)
			{
				return reader.GetCharacterString(this.expectedTag, this.localFieldData.TagType.Value);
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00016E80 File Offset: 0x00015080
			internal object <GetSimpleDeserializer>b__5(AsnReader reader)
			{
				int num;
				ReadOnlyMemory<byte> readOnlyMemory;
				if (reader.TryGetPrimitiveBitStringValue(this.expectedTag, out num, out readOnlyMemory))
				{
					return readOnlyMemory;
				}
				int length = reader.PeekEncodedValue().Length;
				byte[] array = ArrayPool<byte>.Shared.Rent(length);
				object result;
				try
				{
					int length2;
					if (!reader.TryCopyBitStringBytes(this.expectedTag, array, out num, out length2))
					{
						throw new CryptographicException();
					}
					result = new ReadOnlyMemory<byte>(array.AsSpan(0, length2).ToArray());
				}
				finally
				{
					Array.Clear(array, 0, length);
					ArrayPool<byte>.Shared.Return(array, false);
				}
				return result;
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x00016F28 File Offset: 0x00015128
			internal object <GetSimpleDeserializer>b__6(AsnReader reader)
			{
				ReadOnlyMemory<byte> readOnlyMemory;
				if (reader.TryGetPrimitiveOctetStringBytes(this.expectedTag, out readOnlyMemory))
				{
					return readOnlyMemory;
				}
				int length = reader.PeekEncodedValue().Length;
				byte[] array = ArrayPool<byte>.Shared.Rent(length);
				object result;
				try
				{
					int length2;
					if (!reader.TryCopyOctetStringBytes(this.expectedTag, array, out length2))
					{
						throw new CryptographicException();
					}
					result = new ReadOnlyMemory<byte>(array.AsSpan(0, length2).ToArray());
				}
				finally
				{
					Array.Clear(array, 0, length);
					ArrayPool<byte>.Shared.Return(array, false);
				}
				return result;
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x00016FCC File Offset: 0x000151CC
			internal object <GetSimpleDeserializer>b__7(AsnReader reader)
			{
				return reader.GetIntegerBytes(this.expectedTag);
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x00016FDF File Offset: 0x000151DF
			internal object <GetSimpleDeserializer>b__8(AsnReader reader)
			{
				return reader.GetUtcTime(this.expectedTag, this.localFieldData.TwoDigitYearMax.Value);
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x00017002 File Offset: 0x00015202
			internal object <GetSimpleDeserializer>b__9(AsnReader reader)
			{
				return reader.GetUtcTime(this.expectedTag, 2049);
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x0001701A File Offset: 0x0001521A
			internal object <GetSimpleDeserializer>b__10(AsnReader reader)
			{
				return reader.GetInteger(this.expectedTag);
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x0001702D File Offset: 0x0001522D
			internal object <GetSimpleDeserializer>b__11(AsnReader reader)
			{
				return AsnSerializer.DeserializeCustomType(reader, this.typeT, this.expectedTag);
			}

			// Token: 0x040003AC RID: 940
			public Type typeT;

			// Token: 0x040003AD RID: 941
			public Asn1Tag expectedTag;

			// Token: 0x040003AE RID: 942
			public AsnSerializer.SerializerFieldData localFieldData;
		}

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_1
		{
			// Token: 0x0600057E RID: 1406 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass21_1()
			{
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x00017044 File Offset: 0x00015244
			internal object <GetSimpleDeserializer>b__13(AsnReader reader)
			{
				Asn1Tag asn1Tag = reader.PeekTag();
				if (this.matchTag.TagClass != asn1Tag.TagClass || this.matchTag.TagValue != asn1Tag.TagValue)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return reader.GetEncodedValue();
			}

			// Token: 0x040003AF RID: 943
			public Asn1Tag matchTag;
		}

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_2
		{
			// Token: 0x06000580 RID: 1408 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass21_2()
			{
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00017096 File Offset: 0x00015296
			internal object <GetSimpleDeserializer>b__14(AsnReader reader)
			{
				return reader.ReadObjectIdentifier(this.CS$<>8__locals1.expectedTag, this.skipFriendlyName);
			}

			// Token: 0x040003B0 RID: 944
			public bool skipFriendlyName;

			// Token: 0x040003B1 RID: 945
			public AsnSerializer.<>c__DisplayClass21_0 CS$<>8__locals1;
		}

		// Token: 0x020000DD RID: 221
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_3
		{
			// Token: 0x06000582 RID: 1410 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass21_3()
			{
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x000170B0 File Offset: 0x000152B0
			internal object <GetSimpleDeserializer>b__15(AsnReader reader)
			{
				LinkedList<object> linkedList = new LinkedList<object>();
				UniversalTagNumber? tagType = this.CS$<>8__locals2.localFieldData.TagType;
				UniversalTagNumber universalTagNumber = UniversalTagNumber.Set;
				AsnReader asnReader;
				if (tagType.GetValueOrDefault() == universalTagNumber & tagType != null)
				{
					asnReader = reader.ReadSetOf(this.CS$<>8__locals2.expectedTag, false);
				}
				else
				{
					asnReader = reader.ReadSequence(this.CS$<>8__locals2.expectedTag);
				}
				AsnSerializer.Deserializer deserializer = AsnSerializer.GetDeserializer(this.baseType, null);
				while (asnReader.HasData)
				{
					LinkedListNode<object> node = new LinkedListNode<object>(deserializer(asnReader));
					linkedList.AddLast(node);
				}
				object[] array = linkedList.ToArray<object>();
				Array array2 = Array.CreateInstance(this.baseType, array.Length);
				Array.Copy(array, array2, array.Length);
				return array2;
			}

			// Token: 0x040003B2 RID: 946
			public Type baseType;

			// Token: 0x040003B3 RID: 947
			public AsnSerializer.<>c__DisplayClass21_0 CS$<>8__locals2;
		}

		// Token: 0x020000DE RID: 222
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_4
		{
			// Token: 0x06000584 RID: 1412 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass21_4()
			{
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x00017166 File Offset: 0x00015366
			internal object <GetSimpleDeserializer>b__16(AsnReader reader)
			{
				return reader.GetGeneralizedTime(this.CS$<>8__locals3.expectedTag, this.disallowFractions);
			}

			// Token: 0x040003B4 RID: 948
			public bool disallowFractions;

			// Token: 0x040003B5 RID: 949
			public AsnSerializer.<>c__DisplayClass21_0 CS$<>8__locals3;
		}

		// Token: 0x020000DF RID: 223
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x06000586 RID: 1414 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x00017184 File Offset: 0x00015384
			internal object <GetPrimitiveDeserializer>b__0(AsnReader reader)
			{
				return reader.ReadBoolean(this.tag);
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x00017197 File Offset: 0x00015397
			internal bool <GetPrimitiveDeserializer>b__1(AsnReader reader, out int value)
			{
				return reader.TryReadInt32(this.tag, out value);
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x000171A6 File Offset: 0x000153A6
			internal bool <GetPrimitiveDeserializer>b__2(AsnReader reader, out uint value)
			{
				return reader.TryReadUInt32(this.tag, out value);
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x000171B5 File Offset: 0x000153B5
			internal bool <GetPrimitiveDeserializer>b__3(AsnReader reader, out short value)
			{
				return reader.TryReadInt16(this.tag, out value);
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x000171C4 File Offset: 0x000153C4
			internal bool <GetPrimitiveDeserializer>b__4(AsnReader reader, out ushort value)
			{
				return reader.TryReadUInt16(this.tag, out value);
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x000171D3 File Offset: 0x000153D3
			internal bool <GetPrimitiveDeserializer>b__5(AsnReader reader, out byte value)
			{
				return reader.TryReadUInt8(this.tag, out value);
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x000171E2 File Offset: 0x000153E2
			internal bool <GetPrimitiveDeserializer>b__6(AsnReader reader, out sbyte value)
			{
				return reader.TryReadInt8(this.tag, out value);
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x000171F1 File Offset: 0x000153F1
			internal bool <GetPrimitiveDeserializer>b__7(AsnReader reader, out long value)
			{
				return reader.TryReadInt64(this.tag, out value);
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00017200 File Offset: 0x00015400
			internal bool <GetPrimitiveDeserializer>b__8(AsnReader reader, out ulong value)
			{
				return reader.TryReadUInt64(this.tag, out value);
			}

			// Token: 0x040003B6 RID: 950
			public Asn1Tag tag;
		}

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_0
		{
			// Token: 0x06000590 RID: 1424 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass26_0()
			{
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0001720F File Offset: 0x0001540F
			internal void <GetPrimitiveSerializer>b__0(object value, AsnWriter writer)
			{
				writer.WriteBoolean(this.primitiveTag, (bool)value);
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x00017223 File Offset: 0x00015423
			internal void <GetPrimitiveSerializer>b__1(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)((int)value));
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x00017238 File Offset: 0x00015438
			internal void <GetPrimitiveSerializer>b__2(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)((ulong)((uint)value)));
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0001724D File Offset: 0x0001544D
			internal void <GetPrimitiveSerializer>b__3(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)((short)value));
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x00017262 File Offset: 0x00015462
			internal void <GetPrimitiveSerializer>b__4(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)((ulong)((ushort)value)));
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x00017277 File Offset: 0x00015477
			internal void <GetPrimitiveSerializer>b__5(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)((ulong)((byte)value)));
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0001728C File Offset: 0x0001548C
			internal void <GetPrimitiveSerializer>b__6(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)((sbyte)value));
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x000172A1 File Offset: 0x000154A1
			internal void <GetPrimitiveSerializer>b__7(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (long)value);
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x000172B5 File Offset: 0x000154B5
			internal void <GetPrimitiveSerializer>b__8(object value, AsnWriter writer)
			{
				writer.WriteInteger(this.primitiveTag, (ulong)value);
			}

			// Token: 0x040003B7 RID: 951
			public Asn1Tag primitiveTag;
		}
	}
}
