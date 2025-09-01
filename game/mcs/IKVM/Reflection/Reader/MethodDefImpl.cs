using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000099 RID: 153
	internal sealed class MethodDefImpl : MethodInfo
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x0001A2B3 File Offset: 0x000184B3
		internal MethodDefImpl(ModuleReader module, TypeDefImpl declaringType, int index)
		{
			this.module = module;
			this.index = index;
			this.declaringType = declaringType;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001A2D0 File Offset: 0x000184D0
		public override MethodBody GetMethodBody()
		{
			return this.GetMethodBody(this);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001A2DC File Offset: 0x000184DC
		internal MethodBody GetMethodBody(IGenericContext context)
		{
			if ((this.GetMethodImplementationFlags() & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL)
			{
				return null;
			}
			int rva = this.module.MethodDef.records[this.index].RVA;
			if (rva != 0)
			{
				return new MethodBody(this.module, rva, context);
			}
			return null;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001A328 File Offset: 0x00018528
		public override int __MethodRVA
		{
			get
			{
				return this.module.MethodDef.records[this.index].RVA;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001A34A File Offset: 0x0001854A
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.MethodSignature.CallingConvention;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001A357 File Offset: 0x00018557
		public override MethodAttributes Attributes
		{
			get
			{
				return (MethodAttributes)this.module.MethodDef.records[this.index].Flags;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001A379 File Offset: 0x00018579
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return (MethodImplAttributes)this.module.MethodDef.records[this.index].ImplFlags;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A39B File Offset: 0x0001859B
		public override ParameterInfo[] GetParameters()
		{
			this.PopulateParameters();
			return (ParameterInfo[])this.parameters.Clone();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A3B4 File Offset: 0x000185B4
		private void PopulateParameters()
		{
			if (this.parameters == null)
			{
				MethodSignature methodSignature = this.MethodSignature;
				this.parameters = new ParameterInfo[methodSignature.GetParameterCount()];
				int i = this.module.MethodDef.records[this.index].ParamList - 1;
				int num = (this.module.MethodDef.records.Length > this.index + 1) ? (this.module.MethodDef.records[this.index + 1].ParamList - 1) : this.module.Param.records.Length;
				while (i < num)
				{
					int num2 = (int)(this.module.Param.records[i].Sequence - 1);
					if (num2 == -1)
					{
						this.returnParameter = new ParameterInfoImpl(this, num2, i);
					}
					else
					{
						this.parameters[num2] = new ParameterInfoImpl(this, num2, i);
					}
					i++;
				}
				for (int j = 0; j < this.parameters.Length; j++)
				{
					if (this.parameters[j] == null)
					{
						this.parameters[j] = new ParameterInfoImpl(this, j, -1);
					}
				}
				if (this.returnParameter == null)
				{
					this.returnParameter = new ParameterInfoImpl(this, -1, -1);
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0001A4FE File Offset: 0x000186FE
		internal override int ParameterCount
		{
			get
			{
				return this.MethodSignature.GetParameterCount();
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0001A50B File Offset: 0x0001870B
		public override ParameterInfo ReturnParameter
		{
			get
			{
				this.PopulateParameters();
				return this.returnParameter;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001A519 File Offset: 0x00018719
		public override Type ReturnType
		{
			get
			{
				return this.ReturnParameter.ParameterType;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001A526 File Offset: 0x00018726
		public override Type DeclaringType
		{
			get
			{
				if (!this.declaringType.IsModulePseudoType)
				{
					return this.declaringType;
				}
				return null;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001A53D File Offset: 0x0001873D
		public override string Name
		{
			get
			{
				return this.module.GetString(this.module.MethodDef.records[this.index].Name);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001A56A File Offset: 0x0001876A
		public override int MetadataToken
		{
			get
			{
				return (6 << 24) + this.index + 1;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001A579 File Offset: 0x00018779
		public override bool IsGenericMethodDefinition
		{
			get
			{
				this.PopulateGenericArguments();
				return this.typeArgs.Length != 0;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00008BCC File Offset: 0x00006DCC
		public override bool IsGenericMethod
		{
			get
			{
				return this.IsGenericMethodDefinition;
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A58B File Offset: 0x0001878B
		public override Type[] GetGenericArguments()
		{
			this.PopulateGenericArguments();
			return Util.Copy(this.typeArgs);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A5A0 File Offset: 0x000187A0
		private void PopulateGenericArguments()
		{
			if (this.typeArgs == null)
			{
				int metadataToken = this.MetadataToken;
				int num = this.module.GenericParam.FindFirstByOwner(metadataToken);
				if (num == -1)
				{
					this.typeArgs = Type.EmptyTypes;
					return;
				}
				List<Type> list = new List<Type>();
				int num2 = this.module.GenericParam.records.Length;
				int num3 = num;
				while (num3 < num2 && this.module.GenericParam.records[num3].Owner == metadataToken)
				{
					list.Add(new GenericTypeParameter(this.module, num3, 30));
					num3++;
				}
				this.typeArgs = list.ToArray();
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A64A File Offset: 0x0001884A
		internal override Type GetGenericMethodArgument(int index)
		{
			this.PopulateGenericArguments();
			return this.typeArgs[index];
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A65A File Offset: 0x0001885A
		internal override int GetGenericMethodArgumentCount()
		{
			this.PopulateGenericArguments();
			return this.typeArgs.Length;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A66A File Offset: 0x0001886A
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (this.IsGenericMethodDefinition)
			{
				return this;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A67B File Offset: 0x0001887B
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return new GenericMethodInstance(this.declaringType, this, typeArguments);
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001A68A File Offset: 0x0001888A
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001A694 File Offset: 0x00018894
		internal override MethodSignature MethodSignature
		{
			get
			{
				MethodSignature result;
				if ((result = this.lazyMethodSignature) == null)
				{
					result = (this.lazyMethodSignature = MethodSignature.ReadSig(this.module, this.module.GetBlob(this.module.MethodDef.records[this.index].Signature), this));
				}
				return result;
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A6EB File Offset: 0x000188EB
		internal override int ImportTo(ModuleBuilder module)
		{
			return module.ImportMethodOrField(this.declaringType, this.Name, this.MethodSignature);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A708 File Offset: 0x00018908
		public override MethodInfo[] __GetMethodImpls()
		{
			Type[] array = null;
			List<MethodInfo> list = null;
			foreach (int num in this.module.MethodImpl.Filter(this.declaringType.MetadataToken))
			{
				if (this.module.MethodImpl.records[num].MethodBody == this.MetadataToken)
				{
					if (array == null)
					{
						array = this.declaringType.GetGenericArguments();
					}
					if (list == null)
					{
						list = new List<MethodInfo>();
					}
					list.Add((MethodInfo)this.module.ResolveMethod(this.module.MethodImpl.records[num].MethodDeclaration, array, null));
				}
			}
			return Util.ToArray<MethodInfo, MethodInfo>(list, Empty<MethodInfo>.Array);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00010856 File Offset: 0x0000EA56
		internal override int GetCurrentToken()
		{
			return this.MetadataToken;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400031A RID: 794
		private readonly ModuleReader module;

		// Token: 0x0400031B RID: 795
		private readonly int index;

		// Token: 0x0400031C RID: 796
		private readonly TypeDefImpl declaringType;

		// Token: 0x0400031D RID: 797
		private MethodSignature lazyMethodSignature;

		// Token: 0x0400031E RID: 798
		private ParameterInfo returnParameter;

		// Token: 0x0400031F RID: 799
		private ParameterInfo[] parameters;

		// Token: 0x04000320 RID: 800
		private Type[] typeArgs;
	}
}
