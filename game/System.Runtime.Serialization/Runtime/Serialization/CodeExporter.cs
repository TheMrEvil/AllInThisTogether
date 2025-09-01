using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x020000B1 RID: 177
	internal class CodeExporter
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x00027870 File Offset: 0x00025A70
		internal CodeExporter(DataContractSet dataContractSet, ImportOptions options, CodeCompileUnit codeCompileUnit)
		{
			this.dataContractSet = dataContractSet;
			this.codeCompileUnit = codeCompileUnit;
			this.AddReferencedAssembly(Assembly.GetExecutingAssembly());
			this.options = options;
			this.namespaces = new Dictionary<string, string>();
			this.clrNamespaces = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in dataContractSet)
			{
				DataContract value = keyValuePair.Value;
				if (!value.IsBuiltInDataContract && !(value is CollectionDataContract))
				{
					ContractCodeDomInfo contractCodeDomInfo = this.GetContractCodeDomInfo(value);
					if (contractCodeDomInfo.IsProcessed && !contractCodeDomInfo.UsesWildcardNamespace)
					{
						string clrNamespace = contractCodeDomInfo.ClrNamespace;
						if (clrNamespace != null && !this.clrNamespaces.ContainsKey(clrNamespace))
						{
							this.clrNamespaces.Add(clrNamespace, value.StableName.Namespace);
							this.namespaces.Add(value.StableName.Namespace, clrNamespace);
						}
					}
				}
			}
			if (this.options != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in options.Namespaces)
				{
					string key = keyValuePair2.Key;
					string text = keyValuePair2.Value;
					if (text == null)
					{
						text = string.Empty;
					}
					string text2;
					if (this.clrNamespaces.TryGetValue(text, out text2))
					{
						if (key != text2)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CLR namespace is mapped multiple times. Current data contract namespace is '{0}', found '{1}' for CLR namespace '{2}'.", new object[]
							{
								text2,
								key,
								text
							})));
						}
					}
					else
					{
						this.clrNamespaces.Add(text, key);
					}
					string b;
					if (this.namespaces.TryGetValue(key, out b))
					{
						if (text != b)
						{
							this.namespaces.Remove(key);
							this.namespaces.Add(key, text);
						}
					}
					else
					{
						this.namespaces.Add(key, text);
					}
				}
			}
			foreach (object obj in codeCompileUnit.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				string text3 = codeNamespace.Name ?? string.Empty;
				if (!this.clrNamespaces.ContainsKey(text3))
				{
					this.clrNamespaces.Add(text3, null);
				}
				if (text3.Length == 0)
				{
					foreach (object obj2 in codeNamespace.Types)
					{
						CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj2;
						this.AddGlobalTypeName(codeTypeDeclaration.Name);
					}
				}
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00027B5C File Offset: 0x00025D5C
		private void AddReferencedAssembly(Assembly assembly)
		{
			string fileName = Path.GetFileName(assembly.Location);
			bool flag = false;
			using (StringEnumerator enumerator = this.codeCompileUnit.ReferencedAssemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(enumerator.Current, fileName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				this.codeCompileUnit.ReferencedAssemblies.Add(fileName);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00027BE0 File Offset: 0x00025DE0
		private bool GenerateSerializableTypes
		{
			get
			{
				return this.options != null && this.options.GenerateSerializable;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00027BF7 File Offset: 0x00025DF7
		private bool GenerateInternalTypes
		{
			get
			{
				return this.options != null && this.options.GenerateInternal;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00027C0E File Offset: 0x00025E0E
		private bool EnableDataBinding
		{
			get
			{
				return this.options != null && this.options.EnableDataBinding;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00027C25 File Offset: 0x00025E25
		private CodeDomProvider CodeProvider
		{
			get
			{
				if (this.options != null)
				{
					return this.options.CodeProvider;
				}
				return null;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00027C3C File Offset: 0x00025E3C
		private bool SupportsDeclareEvents
		{
			[SecuritySafeCritical]
			get
			{
				return this.CodeProvider == null || this.CodeProvider.Supports(GeneratorSupport.DeclareEvents);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00027C58 File Offset: 0x00025E58
		private bool SupportsDeclareValueTypes
		{
			[SecuritySafeCritical]
			get
			{
				return this.CodeProvider == null || this.CodeProvider.Supports(GeneratorSupport.DeclareValueTypes);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00027C74 File Offset: 0x00025E74
		private bool SupportsGenericTypeReference
		{
			[SecuritySafeCritical]
			get
			{
				return this.CodeProvider == null || this.CodeProvider.Supports(GeneratorSupport.GenericTypeReference);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00027C90 File Offset: 0x00025E90
		private bool SupportsAssemblyAttributes
		{
			[SecuritySafeCritical]
			get
			{
				return this.CodeProvider == null || this.CodeProvider.Supports(GeneratorSupport.AssemblyAttributes);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00027CAC File Offset: 0x00025EAC
		private bool SupportsPartialTypes
		{
			[SecuritySafeCritical]
			get
			{
				return this.CodeProvider == null || this.CodeProvider.Supports(GeneratorSupport.PartialTypes);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00027CC8 File Offset: 0x00025EC8
		private bool SupportsNestedTypes
		{
			[SecuritySafeCritical]
			get
			{
				return this.CodeProvider == null || this.CodeProvider.Supports(GeneratorSupport.NestedTypes);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00027CE4 File Offset: 0x00025EE4
		private string FileExtension
		{
			[SecuritySafeCritical]
			get
			{
				if (this.CodeProvider != null)
				{
					return this.CodeProvider.FileExtension;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00027CFF File Offset: 0x00025EFF
		private Dictionary<string, string> Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00027D07 File Offset: 0x00025F07
		private Dictionary<string, string> ClrNamespaces
		{
			get
			{
				return this.clrNamespaces;
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00027D10 File Offset: 0x00025F10
		private bool TryGetReferencedType(XmlQualifiedName stableName, DataContract dataContract, out Type type)
		{
			if (dataContract == null)
			{
				if (this.dataContractSet.TryGetReferencedCollectionType(stableName, dataContract, out type))
				{
					return true;
				}
				if (!this.dataContractSet.TryGetReferencedType(stableName, dataContract, out type))
				{
					return false;
				}
				if (CollectionDataContract.IsCollection(type))
				{
					type = null;
					return false;
				}
				return true;
			}
			else
			{
				if (dataContract is CollectionDataContract)
				{
					return this.dataContractSet.TryGetReferencedCollectionType(stableName, dataContract, out type);
				}
				XmlDataContract xmlDataContract = dataContract as XmlDataContract;
				if (xmlDataContract != null && xmlDataContract.IsAnonymous)
				{
					stableName = SchemaImporter.ImportActualType(xmlDataContract.XsdType.Annotation, stableName, dataContract.StableName);
				}
				return this.dataContractSet.TryGetReferencedType(stableName, dataContract, out type);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00027DA4 File Offset: 0x00025FA4
		[SecurityCritical]
		internal void Export()
		{
			try
			{
				foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in this.dataContractSet)
				{
					DataContract value = keyValuePair.Value;
					if (!value.IsBuiltInDataContract)
					{
						ContractCodeDomInfo contractCodeDomInfo = this.GetContractCodeDomInfo(value);
						if (!contractCodeDomInfo.IsProcessed)
						{
							if (value is ClassDataContract)
							{
								ClassDataContract classDataContract = (ClassDataContract)value;
								if (classDataContract.IsISerializable)
								{
									this.ExportISerializableDataContract(classDataContract, contractCodeDomInfo);
								}
								else
								{
									this.ExportClassDataContractHierarchy(classDataContract.StableName, classDataContract, contractCodeDomInfo, new Dictionary<XmlQualifiedName, object>());
								}
							}
							else if (value is CollectionDataContract)
							{
								this.ExportCollectionDataContract((CollectionDataContract)value, contractCodeDomInfo);
							}
							else if (value is EnumDataContract)
							{
								this.ExportEnumDataContract((EnumDataContract)value, contractCodeDomInfo);
							}
							else
							{
								if (!(value is XmlDataContract))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("An internal error has occurred. Unexpected contract type '{0}' for type '{1}' encountered.", new object[]
									{
										DataContract.GetClrTypeFullName(value.GetType()),
										DataContract.GetClrTypeFullName(value.UnderlyingType)
									})));
								}
								this.ExportXmlDataContract((XmlDataContract)value, contractCodeDomInfo);
							}
							contractCodeDomInfo.IsProcessed = true;
						}
					}
				}
				if (this.dataContractSet.DataContractSurrogate != null)
				{
					CodeNamespace[] array = new CodeNamespace[this.codeCompileUnit.Namespaces.Count];
					this.codeCompileUnit.Namespaces.CopyTo(array, 0);
					foreach (CodeNamespace codeNamespace in array)
					{
						this.InvokeProcessImportedType(codeNamespace.Types);
					}
				}
			}
			finally
			{
				CodeGenerator.ValidateIdentifiers(this.codeCompileUnit);
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00027F6C File Offset: 0x0002616C
		private void ExportClassDataContractHierarchy(XmlQualifiedName typeName, ClassDataContract classContract, ContractCodeDomInfo contractCodeDomInfo, Dictionary<XmlQualifiedName, object> contractNamesInHierarchy)
		{
			if (contractNamesInHierarchy.ContainsKey(classContract.StableName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' in '{1}' namespace cannot be imported: {2}", new object[]
				{
					typeName.Name,
					typeName.Namespace,
					SR.GetString("Circular type reference was found for '{0}' in '{1}' namespace.", new object[]
					{
						classContract.StableName.Name,
						classContract.StableName.Namespace
					})
				})));
			}
			contractNamesInHierarchy.Add(classContract.StableName, null);
			ClassDataContract baseContract = classContract.BaseContract;
			if (baseContract != null)
			{
				ContractCodeDomInfo contractCodeDomInfo2 = this.GetContractCodeDomInfo(baseContract);
				if (!contractCodeDomInfo2.IsProcessed)
				{
					this.ExportClassDataContractHierarchy(typeName, baseContract, contractCodeDomInfo2, contractNamesInHierarchy);
					contractCodeDomInfo2.IsProcessed = true;
				}
			}
			this.ExportClassDataContract(classContract, contractCodeDomInfo);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00028028 File Offset: 0x00026228
		private void InvokeProcessImportedType(CollectionBase collection)
		{
			object[] array = new object[collection.Count];
			((ICollection)collection).CopyTo(array, 0);
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				CodeTypeDeclaration codeTypeDeclaration = array2[i] as CodeTypeDeclaration;
				if (codeTypeDeclaration != null)
				{
					CodeTypeDeclaration codeTypeDeclaration2 = DataContractSurrogateCaller.ProcessImportedType(this.dataContractSet.DataContractSurrogate, codeTypeDeclaration, this.codeCompileUnit);
					if (codeTypeDeclaration2 != codeTypeDeclaration)
					{
						((IList)collection).Remove(codeTypeDeclaration);
						if (codeTypeDeclaration2 != null)
						{
							((IList)collection).Add(codeTypeDeclaration2);
						}
					}
					if (codeTypeDeclaration2 != null)
					{
						this.InvokeProcessImportedType(codeTypeDeclaration2.Members);
					}
				}
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000280A8 File Offset: 0x000262A8
		internal CodeTypeReference GetCodeTypeReference(DataContract dataContract)
		{
			if (dataContract.IsBuiltInDataContract)
			{
				return this.GetCodeTypeReference(dataContract.UnderlyingType);
			}
			ContractCodeDomInfo contractCodeDomInfo = this.GetContractCodeDomInfo(dataContract);
			this.GenerateType(dataContract, contractCodeDomInfo);
			return contractCodeDomInfo.TypeReference;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000280E0 File Offset: 0x000262E0
		private CodeTypeReference GetCodeTypeReference(Type type)
		{
			this.AddReferencedAssembly(type.Assembly);
			return new CodeTypeReference(type);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000280F4 File Offset: 0x000262F4
		internal CodeTypeReference GetElementTypeReference(DataContract dataContract, bool isElementTypeNullable)
		{
			CodeTypeReference codeTypeReference = this.GetCodeTypeReference(dataContract);
			if (dataContract.IsValueType && isElementTypeNullable)
			{
				codeTypeReference = this.WrapNullable(codeTypeReference);
			}
			return codeTypeReference;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0002811C File Offset: 0x0002631C
		private XmlQualifiedName GenericListName
		{
			get
			{
				return DataContract.GetStableName(Globals.TypeOfListGeneric);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00028128 File Offset: 0x00026328
		private CollectionDataContract GenericListContract
		{
			get
			{
				return this.dataContractSet.GetDataContract(Globals.TypeOfListGeneric) as CollectionDataContract;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0002813F File Offset: 0x0002633F
		private XmlQualifiedName GenericDictionaryName
		{
			get
			{
				return DataContract.GetStableName(Globals.TypeOfDictionaryGeneric);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0002814B File Offset: 0x0002634B
		private CollectionDataContract GenericDictionaryContract
		{
			get
			{
				return this.dataContractSet.GetDataContract(Globals.TypeOfDictionaryGeneric) as CollectionDataContract;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00028164 File Offset: 0x00026364
		private ContractCodeDomInfo GetContractCodeDomInfo(DataContract dataContract)
		{
			ContractCodeDomInfo contractCodeDomInfo = this.dataContractSet.GetContractCodeDomInfo(dataContract);
			if (contractCodeDomInfo == null)
			{
				contractCodeDomInfo = new ContractCodeDomInfo();
				this.dataContractSet.SetContractCodeDomInfo(dataContract, contractCodeDomInfo);
			}
			return contractCodeDomInfo;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00028198 File Offset: 0x00026398
		private void GenerateType(DataContract dataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			if (!contractCodeDomInfo.IsProcessed)
			{
				CodeTypeReference referencedType = this.GetReferencedType(dataContract);
				if (referencedType != null)
				{
					contractCodeDomInfo.TypeReference = referencedType;
					contractCodeDomInfo.ReferencedTypeExists = true;
					return;
				}
				if (contractCodeDomInfo.TypeDeclaration == null)
				{
					string clrNamespace = this.GetClrNamespace(dataContract, contractCodeDomInfo);
					CodeNamespace codeNamespace = this.GetCodeNamespace(clrNamespace, dataContract.StableName.Namespace, contractCodeDomInfo);
					CodeTypeDeclaration codeTypeDeclaration = this.GetNestedType(dataContract, contractCodeDomInfo);
					if (codeTypeDeclaration == null)
					{
						string text = XmlConvert.DecodeName(dataContract.StableName.Name);
						text = CodeExporter.GetClrIdentifier(text, "GeneratedType");
						if (this.NamespaceContainsType(codeNamespace, text) || this.GlobalTypeNameConflicts(clrNamespace, text))
						{
							int num = 1;
							string text2;
							for (;;)
							{
								text2 = CodeExporter.AppendToValidClrIdentifier(text, num.ToString(NumberFormatInfo.InvariantInfo));
								if (!this.NamespaceContainsType(codeNamespace, text2) && !this.GlobalTypeNameConflicts(clrNamespace, text2))
								{
									break;
								}
								if (num == 2147483647)
								{
									goto Block_8;
								}
								num++;
							}
							text = text2;
							goto IL_F9;
							Block_8:
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Cannot compute unique name for '{0}'.", new object[]
							{
								text
							})));
						}
						IL_F9:
						codeTypeDeclaration = CodeExporter.CreateTypeDeclaration(text, dataContract);
						codeNamespace.Types.Add(codeTypeDeclaration);
						if (string.IsNullOrEmpty(clrNamespace))
						{
							this.AddGlobalTypeName(text);
						}
						contractCodeDomInfo.TypeReference = new CodeTypeReference((clrNamespace == null || clrNamespace.Length == 0) ? text : (clrNamespace + "." + text));
						if (this.GenerateInternalTypes)
						{
							codeTypeDeclaration.TypeAttributes = TypeAttributes.NotPublic;
						}
						else
						{
							codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
						}
					}
					if (this.dataContractSet.DataContractSurrogate != null)
					{
						codeTypeDeclaration.UserData.Add(CodeExporter.surrogateDataKey, this.dataContractSet.GetSurrogateData(dataContract));
					}
					contractCodeDomInfo.TypeDeclaration = codeTypeDeclaration;
				}
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00028334 File Offset: 0x00026534
		private CodeTypeDeclaration GetNestedType(DataContract dataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			if (!this.SupportsNestedTypes)
			{
				return null;
			}
			string name = dataContract.StableName.Name;
			int num = name.LastIndexOf('.');
			if (num <= 0)
			{
				return null;
			}
			string name2 = name.Substring(0, num);
			DataContract dataContract2 = this.dataContractSet[new XmlQualifiedName(name2, dataContract.StableName.Namespace)];
			if (dataContract2 == null)
			{
				return null;
			}
			string text = XmlConvert.DecodeName(name.Substring(num + 1));
			text = CodeExporter.GetClrIdentifier(text, "GeneratedType");
			ContractCodeDomInfo contractCodeDomInfo2 = this.GetContractCodeDomInfo(dataContract2);
			this.GenerateType(dataContract2, contractCodeDomInfo2);
			if (contractCodeDomInfo2.ReferencedTypeExists)
			{
				return null;
			}
			CodeTypeDeclaration typeDeclaration = contractCodeDomInfo2.TypeDeclaration;
			if (this.TypeContainsNestedType(typeDeclaration, text))
			{
				int num2 = 1;
				string text2;
				for (;;)
				{
					text2 = CodeExporter.AppendToValidClrIdentifier(text, num2.ToString(NumberFormatInfo.InvariantInfo));
					if (!this.TypeContainsNestedType(typeDeclaration, text2))
					{
						break;
					}
					num2++;
				}
				text = text2;
			}
			CodeTypeDeclaration codeTypeDeclaration = CodeExporter.CreateTypeDeclaration(text, dataContract);
			typeDeclaration.Members.Add(codeTypeDeclaration);
			contractCodeDomInfo.TypeReference = new CodeTypeReference(contractCodeDomInfo2.TypeReference.BaseType + "+" + text);
			if (this.GenerateInternalTypes)
			{
				codeTypeDeclaration.TypeAttributes = TypeAttributes.NestedAssembly;
			}
			else
			{
				codeTypeDeclaration.TypeAttributes = TypeAttributes.NestedPublic;
			}
			return codeTypeDeclaration;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002846C File Offset: 0x0002666C
		private static CodeTypeDeclaration CreateTypeDeclaration(string typeName, DataContract dataContract)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(typeName);
			CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName);
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
			AssemblyName name = Assembly.GetExecutingAssembly().GetName();
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Version.ToString())));
			if (!(dataContract is EnumDataContract))
			{
				codeTypeDeclaration.CustomAttributes.Add(value);
			}
			codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00028518 File Offset: 0x00026718
		[SecuritySafeCritical]
		private CodeTypeReference GetReferencedType(DataContract dataContract)
		{
			Type type = null;
			CodeTypeReference codeTypeReference = this.GetSurrogatedTypeReference(dataContract);
			if (codeTypeReference != null)
			{
				return codeTypeReference;
			}
			if (this.TryGetReferencedType(dataContract.StableName, dataContract, out type) && !type.IsGenericTypeDefinition && !type.ContainsGenericParameters)
			{
				if (dataContract is XmlDataContract)
				{
					if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
					{
						XmlDataContract xmlDataContract = (XmlDataContract)dataContract;
						if (xmlDataContract.IsTypeDefinedOnImport)
						{
							if (!xmlDataContract.Equals(this.dataContractSet.GetDataContract(type)))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Referenced type '{0}' does not match the expected type '{1}' in '{2}' namespace.", new object[]
								{
									type.AssemblyQualifiedName,
									dataContract.StableName.Name,
									dataContract.StableName.Namespace
								})));
							}
						}
						else
						{
							xmlDataContract.IsValueType = type.IsValueType;
							xmlDataContract.IsTypeDefinedOnImport = true;
						}
						return this.GetCodeTypeReference(type);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' must be IXmlSerializable. Contract type: '{1}', contract name: '{2}' in '{3}' namespace.", new object[]
					{
						DataContract.GetClrTypeFullName(type),
						DataContract.GetClrTypeFullName(Globals.TypeOfIXmlSerializable),
						dataContract.StableName.Name,
						dataContract.StableName.Namespace
					})));
				}
				else
				{
					if (this.dataContractSet.GetDataContract(type).Equals(dataContract))
					{
						codeTypeReference = this.GetCodeTypeReference(type);
						codeTypeReference.UserData.Add(CodeExporter.codeUserDataActualTypeKey, type);
						return codeTypeReference;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Referenced type '{0}' does not match the expected type '{1}' in '{2}' namespace.", new object[]
					{
						type.AssemblyQualifiedName,
						dataContract.StableName.Name,
						dataContract.StableName.Namespace
					})));
				}
			}
			else
			{
				if (dataContract.GenericInfo == null)
				{
					return this.GetReferencedCollectionType(dataContract as CollectionDataContract);
				}
				XmlQualifiedName expandedStableName = dataContract.GenericInfo.GetExpandedStableName();
				if (expandedStableName != dataContract.StableName)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Generic type name mismatch. Expected '{0}' in '{1}' namespace, got '{2}' in '{3}' namespace instead.", new object[]
					{
						dataContract.StableName.Name,
						dataContract.StableName.Namespace,
						expandedStableName.Name,
						expandedStableName.Namespace
					})));
				}
				DataContract dataContract2;
				codeTypeReference = this.GetReferencedGenericType(dataContract.GenericInfo, out dataContract2);
				if (dataContract2 != null && !dataContract2.Equals(dataContract))
				{
					type = (Type)codeTypeReference.UserData[CodeExporter.codeUserDataActualTypeKey];
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Referenced type '{0}' does not match the expected type '{1}' in '{2}' namespace.", new object[]
					{
						type.AssemblyQualifiedName,
						dataContract2.StableName.Name,
						dataContract2.StableName.Namespace
					})));
				}
				return codeTypeReference;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000287A4 File Offset: 0x000269A4
		private CodeTypeReference GetReferencedCollectionType(CollectionDataContract collectionContract)
		{
			if (collectionContract == null)
			{
				return null;
			}
			if (this.HasDefaultCollectionNames(collectionContract))
			{
				CodeTypeReference result;
				if (!this.TryGetReferencedDictionaryType(collectionContract, out result))
				{
					DataContract itemContract = collectionContract.ItemContract;
					if (collectionContract.IsDictionary)
					{
						this.GenerateKeyValueType(itemContract as ClassDataContract);
					}
					bool isItemTypeNullable = collectionContract.IsItemTypeNullable;
					if (!this.TryGetReferencedListType(itemContract, isItemTypeNullable, out result))
					{
						result = new CodeTypeReference(this.GetElementTypeReference(itemContract, isItemTypeNullable), 1);
					}
				}
				return result;
			}
			return null;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002880C File Offset: 0x00026A0C
		private bool HasDefaultCollectionNames(CollectionDataContract collectionContract)
		{
			DataContract itemContract = collectionContract.ItemContract;
			if (collectionContract.ItemName != itemContract.StableName.Name)
			{
				return false;
			}
			if (collectionContract.IsDictionary && (collectionContract.KeyName != "Key" || collectionContract.ValueName != "Value"))
			{
				return false;
			}
			XmlQualifiedName arrayTypeName = itemContract.GetArrayTypeName(collectionContract.IsItemTypeNullable);
			return collectionContract.StableName.Name == arrayTypeName.Name && collectionContract.StableName.Namespace == arrayTypeName.Namespace;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000288A8 File Offset: 0x00026AA8
		private bool TryGetReferencedDictionaryType(CollectionDataContract collectionContract, out CodeTypeReference typeReference)
		{
			if (collectionContract.IsDictionary && this.SupportsGenericTypeReference)
			{
				Type typeOfDictionaryGeneric;
				if (!this.TryGetReferencedType(this.GenericDictionaryName, this.GenericDictionaryContract, out typeOfDictionaryGeneric))
				{
					typeOfDictionaryGeneric = Globals.TypeOfDictionaryGeneric;
				}
				ClassDataContract classDataContract = collectionContract.ItemContract as ClassDataContract;
				DataMember dataMember = classDataContract.Members[0];
				DataMember dataMember2 = classDataContract.Members[1];
				CodeTypeReference elementTypeReference = this.GetElementTypeReference(dataMember.MemberTypeContract, dataMember.IsNullable);
				CodeTypeReference elementTypeReference2 = this.GetElementTypeReference(dataMember2.MemberTypeContract, dataMember2.IsNullable);
				if (elementTypeReference != null && elementTypeReference2 != null)
				{
					typeReference = this.GetCodeTypeReference(typeOfDictionaryGeneric);
					typeReference.TypeArguments.Add(elementTypeReference);
					typeReference.TypeArguments.Add(elementTypeReference2);
					return true;
				}
			}
			typeReference = null;
			return false;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00028968 File Offset: 0x00026B68
		private bool TryGetReferencedListType(DataContract itemContract, bool isItemTypeNullable, out CodeTypeReference typeReference)
		{
			Type type;
			if (this.SupportsGenericTypeReference && this.TryGetReferencedType(this.GenericListName, this.GenericListContract, out type))
			{
				typeReference = this.GetCodeTypeReference(type);
				typeReference.TypeArguments.Add(this.GetElementTypeReference(itemContract, isItemTypeNullable));
				return true;
			}
			typeReference = null;
			return false;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000289B8 File Offset: 0x00026BB8
		private CodeTypeReference GetSurrogatedTypeReference(DataContract dataContract)
		{
			IDataContractSurrogate dataContractSurrogate = this.dataContractSet.DataContractSurrogate;
			if (dataContractSurrogate != null)
			{
				Type referencedTypeOnImport = DataContractSurrogateCaller.GetReferencedTypeOnImport(dataContractSurrogate, dataContract.StableName.Name, dataContract.StableName.Namespace, this.dataContractSet.GetSurrogateData(dataContract));
				if (referencedTypeOnImport != null)
				{
					CodeTypeReference codeTypeReference = this.GetCodeTypeReference(referencedTypeOnImport);
					codeTypeReference.UserData.Add(CodeExporter.codeUserDataActualTypeKey, referencedTypeOnImport);
					return codeTypeReference;
				}
			}
			return null;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00028A20 File Offset: 0x00026C20
		private CodeTypeReference GetReferencedGenericType(GenericInfo genInfo, out DataContract dataContract)
		{
			dataContract = null;
			if (!this.SupportsGenericTypeReference)
			{
				return null;
			}
			Type type;
			if (this.TryGetReferencedType(genInfo.StableName, null, out type))
			{
				bool flag = type != Globals.TypeOfNullable;
				CodeTypeReference codeTypeReference = this.GetCodeTypeReference(type);
				codeTypeReference.UserData.Add(CodeExporter.codeUserDataActualTypeKey, type);
				if (genInfo.Parameters != null)
				{
					DataContract[] array = new DataContract[genInfo.Parameters.Count];
					for (int i = 0; i < genInfo.Parameters.Count; i++)
					{
						GenericInfo genericInfo = genInfo.Parameters[i];
						XmlQualifiedName expandedStableName = genericInfo.GetExpandedStableName();
						DataContract dataContract2 = this.dataContractSet[expandedStableName];
						CodeTypeReference codeTypeReference2;
						bool flag2;
						if (dataContract2 != null)
						{
							codeTypeReference2 = this.GetCodeTypeReference(dataContract2);
							flag2 = dataContract2.IsValueType;
						}
						else
						{
							codeTypeReference2 = this.GetReferencedGenericType(genericInfo, out dataContract2);
							flag2 = (codeTypeReference2 != null && codeTypeReference2.ArrayRank == 0);
						}
						array[i] = dataContract2;
						if (dataContract2 == null)
						{
							flag = false;
						}
						if (codeTypeReference2 == null)
						{
							return null;
						}
						if (type == Globals.TypeOfNullable && !flag2)
						{
							return codeTypeReference2;
						}
						codeTypeReference.TypeArguments.Add(codeTypeReference2);
					}
					if (flag)
					{
						dataContract = DataContract.GetDataContract(type).BindGenericParameters(array, new Dictionary<DataContract, DataContract>());
					}
				}
				return codeTypeReference;
			}
			if (genInfo.Parameters != null)
			{
				return null;
			}
			dataContract = this.dataContractSet[genInfo.StableName];
			if (dataContract == null)
			{
				return null;
			}
			if (dataContract.GenericInfo != null)
			{
				return null;
			}
			return this.GetCodeTypeReference(dataContract);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00028B90 File Offset: 0x00026D90
		private bool NamespaceContainsType(CodeNamespace ns, string typeName)
		{
			foreach (object obj in ns.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				if (string.Compare(typeName, codeTypeDeclaration.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00028BF8 File Offset: 0x00026DF8
		private bool GlobalTypeNameConflicts(string clrNamespace, string typeName)
		{
			return string.IsNullOrEmpty(clrNamespace) && this.clrNamespaces.ContainsKey(typeName);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00028C10 File Offset: 0x00026E10
		private void AddGlobalTypeName(string typeName)
		{
			if (!this.clrNamespaces.ContainsKey(typeName))
			{
				this.clrNamespaces.Add(typeName, null);
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00028C30 File Offset: 0x00026E30
		private bool TypeContainsNestedType(CodeTypeDeclaration containingType, string typeName)
		{
			foreach (object obj in containingType.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeTypeDeclaration && string.Compare(typeName, ((CodeTypeDeclaration)codeTypeMember).Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00028CA8 File Offset: 0x00026EA8
		private string GetNameForAttribute(string name)
		{
			string text = XmlConvert.DecodeName(name);
			if (string.CompareOrdinal(name, text) == 0)
			{
				return name;
			}
			string strB = DataContract.EncodeLocalName(text);
			if (string.CompareOrdinal(name, strB) != 0)
			{
				return name;
			}
			return text;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00028CDA File Offset: 0x00026EDA
		private void AddSerializableAttribute(bool generateSerializable, CodeTypeDeclaration type, ContractCodeDomInfo contractCodeDomInfo)
		{
			if (generateSerializable)
			{
				type.CustomAttributes.Add(this.SerializableAttribute);
				this.AddImportStatement(Globals.TypeOfSerializableAttribute.Namespace, contractCodeDomInfo.CodeNamespace);
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00028D08 File Offset: 0x00026F08
		private void ExportClassDataContract(ClassDataContract classDataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			this.GenerateType(classDataContract, contractCodeDomInfo);
			if (contractCodeDomInfo.ReferencedTypeExists)
			{
				return;
			}
			CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
			if (this.SupportsPartialTypes)
			{
				typeDeclaration.IsPartial = true;
			}
			if (classDataContract.IsValueType && this.SupportsDeclareValueTypes)
			{
				typeDeclaration.IsStruct = true;
			}
			else
			{
				typeDeclaration.IsClass = true;
			}
			string nameForAttribute = this.GetNameForAttribute(classDataContract.StableName.Name);
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfDataContractAttribute));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(nameForAttribute)));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(classDataContract.StableName.Namespace)));
			if (classDataContract.IsReference)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsReference", new CodePrimitiveExpression(classDataContract.IsReference)));
			}
			typeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			this.AddImportStatement(Globals.TypeOfDataContractAttribute.Namespace, contractCodeDomInfo.CodeNamespace);
			this.AddSerializableAttribute(this.GenerateSerializableTypes, typeDeclaration, contractCodeDomInfo);
			this.AddKnownTypes(classDataContract, contractCodeDomInfo);
			bool raisePropertyChanged = this.EnableDataBinding && this.SupportsDeclareEvents;
			if (classDataContract.BaseContract == null)
			{
				if (!typeDeclaration.IsStruct)
				{
					typeDeclaration.BaseTypes.Add(Globals.TypeOfObject);
				}
				this.AddExtensionData(contractCodeDomInfo);
				this.AddPropertyChangedNotifier(contractCodeDomInfo, typeDeclaration.IsStruct);
			}
			else
			{
				ContractCodeDomInfo contractCodeDomInfo2 = this.GetContractCodeDomInfo(classDataContract.BaseContract);
				typeDeclaration.BaseTypes.Add(contractCodeDomInfo2.TypeReference);
				this.AddBaseMemberNames(contractCodeDomInfo2, contractCodeDomInfo);
				if (contractCodeDomInfo2.ReferencedTypeExists)
				{
					Type type = (Type)contractCodeDomInfo2.TypeReference.UserData[CodeExporter.codeUserDataActualTypeKey];
					this.ThrowIfReferencedBaseTypeSealed(type, classDataContract);
					if (!Globals.TypeOfIExtensibleDataObject.IsAssignableFrom(type))
					{
						this.AddExtensionData(contractCodeDomInfo);
					}
					if (!Globals.TypeOfIPropertyChange.IsAssignableFrom(type))
					{
						this.AddPropertyChangedNotifier(contractCodeDomInfo, typeDeclaration.IsStruct);
					}
					else
					{
						raisePropertyChanged = false;
					}
				}
			}
			if (classDataContract.Members != null)
			{
				for (int i = 0; i < classDataContract.Members.Count; i++)
				{
					DataMember dataMember = classDataContract.Members[i];
					CodeTypeReference elementTypeReference = this.GetElementTypeReference(dataMember.MemberTypeContract, dataMember.IsNullable && dataMember.MemberTypeContract.IsValueType);
					string nameForAttribute2 = this.GetNameForAttribute(dataMember.Name);
					string memberName = this.GetMemberName(nameForAttribute2, contractCodeDomInfo);
					string memberName2 = this.GetMemberName(CodeExporter.AppendToValidClrIdentifier(memberName, "Field"), contractCodeDomInfo);
					CodeMemberField codeMemberField = new CodeMemberField();
					codeMemberField.Type = elementTypeReference;
					codeMemberField.Name = memberName2;
					codeMemberField.Attributes = MemberAttributes.Private;
					CodeMemberProperty codeMemberProperty = this.CreateProperty(elementTypeReference, memberName, memberName2, dataMember.MemberTypeContract.IsValueType && this.SupportsDeclareValueTypes, raisePropertyChanged);
					if (this.dataContractSet.DataContractSurrogate != null)
					{
						codeMemberProperty.UserData.Add(CodeExporter.surrogateDataKey, this.dataContractSet.GetSurrogateData(dataMember));
					}
					CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfDataMemberAttribute));
					if (nameForAttribute2 != codeMemberProperty.Name)
					{
						codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(nameForAttribute2)));
					}
					if (dataMember.IsRequired)
					{
						codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("IsRequired", new CodePrimitiveExpression(dataMember.IsRequired)));
					}
					if (!dataMember.EmitDefaultValue)
					{
						codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("EmitDefaultValue", new CodePrimitiveExpression(dataMember.EmitDefaultValue)));
					}
					if (dataMember.Order != 0)
					{
						codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Order", new CodePrimitiveExpression(dataMember.Order)));
					}
					codeMemberProperty.CustomAttributes.Add(codeAttributeDeclaration2);
					if (this.GenerateSerializableTypes && !dataMember.IsRequired)
					{
						CodeAttributeDeclaration value = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfOptionalFieldAttribute));
						codeMemberField.CustomAttributes.Add(value);
					}
					typeDeclaration.Members.Add(codeMemberField);
					typeDeclaration.Members.Add(codeMemberProperty);
				}
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00029133 File Offset: 0x00027333
		private bool CanDeclareAssemblyAttribute(ContractCodeDomInfo contractCodeDomInfo)
		{
			return this.SupportsAssemblyAttributes && !contractCodeDomInfo.UsesWildcardNamespace;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00029148 File Offset: 0x00027348
		private bool NeedsExplicitNamespace(string dataContractNamespace, string clrNamespace)
		{
			return DataContract.GetDefaultStableNamespace(clrNamespace) != dataContractNamespace;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00029158 File Offset: 0x00027358
		internal ICollection<CodeTypeReference> GetKnownTypeReferences(DataContract dataContract)
		{
			Dictionary<XmlQualifiedName, DataContract> knownTypeContracts = this.GetKnownTypeContracts(dataContract);
			if (knownTypeContracts == null)
			{
				return null;
			}
			ICollection<DataContract> values = knownTypeContracts.Values;
			if (values == null || values.Count == 0)
			{
				return null;
			}
			List<CodeTypeReference> list = new List<CodeTypeReference>();
			foreach (DataContract dataContract2 in values)
			{
				list.Add(this.GetCodeTypeReference(dataContract2));
			}
			return list;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000291D0 File Offset: 0x000273D0
		private Dictionary<XmlQualifiedName, DataContract> GetKnownTypeContracts(DataContract dataContract)
		{
			if (this.dataContractSet.KnownTypesForObject != null && SchemaImporter.IsObjectContract(dataContract))
			{
				return this.dataContractSet.KnownTypesForObject;
			}
			if (dataContract is ClassDataContract)
			{
				ContractCodeDomInfo contractCodeDomInfo = this.GetContractCodeDomInfo(dataContract);
				if (!contractCodeDomInfo.IsProcessed)
				{
					this.GenerateType(dataContract, contractCodeDomInfo);
				}
				if (contractCodeDomInfo.ReferencedTypeExists)
				{
					return this.GetKnownTypeContracts((ClassDataContract)dataContract, new Dictionary<DataContract, object>());
				}
			}
			return null;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002923C File Offset: 0x0002743C
		private Dictionary<XmlQualifiedName, DataContract> GetKnownTypeContracts(ClassDataContract dataContract, Dictionary<DataContract, object> handledContracts)
		{
			if (handledContracts.ContainsKey(dataContract))
			{
				return dataContract.KnownDataContracts;
			}
			handledContracts.Add(dataContract, null);
			if (dataContract.Members != null)
			{
				bool flag = false;
				foreach (DataMember dataMember in dataContract.Members)
				{
					DataContract memberTypeContract = dataMember.MemberTypeContract;
					if (!flag && this.dataContractSet.KnownTypesForObject != null && SchemaImporter.IsObjectContract(memberTypeContract))
					{
						this.AddKnownTypeContracts(dataContract, this.dataContractSet.KnownTypesForObject);
						flag = true;
					}
					else if (memberTypeContract is ClassDataContract)
					{
						ContractCodeDomInfo contractCodeDomInfo = this.GetContractCodeDomInfo(memberTypeContract);
						if (!contractCodeDomInfo.IsProcessed)
						{
							this.GenerateType(memberTypeContract, contractCodeDomInfo);
						}
						if (contractCodeDomInfo.ReferencedTypeExists)
						{
							this.AddKnownTypeContracts(dataContract, this.GetKnownTypeContracts((ClassDataContract)memberTypeContract, handledContracts));
						}
					}
				}
			}
			return dataContract.KnownDataContracts;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00029324 File Offset: 0x00027524
		[SecuritySafeCritical]
		private void AddKnownTypeContracts(ClassDataContract dataContract, Dictionary<XmlQualifiedName, DataContract> knownContracts)
		{
			if (knownContracts == null || knownContracts.Count == 0)
			{
				return;
			}
			if (dataContract.KnownDataContracts == null)
			{
				dataContract.KnownDataContracts = new Dictionary<XmlQualifiedName, DataContract>();
			}
			foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in knownContracts)
			{
				if (dataContract.StableName != keyValuePair.Key && !dataContract.KnownDataContracts.ContainsKey(keyValuePair.Key) && !keyValuePair.Value.IsBuiltInDataContract)
				{
					dataContract.KnownDataContracts.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000293DC File Offset: 0x000275DC
		private void AddKnownTypes(ClassDataContract dataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			Dictionary<XmlQualifiedName, DataContract> knownTypeContracts = this.GetKnownTypeContracts(dataContract, new Dictionary<DataContract, object>());
			if (knownTypeContracts == null || knownTypeContracts.Count == 0)
			{
				return;
			}
			foreach (DataContract dataContract2 in ((IEnumerable<DataContract>)knownTypeContracts.Values))
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfKnownTypeAttribute));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(this.GetCodeTypeReference(dataContract2))));
				contractCodeDomInfo.TypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			}
			this.AddImportStatement(Globals.TypeOfKnownTypeAttribute.Namespace, contractCodeDomInfo.CodeNamespace);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00029490 File Offset: 0x00027690
		private CodeTypeReference WrapNullable(CodeTypeReference memberType)
		{
			if (!this.SupportsGenericTypeReference)
			{
				return memberType;
			}
			CodeTypeReference codeTypeReference = this.GetCodeTypeReference(Globals.TypeOfNullable);
			codeTypeReference.TypeArguments.Add(memberType);
			return codeTypeReference;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000294B4 File Offset: 0x000276B4
		private void AddExtensionData(ContractCodeDomInfo contractCodeDomInfo)
		{
			if (contractCodeDomInfo != null && contractCodeDomInfo.TypeDeclaration != null)
			{
				CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
				typeDeclaration.BaseTypes.Add(DataContract.GetClrTypeFullName(Globals.TypeOfIExtensibleDataObject));
				CodeMemberField extensionDataObjectField = this.ExtensionDataObjectField;
				if (this.GenerateSerializableTypes)
				{
					CodeAttributeDeclaration value = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfNonSerializedAttribute));
					extensionDataObjectField.CustomAttributes.Add(value);
				}
				typeDeclaration.Members.Add(extensionDataObjectField);
				contractCodeDomInfo.GetMemberNames().Add(extensionDataObjectField.Name, null);
				CodeMemberProperty extensionDataObjectProperty = this.ExtensionDataObjectProperty;
				typeDeclaration.Members.Add(extensionDataObjectProperty);
				contractCodeDomInfo.GetMemberNames().Add(extensionDataObjectProperty.Name, null);
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00029560 File Offset: 0x00027760
		private void AddPropertyChangedNotifier(ContractCodeDomInfo contractCodeDomInfo, bool isValueType)
		{
			if (this.EnableDataBinding && this.SupportsDeclareEvents && contractCodeDomInfo != null && contractCodeDomInfo.TypeDeclaration != null)
			{
				CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
				typeDeclaration.BaseTypes.Add(this.CodeTypeIPropertyChange);
				CodeMemberEvent propertyChangedEvent = this.PropertyChangedEvent;
				typeDeclaration.Members.Add(propertyChangedEvent);
				CodeMemberMethod raisePropertyChangedEventMethod = this.RaisePropertyChangedEventMethod;
				if (!isValueType)
				{
					raisePropertyChangedEventMethod.Attributes |= MemberAttributes.Family;
				}
				typeDeclaration.Members.Add(raisePropertyChangedEventMethod);
				contractCodeDomInfo.GetMemberNames().Add(propertyChangedEvent.Name, null);
				contractCodeDomInfo.GetMemberNames().Add(raisePropertyChangedEventMethod.Name, null);
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002960C File Offset: 0x0002780C
		private void ThrowIfReferencedBaseTypeSealed(Type baseType, DataContract dataContract)
		{
			if (baseType.IsSealed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Cannod drive from sealed reference type '{2}', for '{0}' element in '{1}' namespace.", new object[]
				{
					dataContract.StableName.Name,
					dataContract.StableName.Namespace,
					DataContract.GetClrTypeFullName(baseType)
				})));
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00029664 File Offset: 0x00027864
		private void ExportEnumDataContract(EnumDataContract enumDataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			this.GenerateType(enumDataContract, contractCodeDomInfo);
			if (contractCodeDomInfo.ReferencedTypeExists)
			{
				return;
			}
			CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
			typeDeclaration.IsEnum = true;
			typeDeclaration.BaseTypes.Add(EnumDataContract.GetBaseType(enumDataContract.BaseContractName));
			if (enumDataContract.IsFlags)
			{
				typeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfFlagsAttribute)));
				this.AddImportStatement(Globals.TypeOfFlagsAttribute.Namespace, contractCodeDomInfo.CodeNamespace);
			}
			string nameForAttribute = this.GetNameForAttribute(enumDataContract.StableName.Name);
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfDataContractAttribute));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(nameForAttribute)));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(enumDataContract.StableName.Namespace)));
			typeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			this.AddImportStatement(Globals.TypeOfDataContractAttribute.Namespace, contractCodeDomInfo.CodeNamespace);
			if (enumDataContract.Members != null)
			{
				for (int i = 0; i < enumDataContract.Members.Count; i++)
				{
					string name = enumDataContract.Members[i].Name;
					long num = enumDataContract.Values[i];
					CodeMemberField codeMemberField = new CodeMemberField();
					if (enumDataContract.IsULong)
					{
						codeMemberField.InitExpression = new CodeSnippetExpression(enumDataContract.GetStringFromEnumValue(num));
					}
					else
					{
						codeMemberField.InitExpression = new CodePrimitiveExpression(num);
					}
					codeMemberField.Name = this.GetMemberName(name, contractCodeDomInfo);
					CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfEnumMemberAttribute));
					if (codeMemberField.Name != name)
					{
						codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Value", new CodePrimitiveExpression(name)));
					}
					codeMemberField.CustomAttributes.Add(codeAttributeDeclaration2);
					typeDeclaration.Members.Add(codeMemberField);
				}
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00029854 File Offset: 0x00027A54
		private void ExportISerializableDataContract(ClassDataContract dataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			this.GenerateType(dataContract, contractCodeDomInfo);
			if (contractCodeDomInfo.ReferencedTypeExists)
			{
				return;
			}
			if (DataContract.GetDefaultStableNamespace(contractCodeDomInfo.ClrNamespace) != dataContract.StableName.Namespace)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Invalid CLR namespace '{3}' is generated for ISerializable type '{0}' in '{1}' namespace. Data contract namespace from the URI would be generated as '{2}'.", new object[]
				{
					dataContract.StableName.Name,
					dataContract.StableName.Namespace,
					DataContract.GetDataContractNamespaceFromUri(dataContract.StableName.Namespace),
					contractCodeDomInfo.ClrNamespace
				})));
			}
			string nameForAttribute = this.GetNameForAttribute(dataContract.StableName.Name);
			int num = nameForAttribute.LastIndexOf('.');
			string b = (num <= 0 || num == nameForAttribute.Length - 1) ? nameForAttribute : nameForAttribute.Substring(num + 1);
			if (contractCodeDomInfo.TypeDeclaration.Name != b)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Invalid CLR name '{2}' is generated for ISerializable type '{0}' in '{1}' namespace.", new object[]
				{
					dataContract.StableName.Name,
					dataContract.StableName.Namespace,
					contractCodeDomInfo.TypeDeclaration.Name
				})));
			}
			CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
			if (this.SupportsPartialTypes)
			{
				typeDeclaration.IsPartial = true;
			}
			if (dataContract.IsValueType && this.SupportsDeclareValueTypes)
			{
				typeDeclaration.IsStruct = true;
			}
			else
			{
				typeDeclaration.IsClass = true;
			}
			this.AddSerializableAttribute(true, typeDeclaration, contractCodeDomInfo);
			this.AddKnownTypes(dataContract, contractCodeDomInfo);
			if (dataContract.BaseContract == null)
			{
				if (!typeDeclaration.IsStruct)
				{
					typeDeclaration.BaseTypes.Add(Globals.TypeOfObject);
				}
				typeDeclaration.BaseTypes.Add(DataContract.GetClrTypeFullName(Globals.TypeOfISerializable));
				typeDeclaration.Members.Add(this.ISerializableBaseConstructor);
				typeDeclaration.Members.Add(this.SerializationInfoField);
				typeDeclaration.Members.Add(this.SerializationInfoProperty);
				typeDeclaration.Members.Add(this.GetObjectDataMethod);
				this.AddPropertyChangedNotifier(contractCodeDomInfo, typeDeclaration.IsStruct);
				return;
			}
			ContractCodeDomInfo contractCodeDomInfo2 = this.GetContractCodeDomInfo(dataContract.BaseContract);
			this.GenerateType(dataContract.BaseContract, contractCodeDomInfo2);
			typeDeclaration.BaseTypes.Add(contractCodeDomInfo2.TypeReference);
			if (contractCodeDomInfo2.ReferencedTypeExists)
			{
				Type baseType = (Type)contractCodeDomInfo2.TypeReference.UserData[CodeExporter.codeUserDataActualTypeKey];
				this.ThrowIfReferencedBaseTypeSealed(baseType, dataContract);
			}
			typeDeclaration.Members.Add(this.ISerializableDerivedConstructor);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00029AB8 File Offset: 0x00027CB8
		private void GenerateKeyValueType(ClassDataContract keyValueContract)
		{
			if (keyValueContract != null && this.dataContractSet[keyValueContract.StableName] == null && this.dataContractSet.GetContractCodeDomInfo(keyValueContract) == null)
			{
				ContractCodeDomInfo contractCodeDomInfo = new ContractCodeDomInfo();
				this.dataContractSet.SetContractCodeDomInfo(keyValueContract, contractCodeDomInfo);
				this.ExportClassDataContract(keyValueContract, contractCodeDomInfo);
				contractCodeDomInfo.IsProcessed = true;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00029B10 File Offset: 0x00027D10
		private void ExportCollectionDataContract(CollectionDataContract collectionContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			this.GenerateType(collectionContract, contractCodeDomInfo);
			if (contractCodeDomInfo.ReferencedTypeExists)
			{
				return;
			}
			string nameForAttribute = this.GetNameForAttribute(collectionContract.StableName.Name);
			if (!this.SupportsGenericTypeReference)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("For '{0}' in '{1}' namespace, generic type cannot be referenced as the base type.", new object[]
				{
					nameForAttribute,
					collectionContract.StableName.Namespace
				})));
			}
			DataContract itemContract = collectionContract.ItemContract;
			bool isItemTypeNullable = collectionContract.IsItemTypeNullable;
			CodeTypeReference codeTypeReference;
			bool flag = this.TryGetReferencedDictionaryType(collectionContract, out codeTypeReference);
			if (!flag)
			{
				if (collectionContract.IsDictionary)
				{
					this.GenerateKeyValueType(collectionContract.ItemContract as ClassDataContract);
				}
				if (!this.TryGetReferencedListType(itemContract, isItemTypeNullable, out codeTypeReference))
				{
					if (!this.SupportsGenericTypeReference)
					{
						string text = "ArrayOf" + itemContract.StableName.Name;
						string collectionNamespace = DataContract.GetCollectionNamespace(itemContract.StableName.Namespace);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Referenced base type does not exist. Data contract name: '{0}' in '{1}' namespace, expected type: '{2}' in '{3}' namespace. Collection can be '{4}' or '{5}'.", new object[]
						{
							nameForAttribute,
							collectionContract.StableName.Namespace,
							text,
							collectionNamespace,
							DataContract.GetClrTypeFullName(Globals.TypeOfIListGeneric),
							DataContract.GetClrTypeFullName(Globals.TypeOfICollectionGeneric)
						})));
					}
					codeTypeReference = this.GetCodeTypeReference(Globals.TypeOfListGeneric);
					codeTypeReference.TypeArguments.Add(this.GetElementTypeReference(itemContract, isItemTypeNullable));
				}
			}
			CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
			typeDeclaration.BaseTypes.Add(codeTypeReference);
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfCollectionDataContractAttribute));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(nameForAttribute)));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(collectionContract.StableName.Namespace)));
			if (collectionContract.IsReference)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsReference", new CodePrimitiveExpression(collectionContract.IsReference)));
			}
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ItemName", new CodePrimitiveExpression(this.GetNameForAttribute(collectionContract.ItemName))));
			if (flag)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("KeyName", new CodePrimitiveExpression(this.GetNameForAttribute(collectionContract.KeyName))));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ValueName", new CodePrimitiveExpression(this.GetNameForAttribute(collectionContract.ValueName))));
			}
			typeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			this.AddImportStatement(Globals.TypeOfCollectionDataContractAttribute.Namespace, contractCodeDomInfo.CodeNamespace);
			this.AddSerializableAttribute(this.GenerateSerializableTypes, typeDeclaration, contractCodeDomInfo);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00029DAC File Offset: 0x00027FAC
		private void ExportXmlDataContract(XmlDataContract xmlDataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			this.GenerateType(xmlDataContract, contractCodeDomInfo);
			if (contractCodeDomInfo.ReferencedTypeExists)
			{
				return;
			}
			CodeTypeDeclaration typeDeclaration = contractCodeDomInfo.TypeDeclaration;
			if (this.SupportsPartialTypes)
			{
				typeDeclaration.IsPartial = true;
			}
			if (xmlDataContract.IsValueType)
			{
				typeDeclaration.IsStruct = true;
			}
			else
			{
				typeDeclaration.IsClass = true;
				typeDeclaration.BaseTypes.Add(Globals.TypeOfObject);
			}
			this.AddSerializableAttribute(this.GenerateSerializableTypes, typeDeclaration, contractCodeDomInfo);
			typeDeclaration.BaseTypes.Add(DataContract.GetClrTypeFullName(Globals.TypeOfIXmlSerializable));
			typeDeclaration.Members.Add(this.NodeArrayField);
			typeDeclaration.Members.Add(this.NodeArrayProperty);
			typeDeclaration.Members.Add(this.ReadXmlMethod);
			typeDeclaration.Members.Add(this.WriteXmlMethod);
			typeDeclaration.Members.Add(this.GetSchemaMethod);
			if (xmlDataContract.IsAnonymous && !xmlDataContract.HasRoot)
			{
				typeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfXmlSchemaProviderAttribute), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(this.NullReference),
					new CodeAttributeArgument("IsAny", new CodePrimitiveExpression(true))
				}));
			}
			else
			{
				typeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfXmlSchemaProviderAttribute), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression("ExportSchema"))
				}));
				CodeMemberField codeMemberField = new CodeMemberField(Globals.TypeOfXmlQualifiedName, CodeExporter.typeNameFieldName);
				codeMemberField.Attributes |= (MemberAttributes)20483;
				XmlQualifiedName xmlQualifiedName = xmlDataContract.IsAnonymous ? SchemaImporter.ImportActualType(xmlDataContract.XsdType.Annotation, xmlDataContract.StableName, xmlDataContract.StableName) : xmlDataContract.StableName;
				codeMemberField.InitExpression = new CodeObjectCreateExpression(Globals.TypeOfXmlQualifiedName, new CodeExpression[]
				{
					new CodePrimitiveExpression(xmlQualifiedName.Name),
					new CodePrimitiveExpression(xmlQualifiedName.Namespace)
				});
				typeDeclaration.Members.Add(codeMemberField);
				typeDeclaration.Members.Add(this.GetSchemaStaticMethod);
				bool flag = (xmlDataContract.TopLevelElementName != null && xmlDataContract.TopLevelElementName.Value != xmlDataContract.StableName.Name) || (xmlDataContract.TopLevelElementNamespace != null && xmlDataContract.TopLevelElementNamespace.Value != xmlDataContract.StableName.Namespace);
				if (flag || !xmlDataContract.IsTopLevelElementNullable)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfXmlRootAttribute));
					if (flag)
					{
						if (xmlDataContract.TopLevelElementName != null)
						{
							codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(xmlDataContract.TopLevelElementName.Value)));
						}
						if (xmlDataContract.TopLevelElementNamespace != null)
						{
							codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(xmlDataContract.TopLevelElementNamespace.Value)));
						}
					}
					if (!xmlDataContract.IsTopLevelElementNullable)
					{
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(false)));
					}
					typeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
				}
			}
			this.AddPropertyChangedNotifier(contractCodeDomInfo, typeDeclaration.IsStruct);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002A0C8 File Offset: 0x000282C8
		private CodeNamespace GetCodeNamespace(string clrNamespace, string dataContractNamespace, ContractCodeDomInfo contractCodeDomInfo)
		{
			if (contractCodeDomInfo.CodeNamespace != null)
			{
				return contractCodeDomInfo.CodeNamespace;
			}
			CodeNamespaceCollection codeNamespaceCollection = this.codeCompileUnit.Namespaces;
			foreach (object obj in codeNamespaceCollection)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				if (codeNamespace.Name == clrNamespace)
				{
					contractCodeDomInfo.CodeNamespace = codeNamespace;
					return codeNamespace;
				}
			}
			CodeNamespace codeNamespace2 = new CodeNamespace(clrNamespace);
			codeNamespaceCollection.Add(codeNamespace2);
			if (this.CanDeclareAssemblyAttribute(contractCodeDomInfo) && this.NeedsExplicitNamespace(dataContractNamespace, clrNamespace))
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(DataContract.GetClrTypeFullName(Globals.TypeOfContractNamespaceAttribute));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(dataContractNamespace)));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ClrNamespace", new CodePrimitiveExpression(clrNamespace)));
				this.codeCompileUnit.AssemblyCustomAttributes.Add(codeAttributeDeclaration);
			}
			contractCodeDomInfo.CodeNamespace = codeNamespace2;
			return codeNamespace2;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002A1D8 File Offset: 0x000283D8
		private string GetMemberName(string memberName, ContractCodeDomInfo contractCodeDomInfo)
		{
			memberName = CodeExporter.GetClrIdentifier(memberName, "GeneratedMember");
			if (memberName == contractCodeDomInfo.TypeDeclaration.Name)
			{
				memberName = CodeExporter.AppendToValidClrIdentifier(memberName, "Member");
			}
			if (contractCodeDomInfo.GetMemberNames().ContainsKey(memberName))
			{
				int num = 1;
				string text;
				for (;;)
				{
					text = CodeExporter.AppendToValidClrIdentifier(memberName, num.ToString(NumberFormatInfo.InvariantInfo));
					if (!contractCodeDomInfo.GetMemberNames().ContainsKey(text))
					{
						break;
					}
					num++;
				}
				memberName = text;
			}
			contractCodeDomInfo.GetMemberNames().Add(memberName, null);
			return memberName;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002A260 File Offset: 0x00028460
		private void AddBaseMemberNames(ContractCodeDomInfo baseContractCodeDomInfo, ContractCodeDomInfo contractCodeDomInfo)
		{
			if (!baseContractCodeDomInfo.ReferencedTypeExists)
			{
				Dictionary<string, object> memberNames = baseContractCodeDomInfo.GetMemberNames();
				Dictionary<string, object> memberNames2 = contractCodeDomInfo.GetMemberNames();
				foreach (KeyValuePair<string, object> keyValuePair in memberNames)
				{
					memberNames2.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002A2D0 File Offset: 0x000284D0
		[SecuritySafeCritical]
		private static string GetClrIdentifier(string identifier, string defaultIdentifier)
		{
			if (identifier.Length <= 511 && CodeGenerator.IsValidLanguageIndependentIdentifier(identifier))
			{
				return identifier;
			}
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < identifier.Length && stringBuilder.Length < 511)
			{
				char c = identifier[num];
				if (CodeExporter.IsValid(c))
				{
					if (flag && !CodeExporter.IsValidStart(c))
					{
						stringBuilder.Append("_");
					}
					stringBuilder.Append(c);
					flag = false;
				}
				num++;
			}
			if (stringBuilder.Length == 0)
			{
				return defaultIdentifier;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002A35C File Offset: 0x0002855C
		private static string AppendToValidClrIdentifier(string identifier, string appendString)
		{
			int num = 511 - identifier.Length;
			int length = appendString.Length;
			if (num < length)
			{
				identifier = identifier.Substring(0, 511 - length);
			}
			identifier += appendString;
			return identifier;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002A39C File Offset: 0x0002859C
		private string GetClrNamespace(DataContract dataContract, ContractCodeDomInfo contractCodeDomInfo)
		{
			string text = contractCodeDomInfo.ClrNamespace;
			bool usesWildcardNamespace = false;
			if (text == null)
			{
				if (!this.Namespaces.TryGetValue(dataContract.StableName.Namespace, out text))
				{
					if (this.Namespaces.TryGetValue(CodeExporter.wildcardNamespaceMapping, out text))
					{
						usesWildcardNamespace = true;
					}
					else
					{
						text = CodeExporter.GetClrNamespace(dataContract.StableName.Namespace);
						if (this.ClrNamespaces.ContainsKey(text))
						{
							int num = 1;
							string text2;
							for (;;)
							{
								text2 = ((text.Length == 0) ? "GeneratedNamespace" : text) + num.ToString(NumberFormatInfo.InvariantInfo);
								if (!this.ClrNamespaces.ContainsKey(text2))
								{
									break;
								}
								num++;
							}
							text = text2;
						}
						this.AddNamespacePair(dataContract.StableName.Namespace, text);
					}
				}
				contractCodeDomInfo.ClrNamespace = text;
				contractCodeDomInfo.UsesWildcardNamespace = usesWildcardNamespace;
			}
			return text;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002A46B File Offset: 0x0002866B
		private void AddNamespacePair(string dataContractNamespace, string clrNamespace)
		{
			this.Namespaces.Add(dataContractNamespace, clrNamespace);
			this.ClrNamespaces.Add(clrNamespace, dataContractNamespace);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002A488 File Offset: 0x00028688
		private void AddImportStatement(string clrNamespace, CodeNamespace codeNamespace)
		{
			if (clrNamespace == codeNamespace.Name)
			{
				return;
			}
			CodeNamespaceImportCollection imports = codeNamespace.Imports;
			using (IEnumerator enumerator = imports.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((CodeNamespaceImport)enumerator.Current).Namespace == clrNamespace)
					{
						return;
					}
				}
			}
			imports.Add(new CodeNamespaceImport(clrNamespace));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002A508 File Offset: 0x00028708
		private static string GetClrNamespace(string dataContractNamespace)
		{
			if (dataContractNamespace == null || dataContractNamespace.Length == 0)
			{
				return string.Empty;
			}
			Uri uri = null;
			StringBuilder stringBuilder = new StringBuilder();
			if (Uri.TryCreate(dataContractNamespace, UriKind.RelativeOrAbsolute, out uri))
			{
				Dictionary<string, object> fragments = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
				if (!uri.IsAbsoluteUri)
				{
					CodeExporter.AddToNamespace(stringBuilder, uri.OriginalString, fragments);
				}
				else
				{
					string absoluteUri = uri.AbsoluteUri;
					if (absoluteUri.StartsWith("http://schemas.datacontract.org/2004/07/", StringComparison.Ordinal))
					{
						CodeExporter.AddToNamespace(stringBuilder, absoluteUri.Substring("http://schemas.datacontract.org/2004/07/".Length), fragments);
					}
					else
					{
						string host = uri.Host;
						if (host != null)
						{
							CodeExporter.AddToNamespace(stringBuilder, host, fragments);
						}
						string pathAndQuery = uri.PathAndQuery;
						if (pathAndQuery != null)
						{
							CodeExporter.AddToNamespace(stringBuilder, pathAndQuery, fragments);
						}
					}
				}
			}
			if (stringBuilder.Length == 0)
			{
				return string.Empty;
			}
			int num = stringBuilder.Length;
			if (stringBuilder[stringBuilder.Length - 1] == '.')
			{
				num--;
			}
			num = Math.Min(511, num);
			return stringBuilder.ToString(0, num);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002A5F8 File Offset: 0x000287F8
		private static void AddToNamespace(StringBuilder builder, string fragment, Dictionary<string, object> fragments)
		{
			if (fragment == null)
			{
				return;
			}
			bool flag = true;
			int length = builder.Length;
			int num = 0;
			int num2 = 0;
			while (num2 < fragment.Length && builder.Length < 511)
			{
				char c = fragment[num2];
				if (CodeExporter.IsValid(c))
				{
					if (flag && !CodeExporter.IsValidStart(c))
					{
						builder.Append("_");
					}
					builder.Append(c);
					num++;
					flag = false;
				}
				else if ((c == '.' || c == '/' || c == ':') && (builder.Length == 1 || (builder.Length > 1 && builder[builder.Length - 1] != '.')))
				{
					CodeExporter.AddNamespaceFragment(builder, length, num, fragments);
					builder.Append('.');
					length = builder.Length;
					num = 0;
					flag = true;
				}
				num2++;
			}
			CodeExporter.AddNamespaceFragment(builder, length, num, fragments);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002A6D0 File Offset: 0x000288D0
		private static void AddNamespaceFragment(StringBuilder builder, int fragmentOffset, int fragmentLength, Dictionary<string, object> fragments)
		{
			if (fragmentLength == 0)
			{
				return;
			}
			string text = builder.ToString(fragmentOffset, fragmentLength);
			if (fragments.ContainsKey(text))
			{
				int num = 1;
				string text2;
				string text3;
				for (;;)
				{
					text2 = num.ToString(NumberFormatInfo.InvariantInfo);
					text3 = CodeExporter.AppendToValidClrIdentifier(text, text2);
					if (!fragments.ContainsKey(text3))
					{
						break;
					}
					if (num == 2147483647)
					{
						goto Block_4;
					}
					num++;
				}
				builder.Append(text2);
				text = text3;
				goto IL_6F;
				Block_4:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Cannot compute unique name for '{0}'.", new object[]
				{
					text
				})));
			}
			IL_6F:
			fragments.Add(text, null);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002A754 File Offset: 0x00028954
		private static bool IsValidStart(char c)
		{
			return char.GetUnicodeCategory(c) != UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002A764 File Offset: 0x00028964
		private static bool IsValid(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory <= UnicodeCategory.SpacingCombiningMark || unicodeCategory == UnicodeCategory.DecimalDigitNumber || unicodeCategory == UnicodeCategory.ConnectorPunctuation;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002A788 File Offset: 0x00028988
		private CodeTypeReference CodeTypeIPropertyChange
		{
			get
			{
				return this.GetCodeTypeReference(typeof(INotifyPropertyChanged));
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002A79A File Offset: 0x0002899A
		private CodeThisReferenceExpression ThisReference
		{
			get
			{
				return new CodeThisReferenceExpression();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002A7A1 File Offset: 0x000289A1
		private CodePrimitiveExpression NullReference
		{
			get
			{
				return new CodePrimitiveExpression(null);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002A7A9 File Offset: 0x000289A9
		private CodeParameterDeclarationExpression SerializationInfoParameter
		{
			get
			{
				return new CodeParameterDeclarationExpression(this.GetCodeTypeReference(Globals.TypeOfSerializationInfo), "info");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002A7C0 File Offset: 0x000289C0
		private CodeParameterDeclarationExpression StreamingContextParameter
		{
			get
			{
				return new CodeParameterDeclarationExpression(this.GetCodeTypeReference(Globals.TypeOfStreamingContext), "context");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002A7D7 File Offset: 0x000289D7
		private CodeAttributeDeclaration SerializableAttribute
		{
			get
			{
				return new CodeAttributeDeclaration(this.GetCodeTypeReference(Globals.TypeOfSerializableAttribute));
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0002A7E9 File Offset: 0x000289E9
		private CodeMemberProperty NodeArrayProperty
		{
			get
			{
				return this.CreateProperty(this.GetCodeTypeReference(Globals.TypeOfXmlNodeArray), "Nodes", "nodesField", false);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0002A807 File Offset: 0x00028A07
		private CodeMemberField NodeArrayField
		{
			get
			{
				return new CodeMemberField
				{
					Type = this.GetCodeTypeReference(Globals.TypeOfXmlNodeArray),
					Name = "nodesField",
					Attributes = MemberAttributes.Private
				};
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002A838 File Offset: 0x00028A38
		private CodeMemberMethod ReadXmlMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "ReadXml";
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(typeof(XmlReader), "reader");
				codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
				codeMemberMethod.Attributes = (MemberAttributes)24578;
				codeMemberMethod.ImplementationTypes.Add(Globals.TypeOfIXmlSerializable);
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodeFieldReferenceExpression(this.ThisReference, "nodesField");
				codeAssignStatement.Right = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(this.GetCodeTypeReference(Globals.TypeOfXmlSerializableServices)), XmlSerializableServices.ReadNodesMethodName, new CodeExpression[]
				{
					new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name)
				});
				codeMemberMethod.Statements.Add(codeAssignStatement);
				return codeMemberMethod;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		private CodeMemberMethod WriteXmlMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "WriteXml";
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(typeof(XmlWriter), "writer");
				codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
				codeMemberMethod.Attributes = (MemberAttributes)24578;
				codeMemberMethod.ImplementationTypes.Add(Globals.TypeOfIXmlSerializable);
				codeMemberMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(this.GetCodeTypeReference(Globals.TypeOfXmlSerializableServices)), XmlSerializableServices.WriteNodesMethodName, new CodeExpression[]
				{
					new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name),
					new CodePropertyReferenceExpression(this.ThisReference, "Nodes")
				}));
				return codeMemberMethod;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002A9A0 File Offset: 0x00028BA0
		private CodeMemberMethod GetSchemaMethod
		{
			get
			{
				return new CodeMemberMethod
				{
					Name = "GetSchema",
					Attributes = (MemberAttributes)24578,
					ImplementationTypes = 
					{
						Globals.TypeOfIXmlSerializable
					},
					ReturnType = this.GetCodeTypeReference(typeof(XmlSchema)),
					Statements = 
					{
						new CodeMethodReturnStatement(this.NullReference)
					}
				};
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002AA08 File Offset: 0x00028C08
		private CodeMemberMethod GetSchemaStaticMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "ExportSchema";
				codeMemberMethod.ReturnType = this.GetCodeTypeReference(Globals.TypeOfXmlQualifiedName);
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(Globals.TypeOfXmlSchemaSet, "schemas");
				codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
				codeMemberMethod.Attributes = (MemberAttributes)24579;
				codeMemberMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(this.GetCodeTypeReference(typeof(XmlSerializableServices))), XmlSerializableServices.AddDefaultSchemaMethodName, new CodeExpression[]
				{
					new CodeArgumentReferenceExpression(codeParameterDeclarationExpression.Name),
					new CodeFieldReferenceExpression(null, CodeExporter.typeNameFieldName)
				}));
				codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, CodeExporter.typeNameFieldName)));
				return codeMemberMethod;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0002AACC File Offset: 0x00028CCC
		private CodeConstructor ISerializableBaseConstructor
		{
			get
			{
				CodeConstructor codeConstructor = new CodeConstructor();
				codeConstructor.Attributes = MemberAttributes.Public;
				codeConstructor.Parameters.Add(this.SerializationInfoParameter);
				codeConstructor.Parameters.Add(this.StreamingContextParameter);
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodePropertyReferenceExpression(this.ThisReference, "info");
				codeAssignStatement.Right = new CodeArgumentReferenceExpression("info");
				codeConstructor.Statements.Add(codeAssignStatement);
				if (this.EnableDataBinding && this.SupportsDeclareEvents && string.CompareOrdinal(this.FileExtension, "vb") != 0)
				{
					codeConstructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(this.ThisReference, this.PropertyChangedEvent.Name), this.NullReference));
				}
				return codeConstructor;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002AB98 File Offset: 0x00028D98
		private CodeConstructor ISerializableDerivedConstructor
		{
			get
			{
				return new CodeConstructor
				{
					Attributes = MemberAttributes.Public,
					Parameters = 
					{
						this.SerializationInfoParameter,
						this.StreamingContextParameter
					},
					BaseConstructorArgs = 
					{
						new CodeVariableReferenceExpression("info"),
						new CodeVariableReferenceExpression("context")
					}
				};
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002AC05 File Offset: 0x00028E05
		private CodeMemberField SerializationInfoField
		{
			get
			{
				return new CodeMemberField
				{
					Type = this.GetCodeTypeReference(Globals.TypeOfSerializationInfo),
					Name = "info",
					Attributes = MemberAttributes.Private
				};
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002AC33 File Offset: 0x00028E33
		private CodeMemberProperty SerializationInfoProperty
		{
			get
			{
				return this.CreateProperty(this.GetCodeTypeReference(Globals.TypeOfSerializationInfo), "SerializationInfo", "info", false);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002AC54 File Offset: 0x00028E54
		private CodeMemberMethod GetObjectDataMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "GetObjectData";
				codeMemberMethod.Parameters.Add(this.SerializationInfoParameter);
				codeMemberMethod.Parameters.Add(this.StreamingContextParameter);
				codeMemberMethod.Attributes = (MemberAttributes)24578;
				codeMemberMethod.ImplementationTypes.Add(Globals.TypeOfISerializable);
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(this.ThisReference, "SerializationInfo"), CodeBinaryOperatorType.IdentityEquality, this.NullReference);
				codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement());
				CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
				codeVariableDeclarationStatement.Type = this.GetCodeTypeReference(Globals.TypeOfSerializationInfoEnumerator);
				codeVariableDeclarationStatement.Name = "enumerator";
				codeVariableDeclarationStatement.InitExpression = new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(this.ThisReference, "SerializationInfo"), "GetEnumerator", Array.Empty<CodeExpression>());
				CodeVariableDeclarationStatement codeVariableDeclarationStatement2 = new CodeVariableDeclarationStatement();
				codeVariableDeclarationStatement2.Type = this.GetCodeTypeReference(Globals.TypeOfSerializationEntry);
				codeVariableDeclarationStatement2.Name = "entry";
				codeVariableDeclarationStatement2.InitExpression = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("enumerator"), "Current");
				CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement();
				CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("entry"), "Name");
				CodePropertyReferenceExpression codePropertyReferenceExpression2 = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("entry"), "Value");
				codeExpressionStatement.Expression = new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("info"), "AddValue", new CodeExpression[]
				{
					codePropertyReferenceExpression,
					codePropertyReferenceExpression2
				});
				CodeIterationStatement codeIterationStatement = new CodeIterationStatement();
				codeIterationStatement.TestExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("enumerator"), "MoveNext", Array.Empty<CodeExpression>());
				codeIterationStatement.InitStatement = (codeIterationStatement.IncrementStatement = new CodeSnippetStatement(string.Empty));
				codeIterationStatement.Statements.Add(codeVariableDeclarationStatement2);
				codeIterationStatement.Statements.Add(codeExpressionStatement);
				codeMemberMethod.Statements.Add(codeConditionStatement);
				codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
				codeMemberMethod.Statements.Add(codeIterationStatement);
				return codeMemberMethod;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002AE55 File Offset: 0x00029055
		private CodeMemberField ExtensionDataObjectField
		{
			get
			{
				return new CodeMemberField
				{
					Type = this.GetCodeTypeReference(Globals.TypeOfExtensionDataObject),
					Name = "extensionDataField",
					Attributes = MemberAttributes.Private
				};
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002AE84 File Offset: 0x00029084
		private CodeMemberProperty ExtensionDataObjectProperty
		{
			get
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Type = this.GetCodeTypeReference(Globals.TypeOfExtensionDataObject);
				codeMemberProperty.Name = "ExtensionData";
				codeMemberProperty.Attributes = (MemberAttributes)24578;
				codeMemberProperty.ImplementationTypes.Add(Globals.TypeOfIExtensibleDataObject);
				CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
				codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(this.ThisReference, "extensionDataField");
				codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodeFieldReferenceExpression(this.ThisReference, "extensionDataField");
				codeAssignStatement.Right = new CodePropertySetValueReferenceExpression();
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
				return codeMemberProperty;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002AF2C File Offset: 0x0002912C
		private CodeMemberMethod RaisePropertyChangedEventMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "RaisePropertyChanged";
				codeMemberMethod.Attributes = MemberAttributes.Final;
				CodeArgumentReferenceExpression codeArgumentReferenceExpression = new CodeArgumentReferenceExpression("propertyName");
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), codeArgumentReferenceExpression.ParameterName));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("propertyChanged");
				codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(PropertyChangedEventHandler), codeVariableReferenceExpression.VariableName, new CodeEventReferenceExpression(this.ThisReference, this.PropertyChangedEvent.Name)));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, this.NullReference), Array.Empty<CodeStatement>());
				codeMemberMethod.Statements.Add(codeConditionStatement);
				codeConditionStatement.TrueStatements.Add(new CodeDelegateInvokeExpression(codeVariableReferenceExpression, new CodeExpression[]
				{
					this.ThisReference,
					new CodeObjectCreateExpression(typeof(PropertyChangedEventArgs), new CodeExpression[]
					{
						codeArgumentReferenceExpression
					})
				}));
				return codeMemberMethod;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0002B024 File Offset: 0x00029224
		private CodeMemberEvent PropertyChangedEvent
		{
			get
			{
				return new CodeMemberEvent
				{
					Attributes = MemberAttributes.Public,
					Name = "PropertyChanged",
					Type = this.GetCodeTypeReference(typeof(PropertyChangedEventHandler)),
					ImplementationTypes = 
					{
						Globals.TypeOfIPropertyChange
					}
				};
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002B072 File Offset: 0x00029272
		private CodeMemberProperty CreateProperty(CodeTypeReference type, string propertyName, string fieldName, bool isValueType)
		{
			return this.CreateProperty(type, propertyName, fieldName, isValueType, this.EnableDataBinding && this.SupportsDeclareEvents);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002B090 File Offset: 0x00029290
		private CodeMemberProperty CreateProperty(CodeTypeReference type, string propertyName, string fieldName, bool isValueType, bool raisePropertyChanged)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = type;
			codeMemberProperty.Name = propertyName;
			codeMemberProperty.Attributes = MemberAttributes.Final;
			if (this.GenerateInternalTypes)
			{
				codeMemberProperty.Attributes |= MemberAttributes.Assembly;
			}
			else
			{
				codeMemberProperty.Attributes |= MemberAttributes.Public;
			}
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(this.ThisReference, fieldName);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodeFieldReferenceExpression(this.ThisReference, fieldName);
			codeAssignStatement.Right = new CodePropertySetValueReferenceExpression();
			if (raisePropertyChanged)
			{
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				CodeExpression codeExpression = new CodeFieldReferenceExpression(this.ThisReference, fieldName);
				CodeExpression codeExpression2 = new CodePropertySetValueReferenceExpression();
				if (!isValueType)
				{
					codeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(Globals.TypeOfObject), "ReferenceEquals", new CodeExpression[]
					{
						codeExpression,
						codeExpression2
					});
				}
				else
				{
					codeExpression = new CodeMethodInvokeExpression(codeExpression, "Equals", new CodeExpression[]
					{
						codeExpression2
					});
				}
				codeExpression2 = new CodePrimitiveExpression(true);
				codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.IdentityInequality, codeExpression2);
				codeConditionStatement.TrueStatements.Add(codeAssignStatement);
				codeConditionStatement.TrueStatements.Add(new CodeMethodInvokeExpression(this.ThisReference, this.RaisePropertyChangedEventMethod.Name, new CodeExpression[]
				{
					new CodePrimitiveExpression(propertyName)
				}));
				codeMemberProperty.SetStatements.Add(codeConditionStatement);
			}
			else
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
			}
			return codeMemberProperty;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002B20D File Offset: 0x0002940D
		// Note: this type is marked as 'beforefieldinit'.
		static CodeExporter()
		{
		}

		// Token: 0x0400041B RID: 1051
		private DataContractSet dataContractSet;

		// Token: 0x0400041C RID: 1052
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x0400041D RID: 1053
		private ImportOptions options;

		// Token: 0x0400041E RID: 1054
		private Dictionary<string, string> namespaces;

		// Token: 0x0400041F RID: 1055
		private Dictionary<string, string> clrNamespaces;

		// Token: 0x04000420 RID: 1056
		private static readonly string wildcardNamespaceMapping = "*";

		// Token: 0x04000421 RID: 1057
		private static readonly string typeNameFieldName = "typeName";

		// Token: 0x04000422 RID: 1058
		private static readonly object codeUserDataActualTypeKey = new object();

		// Token: 0x04000423 RID: 1059
		private static readonly object surrogateDataKey = typeof(IDataContractSurrogate);

		// Token: 0x04000424 RID: 1060
		private const int MaxIdentifierLength = 511;
	}
}
