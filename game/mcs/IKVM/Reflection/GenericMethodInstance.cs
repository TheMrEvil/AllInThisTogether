using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x0200002F RID: 47
	internal sealed class GenericMethodInstance : MethodInfo
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00007838 File Offset: 0x00005A38
		internal GenericMethodInstance(Type declaringType, MethodInfo method, Type[] methodArgs)
		{
			this.declaringType = declaringType;
			this.method = method;
			this.methodArgs = methodArgs;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007858 File Offset: 0x00005A58
		public override bool Equals(object obj)
		{
			GenericMethodInstance genericMethodInstance = obj as GenericMethodInstance;
			return genericMethodInstance != null && genericMethodInstance.method.Equals(this.method) && genericMethodInstance.declaringType.Equals(this.declaringType) && Util.ArrayEquals(genericMethodInstance.methodArgs, this.methodArgs);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000078AE File Offset: 0x00005AAE
		public override int GetHashCode()
		{
			return this.declaringType.GetHashCode() * 33 ^ this.method.GetHashCode() ^ Util.GetHashCode(this.methodArgs);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000078D6 File Offset: 0x00005AD6
		public override Type ReturnType
		{
			get
			{
				return this.method.ReturnType.BindTypeParameters(this);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000078E9 File Offset: 0x00005AE9
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return new GenericParameterInfoImpl(this, this.method.ReturnParameter);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000078FC File Offset: 0x00005AFC
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parameters = this.method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i] = new GenericParameterInfoImpl(this, parameters[i]);
			}
			return parameters;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007930 File Offset: 0x00005B30
		internal override int ParameterCount
		{
			get
			{
				return this.method.ParameterCount;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000793D File Offset: 0x00005B3D
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.method.CallingConvention;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000794A File Offset: 0x00005B4A
		public override MethodAttributes Attributes
		{
			get
			{
				return this.method.Attributes;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007957 File Offset: 0x00005B57
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.method.GetMethodImplementationFlags();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007964 File Offset: 0x00005B64
		public override string Name
		{
			get
			{
				return this.method.Name;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007971 File Offset: 0x00005B71
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007988 File Offset: 0x00005B88
		public override Module Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007995 File Offset: 0x00005B95
		public override int MetadataToken
		{
			get
			{
				return this.method.MetadataToken;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000079A4 File Offset: 0x00005BA4
		public override MethodBody GetMethodBody()
		{
			MethodDefImpl methodDefImpl = this.method as MethodDefImpl;
			if (methodDefImpl != null)
			{
				return methodDefImpl.GetMethodBody(this);
			}
			throw new NotSupportedException();
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000079D3 File Offset: 0x00005BD3
		public override int __MethodRVA
		{
			get
			{
				return this.method.__MethodRVA;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000079E0 File Offset: 0x00005BE0
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return new GenericMethodInstance(this.declaringType, this.method, typeArguments);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000079F4 File Offset: 0x00005BF4
		public override bool IsGenericMethod
		{
			get
			{
				return this.method.IsGenericMethod;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007A01 File Offset: 0x00005C01
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.method.IsGenericMethodDefinition && this.methodArgs == null;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007A1C File Offset: 0x00005C1C
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.declaringType.ContainsGenericParameters)
				{
					return true;
				}
				if (this.methodArgs != null)
				{
					Type[] array = this.methodArgs;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007A62 File Offset: 0x00005C62
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			if (this.IsGenericMethodDefinition)
			{
				return this;
			}
			if (this.declaringType.IsConstructedGenericType)
			{
				return new GenericMethodInstance(this.declaringType, this.method, null);
			}
			return this.method;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007AA2 File Offset: 0x00005CA2
		public override MethodBase __GetMethodOnTypeDefinition()
		{
			return this.method;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007AAA File Offset: 0x00005CAA
		public override Type[] GetGenericArguments()
		{
			if (this.methodArgs == null)
			{
				return this.method.GetGenericArguments();
			}
			return (Type[])this.methodArgs.Clone();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007AD0 File Offset: 0x00005CD0
		internal override Type GetGenericMethodArgument(int index)
		{
			if (this.methodArgs == null)
			{
				return this.method.GetGenericMethodArgument(index);
			}
			return this.methodArgs[index];
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007AEF File Offset: 0x00005CEF
		internal override int GetGenericMethodArgumentCount()
		{
			return this.method.GetGenericMethodArgumentCount();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007AFC File Offset: 0x00005CFC
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this.method.GetMethodOnTypeDefinition();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007B0C File Offset: 0x00005D0C
		internal override int ImportTo(ModuleBuilder module)
		{
			if (this.methodArgs == null)
			{
				return module.ImportMethodOrField(this.declaringType, this.method.Name, this.method.MethodSignature);
			}
			return module.ImportMethodSpec(this.declaringType, this.method, this.methodArgs);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007B5C File Offset: 0x00005D5C
		internal override MethodSignature MethodSignature
		{
			get
			{
				MethodSignature result;
				if ((result = this.lazyMethodSignature) == null)
				{
					result = (this.lazyMethodSignature = this.method.MethodSignature.Bind(this.declaringType, this.methodArgs));
				}
				return result;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007B98 File Offset: 0x00005D98
		internal override MethodBase BindTypeParameters(Type type)
		{
			return new GenericMethodInstance(this.declaringType.BindTypeParameters(type), this.method, null);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007BB2 File Offset: 0x00005DB2
		internal override bool HasThis
		{
			get
			{
				return this.method.HasThis;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public override MethodInfo[] __GetMethodImpls()
		{
			MethodInfo[] array = this.method.__GetMethodImpls();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (MethodInfo)array[i].BindTypeParameters(this.declaringType);
			}
			return array;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007BFE File Offset: 0x00005DFE
		internal override int GetCurrentToken()
		{
			return this.method.GetCurrentToken();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007C0B File Offset: 0x00005E0B
		internal override bool IsBaked
		{
			get
			{
				return this.method.IsBaked;
			}
		}

		// Token: 0x0400013A RID: 314
		private readonly Type declaringType;

		// Token: 0x0400013B RID: 315
		private readonly MethodInfo method;

		// Token: 0x0400013C RID: 316
		private readonly Type[] methodArgs;

		// Token: 0x0400013D RID: 317
		private MethodSignature lazyMethodSignature;
	}
}
