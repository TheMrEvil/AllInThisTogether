using System;

namespace IKVM.Reflection
{
	// Token: 0x02000043 RID: 67
	public struct MissingGenericMethodBuilder
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00009BAC File Offset: 0x00007DAC
		public MissingGenericMethodBuilder(Type declaringType, CallingConventions callingConvention, string name, int genericParameterCount)
		{
			this.method = new MissingMethod(declaringType, name, new MethodSignature(null, null, default(PackedCustomModifiers), callingConvention, genericParameterCount));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009BD9 File Offset: 0x00007DD9
		public Type[] GetGenericArguments()
		{
			return this.method.GetGenericArguments();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public void SetSignature(Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			this.method.signature = new MethodSignature(returnType ?? this.method.Module.universe.System_Void, Util.Copy(parameterTypes), PackedCustomModifiers.CreateFromExternal(returnTypeCustomModifiers, parameterTypeCustomModifiers, parameterTypes.Length), this.method.signature.CallingConvention, this.method.signature.GenericParameterCount);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009C50 File Offset: 0x00007E50
		[Obsolete("Please use SetSignature(Type, CustomModifiers, Type[], CustomModifiers[]) instead.")]
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.method.signature = new MethodSignature(returnType ?? this.method.Module.universe.System_Void, Util.Copy(parameterTypes), PackedCustomModifiers.CreateFromExternal(returnTypeOptionalCustomModifiers, returnTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, parameterTypeRequiredCustomModifiers, parameterTypes.Length), this.method.signature.CallingConvention, this.method.signature.GenericParameterCount);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009CBD File Offset: 0x00007EBD
		public MethodInfo Finish()
		{
			return this.method;
		}

		// Token: 0x0400016F RID: 367
		private readonly MissingMethod method;
	}
}
