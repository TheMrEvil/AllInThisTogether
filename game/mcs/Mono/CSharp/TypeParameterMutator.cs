using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000222 RID: 546
	public class TypeParameterMutator
	{
		// Token: 0x06001BD2 RID: 7122 RVA: 0x00086BA9 File Offset: 0x00084DA9
		public TypeParameterMutator(TypeParameters mvar, TypeParameters var)
		{
			if (mvar.Count != var.Count)
			{
				throw new ArgumentException();
			}
			this.mvar = mvar;
			this.var = var;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00086BD3 File Offset: 0x00084DD3
		public TypeParameterMutator(TypeParameterSpec[] srcVar, TypeParameters destVar)
		{
			if (srcVar.Length != destVar.Count)
			{
				throw new ArgumentException();
			}
			this.src = srcVar;
			this.var = destVar;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00086BFA File Offset: 0x00084DFA
		public TypeParameters MethodTypeParameters
		{
			get
			{
				return this.mvar;
			}
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00086C02 File Offset: 0x00084E02
		public static TypeSpec GetMemberDeclaringType(TypeSpec type)
		{
			if (type is InflatedTypeSpec)
			{
				if (type.DeclaringType == null)
				{
					return type.GetDefinition();
				}
				type = MemberCache.GetMember<TypeSpec>(TypeParameterMutator.GetMemberDeclaringType(type.DeclaringType), type);
			}
			return type;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00086C30 File Offset: 0x00084E30
		public TypeSpec Mutate(TypeSpec ts)
		{
			TypeSpec typeSpec;
			if (this.mutated_typespec != null && this.mutated_typespec.TryGetValue(ts, out typeSpec))
			{
				return typeSpec;
			}
			typeSpec = ts.Mutate(this);
			if (this.mutated_typespec == null)
			{
				this.mutated_typespec = new Dictionary<TypeSpec, TypeSpec>();
			}
			this.mutated_typespec.Add(ts, typeSpec);
			return typeSpec;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00086C80 File Offset: 0x00084E80
		public TypeParameterSpec Mutate(TypeParameterSpec tp)
		{
			if (this.mvar != null)
			{
				for (int i = 0; i < this.mvar.Count; i++)
				{
					if (this.mvar[i].Type == tp)
					{
						return this.var[i].Type;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.src.Length; j++)
				{
					if (this.src[j] == tp)
					{
						return this.var[j].Type;
					}
				}
			}
			return tp;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00086D04 File Offset: 0x00084F04
		public TypeSpec[] Mutate(TypeSpec[] targs)
		{
			TypeSpec[] array = new TypeSpec[targs.Length];
			bool flag = false;
			for (int i = 0; i < targs.Length; i++)
			{
				array[i] = this.Mutate(targs[i]);
				flag |= (targs[i] != array[i]);
			}
			if (!flag)
			{
				return targs;
			}
			return array;
		}

		// Token: 0x04000A53 RID: 2643
		private readonly TypeParameters mvar;

		// Token: 0x04000A54 RID: 2644
		private readonly TypeParameters var;

		// Token: 0x04000A55 RID: 2645
		private readonly TypeParameterSpec[] src;

		// Token: 0x04000A56 RID: 2646
		private Dictionary<TypeSpec, TypeSpec> mutated_typespec;
	}
}
