using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000038 RID: 56
	public abstract class MemberInfo : ICustomAttributeProvider
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00002CCC File Offset: 0x00000ECC
		internal MemberInfo()
		{
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001FE RID: 510
		public abstract string Name { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001FF RID: 511
		public abstract Type DeclaringType { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000200 RID: 512
		public abstract MemberTypes MemberType { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000089FD File Offset: 0x00006BFD
		public virtual Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x06000202 RID: 514
		internal abstract MemberInfo SetReflectedType(Type type);

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int MetadataToken
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000204 RID: 516
		public abstract Module Module { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool __IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008A05 File Offset: 0x00006C05
		public bool IsDefined(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit).Count != 0;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008A17 File Offset: 0x00006C17
		public IList<CustomAttributeData> __GetCustomAttributes(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008A21 File Offset: 0x00006C21
		public IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008A29 File Offset: 0x00006C29
		public IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008A31 File Offset: 0x00006C31
		public static bool operator ==(MemberInfo m1, MemberInfo m2)
		{
			return m1 == m2 || (m1 != null && m1.Equals(m2));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008A45 File Offset: 0x00006C45
		public static bool operator !=(MemberInfo m1, MemberInfo m2)
		{
			return !(m1 == m2);
		}

		// Token: 0x0600020C RID: 524
		internal abstract int GetCurrentToken();

		// Token: 0x0600020D RID: 525
		internal abstract List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType);

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600020E RID: 526
		internal abstract bool IsBaked { get; }

		// Token: 0x0600020F RID: 527 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual bool BindingFlagsMatch(BindingFlags flags)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual bool BindingFlagsMatchInherited(BindingFlags flags)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008A51 File Offset: 0x00006C51
		protected static bool BindingFlagsMatch(bool state, BindingFlags flags, BindingFlags trueFlag, BindingFlags falseFlag)
		{
			return (state && (flags & trueFlag) == trueFlag) || (!state && (flags & falseFlag) == falseFlag);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008A6C File Offset: 0x00006C6C
		protected static T SetReflectedType<T>(T member, Type type) where T : MemberInfo
		{
			if (!(member == null))
			{
				return (T)((object)member.SetReflectedType(type));
			}
			return default(T);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008AA4 File Offset: 0x00006CA4
		protected static T[] SetReflectedType<T>(T[] members, Type type) where T : MemberInfo
		{
			for (int i = 0; i < members.Length; i++)
			{
				members[i] = MemberInfo.SetReflectedType<T>(members[i], type);
			}
			return members;
		}
	}
}
