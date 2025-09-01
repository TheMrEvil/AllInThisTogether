using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000225 RID: 549
	public class TypeArguments
	{
		// Token: 0x06001BF3 RID: 7155 RVA: 0x000875AD File Offset: 0x000857AD
		public TypeArguments(params FullNamedExpression[] types)
		{
			this.args = new List<FullNamedExpression>(types);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000875C1 File Offset: 0x000857C1
		public void Add(FullNamedExpression type)
		{
			this.args.Add(type);
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000875CF File Offset: 0x000857CF
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x000875D7 File Offset: 0x000857D7
		public TypeSpec[] Arguments
		{
			get
			{
				return this.atypes;
			}
			set
			{
				this.atypes = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000875E0 File Offset: 0x000857E0
		public int Count
		{
			get
			{
				return this.args.Count;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x000875ED File Offset: 0x000857ED
		public List<FullNamedExpression> TypeExpressions
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000875F8 File Offset: 0x000857F8
		public string GetSignatureForError()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				FullNamedExpression fullNamedExpression = this.args[i];
				if (fullNamedExpression != null)
				{
					stringBuilder.Append(fullNamedExpression.GetSignatureForError());
				}
				if (i + 1 < this.Count)
				{
					stringBuilder.Append(',');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00087654 File Offset: 0x00085854
		public virtual bool Resolve(IMemberContext ec, bool allowUnbound)
		{
			if (this.atypes != null)
			{
				return true;
			}
			int count = this.args.Count;
			bool flag = true;
			this.atypes = new TypeSpec[count];
			int errors = ec.Module.Compiler.Report.Errors;
			for (int i = 0; i < count; i++)
			{
				TypeSpec typeSpec = this.args[i].ResolveAsType(ec, false);
				if (typeSpec == null)
				{
					flag = false;
				}
				else
				{
					this.atypes[i] = typeSpec;
					if (typeSpec.IsStatic)
					{
						ec.Module.Compiler.Report.Error(718, this.args[i].Location, "`{0}': static classes cannot be used as generic arguments", typeSpec.GetSignatureForError());
						flag = false;
					}
					if (typeSpec.IsPointer || typeSpec.IsSpecialRuntimeType)
					{
						ec.Module.Compiler.Report.Error(306, this.args[i].Location, "The type `{0}' may not be used as a type argument", typeSpec.GetSignatureForError());
						flag = false;
					}
				}
			}
			if (!flag || errors != ec.Module.Compiler.Report.Errors)
			{
				this.atypes = null;
			}
			return flag;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00087788 File Offset: 0x00085988
		public TypeArguments Clone()
		{
			TypeArguments typeArguments = new TypeArguments(new FullNamedExpression[0]);
			foreach (FullNamedExpression item in this.args)
			{
				typeArguments.args.Add(item);
			}
			return typeArguments;
		}

		// Token: 0x04000A5B RID: 2651
		private List<FullNamedExpression> args;

		// Token: 0x04000A5C RID: 2652
		private TypeSpec[] atypes;
	}
}
