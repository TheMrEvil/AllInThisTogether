using System;

namespace Mono.CSharp
{
	// Token: 0x020002B0 RID: 688
	public class BlockVariableDeclarator
	{
		// Token: 0x060020EB RID: 8427 RVA: 0x000A159B File Offset: 0x0009F79B
		public BlockVariableDeclarator(LocalVariable li, Expression initializer)
		{
			if (li.Type != null)
			{
				throw new ArgumentException("Expected null variable type");
			}
			this.li = li;
			this.initializer = initializer;
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x000A15C4 File Offset: 0x0009F7C4
		public LocalVariable Variable
		{
			get
			{
				return this.li;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x000A15CC File Offset: 0x0009F7CC
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x000A15D4 File Offset: 0x0009F7D4
		public Expression Initializer
		{
			get
			{
				return this.initializer;
			}
			set
			{
				this.initializer = value;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000A15E0 File Offset: 0x0009F7E0
		public virtual BlockVariableDeclarator Clone(CloneContext cloneCtx)
		{
			BlockVariableDeclarator blockVariableDeclarator = (BlockVariableDeclarator)base.MemberwiseClone();
			if (this.initializer != null)
			{
				blockVariableDeclarator.initializer = this.initializer.Clone(cloneCtx);
			}
			return blockVariableDeclarator;
		}

		// Token: 0x04000C3B RID: 3131
		private LocalVariable li;

		// Token: 0x04000C3C RID: 3132
		private Expression initializer;
	}
}
