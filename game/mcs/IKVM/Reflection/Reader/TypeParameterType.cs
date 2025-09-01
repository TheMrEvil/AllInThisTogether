using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000095 RID: 149
	internal abstract class TypeParameterType : TypeInfo
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x00019712 File Offset: 0x00017912
		protected TypeParameterType(byte sigElementType) : base(sigElementType)
		{
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000055E7 File Offset: 0x000037E7
		public sealed override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001971B File Offset: 0x0001791B
		public sealed override bool IsValueType
		{
			get
			{
				return (this.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) > GenericParameterAttributes.None;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00019728 File Offset: 0x00017928
		public sealed override Type BaseType
		{
			get
			{
				foreach (Type type in this.GetGenericParameterConstraints())
				{
					if (!type.IsInterface && !type.IsGenericParameter)
					{
						return type;
					}
				}
				if (!this.IsValueType)
				{
					return this.Module.universe.System_Object;
				}
				return this.Module.universe.System_ValueType;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001978C File Offset: 0x0001798C
		public override Type[] __GetDeclaredInterfaces()
		{
			List<Type> list = new List<Type>();
			foreach (Type type in this.GetGenericParameterConstraints())
			{
				if (type.IsInterface)
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0000212D File Offset: 0x0000032D
		public sealed override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.Public;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x000055E7 File Offset: 0x000037E7
		public sealed override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000197CD File Offset: 0x000179CD
		public sealed override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x000197D5 File Offset: 0x000179D5
		protected sealed override bool ContainsMissingTypeImpl
		{
			get
			{
				return Type.ContainsMissingType(this.GetGenericParameterConstraints());
			}
		}
	}
}
