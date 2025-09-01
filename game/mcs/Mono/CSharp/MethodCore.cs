using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000248 RID: 584
	public abstract class MethodCore : InterfaceMemberBase, IParametersMember, IInterfaceMemberSpec
	{
		// Token: 0x06001CFB RID: 7419 RVA: 0x0008D161 File Offset: 0x0008B361
		protected MethodCore(TypeDefinition parent, FullNamedExpression type, Modifiers mod, Modifiers allowed_mod, MemberName name, Attributes attrs, ParametersCompiled parameters) : base(parent, type, mod, allowed_mod, name, attrs)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0000212D File Offset: 0x0000032D
		public override Variance ExpectedMemberTypeVariance
		{
			get
			{
				return Variance.Covariant;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0008D17A File Offset: 0x0008B37A
		public TypeSpec[] ParameterTypes
		{
			get
			{
				return this.parameters.Types;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0008D187 File Offset: 0x0008B387
		public ParametersCompiled ParameterInfo
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0008D187 File Offset: 0x0008B387
		AParametersCollection IParametersMember.Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0008D18F File Offset: 0x0008B38F
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0008D197 File Offset: 0x0008B397
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

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0008D1A0 File Offset: 0x0008B3A0
		public CallingConventions CallingConventions
		{
			get
			{
				CallingConventions callingConventions = this.parameters.CallingConvention;
				if (!this.IsInterface && (base.ModFlags & Modifiers.STATIC) == (Modifiers)0)
				{
					callingConventions |= CallingConventions.HasThis;
				}
				return callingConventions;
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0008D1D8 File Offset: 0x0008B3D8
		protected override bool CheckOverrideAgainstBase(MemberSpec base_member)
		{
			bool result = base.CheckOverrideAgainstBase(base_member);
			if (!InterfaceMemberBase.CheckAccessModifiers(this, base_member))
			{
				base.Error_CannotChangeAccessModifiers(this, base_member);
				result = false;
			}
			return result;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0008D201 File Offset: 0x0008B401
		protected override bool CheckBase()
		{
			return base.DefineParameters(this.parameters) && base.CheckBase();
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0008D219 File Offset: 0x0008B419
		public override string DocCommentHeader
		{
			get
			{
				return "M:";
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0008D220 File Offset: 0x0008B420
		public override void Emit()
		{
			if ((base.ModFlags & Modifiers.COMPILER_GENERATED) == (Modifiers)0)
			{
				this.parameters.CheckConstraints(this);
			}
			base.Emit();
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0008D242 File Offset: 0x0008B442
		public override bool EnableOverloadChecks(MemberCore overload)
		{
			if (overload is MethodCore)
			{
				this.caching_flags |= MemberCore.Flags.MethodOverloadsExist;
				return true;
			}
			return overload is AbstractPropertyEventMethod || base.EnableOverloadChecks(overload);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0008D274 File Offset: 0x0008B474
		public override string GetSignatureForDocumentation()
		{
			string str = base.GetSignatureForDocumentation();
			if (base.MemberName.Arity > 0)
			{
				str = str + "``" + base.MemberName.Arity.ToString();
			}
			return str + this.parameters.GetSignatureForDocumentation();
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0008D2C6 File Offset: 0x0008B4C6
		public virtual void PrepareEmit()
		{
			this.parameters.ResolveDefaultValues(this);
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0008D2D4 File Offset: 0x0008B4D4
		public MethodSpec Spec
		{
			get
			{
				return this.spec;
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0008D2DC File Offset: 0x0008B4DC
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			if (this.parameters.HasArglist)
			{
				base.Report.Warning(3000, 1, base.Location, "Methods with variable arguments are not CLS-compliant");
			}
			if (this.member_type != null && !this.member_type.IsCLSCompliant())
			{
				base.Report.Warning(3002, 1, base.Location, "Return type of `{0}' is not CLS-compliant", this.GetSignatureForError());
			}
			this.parameters.VerifyClsCompliance(this);
			return true;
		}

		// Token: 0x04000AC8 RID: 2760
		protected ParametersCompiled parameters;

		// Token: 0x04000AC9 RID: 2761
		protected ToplevelBlock block;

		// Token: 0x04000ACA RID: 2762
		protected MethodSpec spec;
	}
}
