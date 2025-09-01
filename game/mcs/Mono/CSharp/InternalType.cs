using System;

namespace Mono.CSharp
{
	// Token: 0x020002E2 RID: 738
	internal class InternalType : TypeSpec, ITypeDefinition, IMemberDefinition
	{
		// Token: 0x06002325 RID: 8997 RVA: 0x000AC91E File Offset: 0x000AAB1E
		private InternalType(string name) : base(MemberKind.InternalCompilerType, null, null, null, Modifiers.PUBLIC)
		{
			this.name = name;
			this.definition = this;
			this.cache = MemberCache.Empty;
			this.state = ((this.state & ~(MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.MissingDependency_Undetected)) | MemberSpec.StateFlags.CLSCompliant);
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000022F4 File Offset: 0x000004F4
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00023DF4 File Offset: 0x00021FF4
		IAssemblyDefinition ITypeDefinition.DeclaringAssembly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x000022F4 File Offset: 0x000004F4
		bool IMemberDefinition.IsImported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x000AC959 File Offset: 0x000AAB59
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000055E7 File Offset: 0x000037E7
		string ITypeDefinition.Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x000022F4 File Offset: 0x000004F4
		int ITypeDefinition.TypeParametersCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000055E7 File Offset: 0x000037E7
		TypeParameterSpec[] ITypeDefinition.TypeParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000AC959 File Offset: 0x000AAB59
		public override string GetSignatureForError()
		{
			return this.name;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000055E7 File Offset: 0x000037E7
		TypeSpec ITypeDefinition.GetAttributeCoClass()
		{
			return null;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000055E7 File Offset: 0x000037E7
		string ITypeDefinition.GetAttributeDefaultMember()
		{
			return null;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000055E7 File Offset: 0x000037E7
		AttributeUsageAttribute ITypeDefinition.GetAttributeUsage(PredefinedAttribute pa)
		{
			return null;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00023DF4 File Offset: 0x00021FF4
		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00023DF4 File Offset: 0x00021FF4
		void ITypeDefinition.LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000055E7 File Offset: 0x000037E7
		string[] IMemberDefinition.ConditionalConditions()
		{
			return null;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000055E7 File Offset: 0x000037E7
		ObsoleteAttribute IMemberDefinition.GetAttributeObsolete()
		{
			return null;
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000AC964 File Offset: 0x000AAB64
		bool? IMemberDefinition.CLSAttributeValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0000AF70 File Offset: 0x00009170
		void IMemberDefinition.SetIsAssigned()
		{
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0000AF70 File Offset: 0x00009170
		void IMemberDefinition.SetIsUsed()
		{
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000AC97C File Offset: 0x000AAB7C
		// Note: this type is marked as 'beforefieldinit'.
		static InternalType()
		{
		}

		// Token: 0x04000D71 RID: 3441
		public static readonly InternalType AnonymousMethod = new InternalType("anonymous method");

		// Token: 0x04000D72 RID: 3442
		public static readonly InternalType Arglist = new InternalType("__arglist");

		// Token: 0x04000D73 RID: 3443
		public static readonly InternalType MethodGroup = new InternalType("method group");

		// Token: 0x04000D74 RID: 3444
		public static readonly InternalType NullLiteral = new InternalType("null");

		// Token: 0x04000D75 RID: 3445
		public static readonly InternalType FakeInternalType = new InternalType("<fake$type>");

		// Token: 0x04000D76 RID: 3446
		public static readonly InternalType Namespace = new InternalType("<namespace>");

		// Token: 0x04000D77 RID: 3447
		public static readonly InternalType ErrorType = new InternalType("<error>");

		// Token: 0x04000D78 RID: 3448
		public static readonly InternalType VarOutType = new InternalType("var out");

		// Token: 0x04000D79 RID: 3449
		private readonly string name;
	}
}
