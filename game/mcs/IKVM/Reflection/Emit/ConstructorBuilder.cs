using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000D6 RID: 214
	public sealed class ConstructorBuilder : ConstructorInfo
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00022A2F File Offset: 0x00020C2F
		internal ConstructorBuilder(MethodBuilder mb)
		{
			this.methodBuilder = mb;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00022A40 File Offset: 0x00020C40
		public override bool Equals(object obj)
		{
			ConstructorBuilder constructorBuilder = obj as ConstructorBuilder;
			return constructorBuilder != null && constructorBuilder.methodBuilder.Equals(this.methodBuilder);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00022A70 File Offset: 0x00020C70
		public override int GetHashCode()
		{
			return this.methodBuilder.GetHashCode();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00022A7D File Offset: 0x00020C7D
		public void __SetSignature(Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			this.methodBuilder.__SetSignature(returnType, returnTypeCustomModifiers, parameterTypes, parameterTypeCustomModifiers);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00022A8F File Offset: 0x00020C8F
		[Obsolete("Please use __SetSignature(Type, CustomModifiers, Type[], CustomModifiers[]) instead.")]
		public void __SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.methodBuilder.SetSignature(returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00022AA5 File Offset: 0x00020CA5
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			return this.methodBuilder.DefineParameter(position, attributes, strParamName);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00022AB5 File Offset: 0x00020CB5
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.methodBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00022AC3 File Offset: 0x00020CC3
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.methodBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00022AD2 File Offset: 0x00020CD2
		public void __AddDeclarativeSecurity(CustomAttributeBuilder customBuilder)
		{
			this.methodBuilder.__AddDeclarativeSecurity(customBuilder);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00022AE0 File Offset: 0x00020CE0
		public void AddDeclarativeSecurity(SecurityAction securityAction, PermissionSet permissionSet)
		{
			this.methodBuilder.AddDeclarativeSecurity(securityAction, permissionSet);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00022AEF File Offset: 0x00020CEF
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.methodBuilder.SetImplementationFlags(attributes);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00022AFD File Offset: 0x00020CFD
		public ILGenerator GetILGenerator()
		{
			return this.methodBuilder.GetILGenerator();
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00022B0A File Offset: 0x00020D0A
		public ILGenerator GetILGenerator(int streamSize)
		{
			return this.methodBuilder.GetILGenerator(streamSize);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00022B18 File Offset: 0x00020D18
		public void __ReleaseILGenerator()
		{
			this.methodBuilder.__ReleaseILGenerator();
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00022B25 File Offset: 0x00020D25
		public Type ReturnType
		{
			get
			{
				return this.methodBuilder.ReturnType;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00022B32 File Offset: 0x00020D32
		public Module GetModule()
		{
			return this.methodBuilder.GetModule();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00022B3F File Offset: 0x00020D3F
		public MethodToken GetToken()
		{
			return this.methodBuilder.GetToken();
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00022B4C File Offset: 0x00020D4C
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00022B59 File Offset: 0x00020D59
		public bool InitLocals
		{
			get
			{
				return this.methodBuilder.InitLocals;
			}
			set
			{
				this.methodBuilder.InitLocals = value;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00022B67 File Offset: 0x00020D67
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.methodBuilder.SetMethodBody(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00022B7B File Offset: 0x00020D7B
		internal override MethodInfo GetMethodInfo()
		{
			return this.methodBuilder;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00022B7B File Offset: 0x00020D7B
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this.methodBuilder;
		}

		// Token: 0x04000415 RID: 1045
		private readonly MethodBuilder methodBuilder;
	}
}
