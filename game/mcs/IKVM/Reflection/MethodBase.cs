using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000039 RID: 57
	public abstract class MethodBase : MemberInfo
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00005501 File Offset: 0x00003701
		internal MethodBase()
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000215 RID: 533
		internal abstract MethodSignature MethodSignature { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000216 RID: 534
		internal abstract int ParameterCount { get; }

		// Token: 0x06000217 RID: 535
		public abstract ParameterInfo[] GetParameters();

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000218 RID: 536
		public abstract MethodAttributes Attributes { get; }

		// Token: 0x06000219 RID: 537
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x0600021A RID: 538
		public abstract MethodBody GetMethodBody();

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600021B RID: 539
		public abstract CallingConventions CallingConvention { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600021C RID: 540
		public abstract int __MethodRVA { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public bool IsConstructor
		{
			get
			{
				if ((this.Attributes & MethodAttributes.RTSpecialName) != MethodAttributes.PrivateScope)
				{
					string name = this.Name;
					return name == ConstructorInfo.ConstructorName || name == ConstructorInfo.TypeConstructorName;
				}
				return false;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008B12 File Offset: 0x00006D12
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008B20 File Offset: 0x00006D20
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008B2E File Offset: 0x00006D2E
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008B3F File Offset: 0x00006D3F
		public bool IsFinal
		{
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008B4D File Offset: 0x00006D4D
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008B5A File Offset: 0x00006D5A
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008B67 File Offset: 0x00006D67
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008B74 File Offset: 0x00006D74
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008B81 File Offset: 0x00006D81
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00008B8E File Offset: 0x00006D8E
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008B9B File Offset: 0x00006D9B
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00008BAC File Offset: 0x00006DAC
		public bool IsHideBySig
		{
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008BBD File Offset: 0x00006DBD
		public MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008BC5 File Offset: 0x00006DC5
		public virtual Type[] GetGenericArguments()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008BCC File Offset: 0x00006DCC
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return this.IsGenericMethodDefinition;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005936 File Offset: 0x00003B36
		public virtual MethodBase __GetMethodOnTypeDefinition()
		{
			return this;
		}

		// Token: 0x06000230 RID: 560
		internal abstract MethodInfo GetMethodOnTypeDefinition();

		// Token: 0x06000231 RID: 561
		internal abstract int ImportTo(ModuleBuilder module);

		// Token: 0x06000232 RID: 562
		internal abstract MethodBase BindTypeParameters(Type type);

		// Token: 0x06000233 RID: 563 RVA: 0x00008BD4 File Offset: 0x00006DD4
		internal sealed override bool BindingFlagsMatch(BindingFlags flags)
		{
			return MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static, BindingFlags.Instance);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008BF8 File Offset: 0x00006DF8
		internal sealed override bool BindingFlagsMatchInherited(BindingFlags flags)
		{
			return (this.Attributes & MethodAttributes.MemberAccessMask) > MethodAttributes.Private && MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static | BindingFlags.FlattenHierarchy, BindingFlags.Instance);
		}
	}
}
