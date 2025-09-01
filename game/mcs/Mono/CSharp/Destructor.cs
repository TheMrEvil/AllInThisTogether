using System;

namespace Mono.CSharp
{
	// Token: 0x02000254 RID: 596
	public class Destructor : MethodOrOperator
	{
		// Token: 0x06001D8B RID: 7563 RVA: 0x00090493 File Offset: 0x0008E693
		public Destructor(TypeDefinition parent, Modifiers mod, ParametersCompiled parameters, Attributes attrs, Location l) : base(parent, null, mod, Modifiers.AllowedExplicitImplFlags, new MemberName(Destructor.MetadataName, l), attrs, parameters)
		{
			base.ModFlags &= ~Modifiers.PRIVATE;
			base.ModFlags |= (Modifiers.PROTECTED | Modifiers.OVERRIDE);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000904D3 File Offset: 0x0008E6D3
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000904DC File Offset: 0x0008E6DC
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.Conditional)
			{
				base.Error_ConditionalAttributeIsNotValid();
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00090504 File Offset: 0x0008E704
		protected override bool CheckBase()
		{
			if ((this.caching_flags & MemberCore.Flags.MethodOverloadsExist) != (MemberCore.Flags)0)
			{
				this.CheckForDuplications();
			}
			return true;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0009051C File Offset: 0x0008E71C
		public override bool Define()
		{
			base.Define();
			if (this.Compiler.Settings.WriteMetadataOnly)
			{
				this.block = null;
			}
			return true;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00090540 File Offset: 0x0008E740
		public override void Emit()
		{
			TypeSpec baseType = this.Parent.PartialContainer.BaseType;
			if (baseType != null && base.Block != null)
			{
				MethodSpec methodSpec = MemberCache.FindMember(baseType, new MemberFilter(Destructor.MetadataName, 0, MemberKind.Destructor, null, null), BindingRestriction.InstanceOnly) as MethodSpec;
				if (methodSpec == null)
				{
					throw new NotImplementedException();
				}
				MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(methodSpec, baseType, base.Location);
				methodGroupExpr.InstanceExpression = new BaseThis(baseType, base.Location);
				ExplicitBlock explicitBlock = new ExplicitBlock(this.block, this.block.StartLocation, this.block.EndLocation)
				{
					IsCompilerGenerated = true
				};
				ExplicitBlock explicitBlock2 = new ExplicitBlock(this.block, base.Location, base.Location)
				{
					IsCompilerGenerated = true
				};
				explicitBlock2.AddStatement(new StatementExpression(new Invocation(methodGroupExpr, new Arguments(0)), Location.Null));
				TryFinally tf = new TryFinally(explicitBlock, explicitBlock2, base.Location);
				this.block.WrapIntoDestructor(tf, explicitBlock);
			}
			base.Emit();
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0009063C File Offset: 0x0008E83C
		public override string GetSignatureForError()
		{
			return this.Parent.GetSignatureForError() + ".~" + this.Parent.MemberName.Name + "()";
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00090668 File Offset: 0x0008E868
		protected override bool ResolveMemberType()
		{
			this.member_type = this.Compiler.BuiltinTypes.Void;
			return true;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x00090681 File Offset: 0x0008E881
		public override string[] ValidAttributeTargets
		{
			get
			{
				return Destructor.attribute_targets;
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00090688 File Offset: 0x0008E888
		// Note: this type is marked as 'beforefieldinit'.
		static Destructor()
		{
		}

		// Token: 0x04000AEC RID: 2796
		private const Modifiers AllowedModifiers = Modifiers.AllowedExplicitImplFlags;

		// Token: 0x04000AED RID: 2797
		private static readonly string[] attribute_targets = new string[]
		{
			"method"
		};

		// Token: 0x04000AEE RID: 2798
		public static readonly string MetadataName = "Finalize";
	}
}
