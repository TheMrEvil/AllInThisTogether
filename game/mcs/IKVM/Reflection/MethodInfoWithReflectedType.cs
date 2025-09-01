using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x0200003D RID: 61
	internal sealed class MethodInfoWithReflectedType : MethodInfo
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00009113 File Offset: 0x00007313
		internal MethodInfoWithReflectedType(Type reflectedType, MethodInfo method)
		{
			this.reflectedType = reflectedType;
			this.method = method;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000912C File Offset: 0x0000732C
		public override bool Equals(object obj)
		{
			MethodInfoWithReflectedType methodInfoWithReflectedType = obj as MethodInfoWithReflectedType;
			return methodInfoWithReflectedType != null && methodInfoWithReflectedType.reflectedType == this.reflectedType && methodInfoWithReflectedType.method == this.method;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000916F File Offset: 0x0000736F
		public override int GetHashCode()
		{
			return this.reflectedType.GetHashCode() ^ this.method.GetHashCode();
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009188 File Offset: 0x00007388
		internal override MethodSignature MethodSignature
		{
			get
			{
				return this.method.MethodSignature;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009195 File Offset: 0x00007395
		internal override int ParameterCount
		{
			get
			{
				return this.method.ParameterCount;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000091A4 File Offset: 0x000073A4
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parameters = this.method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i] = new ParameterInfoWrapper(this, parameters[i]);
			}
			return parameters;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000091D8 File Offset: 0x000073D8
		public override MethodAttributes Attributes
		{
			get
			{
				return this.method.Attributes;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000091E5 File Offset: 0x000073E5
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.method.GetMethodImplementationFlags();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000091F2 File Offset: 0x000073F2
		public override MethodBody GetMethodBody()
		{
			return this.method.GetMethodBody();
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000091FF File Offset: 0x000073FF
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.method.CallingConvention;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000920C File Offset: 0x0000740C
		public override int __MethodRVA
		{
			get
			{
				return this.method.__MethodRVA;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00009219 File Offset: 0x00007419
		public override Type ReturnType
		{
			get
			{
				return this.method.ReturnType;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009226 File Offset: 0x00007426
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return new ParameterInfoWrapper(this, this.method.ReturnParameter);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009239 File Offset: 0x00007439
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.method.MakeGenericMethod(typeArguments), this.reflectedType);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009252 File Offset: 0x00007452
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.method.GetGenericMethodDefinition();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000925F File Offset: 0x0000745F
		public override string ToString()
		{
			return this.method.ToString();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000926C File Offset: 0x0000746C
		public override MethodInfo[] __GetMethodImpls()
		{
			return this.method.__GetMethodImpls();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009279 File Offset: 0x00007479
		internal override Type GetGenericMethodArgument(int index)
		{
			return this.method.GetGenericMethodArgument(index);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009287 File Offset: 0x00007487
		internal override int GetGenericMethodArgumentCount()
		{
			return this.method.GetGenericMethodArgumentCount();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009294 File Offset: 0x00007494
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this.method.GetMethodOnTypeDefinition();
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000092A1 File Offset: 0x000074A1
		internal override bool HasThis
		{
			get
			{
				return this.method.HasThis;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000092AE File Offset: 0x000074AE
		public override Module Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000092BB File Offset: 0x000074BB
		public override Type DeclaringType
		{
			get
			{
				return this.method.DeclaringType;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000092C8 File Offset: 0x000074C8
		public override Type ReflectedType
		{
			get
			{
				return this.reflectedType;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000092D0 File Offset: 0x000074D0
		public override string Name
		{
			get
			{
				return this.method.Name;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000092DD File Offset: 0x000074DD
		internal override int ImportTo(ModuleBuilder module)
		{
			return this.method.ImportTo(module);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000092EB File Offset: 0x000074EB
		public override MethodBase __GetMethodOnTypeDefinition()
		{
			return this.method.__GetMethodOnTypeDefinition();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000092F8 File Offset: 0x000074F8
		public override bool __IsMissing
		{
			get
			{
				return this.method.__IsMissing;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009305 File Offset: 0x00007505
		internal override MethodBase BindTypeParameters(Type type)
		{
			return this.method.BindTypeParameters(type);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00009313 File Offset: 0x00007513
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.method.ContainsGenericParameters;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009320 File Offset: 0x00007520
		public override Type[] GetGenericArguments()
		{
			return this.method.GetGenericArguments();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000932D File Offset: 0x0000752D
		public override bool IsGenericMethod
		{
			get
			{
				return this.method.IsGenericMethod;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000933A File Offset: 0x0000753A
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009347 File Offset: 0x00007547
		public override int MetadataToken
		{
			get
			{
				return this.method.MetadataToken;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009354 File Offset: 0x00007554
		internal override int GetCurrentToken()
		{
			return this.method.GetCurrentToken();
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00009361 File Offset: 0x00007561
		internal override bool IsBaked
		{
			get
			{
				return this.method.IsBaked;
			}
		}

		// Token: 0x04000164 RID: 356
		private readonly Type reflectedType;

		// Token: 0x04000165 RID: 357
		private readonly MethodInfo method;
	}
}
