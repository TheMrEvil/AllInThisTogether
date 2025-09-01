using System;
using System.Globalization;

namespace System.Runtime.Serialization
{
	// Token: 0x0200015D RID: 349
	internal static class SR
	{
		// Token: 0x06001264 RID: 4708 RVA: 0x0004717E File Offset: 0x0004537E
		internal static string GetString(string name, params object[] args)
		{
			return SR.GetString(CultureInfo.InvariantCulture, name, args);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0000205E File Offset: 0x0000025E
		internal static string GetString(CultureInfo culture, string name, params object[] args)
		{
			return string.Format(culture, name, args);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00002068 File Offset: 0x00000268
		internal static string GetString(string name)
		{
			return name;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0000206B File Offset: 0x0000026B
		internal static string GetString(CultureInfo culture, string name)
		{
			return name;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00002068 File Offset: 0x00000268
		internal static string Format(string resourceFormats)
		{
			return resourceFormats;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00002081 File Offset: 0x00000281
		internal static string Format(string resourceFormat, object p1)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
		}

		// Token: 0x0400074B RID: 1867
		public const string ArrayExceededSize = "Array length '{0}' provided by the get-only collection of type '{1}' is less than the number of array elements found in the input stream.  Consider increasing the length of the array.";

		// Token: 0x0400074C RID: 1868
		public const string ArrayExceededSizeAttribute = "Array length '{0}' provided by Size attribute is not equal to the number of array elements '{1}' from namespace '{2}' found.";

		// Token: 0x0400074D RID: 1869
		public const string ArrayTypeIsNotSupported = "An internal error has occurred. '{0}[]' is not supported when generating code for serialization.";

		// Token: 0x0400074E RID: 1870
		public const string CannotDeserializeRefAtTopLevel = "Cannot deserialize since root element references unrecognized object with id '{0}'.";

		// Token: 0x0400074F RID: 1871
		public const string CannotLoadMemberType = "Cannot load member type '{0}'.";

		// Token: 0x04000750 RID: 1872
		public const string CannotSerializeObjectWithCycles = "Object graph for type '{0}' contains cycles and cannot be serialized if references are not tracked. Consider using the DataContractAttribute with the IsReference property set to true.";

		// Token: 0x04000751 RID: 1873
		public const string CanOnlyStoreIntoArgOrLocGot0 = "An internal error has occurred. Data can only be stored into ArgBuilder or LocalBuilder. Got: {0}.";

		// Token: 0x04000752 RID: 1874
		public const string CharIsInvalidPrimitive = "An internal error has occurred. Char is not a valid schema primitive and should be treated as int in DataContract.";

		// Token: 0x04000753 RID: 1875
		public const string CallbackMustReturnVoid = "Serialization Callback '{1}' in type '{0}' must return void.";

		// Token: 0x04000754 RID: 1876
		public const string CallbackParameterInvalid = "Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.";

		// Token: 0x04000755 RID: 1877
		public const string CallbacksCannotBeVirtualMethods = "Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.";

		// Token: 0x04000756 RID: 1878
		public const string CollectionMustHaveAddMethod = "Collection type '{0}' does not have a valid Add method.";

		// Token: 0x04000757 RID: 1879
		public const string CollectionMustHaveGetEnumeratorMethod = "Collection type '{0}' does not have a valid GetEnumerator method.";

		// Token: 0x04000758 RID: 1880
		public const string CollectionMustHaveItemType = "Collection type '{0}' must have a non-null item type.";

		// Token: 0x04000759 RID: 1881
		public const string CollectionTypeCannotBeBuiltIn = "{0} is a built-in type and cannot be a collection.";

		// Token: 0x0400075A RID: 1882
		public const string CollectionTypeCannotHaveDataContract = "{0} has DataContractAttribute attribute.";

		// Token: 0x0400075B RID: 1883
		public const string CollectionTypeDoesNotHaveAddMethod = "{0} does not have a valid Add method with parameter of type '{1}'.";

		// Token: 0x0400075C RID: 1884
		public const string CollectionTypeDoesNotHaveDefaultCtor = "{0} does not have a default constructor.";

		// Token: 0x0400075D RID: 1885
		public const string CollectionTypeHasMultipleDefinitionsOfInterface = "{0} has multiple definitions of interface '{1}'.";

		// Token: 0x0400075E RID: 1886
		public const string CollectionTypeIsNotIEnumerable = "{0} does not implement IEnumerable interface.";

		// Token: 0x0400075F RID: 1887
		public const string DataContractCacheOverflow = "An internal error has occurred. DataContract cache overflow.";

		// Token: 0x04000760 RID: 1888
		public const string DataContractNamespaceAlreadySet = "ContractNamespaceAttribute attribute maps CLR namespace '{2}' to multiple data contract namespaces '{0}' and '{1}'. You can map a CLR namespace to only one data contract namespace.";

		// Token: 0x04000761 RID: 1889
		public const string DataContractNamespaceIsNotValid = "DataContract namespace '{0}' is not a valid URI.";

		// Token: 0x04000762 RID: 1890
		public const string DataContractNamespaceReserved = "DataContract namespace '{0}' cannot be specified since it is reserved.";

		// Token: 0x04000763 RID: 1891
		public const string DataMemberOnEnumField = "Member '{0}.{1}' has DataMemberAttribute attribute. Use EnumMemberAttribute attribute instead.";

		// Token: 0x04000764 RID: 1892
		public const string DcTypeNotFoundOnDeserialize = "Element '{2}:{3}' contains data of the '{0}:{1}' data contract. The deserializer has no knowledge of any type that maps to this contract. Add the type corresponding to '{1}' to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding it to the list of known types passed to DataContractSerializer.";

		// Token: 0x04000765 RID: 1893
		public const string DcTypeNotFoundOnSerialize = "Type '{0}' with data contract name '{1}:{2}' is not expected. Add any types not known statically to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding them to the list of known types passed to DataContractSerializer.";

		// Token: 0x04000766 RID: 1894
		public const string DcTypeNotResolvedOnDeserialize = "Element '{2}:{3}' contains data from a type that maps to the name '{0}:{1}'. The deserializer has no knowledge of any type that maps to this name. Consider changing the implementation of the ResolveName method on your DataContractResolver to return a non-null value for name '{1}' and namespace '{0}'.";

		// Token: 0x04000767 RID: 1895
		public const string DeserializedObjectWithIdNotFound = "Deserialized object with reference id '{0}' not found in stream.";

		// Token: 0x04000768 RID: 1896
		public const string DupContractInKnownTypes = "Type '{0}' cannot be added to list of known types since another type '{1}' with the same data contract name '{2}:{3}' is already present.";

		// Token: 0x04000769 RID: 1897
		public const string DupKeyValueName = "The collection data contract type '{0}' specifies the same value '{1}' for both the KeyName and the ValueName properties. This is not allowed. Consider changing either the KeyName or the ValueName property.";

		// Token: 0x0400076A RID: 1898
		public const string DupEnumMemberValue = "Type '{2}' contains two members '{0}' 'and '{1}' with the same name '{3}'. Multiple members with the same name in one type are not supported. Consider changing one of the member names using EnumMemberAttribute attribute.";

		// Token: 0x0400076B RID: 1899
		public const string DupMemberName = "Type '{2}' contains two members '{0}' 'and '{1}' with the same data member name '{3}'. Multiple members with the same name in one type are not supported. Consider changing one of the member names using DataMemberAttribute attribute.";

		// Token: 0x0400076C RID: 1900
		public const string DuplicateAttribute = "Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.";

		// Token: 0x0400076D RID: 1901
		public const string DuplicateCallback = "Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.";

		// Token: 0x0400076E RID: 1902
		public const string EncounteredWithNameNamespace = "{0}. Encountered '{1}'  with name '{2}', namespace '{3}'.";

		// Token: 0x0400076F RID: 1903
		public const string EnumTypeCannotHaveIsReference = "Enum type '{0}' cannot have the IsReference setting of '{1}'. Either change the setting to '{2}', or remove it completely.";

		// Token: 0x04000770 RID: 1904
		public const string ErrorDeserializing = "There was an error deserializing the object {0}. {1}";

		// Token: 0x04000771 RID: 1905
		public const string ErrorInLine = "Error in line {0} position {1}.";

		// Token: 0x04000772 RID: 1906
		public const string ErrorIsStartObject = "There was an error checking start element of object {0}. {1}";

		// Token: 0x04000773 RID: 1907
		public const string ErrorSerializing = "There was an error serializing the object {0}. {1}";

		// Token: 0x04000774 RID: 1908
		public const string ErrorTypeInfo = "of type {0}";

		// Token: 0x04000775 RID: 1909
		public const string ErrorWriteEndObject = "There was an error writing end element of object {0}. {1}";

		// Token: 0x04000776 RID: 1910
		public const string ErrorWriteStartObject = "There was an error writing start element of object {0}. {1}";

		// Token: 0x04000777 RID: 1911
		public const string ExceededMaxItemsQuota = "Maximum number of items that can be serialized or deserialized in an object graph is '{0}'.";

		// Token: 0x04000778 RID: 1912
		public const string ExpectingElement = "Expecting element '{1}' from namespace '{0}'.";

		// Token: 0x04000779 RID: 1913
		public const string ExpectingElementAtDeserialize = "Expecting state '{0}' when ReadObject is called.";

		// Token: 0x0400077A RID: 1914
		public const string ExpectingEnd = "Expecting End'{0}'.";

		// Token: 0x0400077B RID: 1915
		public const string ExpectingState = "Expecting state '{0}'.";

		// Token: 0x0400077C RID: 1916
		public const string GenericNameBraceMismatch = "The data contract name '{0}' for type '{1}' has a curly brace '{{' that is not matched with a closing curly brace. Curly braces have special meaning in data contract names - they are used to customize the naming of data contracts for generic types.";

		// Token: 0x0400077D RID: 1917
		public const string GenericParameterNotValid = "In the data contract name for type '{1}', there are curly braces with '{0}' inside, which is an invalid value. Curly braces have special meaning in data contract names - they are used to customize the naming of data contracts for generic types. Based on the number of generic parameters this type has, the contents of the curly braces must either be a number between 0 and '{2}' to insert the name of the generic parameter at that index or the '#' symbol to insert a digest of the generic parameter namespaces.";

		// Token: 0x0400077E RID: 1918
		public const string InconsistentIsReference = "The IsReference setting for type '{0}' is '{1}', but the same setting for its parent class '{2}' is '{3}'. Derived types must have the same value for IsReference as the base type. Change the setting on type '{0}' to '{3}', or on type '{2}' to '{1}', or do not set IsReference explicitly.";

		// Token: 0x0400077F RID: 1919
		public const string IndexedPropertyCannotBeSerialized = "Property '{1}' in type '{0}' cannot be serialized because serialization of indexed properties is not supported.";

		// Token: 0x04000780 RID: 1920
		public const string InterfaceTypeCannotBeCreated = "Interface type '{0}' cannot be created. Consider replacing with a non-interface serializable type.";

		// Token: 0x04000781 RID: 1921
		public const string InvalidCollectionContractItemName = "Type '{0}' cannot have CollectionDataContractAttribute attribute ItemName set to null or empty string.";

		// Token: 0x04000782 RID: 1922
		public const string InvalidCollectionContractKeyName = "Type '{0}' cannot have CollectionDataContractAttribute attribute KeyName set to null or empty string.";

		// Token: 0x04000783 RID: 1923
		public const string InvalidCollectionContractKeyNoDictionary = "The collection data contract type '{0}' specifies '{1}' for the KeyName property. This is not allowed since the type is not IDictionary. Remove the setting for the KeyName property.";

		// Token: 0x04000784 RID: 1924
		public const string InvalidCollectionContractName = "Type '{0}' cannot have CollectionDataContractAttribute attribute Name set to null or empty string.";

		// Token: 0x04000785 RID: 1925
		public const string InvalidCollectionContractNamespace = "Type '{0}' cannot have CollectionDataContractAttribute attribute Namespace set to null.";

		// Token: 0x04000786 RID: 1926
		public const string InvalidCollectionContractValueName = "Type '{0}' cannot have CollectionDataContractAttribute attribute ValueName set to null or empty string.";

		// Token: 0x04000787 RID: 1927
		public const string InvalidCollectionContractValueNoDictionary = "The collection data contract type '{0}' specifies '{1}' for the ValueName property. This is not allowed since the type is not IDictionary. Remove the setting for the ValueName property.";

		// Token: 0x04000788 RID: 1928
		public const string InvalidCollectionDataContract = "Type '{0}' with CollectionDataContractAttribute attribute is an invalid collection type since it";

		// Token: 0x04000789 RID: 1929
		public const string InvalidCollectionType = "Type '{0}' is an invalid collection type since it";

		// Token: 0x0400078A RID: 1930
		public const string InvalidDataContractName = "Type '{0}' cannot have DataContractAttribute attribute Name set to null or empty string.";

		// Token: 0x0400078B RID: 1931
		public const string InvalidDataContractNamespace = "Type '{0}' cannot have DataContractAttribute attribute Namespace set to null.";

		// Token: 0x0400078C RID: 1932
		public const string InvalidDataMemberName = "Member '{0}' in type '{1}' cannot have DataMemberAttribute attribute Name set to null or empty string.";

		// Token: 0x0400078D RID: 1933
		public const string InvalidEnumMemberValue = "'{0}' in type '{1}' cannot have EnumMemberAttribute attribute Value set to null or empty string.";

		// Token: 0x0400078E RID: 1934
		public const string InvalidEnumValueOnRead = "Invalid enum value '{0}' cannot be deserialized into type '{1}'. Ensure that the necessary enum values are present and are marked with EnumMemberAttribute attribute if the type has DataContractAttribute attribute.";

		// Token: 0x0400078F RID: 1935
		public const string InvalidEnumValueOnWrite = "Enum value '{0}' is invalid for type '{1}' and cannot be serialized. Ensure that the necessary enum values are present and are marked with EnumMemberAttribute attribute if the type has DataContractAttribute attribute.";

		// Token: 0x04000790 RID: 1936
		public const string InvalidGetSchemaMethod = "Type '{0}' cannot have MethodName on XmlSchemaProviderAttribute attribute set to null or empty string.";

		// Token: 0x04000791 RID: 1937
		public const string InvalidGlobalDataContractNamespace = "CLR namespace '{0}' cannot have ContractNamespace set to null.";

		// Token: 0x04000792 RID: 1938
		public const string InvalidMember = "Member '{0}.{1}' cannot be serialized since it is neither a field nor a property, and therefore cannot be marked with the DataMemberAttribute attribute. Remove the DataMemberAttribute attribute from the '{1}' member.";

		// Token: 0x04000793 RID: 1939
		public const string InvalidNonNullReturnValueByIsAny = "Method '{0}.{1}()' returns a non-null value. The return value must be null since IsAny=true.";

		// Token: 0x04000794 RID: 1940
		public const string InvalidPrimitiveType = "Type '{0}' is not a valid serializable type.";

		// Token: 0x04000795 RID: 1941
		public const string InvalidReturnTypeOnGetSchemaMethod = "Method '{0}.{1}()' returns '{2}'. The return type must be compatible with '{3}'.";

		// Token: 0x04000796 RID: 1942
		public const string InvalidSizeDefinition = "Invalid Size '{0}'. Must be non-negative integer.";

		// Token: 0x04000797 RID: 1943
		public const string InvalidXmlDataContractName = "XML data contract Name for type '{0}' cannot be set to null or empty string.";

		// Token: 0x04000798 RID: 1944
		public const string InvalidXsIdDefinition = "Invalid Id '{0}'. Must not be null or empty.";

		// Token: 0x04000799 RID: 1945
		public const string InvalidXsRefDefinition = "Invalid Ref '{0}'. Must not be null or empty.";

		// Token: 0x0400079A RID: 1946
		public const string IsAnyCannotBeNull = "A null value cannot be serialized at the top level for IXmlSerializable root type '{0}' since its IsAny setting is 'true'. This type must write all its contents including the root element. Verify that the IXmlSerializable implementation is correct.";

		// Token: 0x0400079B RID: 1947
		public const string IsAnyCannotBeSerializedAsDerivedType = "An object of type '{0}' cannot be serialized at the top level for IXmlSerializable root type '{1}' since its IsAny setting is 'true'. This type must write all its contents including the root element. Verify that the IXmlSerializable implementation is correct.";

		// Token: 0x0400079C RID: 1948
		public const string IsAnyCannotHaveXmlRoot = "Type '{0}' cannot specify an XmlRootAttribute attribute because its IsAny setting is 'true'. This type must write all its contents including the root element. Verify that the IXmlSerializable implementation is correct.";

		// Token: 0x0400079D RID: 1949
		public const string IsNotAssignableFrom = "An internal error has occurred. '{0}' is not assignable from '{1}' - error generating code for serialization.";

		// Token: 0x0400079E RID: 1950
		public const string IsRequiredDataMemberOnIsReferenceDataContractType = "'{0}.{1}' has the IsRequired setting of '{2}. However, '{0}' has the IsReference setting of '{2}', because either it is set explicitly, or it is derived from a base class. Set IsRequired on '{0}.{1}' to false, or disable IsReference on '{0}'.";

		// Token: 0x0400079F RID: 1951
		public const string IXmlSerializableCannotHaveCollectionDataContract = "Type '{0}' cannot be IXmlSerializable and have CollectionDataContractAttribute attribute.";

		// Token: 0x040007A0 RID: 1952
		public const string IXmlSerializableCannotHaveDataContract = "Type '{0}' cannot be IXmlSerializable and have DataContractAttribute attribute.";

		// Token: 0x040007A1 RID: 1953
		public const string IXmlSerializableIllegalOperation = "This method cannot be called from IXmlSerializable implementations.";

		// Token: 0x040007A2 RID: 1954
		public const string IXmlSerializableMissingEndElements = "IXmlSerializable.WriteXml method of type '{0}' did not close all open tags. Verify that the IXmlSerializable implementation is correct.";

		// Token: 0x040007A3 RID: 1955
		public const string IXmlSerializableMustHaveDefaultConstructor = "IXmlSerializable Type '{0}' must have default constructor.";

		// Token: 0x040007A4 RID: 1956
		public const string IXmlSerializableWritePastSubTree = "IXmlSerializable.WriteXml method of type '{0}' attempted to close too many tags.  Verify that the IXmlSerializable implementation is correct.";

		// Token: 0x040007A5 RID: 1957
		public const string KnownTypeAttributeEmptyString = "Method name specified by KnownTypeAttribute attribute on type '{0}' cannot be the empty string.";

		// Token: 0x040007A6 RID: 1958
		public const string KnownTypeAttributeUnknownMethod = "KnownTypeAttribute attribute on type '{1}' specifies a method named '{0}' to provide known types. Static method '{0}()' was not found on this type. Ensure that the method exists and is marked as static.";

		// Token: 0x040007A7 RID: 1959
		public const string KnownTypeAttributeReturnType = "KnownTypeAttribute attribute on type '{0}' specifies a method named '{1}' to provide known types. The return type of this method is invalid because it is not assignable to IEnumerable<Type>. Ensure that the method exists and has a valid signature.";

		// Token: 0x040007A8 RID: 1960
		public const string KnownTypeAttributeOneScheme = "Type '{0}': If a KnownTypeAttribute attribute specifies a method it must be the only KnownTypeAttribute attribute on that type.";

		// Token: 0x040007A9 RID: 1961
		public const string KnownTypeAttributeNoType = "KnownTypeAttribute attribute on type '{0}' contains no Type.";

		// Token: 0x040007AA RID: 1962
		public const string KnownTypeConfigClosedGenericDeclared = "Declared type '{0}' in config cannot be a closed or partial generic type.";

		// Token: 0x040007AB RID: 1963
		public const string KnownTypeAttributeValidMethodTypes = "Method specified by KnownTypeAttribute attribute on type '{0}' does not expose valid types.";

		// Token: 0x040007AC RID: 1964
		public const string KnownTypeAttributeNoData = "KnownTypeAttribute attribute on type '{0}' contains no data.";

		// Token: 0x040007AD RID: 1965
		public const string KnownTypeAttributeMethodNull = "Method specified by KnownTypeAttribute attribute on type '{0}' returned null.";

		// Token: 0x040007AE RID: 1966
		public const string MaxArrayLengthExceeded = "The maximum array length ({0}) has been exceeded while reading XML data for array of type '{1}'.";

		// Token: 0x040007AF RID: 1967
		public const string MissingGetSchemaMethod = "Type '{0}' does not have a static method '{1}' that takes a parameter of type 'System.Xml.Schema.XmlSchemaSet' as specified by the XmlSchemaProviderAttribute attribute.";

		// Token: 0x040007B0 RID: 1968
		public const string MultipleIdDefinition = "Invalid XML encountered. The same Id value '{0}' is defined more than once. Multiple objects cannot be deserialized using the same Id.";

		// Token: 0x040007B1 RID: 1969
		public const string NoConversionPossibleTo = "An internal error has occurred. No conversion is possible to '{0}' - error generating code for serialization.";

		// Token: 0x040007B2 RID: 1970
		public const string NoGetMethodForProperty = "No get method for property '{1}' in type '{0}'.";

		// Token: 0x040007B3 RID: 1971
		public const string NoSetMethodForProperty = "No set method for property '{1}' in type '{0}'.";

		// Token: 0x040007B4 RID: 1972
		public const string NullKnownType = "One of the known types provided to the serializer via '{0}' argument was invalid because it was null. All known types specified must be non-null values.";

		// Token: 0x040007B5 RID: 1973
		public const string NullValueReturnedForGetOnlyCollection = "The get-only collection of type '{0}' returned a null value.  The input stream contains collection items which cannot be added if the instance is null.  Consider initializing the collection either in the constructor of the the object or in the getter.";

		// Token: 0x040007B6 RID: 1974
		public const string ObjectTableOverflow = "An internal error has occurred. Object table overflow. This could be caused by serializing or deserializing extremely large object graphs.";

		// Token: 0x040007B7 RID: 1975
		public const string OrderCannotBeNegative = "Property 'Order' in DataMemberAttribute attribute cannot be a negative number.";

		// Token: 0x040007B8 RID: 1976
		public const string ParameterCountMismatch = "Invalid number of parameters to call method '{0}'. Expected '{1}' parameters, but '{2}' were provided.";

		// Token: 0x040007B9 RID: 1977
		public const string PartialTrustCollectionContractAddMethodNotPublic = "The collection data contract type '{0}' cannot be deserialized because the method '{1}' is not public. Making the method public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007BA RID: 1978
		public const string PartialTrustCollectionContractNoPublicConstructor = "The collection data contract type '{0}' cannot be deserialized because it does not have a public parameterless constructor. Adding a public parameterless constructor will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007BB RID: 1979
		public const string PartialTrustCollectionContractTypeNotPublic = "The collection data contract type '{0}' cannot be deserialized because it does not have a public parameterless constructor. Adding a public parameterless constructor will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007BC RID: 1980
		public const string PartialTrustDataContractOnSerializingNotPublic = "The data contract type '{0}' cannot be serialized because the OnSerializing method '{1}' is not public. Making the method public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007BD RID: 1981
		public const string PartialTrustDataContractOnSerializedNotPublic = "The data contract type '{0}' cannot be serialized because the OnSerialized method '{1}' is not public. Making the method public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007BE RID: 1982
		public const string PartialTrustDataContractOnDeserializingNotPublic = "The data contract type '{0}' cannot be deserialized because the OnDeserializing method '{1}' is not public. Making the method public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007BF RID: 1983
		public const string PartialTrustDataContractOnDeserializedNotPublic = "The data contract type '{0}' cannot be deserialized because the OnDeserialized method '{1}' is not public. Making the method public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C0 RID: 1984
		public const string PartialTrustDataContractFieldGetNotPublic = "The data contract type '{0}' cannot be serialized because the member '{1}' is not public. Making the member public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C1 RID: 1985
		public const string PartialTrustDataContractFieldSetNotPublic = "The data contract type '{0}' cannot be deserialized because the member '{1}' is not public. Making the member public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C2 RID: 1986
		public const string PartialTrustDataContractPropertyGetNotPublic = "The data contract type '{0}' cannot be serialized because the property '{1}' does not have a public getter. Adding a public getter will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C3 RID: 1987
		public const string PartialTrustDataContractPropertySetNotPublic = "The data contract type '{0}' cannot be deserialized because the property '{1}' does not have a public setter. Adding a public setter will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C4 RID: 1988
		public const string PartialTrustDataContractTypeNotPublic = "The data contract type '{0}' is not serializable because it is not public. Making the type public will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C5 RID: 1989
		public const string PartialTrustNonAttributedSerializableTypeNoPublicConstructor = "The type '{0}' cannot be deserialized because it does not have a public parameterless constructor. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C6 RID: 1990
		public const string PartialTrustIXmlSerializableTypeNotPublic = "The IXmlSerializable type '{0}' is not serializable in partial trust because it is not public. Adding a public parameterless constructor will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C7 RID: 1991
		public const string PartialTrustIXmlSerialzableNoPublicConstructor = "The IXmlSerializable type '{0}' cannot be deserialized because it does not have a public parameterless constructor. Adding a public parameterless constructor will fix this error. Alternatively, you can make it internal, and use the InternalsVisibleToAttribute attribute on your assembly in order to enable serialization of internal members - see documentation for more details. Be aware that doing so has certain security implications.";

		// Token: 0x040007C8 RID: 1992
		public const string NonAttributedSerializableTypesMustHaveDefaultConstructor = "The Type '{0}' must have a parameterless constructor.";

		// Token: 0x040007C9 RID: 1993
		public const string AttributedTypesCannotInheritFromNonAttributedSerializableTypes = "Type '{0}' cannot inherit from a type that is not marked with DataContractAttribute or SerializableAttribute.  Consider marking the base type '{1}' with DataContractAttribute or SerializableAttribute, or removing them from the derived type.";

		// Token: 0x040007CA RID: 1994
		public const string GetOnlyCollectionsNotSupported = "Get-only collection properties are not supported.  Consider adding a public setter to property '{0}.{1}' or marking the it with the IgnoreDataMemberAttribute.";

		// Token: 0x040007CB RID: 1995
		public const string QuotaMustBePositive = "Quota must be a positive value.";

		// Token: 0x040007CC RID: 1996
		public const string QuotaIsReadOnly = "The '{0}' quota is readonly.";

		// Token: 0x040007CD RID: 1997
		public const string QuotaCopyReadOnly = "Cannot copy XmlDictionaryReaderQuotas. Target is readonly.";

		// Token: 0x040007CE RID: 1998
		public const string RequiredMemberMustBeEmitted = "Member {0} in type {1} cannot be serialized. This exception is usually caused by trying to use a null value where a null value is not allowed. The '{0}' member is set to its default value (usually null or zero). The member's EmitDefault setting is 'false', indicating that the member should not be serialized. However, the member's IsRequired setting is 'true', indicating that it must be serialized. This conflict cannot be resolved.  Consider setting '{0}' to a non-default value. Alternatively, you can change the EmitDefaultValue property on the DataMemberAttribute attribute to true, or changing the IsRequired property to false.";

		// Token: 0x040007CF RID: 1999
		public const string ResolveTypeReturnedFalse = "An object of type '{0}' which derives from DataContractResolver returned false from its TryResolveType method when attempting to resolve the name for an object of type '{1}', indicating that the resolution failed. Change the TryResolveType implementation to return true.";

		// Token: 0x040007D0 RID: 2000
		public const string ResolveTypeReturnedNull = "An object of type '{0}' which derives from DataContractResolver returned a null typeName or typeNamespace but not both from its TryResolveType method when attempting to resolve the name for an object of type '{1}'. Change the TryResolveType implementation to return non-null values, or to return null values for both typeName and typeNamespace in order to serialize as the declared type.";

		// Token: 0x040007D1 RID: 2001
		public const string SupportForMultidimensionalArraysNotPresent = "Multi-dimensional arrays are not supported.";

		// Token: 0x040007D2 RID: 2002
		public const string TooManyCollectionContracts = "Type '{0}' has more than one CollectionDataContractAttribute attribute.";

		// Token: 0x040007D3 RID: 2003
		public const string TooManyDataContracts = "Type '{0}' has more than one DataContractAttribute attribute.";

		// Token: 0x040007D4 RID: 2004
		public const string TooManyDataMembers = "Member '{0}.{1}' has more than one DataMemberAttribute attribute.";

		// Token: 0x040007D5 RID: 2005
		public const string TooManyEnumMembers = "Member '{0}.{1}' has more than one EnumMemberAttribute attribute.";

		// Token: 0x040007D6 RID: 2006
		public const string TooManyIgnoreDataMemberAttributes = "Member '{0}.{1}' has more than one IgnoreDataMemberAttribute attribute.";

		// Token: 0x040007D7 RID: 2007
		public const string TypeMustBeConcrete = "Error while getting known types for Type '{0}'. The type must not be an open or partial generic class.";

		// Token: 0x040007D8 RID: 2008
		public const string TypeNotSerializable = "Type '{0}' cannot be serialized. Consider marking it with the DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute attribute. Alternatively, you can ensure that the type is public and has a parameterless constructor - all public members of the type will then be serialized, and no attributes will be required.";

		// Token: 0x040007D9 RID: 2009
		public const string UnexpectedContractType = "An internal error has occurred. Unexpected contract type '{0}' for type '{1}' encountered.";

		// Token: 0x040007DA RID: 2010
		public const string UnexpectedElementExpectingElements = "'{0}' '{1}' from namespace '{2}' is not expected. Expecting element '{3}'.";

		// Token: 0x040007DB RID: 2011
		public const string UnexpectedEndOfFile = "Unexpected end of file.";

		// Token: 0x040007DC RID: 2012
		public const string UnknownConstantType = "Unrecognized constant type '{0}'.";

		// Token: 0x040007DD RID: 2013
		public const string UnsupportedIDictionaryAsDataMemberType = "Cannot deserialize one of the DataMember because it is an IDictionary. Use IDictionary<K,V> instead.";

		// Token: 0x040007DE RID: 2014
		public const string ValueMustBeNonNegative = "The value of this argument must be non-negative.";

		// Token: 0x040007DF RID: 2015
		public const string ValueTypeCannotBeNull = "ValueType '{0}' cannot be null.";

		// Token: 0x040007E0 RID: 2016
		public const string ValueTypeCannotHaveBaseType = "Data contract '{0}' from namespace '{1}' is a value type and cannot have base contract '{2}' from namespace '{3}'.";

		// Token: 0x040007E1 RID: 2017
		public const string ValueTypeCannotHaveId = "ValueType '{0}' cannot have id.";

		// Token: 0x040007E2 RID: 2018
		public const string ValueTypeCannotHaveIsReference = "Value type '{0}' cannot have the IsReference setting of '{1}'. Either change the setting to '{2}', or remove it completely.";

		// Token: 0x040007E3 RID: 2019
		public const string ValueTypeCannotHaveRef = "ValueType '{0}' cannot have ref to another object.";

		// Token: 0x040007E4 RID: 2020
		public const string XmlElementAttributes = "Only Element nodes have attributes.";

		// Token: 0x040007E5 RID: 2021
		public const string XmlForObjectCannotHaveContent = "Element {0} from namespace {1} cannot have child contents to be deserialized as an object. Please use XElement to deserialize this pattern of XML.";

		// Token: 0x040007E6 RID: 2022
		public const string XmlInvalidConversion = "The value '{0}' cannot be parsed as the type '{1}'.";

		// Token: 0x040007E7 RID: 2023
		public const string XmlInvalidConversionWithoutValue = "The value cannot be parsed as the type '{0}'.";

		// Token: 0x040007E8 RID: 2024
		public const string XmlStartElementExpected = "Start element expected. Found {0}.";

		// Token: 0x040007E9 RID: 2025
		public const string XmlWriterMustBeInElement = "WriteState '{0}' not valid. Caller must write start element before serializing in contentOnly mode.";

		// Token: 0x040007EA RID: 2026
		public const string OffsetExceedsBufferSize = "The specified offset exceeds the buffer size ({0} bytes).";

		// Token: 0x040007EB RID: 2027
		public const string SizeExceedsRemainingBufferSpace = "The specified size exceeds the remaining buffer space ({0} bytes).";

		// Token: 0x040007EC RID: 2028
		public const string ValueMustBeInRange = "The value of this argument must fall within the range {0} to {1}.";

		// Token: 0x040007ED RID: 2029
		public const string XmlArrayTooSmallOutput = "Array too small.  Must be able to hold at least {0}.";

		// Token: 0x040007EE RID: 2030
		public const string XmlInvalidBase64Length = "Base64 sequence length ({0}) not valid. Must be a multiple of 4.";

		// Token: 0x040007EF RID: 2031
		public const string XmlInvalidBase64Sequence = "The characters '{0}' at offset {1} are not a valid Base64 sequence.";

		// Token: 0x040007F0 RID: 2032
		public const string XmlInvalidBinHexLength = "BinHex sequence length ({0}) not valid. Must be a multiple of 2.";

		// Token: 0x040007F1 RID: 2033
		public const string XmlInvalidBinHexSequence = "The characters '{0}' at offset {1} are not a valid BinHex sequence.";

		// Token: 0x040007F2 RID: 2034
		public const string XmlInvalidHighSurrogate = "High surrogate char '0x{0}' not valid. High surrogate chars range from 0xD800 to 0xDBFF.";

		// Token: 0x040007F3 RID: 2035
		public const string XmlInvalidLowSurrogate = "Low surrogate char '0x{0}' not valid. Low surrogate chars range from 0xDC00 to 0xDFFF.";

		// Token: 0x040007F4 RID: 2036
		public const string XmlInvalidSurrogate = "Surrogate char '0x{0}' not valid. Surrogate chars range from 0x10000 to 0x10FFFF.";

		// Token: 0x040007F5 RID: 2037
		public const string CombinedPrefixNSLength = "The combined length of the prefix and namespace must not be greater than {0}.";

		// Token: 0x040007F6 RID: 2038
		public const string InvalidInclusivePrefixListCollection = "The inclusive namespace prefix collection cannot contain null as one of the items.";

		// Token: 0x040007F7 RID: 2039
		public const string InvalidLocalNameEmpty = "The empty string is not a valid local name.";

		// Token: 0x040007F8 RID: 2040
		public const string XmlArrayTooSmall = "Array too small.";

		// Token: 0x040007F9 RID: 2041
		public const string XmlArrayTooSmallInput = "Array too small.  Length of available data must be at least {0}.";

		// Token: 0x040007FA RID: 2042
		public const string XmlBadBOM = "Unrecognized Byte Order Mark.";

		// Token: 0x040007FB RID: 2043
		public const string XmlBase64DataExpected = "Base64 encoded data expected. Found {0}.";

		// Token: 0x040007FC RID: 2044
		public const string XmlCDATAInvalidAtTopLevel = "CData elements not valid at top level of an XML document.";

		// Token: 0x040007FD RID: 2045
		public const string XmlCloseCData = "']]>' not valid in text node content.";

		// Token: 0x040007FE RID: 2046
		public const string XmlConversionOverflow = "The value '{0}' cannot be represented with the type '{1}'.";

		// Token: 0x040007FF RID: 2047
		public const string XmlDeclarationRequired = "An XML declaration with an encoding is required for all non-UTF8 documents.";

		// Token: 0x04000800 RID: 2048
		public const string XmlDeclMissingVersion = "Version not found in XML declaration.";

		// Token: 0x04000801 RID: 2049
		public const string XmlDeclMissing = "An XML declaration is required for all non-UTF8 documents.";

		// Token: 0x04000802 RID: 2050
		public const string XmlDeclNotFirst = "No characters can appear before the XML declaration.";

		// Token: 0x04000803 RID: 2051
		public const string XmlDictionaryStringIDRange = "XmlDictionaryString IDs must be in the range from {0} to {1}.";

		// Token: 0x04000804 RID: 2052
		public const string XmlDictionaryStringIDUndefinedSession = "XmlDictionaryString ID {0} not defined in the XmlBinaryReaderSession.";

		// Token: 0x04000805 RID: 2053
		public const string XmlDictionaryStringIDUndefinedStatic = "XmlDictionaryString ID {0} not defined in the static dictionary.";

		// Token: 0x04000806 RID: 2054
		public const string XmlDuplicateAttribute = "Duplicate attribute found. Both '{0}' and '{1}' are from the namespace '{2}'.";

		// Token: 0x04000807 RID: 2055
		public const string XmlEmptyNamespaceRequiresNullPrefix = "The empty namespace requires a null or empty prefix.";

		// Token: 0x04000808 RID: 2056
		public const string XmlEncodingMismatch = "The encoding in the declaration '{0}' does not match the encoding of the document '{1}'.";

		// Token: 0x04000809 RID: 2057
		public const string XmlEncodingNotSupported = "XML encoding not supported.";

		// Token: 0x0400080A RID: 2058
		public const string XmlEndElementExpected = "End element '{0}' from namespace '{1}' expected. Found {2}.";

		// Token: 0x0400080B RID: 2059
		public const string XmlEndElementNoOpenNodes = "No corresponding start element is open.";

		// Token: 0x0400080C RID: 2060
		public const string XmlExpectedEncoding = "The expected encoding '{0}' does not match the actual encoding '{1}'.";

		// Token: 0x0400080D RID: 2061
		public const string XmlFoundCData = "cdata '{0}'";

		// Token: 0x0400080E RID: 2062
		public const string XmlFoundComment = "comment '{0}'";

		// Token: 0x0400080F RID: 2063
		public const string XmlFoundElement = "element '{0}' from namespace '{1}'";

		// Token: 0x04000810 RID: 2064
		public const string XmlFoundEndElement = "end element '{0}' from namespace '{1}'";

		// Token: 0x04000811 RID: 2065
		public const string XmlFoundEndOfFile = "end of file";

		// Token: 0x04000812 RID: 2066
		public const string XmlFoundNodeType = "node {0}";

		// Token: 0x04000813 RID: 2067
		public const string XmlFoundText = "text '{0}'";

		// Token: 0x04000814 RID: 2068
		public const string XmlFullStartElementExpected = "Non-empty start element expected. Found {0}.";

		// Token: 0x04000815 RID: 2069
		public const string XmlFullStartElementLocalNameNsExpected = "Non-empty start element '{0}' from namespace '{1}' expected. Found {2}.";

		// Token: 0x04000816 RID: 2070
		public const string XmlFullStartElementNameExpected = "Non-empty start element '{0}' expected. Found {1}.";

		// Token: 0x04000817 RID: 2071
		public const string XmlIDDefined = "ID already defined.";

		// Token: 0x04000818 RID: 2072
		public const string XmlKeyAlreadyExists = "The specified key already exists in the dictionary.";

		// Token: 0x04000819 RID: 2073
		public const string XmlIllegalOutsideRoot = "Text cannot be written outside the root element.";

		// Token: 0x0400081A RID: 2074
		public const string XmlInvalidBytes = "Invalid byte encoding.";

		// Token: 0x0400081B RID: 2075
		public const string XmlInvalidCharRef = "Character reference not valid.";

		// Token: 0x0400081C RID: 2076
		public const string XmlInvalidCommentChars = "XML comments cannot contain '--' or end with '-'.";

		// Token: 0x0400081D RID: 2077
		public const string XmlInvalidDeclaration = "XML declaration can only be written at the beginning of the document.";

		// Token: 0x0400081E RID: 2078
		public const string XmlInvalidDepth = "Cannot call '{0}' while Depth is '{1}'.";

		// Token: 0x0400081F RID: 2079
		public const string XmlInvalidEncoding = "XML encoding must be 'UTF-8'.";

		// Token: 0x04000820 RID: 2080
		public const string XmlInvalidFFFE = "Characters with hexadecimal values 0xFFFE and 0xFFFF are not valid.";

		// Token: 0x04000821 RID: 2081
		public const string XmlInvalidFormat = "The input source is not correctly formatted.";

		// Token: 0x04000822 RID: 2082
		public const string XmlInvalidID = "ID must be >= 0.";

		// Token: 0x04000823 RID: 2083
		public const string XmlInvalidOperation = "The reader cannot be advanced.";

		// Token: 0x04000824 RID: 2084
		public const string XmlInvalidPrefixState = "A prefix cannot be defined while WriteState is '{0}'.";

		// Token: 0x04000825 RID: 2085
		public const string XmlInvalidQualifiedName = "Expected XML qualified name. Found '{0}'.";

		// Token: 0x04000826 RID: 2086
		public const string XmlInvalidRootData = "The data at the root level is invalid.";

		// Token: 0x04000827 RID: 2087
		public const string XmlInvalidStandalone = "'standalone' value in declaration must be 'yes' or 'no'.";

		// Token: 0x04000828 RID: 2088
		public const string XmlInvalidStream = "Stream returned by IStreamProvider cannot be null.";

		// Token: 0x04000829 RID: 2089
		public const string XmlInvalidUniqueId = "UniqueId cannot be zero length.";

		// Token: 0x0400082A RID: 2090
		public const string XmlInvalidUTF8Bytes = "'{0}' contains invalid UTF8 bytes.";

		// Token: 0x0400082B RID: 2091
		public const string XmlInvalidVersion = "XML version must be '1.0'.";

		// Token: 0x0400082C RID: 2092
		public const string XmlInvalidWriteState = "'{0}' cannot be called while WriteState is '{1}'.";

		// Token: 0x0400082D RID: 2093
		public const string XmlInvalidXmlByte = "The byte 0x{0} is not valid at this location.";

		// Token: 0x0400082E RID: 2094
		public const string XmlInvalidXmlSpace = "'{0}' is not a valid xml:space value. Valid values are 'default' and 'preserve'.";

		// Token: 0x0400082F RID: 2095
		public const string XmlLineInfo = "Line {0}, position {1}.";

		// Token: 0x04000830 RID: 2096
		public const string XmlMalformedDecl = "Malformed XML declaration.";

		// Token: 0x04000831 RID: 2097
		public const string XmlMaxArrayLengthExceeded = "The maximum array length quota ({0}) has been exceeded while reading XML data. This quota may be increased by changing the MaxArrayLength property on the XmlDictionaryReaderQuotas object used when creating the XML reader.";

		// Token: 0x04000832 RID: 2098
		public const string XmlMaxNameTableCharCountExceeded = "The maximum nametable character count quota ({0}) has been exceeded while reading XML data. The nametable is a data structure used to store strings encountered during XML processing - long XML documents with non-repeating element names, attribute names and attribute values may trigger this quota. This quota may be increased by changing the MaxNameTableCharCount property on the XmlDictionaryReaderQuotas object used when creating the XML reader.";

		// Token: 0x04000833 RID: 2099
		public const string XmlMethodNotSupported = "This XmlWriter implementation does not support the '{0}' method.";

		// Token: 0x04000834 RID: 2100
		public const string XmlMissingLowSurrogate = "The surrogate pair is invalid. Missing a low surrogate character.";

		// Token: 0x04000835 RID: 2101
		public const string XmlMultipleRootElements = "There are multiple root elements.";

		// Token: 0x04000836 RID: 2102
		public const string XmlNamespaceNotFound = "The namespace '{0}' is not defined.";

		// Token: 0x04000837 RID: 2103
		public const string XmlNestedArraysNotSupported = "Nested arrays are not supported.";

		// Token: 0x04000838 RID: 2104
		public const string XmlNoRootElement = "The document does not have a root element.";

		// Token: 0x04000839 RID: 2105
		public const string XmlOnlyOneRoot = "Only one root element is permitted per document.";

		// Token: 0x0400083A RID: 2106
		public const string XmlOnlyWhitespace = "Only white space characters can be written with this method.";

		// Token: 0x0400083B RID: 2107
		public const string XmlOnlySingleValue = "Only a single typed value may be written inside an attribute or content.";

		// Token: 0x0400083C RID: 2108
		public const string XmlPrefixBoundToNamespace = "The prefix '{0}' is bound to the namespace '{1}' and cannot be changed to '{2}'.";

		// Token: 0x0400083D RID: 2109
		public const string XmlProcessingInstructionNotSupported = "Processing instructions (other than the XML declaration) and DTDs are not supported.";

		// Token: 0x0400083E RID: 2110
		public const string XmlReservedPrefix = "Prefixes beginning with \"xml\" (regardless of casing) are reserved for use by XML.";

		// Token: 0x0400083F RID: 2111
		public const string XmlSpaceBetweenAttributes = "Whitespace must appear between attributes.";

		// Token: 0x04000840 RID: 2112
		public const string XmlSpecificBindingNamespace = "The namespace '{1}' can only be bound to the prefix '{0}'.";

		// Token: 0x04000841 RID: 2113
		public const string XmlSpecificBindingPrefix = "The prefix '{0}' can only be bound to the namespace '{1}'.";

		// Token: 0x04000842 RID: 2114
		public const string XmlStartElementLocalNameNsExpected = "Start element '{0}' from namespace '{1}' expected. Found {2}.";

		// Token: 0x04000843 RID: 2115
		public const string XmlStartElementNameExpected = "Start element '{0}' expected. Found {1}.";

		// Token: 0x04000844 RID: 2116
		public const string XmlTagMismatch = "Start element '{0}' does not match end element '{1}'.";

		// Token: 0x04000845 RID: 2117
		public const string XmlTokenExpected = "The token '{0}' was expected but found '{1}'.";

		// Token: 0x04000846 RID: 2118
		public const string XmlUndefinedPrefix = "The prefix '{0}' is not defined.";

		// Token: 0x04000847 RID: 2119
		public const string XmlUnexpectedEndElement = "No matching start tag for end element.";

		// Token: 0x04000848 RID: 2120
		public const string XmlUnexpectedEndOfFile = "Unexpected end of file. Following elements are not closed: {0}.";

		// Token: 0x04000849 RID: 2121
		public const string XmlWriterClosed = "The XmlWriter is closed.";

		// Token: 0x0400084A RID: 2122
		public const string Xml_InvalidNmToken = "Invalid NmToken value '{0}'.";

		// Token: 0x0400084B RID: 2123
		public const string AbstractElementNotSupported = "Abstract element '{0}' is not supported.";

		// Token: 0x0400084C RID: 2124
		public const string AbstractTypeNotSupported = "Abstract type is not supported";

		// Token: 0x0400084D RID: 2125
		public const string AmbiguousReferencedCollectionTypes1 = "Ambiguous collection types were referenced: {0}";

		// Token: 0x0400084E RID: 2126
		public const string AmbiguousReferencedCollectionTypes3 = "In '{0}' element in '{1}' namespace, ambiguous collection types were referenced: {2}";

		// Token: 0x0400084F RID: 2127
		public const string AmbiguousReferencedTypes1 = "Ambiguous types were referenced: {0}";

		// Token: 0x04000850 RID: 2128
		public const string AmbiguousReferencedTypes3 = "In '{0}' element in '{1}' namespace, ambiguous types were referenced: {2}";

		// Token: 0x04000851 RID: 2129
		public const string AnnotationAttributeNotFound = "Annotation attribute was not found: default value annotation is '{0}', type is '{1}' in '{2}' namespace, emit default value is {3}.";

		// Token: 0x04000852 RID: 2130
		public const string AnonymousTypeNotSupported = "Anonymous type is not supported. Type is '{0}' in '{1}' namespace.";

		// Token: 0x04000853 RID: 2131
		public const string AnyAttributeNotSupported = "XML Schema 'any' attribute is not supported";

		// Token: 0x04000854 RID: 2132
		public const string ArrayItemFormMustBe = "For array item, element 'form' must be {0}.";

		// Token: 0x04000855 RID: 2133
		public const string ArraySizeAttributeIncorrect = "Array size attribute is incorrect; must be between {0} and {1}.";

		// Token: 0x04000856 RID: 2134
		public const string ArrayTypeCannotBeImported = "Array type cannot be imported for '{0}' in '{1}' namespace: {2}.";

		// Token: 0x04000857 RID: 2135
		public const string AssemblyNotFound = "Assembly '{0}' was not found.";

		// Token: 0x04000858 RID: 2136
		public const string AttributeNotFound = "Attribute was not found for CLR type '{1}' in namespace '{0}'. XML reader node is on {2}, '{4}' node in '{3}' namespace.";

		// Token: 0x04000859 RID: 2137
		public const string BaseTypeNotISerializable = "Base type '{0}' in '{1}' namespace is not ISerializable.";

		// Token: 0x0400085A RID: 2138
		public const string CannotComputeUniqueName = "Cannot compute unique name for '{0}'.";

		// Token: 0x0400085B RID: 2139
		public const string CannotDeriveFromSealedReferenceType = "Cannod drive from sealed reference type '{2}', for '{0}' element in '{1}' namespace.";

		// Token: 0x0400085C RID: 2140
		public const string CannotDeserializeForwardedType = "Cannot deserialize forwarded type '{0}'.";

		// Token: 0x0400085D RID: 2141
		public const string CannotExportNullAssembly = "Cannot export null assembly.";

		// Token: 0x0400085E RID: 2142
		public const string CannotExportNullKnownType = "Cannot export null known type.";

		// Token: 0x0400085F RID: 2143
		public const string CannotExportNullType = "Cannot export null type.";

		// Token: 0x04000860 RID: 2144
		public const string CannotHaveDuplicateAttributeNames = "Cannot have duplicate attribute names '{0}'.";

		// Token: 0x04000861 RID: 2145
		public const string CannotHaveDuplicateElementNames = "Cannot have duplicate element names '{0}'.";

		// Token: 0x04000862 RID: 2146
		public const string CannotImportInvalidSchemas = "Cannot import invalid schemas.";

		// Token: 0x04000863 RID: 2147
		public const string CannotImportNullDataContractName = "Cannot import data contract with null name.";

		// Token: 0x04000864 RID: 2148
		public const string CannotImportNullSchema = "Cannot import from schema list that contains null.";

		// Token: 0x04000865 RID: 2149
		public const string CannotSetMembersForReferencedType = "Cannot set members for already referenced type. Base type is '{0}'.";

		// Token: 0x04000866 RID: 2150
		public const string CannotSetNamespaceForReferencedType = "Cannot set namespace for already referenced type. Base type is '{0}'.";

		// Token: 0x04000867 RID: 2151
		public const string CannotUseGenericTypeAsBase = "For '{0}' in '{1}' namespace, generic type cannot be referenced as the base type.";

		// Token: 0x04000868 RID: 2152
		public const string ChangingFullTypeNameNotSupported = "Changing full type name is not supported. Serialization type name: '{0}', data contract type name: '{1}'.";

		// Token: 0x04000869 RID: 2153
		public const string CircularTypeReference = "Circular type reference was found for '{0}' in '{1}' namespace.";

		// Token: 0x0400086A RID: 2154
		public const string ClassDataContractReturnedForGetOnlyCollection = "For '{0}' type, class data contract was returned for get-only collection.";

		// Token: 0x0400086B RID: 2155
		public const string CLRNamespaceMappedMultipleTimes = "CLR namespace is mapped multiple times. Current data contract namespace is '{0}', found '{1}' for CLR namespace '{2}'.";

		// Token: 0x0400086C RID: 2156
		public const string ClrTypeNotFound = "CLR type '{1}' in assembly '{0}' is not found.";

		// Token: 0x0400086D RID: 2157
		public const string CollectionAssignedToIncompatibleInterface = "Collection of type '{0}' is assigned to an incompatible interface '{1}'";

		// Token: 0x0400086E RID: 2158
		public const string ComplexTypeRestrictionNotSupported = "XML schema complexType restriction is not supported.";

		// Token: 0x0400086F RID: 2159
		public const string ConfigDataContractSerializerSectionLoadError = "Failed to load configuration section for dataContractSerializer.";

		// Token: 0x04000870 RID: 2160
		public const string ConfigIndexOutOfRange = "For type '{0}', configuration index is out of range.";

		// Token: 0x04000871 RID: 2161
		public const string ConfigMustOnlyAddParamsWithType = "Configuration parameter element must only add params with type.";

		// Token: 0x04000872 RID: 2162
		public const string ConfigMustOnlySetTypeOrIndex = "Configuration parameter element can set only one of either type or index.";

		// Token: 0x04000873 RID: 2163
		public const string ConfigMustSetTypeOrIndex = "Configuration parameter element must set either type or index.";

		// Token: 0x04000874 RID: 2164
		public const string CouldNotReadSerializationSchema = "Could not read serialization schema for '{0}' namespace.";

		// Token: 0x04000875 RID: 2165
		public const string DefaultOnElementNotSupported = "On element '{0}', default value is not supported.";

		// Token: 0x04000876 RID: 2166
		public const string DerivedTypeNotISerializable = "On type '{0}' in '{1}' namespace, derived type is not ISerializable.";

		// Token: 0x04000877 RID: 2167
		public const string DupContractInDataContractSet = "Duplicate contract in data contract set was found, for '{0}' in '{1}' namespace.";

		// Token: 0x04000878 RID: 2168
		public const string DuplicateExtensionDataSetMethod = "Duplicate extension data set method was found, for method '{0}', existing method is '{1}', on data contract type '{2}'.";

		// Token: 0x04000879 RID: 2169
		public const string DupTypeContractInDataContractSet = "Duplicate type contract in data contract set. Type name '{0}', for data contract '{1}' in '{2}' namespace.";

		// Token: 0x0400087A RID: 2170
		public const string ElementMaxOccursMustBe = "On element '{0}', schema element maxOccurs must be 1.";

		// Token: 0x0400087B RID: 2171
		public const string ElementMinOccursMustBe = "On element '{0}', schema element minOccurs must be less or equal to 1.";

		// Token: 0x0400087C RID: 2172
		public const string ElementRefOnLocalElementNotSupported = "For local element, ref is not supported. The referenced name is '{0}' in '{1}' namespace.";

		// Token: 0x0400087D RID: 2173
		public const string EnumEnumerationFacetsMustHaveValue = "Schema enumeration facet must have values.";

		// Token: 0x0400087E RID: 2174
		public const string EnumListInAnonymousTypeNotSupported = "Enum list in anonymous type is not supported.";

		// Token: 0x0400087F RID: 2175
		public const string EnumListMustContainAnonymousType = "Enum list must contain an anonymous type.";

		// Token: 0x04000880 RID: 2176
		public const string EnumOnlyEnumerationFacetsSupported = "For schema facets, only enumeration is supported.";

		// Token: 0x04000881 RID: 2177
		public const string EnumRestrictionInvalid = "For simpleType restriction, only enum is supported and this type could not be convert to enum.";

		// Token: 0x04000882 RID: 2178
		public const string EnumTypeCannotBeImported = "For '{0}' in '{1}' namespace, enum type cannot be imported: {2}";

		// Token: 0x04000883 RID: 2179
		public const string EnumTypeNotSupportedByDataContractJsonSerializer = "Enum type is not supported by DataContractJsonSerializer. The underlying type is '{0}'.";

		// Token: 0x04000884 RID: 2180
		public const string EnumUnionInAnonymousTypeNotSupported = "Enum union in anonymous type is not supported.";

		// Token: 0x04000885 RID: 2181
		public const string ExtensionDataSetMustReturnVoid = "For type '{0}' method '{1}', extension data set method must return void.";

		// Token: 0x04000886 RID: 2182
		public const string ExtensionDataSetParameterInvalid = "For type '{0}' method '{1}', extension data set method has invalid type of parameter '{2}'.";

		// Token: 0x04000887 RID: 2183
		public const string FactoryObjectContainsSelfReference = "Factory object contains a reference to self. Old object is '{0}', new object is '{1}'.";

		// Token: 0x04000888 RID: 2184
		public const string FactoryTypeNotISerializable = "For data contract '{1}', factory type '{0}' is not ISerializable.";

		// Token: 0x04000889 RID: 2185
		public const string FixedOnElementNotSupported = "On schema element '{0}', fixed value is not supported.";

		// Token: 0x0400088A RID: 2186
		public const string FlushBufferAlreadyInUse = "Flush buffer is already in use.";

		// Token: 0x0400088B RID: 2187
		public const string FormMustBeQualified = "On schema element '{0}', form must be qualified.";

		// Token: 0x0400088C RID: 2188
		public const string GenericAnnotationAttributeNotFound = "On type '{0}' Generic annotation attribute '{1}' was not found.";

		// Token: 0x0400088D RID: 2189
		public const string GenericAnnotationForNestedLevelMustBeIncreasing = "On type '{2}', generic annotation for nested level must be increasing. Argument element is '{0}' in '{1}' namespace.";

		// Token: 0x0400088E RID: 2190
		public const string GenericAnnotationHasInvalidAttributeValue = "On type '{2}', generic annotation has invalid attribute value '{3}'. Argument element is '{0}' in '{1}' namespace. Nested level attribute attribute name is '{4}'. Type is '{5}'.";

		// Token: 0x0400088F RID: 2191
		public const string GenericAnnotationHasInvalidElement = "On type '{2}', generic annotation has invalid element. Argument element is '{0}' in '{1}' namespace.";

		// Token: 0x04000890 RID: 2192
		public const string GenericTypeNameMismatch = "Generic type name mismatch. Expected '{0}' in '{1}' namespace, got '{2}' in '{3}' namespace instead.";

		// Token: 0x04000891 RID: 2193
		public const string GenericTypeNotExportable = "Generic type '{0}' is not exportable.";

		// Token: 0x04000892 RID: 2194
		public const string GetOnlyCollectionMustHaveAddMethod = "On type '{0}', get-only collection must have an Add method.";

		// Token: 0x04000893 RID: 2195
		public const string GetRealObjectReturnedNull = "On the surrogate data contract for '{0}', GetRealObject method returned null.";

		// Token: 0x04000894 RID: 2196
		public const string InvalidAnnotationExpectingText = "For annotation element '{0}' in namespace '{1}', expected text but got element '{2}' in '{3}' namespace.";

		// Token: 0x04000895 RID: 2197
		public const string InvalidAssemblyFormat = "'{0}': invalid assembly format.";

		// Token: 0x04000896 RID: 2198
		public const string InvalidCharacterEncountered = "Encountered an invalid character '{0}'.";

		// Token: 0x04000897 RID: 2199
		public const string InvalidClassDerivation = "Invalid class derivation from '{0}' in '{1}' namespace.";

		// Token: 0x04000898 RID: 2200
		public const string InvalidClrNameGeneratedForISerializable = "Invalid CLR name '{2}' is generated for ISerializable type '{0}' in '{1}' namespace.";

		// Token: 0x04000899 RID: 2201
		public const string InvalidClrNamespaceGeneratedForISerializable = "Invalid CLR namespace '{3}' is generated for ISerializable type '{0}' in '{1}' namespace. Data contract namespace from the URI would be generated as '{2}'.";

		// Token: 0x0400089A RID: 2202
		public const string InvalidDataNode = "Invalid data node for '{0}' type.";

		// Token: 0x0400089B RID: 2203
		public const string InvalidEmitDefaultAnnotation = "Invalid EmilDefault annotation for '{0}' in type '{1}' in '{2}' namespace.";

		// Token: 0x0400089C RID: 2204
		public const string InvalidEnumBaseType = "Invalid enum base type is specified for type '{0}' in '{1}' namespace, element name is '{2}' in '{3}' namespace.";

		// Token: 0x0400089D RID: 2205
		public const string InvalidISerializableDerivation = "Invalid ISerializable derivation from '{0}' in '{1}' namespace.";

		// Token: 0x0400089E RID: 2206
		public const string InvalidKeyValueType = "'{0}' is an invalid key value type.";

		// Token: 0x0400089F RID: 2207
		public const string InvalidKeyValueTypeNamespace = "'{0}' in '{1}' namespace is an invalid key value type.";

		// Token: 0x040008A0 RID: 2208
		public const string InvalidReturnSchemaOnGetSchemaMethod = "On type '{0}', the return value from GetSchema method was invalid.";

		// Token: 0x040008A1 RID: 2209
		public const string InvalidStateInExtensionDataReader = "Invalid state in extension data reader.";

		// Token: 0x040008A2 RID: 2210
		public const string InvalidXmlDeserializingExtensionData = "Invalid XML while deserializing extension data.";

		// Token: 0x040008A3 RID: 2211
		public const string IsAnyNotSupportedByNetDataContractSerializer = "For type '{0}', IsAny is not supported by NetDataContractSerializer.";

		// Token: 0x040008A4 RID: 2212
		public const string IsDictionaryFormattedIncorrectly = "IsDictionary formatted value '{0}' is incorrect: {1}";

		// Token: 0x040008A5 RID: 2213
		public const string ISerializableAssemblyNameSetToZero = "ISerializable AssemblyName is set to \"0\" for type '{0}'.";

		// Token: 0x040008A6 RID: 2214
		public const string ISerializableCannotHaveDataContract = "ISerializable type '{0}' cannot have DataContract.";

		// Token: 0x040008A7 RID: 2215
		public const string ISerializableContainsMoreThanOneItems = "ISerializable cannot contain more than one item.";

		// Token: 0x040008A8 RID: 2216
		public const string ISerializableDerivedContainsOneOrMoreItems = "Type derived from ISerializable cannot contain more than one item.";

		// Token: 0x040008A9 RID: 2217
		public const string ISerializableDoesNotContainAny = "ISerializable does not contain any element.";

		// Token: 0x040008AA RID: 2218
		public const string ISerializableMustRefFactoryTypeAttribute = "ISerializable must have ref attribute that points to its factory type.";

		// Token: 0x040008AB RID: 2219
		public const string ISerializableTypeCannotBeImported = "ISerializable type '{0}' in '{1}' namespace cannot be imported: {2}";

		// Token: 0x040008AC RID: 2220
		public const string ISerializableWildcardMaxOccursMustBe = "ISerializable wildcard maxOccurs must be '{0}'.";

		// Token: 0x040008AD RID: 2221
		public const string ISerializableWildcardMinOccursMustBe = "ISerializable wildcard maxOccurs must be '{0}'.";

		// Token: 0x040008AE RID: 2222
		public const string ISerializableWildcardNamespaceInvalid = "ISerializable wildcard namespace is invalid: '{0}'.";

		// Token: 0x040008AF RID: 2223
		public const string ISerializableWildcardProcessContentsInvalid = "ISerializable wildcard processContents is invalid: '{0}'.";

		// Token: 0x040008B0 RID: 2224
		public const string IsReferenceGetOnlyCollectionsNotSupported = "On type '{1}', attribute '{0}' points to get-only collection, which is not supported.";

		// Token: 0x040008B1 RID: 2225
		public const string IsValueTypeFormattedIncorrectly = "IsValueType is formatted incorrectly as '{0}': {1}";

		// Token: 0x040008B2 RID: 2226
		public const string JsonAttributeAlreadyWritten = "JSON attribute '{0}' is already written.";

		// Token: 0x040008B3 RID: 2227
		public const string JsonAttributeMustHaveElement = "JSON attribute must have an owner element.";

		// Token: 0x040008B4 RID: 2228
		public const string JsonCannotWriteStandaloneTextAfterQuotedText = "JSON writer cannot write standalone text after quoted text.";

		// Token: 0x040008B5 RID: 2229
		public const string JsonCannotWriteTextAfterNonTextAttribute = "JSON writer cannot write text after non-text attribute. Data type is '{0}'.";

		// Token: 0x040008B6 RID: 2230
		public const string JsonDateTimeOutOfRange = "JSON DateTime is out of range.";

		// Token: 0x040008B7 RID: 2231
		public const string JsonDuplicateMemberInInput = "Duplicate member '{0}' is found in JSON input.";

		// Token: 0x040008B8 RID: 2232
		public const string JsonDuplicateMemberNames = "Duplicate member, including '{1}', is found in JSON input, in type '{0}'.";

		// Token: 0x040008B9 RID: 2233
		public const string JsonEncodingNotSupported = "JSON Encoding is not supported.";

		// Token: 0x040008BA RID: 2234
		public const string JsonEncounteredUnexpectedCharacter = "Encountered an unexpected character '{0}' in JSON.";

		// Token: 0x040008BB RID: 2235
		public const string JsonEndElementNoOpenNodes = "Encountered an end element while there was no open element in JSON writer.";

		// Token: 0x040008BC RID: 2236
		public const string JsonExpectedEncoding = "Expected encoding '{0}', got '{1}' instead.";

		// Token: 0x040008BD RID: 2237
		public const string JsonInvalidBytes = "Invalid bytes in JSON.";

		// Token: 0x040008BE RID: 2238
		public const string JsonInvalidDataTypeSpecifiedForServerType = "The specified data type is invalid for server type. Type: '{0}', specified data type: '{1}', server type: '{2}', object '{3}'.";

		// Token: 0x040008BF RID: 2239
		public const string JsonInvalidDateTimeString = "Invalid JSON dateTime string is specified: original value '{0}', start guide writer: {1}, end guard writer: {2}.";

		// Token: 0x040008C0 RID: 2240
		public const string JsonInvalidFFFE = "FFFE in JSON is invalid.";

		// Token: 0x040008C1 RID: 2241
		public const string JsonInvalidItemNameForArrayElement = "Invalid JSON item name '{0}' for array element (item element is '{1}' in JSON).";

		// Token: 0x040008C2 RID: 2242
		public const string JsonInvalidLocalNameEmpty = "Empty string is invalid as a local name.";

		// Token: 0x040008C3 RID: 2243
		public const string JsonInvalidMethodBetweenStartEndAttribute = "Invalid method call state between start and end attribute.";

		// Token: 0x040008C4 RID: 2244
		public const string JsonInvalidRootElementName = "Invalid root element name '{0}' (root element is '{1}' in JSON).";

		// Token: 0x040008C5 RID: 2245
		public const string JsonInvalidStartElementCall = "Invalid call to JSON WriteStartElement method.";

		// Token: 0x040008C6 RID: 2246
		public const string JsonInvalidWriteState = "Invalid write state {1} for '{0}' method.";

		// Token: 0x040008C7 RID: 2247
		public const string JsonMethodNotSupported = "Method {0} is not supported in JSON.";

		// Token: 0x040008C8 RID: 2248
		public const string JsonMultipleRootElementsNotAllowedOnWriter = "Multiple root element is not allowed on JSON writer.";

		// Token: 0x040008C9 RID: 2249
		public const string JsonMustSpecifyDataType = "On JSON writer data type '{0}' must be specified. Object string is '{1}', server type string is '{2}'.";

		// Token: 0x040008CA RID: 2250
		public const string JsonMustUseWriteStringForWritingAttributeValues = "On JSON writer WriteString must be used for writing attribute values.";

		// Token: 0x040008CB RID: 2251
		public const string JsonNamespaceMustBeEmpty = "JSON namespace is specified as '{0}' but it must be empty.";

		// Token: 0x040008CC RID: 2252
		public const string JsonNestedArraysNotSupported = "Nested array is not supported in JSON: '{0}'";

		// Token: 0x040008CD RID: 2253
		public const string JsonNodeTypeArrayOrObjectNotSpecified = "Either Object or Array of JSON node type must be specified.";

		// Token: 0x040008CE RID: 2254
		public const string JsonNoMatchingStartAttribute = "WriteEndAttribute was called while there is no open attribute.";

		// Token: 0x040008CF RID: 2255
		public const string JsonOffsetExceedsBufferSize = "On JSON writer, offset exceeded buffer size {0}.";

		// Token: 0x040008D0 RID: 2256
		public const string JsonOneRequiredMemberNotFound = "Required member {1} in type '{0}' is not found.";

		// Token: 0x040008D1 RID: 2257
		public const string JsonOnlyWhitespace = "Only whitespace characters are allowed for {1} method. The specified value is '{0}'";

		// Token: 0x040008D2 RID: 2258
		public const string JsonOpenAttributeMustBeClosedFirst = "JSON attribute must be closed first before calling {0} method.";

		// Token: 0x040008D3 RID: 2259
		public const string JsonPrefixMustBeNullOrEmpty = "JSON prefix must be null or empty. '{0}' is specified instead.";

		// Token: 0x040008D4 RID: 2260
		public const string JsonRequiredMembersNotFound = "Required members {0} in type '{1}' are not found.";

		// Token: 0x040008D5 RID: 2261
		public const string JsonServerTypeSpecifiedForInvalidDataType = "Server type is specified for invalid data type in JSON. Server type: '{0}', type: '{1}', dataType: '{2}', object: '{3}'.";

		// Token: 0x040008D6 RID: 2262
		public const string JsonSizeExceedsRemainingBufferSpace = "JSON size exceeded remaining buffer space, by {0} byte(s).";

		// Token: 0x040008D7 RID: 2263
		public const string JsonTypeNotSupportedByDataContractJsonSerializer = "Type '{0}' is not suppotred by DataContractJsonSerializer.";

		// Token: 0x040008D8 RID: 2264
		public const string JsonUnexpectedAttributeLocalName = "Unexpected attribute local name '{0}'.";

		// Token: 0x040008D9 RID: 2265
		public const string JsonUnexpectedAttributeValue = "Unexpected attribute value '{0}'.";

		// Token: 0x040008DA RID: 2266
		public const string JsonUnexpectedEndOfFile = "Unexpected end of file in JSON.";

		// Token: 0x040008DB RID: 2267
		public const string JsonUnsupportedForIsReference = "Unsupported value for IsReference for type '{0}', IsReference value is {1}.";

		// Token: 0x040008DC RID: 2268
		public const string JsonWriteArrayNotSupported = "JSON WriteArray is not supported.";

		// Token: 0x040008DD RID: 2269
		public const string JsonWriterClosed = "JSON writer is already closed.";

		// Token: 0x040008DE RID: 2270
		public const string JsonXmlInvalidDeclaration = "Attempt to write invalid XML declration.";

		// Token: 0x040008DF RID: 2271
		public const string JsonXmlProcessingInstructionNotSupported = "processing instruction is not supported in JSON writer.";

		// Token: 0x040008E0 RID: 2272
		public const string KeyTypeCannotBeParsedInSimpleDictionary = "Key type '{1}' for collection type '{0}' cannot be parsed in simple dictionary.";

		// Token: 0x040008E1 RID: 2273
		public const string KnownTypeConfigGenericParamMismatch = "Generic parameter count do not match between known type and configuration. Type is '{0}', known type has {1} parameters, configuration has {2} parameters.";

		// Token: 0x040008E2 RID: 2274
		public const string KnownTypeConfigIndexOutOfBounds = "For known type configuration, index is out of bound. Root type: '{0}' has {1} type arguments, and index was {2}.";

		// Token: 0x040008E3 RID: 2275
		public const string KnownTypeConfigIndexOutOfBoundsZero = "For known type configuration, index is out of bound. Root type: '{0}' has {1} type arguments, and index was {2}.";

		// Token: 0x040008E4 RID: 2276
		public const string KnownTypeConfigObject = "Known type configuration specifies System.Object.";

		// Token: 0x040008E5 RID: 2277
		public const string MaxMimePartsExceeded = "MIME parts number exceeded the maximum settings. Must be less than {0}. Specified as '{1}'.";

		// Token: 0x040008E6 RID: 2278
		public const string MimeContentTypeHeaderInvalid = "MIME content type header is invalid.";

		// Token: 0x040008E7 RID: 2279
		public const string MimeHeaderInvalidCharacter = "MIME header has an invalid character ('{0}', {1} in hexadecimal value).";

		// Token: 0x040008E8 RID: 2280
		public const string MimeMessageGetContentStreamCalledAlready = "On MimeMessage, GetContentStream method is already called.";

		// Token: 0x040008E9 RID: 2281
		public const string MimeReaderHeaderAlreadyExists = "MIME header '{0}' already exists.";

		// Token: 0x040008EA RID: 2282
		public const string MimeReaderMalformedHeader = "Malformed MIME header.";

		// Token: 0x040008EB RID: 2283
		public const string MimeReaderResetCalledBeforeEOF = "On MimeReader, Reset method is called before EOF.";

		// Token: 0x040008EC RID: 2284
		public const string MimeReaderTruncated = "MIME parts are truncated.";

		// Token: 0x040008ED RID: 2285
		public const string MimeVersionHeaderInvalid = "MIME version header is invalid.";

		// Token: 0x040008EE RID: 2286
		public const string MimeWriterInvalidStateForClose = "MIME writer is at invalid state for closing.";

		// Token: 0x040008EF RID: 2287
		public const string MimeWriterInvalidStateForContent = "MIME writer is at invalid state for content.";

		// Token: 0x040008F0 RID: 2288
		public const string MimeWriterInvalidStateForHeader = "MIME writer is at invalid state for header.";

		// Token: 0x040008F1 RID: 2289
		public const string MimeWriterInvalidStateForStartPart = "MIME writer is at invalid state for starting a part.";

		// Token: 0x040008F2 RID: 2290
		public const string MimeWriterInvalidStateForStartPreface = "MIME writer is at invalid state for starting preface.";

		// Token: 0x040008F3 RID: 2291
		public const string MissingSchemaType = "Schema type '{0}' is missing and required for '{1}' type.";

		// Token: 0x040008F4 RID: 2292
		public const string MixedContentNotSupported = "Mixed content is not supported.";

		// Token: 0x040008F5 RID: 2293
		public const string MtomBoundaryInvalid = "MIME boundary is invalid: '{0}'.";

		// Token: 0x040008F6 RID: 2294
		public const string MtomBufferQuotaExceeded = "MTOM buffer quota exceeded. The maximum size is {0}.";

		// Token: 0x040008F7 RID: 2295
		public const string MtomContentTransferEncodingNotPresent = "MTOM content transfer encoding is not present. ContentTransferEncoding header is '{0}'.";

		// Token: 0x040008F8 RID: 2296
		public const string MtomContentTransferEncodingNotSupported = "MTOM content transfer encoding value is not supported. Raw value is '{0}', '{1}' in 7bit encoding, '{2}' in 8bit encoding, and '{3}' in binary.";

		// Token: 0x040008F9 RID: 2297
		public const string MtomContentTypeInvalid = "MTOM content type is invalid.";

		// Token: 0x040008FA RID: 2298
		public const string MtomDataMustNotContainXopInclude = "MTOM data must not contain xop:Include element. '{0}' element in '{1}' namespace.";

		// Token: 0x040008FB RID: 2299
		public const string MtomExceededMaxSizeInBytes = "MTOM exceeded max size in bytes. The maximum size is {0}.";

		// Token: 0x040008FC RID: 2300
		public const string MtomInvalidCIDUri = "Invalid MTOM CID URI: '{0}'.";

		// Token: 0x040008FD RID: 2301
		public const string MtomInvalidEmptyURI = "empty URI is invalid for MTOM MIME part.";

		// Token: 0x040008FE RID: 2302
		public const string MtomInvalidStartUri = "Invalid MTOM start URI: '{0}'.";

		// Token: 0x040008FF RID: 2303
		public const string MtomInvalidTransferEncodingForMimePart = "Invalid transfer encoding for MIME part: '{0}', in binary: '{1}'.";

		// Token: 0x04000900 RID: 2304
		public const string MtomMessageContentTypeNotFound = "MTOM message content type was not found.";

		// Token: 0x04000901 RID: 2305
		public const string MtomMessageInvalidContent = "MTOM message content is invalid.";

		// Token: 0x04000902 RID: 2306
		public const string MtomMessageInvalidContentInMimePart = "MTOM message content in MIME part is invalid.";

		// Token: 0x04000903 RID: 2307
		public const string MtomMessageInvalidMimeVersion = "MTOM message has invalid MIME version. Expected '{1}', got '{0}' instead.";

		// Token: 0x04000904 RID: 2308
		public const string MtomMessageNotApplicationXopXml = "MTOM msssage type is not '{0}'.";

		// Token: 0x04000905 RID: 2309
		public const string MtomMessageNotMultipart = "MTOM message is not multipart: media type should be '{0}', media subtype should be '{1}'.";

		// Token: 0x04000906 RID: 2310
		public const string MtomMessageRequiredParamNotSpecified = "Required MTOM parameter '{0}' is not specified.";

		// Token: 0x04000907 RID: 2311
		public const string MtomMimePartReferencedMoreThanOnce = "Specified MIME part '{0}' is referenced more than once.";

		// Token: 0x04000908 RID: 2312
		public const string MtomPartNotFound = "MTOM part with URI '{0}' is not found.";

		// Token: 0x04000909 RID: 2313
		public const string MtomRootContentTypeNotFound = "MTOM root content type is not found.";

		// Token: 0x0400090A RID: 2314
		public const string MtomRootNotApplicationXopXml = "MTOM root should have media type '{0}' and subtype '{1}'.";

		// Token: 0x0400090B RID: 2315
		public const string MtomRootPartNotFound = "MTOM root part is not found.";

		// Token: 0x0400090C RID: 2316
		public const string MtomRootRequiredParamNotSpecified = "Required MTOM root parameter '{0}' is not specified.";

		// Token: 0x0400090D RID: 2317
		public const string MtomRootUnexpectedCharset = "Unexpected charset on MTOM root. Expected '{1}', got '{0}' instead.";

		// Token: 0x0400090E RID: 2318
		public const string MtomRootUnexpectedType = "Unexpected type on MTOM root. Expected '{1}', got '{0}' instead.";

		// Token: 0x0400090F RID: 2319
		public const string MtomXopIncludeHrefNotSpecified = "xop Include element did not specify '{0}' attribute.";

		// Token: 0x04000910 RID: 2320
		public const string MtomXopIncludeInvalidXopAttributes = "xop Include element has invalid attribute: '{0}' in '{1}' namespace.";

		// Token: 0x04000911 RID: 2321
		public const string MtomXopIncludeInvalidXopElement = "xop Include element has invalid element: '{0}' in '{1}' namespace.";

		// Token: 0x04000912 RID: 2322
		public const string MustContainOnlyLocalElements = "Only local elements can be imported.";

		// Token: 0x04000913 RID: 2323
		public const string NoAsyncWritePending = "No async write operation is pending.";

		// Token: 0x04000914 RID: 2324
		public const string NonOptionalFieldMemberOnIsReferenceSerializableType = "For type '{0}', non-optional field member '{1}' is on the Serializable type that has IsReference as {2}.";

		// Token: 0x04000915 RID: 2325
		public const string OnlyDataContractTypesCanHaveExtensionData = "On '{0}' type, only DataContract types can have extension data.";

		// Token: 0x04000916 RID: 2326
		public const string PartialTrustISerializableNoPublicConstructor = "Partial trust access required for the constructor on the ISerializable type '{0}'";

		// Token: 0x04000917 RID: 2327
		public const string QueryGeneratorPathToMemberNotFound = "The path to member was not found for XPath query generator.";

		// Token: 0x04000918 RID: 2328
		public const string ReadNotSupportedOnStream = "Read operation is not supported on the Stream.";

		// Token: 0x04000919 RID: 2329
		public const string ReadOnlyClassDeserialization = "Error on deserializing read-only members in the class: {0}";

		// Token: 0x0400091A RID: 2330
		public const string ReadOnlyCollectionDeserialization = "Error on deserializing read-only collection: {0}";

		// Token: 0x0400091B RID: 2331
		public const string RecursiveCollectionType = "Type '{0}' involves recursive collection.";

		// Token: 0x0400091C RID: 2332
		public const string RedefineNotSupported = "XML Schema 'redefine' is not supported.";

		// Token: 0x0400091D RID: 2333
		public const string ReferencedBaseTypeDoesNotExist = "Referenced base type does not exist. Data contract name: '{0}' in '{1}' namespace, expected type: '{2}' in '{3}' namespace. Collection can be '{4}' or '{5}'.";

		// Token: 0x0400091E RID: 2334
		public const string ReferencedCollectionTypesCannotContainNull = "Referenced collection types cannot contain null.";

		// Token: 0x0400091F RID: 2335
		public const string ReferencedTypeDoesNotMatch = "Referenced type '{0}' does not match the expected type '{1}' in '{2}' namespace.";

		// Token: 0x04000920 RID: 2336
		public const string ReferencedTypeMatchingMessage = "Reference type matches.";

		// Token: 0x04000921 RID: 2337
		public const string ReferencedTypeNotMatchingMessage = "Reference type does not match.";

		// Token: 0x04000922 RID: 2338
		public const string ReferencedTypesCannotContainNull = "Referenced types cannot contain null.";

		// Token: 0x04000923 RID: 2339
		public const string RequiresClassDataContractToSetIsISerializable = "To set IsISerializable, class data cotnract is required.";

		// Token: 0x04000924 RID: 2340
		public const string RootParticleMustBeSequence = "Root particle must be sequence to be imported.";

		// Token: 0x04000925 RID: 2341
		public const string RootSequenceMaxOccursMustBe = "On root sequence, maxOccurs must be 1.";

		// Token: 0x04000926 RID: 2342
		public const string RootSequenceMustBeRequired = "Root sequence must have an item and minOccurs must be 1.";

		// Token: 0x04000927 RID: 2343
		public const string SeekNotSupportedOnStream = "Seek operation is not supported on this Stream.";

		// Token: 0x04000928 RID: 2344
		public const string SerializationInfo_ConstructorNotFound = "Constructor that takes SerializationInfo and StreamingContext is not found for '{0}'.";

		// Token: 0x04000929 RID: 2345
		public const string SimpleContentNotSupported = "Simple content is not supported.";

		// Token: 0x0400092A RID: 2346
		public const string SimpleTypeRestrictionDoesNotSpecifyBase = "This simpleType restriction does not specify the base type.";

		// Token: 0x0400092B RID: 2347
		public const string SimpleTypeUnionNotSupported = "simpleType union is not supported.";

		// Token: 0x0400092C RID: 2348
		public const string SpecifiedTypeNotFoundInSchema = "Specified type '{0}' in '{1}' namespace is not found in the schemas.";

		// Token: 0x0400092D RID: 2349
		public const string SubstitutionGroupOnElementNotSupported = "substitutionGroups on elements are not supported.";

		// Token: 0x0400092E RID: 2350
		public const string SurrogatesWithGetOnlyCollectionsNotSupported = "Surrogates with get-only collections are not supported. Type '{1}' contains '{2}' which is of '{0}' type.";

		// Token: 0x0400092F RID: 2351
		public const string SurrogatesWithGetOnlyCollectionsNotSupportedSerDeser = "Surrogates with get-only collections are not supported. Found on type '{0}'.";

		// Token: 0x04000930 RID: 2352
		public const string TopLevelElementRepresentsDifferentType = "Top-level element represents a different type. Expected '{0}' type in '{1}' namespace.";

		// Token: 0x04000931 RID: 2353
		public const string TraceCodeElementIgnored = "Element ignored";

		// Token: 0x04000932 RID: 2354
		public const string TraceCodeFactoryTypeNotFound = "Factory type not found";

		// Token: 0x04000933 RID: 2355
		public const string TraceCodeObjectWithLargeDepth = "Object with large depth";

		// Token: 0x04000934 RID: 2356
		public const string TraceCodeReadObjectBegin = "ReadObject begins";

		// Token: 0x04000935 RID: 2357
		public const string TraceCodeReadObjectEnd = "ReadObject ends";

		// Token: 0x04000936 RID: 2358
		public const string TraceCodeWriteObjectBegin = "WriteObject begins";

		// Token: 0x04000937 RID: 2359
		public const string TraceCodeWriteObjectContentBegin = "WriteObjectContent begins";

		// Token: 0x04000938 RID: 2360
		public const string TraceCodeWriteObjectContentEnd = "WriteObjectContent ends";

		// Token: 0x04000939 RID: 2361
		public const string TraceCodeWriteObjectEnd = "WriteObject ends";

		// Token: 0x0400093A RID: 2362
		public const string TraceCodeXsdExportAnnotationFailed = "XSD export annotation failed";

		// Token: 0x0400093B RID: 2363
		public const string TraceCodeXsdExportBegin = "XSD export begins";

		// Token: 0x0400093C RID: 2364
		public const string TraceCodeXsdExportDupItems = "XSD export duplicate items";

		// Token: 0x0400093D RID: 2365
		public const string TraceCodeXsdExportEnd = "XSD export ends";

		// Token: 0x0400093E RID: 2366
		public const string TraceCodeXsdExportError = "XSD export error";

		// Token: 0x0400093F RID: 2367
		public const string TraceCodeXsdImportAnnotationFailed = "XSD import annotation failed";

		// Token: 0x04000940 RID: 2368
		public const string TraceCodeXsdImportBegin = "XSD import begins";

		// Token: 0x04000941 RID: 2369
		public const string TraceCodeXsdImportEnd = "XSD import ends";

		// Token: 0x04000942 RID: 2370
		public const string TraceCodeXsdImportError = "XSD import error";

		// Token: 0x04000943 RID: 2371
		public const string TypeCannotBeForwardedFrom = "Type '{0}' in assembly '{1}' cannot be forwarded from assembly '{2}'.";

		// Token: 0x04000944 RID: 2372
		public const string TypeCannotBeImported = "Type '{0}' in '{1}' namespace cannot be imported: {2}";

		// Token: 0x04000945 RID: 2373
		public const string TypeCannotBeImportedHowToFix = "Type cannot be imported: {0}";

		// Token: 0x04000946 RID: 2374
		public const string TypeHasNotBeenImported = "Type '{0}' in '{1}' namespace has not been imported.";

		// Token: 0x04000947 RID: 2375
		public const string TypeMustBeIXmlSerializable = "Type '{0}' must be IXmlSerializable. Contract type: '{1}', contract name: '{2}' in '{3}' namespace.";

		// Token: 0x04000948 RID: 2376
		public const string TypeShouldNotContainAttributes = "Type should not contain attributes. Serialization namespace: '{0}'.";

		// Token: 0x04000949 RID: 2377
		public const string UnknownXmlType = "Unknown XML type: '{0}'.";

		// Token: 0x0400094A RID: 2378
		public const string WriteBufferOverflow = "Write buffer overflow.";

		// Token: 0x0400094B RID: 2379
		public const string WriteNotSupportedOnStream = "Write operation is not supported on this '{0}' Stream.";

		// Token: 0x0400094C RID: 2380
		public const string XmlCanonicalizationNotStarted = "XML canonicalization was not started.";

		// Token: 0x0400094D RID: 2381
		public const string XmlCanonicalizationStarted = "XML canonicalization started";

		// Token: 0x0400094E RID: 2382
		public const string XmlMaxArrayLengthOrMaxItemsQuotaExceeded = "XML max array length or max items quota exceeded. It must be less than {0}.";

		// Token: 0x0400094F RID: 2383
		public const string XmlMaxBytesPerReadExceeded = "XML max bytes per read exceeded. It must be less than {0}.";

		// Token: 0x04000950 RID: 2384
		public const string XmlMaxDepthExceeded = "XML max depth exceeded. It must be less than {0}.";

		// Token: 0x04000951 RID: 2385
		public const string XmlMaxStringContentLengthExceeded = "XML max string content length exceeded. It must be less than {0}.";

		// Token: 0x04000952 RID: 2386
		public const string XmlObjectAssignedToIncompatibleInterface = "Object of type '{0}' is assigned to an incompatible interface '{1}'.";

		// Token: 0x04000953 RID: 2387
		public const string PlatformNotSupported_SchemaImporter = "The implementation of the function requires System.Runtime.Serialization.SchemaImporter which is not supported on this platform.";

		// Token: 0x04000954 RID: 2388
		public const string PlatformNotSupported_IDataContractSurrogate = "The implementation of the function requires System.Runtime.Serialization.IDataContractSurrogate which is not supported on this platform.";
	}
}
