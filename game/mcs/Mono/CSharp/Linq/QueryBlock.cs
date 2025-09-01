using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F9 RID: 761
	public class QueryBlock : ParametersBlock
	{
		// Token: 0x0600243B RID: 9275 RVA: 0x000ADAC5 File Offset: 0x000ABCC5
		public QueryBlock(Block parent, Location start) : base(parent, ParametersCompiled.EmptyReadOnlyParameters, start, Block.Flags.CompilerGenerated)
		{
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000ADAD9 File Offset: 0x000ABCD9
		public void AddRangeVariable(RangeVariable variable)
		{
			variable.Block = this;
			base.TopBlock.AddLocalName(variable.Name, variable, true);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000ADAF5 File Offset: 0x000ABCF5
		public override void Error_AlreadyDeclared(string name, INamedBlockVariable variable, string reason)
		{
			base.TopBlock.Report.Error(1931, variable.Location, "A range variable `{0}' conflicts with a previous declaration of `{0}'", name);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000ADB18 File Offset: 0x000ABD18
		public override void Error_AlreadyDeclared(string name, INamedBlockVariable variable)
		{
			base.TopBlock.Report.Error(1930, variable.Location, "A range variable `{0}' has already been declared in this scope", name);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000ADB3B File Offset: 0x000ABD3B
		public override void Error_AlreadyDeclaredTypeParameter(string name, Location loc)
		{
			base.TopBlock.Report.Error(1948, loc, "A range variable `{0}' conflicts with a method type parameter", name);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000ADB59 File Offset: 0x000ABD59
		public void SetParameter(Parameter parameter)
		{
			this.parameters = new ParametersCompiled(new Parameter[]
			{
				parameter
			});
			this.parameter_info = new ParametersBlock.ParameterInfo[]
			{
				new ParametersBlock.ParameterInfo(this, 0)
			};
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000ADB86 File Offset: 0x000ABD86
		public void SetParameters(Parameter first, Parameter second)
		{
			this.parameters = new ParametersCompiled(new Parameter[]
			{
				first,
				second
			});
			this.parameter_info = new ParametersBlock.ParameterInfo[]
			{
				new ParametersBlock.ParameterInfo(this, 0),
				new ParametersBlock.ParameterInfo(this, 1)
			};
		}

		// Token: 0x0200040F RID: 1039
		public sealed class TransparentParameter : ImplicitLambdaParameter
		{
			// Token: 0x06002857 RID: 10327 RVA: 0x000BF456 File Offset: 0x000BD656
			public TransparentParameter(Parameter parent, RangeVariable identifier) : base("<>__TranspIdent" + QueryBlock.TransparentParameter.Counter++, identifier.Location)
			{
				this.Parent = parent;
				this.Identifier = identifier.Name;
			}

			// Token: 0x06002858 RID: 10328 RVA: 0x000BF493 File Offset: 0x000BD693
			public static void Reset()
			{
				QueryBlock.TransparentParameter.Counter = 0;
			}

			// Token: 0x04001190 RID: 4496
			public static int Counter;

			// Token: 0x04001191 RID: 4497
			private const string ParameterNamePrefix = "<>__TranspIdent";

			// Token: 0x04001192 RID: 4498
			public readonly Parameter Parent;

			// Token: 0x04001193 RID: 4499
			public readonly string Identifier;
		}
	}
}
