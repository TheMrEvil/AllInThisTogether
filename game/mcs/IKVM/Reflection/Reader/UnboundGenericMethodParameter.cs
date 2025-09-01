using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000096 RID: 150
	internal sealed class UnboundGenericMethodParameter : TypeParameterType
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x000197E2 File Offset: 0x000179E2
		internal static Type Make(int position)
		{
			return UnboundGenericMethodParameter.module.universe.CanonicalizeType(new UnboundGenericMethodParameter(position));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000197F9 File Offset: 0x000179F9
		private UnboundGenericMethodParameter(int position) : base(30)
		{
			this.position = position;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001980C File Offset: 0x00017A0C
		public override bool Equals(object obj)
		{
			UnboundGenericMethodParameter unboundGenericMethodParameter = obj as UnboundGenericMethodParameter;
			return unboundGenericMethodParameter != null && unboundGenericMethodParameter.position == this.position;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00019839 File Offset: 0x00017A39
		public override int GetHashCode()
		{
			return this.position;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override string Namespace
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override string Name
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00019841 File Offset: 0x00017A41
		public override Module Module
		{
			get
			{
				return UnboundGenericMethodParameter.module;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00019839 File Offset: 0x00017A39
		public override int GenericParameterPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override MethodBase DeclaringMethod
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override Type[] GetGenericParameterConstraints()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override CustomModifiers[] __GetGenericParameterConstraintCustomModifiers()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00019848 File Offset: 0x00017A48
		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			return binder.BindMethodParameter(this);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal override bool IsBaked
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00019851 File Offset: 0x00017A51
		// Note: this type is marked as 'beforefieldinit'.
		static UnboundGenericMethodParameter()
		{
		}

		// Token: 0x04000312 RID: 786
		private static readonly UnboundGenericMethodParameter.DummyModule module = new UnboundGenericMethodParameter.DummyModule();

		// Token: 0x04000313 RID: 787
		private readonly int position;

		// Token: 0x0200033D RID: 829
		private sealed class DummyModule : NonPEModule
		{
			// Token: 0x060025E4 RID: 9700 RVA: 0x000B4EDD File Offset: 0x000B30DD
			internal DummyModule() : base(new Universe())
			{
			}

			// Token: 0x060025E5 RID: 9701 RVA: 0x0000AF7B File Offset: 0x0000917B
			protected override Exception NotSupportedException()
			{
				return new InvalidOperationException();
			}

			// Token: 0x060025E6 RID: 9702 RVA: 0x0000AF7B File Offset: 0x0000917B
			protected override Exception ArgumentOutOfRangeException()
			{
				return new InvalidOperationException();
			}

			// Token: 0x060025E7 RID: 9703 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override bool Equals(object obj)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060025E8 RID: 9704 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override int GetHashCode()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060025E9 RID: 9705 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override string ToString()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x060025EA RID: 9706 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override int MDStreamVersion
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700089C RID: 2204
			// (get) Token: 0x060025EB RID: 9707 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override Assembly Assembly
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060025EC RID: 9708 RVA: 0x00002CD4 File Offset: 0x00000ED4
			internal override Type FindType(TypeName typeName)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060025ED RID: 9709 RVA: 0x00002CD4 File Offset: 0x00000ED4
			internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060025EE RID: 9710 RVA: 0x00002CD4 File Offset: 0x00000ED4
			internal override void GetTypesImpl(List<Type> list)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x1700089D RID: 2205
			// (get) Token: 0x060025EF RID: 9711 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override string FullyQualifiedName
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700089E RID: 2206
			// (get) Token: 0x060025F0 RID: 9712 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override string Name
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700089F RID: 2207
			// (get) Token: 0x060025F1 RID: 9713 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override Guid ModuleVersionId
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x060025F2 RID: 9714 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override string ScopeName
			{
				get
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
