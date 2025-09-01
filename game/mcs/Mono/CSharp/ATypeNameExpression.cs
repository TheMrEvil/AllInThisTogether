using System;

namespace Mono.CSharp
{
	// Token: 0x020001B5 RID: 437
	public abstract class ATypeNameExpression : FullNamedExpression
	{
		// Token: 0x060016EA RID: 5866 RVA: 0x0006D4F7 File Offset: 0x0006B6F7
		protected ATypeNameExpression(string name, Location l)
		{
			this.name = name;
			this.loc = l;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0006D50D File Offset: 0x0006B70D
		protected ATypeNameExpression(string name, TypeArguments targs, Location l)
		{
			this.name = name;
			this.targs = targs;
			this.loc = l;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0006D52A File Offset: 0x0006B72A
		protected ATypeNameExpression(string name, int arity, Location l) : this(name, new UnboundTypeArguments(arity, l), l)
		{
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0006D53B File Offset: 0x0006B73B
		public int Arity
		{
			get
			{
				if (this.targs != null)
				{
					return this.targs.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0006D552 File Offset: 0x0006B752
		public bool HasTypeArguments
		{
			get
			{
				return this.targs != null && !this.targs.IsEmpty;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0006D56C File Offset: 0x0006B76C
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0006D574 File Offset: 0x0006B774
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0006D57D File Offset: 0x0006B77D
		public TypeArguments TypeArguments
		{
			get
			{
				return this.targs;
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0006D588 File Offset: 0x0006B788
		public override bool Equals(object obj)
		{
			ATypeNameExpression atypeNameExpression = obj as ATypeNameExpression;
			return atypeNameExpression != null && atypeNameExpression.Name == this.Name && (this.targs == null || this.targs.Equals(atypeNameExpression.targs));
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0006D5CF File Offset: 0x0006B7CF
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0006D5DC File Offset: 0x0006B7DC
		public static string GetMemberType(MemberCore mc)
		{
			if (mc is Property)
			{
				return "property";
			}
			if (mc is Indexer)
			{
				return "indexer";
			}
			if (mc is FieldBase)
			{
				return "field";
			}
			if (mc is MethodCore)
			{
				return "method";
			}
			if (mc is EnumMember)
			{
				return "enum";
			}
			if (mc is Event)
			{
				return "event";
			}
			return "type";
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0006D642 File Offset: 0x0006B842
		public override string GetSignatureForError()
		{
			if (this.targs != null)
			{
				return this.Name + "<" + this.targs.GetSignatureForError() + ">";
			}
			return this.Name;
		}

		// Token: 0x060016F6 RID: 5878
		public abstract Expression LookupNameExpression(ResolveContext rc, Expression.MemberLookupRestrictions restriction);

		// Token: 0x0400095F RID: 2399
		private string name;

		// Token: 0x04000960 RID: 2400
		protected TypeArguments targs;
	}
}
