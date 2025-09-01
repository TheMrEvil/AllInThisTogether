using System;
using System.Runtime.InteropServices;

namespace IKVM.Reflection
{
	// Token: 0x02000058 RID: 88
	public sealed class __StandAloneMethodSig
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		internal __StandAloneMethodSig(bool unmanaged, CallingConvention unmanagedCallingConvention, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, PackedCustomModifiers customModifiers)
		{
			this.unmanaged = unmanaged;
			this.unmanagedCallingConvention = unmanagedCallingConvention;
			this.callingConvention = callingConvention;
			this.returnType = returnType;
			this.parameterTypes = parameterTypes;
			this.optionalParameterTypes = optionalParameterTypes;
			this.customModifiers = customModifiers;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public bool Equals(__StandAloneMethodSig other)
		{
			return other != null && other.unmanaged == this.unmanaged && other.unmanagedCallingConvention == this.unmanagedCallingConvention && other.callingConvention == this.callingConvention && other.returnType == this.returnType && Util.ArrayEquals(other.parameterTypes, this.parameterTypes) && Util.ArrayEquals(other.optionalParameterTypes, this.optionalParameterTypes) && other.customModifiers.Equals(this.customModifiers);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000DBC1 File Offset: 0x0000BDC1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as __StandAloneMethodSig);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000DBCF File Offset: 0x0000BDCF
		public override int GetHashCode()
		{
			return this.returnType.GetHashCode() ^ Util.GetHashCode(this.parameterTypes);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public bool IsUnmanaged
		{
			get
			{
				return this.unmanaged;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		public CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		public CallingConvention UnmanagedCallingConvention
		{
			get
			{
				return this.unmanagedCallingConvention;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public CustomModifiers GetReturnTypeCustomModifiers()
		{
			return this.customModifiers.GetReturnTypeCustomModifiers();
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000DC23 File Offset: 0x0000BE23
		public Type[] ParameterTypes
		{
			get
			{
				return Util.Copy(this.parameterTypes);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000DC30 File Offset: 0x0000BE30
		public Type[] OptionalParameterTypes
		{
			get
			{
				return Util.Copy(this.optionalParameterTypes);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public CustomModifiers GetParameterCustomModifiers(int index)
		{
			return this.customModifiers.GetParameterCustomModifiers(index);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		public bool ContainsMissingType
		{
			get
			{
				return this.returnType.__ContainsMissingType || Type.ContainsMissingType(this.parameterTypes) || Type.ContainsMissingType(this.optionalParameterTypes) || this.customModifiers.ContainsMissingType;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		internal int ParameterCount
		{
			get
			{
				return this.parameterTypes.Length + this.optionalParameterTypes.Length;
			}
		}

		// Token: 0x040001F2 RID: 498
		private readonly bool unmanaged;

		// Token: 0x040001F3 RID: 499
		private readonly CallingConvention unmanagedCallingConvention;

		// Token: 0x040001F4 RID: 500
		private readonly CallingConventions callingConvention;

		// Token: 0x040001F5 RID: 501
		private readonly Type returnType;

		// Token: 0x040001F6 RID: 502
		private readonly Type[] parameterTypes;

		// Token: 0x040001F7 RID: 503
		private readonly Type[] optionalParameterTypes;

		// Token: 0x040001F8 RID: 504
		private readonly PackedCustomModifiers customModifiers;
	}
}
