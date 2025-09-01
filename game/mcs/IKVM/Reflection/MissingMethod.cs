using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000048 RID: 72
	internal sealed class MissingMethod : MethodInfo
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000A0DD File Offset: 0x000082DD
		internal MissingMethod(Type declaringType, string name, MethodSignature signature)
		{
			this.declaringType = declaringType;
			this.name = name;
			this.signature = signature;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000A0FA File Offset: 0x000082FA
		private MethodInfo Forwarder
		{
			get
			{
				MethodInfo methodInfo = this.TryGetForwarder();
				if (methodInfo == null)
				{
					throw new MissingMemberException(this);
				}
				return methodInfo;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A114 File Offset: 0x00008314
		private MethodInfo TryGetForwarder()
		{
			if (this.forwarder == null && !this.declaringType.__IsMissing)
			{
				MethodBase methodBase = this.declaringType.FindMethod(this.name, this.signature);
				ConstructorInfo constructorInfo = methodBase as ConstructorInfo;
				if (constructorInfo != null)
				{
					this.forwarder = constructorInfo.GetMethodInfo();
				}
				else
				{
					this.forwarder = (MethodInfo)methodBase;
				}
			}
			return this.forwarder;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000A184 File Offset: 0x00008384
		public override bool __IsMissing
		{
			get
			{
				return this.TryGetForwarder() == null;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000A192 File Offset: 0x00008392
		public override Type ReturnType
		{
			get
			{
				return this.signature.GetReturnType(this);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return new MissingMethod.ParameterInfoImpl(this, -1);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000A1A9 File Offset: 0x000083A9
		internal override MethodSignature MethodSignature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000A1B1 File Offset: 0x000083B1
		internal override int ParameterCount
		{
			get
			{
				return this.signature.GetParameterCount();
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] array = new ParameterInfo[this.signature.GetParameterCount()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new MissingMethod.ParameterInfoImpl(this, i);
			}
			return array;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000A1F7 File Offset: 0x000083F7
		public override MethodAttributes Attributes
		{
			get
			{
				return this.Forwarder.Attributes;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A204 File Offset: 0x00008404
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.Forwarder.GetMethodImplementationFlags();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A211 File Offset: 0x00008411
		public override MethodBody GetMethodBody()
		{
			return this.Forwarder.GetMethodBody();
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000A21E File Offset: 0x0000841E
		public override int __MethodRVA
		{
			get
			{
				return this.Forwarder.__MethodRVA;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000A22B File Offset: 0x0000842B
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.signature.CallingConvention;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000A238 File Offset: 0x00008438
		internal override int ImportTo(ModuleBuilder module)
		{
			MethodInfo methodInfo = this.TryGetForwarder();
			if (methodInfo != null)
			{
				return methodInfo.ImportTo(module);
			}
			return module.ImportMethodOrField(this.declaringType, this.Name, this.MethodSignature);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000A275 File Offset: 0x00008475
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000A27D File Offset: 0x0000847D
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000A294 File Offset: 0x00008494
		public override Module Module
		{
			get
			{
				return this.declaringType.Module;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public override bool Equals(object obj)
		{
			MissingMethod missingMethod = obj as MissingMethod;
			return missingMethod != null && missingMethod.declaringType == this.declaringType && missingMethod.name == this.name && missingMethod.signature.Equals(this.signature);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A2FA File Offset: 0x000084FA
		public override int GetHashCode()
		{
			return this.declaringType.GetHashCode() ^ this.name.GetHashCode() ^ this.signature.GetHashCode();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A320 File Offset: 0x00008520
		internal override MethodBase BindTypeParameters(Type type)
		{
			MethodInfo methodInfo = this.TryGetForwarder();
			if (methodInfo != null)
			{
				return methodInfo.BindTypeParameters(type);
			}
			return new GenericMethodInstance(type, this, null);
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000A34D File Offset: 0x0000854D
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.Forwarder.ContainsGenericParameters;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A35C File Offset: 0x0000855C
		public override Type[] GetGenericArguments()
		{
			if (this.TryGetForwarder() != null)
			{
				return this.Forwarder.GetGenericArguments();
			}
			if (this.typeArgs == null)
			{
				this.typeArgs = new Type[this.signature.GenericParameterCount];
				for (int i = 0; i < this.typeArgs.Length; i++)
				{
					this.typeArgs[i] = new MissingTypeParameter(this, i);
				}
			}
			return Util.Copy(this.typeArgs);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A3CE File Offset: 0x000085CE
		internal override Type GetGenericMethodArgument(int index)
		{
			return this.GetGenericArguments()[index];
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000A3D8 File Offset: 0x000085D8
		internal override int GetGenericMethodArgumentCount()
		{
			return this.Forwarder.GetGenericMethodArgumentCount();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A3E5 File Offset: 0x000085E5
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.Forwarder.GetGenericMethodDefinition();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A3F2 File Offset: 0x000085F2
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this.Forwarder.GetMethodOnTypeDefinition();
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000A3FF File Offset: 0x000085FF
		internal override bool HasThis
		{
			get
			{
				return (this.signature.CallingConvention & (CallingConventions.HasThis | CallingConventions.ExplicitThis)) == CallingConventions.HasThis;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00008BCC File Offset: 0x00006DCC
		public override bool IsGenericMethod
		{
			get
			{
				return this.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000A413 File Offset: 0x00008613
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.signature.GenericParameterCount != 0;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A424 File Offset: 0x00008624
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			MethodInfo methodInfo = this.TryGetForwarder();
			if (methodInfo != null)
			{
				return methodInfo.MakeGenericMethod(typeArguments);
			}
			return new GenericMethodInstance(this.declaringType, this, typeArguments);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000A456 File Offset: 0x00008656
		public override int MetadataToken
		{
			get
			{
				return this.Forwarder.MetadataToken;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A463 File Offset: 0x00008663
		internal override int GetCurrentToken()
		{
			return this.Forwarder.GetCurrentToken();
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000A470 File Offset: 0x00008670
		internal override bool IsBaked
		{
			get
			{
				return this.Forwarder.IsBaked;
			}
		}

		// Token: 0x0400017D RID: 381
		private readonly Type declaringType;

		// Token: 0x0400017E RID: 382
		private readonly string name;

		// Token: 0x0400017F RID: 383
		internal MethodSignature signature;

		// Token: 0x04000180 RID: 384
		private MethodInfo forwarder;

		// Token: 0x04000181 RID: 385
		private Type[] typeArgs;

		// Token: 0x02000327 RID: 807
		private sealed class ParameterInfoImpl : ParameterInfo
		{
			// Token: 0x0600258E RID: 9614 RVA: 0x000B3DBB File Offset: 0x000B1FBB
			internal ParameterInfoImpl(MissingMethod method, int index)
			{
				this.method = method;
				this.index = index;
			}

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x0600258F RID: 9615 RVA: 0x000B3DD1 File Offset: 0x000B1FD1
			private ParameterInfo Forwarder
			{
				get
				{
					if (this.index != -1)
					{
						return this.method.Forwarder.GetParameters()[this.index];
					}
					return this.method.Forwarder.ReturnParameter;
				}
			}

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x06002590 RID: 9616 RVA: 0x000B3E04 File Offset: 0x000B2004
			public override string Name
			{
				get
				{
					return this.Forwarder.Name;
				}
			}

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x06002591 RID: 9617 RVA: 0x000B3E11 File Offset: 0x000B2011
			public override Type ParameterType
			{
				get
				{
					if (this.index != -1)
					{
						return this.method.signature.GetParameterType(this.method, this.index);
					}
					return this.method.signature.GetReturnType(this.method);
				}
			}

			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x06002592 RID: 9618 RVA: 0x000B3E4F File Offset: 0x000B204F
			public override ParameterAttributes Attributes
			{
				get
				{
					return this.Forwarder.Attributes;
				}
			}

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x06002593 RID: 9619 RVA: 0x000B3E5C File Offset: 0x000B205C
			public override int Position
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x06002594 RID: 9620 RVA: 0x000B3E64 File Offset: 0x000B2064
			public override object RawDefaultValue
			{
				get
				{
					return this.Forwarder.RawDefaultValue;
				}
			}

			// Token: 0x06002595 RID: 9621 RVA: 0x000B3E71 File Offset: 0x000B2071
			public override CustomModifiers __GetCustomModifiers()
			{
				if (this.index != -1)
				{
					return this.method.signature.GetParameterCustomModifiers(this.method, this.index);
				}
				return this.method.signature.GetReturnTypeCustomModifiers(this.method);
			}

			// Token: 0x06002596 RID: 9622 RVA: 0x000B3EAF File Offset: 0x000B20AF
			public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
			{
				return this.Forwarder.__TryGetFieldMarshal(out fieldMarshal);
			}

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x06002597 RID: 9623 RVA: 0x000B3EBD File Offset: 0x000B20BD
			public override MemberInfo Member
			{
				get
				{
					return this.method;
				}
			}

			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x06002598 RID: 9624 RVA: 0x000B3EC5 File Offset: 0x000B20C5
			public override int MetadataToken
			{
				get
				{
					return this.Forwarder.MetadataToken;
				}
			}

			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x06002599 RID: 9625 RVA: 0x000B3ED2 File Offset: 0x000B20D2
			internal override Module Module
			{
				get
				{
					return this.method.Module;
				}
			}

			// Token: 0x0600259A RID: 9626 RVA: 0x000B3EDF File Offset: 0x000B20DF
			public override string ToString()
			{
				return this.Forwarder.ToString();
			}

			// Token: 0x04000E44 RID: 3652
			private readonly MissingMethod method;

			// Token: 0x04000E45 RID: 3653
			private readonly int index;
		}
	}
}
