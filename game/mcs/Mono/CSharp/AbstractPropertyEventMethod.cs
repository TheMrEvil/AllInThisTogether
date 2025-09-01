using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x02000255 RID: 597
	public abstract class AbstractPropertyEventMethod : MemberCore, IMethodData, IMemberContext, IModuleContext, IMethodDefinition, IMemberDefinition
	{
		// Token: 0x06001D95 RID: 7573 RVA: 0x000906A7 File Offset: 0x0008E8A7
		protected AbstractPropertyEventMethod(InterfaceMemberBase member, string prefix, Attributes attrs, Location loc) : base(member.Parent, AbstractPropertyEventMethod.SetupName(prefix, member, loc), attrs)
		{
			this.prefix = prefix;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000906C6 File Offset: 0x0008E8C6
		private static MemberName SetupName(string prefix, InterfaceMemberBase member, Location loc)
		{
			return new MemberName(member.MemberName.Left, prefix + member.ShortName, member.MemberName.ExplicitInterface, loc);
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000906F0 File Offset: 0x0008E8F0
		public void UpdateName(InterfaceMemberBase member)
		{
			this.SetMemberName(AbstractPropertyEventMethod.SetupName(this.prefix, member, base.Location));
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0009070A File Offset: 0x0008E90A
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x00090712 File Offset: 0x0008E912
		public ToplevelBlock Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0000212D File Offset: 0x0000032D
		public CallingConventions CallingConventions
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0009071B File Offset: 0x0008E91B
		public EmitContext CreateEmitContext(ILGenerator ig, SourceMethodBuilder sourceMethod)
		{
			return new EmitContext(this, ig, this.ReturnType, sourceMethod);
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0000212D File Offset: 0x0000032D
		public bool IsAccessor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0008E138 File Offset: 0x0008C338
		public MemberName MethodName
		{
			get
			{
				return base.MemberName;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0009072B File Offset: 0x0008E92B
		public TypeSpec[] ParameterTypes
		{
			get
			{
				return this.ParameterInfo.Types;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00090738 File Offset: 0x0008E938
		MethodBase IMethodDefinition.Metadata
		{
			get
			{
				return this.method_data.MethodBuilder;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001DA0 RID: 7584
		public abstract ParametersCompiled ParameterInfo { get; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001DA1 RID: 7585
		public abstract TypeSpec ReturnType { get; }

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00090748 File Offset: 0x0008E948
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.CLSCompliant || a.Type == pa.Obsolete || a.Type == pa.Conditional)
			{
				base.Report.Error(1667, a.Location, "Attribute `{0}' is not valid on property or event accessors. It is valid on `{1}' declarations only", a.Type.GetSignatureForError(), a.GetValidTargets());
				return;
			}
			if (a.IsValidSecurityAttribute())
			{
				a.ExtractSecurityPermissionSet(ctor, ref this.declarative_security);
				return;
			}
			if (a.Target == AttributeTargets.Method)
			{
				this.method_data.MethodBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
				return;
			}
			if (a.Target == AttributeTargets.ReturnValue)
			{
				if (this.return_attributes == null)
				{
					this.return_attributes = new ReturnParameter(this, this.method_data.MethodBuilder, base.Location);
				}
				this.return_attributes.ApplyAttributeBuilder(a, ctor, cdata, pa);
				return;
			}
			this.ApplyToExtraTarget(a, ctor, cdata, pa);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00090849 File Offset: 0x0008EA49
		protected virtual void ApplyToExtraTarget(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			throw new NotSupportedException("You forgot to define special attribute target handling");
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0000225C File Offset: 0x0000045C
		public sealed override bool Define()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00090858 File Offset: 0x0008EA58
		public virtual void Emit(TypeDefinition parent)
		{
			this.method_data.Emit(parent);
			if ((base.ModFlags & Modifiers.COMPILER_GENERATED) != (Modifiers)0 && !this.Parent.IsCompilerGenerated)
			{
				this.Module.PredefinedAttributes.CompilerGenerated.EmitAttribute(this.method_data.MethodBuilder);
			}
			if ((base.ModFlags & Modifiers.DEBUGGER_HIDDEN) != (Modifiers)0)
			{
				this.Module.PredefinedAttributes.DebuggerHidden.EmitAttribute(this.method_data.MethodBuilder);
			}
			if (this.ReturnType.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				this.return_attributes = new ReturnParameter(this, this.method_data.MethodBuilder, base.Location);
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.return_attributes.Builder);
			}
			else if (this.ReturnType.HasDynamicElement)
			{
				this.return_attributes = new ReturnParameter(this, this.method_data.MethodBuilder, base.Location);
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.return_attributes.Builder, this.ReturnType, base.Location);
			}
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if (this.declarative_security != null)
			{
				foreach (KeyValuePair<SecurityAction, PermissionSet> keyValuePair in this.declarative_security)
				{
					this.method_data.MethodBuilder.AddDeclarativeSecurity(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.block = null;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00090A00 File Offset: 0x0008EC00
		public override bool EnableOverloadChecks(MemberCore overload)
		{
			if (overload is MethodCore)
			{
				this.caching_flags |= MemberCore.Flags.MethodOverloadsExist;
				return true;
			}
			return overload is AbstractPropertyEventMethod;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00090A29 File Offset: 0x0008EC29
		public override string GetCallerMemberName()
		{
			return base.GetCallerMemberName().Substring(this.prefix.Length);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0000225C File Offset: 0x0000045C
		public override string GetSignatureForDocumentation()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsClsComplianceRequired()
		{
			return false;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00090A41 File Offset: 0x0008EC41
		public void PrepareEmit()
		{
			this.method_data.DefineMethodBuilder(this.Parent.PartialContainer, this.ParameterInfo);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00090A60 File Offset: 0x0008EC60
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.method_data != null)
			{
				this.method_data.WriteDebugSymbol(file);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00090A76 File Offset: 0x0008EC76
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x00090A7E File Offset: 0x0008EC7E
		public MethodSpec Spec
		{
			[CompilerGenerated]
			get
			{
				return this.<Spec>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Spec>k__BackingField = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00090A87 File Offset: 0x0008EC87
		public override string DocCommentHeader
		{
			get
			{
				throw new InvalidOperationException("Unexpected attempt to get doc comment from " + base.GetType() + ".");
			}
		}

		// Token: 0x04000AEF RID: 2799
		protected MethodData method_data;

		// Token: 0x04000AF0 RID: 2800
		protected ToplevelBlock block;

		// Token: 0x04000AF1 RID: 2801
		protected Dictionary<SecurityAction, PermissionSet> declarative_security;

		// Token: 0x04000AF2 RID: 2802
		protected readonly string prefix;

		// Token: 0x04000AF3 RID: 2803
		private ReturnParameter return_attributes;

		// Token: 0x04000AF4 RID: 2804
		[CompilerGenerated]
		private MethodSpec <Spec>k__BackingField;
	}
}
