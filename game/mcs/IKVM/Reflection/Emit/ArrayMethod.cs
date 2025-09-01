using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000EC RID: 236
	internal class ArrayMethod : MethodInfo
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00028CA8 File Offset: 0x00026EA8
		internal ArrayMethod(Module module, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.module = module;
			this.arrayClass = arrayClass;
			this.methodName = methodName;
			this.callingConvention = callingConvention;
			this.returnType = (returnType ?? module.universe.System_Void);
			this.parameterTypes = Util.Copy(parameterTypes);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override int __MethodRVA
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0000225C File Offset: 0x0000045C
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0000225C File Offset: 0x0000045C
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00028CFC File Offset: 0x00026EFC
		internal override int ImportTo(ModuleBuilder module)
		{
			return module.ImportMethodOrField(this.arrayClass, this.methodName, this.MethodSignature);
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0000225C File Offset: 0x0000045C
		public override MethodAttributes Attributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00028D16 File Offset: 0x00026F16
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00028D1E File Offset: 0x00026F1E
		public override Type DeclaringType
		{
			get
			{
				return this.arrayClass;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00028D28 File Offset: 0x00026F28
		internal override MethodSignature MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					this.methodSignature = MethodSignature.MakeFromBuilder(this.returnType, this.parameterTypes, default(PackedCustomModifiers), this.callingConvention, 0);
				}
				return this.methodSignature;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00028D6A File Offset: 0x00026F6A
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00028D72 File Offset: 0x00026F72
		public override string Name
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00028D7A File Offset: 0x00026F7A
		internal override int ParameterCount
		{
			get
			{
				return this.parameterTypes.Length;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00028D84 File Offset: 0x00026F84
		public override Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00028D8C File Offset: 0x00026F8C
		internal override bool HasThis
		{
			get
			{
				return (this.callingConvention & (CallingConventions.HasThis | CallingConventions.ExplicitThis)) == CallingConventions.HasThis;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00010856 File Offset: 0x0000EA56
		internal override int GetCurrentToken()
		{
			return this.MetadataToken;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00028D9B File Offset: 0x00026F9B
		internal override bool IsBaked
		{
			get
			{
				return this.arrayClass.IsBaked;
			}
		}

		// Token: 0x040004EA RID: 1258
		private readonly Module module;

		// Token: 0x040004EB RID: 1259
		private readonly Type arrayClass;

		// Token: 0x040004EC RID: 1260
		private readonly string methodName;

		// Token: 0x040004ED RID: 1261
		private readonly CallingConventions callingConvention;

		// Token: 0x040004EE RID: 1262
		private readonly Type returnType;

		// Token: 0x040004EF RID: 1263
		protected readonly Type[] parameterTypes;

		// Token: 0x040004F0 RID: 1264
		private MethodSignature methodSignature;
	}
}
