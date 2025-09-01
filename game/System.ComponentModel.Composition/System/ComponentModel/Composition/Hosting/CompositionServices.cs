using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	// Token: 0x020000D6 RID: 214
	internal static class CompositionServices
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00010540 File Offset: 0x0000E740
		internal static Type GetDefaultTypeFromMember(this MemberInfo member)
		{
			Assumes.NotNull<MemberInfo>(member);
			MemberTypes memberType = member.MemberType;
			if (memberType <= MemberTypes.Property)
			{
				if (memberType != MemberTypes.Field)
				{
					if (memberType == MemberTypes.Property)
					{
						return ((PropertyInfo)member).PropertyType;
					}
				}
			}
			else if (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
			{
				return (Type)member;
			}
			Assumes.IsTrue(member.MemberType == MemberTypes.Field);
			return ((FieldInfo)member).FieldType;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000105A5 File Offset: 0x0000E7A5
		internal static Type AdjustSpecifiedTypeIdentityType(this Type specifiedContractType, MemberInfo member)
		{
			if (member.MemberType == MemberTypes.Method)
			{
				return specifiedContractType;
			}
			return specifiedContractType.AdjustSpecifiedTypeIdentityType(member.GetDefaultTypeFromMember());
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000105C0 File Offset: 0x0000E7C0
		internal static Type AdjustSpecifiedTypeIdentityType(this Type specifiedContractType, Type memberType)
		{
			Assumes.NotNull<Type>(specifiedContractType);
			if (memberType != null && memberType.IsGenericType && specifiedContractType.IsGenericType)
			{
				if (specifiedContractType.ContainsGenericParameters && !memberType.ContainsGenericParameters)
				{
					Type[] genericArguments = memberType.GetGenericArguments();
					Type[] genericArguments2 = specifiedContractType.GetGenericArguments();
					if (genericArguments.Length == genericArguments2.Length)
					{
						return specifiedContractType.MakeGenericType(genericArguments);
					}
				}
				else if (specifiedContractType.ContainsGenericParameters && memberType.ContainsGenericParameters)
				{
					IList<Type> pureGenericParameters = memberType.GetPureGenericParameters();
					if (specifiedContractType.GetPureGenericArity() == pureGenericParameters.Count)
					{
						return specifiedContractType.GetGenericTypeDefinition().MakeGenericType(pureGenericParameters.ToArray<Type>());
					}
				}
			}
			return specifiedContractType;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00010652 File Offset: 0x0000E852
		private static string AdjustTypeIdentity(string originalTypeIdentity, Type typeIdentityType)
		{
			return GenericServices.GetGenericName(originalTypeIdentity, GenericServices.GetGenericParametersOrder(typeIdentityType), typeIdentityType.GetPureGenericArity());
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00010666 File Offset: 0x0000E866
		internal static void GetContractInfoFromExport(this MemberInfo member, ExportAttribute export, out Type typeIdentityType, out string contractName)
		{
			typeIdentityType = member.GetTypeIdentityTypeFromExport(export);
			if (!string.IsNullOrEmpty(export.ContractName))
			{
				contractName = export.ContractName;
				return;
			}
			contractName = member.GetTypeIdentityFromExport(typeIdentityType);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00010694 File Offset: 0x0000E894
		internal static string GetTypeIdentityFromExport(this MemberInfo member, Type typeIdentityType)
		{
			if (typeIdentityType != null)
			{
				string text = AttributedModelServices.GetTypeIdentity(typeIdentityType);
				if (typeIdentityType.ContainsGenericParameters)
				{
					text = CompositionServices.AdjustTypeIdentity(text, typeIdentityType);
				}
				return text;
			}
			MethodInfo methodInfo = member as MethodInfo;
			Assumes.NotNull<MethodInfo>(methodInfo);
			return AttributedModelServices.GetTypeIdentity(methodInfo);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000106D4 File Offset: 0x0000E8D4
		private static Type GetTypeIdentityTypeFromExport(this MemberInfo member, ExportAttribute export)
		{
			if (export.ContractType != null)
			{
				return export.ContractType.AdjustSpecifiedTypeIdentityType(member);
			}
			if (member.MemberType == MemberTypes.Method)
			{
				return null;
			}
			return member.GetDefaultTypeFromMember();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00010702 File Offset: 0x0000E902
		internal static bool IsContractNameSameAsTypeIdentity(this ExportAttribute export)
		{
			return string.IsNullOrEmpty(export.ContractName);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001070F File Offset: 0x0000E90F
		internal static Type GetContractTypeFromImport(this IAttributedImport import, ImportType importType)
		{
			if (import.ContractType != null)
			{
				return import.ContractType.AdjustSpecifiedTypeIdentityType(importType.ContractType);
			}
			return importType.ContractType;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00010737 File Offset: 0x0000E937
		internal static string GetContractNameFromImport(this IAttributedImport import, ImportType importType)
		{
			if (!string.IsNullOrEmpty(import.ContractName))
			{
				return import.ContractName;
			}
			return AttributedModelServices.GetContractName(import.GetContractTypeFromImport(importType));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001075C File Offset: 0x0000E95C
		internal static string GetTypeIdentityFromImport(this IAttributedImport import, ImportType importType)
		{
			Type contractTypeFromImport = import.GetContractTypeFromImport(importType);
			if (contractTypeFromImport == CompositionServices.ObjectType)
			{
				return null;
			}
			return AttributedModelServices.GetTypeIdentity(contractTypeFromImport);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00010788 File Offset: 0x0000E988
		internal static IDictionary<string, object> GetPartMetadataForType(this Type type, CreationPolicy creationPolicy)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparers.MetadataKeyNames);
			if (creationPolicy != CreationPolicy.Any)
			{
				dictionary.Add("System.ComponentModel.Composition.CreationPolicy", creationPolicy);
			}
			foreach (PartMetadataAttribute partMetadataAttribute in type.GetAttributes<PartMetadataAttribute>())
			{
				if (!CompositionServices.reservedMetadataNames.Contains(partMetadataAttribute.Name, StringComparers.MetadataKeyNames) && !dictionary.ContainsKey(partMetadataAttribute.Name))
				{
					dictionary.Add(partMetadataAttribute.Name, partMetadataAttribute.Value);
				}
			}
			if (type.ContainsGenericParameters)
			{
				dictionary.Add("System.ComponentModel.Composition.IsGenericPart", true);
				Type[] genericArguments = type.GetGenericArguments();
				dictionary.Add("System.ComponentModel.Composition.GenericPartArity", genericArguments.Length);
				bool flag = false;
				object[] array = new object[genericArguments.Length];
				GenericParameterAttributes[] array2 = new GenericParameterAttributes[genericArguments.Length];
				for (int j = 0; j < genericArguments.Length; j++)
				{
					Type type2 = genericArguments[j];
					Type[] array3 = type2.GetGenericParameterConstraints();
					if (array3.Length == 0)
					{
						array3 = null;
					}
					GenericParameterAttributes genericParameterAttributes = type2.GenericParameterAttributes;
					if (array3 != null || genericParameterAttributes != GenericParameterAttributes.None)
					{
						array[j] = array3;
						array2[j] = genericParameterAttributes;
						flag = true;
					}
				}
				if (flag)
				{
					dictionary.Add("System.ComponentModel.Composition.GenericParameterConstraints", array);
					dictionary.Add("System.ComponentModel.Composition.GenericParameterAttributes", array2);
				}
			}
			if (dictionary.Count == 0)
			{
				return MetadataServices.EmptyMetadata;
			}
			return dictionary;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000108CC File Offset: 0x0000EACC
		internal static void TryExportMetadataForMember(this MemberInfo member, out IDictionary<string, object> dictionary)
		{
			dictionary = new Dictionary<string, object>();
			foreach (Attribute attribute in member.GetAttributes<Attribute>())
			{
				ExportMetadataAttribute exportMetadataAttribute = attribute as ExportMetadataAttribute;
				if (exportMetadataAttribute != null)
				{
					if (CompositionServices.reservedMetadataNames.Contains(exportMetadataAttribute.Name, StringComparers.MetadataKeyNames))
					{
						throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_ReservedMetadataNameUsed, new string[]
						{
							member.GetDisplayName(),
							exportMetadataAttribute.Name
						});
					}
					if (!dictionary.TryContributeMetadataValue(exportMetadataAttribute.Name, exportMetadataAttribute.Value, null, exportMetadataAttribute.IsMultiple))
					{
						throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_DuplicateMetadataNameValues, new string[]
						{
							member.GetDisplayName(),
							exportMetadataAttribute.Name
						});
					}
				}
				else
				{
					Type type = attribute.GetType();
					if (type != CompositionServices.ExportAttributeType && type.IsAttributeDefined(true))
					{
						bool allowsMultiple = false;
						AttributeUsageAttribute firstAttribute = type.GetFirstAttribute(true);
						if (firstAttribute != null)
						{
							allowsMultiple = firstAttribute.AllowMultiple;
						}
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							if (!(propertyInfo.DeclaringType == CompositionServices.ExportAttributeType) && !(propertyInfo.DeclaringType == CompositionServices.AttributeType))
							{
								if (CompositionServices.reservedMetadataNames.Contains(propertyInfo.Name, StringComparers.MetadataKeyNames))
								{
									throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_ReservedMetadataNameUsed, new string[]
									{
										member.GetDisplayName(),
										exportMetadataAttribute.Name
									});
								}
								object value = propertyInfo.GetValue(attribute, null);
								if (value != null && !CompositionServices.IsValidAttributeType(value.GetType()))
								{
									throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_MetadataContainsValueWithInvalidType, new string[]
									{
										propertyInfo.GetDisplayName(),
										value.GetType().GetDisplayName()
									});
								}
								if (!dictionary.TryContributeMetadataValue(propertyInfo.Name, value, propertyInfo.PropertyType, allowsMultiple))
								{
									throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_DuplicateMetadataNameValues, new string[]
									{
										member.GetDisplayName(),
										propertyInfo.Name
									});
								}
							}
						}
					}
				}
			}
			foreach (string key in dictionary.Keys.ToArray<string>())
			{
				CompositionServices.MetadataList metadataList = dictionary[key] as CompositionServices.MetadataList;
				if (metadataList != null)
				{
					dictionary[key] = metadataList.ToArray();
				}
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00010B24 File Offset: 0x0000ED24
		private static bool TryContributeMetadataValue(this IDictionary<string, object> dictionary, string name, object value, Type valueType, bool allowsMultiple)
		{
			object obj;
			if (!dictionary.TryGetValue(name, out obj))
			{
				if (allowsMultiple)
				{
					CompositionServices.MetadataList metadataList = new CompositionServices.MetadataList();
					metadataList.Add(value, valueType);
					value = metadataList;
				}
				dictionary.Add(name, value);
			}
			else
			{
				CompositionServices.MetadataList metadataList2 = obj as CompositionServices.MetadataList;
				if (!allowsMultiple || metadataList2 == null)
				{
					dictionary.Remove(name);
					return false;
				}
				metadataList2.Add(value, valueType);
			}
			return true;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00010B7C File Offset: 0x0000ED7C
		internal static IEnumerable<KeyValuePair<string, Type>> GetRequiredMetadata(Type metadataViewType)
		{
			if (metadataViewType == null || ExportServices.IsDefaultMetadataViewType(metadataViewType) || ExportServices.IsDictionaryConstructorViewType(metadataViewType) || !metadataViewType.IsInterface)
			{
				return Enumerable.Empty<KeyValuePair<string, Type>>();
			}
			return from property in (from property in metadataViewType.GetAllProperties()
			where property.GetFirstAttribute<DefaultValueAttribute>() == null
			select property).ToList<PropertyInfo>()
			select new KeyValuePair<string, Type>(property.Name, property.PropertyType);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00010C03 File Offset: 0x0000EE03
		internal static IDictionary<string, object> GetImportMetadata(ImportType importType, IAttributedImport attributedImport)
		{
			return CompositionServices.GetImportMetadata(importType.ContractType, attributedImport);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00010C14 File Offset: 0x0000EE14
		internal static IDictionary<string, object> GetImportMetadata(Type type, IAttributedImport attributedImport)
		{
			Dictionary<string, object> dictionary = null;
			if (type.IsGenericType)
			{
				dictionary = new Dictionary<string, object>();
				if (type.ContainsGenericParameters)
				{
					dictionary["System.ComponentModel.Composition.GenericImportParametersOrderMetadataName"] = GenericServices.GetGenericParametersOrder(type);
				}
				else
				{
					dictionary["System.ComponentModel.Composition.GenericContractName"] = ContractNameServices.GetTypeIdentity(type.GetGenericTypeDefinition());
					dictionary["System.ComponentModel.Composition.GenericParameters"] = type.GetGenericArguments();
				}
			}
			if (attributedImport != null && attributedImport.Source != ImportSource.Any)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
				}
				dictionary["System.ComponentModel.Composition.ImportSource"] = attributedImport.Source;
			}
			if (dictionary != null)
			{
				return dictionary.AsReadOnly();
			}
			return MetadataServices.EmptyMetadata;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00010CAC File Offset: 0x0000EEAC
		internal static object GetExportedValueFromComposedPart(ImportEngine engine, ComposablePart part, ExportDefinition definition)
		{
			if (engine != null)
			{
				try
				{
					engine.SatisfyImports(part);
				}
				catch (CompositionException innerException)
				{
					throw ExceptionBuilder.CreateCannotGetExportedValue(part, definition, innerException);
				}
			}
			object exportedValue;
			try
			{
				exportedValue = part.GetExportedValue(definition);
			}
			catch (ComposablePartException innerException2)
			{
				throw ExceptionBuilder.CreateCannotGetExportedValue(part, definition, innerException2);
			}
			return exportedValue;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00010D04 File Offset: 0x0000EF04
		internal static bool IsRecomposable(this ComposablePart part)
		{
			return part.ImportDefinitions.Any((ImportDefinition import) => import.IsRecomposable);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00010D30 File Offset: 0x0000EF30
		internal static CompositionResult TryInvoke(Action action)
		{
			CompositionResult result;
			try
			{
				action();
				result = CompositionResult.SucceededResult;
			}
			catch (CompositionException ex)
			{
				result = new CompositionResult(ex.Errors);
			}
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00010D6C File Offset: 0x0000EF6C
		internal static CompositionResult TryFire<TEventArgs>(EventHandler<TEventArgs> _delegate, object sender, TEventArgs e) where TEventArgs : EventArgs
		{
			CompositionResult result = CompositionResult.SucceededResult;
			foreach (EventHandler<TEventArgs> eventHandler in _delegate.GetInvocationList())
			{
				try
				{
					eventHandler(sender, e);
				}
				catch (CompositionException ex)
				{
					result = result.MergeErrors(ex.Errors);
				}
			}
			return result;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00010DCC File Offset: 0x0000EFCC
		internal static CreationPolicy GetRequiredCreationPolicy(this ImportDefinition definition)
		{
			ContractBasedImportDefinition contractBasedImportDefinition = definition as ContractBasedImportDefinition;
			if (contractBasedImportDefinition != null)
			{
				return contractBasedImportDefinition.RequiredCreationPolicy;
			}
			return CreationPolicy.Any;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00010DEB File Offset: 0x0000EFEB
		internal static bool IsAtMostOne(this ImportCardinality cardinality)
		{
			return cardinality == ImportCardinality.ZeroOrOne || cardinality == ImportCardinality.ExactlyOne;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00010DF6 File Offset: 0x0000EFF6
		private static bool IsValidAttributeType(Type type)
		{
			return CompositionServices.IsValidAttributeType(type, true);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00010E00 File Offset: 0x0000F000
		private static bool IsValidAttributeType(Type type, bool arrayAllowed)
		{
			Assumes.NotNull<Type>(type);
			return type.IsPrimitive || type == typeof(string) || (type.IsEnum && type.IsVisible) || typeof(Type).IsAssignableFrom(type) || (arrayAllowed && type.IsArray && type.GetArrayRank() == 1 && CompositionServices.IsValidAttributeType(type.GetElementType(), false));
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00010E7C File Offset: 0x0000F07C
		// Note: this type is marked as 'beforefieldinit'.
		static CompositionServices()
		{
		}

		// Token: 0x0400025E RID: 606
		internal static readonly Type InheritedExportAttributeType = typeof(InheritedExportAttribute);

		// Token: 0x0400025F RID: 607
		internal static readonly Type ExportAttributeType = typeof(ExportAttribute);

		// Token: 0x04000260 RID: 608
		internal static readonly Type AttributeType = typeof(Attribute);

		// Token: 0x04000261 RID: 609
		internal static readonly Type ObjectType = typeof(object);

		// Token: 0x04000262 RID: 610
		private static readonly string[] reservedMetadataNames = new string[]
		{
			"System.ComponentModel.Composition.CreationPolicy"
		};

		// Token: 0x020000D7 RID: 215
		private class MetadataList
		{
			// Token: 0x06000590 RID: 1424 RVA: 0x00010ED8 File Offset: 0x0000F0D8
			public void Add(object item, Type itemType)
			{
				this._containsNulls |= (item == null);
				if (itemType == CompositionServices.MetadataList.ObjectType)
				{
					itemType = null;
				}
				if (itemType == null && item != null)
				{
					itemType = item.GetType();
				}
				if (item is Type)
				{
					itemType = CompositionServices.MetadataList.TypeType;
				}
				if (itemType != null)
				{
					this.InferArrayType(itemType);
				}
				this._innerList.Add(item);
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x00010F45 File Offset: 0x0000F145
			private void InferArrayType(Type itemType)
			{
				Assumes.NotNull<Type>(itemType);
				if (this._arrayType == null)
				{
					this._arrayType = itemType;
					return;
				}
				if (this._arrayType != itemType)
				{
					this._arrayType = CompositionServices.MetadataList.ObjectType;
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x00010F7C File Offset: 0x0000F17C
			public Array ToArray()
			{
				if (this._arrayType == null)
				{
					this._arrayType = CompositionServices.MetadataList.ObjectType;
				}
				else if (this._containsNulls && this._arrayType.IsValueType)
				{
					this._arrayType = CompositionServices.MetadataList.ObjectType;
				}
				Array array = Array.CreateInstance(this._arrayType, this._innerList.Count);
				for (int i = 0; i < array.Length; i++)
				{
					array.SetValue(this._innerList[i], i);
				}
				return array;
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x00011000 File Offset: 0x0000F200
			public MetadataList()
			{
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00011013 File Offset: 0x0000F213
			// Note: this type is marked as 'beforefieldinit'.
			static MetadataList()
			{
			}

			// Token: 0x04000263 RID: 611
			private Type _arrayType;

			// Token: 0x04000264 RID: 612
			private bool _containsNulls;

			// Token: 0x04000265 RID: 613
			private static readonly Type ObjectType = typeof(object);

			// Token: 0x04000266 RID: 614
			private static readonly Type TypeType = typeof(Type);

			// Token: 0x04000267 RID: 615
			private Collection<object> _innerList = new Collection<object>();
		}

		// Token: 0x020000D8 RID: 216
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x00011033 File Offset: 0x0000F233
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0001103F File Offset: 0x0000F23F
			internal bool <GetRequiredMetadata>b__20_0(PropertyInfo property)
			{
				return property.GetFirstAttribute<DefaultValueAttribute>() == null;
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x0001104A File Offset: 0x0000F24A
			internal KeyValuePair<string, Type> <GetRequiredMetadata>b__20_1(PropertyInfo property)
			{
				return new KeyValuePair<string, Type>(property.Name, property.PropertyType);
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x0001105D File Offset: 0x0000F25D
			internal bool <IsRecomposable>b__24_0(ImportDefinition import)
			{
				return import.IsRecomposable;
			}

			// Token: 0x04000268 RID: 616
			public static readonly CompositionServices.<>c <>9 = new CompositionServices.<>c();

			// Token: 0x04000269 RID: 617
			public static Func<PropertyInfo, bool> <>9__20_0;

			// Token: 0x0400026A RID: 618
			public static Func<PropertyInfo, KeyValuePair<string, Type>> <>9__20_1;

			// Token: 0x0400026B RID: 619
			public static Func<ImportDefinition, bool> <>9__24_0;
		}
	}
}
