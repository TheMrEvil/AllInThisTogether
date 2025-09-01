using System;

namespace Mono.CSharp
{
	// Token: 0x020001BA RID: 442
	public class NamespaceExpression : FullNamedExpression
	{
		// Token: 0x06001713 RID: 5907 RVA: 0x0006E12D File Offset: 0x0006C32D
		public NamespaceExpression(Namespace ns, Location loc)
		{
			this.ns = ns;
			base.Type = InternalType.Namespace;
			this.eclass = ExprClass.Namespace;
			this.loc = loc;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x0006E155 File Offset: 0x0006C355
		public Namespace Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00023DF4 File Offset: 0x00021FF4
		protected override Expression DoResolve(ResolveContext rc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00005936 File Offset: 0x00003B36
		public override FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext mc, bool allowUnboundTypeArguments)
		{
			return this;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0006E160 File Offset: 0x0006C360
		public void Error_NamespaceDoesNotExist(IMemberContext ctx, string name, int arity, Location loc)
		{
			TypeSpec typeSpec = this.Namespace.LookupType(ctx, name, arity, LookupMode.IgnoreAccessibility, loc);
			if (typeSpec != null)
			{
				Expression.ErrorIsInaccesible(ctx, typeSpec.GetSignatureForError(), loc);
				return;
			}
			typeSpec = this.Namespace.LookupType(ctx, name, -Math.Max(1, arity), LookupMode.Probing, loc);
			if (typeSpec != null)
			{
				base.Error_TypeArgumentsCannotBeUsed(ctx, typeSpec, loc);
				return;
			}
			Namespace @namespace;
			if (arity > 0 && this.Namespace.TryGetNamespace(name, out @namespace))
			{
				Expression.Error_TypeArgumentsCannotBeUsed(ctx, this.ExprClassName, @namespace.GetSignatureForError(), loc);
				return;
			}
			string text = null;
			string text2 = this.Namespace.GetSignatureForError() + "." + name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
			if (num > 2161857712U)
			{
				if (num <= 3301415309U)
				{
					if (num <= 2823335057U)
					{
						if (num != 2205724446U)
						{
							if (num != 2823335057U)
							{
								goto IL_36C;
							}
							if (!(text2 == "System.Linq.Expressions"))
							{
								goto IL_36C;
							}
						}
						else
						{
							if (!(text2 == "System.Numerics"))
							{
								goto IL_36C;
							}
							goto IL_35A;
						}
					}
					else if (num != 2968983462U)
					{
						if (num != 3016133270U)
						{
							if (num != 3301415309U)
							{
								goto IL_36C;
							}
							if (!(text2 == "System.Runtime.Caching"))
							{
								goto IL_36C;
							}
							goto IL_35A;
						}
						else
						{
							if (!(text2 == "System.Data"))
							{
								goto IL_36C;
							}
							goto IL_35A;
						}
					}
					else
					{
						if (!(text2 == "System.Web.Services"))
						{
							goto IL_36C;
						}
						goto IL_35A;
					}
				}
				else if (num <= 3462746850U)
				{
					if (num != 3378336992U)
					{
						if (num != 3462746850U)
						{
							goto IL_36C;
						}
						if (!(text2 == "System.ServiceModel"))
						{
							goto IL_36C;
						}
						goto IL_35A;
					}
					else
					{
						if (!(text2 == "System.Web"))
						{
							goto IL_36C;
						}
						goto IL_35A;
					}
				}
				else if (num != 3498418688U)
				{
					if (num != 3723126785U)
					{
						if (num != 3725550837U)
						{
							goto IL_36C;
						}
						if (!(text2 == "System.Net.Http"))
						{
							goto IL_36C;
						}
						goto IL_35A;
					}
					else
					{
						if (!(text2 == "System.Xml.Linq"))
						{
							goto IL_36C;
						}
						goto IL_35A;
					}
				}
				else if (!(text2 == "System.Linq"))
				{
					goto IL_36C;
				}
				text = "System.Core";
				goto IL_36C;
			}
			if (num <= 1221886074U)
			{
				if (num <= 131786028U)
				{
					if (num != 35798259U)
					{
						if (num != 131786028U)
						{
							goto IL_36C;
						}
						if (!(text2 == "System.Web.Routing"))
						{
							goto IL_36C;
						}
					}
					else if (!(text2 == "System.Transactions"))
					{
						goto IL_36C;
					}
				}
				else
				{
					if (num != 665970248U)
					{
						if (num != 937460914U)
						{
							if (num != 1221886074U)
							{
								goto IL_36C;
							}
							if (!(text2 == "System.Windows.Forms.Layout"))
							{
								goto IL_36C;
							}
						}
						else if (!(text2 == "System.Windows.Forms"))
						{
							goto IL_36C;
						}
						text = "System.Windows.Forms";
						goto IL_36C;
					}
					if (!(text2 == "System.Drawing"))
					{
						goto IL_36C;
					}
				}
			}
			else if (num <= 1560674155U)
			{
				if (num != 1341696477U)
				{
					if (num != 1560674155U)
					{
						goto IL_36C;
					}
					if (!(text2 == "System.Xml"))
					{
						goto IL_36C;
					}
				}
				else if (!(text2 == "System.DirectoryServices"))
				{
					goto IL_36C;
				}
			}
			else if (num != 1692796700U)
			{
				if (num != 1893461208U)
				{
					if (num != 2161857712U)
					{
						goto IL_36C;
					}
					if (!(text2 == "System.Json"))
					{
						goto IL_36C;
					}
				}
				else if (!(text2 == "System.Data.Services"))
				{
					goto IL_36C;
				}
			}
			else if (!(text2 == "System.Configuration"))
			{
				goto IL_36C;
			}
			IL_35A:
			text = text2;
			IL_36C:
			text = ((text == null) ? "an" : ("`" + text + "'"));
			if (this.Namespace is GlobalRootNamespace)
			{
				ctx.Module.Compiler.Report.Error(400, loc, "The type or namespace name `{0}' could not be found in the global namespace. Are you missing {1} assembly reference?", name, text);
				return;
			}
			ctx.Module.Compiler.Report.Error(234, loc, "The type or namespace name `{0}' does not exist in the namespace `{1}'. Are you missing {2} assembly reference?", new string[]
			{
				name,
				this.GetSignatureForError(),
				text
			});
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0006E55D File Offset: 0x0006C75D
		public override string GetSignatureForError()
		{
			return this.ns.GetSignatureForError();
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0006E56A File Offset: 0x0006C76A
		public FullNamedExpression LookupTypeOrNamespace(IMemberContext ctx, string name, int arity, LookupMode mode, Location loc)
		{
			return this.ns.LookupTypeOrNamespace(ctx, name, arity, mode, loc);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0006E57E File Offset: 0x0006C77E
		public override string ToString()
		{
			return this.Namespace.Name;
		}

		// Token: 0x04000961 RID: 2401
		private readonly Namespace ns;
	}
}
