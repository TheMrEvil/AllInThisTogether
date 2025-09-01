using System;
using System.Reflection;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x0200027D RID: 637
	public class Indexer : PropertyBase, IParametersMember, IInterfaceMemberSpec
	{
		// Token: 0x06001F33 RID: 7987 RVA: 0x00099AAB File Offset: 0x00097CAB
		public Indexer(TypeDefinition parent, FullNamedExpression type, MemberName name, Modifiers mod, ParametersCompiled parameters, Attributes attrs) : base(parent, type, mod, (parent.PartialContainer.Kind == MemberKind.Interface) ? Modifiers.NEW : (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE), name, attrs)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00099ADD File Offset: 0x00097CDD
		AParametersCollection IParametersMember.Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x00099ADD File Offset: 0x00097CDD
		public ParametersCompiled ParameterInfo
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00099AE5 File Offset: 0x00097CE5
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00099AEE File Offset: 0x00097CEE
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.IndexerName)
			{
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00099B10 File Offset: 0x00097D10
		protected override bool CheckForDuplications()
		{
			return this.Parent.MemberCache.CheckExistingMembersOverloads(this, this.parameters);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00099B2C File Offset: 0x00097D2C
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			if (!base.DefineParameters(this.parameters))
			{
				return false;
			}
			if (base.OptAttributes != null)
			{
				Attribute attribute = base.OptAttributes.Search(this.Module.PredefinedAttributes.IndexerName);
				if (attribute != null)
				{
					TypeContainer typeContainer = attribute.Type.MemberDefinition as TypeContainer;
					if (typeContainer != null)
					{
						typeContainer.Define();
					}
					if (this.IsExplicitImpl)
					{
						base.Report.Error(415, attribute.Location, "The `{0}' attribute is valid only on an indexer that is not an explicit interface member declaration", attribute.Type.GetSignatureForError());
					}
					else if ((base.ModFlags & Modifiers.OVERRIDE) != (Modifiers)0)
					{
						base.Report.Error(609, attribute.Location, "Cannot set the `IndexerName' attribute on an indexer marked override");
					}
					else
					{
						string indexerAttributeValue = attribute.GetIndexerAttributeValue();
						if (!string.IsNullOrEmpty(indexerAttributeValue))
						{
							this.SetMemberName(new MemberName(base.MemberName.Left, indexerAttributeValue, base.Location));
						}
					}
				}
			}
			if (this.InterfaceType != null)
			{
				string attributeDefaultMember = this.InterfaceType.MemberDefinition.GetAttributeDefaultMember();
				if (attributeDefaultMember != base.ShortName)
				{
					this.SetMemberName(new MemberName(base.MemberName.Left, attributeDefaultMember, new TypeExpression(this.InterfaceType, base.Location), base.Location));
				}
			}
			this.Parent.AddNameToContainer(this, base.MemberName.Basename);
			this.flags |= (MethodAttributes.HideBySig | MethodAttributes.SpecialName);
			if (!base.DefineAccessors())
			{
				return false;
			}
			if (!this.CheckBase())
			{
				return false;
			}
			base.DefineBuilders(MemberKind.Indexer, this.parameters);
			return true;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00099CC3 File Offset: 0x00097EC3
		public override bool EnableOverloadChecks(MemberCore overload)
		{
			if (overload is Indexer)
			{
				this.caching_flags |= MemberCore.Flags.MethodOverloadsExist;
				return true;
			}
			return base.EnableOverloadChecks(overload);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00099CE8 File Offset: 0x00097EE8
		public override void Emit()
		{
			this.parameters.CheckConstraints(this);
			base.Emit();
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00099CFC File Offset: 0x00097EFC
		public override string GetSignatureForError()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Parent.GetSignatureForError());
			if (base.MemberName.ExplicitInterface != null)
			{
				stringBuilder.Append(".");
				stringBuilder.Append(base.MemberName.ExplicitInterface.GetSignatureForError());
			}
			stringBuilder.Append(".this");
			stringBuilder.Append(this.parameters.GetSignatureForError("[", "]", this.parameters.Count));
			return stringBuilder.ToString();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00099D83 File Offset: 0x00097F83
		public override string GetSignatureForDocumentation()
		{
			return base.GetSignatureForDocumentation() + this.parameters.GetSignatureForDocumentation();
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00099D9B File Offset: 0x00097F9B
		public override void PrepareEmit()
		{
			this.parameters.ResolveDefaultValues(this);
			base.PrepareEmit();
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00099DAF File Offset: 0x00097FAF
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			this.parameters.VerifyClsCompliance(this);
			return true;
		}

		// Token: 0x04000B77 RID: 2935
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE;

		// Token: 0x04000B78 RID: 2936
		private const Modifiers AllowedInterfaceModifiers = Modifiers.NEW;

		// Token: 0x04000B79 RID: 2937
		private readonly ParametersCompiled parameters;

		// Token: 0x020003E5 RID: 997
		public class GetIndexerMethod : PropertyBase.GetMethod, IParametersMember, IInterfaceMemberSpec
		{
			// Token: 0x060027C3 RID: 10179 RVA: 0x000BD06C File Offset: 0x000BB26C
			public GetIndexerMethod(PropertyBase property, Modifiers modifiers, ParametersCompiled parameters, Attributes attrs, Location loc) : base(property, modifiers, attrs, loc)
			{
				this.parameters = parameters;
			}

			// Token: 0x060027C4 RID: 10180 RVA: 0x000BD084 File Offset: 0x000BB284
			public override void Define(TypeContainer parent)
			{
				base.Report.DisableReporting();
				try
				{
					this.parameters.Resolve(this);
				}
				finally
				{
					base.Report.EnableReporting();
				}
				base.Define(parent);
			}

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000BD0D0 File Offset: 0x000BB2D0
			public override ParametersCompiled ParameterInfo
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000BD0D0 File Offset: 0x000BB2D0
			AParametersCollection IParametersMember.Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000BD0D8 File Offset: 0x000BB2D8
			TypeSpec IInterfaceMemberSpec.MemberType
			{
				get
				{
					return this.ReturnType;
				}
			}

			// Token: 0x04001114 RID: 4372
			private ParametersCompiled parameters;
		}

		// Token: 0x020003E6 RID: 998
		public class SetIndexerMethod : PropertyBase.SetMethod, IParametersMember, IInterfaceMemberSpec
		{
			// Token: 0x060027C8 RID: 10184 RVA: 0x000BD0E0 File Offset: 0x000BB2E0
			public SetIndexerMethod(PropertyBase property, Modifiers modifiers, ParametersCompiled parameters, Attributes attrs, Location loc) : base(property, modifiers, parameters, attrs, loc)
			{
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000BC60E File Offset: 0x000BA80E
			AParametersCollection IParametersMember.Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x060027CA RID: 10186 RVA: 0x000BD0D8 File Offset: 0x000BB2D8
			TypeSpec IInterfaceMemberSpec.MemberType
			{
				get
				{
					return this.ReturnType;
				}
			}
		}
	}
}
