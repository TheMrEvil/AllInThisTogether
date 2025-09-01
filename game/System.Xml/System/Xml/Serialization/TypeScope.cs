using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002BE RID: 702
	internal class TypeScope
	{
		// Token: 0x06001AB2 RID: 6834 RVA: 0x00099610 File Offset: 0x00097810
		static TypeScope()
		{
			TypeScope.AddPrimitive(typeof(string), "string", "String", (TypeFlags)2106);
			TypeScope.AddPrimitive(typeof(int), "int", "Int32", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(bool), "boolean", "Boolean", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(short), "short", "Int16", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(long), "long", "Int64", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(float), "float", "Single", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(double), "double", "Double", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(decimal), "decimal", "Decimal", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(DateTime), "dateTime", "DateTime", (TypeFlags)4200);
			TypeScope.AddPrimitive(typeof(XmlQualifiedName), "QName", "XmlQualifiedName", (TypeFlags)5226);
			TypeScope.AddPrimitive(typeof(byte), "unsignedByte", "Byte", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(sbyte), "byte", "SByte", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(ushort), "unsignedShort", "UInt16", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(uint), "unsignedInt", "UInt32", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(ulong), "unsignedLong", "UInt64", (TypeFlags)4136);
			TypeScope.AddPrimitive(typeof(DateTime), "date", "Date", (TypeFlags)4328);
			TypeScope.AddPrimitive(typeof(DateTime), "time", "Time", (TypeFlags)4328);
			TypeScope.AddPrimitive(typeof(string), "Name", "XmlName", (TypeFlags)234);
			TypeScope.AddPrimitive(typeof(string), "NCName", "XmlNCName", (TypeFlags)234);
			TypeScope.AddPrimitive(typeof(string), "NMTOKEN", "XmlNmToken", (TypeFlags)234);
			TypeScope.AddPrimitive(typeof(string), "NMTOKENS", "XmlNmTokens", (TypeFlags)234);
			TypeScope.AddPrimitive(typeof(byte[]), "base64Binary", "ByteArrayBase64", (TypeFlags)6890);
			TypeScope.AddPrimitive(typeof(byte[]), "hexBinary", "ByteArrayHex", (TypeFlags)6890);
			XmlSchemaPatternFacet xmlSchemaPatternFacet = new XmlSchemaPatternFacet();
			xmlSchemaPatternFacet.Value = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
			TypeScope.AddNonXsdPrimitive(typeof(Guid), "guid", "http://microsoft.com/wsdl/types/", "Guid", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), new XmlSchemaFacet[]
			{
				xmlSchemaPatternFacet
			}, (TypeFlags)4648);
			TypeScope.AddNonXsdPrimitive(typeof(char), "char", "http://microsoft.com/wsdl/types/", "Char", new XmlQualifiedName("unsignedShort", "http://www.w3.org/2001/XMLSchema"), new XmlSchemaFacet[0], (TypeFlags)616);
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				TypeScope.AddNonXsdPrimitive(typeof(TimeSpan), "TimeSpan", "http://microsoft.com/wsdl/types/", "TimeSpan", new XmlQualifiedName("duration", "http://www.w3.org/2001/XMLSchema"), new XmlSchemaFacet[0], (TypeFlags)4136);
			}
			TypeScope.AddSoapEncodedTypes("http://schemas.xmlsoap.org/soap/encoding/");
			TypeScope.AddPrimitive(typeof(string), "normalizedString", "String", (TypeFlags)2234);
			for (int i = 0; i < TypeScope.unsupportedTypes.Length; i++)
			{
				TypeScope.AddPrimitive(typeof(string), TypeScope.unsupportedTypes[i], "String", (TypeFlags)32954);
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00099AC4 File Offset: 0x00097CC4
		internal static bool IsKnownType(Type type)
		{
			if (type == typeof(object))
			{
				return true;
			}
			if (type.IsEnum)
			{
				return false;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return true;
			case TypeCode.Char:
				return true;
			case TypeCode.SByte:
				return true;
			case TypeCode.Byte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt64:
				return true;
			case TypeCode.Single:
				return true;
			case TypeCode.Double:
				return true;
			case TypeCode.Decimal:
				return true;
			case TypeCode.DateTime:
				return true;
			case TypeCode.String:
				return true;
			}
			return type == typeof(XmlQualifiedName) || type == typeof(byte[]) || type == typeof(Guid) || (LocalAppContextSwitches.EnableTimeSpanSerialization && type == typeof(TimeSpan)) || type == typeof(XmlNode[]);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00099BCC File Offset: 0x00097DCC
		private static void AddSoapEncodedTypes(string ns)
		{
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "normalizedString", ns, "String", new XmlQualifiedName("normalizedString", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)2218);
			for (int i = 0; i < TypeScope.unsupportedTypes.Length; i++)
			{
				TypeScope.AddSoapEncodedPrimitive(typeof(string), TypeScope.unsupportedTypes[i], ns, "String", new XmlQualifiedName(TypeScope.unsupportedTypes[i], "http://www.w3.org/2001/XMLSchema"), (TypeFlags)32938);
			}
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "string", ns, "String", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)58);
			TypeScope.AddSoapEncodedPrimitive(typeof(int), "int", ns, "Int32", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(bool), "boolean", ns, "Boolean", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(short), "short", ns, "Int16", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(long), "long", ns, "Int64", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(float), "float", ns, "Single", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(double), "double", ns, "Double", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(decimal), "decimal", ns, "Decimal", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(DateTime), "dateTime", ns, "DateTime", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4200);
			TypeScope.AddSoapEncodedPrimitive(typeof(XmlQualifiedName), "QName", ns, "XmlQualifiedName", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)5226);
			TypeScope.AddSoapEncodedPrimitive(typeof(byte), "unsignedByte", ns, "Byte", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(sbyte), "byte", ns, "SByte", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(ushort), "unsignedShort", ns, "UInt16", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(uint), "unsignedInt", ns, "UInt32", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(ulong), "unsignedLong", ns, "UInt64", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4136);
			TypeScope.AddSoapEncodedPrimitive(typeof(DateTime), "date", ns, "Date", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4328);
			TypeScope.AddSoapEncodedPrimitive(typeof(DateTime), "time", ns, "Time", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4328);
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "Name", ns, "XmlName", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)234);
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "NCName", ns, "XmlNCName", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)234);
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "NMTOKEN", ns, "XmlNmToken", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)234);
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "NMTOKENS", ns, "XmlNmTokens", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)234);
			TypeScope.AddSoapEncodedPrimitive(typeof(byte[]), "base64Binary", ns, "ByteArrayBase64", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4842);
			TypeScope.AddSoapEncodedPrimitive(typeof(byte[]), "hexBinary", ns, "ByteArrayHex", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)4842);
			TypeScope.AddSoapEncodedPrimitive(typeof(string), "arrayCoordinate", ns, "String", new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)40);
			TypeScope.AddSoapEncodedPrimitive(typeof(byte[]), "base64", ns, "ByteArrayBase64", new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema"), (TypeFlags)554);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0009A0C4 File Offset: 0x000982C4
		private static void AddPrimitive(Type type, string dataTypeName, string formatterName, TypeFlags flags)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = dataTypeName;
			TypeDesc value = new TypeDesc(type, true, xmlSchemaSimpleType, formatterName, flags);
			if (TypeScope.primitiveTypes[type] == null)
			{
				TypeScope.primitiveTypes.Add(type, value);
			}
			TypeScope.primitiveDataTypes.Add(xmlSchemaSimpleType, value);
			TypeScope.primitiveNames.Add(dataTypeName, "http://www.w3.org/2001/XMLSchema", value);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0009A120 File Offset: 0x00098320
		private static void AddNonXsdPrimitive(Type type, string dataTypeName, string ns, string formatterName, XmlQualifiedName baseTypeName, XmlSchemaFacet[] facets, TypeFlags flags)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = dataTypeName;
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = baseTypeName;
			foreach (XmlSchemaFacet item in facets)
			{
				xmlSchemaSimpleTypeRestriction.Facets.Add(item);
			}
			xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
			TypeDesc value = new TypeDesc(type, false, xmlSchemaSimpleType, formatterName, flags);
			if (TypeScope.primitiveTypes[type] == null)
			{
				TypeScope.primitiveTypes.Add(type, value);
			}
			TypeScope.primitiveDataTypes.Add(xmlSchemaSimpleType, value);
			TypeScope.primitiveNames.Add(dataTypeName, ns, value);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0009A1B6 File Offset: 0x000983B6
		private static void AddSoapEncodedPrimitive(Type type, string dataTypeName, string ns, string formatterName, XmlQualifiedName baseTypeName, TypeFlags flags)
		{
			TypeScope.AddNonXsdPrimitive(type, dataTypeName, ns, formatterName, baseTypeName, new XmlSchemaFacet[0], flags);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0009A1CB File Offset: 0x000983CB
		internal TypeDesc GetTypeDesc(string name, string ns)
		{
			return this.GetTypeDesc(name, ns, (TypeFlags)56);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0009A1D8 File Offset: 0x000983D8
		internal TypeDesc GetTypeDesc(string name, string ns, TypeFlags flags)
		{
			TypeDesc typeDesc = (TypeDesc)TypeScope.primitiveNames[name, ns];
			if (typeDesc != null && (typeDesc.Flags & flags) != TypeFlags.None)
			{
				return typeDesc;
			}
			return null;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0009A207 File Offset: 0x00098407
		internal TypeDesc GetTypeDesc(XmlSchemaSimpleType dataType)
		{
			return (TypeDesc)TypeScope.primitiveDataTypes[dataType];
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0009A219 File Offset: 0x00098419
		internal TypeDesc GetTypeDesc(Type type)
		{
			return this.GetTypeDesc(type, null, true, true);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0009A225 File Offset: 0x00098425
		internal TypeDesc GetTypeDesc(Type type, MemberInfo source)
		{
			return this.GetTypeDesc(type, source, true, true);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0009A231 File Offset: 0x00098431
		internal TypeDesc GetTypeDesc(Type type, MemberInfo source, bool directReference)
		{
			return this.GetTypeDesc(type, source, directReference, true);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0009A240 File Offset: 0x00098440
		internal TypeDesc GetTypeDesc(Type type, MemberInfo source, bool directReference, bool throwOnError)
		{
			if (type.ContainsGenericParameters)
			{
				throw new InvalidOperationException(Res.GetString("Type {0} is not supported because it has unbound generic parameters.  Only instantiated generic types can be serialized.", new object[]
				{
					type.ToString()
				}));
			}
			TypeDesc typeDesc = (TypeDesc)TypeScope.primitiveTypes[type];
			if (typeDesc == null)
			{
				typeDesc = (TypeDesc)this.typeDescs[type];
				if (typeDesc == null)
				{
					typeDesc = this.ImportTypeDesc(type, source, directReference);
				}
			}
			if (throwOnError)
			{
				typeDesc.CheckSupported();
			}
			return typeDesc;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0009A2B4 File Offset: 0x000984B4
		internal TypeDesc GetArrayTypeDesc(Type type)
		{
			TypeDesc typeDesc = (TypeDesc)this.arrayTypeDescs[type];
			if (typeDesc == null)
			{
				typeDesc = this.GetTypeDesc(type);
				if (!typeDesc.IsArrayLike)
				{
					typeDesc = this.ImportTypeDesc(type, null, false);
				}
				typeDesc.CheckSupported();
				this.arrayTypeDescs.Add(type, typeDesc);
			}
			return typeDesc;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009A304 File Offset: 0x00098504
		internal TypeMapping GetTypeMappingFromTypeDesc(TypeDesc typeDesc)
		{
			foreach (object obj in this.TypeMappings)
			{
				TypeMapping typeMapping = (TypeMapping)obj;
				if (typeMapping.TypeDesc == typeDesc)
				{
					return typeMapping;
				}
			}
			return null;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0009A368 File Offset: 0x00098568
		internal Type GetTypeFromTypeDesc(TypeDesc typeDesc)
		{
			if (typeDesc.Type != null)
			{
				return typeDesc.Type;
			}
			foreach (object obj in this.typeDescs)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (dictionaryEntry.Value == typeDesc)
				{
					return dictionaryEntry.Key as Type;
				}
			}
			return null;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0009A3EC File Offset: 0x000985EC
		private TypeDesc ImportTypeDesc(Type type, MemberInfo memberInfo, bool directReference)
		{
			Type type2 = null;
			Type type3 = null;
			TypeFlags typeFlags = TypeFlags.None;
			Exception ex = null;
			if (!type.IsPublic && !type.IsNestedPublic)
			{
				typeFlags |= TypeFlags.Unsupported;
				ex = new InvalidOperationException(Res.GetString("{0} is inaccessible due to its protection level. Only public types can be processed.", new object[]
				{
					type.FullName
				}));
			}
			else if (directReference && type.IsAbstract && type.IsSealed)
			{
				typeFlags |= TypeFlags.Unsupported;
				ex = new InvalidOperationException(Res.GetString("{0} cannot be serialized. Static types cannot be used as parameters or return types.", new object[]
				{
					type.FullName
				}));
			}
			if (DynamicAssemblies.IsTypeDynamic(type))
			{
				typeFlags |= TypeFlags.UseReflection;
			}
			if (!type.IsValueType)
			{
				typeFlags |= TypeFlags.Reference;
			}
			TypeKind typeKind;
			if (type == typeof(object))
			{
				typeKind = TypeKind.Root;
				typeFlags |= TypeFlags.HasDefaultConstructor;
			}
			else if (type == typeof(ValueType))
			{
				typeKind = TypeKind.Enum;
				typeFlags |= TypeFlags.Unsupported;
				if (ex == null)
				{
					ex = new NotSupportedException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[]
					{
						type.FullName
					}));
				}
			}
			else if (type == typeof(void))
			{
				typeKind = TypeKind.Void;
			}
			else if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				typeKind = TypeKind.Serializable;
				typeFlags |= (TypeFlags)36;
				typeFlags |= TypeScope.GetConstructorFlags(type, ref ex);
			}
			else if (type.IsArray)
			{
				typeKind = TypeKind.Array;
				if (type.GetArrayRank() > 1)
				{
					typeFlags |= TypeFlags.Unsupported;
					if (ex == null)
					{
						ex = new NotSupportedException(Res.GetString("Cannot serialize object of type {0}. Multidimensional arrays are not supported.", new object[]
						{
							type.FullName
						}));
					}
				}
				type2 = type.GetElementType();
				typeFlags |= TypeFlags.HasDefaultConstructor;
			}
			else if (typeof(ICollection).IsAssignableFrom(type) && !TypeScope.IsArraySegment(type))
			{
				typeKind = TypeKind.Collection;
				type2 = TypeScope.GetCollectionElementType(type, (memberInfo == null) ? null : (memberInfo.DeclaringType.FullName + "." + memberInfo.Name));
				typeFlags |= TypeScope.GetConstructorFlags(type, ref ex);
			}
			else if (type == typeof(XmlQualifiedName))
			{
				typeKind = TypeKind.Primitive;
			}
			else if (type.IsPrimitive)
			{
				typeKind = TypeKind.Primitive;
				typeFlags |= TypeFlags.Unsupported;
				if (ex == null)
				{
					ex = new NotSupportedException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[]
					{
						type.FullName
					}));
				}
			}
			else if (type.IsEnum)
			{
				typeKind = TypeKind.Enum;
			}
			else if (type.IsValueType)
			{
				typeKind = TypeKind.Struct;
				if (TypeScope.IsOptionalValue(type))
				{
					type3 = type.GetGenericArguments()[0];
					typeFlags |= TypeFlags.OptionalValue;
				}
				else
				{
					type3 = type.BaseType;
				}
				if (type.IsAbstract)
				{
					typeFlags |= TypeFlags.Abstract;
				}
			}
			else if (type.IsClass)
			{
				if (type == typeof(XmlAttribute))
				{
					typeKind = TypeKind.Attribute;
					typeFlags |= (TypeFlags)12;
				}
				else if (typeof(XmlNode).IsAssignableFrom(type))
				{
					typeKind = TypeKind.Node;
					type3 = type.BaseType;
					typeFlags |= (TypeFlags)52;
					if (typeof(XmlText).IsAssignableFrom(type))
					{
						typeFlags &= (TypeFlags)(-33);
					}
					else if (typeof(XmlElement).IsAssignableFrom(type))
					{
						typeFlags &= (TypeFlags)(-17);
					}
					else if (type.IsAssignableFrom(typeof(XmlAttribute)))
					{
						typeFlags |= TypeFlags.CanBeAttributeValue;
					}
				}
				else
				{
					typeKind = TypeKind.Class;
					type3 = type.BaseType;
					if (type.IsAbstract)
					{
						typeFlags |= TypeFlags.Abstract;
					}
				}
			}
			else if (type.IsInterface)
			{
				typeKind = TypeKind.Void;
				typeFlags |= TypeFlags.Unsupported;
				if (ex == null)
				{
					if (memberInfo == null)
					{
						ex = new NotSupportedException(Res.GetString("Cannot serialize interface {0}.", new object[]
						{
							type.FullName
						}));
					}
					else
					{
						ex = new NotSupportedException(Res.GetString("Cannot serialize member {0} of type {1} because it is an interface.", new object[]
						{
							memberInfo.DeclaringType.FullName + "." + memberInfo.Name,
							type.FullName
						}));
					}
				}
			}
			else
			{
				typeKind = TypeKind.Void;
				typeFlags |= TypeFlags.Unsupported;
				if (ex == null)
				{
					ex = new NotSupportedException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[]
					{
						type.FullName
					}));
				}
			}
			if (typeKind == TypeKind.Class && !type.IsAbstract)
			{
				typeFlags |= TypeScope.GetConstructorFlags(type, ref ex);
			}
			if ((typeKind == TypeKind.Struct || typeKind == TypeKind.Class) && typeof(IEnumerable).IsAssignableFrom(type) && !TypeScope.IsArraySegment(type))
			{
				type2 = TypeScope.GetEnumeratorElementType(type, ref typeFlags);
				typeKind = TypeKind.Enumerable;
				typeFlags |= TypeScope.GetConstructorFlags(type, ref ex);
			}
			TypeDesc typeDesc = new TypeDesc(type, CodeIdentifier.MakeValid(TypeScope.TypeName(type)), type.ToString(), typeKind, null, typeFlags, null);
			typeDesc.Exception = ex;
			if (directReference && (typeDesc.IsClass || typeKind == TypeKind.Serializable))
			{
				typeDesc.CheckNeedConstructor();
			}
			if (typeDesc.IsUnsupported)
			{
				return typeDesc;
			}
			this.typeDescs.Add(type, typeDesc);
			if (type2 != null)
			{
				TypeDesc typeDesc2 = this.GetTypeDesc(type2, memberInfo, true, false);
				if (directReference && (typeDesc2.IsCollection || typeDesc2.IsEnumerable) && !typeDesc2.IsPrimitive)
				{
					typeDesc2.CheckNeedConstructor();
				}
				typeDesc.ArrayElementTypeDesc = typeDesc2;
			}
			if (type3 != null && type3 != typeof(object) && type3 != typeof(ValueType))
			{
				typeDesc.BaseTypeDesc = this.GetTypeDesc(type3, memberInfo, false, false);
			}
			if (type.IsNestedPublic)
			{
				Type declaringType = type.DeclaringType;
				while (declaringType != null && !declaringType.ContainsGenericParameters && (!declaringType.IsAbstract || !declaringType.IsSealed))
				{
					this.GetTypeDesc(declaringType, null, false);
					declaringType = declaringType.DeclaringType;
				}
			}
			return typeDesc;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0009A9A1 File Offset: 0x00098BA1
		private static bool IsArraySegment(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ArraySegment<>);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0009A9C2 File Offset: 0x00098BC2
		internal static bool IsOptionalValue(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>).GetGenericTypeDefinition();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0009A9EC File Offset: 0x00098BEC
		internal static string TypeName(Type t)
		{
			if (t.IsArray)
			{
				return "ArrayOf" + TypeScope.TypeName(t.GetElementType());
			}
			if (t.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				string text = t.Name;
				int num = text.IndexOf("`", StringComparison.Ordinal);
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
				stringBuilder.Append(text);
				stringBuilder.Append("Of");
				Type[] genericArguments = t.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					stringBuilder.Append(TypeScope.TypeName(genericArguments[i]));
					stringBuilder2.Append(genericArguments[i].Namespace);
				}
				return stringBuilder.ToString();
			}
			return t.Name;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0009AAB0 File Offset: 0x00098CB0
		internal static Type GetArrayElementType(Type type, string memberInfo)
		{
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			if (TypeScope.IsArraySegment(type))
			{
				return null;
			}
			if (typeof(ICollection).IsAssignableFrom(type))
			{
				return TypeScope.GetCollectionElementType(type, memberInfo);
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				TypeFlags typeFlags = TypeFlags.None;
				return TypeScope.GetEnumeratorElementType(type, ref typeFlags);
			}
			return null;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0009AB10 File Offset: 0x00098D10
		internal static MemberMapping[] GetAllMembers(StructMapping mapping)
		{
			if (mapping.BaseMapping == null)
			{
				return mapping.Members;
			}
			ArrayList arrayList = new ArrayList();
			TypeScope.GetAllMembers(mapping, arrayList);
			return (MemberMapping[])arrayList.ToArray(typeof(MemberMapping));
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0009AB50 File Offset: 0x00098D50
		internal static void GetAllMembers(StructMapping mapping, ArrayList list)
		{
			if (mapping.BaseMapping != null)
			{
				TypeScope.GetAllMembers(mapping.BaseMapping, list);
			}
			for (int i = 0; i < mapping.Members.Length; i++)
			{
				list.Add(mapping.Members[i]);
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0009AB94 File Offset: 0x00098D94
		internal static MemberMapping[] GetAllMembers(StructMapping mapping, Dictionary<string, MemberInfo> memberInfos)
		{
			MemberMapping[] allMembers = TypeScope.GetAllMembers(mapping);
			TypeScope.PopulateMemberInfos(mapping, allMembers, memberInfos);
			return allMembers;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0009ABB4 File Offset: 0x00098DB4
		internal static MemberMapping[] GetSettableMembers(StructMapping structMapping)
		{
			ArrayList arrayList = new ArrayList();
			TypeScope.GetSettableMembers(structMapping, arrayList);
			return (MemberMapping[])arrayList.ToArray(typeof(MemberMapping));
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0009ABE4 File Offset: 0x00098DE4
		private static void GetSettableMembers(StructMapping mapping, ArrayList list)
		{
			if (mapping.BaseMapping != null)
			{
				TypeScope.GetSettableMembers(mapping.BaseMapping, list);
			}
			if (mapping.Members != null)
			{
				foreach (MemberMapping memberMapping in mapping.Members)
				{
					MemberInfo memberInfo = memberMapping.MemberInfo;
					if (memberInfo != null && memberInfo.MemberType == MemberTypes.Property)
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo != null && !TypeScope.CanWriteProperty(propertyInfo, memberMapping.TypeDesc))
						{
							throw new InvalidOperationException(Res.GetString("Cannot deserialize type '{0}' because it contains property '{1}' which has no public setter.", new object[]
							{
								propertyInfo.DeclaringType,
								propertyInfo.Name
							}));
						}
					}
					list.Add(memberMapping);
				}
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0009AC96 File Offset: 0x00098E96
		private static bool CanWriteProperty(PropertyInfo propertyInfo, TypeDesc typeDesc)
		{
			return typeDesc.Kind == TypeKind.Collection || typeDesc.Kind == TypeKind.Enumerable || (propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0009ACC8 File Offset: 0x00098EC8
		internal static MemberMapping[] GetSettableMembers(StructMapping mapping, Dictionary<string, MemberInfo> memberInfos)
		{
			MemberMapping[] settableMembers = TypeScope.GetSettableMembers(mapping);
			TypeScope.PopulateMemberInfos(mapping, settableMembers, memberInfos);
			return settableMembers;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0009ACE8 File Offset: 0x00098EE8
		private static void PopulateMemberInfos(StructMapping structMapping, MemberMapping[] mappings, Dictionary<string, MemberInfo> memberInfos)
		{
			memberInfos.Clear();
			for (int i = 0; i < mappings.Length; i++)
			{
				memberInfos[mappings[i].Name] = mappings[i].MemberInfo;
				if (mappings[i].ChoiceIdentifier != null)
				{
					memberInfos[mappings[i].ChoiceIdentifier.MemberName] = mappings[i].ChoiceIdentifier.MemberInfo;
				}
				if (mappings[i].CheckSpecifiedMemberInfo != null)
				{
					memberInfos[mappings[i].Name + "Specified"] = mappings[i].CheckSpecifiedMemberInfo;
				}
			}
			Dictionary<string, MemberInfo> dictionary = null;
			MemberInfo value = null;
			foreach (KeyValuePair<string, MemberInfo> keyValuePair in memberInfos)
			{
				if (TypeScope.ShouldBeReplaced(keyValuePair.Value, structMapping.TypeDesc.Type, out value))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, MemberInfo>();
					}
					dictionary.Add(keyValuePair.Key, value);
				}
			}
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, MemberInfo> keyValuePair2 in dictionary)
				{
					memberInfos[keyValuePair2.Key] = keyValuePair2.Value;
				}
				for (int j = 0; j < mappings.Length; j++)
				{
					MemberInfo memberInfo;
					if (dictionary.TryGetValue(mappings[j].Name, out memberInfo))
					{
						MemberMapping memberMapping = mappings[j].Clone();
						memberMapping.MemberInfo = memberInfo;
						mappings[j] = memberMapping;
					}
				}
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0009AE78 File Offset: 0x00099078
		private static bool ShouldBeReplaced(MemberInfo memberInfoToBeReplaced, Type derivedType, out MemberInfo replacedInfo)
		{
			replacedInfo = memberInfoToBeReplaced;
			Type type = derivedType;
			Type declaringType = memberInfoToBeReplaced.DeclaringType;
			if (declaringType.IsAssignableFrom(type))
			{
				while (type != declaringType)
				{
					TypeInfo typeInfo = type.GetTypeInfo();
					foreach (PropertyInfo propertyInfo in typeInfo.DeclaredProperties)
					{
						if (propertyInfo.Name == memberInfoToBeReplaced.Name)
						{
							replacedInfo = propertyInfo;
							if (replacedInfo != memberInfoToBeReplaced)
							{
								return true;
							}
						}
					}
					foreach (FieldInfo fieldInfo in typeInfo.DeclaredFields)
					{
						if (fieldInfo.Name == memberInfoToBeReplaced.Name)
						{
							replacedInfo = fieldInfo;
							if (replacedInfo != memberInfoToBeReplaced)
							{
								return true;
							}
						}
					}
					type = type.BaseType;
				}
			}
			return false;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0009AF84 File Offset: 0x00099184
		private static TypeFlags GetConstructorFlags(Type type, ref Exception exception)
		{
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
			if (constructor != null)
			{
				TypeFlags typeFlags = TypeFlags.HasDefaultConstructor;
				if (!constructor.IsPublic)
				{
					typeFlags |= TypeFlags.CtorInaccessible;
				}
				else
				{
					object[] customAttributes = constructor.GetCustomAttributes(typeof(ObsoleteAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0 && ((ObsoleteAttribute)customAttributes[0]).IsError)
					{
						typeFlags |= TypeFlags.CtorInaccessible;
					}
				}
				return typeFlags;
			}
			return TypeFlags.None;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0009AFF8 File Offset: 0x000991F8
		private static Type GetEnumeratorElementType(Type type, ref TypeFlags flags)
		{
			if (!typeof(IEnumerable).IsAssignableFrom(type))
			{
				return null;
			}
			MethodInfo methodInfo = type.GetMethod("GetEnumerator", new Type[0]);
			if (methodInfo == null || !typeof(IEnumerator).IsAssignableFrom(methodInfo.ReturnType))
			{
				methodInfo = null;
				MemberInfo[] member = type.GetMember("System.Collections.Generic.IEnumerable<*", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < member.Length; i++)
				{
					methodInfo = (member[i] as MethodInfo);
					if (methodInfo != null && typeof(IEnumerator).IsAssignableFrom(methodInfo.ReturnType))
					{
						flags |= TypeFlags.GenericInterface;
						break;
					}
					methodInfo = null;
				}
				if (methodInfo == null)
				{
					flags |= TypeFlags.UsePrivateImplementation;
					methodInfo = type.GetMethod("System.Collections.IEnumerable.GetEnumerator", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
			}
			if (methodInfo == null || !typeof(IEnumerator).IsAssignableFrom(methodInfo.ReturnType))
			{
				return null;
			}
			if (new XmlAttributes(methodInfo).XmlIgnore)
			{
				return null;
			}
			PropertyInfo property = methodInfo.ReturnType.GetProperty("Current");
			Type type2 = (property == null) ? typeof(object) : property.PropertyType;
			MethodInfo method = type.GetMethod("Add", new Type[]
			{
				type2
			});
			if (method == null && type2 != typeof(object))
			{
				type2 = typeof(object);
				method = type.GetMethod("Add", new Type[]
				{
					type2
				});
			}
			if (method == null)
			{
				throw new InvalidOperationException(Res.GetString("To be XML serializable, types which inherit from {2} must have an implementation of Add({1}) at all levels of their inheritance hierarchy. {0} does not implement Add({1}).", new object[]
				{
					type.FullName,
					type2,
					"IEnumerable"
				}));
			}
			return type2;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0009B1BC File Offset: 0x000993BC
		internal static PropertyInfo GetDefaultIndexer(Type type, string memberInfo)
		{
			if (typeof(IDictionary).IsAssignableFrom(type))
			{
				if (memberInfo == null)
				{
					throw new NotSupportedException(Res.GetString("The type {0} is not supported because it implements IDictionary.", new object[]
					{
						type.FullName
					}));
				}
				throw new NotSupportedException(Res.GetString("Cannot serialize member {0} of type {1}, because it implements IDictionary.", new object[]
				{
					memberInfo,
					type.FullName
				}));
			}
			else
			{
				MemberInfo[] defaultMembers = type.GetDefaultMembers();
				PropertyInfo propertyInfo = null;
				if (defaultMembers != null && defaultMembers.Length != 0)
				{
					Type type2 = type;
					while (type2 != null)
					{
						for (int i = 0; i < defaultMembers.Length; i++)
						{
							if (defaultMembers[i] is PropertyInfo)
							{
								PropertyInfo propertyInfo2 = (PropertyInfo)defaultMembers[i];
								if (!(propertyInfo2.DeclaringType != type2) && propertyInfo2.CanRead)
								{
									ParameterInfo[] parameters = propertyInfo2.GetGetMethod().GetParameters();
									if (parameters.Length == 1 && parameters[0].ParameterType == typeof(int))
									{
										propertyInfo = propertyInfo2;
										break;
									}
								}
							}
						}
						if (propertyInfo != null)
						{
							break;
						}
						type2 = type2.BaseType;
					}
				}
				if (propertyInfo == null)
				{
					throw new InvalidOperationException(Res.GetString("You must implement a default accessor on {0} because it inherits from ICollection.", new object[]
					{
						type.FullName
					}));
				}
				if (type.GetMethod("Add", new Type[]
				{
					propertyInfo.PropertyType
				}) == null)
				{
					throw new InvalidOperationException(Res.GetString("To be XML serializable, types which inherit from {2} must have an implementation of Add({1}) at all levels of their inheritance hierarchy. {0} does not implement Add({1}).", new object[]
					{
						type.FullName,
						propertyInfo.PropertyType,
						"ICollection"
					}));
				}
				return propertyInfo;
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0009B33F File Offset: 0x0009953F
		private static Type GetCollectionElementType(Type type, string memberInfo)
		{
			return TypeScope.GetDefaultIndexer(type, memberInfo).PropertyType;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0009B350 File Offset: 0x00099550
		internal static XmlQualifiedName ParseWsdlArrayType(string type, out string dims, XmlSchemaObject parent)
		{
			int num = type.LastIndexOf(':');
			string text;
			if (num <= 0)
			{
				text = "";
			}
			else
			{
				text = type.Substring(0, num);
			}
			int num2 = type.IndexOf('[', num + 1);
			if (num2 <= num)
			{
				throw new InvalidOperationException(Res.GetString("Invalid wsd:arrayType syntax: '{0}'.", new object[]
				{
					type
				}));
			}
			string name = type.Substring(num + 1, num2 - num - 1);
			dims = type.Substring(num2);
			while (parent != null)
			{
				if (parent.Namespaces != null)
				{
					string text2 = (string)parent.Namespaces.Namespaces[text];
					if (text2 != null)
					{
						text = text2;
						break;
					}
				}
				parent = parent.Parent;
			}
			return new XmlQualifiedName(name, text);
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0009B3FA File Offset: 0x000995FA
		internal ICollection Types
		{
			get
			{
				return this.typeDescs.Keys;
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0009B407 File Offset: 0x00099607
		internal void AddTypeMapping(TypeMapping typeMapping)
		{
			this.typeMappings.Add(typeMapping);
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0009B416 File Offset: 0x00099616
		internal ICollection TypeMappings
		{
			get
			{
				return this.typeMappings;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0009B41E File Offset: 0x0009961E
		internal static Hashtable PrimtiveTypes
		{
			get
			{
				return TypeScope.primitiveTypes;
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0009B425 File Offset: 0x00099625
		public TypeScope()
		{
		}

		// Token: 0x040019A3 RID: 6563
		private Hashtable typeDescs = new Hashtable();

		// Token: 0x040019A4 RID: 6564
		private Hashtable arrayTypeDescs = new Hashtable();

		// Token: 0x040019A5 RID: 6565
		private ArrayList typeMappings = new ArrayList();

		// Token: 0x040019A6 RID: 6566
		private static Hashtable primitiveTypes = new Hashtable();

		// Token: 0x040019A7 RID: 6567
		private static Hashtable primitiveDataTypes = new Hashtable();

		// Token: 0x040019A8 RID: 6568
		private static NameTable primitiveNames = new NameTable();

		// Token: 0x040019A9 RID: 6569
		private static string[] unsupportedTypes = new string[]
		{
			"anyURI",
			"duration",
			"ENTITY",
			"ENTITIES",
			"gDay",
			"gMonth",
			"gMonthDay",
			"gYear",
			"gYearMonth",
			"ID",
			"IDREF",
			"IDREFS",
			"integer",
			"language",
			"negativeInteger",
			"nonNegativeInteger",
			"nonPositiveInteger",
			"NOTATION",
			"positiveInteger",
			"token"
		};
	}
}
