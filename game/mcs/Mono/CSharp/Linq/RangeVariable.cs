using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002EC RID: 748
	public sealed class RangeVariable : INamedBlockVariable
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x000AD458 File Offset: 0x000AB658
		public RangeVariable(string name, Location loc)
		{
			this.Name = name;
			this.Location = loc;
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000AD46E File Offset: 0x000AB66E
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x000AD476 File Offset: 0x000AB676
		public Block Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0000212D File Offset: 0x0000032D
		public bool IsDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x000AD47F File Offset: 0x000AB67F
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x000AD487 File Offset: 0x000AB687
		public Location Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Location>k__BackingField = value;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000AD490 File Offset: 0x000AB690
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x000AD498 File Offset: 0x000AB698
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000AD4A4 File Offset: 0x000AB6A4
		public Expression CreateReferenceExpression(ResolveContext rc, Location loc)
		{
			ParametersBlock parametersBlock = rc.CurrentBlock.ParametersBlock;
			int i;
			Expression expression;
			for (;;)
			{
				if (parametersBlock is QueryBlock)
				{
					for (i = parametersBlock.Parameters.Count - 1; i >= 0; i--)
					{
						Parameter parameter = parametersBlock.Parameters[i];
						if (parameter.Name == this.Name)
						{
							goto Block_2;
						}
						expression = null;
						for (QueryBlock.TransparentParameter transparentParameter = parameter as QueryBlock.TransparentParameter; transparentParameter != null; transparentParameter = (transparentParameter.Parent as QueryBlock.TransparentParameter))
						{
							if (expression == null)
							{
								expression = parametersBlock.GetParameterReference(i, loc);
							}
							else
							{
								expression = new TransparentMemberAccess(expression, transparentParameter.Name);
							}
							if (transparentParameter.Identifier == this.Name)
							{
								goto Block_4;
							}
							if (transparentParameter.Parent.Name == this.Name)
							{
								goto Block_5;
							}
						}
					}
				}
				if (parametersBlock == this.block)
				{
					goto Block_7;
				}
				parametersBlock = parametersBlock.Parent.ParametersBlock;
			}
			Block_2:
			return parametersBlock.GetParameterReference(i, loc);
			Block_4:
			return new TransparentMemberAccess(expression, this.Name);
			Block_5:
			return new TransparentMemberAccess(expression, this.Name);
			Block_7:
			return null;
		}

		// Token: 0x04000D83 RID: 3459
		private Block block;

		// Token: 0x04000D84 RID: 3460
		[CompilerGenerated]
		private Location <Location>k__BackingField;

		// Token: 0x04000D85 RID: 3461
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
