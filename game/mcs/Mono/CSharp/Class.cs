using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200012F RID: 303
	public sealed class Class : ClassOrStruct
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x0003DA54 File Offset: 0x0003BC54
		public Class(TypeContainer parent, MemberName name, Modifiers mod, Attributes attrs) : base(parent, name, attrs, MemberKind.Class)
		{
			Modifiers def_access = base.IsTopLevel ? Modifiers.INTERNAL : Modifiers.PRIVATE;
			base.ModFlags = ModifiersExtensions.Check(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.UNSAFE, mod, def_access, base.Location, base.Report);
			this.spec = new TypeSpec(this.Kind, null, this, null, base.ModFlags);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003DAB5 File Offset: 0x0003BCB5
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003DAC0 File Offset: 0x0003BCC0
		public override void SetBaseTypes(List<FullNamedExpression> baseTypes)
		{
			MemberName memberName = base.MemberName;
			if (memberName.Name == "Object" && !memberName.IsGeneric && this.Parent.MemberName.Name == "System" && this.Parent.MemberName.Left == null)
			{
				base.Report.Error(537, base.Location, "The class System.Object cannot have a base class or implement an interface.");
			}
			base.SetBaseTypes(baseTypes);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003DB40 File Offset: 0x0003BD40
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.AttributeUsage && !base.BaseType.IsAttribute && this.spec.BuiltinType != BuiltinTypeSpec.Type.Attribute)
			{
				base.Report.Error(641, a.Location, "Attribute `{0}' is only valid on classes derived from System.Attribute", a.GetSignatureForError());
			}
			if (a.Type == pa.Conditional && !base.BaseType.IsAttribute)
			{
				base.Report.Error(1689, a.Location, "Attribute `System.Diagnostics.ConditionalAttribute' is only valid on methods or attribute classes");
				return;
			}
			if (a.Type == pa.ComImport && !this.attributes.Contains(pa.Guid))
			{
				a.Error_MissingGuidAttribute();
				return;
			}
			if (a.Type == pa.Extension)
			{
				a.Error_MisusedExtensionAttribute();
				return;
			}
			if (a.Type.IsConditionallyExcluded(this))
			{
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00005836 File Offset: 0x00003A36
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Class;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003DC40 File Offset: 0x0003BE40
		protected override bool DoDefineMembers()
		{
			if ((base.ModFlags & Modifiers.ABSTRACT) == Modifiers.ABSTRACT && (base.ModFlags & (Modifiers.SEALED | Modifiers.STATIC)) != (Modifiers)0)
			{
				base.Report.Error(418, base.Location, "`{0}': an abstract class cannot be sealed or static", this.GetSignatureForError());
			}
			if ((base.ModFlags & (Modifiers.SEALED | Modifiers.STATIC)) == (Modifiers.SEALED | Modifiers.STATIC))
			{
				base.Report.Error(441, base.Location, "`{0}': a class cannot be both static and sealed", this.GetSignatureForError());
			}
			if (base.IsStatic)
			{
				if (base.PrimaryConstructorParameters != null)
				{
					base.Report.Error(-800, base.Location, "`{0}': Static classes cannot have primary constructor", this.GetSignatureForError());
					base.PrimaryConstructorParameters = null;
				}
				using (List<MemberCore>.Enumerator enumerator = base.Members.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MemberCore memberCore = enumerator.Current;
						if (memberCore is Operator)
						{
							base.Report.Error(715, memberCore.Location, "`{0}': Static classes cannot contain user-defined operators", memberCore.GetSignatureForError());
						}
						else if (memberCore is Destructor)
						{
							base.Report.Error(711, memberCore.Location, "`{0}': Static classes cannot contain destructor", this.GetSignatureForError());
						}
						else if (memberCore is Indexer)
						{
							base.Report.Error(720, memberCore.Location, "`{0}': cannot declare indexers in a static class", memberCore.GetSignatureForError());
						}
						else if ((memberCore.ModFlags & Modifiers.STATIC) == (Modifiers)0 && !(memberCore is TypeContainer))
						{
							if (memberCore is Constructor)
							{
								base.Report.Error(710, memberCore.Location, "`{0}': Static classes cannot have instance constructors", this.GetSignatureForError());
							}
							else
							{
								base.Report.Error(708, memberCore.Location, "`{0}': cannot declare instance members in a static class", memberCore.GetSignatureForError());
							}
						}
					}
					goto IL_1EB;
				}
			}
			if (!base.PartialContainer.HasInstanceConstructor || base.PrimaryConstructorParameters != null)
			{
				this.generated_primary_constructor = this.DefineDefaultConstructor(false);
			}
			IL_1EB:
			return base.DoDefineMembers();
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003DE5C File Offset: 0x0003C05C
		public override void Emit()
		{
			base.Emit();
			if ((base.ModFlags & Modifiers.METHOD_EXTENSION) != (Modifiers)0)
			{
				this.Module.PredefinedAttributes.Extension.EmitAttribute(this.TypeBuilder);
			}
			if (this.base_type != null && this.base_type.HasDynamicElement)
			{
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.TypeBuilder, this.base_type, base.Location);
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003DED4 File Offset: 0x0003C0D4
		public override void GetCompletionStartingWith(string prefix, List<string> results)
		{
			base.GetCompletionStartingWith(prefix, results);
			for (TypeSpec typeSpec = this.base_type; typeSpec != null; typeSpec = typeSpec.BaseType)
			{
				results.AddRange(from l in MemberCache.GetCompletitionMembers(this, typeSpec, prefix)
				where l.IsStatic
				select l.Name);
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003DF54 File Offset: 0x0003C154
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			TypeSpec[] array = base.ResolveBaseTypes(out base_class);
			if (base_class == null)
			{
				if (this.spec.BuiltinType != BuiltinTypeSpec.Type.Object)
				{
					this.base_type = this.Compiler.BuiltinTypes.Object;
				}
			}
			else
			{
				if (this.base_type.IsGenericParameter)
				{
					base.Report.Error(689, base_class.Location, "`{0}': Cannot derive from type parameter `{1}'", this.GetSignatureForError(), this.base_type.GetSignatureForError());
				}
				else if (this.base_type.IsStatic)
				{
					base.Report.SymbolRelatedToPreviousError(this.base_type);
					base.Report.Error(709, base.Location, "`{0}': Cannot derive from static class `{1}'", this.GetSignatureForError(), this.base_type.GetSignatureForError());
				}
				else if (this.base_type.IsSealed)
				{
					base.Report.SymbolRelatedToPreviousError(this.base_type);
					base.Report.Error(509, base.Location, "`{0}': cannot derive from sealed type `{1}'", this.GetSignatureForError(), this.base_type.GetSignatureForError());
				}
				else if (base.PartialContainer.IsStatic && this.base_type.BuiltinType != BuiltinTypeSpec.Type.Object)
				{
					base.Report.Error(713, base.Location, "Static class `{0}' cannot derive from type `{1}'. Static classes must derive from object", this.GetSignatureForError(), this.base_type.GetSignatureForError());
				}
				switch (this.base_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.ValueType:
				case BuiltinTypeSpec.Type.Enum:
				case BuiltinTypeSpec.Type.Delegate:
				case BuiltinTypeSpec.Type.MulticastDelegate:
				case BuiltinTypeSpec.Type.Array:
					if (!(this.spec is BuiltinTypeSpec))
					{
						base.Report.Error(644, base.Location, "`{0}' cannot derive from special class `{1}'", this.GetSignatureForError(), this.base_type.GetSignatureForError());
						this.base_type = this.Compiler.BuiltinTypes.Object;
					}
					break;
				}
				if (!base.IsAccessibleAs(this.base_type))
				{
					base.Report.SymbolRelatedToPreviousError(this.base_type);
					base.Report.Error(60, base.Location, "Inconsistent accessibility: base class `{0}' is less accessible than class `{1}'", this.base_type.GetSignatureForError(), this.GetSignatureForError());
				}
			}
			if (base.PartialContainer.IsStatic && array != null)
			{
				foreach (TypeSpec ms in array)
				{
					base.Report.SymbolRelatedToPreviousError(ms);
				}
				base.Report.Error(714, base.Location, "Static class `{0}' cannot implement interfaces", this.GetSignatureForError());
			}
			return array;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003E1D4 File Offset: 0x0003C3D4
		public override string[] ConditionalConditions()
		{
			if ((this.caching_flags & (MemberCore.Flags.Excluded_Undetected | MemberCore.Flags.Excluded)) == (MemberCore.Flags)0)
			{
				return null;
			}
			this.caching_flags &= ~MemberCore.Flags.Excluded_Undetected;
			if (base.OptAttributes == null)
			{
				return null;
			}
			Attribute[] array = base.OptAttributes.SearchMulti(this.Module.PredefinedAttributes.Conditional);
			if (array == null)
			{
				return null;
			}
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i].GetConditionalAttributeValue();
			}
			this.caching_flags |= MemberCore.Flags.Excluded;
			return array2;
		}

		// Token: 0x040006FB RID: 1787
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.UNSAFE;

		// Token: 0x02000387 RID: 903
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060026B4 RID: 9908 RVA: 0x000B6E5F File Offset: 0x000B505F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060026B5 RID: 9909 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x060026B6 RID: 9910 RVA: 0x000B6E6B File Offset: 0x000B506B
			internal bool <GetCompletionStartingWith>b__9_0(MemberSpec l)
			{
				return l.IsStatic;
			}

			// Token: 0x060026B7 RID: 9911 RVA: 0x000B6E73 File Offset: 0x000B5073
			internal string <GetCompletionStartingWith>b__9_1(MemberSpec l)
			{
				return l.Name;
			}

			// Token: 0x04000F57 RID: 3927
			public static readonly Class.<>c <>9 = new Class.<>c();

			// Token: 0x04000F58 RID: 3928
			public static Func<MemberSpec, bool> <>9__9_0;

			// Token: 0x04000F59 RID: 3929
			public static Func<MemberSpec, string> <>9__9_1;
		}
	}
}
