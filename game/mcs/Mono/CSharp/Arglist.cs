using System;

namespace Mono.CSharp
{
	// Token: 0x020001EF RID: 495
	public class Arglist : Expression
	{
		// Token: 0x060019E8 RID: 6632 RVA: 0x0007F9CE File Offset: 0x0007DBCE
		public Arglist(Location loc) : this(null, loc)
		{
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0007F9D8 File Offset: 0x0007DBD8
		public Arglist(Arguments args, Location l)
		{
			this.arguments = args;
			this.loc = l;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x0007F9EE File Offset: 0x0007DBEE
		public Arguments Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0007F9F8 File Offset: 0x0007DBF8
		public Type[] ArgumentTypes
		{
			get
			{
				if (this.arguments == null)
				{
					return System.Type.EmptyTypes;
				}
				Type[] array = new Type[this.arguments.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.arguments[i].Expr.Type.GetMetaInfo();
				}
				return array;
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override bool ContainsEmitWithAwait()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0007FA51 File Offset: 0x0007DC51
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(1952, this.loc, "An expression tree cannot contain a method with variable arguments");
			return null;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0007FA70 File Offset: 0x0007DC70
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Variable;
			this.type = InternalType.Arglist;
			if (this.arguments != null)
			{
				bool flag;
				this.arguments.Resolve(ec, out flag);
			}
			return this;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0007FAA6 File Offset: 0x0007DCA6
		public override void Emit(EmitContext ec)
		{
			if (this.arguments != null)
			{
				this.arguments.Emit(ec);
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0007FABC File Offset: 0x0007DCBC
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Arglist arglist = (Arglist)t;
			if (this.arguments != null)
			{
				arglist.arguments = this.arguments.Clone(clonectx);
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0007FAEA File Offset: 0x0007DCEA
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009DF RID: 2527
		private Arguments arguments;
	}
}
