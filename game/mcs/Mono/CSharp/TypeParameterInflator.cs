using System;

namespace Mono.CSharp
{
	// Token: 0x02000221 RID: 545
	public struct TypeParameterInflator
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x00086969 File Offset: 0x00084B69
		public TypeParameterInflator(TypeParameterInflator nested, TypeSpec type)
		{
			this = new TypeParameterInflator(nested.context, type, nested.tparams, nested.targs);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00086984 File Offset: 0x00084B84
		public TypeParameterInflator(IModuleContext context, TypeSpec type, TypeParameterSpec[] tparams, TypeSpec[] targs)
		{
			if (tparams.Length != targs.Length)
			{
				throw new ArgumentException("Invalid arguments");
			}
			this.context = context;
			this.tparams = tparams;
			this.targs = targs;
			this.type = type;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x000869B7 File Offset: 0x00084BB7
		public IModuleContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x000869BF File Offset: 0x00084BBF
		public TypeSpec TypeInstance
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x000869C7 File Offset: 0x00084BC7
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return this.tparams;
			}
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000869D0 File Offset: 0x00084BD0
		public TypeSpec Inflate(TypeSpec type)
		{
			TypeParameterSpec typeParameterSpec = type as TypeParameterSpec;
			if (typeParameterSpec != null)
			{
				return this.Inflate(typeParameterSpec);
			}
			ElementTypeSpec elementTypeSpec = type as ElementTypeSpec;
			if (elementTypeSpec != null)
			{
				TypeSpec typeSpec = this.Inflate(elementTypeSpec.Element);
				if (typeSpec == elementTypeSpec.Element)
				{
					return elementTypeSpec;
				}
				ArrayContainer arrayContainer = elementTypeSpec as ArrayContainer;
				if (arrayContainer != null)
				{
					return ArrayContainer.MakeType(this.context.Module, typeSpec, arrayContainer.Rank);
				}
				if (elementTypeSpec is PointerContainer)
				{
					return PointerContainer.MakeType(this.context.Module, typeSpec);
				}
				throw new NotImplementedException();
			}
			else
			{
				if (type.Kind == MemberKind.MissingType)
				{
					return type;
				}
				int i = 0;
				TypeSpec[] array;
				if (type.IsNested)
				{
					TypeSpec container = this.Inflate(type.DeclaringType);
					array = type.TypeArguments;
					if (array.Length == 0 && type.Arity > 0)
					{
						array = type.MemberDefinition.TypeParameters;
					}
					type = MemberCache.FindNestedType(container, type.Name, type.Arity);
					if (array.Length != 0)
					{
						TypeSpec[] array2 = new TypeSpec[array.Length];
						while (i < array.Length)
						{
							array2[i] = this.Inflate(array[i]);
							i++;
						}
						type = type.MakeGenericType(this.context, array2);
					}
					return type;
				}
				if (type.Arity == 0)
				{
					return type;
				}
				array = new TypeSpec[type.Arity];
				if (type is InflatedTypeSpec)
				{
					while (i < array.Length)
					{
						array[i] = this.Inflate(type.TypeArguments[i]);
						i++;
					}
					type = type.GetDefinition();
				}
				else
				{
					foreach (TypeParameterSpec tp in type.MemberDefinition.TypeParameters)
					{
						array[i++] = this.Inflate(tp);
					}
				}
				return type.MakeGenericType(this.context, array);
			}
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00086B74 File Offset: 0x00084D74
		public TypeSpec Inflate(TypeParameterSpec tp)
		{
			for (int i = 0; i < this.tparams.Length; i++)
			{
				if (this.tparams[i] == tp)
				{
					return this.targs[i];
				}
			}
			return tp;
		}

		// Token: 0x04000A4F RID: 2639
		private readonly TypeSpec type;

		// Token: 0x04000A50 RID: 2640
		private readonly TypeParameterSpec[] tparams;

		// Token: 0x04000A51 RID: 2641
		private readonly TypeSpec[] targs;

		// Token: 0x04000A52 RID: 2642
		private readonly IModuleContext context;
	}
}
