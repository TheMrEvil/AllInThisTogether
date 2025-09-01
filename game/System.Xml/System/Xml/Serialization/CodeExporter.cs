using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.CSharp;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Represents a class that can generate proxy code from an XML representation of a data structure.</summary>
	// Token: 0x02000266 RID: 614
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeExporter
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x00088860 File Offset: 0x00086A60
		internal CodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable exportedMappings)
		{
			this.includeMetadata = new CodeAttributeDeclarationCollection();
			base..ctor();
			if (codeNamespace != null)
			{
				CodeGenerator.ValidateIdentifiers(codeNamespace);
			}
			this.codeNamespace = codeNamespace;
			if (codeCompileUnit != null)
			{
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.dll");
				}
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.Xml.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
				}
			}
			this.codeCompileUnit = codeCompileUnit;
			this.options = options;
			this.exportedMappings = exportedMappings;
			this.codeProvider = codeProvider;
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000888F5 File Offset: 0x00086AF5
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.codeCompileUnit;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x000888FD File Offset: 0x00086AFD
		internal CodeNamespace CodeNamespace
		{
			get
			{
				if (this.codeNamespace == null)
				{
					this.codeNamespace = new CodeNamespace();
				}
				return this.codeNamespace;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00088918 File Offset: 0x00086B18
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00088933 File Offset: 0x00086B33
		internal Hashtable ExportedClasses
		{
			get
			{
				if (this.exportedClasses == null)
				{
					this.exportedClasses = new Hashtable();
				}
				return this.exportedClasses;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0008894E File Offset: 0x00086B4E
		internal Hashtable ExportedMappings
		{
			get
			{
				if (this.exportedMappings == null)
				{
					this.exportedMappings = new Hashtable();
				}
				return this.exportedMappings;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00088969 File Offset: 0x00086B69
		internal bool GenerateProperties
		{
			get
			{
				return (this.options & CodeGenerationOptions.GenerateProperties) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00088978 File Offset: 0x00086B78
		internal CodeAttributeDeclaration GeneratedCodeAttribute
		{
			get
			{
				if (this.generatedCodeAttribute == null)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetExecutingAssembly();
						if (assembly == null)
						{
							assembly = typeof(CodeExporter).Assembly;
						}
					}
					AssemblyName name = assembly.GetName();
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
					string productVersion = CodeExporter.GetProductVersion(assembly);
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((productVersion == null) ? name.Version.ToString() : productVersion)));
					this.generatedCodeAttribute = codeAttributeDeclaration;
				}
				return this.generatedCodeAttribute;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00088A34 File Offset: 0x00086C34
		internal static CodeAttributeDeclaration FindAttributeDeclaration(Type type, CodeAttributeDeclarationCollection metadata)
		{
			foreach (object obj in metadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name == type.FullName || codeAttributeDeclaration.Name == type.Name)
				{
					return codeAttributeDeclaration;
				}
			}
			return null;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00088AB0 File Offset: 0x00086CB0
		private static string GetProductVersion(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is AssemblyInformationalVersionAttribute)
				{
					return ((AssemblyInformationalVersionAttribute)customAttributes[i]).InformationalVersion;
				}
			}
			return null;
		}

		/// <summary>Gets a collection of code attribute metadata that is included when the code is exported.</summary>
		/// <returns>A collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects that represent metadata that is included when the code is exported.</returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00088AEC File Offset: 0x00086CEC
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				return this.includeMetadata;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00088AF4 File Offset: 0x00086CF4
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00088AFC File Offset: 0x00086CFC
		internal void CheckScope(TypeScope scope)
		{
			if (this.scope == null)
			{
				this.scope = scope;
				return;
			}
			if (this.scope != scope)
			{
				throw new InvalidOperationException(Res.GetString("Exported mappings must come from the same importer."));
			}
		}

		// Token: 0x06001712 RID: 5906
		internal abstract void ExportDerivedStructs(StructMapping mapping);

		// Token: 0x06001713 RID: 5907
		internal abstract void EnsureTypesExported(Accessor[] accessors, string ns);

		// Token: 0x06001714 RID: 5908 RVA: 0x00088B27 File Offset: 0x00086D27
		internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
		{
			comments.Add(new CodeCommentStatement(Res.GetString("CODEGEN Warning: {0}", new object[]
			{
				text
			}), false));
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00088B4C File Offset: 0x00086D4C
		internal void ExportRoot(StructMapping mapping, Type includeType)
		{
			if (!this.rootExported)
			{
				this.rootExported = true;
				this.ExportDerivedStructs(mapping);
				for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
				{
					if (!structMapping.ReferencedByElement && structMapping.IncludeInSchema && !structMapping.IsAnonymousType)
					{
						CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(includeType.FullName);
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName)));
						this.includeMetadata.Add(codeAttributeDeclaration);
					}
				}
				Hashtable hashtable = new Hashtable();
				foreach (object obj in this.Scope.TypeMappings)
				{
					TypeMapping typeMapping = (TypeMapping)obj;
					if (typeMapping is ArrayMapping)
					{
						ArrayMapping arrayMapping = (ArrayMapping)typeMapping;
						if (CodeExporter.ShouldInclude(arrayMapping) && !hashtable.Contains(arrayMapping.TypeDesc.FullName))
						{
							CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(includeType.FullName);
							codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(arrayMapping.TypeDesc.FullName)));
							this.includeMetadata.Add(codeAttributeDeclaration2);
							hashtable.Add(arrayMapping.TypeDesc.FullName, string.Empty);
							Accessor[] elements = arrayMapping.Elements;
							this.EnsureTypesExported(elements, arrayMapping.Namespace);
						}
					}
				}
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00088CD4 File Offset: 0x00086ED4
		private static bool ShouldInclude(ArrayMapping arrayMapping)
		{
			if (arrayMapping.ReferencedByElement)
			{
				return false;
			}
			if (arrayMapping.Next != null)
			{
				return false;
			}
			if (arrayMapping.Elements.Length == 1 && arrayMapping.Elements[0].Mapping.TypeDesc.Kind == TypeKind.Node)
			{
				return false;
			}
			for (int i = 0; i < arrayMapping.Elements.Length; i++)
			{
				if (arrayMapping.Elements[i].Name != arrayMapping.Elements[i].Mapping.DefaultElementName)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00088D5C File Offset: 0x00086F5C
		internal CodeTypeDeclaration ExportEnum(EnumMapping mapping, Type type)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(mapping.TypeDesc.Name);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			codeTypeDeclaration.IsEnum = true;
			if (mapping.IsFlags && mapping.Constants.Length > 31)
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(long)));
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			this.CodeNamespace.Types.Add(codeTypeDeclaration);
			for (int i = 0; i < mapping.Constants.Length; i++)
			{
				CodeExporter.ExportConstant(codeTypeDeclaration, mapping.Constants[i], type, mapping.IsFlags, 1L << i);
			}
			if (mapping.IsFlags)
			{
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(FlagsAttribute).FullName);
				codeTypeDeclaration.CustomAttributes.Add(value);
			}
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00088E4C File Offset: 0x0008704C
		internal void AddTypeMetadata(CodeAttributeDeclarationCollection metadata, Type type, string defaultName, string name, string ns, bool includeInSchema)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
			if (name == null || name.Length == 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("AnonymousType", new CodePrimitiveExpression(true)));
			}
			else if (defaultName != name)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("TypeName", new CodePrimitiveExpression(name)));
			}
			if (ns != null && ns.Length != 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (!includeInSchema)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IncludeInSchema", new CodePrimitiveExpression(false)));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				metadata.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00088F24 File Offset: 0x00087124
		internal static void AddIncludeMetadata(CodeAttributeDeclarationCollection metadata, StructMapping mapping, Type type)
		{
			if (mapping.IsAnonymousType)
			{
				return;
			}
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				metadata.Add(new CodeAttributeDeclaration(type.FullName)
				{
					Arguments = 
					{
						new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName))
					}
				});
				CodeExporter.AddIncludeMetadata(metadata, structMapping, type);
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00088F8C File Offset: 0x0008718C
		internal static void ExportConstant(CodeTypeDeclaration codeClass, ConstantMapping constant, Type type, bool init, long enumValue)
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(int).FullName, constant.Name);
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			if (init)
			{
				codeMemberField.InitExpression = new CodePrimitiveExpression(enumValue);
			}
			codeClass.Members.Add(codeMemberField);
			if (constant.XmlName != constant.Name)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(constant.XmlName)));
				codeMemberField.CustomAttributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0008903C File Offset: 0x0008723C
		internal static object PromoteType(Type type, object value)
		{
			if (type == typeof(sbyte))
			{
				return ((IConvertible)value).ToInt16(null);
			}
			if (type == typeof(ushort))
			{
				return ((IConvertible)value).ToInt32(null);
			}
			if (type == typeof(uint))
			{
				return ((IConvertible)value).ToInt64(null);
			}
			if (type == typeof(ulong))
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return value;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000890DC File Offset: 0x000872DC
		internal CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeName);
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = ((codeMemberProperty.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			CodeExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			CodeExpression right = new CodePropertySetValueReferenceExpression();
			codeAssignStatement.Left = left;
			codeAssignStatement.Right = right;
			if (this.EnableDataBinding)
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
				codeMemberProperty.SetStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), CodeExporter.RaisePropertyChangedEventMethod.Name, new CodeExpression[]
				{
					new CodePrimitiveExpression(name)
				}));
			}
			else
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
			}
			return codeMemberProperty;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000891C4 File Offset: 0x000873C4
		internal static string MakeFieldName(string name)
		{
			return CodeIdentifier.MakeCamel(name) + "Field";
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000891D8 File Offset: 0x000873D8
		internal void AddPropertyChangedNotifier(CodeTypeDeclaration codeClass)
		{
			if (this.EnableDataBinding && codeClass != null)
			{
				if (codeClass.BaseTypes.Count == 0)
				{
					codeClass.BaseTypes.Add(typeof(object));
				}
				codeClass.BaseTypes.Add(new CodeTypeReference(typeof(INotifyPropertyChanged)));
				codeClass.Members.Add(CodeExporter.PropertyChangedEvent);
				codeClass.Members.Add(CodeExporter.RaisePropertyChangedEventMethod);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0008924F File Offset: 0x0008744F
		private bool EnableDataBinding
		{
			get
			{
				return (this.options & CodeGenerationOptions.EnableDataBinding) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00089260 File Offset: 0x00087460
		internal static CodeMemberMethod RaisePropertyChangedEventMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "RaisePropertyChanged";
				codeMemberMethod.Attributes = (MemberAttributes)12290;
				CodeArgumentReferenceExpression codeArgumentReferenceExpression = new CodeArgumentReferenceExpression("propertyName");
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), codeArgumentReferenceExpression.ParameterName));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("propertyChanged");
				codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(PropertyChangedEventHandler), codeVariableReferenceExpression.VariableName, new CodeEventReferenceExpression(new CodeThisReferenceExpression(), CodeExporter.PropertyChangedEvent.Name)));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), Array.Empty<CodeStatement>());
				codeMemberMethod.Statements.Add(codeConditionStatement);
				codeConditionStatement.TrueStatements.Add(new CodeDelegateInvokeExpression(codeVariableReferenceExpression, new CodeExpression[]
				{
					new CodeThisReferenceExpression(),
					new CodeObjectCreateExpression(typeof(PropertyChangedEventArgs), new CodeExpression[]
					{
						codeArgumentReferenceExpression
					})
				}));
				return codeMemberMethod;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00089358 File Offset: 0x00087558
		internal static CodeMemberEvent PropertyChangedEvent
		{
			get
			{
				return new CodeMemberEvent
				{
					Attributes = MemberAttributes.Public,
					Name = "PropertyChanged",
					Type = new CodeTypeReference(typeof(PropertyChangedEventHandler)),
					ImplementationTypes = 
					{
						typeof(INotifyPropertyChanged)
					}
				};
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00067344 File Offset: 0x00065544
		internal CodeExporter()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001838 RID: 6200
		private Hashtable exportedMappings;

		// Token: 0x04001839 RID: 6201
		private Hashtable exportedClasses;

		// Token: 0x0400183A RID: 6202
		private CodeNamespace codeNamespace;

		// Token: 0x0400183B RID: 6203
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x0400183C RID: 6204
		private bool rootExported;

		// Token: 0x0400183D RID: 6205
		private TypeScope scope;

		// Token: 0x0400183E RID: 6206
		private CodeAttributeDeclarationCollection includeMetadata;

		// Token: 0x0400183F RID: 6207
		private CodeGenerationOptions options;

		// Token: 0x04001840 RID: 6208
		private CodeDomProvider codeProvider;

		// Token: 0x04001841 RID: 6209
		private CodeAttributeDeclaration generatedCodeAttribute;
	}
}
