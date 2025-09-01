using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200018E RID: 398
	public abstract class MemberSpec
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x000676B3 File Offset: 0x000658B3
		protected MemberSpec(MemberKind kind, TypeSpec declaringType, IMemberDefinition definition, Modifiers modifiers)
		{
			this.Kind = kind;
			this.declaringType = declaringType;
			this.definition = definition;
			this.modifiers = modifiers;
			if (kind == MemberKind.MissingType)
			{
				this.state = MemberSpec.StateFlags.MissingDependency;
				return;
			}
			this.state = (MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.MissingDependency_Undetected);
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x000676F1 File Offset: 0x000658F1
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x000676F9 File Offset: 0x000658F9
		public TypeSpec DeclaringType
		{
			get
			{
				return this.declaringType;
			}
			set
			{
				this.declaringType = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00067702 File Offset: 0x00065902
		public IMemberDefinition MemberDefinition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0006770A File Offset: 0x0006590A
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x00067712 File Offset: 0x00065912
		public Modifiers Modifiers
		{
			get
			{
				return this.modifiers;
			}
			set
			{
				this.modifiers = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0006771B File Offset: 0x0006591B
		public virtual string Name
		{
			get
			{
				return this.definition.Name;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x00067728 File Offset: 0x00065928
		public bool IsAbstract
		{
			get
			{
				return (this.modifiers & Modifiers.ABSTRACT) > (Modifiers)0;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00067736 File Offset: 0x00065936
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x00067747 File Offset: 0x00065947
		public bool IsAccessor
		{
			get
			{
				return (this.state & MemberSpec.StateFlags.IsAccessor) > (MemberSpec.StateFlags)0;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.IsAccessor) : (this.state & ~MemberSpec.StateFlags.IsAccessor));
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0006776C File Offset: 0x0006596C
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x0006777D File Offset: 0x0006597D
		public bool IsGeneric
		{
			get
			{
				return (this.state & MemberSpec.StateFlags.IsGeneric) > (MemberSpec.StateFlags)0;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.IsGeneric) : (this.state & ~MemberSpec.StateFlags.IsGeneric));
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x000677A2 File Offset: 0x000659A2
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x000677B3 File Offset: 0x000659B3
		public bool IsNotCSharpCompatible
		{
			get
			{
				return (this.state & MemberSpec.StateFlags.IsNotCSharpCompatible) > (MemberSpec.StateFlags)0;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.IsNotCSharpCompatible) : (this.state & ~MemberSpec.StateFlags.IsNotCSharpCompatible));
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000677D8 File Offset: 0x000659D8
		public bool IsPrivate
		{
			get
			{
				return (this.modifiers & Modifiers.PRIVATE) > (Modifiers)0;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x000677E5 File Offset: 0x000659E5
		public bool IsPublic
		{
			get
			{
				return (this.modifiers & Modifiers.PUBLIC) > (Modifiers)0;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000677F2 File Offset: 0x000659F2
		public bool IsStatic
		{
			get
			{
				return (this.modifiers & Modifiers.STATIC) > (Modifiers)0;
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00067803 File Offset: 0x00065A03
		public virtual ObsoleteAttribute GetAttributeObsolete()
		{
			if ((this.state & (MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.Obsolete)) == (MemberSpec.StateFlags)0)
			{
				return null;
			}
			this.state &= ~MemberSpec.StateFlags.Obsolete_Undetected;
			ObsoleteAttribute attributeObsolete = this.definition.GetAttributeObsolete();
			if (attributeObsolete != null)
			{
				this.state |= MemberSpec.StateFlags.Obsolete;
			}
			return attributeObsolete;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0006783C File Offset: 0x00065A3C
		public List<MissingTypeSpecReference> GetMissingDependencies()
		{
			return this.GetMissingDependencies(this);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00067848 File Offset: 0x00065A48
		public List<MissingTypeSpecReference> GetMissingDependencies(MemberSpec caller)
		{
			if ((this.state & (MemberSpec.StateFlags.MissingDependency_Undetected | MemberSpec.StateFlags.MissingDependency)) == (MemberSpec.StateFlags)0)
			{
				return null;
			}
			this.state &= ~MemberSpec.StateFlags.MissingDependency_Undetected;
			List<MissingTypeSpecReference> list;
			if (this.definition is ImportedDefinition)
			{
				list = this.ResolveMissingDependencies(caller);
			}
			else if (this is ElementTypeSpec)
			{
				list = ((ElementTypeSpec)this).Element.GetMissingDependencies(caller);
			}
			else
			{
				list = null;
			}
			if (list != null)
			{
				this.state |= MemberSpec.StateFlags.MissingDependency;
			}
			return list;
		}

		// Token: 0x060015A4 RID: 5540
		public abstract List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller);

		// Token: 0x060015A5 RID: 5541 RVA: 0x000678BC File Offset: 0x00065ABC
		protected virtual bool IsNotCLSCompliant(out bool attrValue)
		{
			bool? clsattributeValue = this.MemberDefinition.CLSAttributeValue;
			attrValue = (clsattributeValue ?? false);
			return clsattributeValue == false;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00067903 File Offset: 0x00065B03
		public virtual string GetSignatureForDocumentation()
		{
			return this.DeclaringType.GetSignatureForDocumentation() + "." + this.Name;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00067920 File Offset: 0x00065B20
		public virtual string GetSignatureForError()
		{
			Property.BackingFieldDeclaration backingFieldDeclaration = this.MemberDefinition as Property.BackingFieldDeclaration;
			string name;
			if (backingFieldDeclaration == null)
			{
				name = this.Name;
			}
			else
			{
				name = backingFieldDeclaration.OriginalProperty.MemberName.Name;
			}
			return this.DeclaringType.GetSignatureForError() + "." + name;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0006796C File Offset: 0x00065B6C
		public virtual MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			MemberSpec memberSpec = (MemberSpec)base.MemberwiseClone();
			memberSpec.declaringType = inflator.TypeInstance;
			if (this.DeclaringType.IsGenericOrParentIsGeneric)
			{
				memberSpec.state |= MemberSpec.StateFlags.PendingMetaInflate;
			}
			return memberSpec;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000679B4 File Offset: 0x00065BB4
		public bool IsAccessible(IMemberContext ctx)
		{
			Modifiers modifiers = this.Modifiers & Modifiers.AccessibilityMask;
			if (modifiers == Modifiers.PUBLIC)
			{
				return true;
			}
			TypeSpec typeSpec = this.DeclaringType;
			TypeSpec currentType = ctx.CurrentType;
			if (modifiers == Modifiers.PRIVATE)
			{
				return currentType != null && typeSpec != null && (typeSpec.MemberDefinition == currentType.MemberDefinition || TypeManager.IsNestedChildOf(currentType, typeSpec.MemberDefinition));
			}
			if ((modifiers & Modifiers.INTERNAL) != (Modifiers)0)
			{
				IAssemblyDefinition assemblyDefinition;
				if (currentType != null)
				{
					assemblyDefinition = currentType.MemberDefinition.DeclaringAssembly;
				}
				else
				{
					IAssemblyDefinition declaringAssembly = ctx.Module.DeclaringAssembly;
					assemblyDefinition = declaringAssembly;
				}
				IAssemblyDefinition assembly = assemblyDefinition;
				bool flag;
				if (typeSpec == null)
				{
					flag = ((ITypeDefinition)this.MemberDefinition).IsInternalAsPublic(assembly);
				}
				else
				{
					flag = this.DeclaringType.MemberDefinition.IsInternalAsPublic(assembly);
				}
				if (flag || modifiers == Modifiers.INTERNAL)
				{
					return flag;
				}
			}
			while (currentType != null)
			{
				if (TypeManager.IsFamilyAccessible(currentType, typeSpec))
				{
					return true;
				}
				currentType = currentType.DeclaringType;
			}
			return false;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00067A78 File Offset: 0x00065C78
		public bool IsCLSCompliant()
		{
			if ((this.state & MemberSpec.StateFlags.CLSCompliant_Undetected) != (MemberSpec.StateFlags)0)
			{
				this.state &= ~MemberSpec.StateFlags.CLSCompliant_Undetected;
				bool flag;
				if (this.IsNotCLSCompliant(out flag))
				{
					return false;
				}
				if (!flag)
				{
					if (this.DeclaringType != null)
					{
						flag = this.DeclaringType.IsCLSCompliant();
					}
					else
					{
						flag = ((ITypeDefinition)this.MemberDefinition).DeclaringAssembly.IsCLSCompliant;
					}
				}
				if (flag)
				{
					this.state |= MemberSpec.StateFlags.CLSCompliant;
				}
			}
			return (this.state & MemberSpec.StateFlags.CLSCompliant) > (MemberSpec.StateFlags)0;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00067AF8 File Offset: 0x00065CF8
		public bool IsConditionallyExcluded(IMemberContext ctx)
		{
			if ((this.Kind & (MemberKind.Method | MemberKind.Class)) == (MemberKind)0)
			{
				return false;
			}
			string[] array = this.MemberDefinition.ConditionalConditions();
			if (array == null)
			{
				return false;
			}
			MemberCore memberCore = ctx.CurrentMemberDefinition;
			CompilationSourceFile compilationSourceFile = null;
			while (memberCore != null && compilationSourceFile == null)
			{
				compilationSourceFile = (memberCore as CompilationSourceFile);
				memberCore = memberCore.Parent;
			}
			if (compilationSourceFile != null)
			{
				foreach (string value in array)
				{
					if (compilationSourceFile.IsConditionalDefined(value))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00067B6F File Offset: 0x00065D6F
		public override string ToString()
		{
			return this.GetSignatureForError();
		}

		// Token: 0x0400090C RID: 2316
		protected const MemberSpec.StateFlags SharedStateFlags = MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.Obsolete | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.CLSCompliant | MemberSpec.StateFlags.MissingDependency_Undetected | MemberSpec.StateFlags.MissingDependency | MemberSpec.StateFlags.HasDynamicElement;

		// Token: 0x0400090D RID: 2317
		protected Modifiers modifiers;

		// Token: 0x0400090E RID: 2318
		public MemberSpec.StateFlags state;

		// Token: 0x0400090F RID: 2319
		protected IMemberDefinition definition;

		// Token: 0x04000910 RID: 2320
		public readonly MemberKind Kind;

		// Token: 0x04000911 RID: 2321
		protected TypeSpec declaringType;

		// Token: 0x020003A2 RID: 930
		[Flags]
		public enum StateFlags
		{
			// Token: 0x04000FEC RID: 4076
			Obsolete_Undetected = 1,
			// Token: 0x04000FED RID: 4077
			Obsolete = 2,
			// Token: 0x04000FEE RID: 4078
			CLSCompliant_Undetected = 4,
			// Token: 0x04000FEF RID: 4079
			CLSCompliant = 8,
			// Token: 0x04000FF0 RID: 4080
			MissingDependency_Undetected = 16,
			// Token: 0x04000FF1 RID: 4081
			MissingDependency = 32,
			// Token: 0x04000FF2 RID: 4082
			HasDynamicElement = 64,
			// Token: 0x04000FF3 RID: 4083
			ConstraintsChecked = 128,
			// Token: 0x04000FF4 RID: 4084
			IsAccessor = 512,
			// Token: 0x04000FF5 RID: 4085
			IsGeneric = 1024,
			// Token: 0x04000FF6 RID: 4086
			PendingMetaInflate = 4096,
			// Token: 0x04000FF7 RID: 4087
			PendingMakeMethod = 8192,
			// Token: 0x04000FF8 RID: 4088
			PendingMemberCacheMembers = 16384,
			// Token: 0x04000FF9 RID: 4089
			PendingBaseTypeInflate = 32768,
			// Token: 0x04000FFA RID: 4090
			InterfacesExpanded = 65536,
			// Token: 0x04000FFB RID: 4091
			IsNotCSharpCompatible = 131072,
			// Token: 0x04000FFC RID: 4092
			SpecialRuntimeType = 262144,
			// Token: 0x04000FFD RID: 4093
			InflatedExpressionType = 524288,
			// Token: 0x04000FFE RID: 4094
			InflatedNullableType = 1048576,
			// Token: 0x04000FFF RID: 4095
			GenericIterateInterface = 2097152,
			// Token: 0x04001000 RID: 4096
			GenericTask = 4194304,
			// Token: 0x04001001 RID: 4097
			InterfacesImported = 8388608
		}
	}
}
