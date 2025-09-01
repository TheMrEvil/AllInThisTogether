using System;

namespace Mono.CSharp
{
	// Token: 0x02000133 RID: 307
	public abstract class MemberBase : MemberCore
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x0003F318 File Offset: 0x0003D518
		protected MemberBase(TypeDefinition parent, FullNamedExpression type, Modifiers mod, Modifiers allowed_mod, Modifiers def_mod, MemberName name, Attributes attrs) : base(parent, name, attrs)
		{
			this.Parent = parent;
			this.type_expr = type;
			if (name != MemberName.Null)
			{
				base.ModFlags = ModifiersExtensions.Check(allowed_mod, mod, def_mod, base.Location, base.Report);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003F358 File Offset: 0x0003D558
		public TypeSpec MemberType
		{
			get
			{
				return this.member_type;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003F360 File Offset: 0x0003D560
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.type_expr;
			}
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003F368 File Offset: 0x0003D568
		public override bool Define()
		{
			this.DoMemberTypeIndependentChecks();
			if (!this.ResolveMemberType())
			{
				return false;
			}
			this.DoMemberTypeDependentChecks();
			return true;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003F384 File Offset: 0x0003D584
		protected virtual void DoMemberTypeIndependentChecks()
		{
			if ((this.Parent.ModFlags & Modifiers.SEALED) != (Modifiers)0 && (base.ModFlags & (Modifiers.ABSTRACT | Modifiers.VIRTUAL)) != (Modifiers)0)
			{
				base.Report.Error(549, base.Location, "New virtual member `{0}' is declared in a sealed class `{1}'", this.GetSignatureForError(), this.Parent.GetSignatureForError());
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003F3DC File Offset: 0x0003D5DC
		protected virtual void DoMemberTypeDependentChecks()
		{
			if (!base.IsAccessibleAs(this.MemberType))
			{
				base.Report.SymbolRelatedToPreviousError(this.MemberType);
				if (this is Property)
				{
					base.Report.Error(53, base.Location, string.Concat(new string[]
					{
						"Inconsistent accessibility: property type `",
						this.MemberType.GetSignatureForError(),
						"' is less accessible than property `",
						this.GetSignatureForError(),
						"'"
					}));
					return;
				}
				if (this is Indexer)
				{
					base.Report.Error(54, base.Location, string.Concat(new string[]
					{
						"Inconsistent accessibility: indexer return type `",
						this.MemberType.GetSignatureForError(),
						"' is less accessible than indexer `",
						this.GetSignatureForError(),
						"'"
					}));
					return;
				}
				if (this is MethodCore)
				{
					if (this is Operator)
					{
						base.Report.Error(56, base.Location, string.Concat(new string[]
						{
							"Inconsistent accessibility: return type `",
							this.MemberType.GetSignatureForError(),
							"' is less accessible than operator `",
							this.GetSignatureForError(),
							"'"
						}));
						return;
					}
					base.Report.Error(50, base.Location, string.Concat(new string[]
					{
						"Inconsistent accessibility: return type `",
						this.MemberType.GetSignatureForError(),
						"' is less accessible than method `",
						this.GetSignatureForError(),
						"'"
					}));
					return;
				}
				else
				{
					if (this is Event)
					{
						base.Report.Error(7025, base.Location, "Inconsistent accessibility: event type `{0}' is less accessible than event `{1}'", this.MemberType.GetSignatureForError(), this.GetSignatureForError());
						return;
					}
					base.Report.Error(52, base.Location, string.Concat(new string[]
					{
						"Inconsistent accessibility: field type `",
						this.MemberType.GetSignatureForError(),
						"' is less accessible than field `",
						this.GetSignatureForError(),
						"'"
					}));
				}
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
		protected void IsTypePermitted()
		{
			if (this.MemberType.IsSpecialRuntimeType)
			{
				if (this.Parent is StateMachine)
				{
					base.Report.Error(4012, base.Location, "Parameters or local variables of type `{0}' cannot be declared in async methods or iterators", this.MemberType.GetSignatureForError());
					return;
				}
				if (this.Parent is HoistedStoreyClass)
				{
					base.Report.Error(4013, base.Location, "Local variables of type `{0}' cannot be used inside anonymous methods, lambda expressions or query expressions", this.MemberType.GetSignatureForError());
					return;
				}
				base.Report.Error(610, base.Location, "Field or property cannot be of type `{0}'", this.MemberType.GetSignatureForError());
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003F693 File Offset: 0x0003D893
		protected virtual bool CheckBase()
		{
			base.CheckProtectedModifier();
			return true;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003F69C File Offset: 0x0003D89C
		public override string GetSignatureForDocumentation()
		{
			return this.Parent.GetSignatureForDocumentation() + "." + base.MemberName.Basename;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003F6BE File Offset: 0x0003D8BE
		protected virtual bool ResolveMemberType()
		{
			if (this.member_type != null)
			{
				throw new InternalErrorException("Multi-resolve");
			}
			this.member_type = this.type_expr.ResolveAsType(this, false);
			return this.member_type != null;
		}

		// Token: 0x0400070B RID: 1803
		protected FullNamedExpression type_expr;

		// Token: 0x0400070C RID: 1804
		protected TypeSpec member_type;

		// Token: 0x0400070D RID: 1805
		public new TypeDefinition Parent;
	}
}
