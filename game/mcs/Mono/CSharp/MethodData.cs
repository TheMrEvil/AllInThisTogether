using System;
using System.Reflection;
using System.Reflection.Emit;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x02000253 RID: 595
	public class MethodData
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0008FD34 File Offset: 0x0008DF34
		public MethodBuilder MethodBuilder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0008FD3C File Offset: 0x0008DF3C
		public TypeSpec DeclaringType
		{
			get
			{
				return this.declaring_type;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x0008FD44 File Offset: 0x0008DF44
		public string MetadataName
		{
			get
			{
				return this.full_name;
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0008FD4C File Offset: 0x0008DF4C
		public MethodData(InterfaceMemberBase member, Modifiers modifiers, MethodAttributes flags, IMethodData method)
		{
			this.member = member;
			this.modifiers = modifiers;
			this.flags = flags;
			this.method = method;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0008FD71 File Offset: 0x0008DF71
		public MethodData(InterfaceMemberBase member, Modifiers modifiers, MethodAttributes flags, IMethodData method, MethodSpec parent_method) : this(member, modifiers, flags, method)
		{
			this.parent_method = parent_method;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0008FD88 File Offset: 0x0008DF88
		public bool Define(TypeDefinition container, string method_full_name)
		{
			PendingImplementation pendingImplementations = container.PendingImplementations;
			bool flag = false;
			MethodSpec methodSpec;
			if (pendingImplementations != null)
			{
				this.implementing = pendingImplementations.IsInterfaceMethod(this.method.MethodName, this.member.InterfaceType, this, out methodSpec, ref flag);
				if (this.member.InterfaceType != null)
				{
					if (this.implementing == null)
					{
						if (this.member is PropertyBase)
						{
							container.Compiler.Report.Error(550, this.method.Location, "`{0}' is an accessor not found in interface member `{1}{2}'", new string[]
							{
								this.method.GetSignatureForError(),
								this.member.InterfaceType.GetSignatureForError(),
								this.member.GetSignatureForError().Substring(this.member.GetSignatureForError().LastIndexOf('.'))
							});
						}
						else
						{
							container.Compiler.Report.Error(539, this.method.Location, "`{0}.{1}' in explicit interface declaration is not a member of interface", this.member.InterfaceType.GetSignatureForError(), this.member.ShortName);
						}
						return false;
					}
					if (this.implementing.IsAccessor && !this.method.IsAccessor)
					{
						container.Compiler.Report.SymbolRelatedToPreviousError(this.implementing);
						container.Compiler.Report.Error(683, this.method.Location, "`{0}' explicit method implementation cannot implement `{1}' because it is an accessor", this.member.GetSignatureForError(), this.implementing.GetSignatureForError());
						return false;
					}
				}
				else if (this.implementing != null && !flag)
				{
					if (!this.method.IsAccessor)
					{
						if (this.implementing.IsAccessor)
						{
							container.Compiler.Report.SymbolRelatedToPreviousError(this.implementing);
							container.Compiler.Report.Error(470, this.method.Location, "Method `{0}' cannot implement interface accessor `{1}'", this.method.GetSignatureForError(), TypeManager.CSharpSignature(this.implementing));
						}
					}
					else if (this.implementing.DeclaringType.IsInterface)
					{
						if (!this.implementing.IsAccessor)
						{
							container.Compiler.Report.SymbolRelatedToPreviousError(this.implementing);
							container.Compiler.Report.Error(686, this.method.Location, "Accessor `{0}' cannot implement interface member `{1}' for type `{2}'. Use an explicit interface implementation", new string[]
							{
								this.method.GetSignatureForError(),
								TypeManager.CSharpSignature(this.implementing),
								container.GetSignatureForError()
							});
						}
						else
						{
							PropertyBase.PropertyMethod propertyMethod = this.method as PropertyBase.PropertyMethod;
							if (propertyMethod != null && propertyMethod.HasCustomAccessModifier && (propertyMethod.ModFlags & Modifiers.PUBLIC) == (Modifiers)0)
							{
								container.Compiler.Report.SymbolRelatedToPreviousError(this.implementing);
								container.Compiler.Report.Error(277, this.method.Location, "Accessor `{0}' must be declared public to implement interface member `{1}'", this.method.GetSignatureForError(), this.implementing.GetSignatureForError());
							}
						}
					}
				}
			}
			else
			{
				methodSpec = null;
			}
			if (this.implementing != null)
			{
				if (this.member.IsExplicitImpl)
				{
					if (this.method.ParameterInfo.HasParams && !this.implementing.Parameters.HasParams)
					{
						container.Compiler.Report.SymbolRelatedToPreviousError(this.implementing);
						container.Compiler.Report.Error(466, this.method.Location, "`{0}': the explicit interface implementation cannot introduce the params modifier", this.method.GetSignatureForError());
					}
					if (methodSpec != null)
					{
						container.Compiler.Report.SymbolRelatedToPreviousError(methodSpec);
						container.Compiler.Report.SymbolRelatedToPreviousError(this.implementing);
						container.Compiler.Report.Warning(473, 2, this.method.Location, "Explicit interface implementation `{0}' matches more than one interface member. Consider using a non-explicit implementation instead", this.method.GetSignatureForError());
					}
				}
				else if (this.implementing.DeclaringType.IsInterface)
				{
					if ((this.flags & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
					{
						this.implementing = null;
					}
					else if (flag && (container.Interfaces == null || !container.Definition.Interfaces.Contains(this.implementing.DeclaringType)))
					{
						this.implementing = null;
					}
				}
				else if ((this.flags & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
				{
					this.implementing = null;
				}
				else if ((this.modifiers & Modifiers.OVERRIDE) == (Modifiers)0)
				{
					this.implementing = null;
				}
				if ((this.modifiers & Modifiers.STATIC) != (Modifiers)0)
				{
					this.implementing = null;
				}
			}
			if (this.implementing != null)
			{
				if ((this.modifiers & Modifiers.OVERRIDE) == (Modifiers)0 && this.implementing.DeclaringType.IsInterface)
				{
					this.flags |= MethodAttributes.VtableLayoutMask;
				}
				this.flags |= (MethodAttributes.Virtual | MethodAttributes.HideBySig);
				if ((this.modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) == (Modifiers)0)
				{
					this.flags |= MethodAttributes.Final;
				}
				pendingImplementations.ImplementMethod(this.method.MethodName, this.member.InterfaceType, this, this.member.IsExplicitImpl, out methodSpec, ref flag);
				if (!this.implementing.DeclaringType.IsInterface && !this.member.IsExplicitImpl && this.implementing.IsAccessor)
				{
					method_full_name = this.implementing.MemberDefinition.Name;
				}
			}
			this.full_name = method_full_name;
			this.declaring_type = container.Definition;
			return true;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00090306 File Offset: 0x0008E506
		private void DefineOverride(TypeDefinition container)
		{
			if (this.implementing == null)
			{
				return;
			}
			if (!this.member.IsExplicitImpl)
			{
				return;
			}
			container.TypeBuilder.DefineMethodOverride(this.builder, (MethodInfo)this.implementing.GetMetaInfo());
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x00090340 File Offset: 0x0008E540
		public MethodBuilder DefineMethodBuilder(TypeDefinition container)
		{
			if (this.builder != null)
			{
				throw new InternalErrorException();
			}
			this.builder = container.TypeBuilder.DefineMethod(this.full_name, this.flags, this.method.CallingConventions);
			return this.builder;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0009037E File Offset: 0x0008E57E
		public MethodBuilder DefineMethodBuilder(TypeDefinition container, ParametersCompiled param)
		{
			this.DefineMethodBuilder(container);
			this.builder.SetReturnType(this.method.ReturnType.GetMetaInfo());
			this.builder.SetParameters(param.GetMetaInfo());
			return this.builder;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000903BC File Offset: 0x0008E5BC
		public void Emit(TypeDefinition parent)
		{
			this.DefineOverride(parent);
			this.method.ParameterInfo.ApplyAttributes(this.method, this.MethodBuilder);
			ToplevelBlock block = this.method.Block;
			if (block != null)
			{
				BlockContext bc = new BlockContext(this.method, block, this.method.ReturnType);
				if (block.Resolve(bc, this.method))
				{
					this.debug_builder = this.member.Parent.CreateMethodSymbolEntry();
					EmitContext ec = this.method.CreateEmitContext(this.MethodBuilder.GetILGenerator(), this.debug_builder);
					block.Emit(ec);
				}
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0009045C File Offset: 0x0008E65C
		public void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.debug_builder == null)
			{
				return;
			}
			int token = this.builder.GetToken().Token;
			this.debug_builder.DefineMethod(file, token);
		}

		// Token: 0x04000AE2 RID: 2786
		public readonly IMethodData method;

		// Token: 0x04000AE3 RID: 2787
		public MethodSpec implementing;

		// Token: 0x04000AE4 RID: 2788
		protected InterfaceMemberBase member;

		// Token: 0x04000AE5 RID: 2789
		protected Modifiers modifiers;

		// Token: 0x04000AE6 RID: 2790
		protected MethodAttributes flags;

		// Token: 0x04000AE7 RID: 2791
		protected TypeSpec declaring_type;

		// Token: 0x04000AE8 RID: 2792
		protected MethodSpec parent_method;

		// Token: 0x04000AE9 RID: 2793
		private SourceMethodBuilder debug_builder;

		// Token: 0x04000AEA RID: 2794
		private string full_name;

		// Token: 0x04000AEB RID: 2795
		private MethodBuilder builder;
	}
}
