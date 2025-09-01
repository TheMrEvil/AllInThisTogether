using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000179 RID: 377
	public abstract class MetadataImporter
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x0004A8A8 File Offset: 0x00048AA8
		protected MetadataImporter(ModuleContainer module)
		{
			this.module = module;
			this.import_cache = new Dictionary<Type, TypeSpec>(1024, ReferenceEquality<Type>.Default);
			this.compiled_types = new Dictionary<Type, TypeSpec>(40, ReferenceEquality<Type>.Default);
			this.assembly_2_definition = new Dictionary<Assembly, IAssemblyDefinition>(ReferenceEquality<Assembly>.Default);
			this.IgnorePrivateMembers = true;
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0004A900 File Offset: 0x00048B00
		public ICollection<IAssemblyDefinition> Assemblies
		{
			get
			{
				return this.assembly_2_definition.Values;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004A90D File Offset: 0x00048B0D
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0004A915 File Offset: 0x00048B15
		public bool IgnorePrivateMembers
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnorePrivateMembers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnorePrivateMembers>k__BackingField = value;
			}
		}

		// Token: 0x060011FC RID: 4604
		public abstract void AddCompiledType(TypeBuilder builder, TypeSpec spec);

		// Token: 0x060011FD RID: 4605
		protected abstract MemberKind DetermineKindFromBaseType(Type baseType);

		// Token: 0x060011FE RID: 4606
		protected abstract bool HasVolatileModifier(Type[] modifiers);

		// Token: 0x060011FF RID: 4607 RVA: 0x0004A920 File Offset: 0x00048B20
		public FieldSpec CreateField(FieldInfo fi, TypeSpec declaringType)
		{
			FieldAttributes attributes = fi.Attributes;
			Modifiers modifiers;
			switch (attributes & FieldAttributes.FieldAccessMask)
			{
			case FieldAttributes.Assembly:
				modifiers = Modifiers.INTERNAL;
				break;
			case FieldAttributes.Family:
				modifiers = Modifiers.PROTECTED;
				break;
			case FieldAttributes.FamORAssem:
				modifiers = (Modifiers.PROTECTED | Modifiers.INTERNAL);
				break;
			case FieldAttributes.Public:
				modifiers = Modifiers.PUBLIC;
				break;
			default:
				if ((this.IgnorePrivateMembers && !declaringType.IsStruct) || MetadataImporter.HasAttribute(CustomAttributeData.GetCustomAttributes(fi), "CompilerGeneratedAttribute", MetadataImporter.CompilerServicesNamespace))
				{
					return null;
				}
				modifiers = Modifiers.PRIVATE;
				break;
			}
			TypeSpec typeSpec;
			try
			{
				typeSpec = this.ImportType(fi.FieldType, new MetadataImporter.DynamicTypeReader(fi));
				if (typeSpec == null)
				{
					return null;
				}
			}
			catch (Exception exception)
			{
				throw new InternalErrorException(exception, "Cannot import field `{0}.{1}' referenced in assembly `{2}'", new object[]
				{
					declaringType.GetSignatureForError(),
					fi.Name,
					declaringType.MemberDefinition.DeclaringAssembly
				});
			}
			ImportedMemberDefinition definition = new ImportedMemberDefinition(fi, typeSpec, this);
			if ((attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope)
			{
				Constant value = (typeSpec.Kind == MemberKind.MissingType) ? new NullConstant(InternalType.ErrorType, Location.Null) : this.CreateConstantFromValue(typeSpec, fi);
				return new ConstSpec(declaringType, definition, typeSpec, fi, modifiers | Modifiers.STATIC, value);
			}
			if ((attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope)
			{
				if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Decimal)
				{
					Constant constant = this.ReadDecimalConstant(CustomAttributeData.GetCustomAttributes(fi));
					if (constant != null)
					{
						return new ConstSpec(declaringType, definition, typeSpec, fi, modifiers | Modifiers.STATIC, constant);
					}
				}
				modifiers |= Modifiers.READONLY;
			}
			else
			{
				Type[] requiredCustomModifiers = fi.GetRequiredCustomModifiers();
				if (requiredCustomModifiers.Length != 0 && this.HasVolatileModifier(requiredCustomModifiers))
				{
					modifiers |= Modifiers.VOLATILE;
				}
			}
			if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
			{
				modifiers |= Modifiers.STATIC;
			}
			else if (declaringType.IsStruct && typeSpec.IsStruct && typeSpec.IsNested && MetadataImporter.HasAttribute(CustomAttributeData.GetCustomAttributes(fi), "FixedBufferAttribute", MetadataImporter.CompilerServicesNamespace))
			{
				FieldSpec element = this.CreateField(fi.FieldType.GetField("FixedElementField"), declaringType);
				return new FixedFieldSpec(this.module, declaringType, definition, fi, element, modifiers);
			}
			return new FieldSpec(declaringType, definition, typeSpec, fi, modifiers);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0004AB14 File Offset: 0x00048D14
		private Constant CreateConstantFromValue(TypeSpec fieldType, FieldInfo fi)
		{
			object rawConstantValue = fi.GetRawConstantValue();
			if (rawConstantValue != null && !fieldType.IsEnum)
			{
				Constant constant = this.ImportConstant(rawConstantValue);
				if (constant != null)
				{
					if (fieldType != constant.Type)
					{
						return constant.ConvertExplicitly(false, fieldType);
					}
					return constant;
				}
			}
			return Constant.CreateConstantFromValue(fieldType, rawConstantValue, Location.Null);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0004AB60 File Offset: 0x00048D60
		public EventSpec CreateEvent(EventInfo ei, TypeSpec declaringType, MethodSpec add, MethodSpec remove)
		{
			add.IsAccessor = true;
			remove.IsAccessor = true;
			if (add.Modifiers != remove.Modifiers)
			{
				throw new NotImplementedException("Different accessor modifiers " + ei.Name);
			}
			TypeSpec typeSpec = this.ImportType(ei.EventHandlerType, new MetadataImporter.DynamicTypeReader(ei));
			ImportedMemberDefinition definition = new ImportedMemberDefinition(ei, typeSpec, this);
			return new EventSpec(declaringType, definition, typeSpec, add.Modifiers, add, remove);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004ABD0 File Offset: 0x00048DD0
		private TypeParameterSpec[] CreateGenericParameters(Type type, TypeSpec declaringType)
		{
			Type[] genericArguments = type.GetGenericArguments();
			int num;
			if (type.IsNested)
			{
				num = type.DeclaringType.GetGenericArguments().Length;
				if (declaringType != null && num > 0)
				{
					int num2 = 0;
					while (num2 != num)
					{
						int arity = declaringType.Arity;
						if (arity != 0)
						{
							TypeParameterSpec[] typeParameters = declaringType.MemberDefinition.TypeParameters;
							num2 += arity;
							for (int i = 0; i < arity; i++)
							{
								this.import_cache.Add(genericArguments[num - num2 + i], typeParameters[i]);
							}
						}
						declaringType = declaringType.DeclaringType;
					}
				}
			}
			else
			{
				num = 0;
			}
			if (genericArguments.Length - num == 0)
			{
				return null;
			}
			return this.CreateGenericParameters(num, genericArguments);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0004AC6C File Offset: 0x00048E6C
		private TypeParameterSpec[] CreateGenericParameters(int first, Type[] tparams)
		{
			TypeParameterSpec[] array = new TypeParameterSpec[tparams.Length - first];
			for (int i = first; i < tparams.Length; i++)
			{
				Type type = tparams[i];
				int num = i - first;
				array[num] = (TypeParameterSpec)this.CreateType(type, default(MetadataImporter.DynamicTypeReader), false);
			}
			return array;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0004ACB8 File Offset: 0x00048EB8
		private TypeSpec[] CreateGenericArguments(int first, Type[] tparams, MetadataImporter.DynamicTypeReader dtype)
		{
			dtype.Position++;
			TypeSpec[] array = new TypeSpec[tparams.Length - first];
			for (int i = first; i < tparams.Length; i++)
			{
				Type type = tparams[i];
				int num = i - first;
				TypeSpec typeSpec;
				if (type.HasElementType)
				{
					Type elementType = type.GetElementType();
					dtype.Position++;
					typeSpec = this.ImportType(elementType, dtype);
					if (!type.IsArray)
					{
						throw new NotImplementedException("Unknown element type " + type.ToString());
					}
					typeSpec = ArrayContainer.MakeType(this.module, typeSpec, type.GetArrayRank());
				}
				else
				{
					typeSpec = this.CreateType(type, dtype, true);
					if (!MetadataImporter.IsMissingType(type) && type.IsGenericTypeDefinition)
					{
						int first2 = (typeSpec.DeclaringType == null) ? 0 : typeSpec.DeclaringType.MemberDefinition.TypeParametersCount;
						TypeSpec[] targs = this.CreateGenericArguments(first2, type.GetGenericArguments(), dtype);
						typeSpec = typeSpec.MakeGenericType(this.module, targs);
					}
				}
				if (typeSpec == null)
				{
					return null;
				}
				dtype.Position++;
				array[num] = typeSpec;
			}
			return array;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004ADC8 File Offset: 0x00048FC8
		public MethodSpec CreateMethod(MethodBase mb, TypeSpec declaringType)
		{
			Modifiers modifiers = MetadataImporter.ReadMethodModifiers(mb, declaringType);
			AParametersCollection aparametersCollection = this.CreateParameters(declaringType, mb.GetParameters(), mb);
			TypeParameterSpec[] array;
			if (mb.IsGenericMethod)
			{
				if (!mb.IsGenericMethodDefinition)
				{
					throw new NotSupportedException("assert");
				}
				array = this.CreateGenericParameters(0, mb.GetGenericArguments());
			}
			else
			{
				array = null;
			}
			MemberKind memberKind;
			TypeSpec typeSpec;
			if (mb.MemberType == MemberTypes.Constructor)
			{
				memberKind = MemberKind.Constructor;
				typeSpec = this.module.Compiler.BuiltinTypes.Void;
			}
			else
			{
				string name = mb.Name;
				memberKind = MemberKind.Method;
				if (array == null && !mb.DeclaringType.IsInterface && name.Length > 6)
				{
					if ((modifiers & (Modifiers.PUBLIC | Modifiers.STATIC)) == (Modifiers.PUBLIC | Modifiers.STATIC))
					{
						if (name[2] == '_' && name[1] == 'p' && name[0] == 'o' && (mb.Attributes & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope && Operator.GetType(name) != null && aparametersCollection.Count > 0 && aparametersCollection.Count < 3)
						{
							memberKind = MemberKind.Operator;
						}
					}
					else if (aparametersCollection.IsEmpty && name == Destructor.MetadataName)
					{
						memberKind = MemberKind.Destructor;
						if (declaringType.BuiltinType == BuiltinTypeSpec.Type.Object)
						{
							modifiers &= ~Modifiers.OVERRIDE;
							modifiers |= Modifiers.VIRTUAL;
						}
					}
				}
				MethodInfo methodInfo = (MethodInfo)mb;
				typeSpec = this.ImportType(methodInfo.ReturnType, new MetadataImporter.DynamicTypeReader(methodInfo.ReturnParameter));
				if ((modifiers & Modifiers.OVERRIDE) != (Modifiers)0)
				{
					bool flag = false;
					if (memberKind == MemberKind.Method && declaringType.BaseType != null)
					{
						TypeSpec baseType = declaringType.BaseType;
						if (this.IsOverrideMethodBaseTypeAccessible(baseType))
						{
							MemberFilter filter = MemberFilter.Method(name, (array != null) ? array.Length : 0, aparametersCollection, null);
							MemberSpec memberSpec = MemberCache.FindMember(baseType, filter, BindingRestriction.None);
							if (memberSpec != null && (memberSpec.Modifiers & (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE)) == (modifiers & (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE)) && !memberSpec.IsStatic)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						modifiers &= ~Modifiers.OVERRIDE;
						if ((modifiers & Modifiers.SEALED) != (Modifiers)0)
						{
							modifiers &= ~Modifiers.SEALED;
						}
						else
						{
							modifiers |= Modifiers.VIRTUAL;
						}
					}
				}
				else if (aparametersCollection.HasExtensionMethodType)
				{
					modifiers |= Modifiers.METHOD_EXTENSION;
				}
			}
			IMethodDefinition details;
			if (array != null)
			{
				ImportedGenericMethodDefinition importedGenericMethodDefinition = new ImportedGenericMethodDefinition((MethodInfo)mb, typeSpec, aparametersCollection, array, this);
				foreach (TypeParameterSpec typeParameterSpec in importedGenericMethodDefinition.TypeParameters)
				{
					TypeParameterSpec typeParameterSpec2 = typeParameterSpec;
					this.ImportTypeParameterTypeConstraints(typeParameterSpec2, typeParameterSpec2.GetMetaInfo());
				}
				details = importedGenericMethodDefinition;
			}
			else
			{
				details = new ImportedMethodDefinition(mb, typeSpec, aparametersCollection, this);
			}
			MethodSpec methodSpec = new MethodSpec(memberKind, declaringType, details, typeSpec, aparametersCollection, modifiers);
			if (array != null)
			{
				methodSpec.IsGeneric = true;
			}
			return methodSpec;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004B044 File Offset: 0x00049244
		private bool IsOverrideMethodBaseTypeAccessible(TypeSpec baseType)
		{
			Modifiers modifiers = baseType.Modifiers & Modifiers.AccessibilityMask;
			return modifiers == Modifiers.PUBLIC || (modifiers != Modifiers.PRIVATE && (modifiers != Modifiers.INTERNAL || baseType.MemberDefinition.IsInternalAsPublic(this.module.DeclaringAssembly)));
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004B088 File Offset: 0x00049288
		private AParametersCollection CreateParameters(TypeSpec parent, ParameterInfo[] pi, MethodBase method)
		{
			int num = (method != null && (method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0) ? 1 : 0;
			if (pi.Length == 0 && num == 0)
			{
				return ParametersCompiled.EmptyReadOnlyParameters;
			}
			TypeSpec[] array = new TypeSpec[pi.Length + num];
			IParameterData[] array2 = new IParameterData[pi.Length + num];
			bool flag = false;
			for (int i = 0; i < pi.Length; i++)
			{
				ParameterInfo parameterInfo = pi[i];
				Parameter.Modifier modifier = Parameter.Modifier.NONE;
				Expression expression = null;
				if (parameterInfo.ParameterType.IsByRef)
				{
					if ((parameterInfo.Attributes & (ParameterAttributes.In | ParameterAttributes.Out)) == ParameterAttributes.Out)
					{
						modifier = Parameter.Modifier.OUT;
					}
					else
					{
						modifier = Parameter.Modifier.REF;
					}
					Type elementType = parameterInfo.ParameterType.GetElementType();
					array[i] = this.ImportType(elementType, new MetadataImporter.DynamicTypeReader(parameterInfo));
				}
				else if (i == 0 && method.IsStatic && (parent.Modifiers & Modifiers.METHOD_EXTENSION) != (Modifiers)0 && MetadataImporter.HasAttribute(CustomAttributeData.GetCustomAttributes(method), "ExtensionAttribute", MetadataImporter.CompilerServicesNamespace))
				{
					modifier = Parameter.Modifier.This;
					array[i] = this.ImportType(parameterInfo.ParameterType, new MetadataImporter.DynamicTypeReader(parameterInfo));
				}
				else
				{
					array[i] = this.ImportType(parameterInfo.ParameterType, new MetadataImporter.DynamicTypeReader(parameterInfo));
					if (i >= pi.Length - 2 && array[i] is ArrayContainer && MetadataImporter.HasAttribute(CustomAttributeData.GetCustomAttributes(parameterInfo), "ParamArrayAttribute", "System"))
					{
						modifier = Parameter.Modifier.PARAMS;
						flag = true;
					}
					if (!flag && parameterInfo.IsOptional)
					{
						object rawDefaultValue = parameterInfo.RawDefaultValue;
						TypeSpec typeSpec = array[i];
						if ((parameterInfo.Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.None && typeSpec.Kind != MemberKind.TypeParameter && (rawDefaultValue != null || TypeSpec.IsReferenceType(typeSpec)))
						{
							if (rawDefaultValue == null)
							{
								expression = Constant.CreateConstantFromValue(typeSpec, null, Location.Null);
							}
							else
							{
								expression = this.ImportConstant(rawDefaultValue);
								if (typeSpec.IsEnum)
								{
									expression = new EnumConstant((Constant)expression, typeSpec);
								}
							}
							IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(parameterInfo);
							for (int j = 0; j < customAttributes.Count; j++)
							{
								Type declaringType = customAttributes[j].Constructor.DeclaringType;
								if (!(declaringType.Namespace != MetadataImporter.CompilerServicesNamespace))
								{
									if (declaringType.Name == "CallerLineNumberAttribute" && (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Int || Convert.ImplicitNumericConversionExists(this.module.Compiler.BuiltinTypes.Int, typeSpec)))
									{
										modifier |= Parameter.Modifier.CallerLineNumber;
									}
									else if (declaringType.Name == "CallerFilePathAttribute" && Convert.ImplicitReferenceConversionExists(this.module.Compiler.BuiltinTypes.String, typeSpec))
									{
										modifier |= Parameter.Modifier.CallerFilePath;
									}
									else if (declaringType.Name == "CallerMemberNameAttribute" && Convert.ImplicitReferenceConversionExists(this.module.Compiler.BuiltinTypes.String, typeSpec))
									{
										modifier |= Parameter.Modifier.CallerMemberName;
									}
								}
							}
						}
						else if (rawDefaultValue == Missing.Value)
						{
							expression = EmptyExpression.MissingValue;
						}
						else if (rawDefaultValue == null)
						{
							expression = new DefaultValueExpression(new TypeExpression(typeSpec, Location.Null), Location.Null);
						}
						else if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Decimal)
						{
							expression = this.ImportConstant(rawDefaultValue);
						}
					}
				}
				array2[i] = new ParameterData(parameterInfo.Name, modifier, expression);
			}
			if (num != 0)
			{
				IParameterData[] array3 = array2;
				array3[array3.Length - 1] = new ArglistParameter(Location.Null);
				TypeSpec[] array4 = array;
				array4[array4.Length - 1] = InternalType.Arglist;
			}
			if (method == null)
			{
				return new ParametersImported(array2, array, flag);
			}
			return new ParametersImported(array2, array, num != 0, flag);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004B3F8 File Offset: 0x000495F8
		public PropertySpec CreateProperty(PropertyInfo pi, TypeSpec declaringType, MethodSpec get, MethodSpec set)
		{
			Modifiers modifiers = (Modifiers)0;
			AParametersCollection aparametersCollection = null;
			TypeSpec typeSpec = null;
			if (get != null)
			{
				modifiers = get.Modifiers;
				aparametersCollection = get.Parameters;
				typeSpec = get.ReturnType;
			}
			bool flag = true;
			if (set != null)
			{
				if (set.ReturnType.Kind != MemberKind.Void)
				{
					flag = false;
				}
				int num = set.Parameters.Count - 1;
				if (num < 0)
				{
					num = 0;
					flag = false;
				}
				TypeSpec typeSpec2 = set.Parameters.Types[num];
				if (modifiers == (Modifiers)0)
				{
					AParametersCollection aparametersCollection2;
					if (num == 0)
					{
						aparametersCollection2 = ParametersCompiled.EmptyReadOnlyParameters;
					}
					else
					{
						IParameterData[] array = new IParameterData[num];
						TypeSpec[] array2 = new TypeSpec[num];
						Array.Copy(set.Parameters.FixedParameters, array, num);
						Array.Copy(set.Parameters.Types, array2, num);
						aparametersCollection2 = new ParametersImported(array, array2, set.Parameters.HasParams);
					}
					modifiers = set.Modifiers;
					aparametersCollection = aparametersCollection2;
					typeSpec = typeSpec2;
				}
				else
				{
					if (num != get.Parameters.Count)
					{
						flag = false;
					}
					if (get.ReturnType != typeSpec2)
					{
						flag = false;
					}
					if ((modifiers & Modifiers.AccessibilityMask) != (set.Modifiers & Modifiers.AccessibilityMask))
					{
						Modifiers modifiers2 = modifiers & Modifiers.AccessibilityMask;
						if (modifiers2 != Modifiers.PUBLIC)
						{
							Modifiers modifiers3 = set.Modifiers & Modifiers.AccessibilityMask;
							if (modifiers2 != modifiers3)
							{
								bool flag2 = ModifiersExtensions.IsRestrictedModifier(modifiers2, modifiers3);
								bool flag3 = ModifiersExtensions.IsRestrictedModifier(modifiers3, modifiers2);
								if (flag2 && flag3)
								{
									flag = false;
								}
								if (flag2)
								{
									modifiers &= ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL);
									modifiers |= modifiers3;
								}
							}
						}
					}
				}
			}
			PropertySpec propertySpec = null;
			if (!aparametersCollection.IsEmpty && flag)
			{
				string attributeDefaultMember = declaringType.MemberDefinition.GetAttributeDefaultMember();
				if (attributeDefaultMember == null)
				{
					flag = false;
				}
				else
				{
					if (get != null)
					{
						if (get.IsStatic)
						{
							flag = false;
						}
						if (get.Name.IndexOf(attributeDefaultMember, StringComparison.Ordinal) != 4)
						{
							flag = false;
						}
					}
					if (set != null)
					{
						if (set.IsStatic)
						{
							flag = false;
						}
						if (set.Name.IndexOf(attributeDefaultMember, StringComparison.Ordinal) != 4)
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					propertySpec = new IndexerSpec(declaringType, new ImportedParameterMemberDefinition(pi, typeSpec, aparametersCollection, this), typeSpec, aparametersCollection, pi, modifiers);
				}
				else if (declaringType.MemberDefinition.IsComImport && aparametersCollection.FixedParameters[0].HasDefaultValue)
				{
					flag = true;
					for (int i = 0; i < aparametersCollection.FixedParameters.Length; i++)
					{
						if (!aparametersCollection.FixedParameters[i].HasDefaultValue)
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (propertySpec == null)
			{
				propertySpec = new PropertySpec(MemberKind.Property, declaringType, new ImportedMemberDefinition(pi, typeSpec, this), typeSpec, pi, modifiers);
			}
			if (!flag)
			{
				propertySpec.IsNotCSharpCompatible = true;
				return propertySpec;
			}
			if (set != null)
			{
				propertySpec.Set = set;
			}
			if (get != null)
			{
				propertySpec.Get = get;
			}
			return propertySpec;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0004B664 File Offset: 0x00049864
		public TypeSpec CreateType(Type type)
		{
			return this.CreateType(type, default(MetadataImporter.DynamicTypeReader), true);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0004B682 File Offset: 0x00049882
		public TypeSpec CreateNestedType(Type type, TypeSpec declaringType)
		{
			return this.CreateType(type, declaringType, new MetadataImporter.DynamicTypeReader(type), false);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0004B694 File Offset: 0x00049894
		private TypeSpec CreateType(Type type, MetadataImporter.DynamicTypeReader dtype, bool canImportBaseType)
		{
			TypeSpec declaringType;
			if (type.IsNested && !type.IsGenericParameter)
			{
				declaringType = this.CreateType(type.DeclaringType, new MetadataImporter.DynamicTypeReader(type.DeclaringType), true);
			}
			else
			{
				declaringType = null;
			}
			return this.CreateType(type, declaringType, dtype, canImportBaseType);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004B6D8 File Offset: 0x000498D8
		protected TypeSpec CreateType(Type type, TypeSpec declaringType, MetadataImporter.DynamicTypeReader dtype, bool canImportBaseType)
		{
			TypeSpec typeSpec;
			if (this.import_cache.TryGetValue(type, out typeSpec))
			{
				if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Object)
				{
					if (dtype.IsDynamicObject())
					{
						return this.module.Compiler.BuiltinTypes.Dynamic;
					}
					return typeSpec;
				}
				else
				{
					if (!typeSpec.IsGeneric || type.IsGenericTypeDefinition)
					{
						return typeSpec;
					}
					if (!dtype.HasDynamicAttribute())
					{
						return typeSpec;
					}
				}
			}
			if (MetadataImporter.IsMissingType(type))
			{
				typeSpec = new TypeSpec(MemberKind.MissingType, declaringType, new ImportedTypeDefinition(type, this), type, Modifiers.PUBLIC);
				typeSpec.MemberCache = MemberCache.Empty;
				this.import_cache.Add(type, typeSpec);
				return typeSpec;
			}
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (this.compiled_types.TryGetValue(genericTypeDefinition, out typeSpec))
				{
					return typeSpec;
				}
				TypeSpec[] array = this.CreateGenericArguments(0, type.GetGenericArguments(), dtype);
				if (array == null)
				{
					return null;
				}
				if (declaringType == null)
				{
					typeSpec = this.CreateType(genericTypeDefinition, null, default(MetadataImporter.DynamicTypeReader), canImportBaseType);
					typeSpec = typeSpec.MakeGenericType(this.module, array);
				}
				else
				{
					List<TypeSpec> list = new List<TypeSpec>();
					while (declaringType.IsNested)
					{
						list.Add(declaringType);
						declaringType = declaringType.DeclaringType;
					}
					int num = 0;
					if (declaringType.Arity > 0)
					{
						typeSpec = declaringType.MakeGenericType(this.module, array.Skip(num).Take(declaringType.Arity).ToArray<TypeSpec>());
						num = typeSpec.Arity;
					}
					else
					{
						typeSpec = declaringType;
					}
					for (int num2 = list.Count; num2 != 0; num2--)
					{
						TypeSpec typeSpec2 = list[num2 - 1];
						if (typeSpec2.Kind == MemberKind.MissingType)
						{
							typeSpec = typeSpec2;
						}
						else
						{
							typeSpec = MemberCache.FindNestedType(typeSpec, typeSpec2.Name, typeSpec2.Arity);
						}
						if (typeSpec2.Arity > 0)
						{
							typeSpec = typeSpec.MakeGenericType(this.module, array.Skip(num).Take(typeSpec.Arity).ToArray<TypeSpec>());
							num += typeSpec2.Arity;
						}
					}
					if (typeSpec.Kind == MemberKind.MissingType)
					{
						typeSpec = new TypeSpec(MemberKind.MissingType, typeSpec, new ImportedTypeDefinition(genericTypeDefinition, this), genericTypeDefinition, Modifiers.PUBLIC);
						typeSpec.MemberCache = MemberCache.Empty;
					}
					else
					{
						if ((genericTypeDefinition.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate && this.IgnorePrivateMembers)
						{
							return null;
						}
						string text = type.Name;
						int num3 = text.IndexOf('`');
						if (num3 > 0)
						{
							text = text.Substring(0, num3);
						}
						typeSpec = MemberCache.FindNestedType(typeSpec, text, array.Length - num);
						if (typeSpec.Arity > 0)
						{
							typeSpec = typeSpec.MakeGenericType(this.module, array.Skip(num).ToArray<TypeSpec>());
						}
					}
				}
				if (!typeSpec.HasDynamicElement && !this.import_cache.ContainsKey(type))
				{
					this.import_cache.Add(type, typeSpec);
				}
				return typeSpec;
			}
			else
			{
				TypeAttributes attributes = type.Attributes;
				Modifiers modifiers;
				switch (attributes & TypeAttributes.VisibilityMask)
				{
				case TypeAttributes.Public:
				case TypeAttributes.NestedPublic:
					modifiers = Modifiers.PUBLIC;
					goto IL_2F2;
				case TypeAttributes.NestedPrivate:
					modifiers = Modifiers.PRIVATE;
					goto IL_2F2;
				case TypeAttributes.NestedFamily:
					modifiers = Modifiers.PROTECTED;
					goto IL_2F2;
				case TypeAttributes.VisibilityMask:
					modifiers = (Modifiers.PROTECTED | Modifiers.INTERNAL);
					goto IL_2F2;
				}
				modifiers = Modifiers.INTERNAL;
				IL_2F2:
				MemberKind memberKind;
				if ((attributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					memberKind = MemberKind.Interface;
				}
				else if (type.IsGenericParameter)
				{
					memberKind = MemberKind.TypeParameter;
				}
				else
				{
					Type baseType = type.BaseType;
					if (baseType == null || (attributes & TypeAttributes.Abstract) != TypeAttributes.NotPublic)
					{
						memberKind = MemberKind.Class;
					}
					else
					{
						memberKind = this.DetermineKindFromBaseType(baseType);
						if (memberKind == MemberKind.Struct || memberKind == MemberKind.Delegate)
						{
							modifiers |= Modifiers.SEALED;
						}
					}
					if (memberKind == MemberKind.Class)
					{
						if ((attributes & TypeAttributes.Sealed) != TypeAttributes.NotPublic)
						{
							if ((attributes & TypeAttributes.Abstract) != TypeAttributes.NotPublic)
							{
								modifiers |= Modifiers.STATIC;
							}
							else
							{
								modifiers |= Modifiers.SEALED;
							}
						}
						else if ((attributes & TypeAttributes.Abstract) != TypeAttributes.NotPublic)
						{
							modifiers |= Modifiers.ABSTRACT;
						}
					}
				}
				ImportedTypeDefinition importedTypeDefinition = new ImportedTypeDefinition(type, this);
				TypeSpec typeSpec3;
				if (memberKind == MemberKind.Enum)
				{
					FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					int num4 = 0;
					if (num4 < fields.Length)
					{
						FieldInfo fieldInfo = fields[num4];
						typeSpec = new EnumSpec(declaringType, importedTypeDefinition, this.CreateType(fieldInfo.FieldType), type, modifiers);
					}
					if (typeSpec == null)
					{
						memberKind = MemberKind.Class;
					}
				}
				else if (memberKind == MemberKind.TypeParameter)
				{
					typeSpec = this.CreateTypeParameter(type, declaringType);
				}
				else if (type.IsGenericTypeDefinition)
				{
					importedTypeDefinition.TypeParameters = this.CreateGenericParameters(type, declaringType);
				}
				else if (this.compiled_types.TryGetValue(type, out typeSpec3))
				{
					typeSpec = typeSpec3;
					BuiltinTypeSpec builtinTypeSpec = typeSpec3 as BuiltinTypeSpec;
					if (builtinTypeSpec != null)
					{
						builtinTypeSpec.SetDefinition(importedTypeDefinition, type, modifiers);
					}
				}
				if (typeSpec == null)
				{
					typeSpec = new TypeSpec(memberKind, declaringType, importedTypeDefinition, type, modifiers);
				}
				this.import_cache.Add(type, typeSpec);
				if (memberKind == MemberKind.TypeParameter)
				{
					if (canImportBaseType)
					{
						this.ImportTypeParameterTypeConstraints((TypeParameterSpec)typeSpec, type);
					}
					return typeSpec;
				}
				if (canImportBaseType)
				{
					this.ImportTypeBase(typeSpec, type);
				}
				return typeSpec;
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0004BB5C File Offset: 0x00049D5C
		public IAssemblyDefinition GetAssemblyDefinition(Assembly assembly)
		{
			IAssemblyDefinition result;
			if (!this.assembly_2_definition.TryGetValue(assembly, out result))
			{
				ImportedAssemblyDefinition importedAssemblyDefinition = new ImportedAssemblyDefinition(assembly);
				this.assembly_2_definition.Add(assembly, importedAssemblyDefinition);
				importedAssemblyDefinition.ReadAttributes();
				result = importedAssemblyDefinition;
			}
			return result;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0004BB98 File Offset: 0x00049D98
		public void ImportTypeBase(Type type)
		{
			TypeSpec typeSpec = this.import_cache[type];
			if (typeSpec != null)
			{
				this.ImportTypeBase(typeSpec, type);
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004BBC0 File Offset: 0x00049DC0
		private TypeParameterSpec CreateTypeParameter(Type type, TypeSpec declaringType)
		{
			GenericParameterAttributes genericParameterAttributes = type.GenericParameterAttributes & GenericParameterAttributes.VarianceMask;
			Variance variance;
			if (genericParameterAttributes != GenericParameterAttributes.Covariant)
			{
				if (genericParameterAttributes != GenericParameterAttributes.Contravariant)
				{
					variance = Variance.None;
				}
				else
				{
					variance = Variance.Contravariant;
				}
			}
			else
			{
				variance = Variance.Covariant;
			}
			SpecialConstraint specialConstraint = SpecialConstraint.None;
			GenericParameterAttributes genericParameterAttributes2 = type.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
			if ((genericParameterAttributes2 & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
			{
				specialConstraint |= SpecialConstraint.Struct;
			}
			else if ((genericParameterAttributes2 & GenericParameterAttributes.DefaultConstructorConstraint) != GenericParameterAttributes.None)
			{
				specialConstraint = SpecialConstraint.Constructor;
			}
			if ((genericParameterAttributes2 & GenericParameterAttributes.ReferenceTypeConstraint) != GenericParameterAttributes.None)
			{
				specialConstraint |= SpecialConstraint.Class;
			}
			ImportedTypeParameterDefinition definition = new ImportedTypeParameterDefinition(type, this);
			TypeParameterSpec result;
			if (type.DeclaringMethod != null)
			{
				result = new TypeParameterSpec(type.GenericParameterPosition, definition, specialConstraint, variance, type);
			}
			else
			{
				result = new TypeParameterSpec(declaringType, type.GenericParameterPosition, definition, specialConstraint, variance, type);
			}
			return result;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0004BC50 File Offset: 0x00049E50
		public static bool HasAttribute(IList<CustomAttributeData> attributesData, string attrName, string attrNamespace)
		{
			if (attributesData.Count == 0)
			{
				return false;
			}
			foreach (CustomAttributeData customAttributeData in attributesData)
			{
				Type declaringType = customAttributeData.Constructor.DeclaringType;
				if (declaringType.Name == attrName && declaringType.Namespace == attrNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0004BCC8 File Offset: 0x00049EC8
		private void ImportTypeBase(TypeSpec spec, Type type)
		{
			if (spec.Kind == MemberKind.Interface)
			{
				spec.BaseType = this.module.Compiler.BuiltinTypes.Object;
			}
			else if (type.BaseType != null)
			{
				TypeSpec baseType;
				if (!MetadataImporter.IsMissingType(type.BaseType) && type.BaseType.IsGenericType)
				{
					baseType = this.CreateType(type.BaseType, new MetadataImporter.DynamicTypeReader(type), true);
				}
				else
				{
					baseType = this.CreateType(type.BaseType);
				}
				spec.BaseType = baseType;
			}
			if (spec.MemberDefinition.TypeParametersCount > 0)
			{
				foreach (TypeParameterSpec typeParameterSpec in spec.MemberDefinition.TypeParameters)
				{
					TypeParameterSpec typeParameterSpec2 = typeParameterSpec;
					this.ImportTypeParameterTypeConstraints(typeParameterSpec2, typeParameterSpec2.GetMetaInfo());
				}
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0004BD84 File Offset: 0x00049F84
		protected void ImportTypes(Type[] types, Namespace targetNamespace, bool importExtensionTypes)
		{
			Namespace @namespace = targetNamespace;
			string a = null;
			foreach (Type type in types)
			{
				if (type != null && type.MemberType != MemberTypes.NestedType && type.Name[0] != '<')
				{
					TypeSpec typeSpec = this.CreateType(type, null, new MetadataImporter.DynamicTypeReader(type), true);
					if (typeSpec != null)
					{
						if (a != type.Namespace)
						{
							@namespace = ((type.Namespace == null) ? targetNamespace : targetNamespace.GetNamespace(type.Namespace, true));
							a = type.Namespace;
						}
						if (typeSpec.IsClass && typeSpec.Arity == 0 && importExtensionTypes && MetadataImporter.HasAttribute(CustomAttributeData.GetCustomAttributes(type), "ExtensionAttribute", MetadataImporter.CompilerServicesNamespace))
						{
							typeSpec.SetExtensionMethodContainer();
						}
						@namespace.AddType(this.module, typeSpec);
					}
				}
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0004BE6C File Offset: 0x0004A06C
		private void ImportTypeParameterTypeConstraints(TypeParameterSpec spec, Type type)
		{
			Type[] genericParameterConstraints = type.GetGenericParameterConstraints();
			List<TypeSpec> list = null;
			foreach (Type type2 in genericParameterConstraints)
			{
				if (type2.IsGenericParameter)
				{
					if (list == null)
					{
						list = new List<TypeSpec>();
					}
					list.Add(this.CreateType(type2));
				}
				else
				{
					TypeSpec typeSpec = this.CreateType(type2);
					if (typeSpec.IsClass)
					{
						spec.BaseType = typeSpec;
					}
					else
					{
						spec.AddInterface(typeSpec);
					}
				}
			}
			if (spec.BaseType == null)
			{
				spec.BaseType = this.module.Compiler.BuiltinTypes.Object;
			}
			if (list != null)
			{
				spec.TypeArguments = list.ToArray();
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004BF0C File Offset: 0x0004A10C
		private Constant ImportConstant(object value)
		{
			BuiltinTypes builtinTypes = this.module.Compiler.BuiltinTypes;
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				return new BoolConstant(builtinTypes, (bool)value, Location.Null);
			case TypeCode.Char:
				return new CharConstant(builtinTypes, (char)value, Location.Null);
			case TypeCode.SByte:
				return new SByteConstant(builtinTypes, (sbyte)value, Location.Null);
			case TypeCode.Byte:
				return new ByteConstant(builtinTypes, (byte)value, Location.Null);
			case TypeCode.Int16:
				return new ShortConstant(builtinTypes, (short)value, Location.Null);
			case TypeCode.UInt16:
				return new UShortConstant(builtinTypes, (ushort)value, Location.Null);
			case TypeCode.Int32:
				return new IntConstant(builtinTypes, (int)value, Location.Null);
			case TypeCode.UInt32:
				return new UIntConstant(builtinTypes, (uint)value, Location.Null);
			case TypeCode.Int64:
				return new LongConstant(builtinTypes, (long)value, Location.Null);
			case TypeCode.UInt64:
				return new ULongConstant(builtinTypes, (ulong)value, Location.Null);
			case TypeCode.Single:
				return new FloatConstant(builtinTypes, (double)((float)value), Location.Null);
			case TypeCode.Double:
				return new DoubleConstant(builtinTypes, (double)value, Location.Null);
			case TypeCode.Decimal:
				return new DecimalConstant(builtinTypes, (decimal)value, Location.Null);
			case TypeCode.String:
				return new StringConstant(builtinTypes, (string)value, Location.Null);
			}
			throw new NotImplementedException(value.GetType().ToString());
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004C090 File Offset: 0x0004A290
		public TypeSpec ImportType(Type type)
		{
			return this.ImportType(type, new MetadataImporter.DynamicTypeReader(type));
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004C0A0 File Offset: 0x0004A2A0
		private TypeSpec ImportType(Type type, MetadataImporter.DynamicTypeReader dtype)
		{
			if (type.HasElementType)
			{
				Type elementType = type.GetElementType();
				dtype.Position++;
				TypeSpec element = this.ImportType(elementType, dtype);
				if (type.IsArray)
				{
					return ArrayContainer.MakeType(this.module, element, type.GetArrayRank());
				}
				if (type.IsByRef)
				{
					return ReferenceContainer.MakeType(this.module, element);
				}
				if (type.IsPointer)
				{
					return PointerContainer.MakeType(this.module, element);
				}
				throw new NotImplementedException("Unknown element type " + type.ToString());
			}
			else
			{
				TypeSpec typeSpec;
				if (!this.compiled_types.TryGetValue(type, out typeSpec))
				{
					return this.CreateType(type, dtype, true);
				}
				if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Object && dtype.IsDynamicObject())
				{
					return this.module.Compiler.BuiltinTypes.Dynamic;
				}
				return typeSpec;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000022F4 File Offset: 0x000004F4
		private static bool IsMissingType(Type type)
		{
			return false;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004C170 File Offset: 0x0004A370
		private Constant ReadDecimalConstant(IList<CustomAttributeData> attrs)
		{
			if (attrs.Count == 0)
			{
				return null;
			}
			foreach (CustomAttributeData customAttributeData in attrs)
			{
				Type declaringType = customAttributeData.Constructor.DeclaringType;
				if (!(declaringType.Name != "DecimalConstantAttribute") && !(declaringType.Namespace != MetadataImporter.CompilerServicesNamespace))
				{
					decimal d = new decimal((int)((uint)customAttributeData.ConstructorArguments[4].Value), (int)((uint)customAttributeData.ConstructorArguments[3].Value), (int)((uint)customAttributeData.ConstructorArguments[2].Value), (byte)customAttributeData.ConstructorArguments[1].Value > 0, (byte)customAttributeData.ConstructorArguments[0].Value);
					return new DecimalConstant(this.module.Compiler.BuiltinTypes, d, Location.Null);
				}
			}
			return null;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0004C2A8 File Offset: 0x0004A4A8
		private static Modifiers ReadMethodModifiers(MethodBase mb, TypeSpec declaringType)
		{
			MethodAttributes attributes = mb.Attributes;
			Modifiers modifiers;
			switch (attributes & MethodAttributes.MemberAccessMask)
			{
			case MethodAttributes.Assembly:
				modifiers = Modifiers.INTERNAL;
				break;
			case MethodAttributes.Family:
				modifiers = Modifiers.PROTECTED;
				break;
			case MethodAttributes.FamORAssem:
				modifiers = (Modifiers.PROTECTED | Modifiers.INTERNAL);
				break;
			case MethodAttributes.Public:
				modifiers = Modifiers.PUBLIC;
				break;
			default:
				modifiers = Modifiers.PRIVATE;
				break;
			}
			if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
			{
				return modifiers | Modifiers.STATIC;
			}
			if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope && declaringType.IsClass)
			{
				return modifiers | Modifiers.ABSTRACT;
			}
			if ((attributes & MethodAttributes.Final) != MethodAttributes.PrivateScope)
			{
				modifiers |= Modifiers.SEALED;
			}
			if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
			{
				if ((attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.PrivateScope)
				{
					if ((modifiers & Modifiers.SEALED) != (Modifiers)0)
					{
						modifiers &= ~Modifiers.SEALED;
					}
					else
					{
						modifiers |= Modifiers.VIRTUAL;
					}
				}
				else
				{
					modifiers |= Modifiers.OVERRIDE;
				}
			}
			return modifiers;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004C34F File Offset: 0x0004A54F
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataImporter()
		{
		}

		// Token: 0x040007A9 RID: 1961
		protected readonly Dictionary<Type, TypeSpec> import_cache;

		// Token: 0x040007AA RID: 1962
		protected readonly Dictionary<Type, TypeSpec> compiled_types;

		// Token: 0x040007AB RID: 1963
		protected readonly Dictionary<Assembly, IAssemblyDefinition> assembly_2_definition;

		// Token: 0x040007AC RID: 1964
		protected readonly ModuleContainer module;

		// Token: 0x040007AD RID: 1965
		public static readonly string CompilerServicesNamespace = "System.Runtime.CompilerServices";

		// Token: 0x040007AE RID: 1966
		[CompilerGenerated]
		private bool <IgnorePrivateMembers>k__BackingField;

		// Token: 0x02000397 RID: 919
		protected struct DynamicTypeReader
		{
			// Token: 0x060026CF RID: 9935 RVA: 0x000B70F1 File Offset: 0x000B52F1
			public DynamicTypeReader(object provider)
			{
				this.Position = 0;
				this.flags = null;
				this.provider = provider;
			}

			// Token: 0x060026D0 RID: 9936 RVA: 0x000B7108 File Offset: 0x000B5308
			public bool IsDynamicObject()
			{
				if (this.provider != null)
				{
					this.ReadAttribute();
				}
				return this.flags != null && this.Position < this.flags.Length && this.flags[this.Position];
			}

			// Token: 0x060026D1 RID: 9937 RVA: 0x000B713F File Offset: 0x000B533F
			public bool HasDynamicAttribute()
			{
				if (this.provider != null)
				{
					this.ReadAttribute();
				}
				return this.flags != null;
			}

			// Token: 0x060026D2 RID: 9938 RVA: 0x000B7158 File Offset: 0x000B5358
			private IList<CustomAttributeData> GetCustomAttributes()
			{
				MemberInfo memberInfo = this.provider as MemberInfo;
				if (memberInfo != null)
				{
					return CustomAttributeData.GetCustomAttributes(memberInfo);
				}
				ParameterInfo parameterInfo = this.provider as ParameterInfo;
				if (parameterInfo != null)
				{
					return CustomAttributeData.GetCustomAttributes(parameterInfo);
				}
				this.provider = null;
				return null;
			}

			// Token: 0x060026D3 RID: 9939 RVA: 0x000B719C File Offset: 0x000B539C
			private void ReadAttribute()
			{
				IList<CustomAttributeData> customAttributes = this.GetCustomAttributes();
				if (customAttributes == null)
				{
					return;
				}
				if (customAttributes.Count > 0)
				{
					foreach (CustomAttributeData customAttributeData in customAttributes)
					{
						Type declaringType = customAttributeData.Constructor.DeclaringType;
						if (!(declaringType.Name != "DynamicAttribute") && !(declaringType.Namespace != MetadataImporter.CompilerServicesNamespace))
						{
							if (customAttributeData.ConstructorArguments.Count == 0)
							{
								this.flags = MetadataImporter.DynamicTypeReader.single_attribute;
								break;
							}
							Type argumentType = customAttributeData.ConstructorArguments[0].ArgumentType;
							if (argumentType.IsArray && Type.GetTypeCode(argumentType.GetElementType()) == TypeCode.Boolean)
							{
								IList<CustomAttributeTypedArgument> list = (IList<CustomAttributeTypedArgument>)customAttributeData.ConstructorArguments[0].Value;
								this.flags = new bool[list.Count];
								for (int i = 0; i < this.flags.Length; i++)
								{
									if (Type.GetTypeCode(list[i].ArgumentType) == TypeCode.Boolean)
									{
										this.flags[i] = (bool)list[i].Value;
									}
								}
								break;
							}
						}
					}
				}
				this.provider = null;
			}

			// Token: 0x060026D4 RID: 9940 RVA: 0x000B7318 File Offset: 0x000B5518
			// Note: this type is marked as 'beforefieldinit'.
			static DynamicTypeReader()
			{
			}

			// Token: 0x04000F9B RID: 3995
			private static readonly bool[] single_attribute = new bool[]
			{
				true
			};

			// Token: 0x04000F9C RID: 3996
			public int Position;

			// Token: 0x04000F9D RID: 3997
			private bool[] flags;

			// Token: 0x04000F9E RID: 3998
			private object provider;
		}
	}
}
