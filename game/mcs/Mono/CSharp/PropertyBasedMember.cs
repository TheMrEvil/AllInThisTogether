using System;

namespace Mono.CSharp
{
	// Token: 0x02000275 RID: 629
	public abstract class PropertyBasedMember : InterfaceMemberBase
	{
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0009826C File Offset: 0x0009646C
		protected PropertyBasedMember(TypeDefinition parent, FullNamedExpression type, Modifiers mod, Modifiers allowed_mod, MemberName name, Attributes attrs) : base(parent, type, mod, allowed_mod, name, attrs)
		{
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00098280 File Offset: 0x00096480
		protected void CheckReservedNameConflict(string prefix, MethodSpec accessor)
		{
			string text;
			AParametersCollection aparametersCollection;
			if (accessor != null)
			{
				text = accessor.Name;
				aparametersCollection = accessor.Parameters;
			}
			else
			{
				text = prefix + base.ShortName;
				if (this.IsExplicitImpl)
				{
					text = base.MemberName.Left + "." + text;
				}
				if (this is Indexer)
				{
					aparametersCollection = ((Indexer)this).ParameterInfo;
					if (prefix[0] == 's')
					{
						IParameterData[] array = new IParameterData[aparametersCollection.Count + 1];
						Array.Copy(aparametersCollection.FixedParameters, array, array.Length - 1);
						IParameterData[] array2 = array;
						array2[array2.Length - 1] = new ParameterData("value", Parameter.Modifier.NONE);
						TypeSpec[] array3 = new TypeSpec[array.Length];
						Array.Copy(aparametersCollection.Types, array3, array.Length - 1);
						array3[array.Length - 1] = this.member_type;
						aparametersCollection = new ParametersImported(array, array3, false);
					}
				}
				else if (prefix[0] == 's')
				{
					aparametersCollection = ParametersCompiled.CreateFullyResolved(new TypeSpec[]
					{
						this.member_type
					});
				}
				else
				{
					aparametersCollection = ParametersCompiled.EmptyReadOnlyParameters;
				}
			}
			MemberSpec memberSpec = MemberCache.FindMember(this.Parent.Definition, new MemberFilter(text, 0, MemberKind.Method, aparametersCollection, null), BindingRestriction.DeclaredOnly | BindingRestriction.NoAccessors);
			if (memberSpec != null)
			{
				base.Report.SymbolRelatedToPreviousError(memberSpec);
				base.Report.Error(82, base.Location, "A member `{0}' is already reserved", memberSpec.GetSignatureForError());
			}
		}

		// Token: 0x06001ED8 RID: 7896
		public abstract void PrepareEmit();

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000983CD File Offset: 0x000965CD
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			if (!base.MemberType.IsCLSCompliant())
			{
				base.Report.Warning(3003, 1, base.Location, "Type of `{0}' is not CLS-compliant", this.GetSignatureForError());
			}
			return true;
		}
	}
}
