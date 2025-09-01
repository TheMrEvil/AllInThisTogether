using System;

namespace IKVM.Reflection
{
	// Token: 0x0200004A RID: 74
	internal sealed class MissingProperty : PropertyInfo
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000A6AA File Offset: 0x000088AA
		internal MissingProperty(Type declaringType, string name, PropertySignature signature)
		{
			this.declaringType = declaringType;
			this.name = name;
			this.signature = signature;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00009FBA File Offset: 0x000081BA
		public override PropertyAttributes Attributes
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00009FBA File Offset: 0x000081BA
		public override bool CanRead
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00009FBA File Offset: 0x000081BA
		public override bool CanWrite
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009FBA File Offset: 0x000081BA
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009FBA File Offset: 0x000081BA
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009FBA File Offset: 0x000081BA
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009FBA File Offset: 0x000081BA
		public override object GetRawConstantValue()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00009FBA File Offset: 0x000081BA
		internal override bool IsPublic
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00009FBA File Offset: 0x000081BA
		internal override bool IsNonPrivate
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009FBA File Offset: 0x000081BA
		internal override bool IsStatic
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A6C7 File Offset: 0x000088C7
		internal override PropertySignature PropertySignature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000A6CF File Offset: 0x000088CF
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A6D7 File Offset: 0x000088D7
		public override Type DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A6DF File Offset: 0x000088DF
		public override Module Module
		{
			get
			{
				return this.declaringType.Module;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A6EC File Offset: 0x000088EC
		internal override bool IsBaked
		{
			get
			{
				return this.declaringType.IsBaked;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009FBA File Offset: 0x000081BA
		internal override int GetCurrentToken()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x04000186 RID: 390
		private readonly Type declaringType;

		// Token: 0x04000187 RID: 391
		private readonly string name;

		// Token: 0x04000188 RID: 392
		private readonly PropertySignature signature;
	}
}
