using System;
using System.Linq;

namespace Mono.CSharp
{
	// Token: 0x0200024C RID: 588
	public class Method : MethodOrOperator, IGenericMethodDefinition, IMethodDefinition, IMemberDefinition
	{
		// Token: 0x06001D3C RID: 7484 RVA: 0x0008E30C File Offset: 0x0008C50C
		public Method(TypeDefinition parent, FullNamedExpression return_type, Modifiers mod, MemberName name, ParametersCompiled parameters, Attributes attrs) : base(parent, return_type, mod, (parent.PartialContainer.Kind == MemberKind.Interface) ? (Modifiers.NEW | Modifiers.UNSAFE) : ((parent.PartialContainer.Kind == MemberKind.Struct) ? (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.STATIC | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE | Modifiers.ASYNC) : (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE | Modifiers.ASYNC)), name, attrs, parameters)
		{
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0008E35F File Offset: 0x0008C55F
		protected Method(TypeDefinition parent, FullNamedExpression return_type, Modifiers mod, Modifiers amod, MemberName name, ParametersCompiled parameters, Attributes attrs) : base(parent, return_type, mod, amod, name, attrs, parameters)
		{
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0008E372 File Offset: 0x0008C572
		public override TypeParameters CurrentTypeParameters
		{
			get
			{
				return base.MemberName.TypeParameters;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0008E37F File Offset: 0x0008C57F
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return this.CurrentTypeParameters.Types;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0008E38C File Offset: 0x0008C58C
		public int TypeParametersCount
		{
			get
			{
				if (this.CurrentTypeParameters != null)
				{
					return this.CurrentTypeParameters.Count;
				}
				return 0;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0008E3A3 File Offset: 0x0008C5A3
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0008E3AC File Offset: 0x0008C5AC
		public static Method Create(TypeDefinition parent, FullNamedExpression returnType, Modifiers mod, MemberName name, ParametersCompiled parameters, Attributes attrs)
		{
			Method method = new Method(parent, returnType, mod, name, parameters, attrs);
			if ((mod & Modifiers.PARTIAL) != (Modifiers)0)
			{
				if ((mod & (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN)) != (Modifiers)0)
				{
					method.Report.Error(750, method.Location, "A partial method cannot define access modifier or any of abstract, extern, new, override, sealed, or virtual modifiers");
					mod &= ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN);
				}
				if ((parent.ModFlags & Modifiers.PARTIAL) == (Modifiers)0)
				{
					method.Report.Error(751, method.Location, "A partial method must be declared within a partial class or partial struct");
				}
			}
			if ((mod & Modifiers.STATIC) == (Modifiers)0 && parameters.HasExtensionMethodType)
			{
				method.Report.Error(1105, method.Location, "`{0}': Extension methods must be declared static", method.GetSignatureForError());
			}
			return method;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0008E45A File Offset: 0x0008C65A
		public override string GetSignatureForError()
		{
			return base.GetSignatureForError() + this.parameters.GetSignatureForError();
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0008E472 File Offset: 0x0008C672
		private void Error_DuplicateEntryPoint(Method b)
		{
			base.Report.Error(17, b.Location, "Program `{0}' has more than one entry point defined: `{1}'", b.Module.Builder.ScopeName, b.GetSignatureForError());
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0008E4A4 File Offset: 0x0008C6A4
		private bool IsEntryPoint()
		{
			if (base.ReturnType.Kind != MemberKind.Void && base.ReturnType.BuiltinType != BuiltinTypeSpec.Type.Int)
			{
				return false;
			}
			if (this.parameters.IsEmpty)
			{
				return true;
			}
			if (this.parameters.Count > 1)
			{
				return false;
			}
			ArrayContainer arrayContainer = this.parameters.Types[0] as ArrayContainer;
			return arrayContainer != null && arrayContainer.Rank == 1 && arrayContainer.Element.BuiltinType == BuiltinTypeSpec.Type.String && (this.parameters[0].ModFlags & Parameter.Modifier.RefOutMask) == Parameter.Modifier.NONE;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0008E538 File Offset: 0x0008C738
		public override FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			if (arity == 0)
			{
				TypeParameters currentTypeParameters = this.CurrentTypeParameters;
				if (currentTypeParameters != null)
				{
					TypeParameter typeParameter = currentTypeParameters.Find(name);
					if (typeParameter != null)
					{
						return new TypeParameterExpr(typeParameter, loc);
					}
				}
			}
			return base.LookupNamespaceOrType(name, arity, mode, loc);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0008E574 File Offset: 0x0008C774
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.Conditional)
			{
				if (this.IsExplicitImpl)
				{
					base.Error_ConditionalAttributeIsNotValid();
					return;
				}
				if ((base.ModFlags & Modifiers.OVERRIDE) != (Modifiers)0)
				{
					base.Report.Error(243, base.Location, "Conditional not valid on `{0}' because it is an override method", this.GetSignatureForError());
					return;
				}
				if (base.ReturnType.Kind != MemberKind.Void)
				{
					base.Report.Error(578, base.Location, "Conditional not valid on `{0}' because its return type is not void", this.GetSignatureForError());
					return;
				}
				if (this.IsInterface)
				{
					base.Report.Error(582, base.Location, "Conditional not valid on interface members");
					return;
				}
				if (this.MethodData.implementing != null)
				{
					base.Report.SymbolRelatedToPreviousError(this.MethodData.implementing.DeclaringType);
					base.Report.Error(629, base.Location, "Conditional member `{0}' cannot implement interface member `{1}'", this.GetSignatureForError(), TypeManager.CSharpSignature(this.MethodData.implementing));
					return;
				}
				for (int i = 0; i < this.parameters.Count; i++)
				{
					if ((this.parameters.FixedParameters[i].ModFlags & Parameter.Modifier.OUT) != Parameter.Modifier.NONE)
					{
						base.Report.Error(685, base.Location, "Conditional method `{0}' cannot have an out parameter", this.GetSignatureForError());
						return;
					}
				}
			}
			if (a.Type == pa.Extension)
			{
				a.Error_MisusedExtensionAttribute();
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0008E700 File Offset: 0x0008C900
		private void CreateTypeParameters()
		{
			TypeParameters typeParameters = base.MemberName.TypeParameters;
			TypeParameters typeParametersAll = this.Parent.TypeParametersAll;
			for (int i = 0; i < base.MemberName.Arity; i++)
			{
				string name = typeParameters[i].MemberName.Name;
				if (this.block == null)
				{
					if (this.parameters.GetParameterIndexByName(name) >= 0)
					{
						ToplevelBlock toplevelBlock = this.block;
						if (toplevelBlock == null)
						{
							toplevelBlock = new ToplevelBlock(this.Compiler, base.Location);
						}
						toplevelBlock.Error_AlreadyDeclaredTypeParameter(name, this.parameters[i].Location);
					}
				}
				else
				{
					INamedBlockVariable namedBlockVariable = null;
					this.block.GetLocalName(name, this.block, ref namedBlockVariable);
					if (namedBlockVariable != null)
					{
						namedBlockVariable.Block.Error_AlreadyDeclaredTypeParameter(name, namedBlockVariable.Location);
					}
				}
				if (typeParametersAll != null)
				{
					TypeParameter typeParameter = typeParametersAll.Find(name);
					if (typeParameter != null)
					{
						typeParameters[i].WarningParentNameConflict(typeParameter);
					}
				}
			}
			typeParameters.Create(null, 0, this.Parent);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0008E800 File Offset: 0x0008CA00
		protected virtual void DefineTypeParameters()
		{
			TypeParameters currentTypeParameters = this.CurrentTypeParameters;
			TypeParameterSpec[] array = null;
			TypeParameterSpec[] array2 = TypeParameterSpec.EmptyTypes;
			TypeSpec[] array3 = TypeSpec.EmptyTypes;
			if ((base.ModFlags & Modifiers.OVERRIDE) != (Modifiers)0 || this.IsExplicitImpl)
			{
				MethodSpec methodSpec = this.base_method ?? this.MethodData.implementing;
				if (methodSpec != null)
				{
					array = methodSpec.GenericDefinition.TypeParameters;
					if (methodSpec.DeclaringType.IsGeneric)
					{
						array2 = methodSpec.DeclaringType.MemberDefinition.TypeParameters;
						if (this.base_method != null)
						{
							TypeSpec typeSpec = this.CurrentType;
							while (typeSpec.BaseType != methodSpec.DeclaringType)
							{
								typeSpec = typeSpec.BaseType;
							}
							array3 = typeSpec.BaseType.TypeArguments;
						}
						else
						{
							foreach (TypeSpec typeSpec2 in this.Parent.CurrentType.Interfaces)
							{
								if (typeSpec2 == methodSpec.DeclaringType)
								{
									array3 = typeSpec2.TypeArguments;
									break;
								}
							}
						}
					}
					if (methodSpec.IsGeneric)
					{
						foreach (TypeParameterSpec typeParameterSpec in array)
						{
							ObsoleteAttribute attributeObsolete = typeParameterSpec.BaseType.GetAttributeObsolete();
							if (attributeObsolete != null)
							{
								AttributeTester.Report_ObsoleteMessage(attributeObsolete, typeParameterSpec.BaseType.GetSignatureForError(), base.Location, base.Report);
							}
							if (typeParameterSpec.InterfacesDefined != null)
							{
								foreach (TypeSpec typeSpec3 in typeParameterSpec.InterfacesDefined)
								{
									attributeObsolete = typeSpec3.GetAttributeObsolete();
									if (attributeObsolete != null)
									{
										AttributeTester.Report_ObsoleteMessage(attributeObsolete, typeSpec3.GetSignatureForError(), base.Location, base.Report);
									}
								}
							}
						}
						if (array2.Length != 0)
						{
							array2 = array2.Concat(array).ToArray<TypeParameterSpec>();
							array3 = array3.Concat(currentTypeParameters.Types).ToArray<TypeSpec>();
						}
						else
						{
							array2 = array;
							array3 = currentTypeParameters.Types;
						}
					}
				}
			}
			for (int k = 0; k < currentTypeParameters.Count; k++)
			{
				TypeParameter typeParameter = currentTypeParameters[k];
				if (array == null)
				{
					typeParameter.ResolveConstraints(this);
				}
				else
				{
					TypeParameterSpec typeParameterSpec2 = array[k];
					TypeParameterSpec type = typeParameter.Type;
					type.SpecialConstraint = typeParameterSpec2.SpecialConstraint;
					TypeParameterInflator inflator = new TypeParameterInflator(this, this.CurrentType, array2, array3);
					typeParameterSpec2.InflateConstraints(inflator, type);
					TypeSpec[] array5 = type.TypeArguments;
					if (array5 != null)
					{
						for (int l = 0; l < array5.Length; l++)
						{
							TypeSpec typeSpec4 = array5[l];
							if (typeSpec4.IsClass || typeSpec4.IsStruct)
							{
								TypeSpec[] array6 = null;
								for (int m = l + 1; m < array5.Length; m++)
								{
									TypeSpec typeSpec5 = array5[m];
									if (TypeSpecComparer.IsEqual(typeSpec4, typeSpec5) || TypeSpec.IsBaseClass(typeSpec4, typeSpec5, false))
									{
										array6 = new TypeSpec[array5.Length - 1];
										Array.Copy(array5, 0, array6, 0, m);
										Array.Copy(array5, m + 1, array6, m, array5.Length - m - 1);
									}
									else if (!TypeSpec.IsBaseClass(typeSpec5, typeSpec4, false))
									{
										Constraints.Error_ConflictingConstraints(this, type, typeSpec4, typeSpec5, base.Location);
									}
								}
								if (array6 != null)
								{
									array5 = array6;
									type.TypeArguments = array5;
								}
								else
								{
									Constraints.CheckConflictingInheritedConstraint(type, typeSpec4, this, base.Location);
								}
							}
						}
					}
				}
			}
			if (array == null && this.MethodData != null && this.MethodData.implementing != null)
			{
				Method.CheckImplementingMethodConstraints(this.Parent, this.spec, this.MethodData.implementing);
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0008EB98 File Offset: 0x0008CD98
		public static bool CheckImplementingMethodConstraints(TypeContainer container, MethodSpec method, MethodSpec baseMethod)
		{
			TypeParameterSpec[] constraints = method.Constraints;
			TypeParameterSpec[] constraints2 = baseMethod.Constraints;
			for (int i = 0; i < constraints.Length; i++)
			{
				if (!constraints[i].HasSameConstraintsImplementation(constraints2[i]))
				{
					container.Compiler.Report.SymbolRelatedToPreviousError(method);
					container.Compiler.Report.SymbolRelatedToPreviousError(baseMethod);
					MemberCore memberCore = (constraints[i].MemberDefinition as MemberCore) ?? container;
					container.Compiler.Report.Error(425, memberCore.Location, "The constraints for type parameter `{0}' of method `{1}' must match the constraints for type parameter `{2}' of interface method `{3}'. Consider using an explicit interface implementation instead", new string[]
					{
						constraints[i].GetSignatureForError(),
						method.GetSignatureForError(),
						constraints2[i].GetSignatureForError(),
						baseMethod.GetSignatureForError()
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0008EC60 File Offset: 0x0008CE60
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			if (this.member_type.Kind == MemberKind.Void && this.parameters.IsEmpty && base.MemberName.Arity == 0 && base.MemberName.Name == Destructor.MetadataName)
			{
				base.Report.Warning(465, 1, base.Location, "Introducing `Finalize' method can interfere with destructor invocation. Did you intend to declare a destructor?");
			}
			if (this.Compiler.Settings.StdLib && base.ReturnType.IsSpecialRuntimeType)
			{
				Method.Error1599(base.Location, base.ReturnType, base.Report);
				return false;
			}
			if (this.CurrentTypeParameters == null)
			{
				if (this.base_method != null && !this.IsExplicitImpl)
				{
					if (this.parameters.Count == 1 && base.ParameterTypes[0].BuiltinType == BuiltinTypeSpec.Type.Object && base.MemberName.Name == "Equals")
					{
						this.Parent.PartialContainer.Mark_HasEquals();
					}
					else if (this.parameters.IsEmpty && base.MemberName.Name == "GetHashCode")
					{
						this.Parent.PartialContainer.Mark_HasGetHashCode();
					}
				}
			}
			else
			{
				this.DefineTypeParameters();
			}
			if (this.block != null)
			{
				if (this.block.IsIterator)
				{
					Iterator.CreateIterator(this, this.Parent.PartialContainer, base.ModFlags);
					base.ModFlags |= Modifiers.DEBUGGER_HIDDEN;
				}
				if ((base.ModFlags & Modifiers.ASYNC) != (Modifiers)0)
				{
					if (base.ReturnType.Kind != MemberKind.Void && base.ReturnType != this.Module.PredefinedTypes.Task.TypeSpec && !base.ReturnType.IsGenericTask)
					{
						base.Report.Error(1983, base.Location, "The return type of an async method must be void, Task, or Task<T>");
					}
					this.block = (ToplevelBlock)this.block.ConvertToAsyncTask(this, this.Parent.PartialContainer, this.parameters, base.ReturnType, null, base.Location);
					base.ModFlags |= Modifiers.DEBUGGER_STEP_THROUGH;
				}
				if (this.Compiler.Settings.WriteMetadataOnly)
				{
					this.block = null;
				}
			}
			if ((base.ModFlags & Modifiers.STATIC) == (Modifiers)0)
			{
				return true;
			}
			if (this.parameters.HasExtensionMethodType)
			{
				if (this.Parent.PartialContainer.IsStatic && !this.Parent.IsGenericOrParentIsGeneric)
				{
					if (!this.Parent.IsTopLevel)
					{
						base.Report.Error(1109, base.Location, "`{0}': Extension methods cannot be defined in a nested class", this.GetSignatureForError());
					}
					if (!this.Module.PredefinedAttributes.Extension.IsDefined)
					{
						base.Report.Error(1110, base.Location, "`{0}': Extension methods require `System.Runtime.CompilerServices.ExtensionAttribute' type to be available. Are you missing an assembly reference?", this.GetSignatureForError());
					}
					base.ModFlags |= Modifiers.METHOD_EXTENSION;
					this.Parent.PartialContainer.ModFlags |= Modifiers.METHOD_EXTENSION;
					base.Spec.DeclaringType.SetExtensionMethodContainer();
					this.Parent.Module.HasExtensionMethod = true;
				}
				else
				{
					base.Report.Error(1106, base.Location, "`{0}': Extension methods must be defined in a non-generic static class", this.GetSignatureForError());
				}
			}
			CompilerSettings settings = this.Compiler.Settings;
			if (settings.NeedsEntryPoint && base.MemberName.Name == "Main" && !base.IsPartialDefinition && (settings.MainClass == null || settings.MainClass == this.Parent.TypeBuilder.FullName))
			{
				if (this.IsEntryPoint())
				{
					if (this.Parent.DeclaringAssembly.EntryPoint == null)
					{
						if (this.Parent.IsGenericOrParentIsGeneric || base.MemberName.IsGeneric)
						{
							base.Report.Warning(402, 4, base.Location, "`{0}': an entry point cannot be generic or in a generic type", this.GetSignatureForError());
						}
						else if ((base.ModFlags & Modifiers.ASYNC) != (Modifiers)0)
						{
							base.Report.Error(4009, base.Location, "`{0}': an entry point cannot be async method", this.GetSignatureForError());
						}
						else
						{
							base.SetIsUsed();
							this.Parent.DeclaringAssembly.EntryPoint = this;
						}
					}
					else
					{
						this.Error_DuplicateEntryPoint(this.Parent.DeclaringAssembly.EntryPoint);
						this.Error_DuplicateEntryPoint(this);
					}
				}
				else
				{
					base.Report.Warning(28, 4, base.Location, "`{0}' has the wrong signature to be an entry point", this.GetSignatureForError());
				}
			}
			return true;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0008F127 File Offset: 0x0008D327
		public override void PrepareEmit()
		{
			if (base.IsPartialDefinition)
			{
				if (this.partialMethodImplementation != null)
				{
					this.MethodData = this.partialMethodImplementation.MethodData;
				}
				return;
			}
			base.PrepareEmit();
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0008F154 File Offset: 0x0008D354
		public override void Emit()
		{
			try
			{
				if (base.IsPartialDefinition)
				{
					if (this.partialMethodImplementation != null && this.CurrentTypeParameters != null)
					{
						this.CurrentTypeParameters.CheckPartialConstraints(this.partialMethodImplementation);
						TypeParameters currentTypeParameters = this.partialMethodImplementation.CurrentTypeParameters;
						for (int i = 0; i < this.CurrentTypeParameters.Count; i++)
						{
							this.CurrentTypeParameters[i].Define(currentTypeParameters[i]);
						}
					}
				}
				else
				{
					if ((base.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && (this.caching_flags & MemberCore.Flags.PartialDefinitionExists) == (MemberCore.Flags)0)
					{
						base.Report.Error(759, base.Location, "A partial method `{0}' implementation is missing a partial method declaration", this.GetSignatureForError());
					}
					if (this.CurrentTypeParameters != null)
					{
						for (int j = 0; j < this.CurrentTypeParameters.Count; j++)
						{
							TypeParameter typeParameter = this.CurrentTypeParameters[j];
							typeParameter.CheckGenericConstraints(false);
							typeParameter.Emit();
						}
					}
					if ((base.ModFlags & Modifiers.METHOD_EXTENSION) != (Modifiers)0)
					{
						this.Module.PredefinedAttributes.Extension.EmitAttribute(base.MethodBuilder);
					}
					base.Emit();
				}
			}
			catch (Exception e)
			{
				throw new InternalErrorException(this, e);
			}
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0008F294 File Offset: 0x0008D494
		public override bool EnableOverloadChecks(MemberCore overload)
		{
			return !(overload is Indexer) && base.EnableOverloadChecks(overload);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0008F2A7 File Offset: 0x0008D4A7
		public static void Error1599(Location loc, TypeSpec t, Report Report)
		{
			Report.Error(1599, loc, "Method or delegate cannot return type `{0}'", t.GetSignatureForError());
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0008F2C0 File Offset: 0x0008D4C0
		protected override bool ResolveMemberType()
		{
			if (this.CurrentTypeParameters != null)
			{
				this.CreateTypeParameters();
			}
			return base.ResolveMemberType();
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0008F2D8 File Offset: 0x0008D4D8
		public void SetPartialDefinition(Method methodDefinition)
		{
			this.caching_flags |= MemberCore.Flags.PartialDefinitionExists;
			methodDefinition.partialMethodImplementation = this;
			for (int i = 0; i < methodDefinition.parameters.Count; i++)
			{
				Parameter parameter = methodDefinition.parameters[i];
				Parameter parameter2 = this.parameters[i];
				parameter2.Name = parameter.Name;
				parameter2.DefaultValue = parameter.DefaultValue;
				if (parameter.OptAttributes != null)
				{
					if (parameter2.OptAttributes == null)
					{
						parameter2.OptAttributes = parameter.OptAttributes;
					}
					else
					{
						parameter2.OptAttributes.Attrs.AddRange(parameter.OptAttributes.Attrs);
					}
				}
			}
			if (methodDefinition.attributes != null)
			{
				if (this.attributes == null)
				{
					this.attributes = methodDefinition.attributes;
				}
				else
				{
					this.attributes.Attrs.AddRange(methodDefinition.attributes.Attrs);
				}
			}
			if (this.CurrentTypeParameters != null)
			{
				for (int j = 0; j < this.CurrentTypeParameters.Count; j++)
				{
					TypeParameter typeParameter = methodDefinition.CurrentTypeParameters[j];
					if (typeParameter.OptAttributes != null)
					{
						TypeParameter typeParameter2 = this.CurrentTypeParameters[j];
						if (typeParameter2.OptAttributes == null)
						{
							typeParameter2.OptAttributes = typeParameter.OptAttributes;
						}
						else
						{
							typeParameter2.OptAttributes.Attrs.AddRange(typeParameter2.OptAttributes.Attrs);
						}
					}
				}
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private Method partialMethodImplementation;
	}
}
