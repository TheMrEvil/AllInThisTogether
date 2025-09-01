using System;
using System.Collections.Generic;
using System.Text;

namespace IKVM.Reflection
{
	// Token: 0x0200003C RID: 60
	public abstract class MethodInfo : MethodBase, IGenericContext, IGenericBinder
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000031F6 File Offset: 0x000013F6
		internal MethodInfo()
		{
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00008E8B File Offset: 0x0000708B
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023F RID: 575
		public abstract Type ReturnType { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000240 RID: 576
		public abstract ParameterInfo ReturnParameter { get; }

		// Token: 0x06000241 RID: 577 RVA: 0x00008E8E File Offset: 0x0000708E
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException(base.GetType().FullName);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008E8E File Offset: 0x0000708E
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException(base.GetType().FullName);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008EA0 File Offset: 0x000070A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ReturnType.Name).Append(' ').Append(this.Name);
			string value;
			if (this.IsGenericMethod)
			{
				stringBuilder.Append('[');
				value = "";
				foreach (Type value2 in this.GetGenericArguments())
				{
					stringBuilder.Append(value).Append(value2);
					value = ", ";
				}
				stringBuilder.Append(']');
			}
			stringBuilder.Append('(');
			value = "";
			foreach (ParameterInfo parameterInfo in this.GetParameters())
			{
				stringBuilder.Append(value).Append(parameterInfo.ParameterType);
				value = ", ";
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00008F7B File Offset: 0x0000717B
		internal bool IsNewSlot
		{
			get
			{
				return (this.Attributes & MethodAttributes.VtableLayoutMask) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008F8C File Offset: 0x0000718C
		public MethodInfo GetBaseDefinition()
		{
			MethodInfo methodInfo = this;
			if (methodInfo.IsVirtual)
			{
				Type baseType = this.DeclaringType.BaseType;
				while (baseType != null && !methodInfo.IsNewSlot)
				{
					MethodInfo methodInfo2 = baseType.FindMethod(this.Name, this.MethodSignature) as MethodInfo;
					if (methodInfo2 != null && methodInfo2.IsVirtual)
					{
						methodInfo = methodInfo2;
					}
					baseType = baseType.BaseType;
				}
			}
			return methodInfo;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual MethodInfo[] __GetMethodImpls()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008FF5 File Offset: 0x000071F5
		public bool __TryGetImplMap(out ImplMapFlags mappingFlags, out string importName, out string importScope)
		{
			return this.Module.__TryGetImplMap(this.GetCurrentToken(), out mappingFlags, out importName, out importScope);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000900B File Offset: 0x0000720B
		public ConstructorInfo __AsConstructorInfo()
		{
			return new ConstructorInfoImpl(this);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009013 File Offset: 0x00007213
		Type IGenericContext.GetGenericTypeArgument(int index)
		{
			return this.DeclaringType.GetGenericTypeArgument(index);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009021 File Offset: 0x00007221
		Type IGenericContext.GetGenericMethodArgument(int index)
		{
			return this.GetGenericMethodArgument(index);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual Type GetGenericMethodArgument(int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual int GetGenericMethodArgumentCount()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005936 File Offset: 0x00003B36
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000902A File Offset: 0x0000722A
		Type IGenericBinder.BindTypeParameter(Type type)
		{
			return this.DeclaringType.GetGenericTypeArgument(type.GenericParameterPosition);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000903D File Offset: 0x0000723D
		Type IGenericBinder.BindMethodParameter(Type type)
		{
			return this.GetGenericMethodArgument(type.GenericParameterPosition);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000904B File Offset: 0x0000724B
		internal override MethodBase BindTypeParameters(Type type)
		{
			return new GenericMethodInstance(this.DeclaringType.BindTypeParameters(type), this, null);
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00009060 File Offset: 0x00007260
		internal virtual bool HasThis
		{
			get
			{
				return !base.IsStatic;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000906B File Offset: 0x0000726B
		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			return new MethodInfoWithReflectedType(type, this);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009074 File Offset: 0x00007274
		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			Module module = this.Module;
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			ImplMapFlags flags;
			string entryPoint;
			string dllName;
			if ((this.Attributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope && (attributeType == null || attributeType.IsAssignableFrom(module.universe.System_Runtime_InteropServices_DllImportAttribute)) && this.__TryGetImplMap(out flags, out entryPoint, out dllName))
			{
				list.Add(CustomAttributeData.CreateDllImportPseudoCustomAttribute(module, flags, entryPoint, dllName, this.GetMethodImplementationFlags()));
			}
			if ((this.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL && (attributeType == null || attributeType.IsAssignableFrom(module.universe.System_Runtime_InteropServices_PreserveSigAttribute)))
			{
				list.Add(CustomAttributeData.CreatePreserveSigPseudoCustomAttribute(module));
			}
			return list;
		}
	}
}
