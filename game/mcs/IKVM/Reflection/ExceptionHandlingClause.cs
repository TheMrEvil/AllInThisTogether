using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000028 RID: 40
	public sealed class ExceptionHandlingClause
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000579C File Offset: 0x0000399C
		internal ExceptionHandlingClause(ModuleReader module, int flags, int tryOffset, int tryLength, int handlerOffset, int handlerLength, int classTokenOrfilterOffset, IGenericContext context)
		{
			this.flags = flags;
			this.tryOffset = tryOffset;
			this.tryLength = tryLength;
			this.handlerOffset = handlerOffset;
			this.handlerLength = handlerLength;
			this.catchType = ((flags == 0 && classTokenOrfilterOffset != 0) ? module.ResolveType(classTokenOrfilterOffset, context) : null);
			this.filterOffset = ((flags == 1) ? classTokenOrfilterOffset : 0);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000057FE File Offset: 0x000039FE
		public Type CatchType
		{
			get
			{
				return this.catchType;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005806 File Offset: 0x00003A06
		public int FilterOffset
		{
			get
			{
				return this.filterOffset;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000580E File Offset: 0x00003A0E
		public ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return (ExceptionHandlingClauseOptions)this.flags;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005816 File Offset: 0x00003A16
		public int HandlerLength
		{
			get
			{
				return this.handlerLength;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000581E File Offset: 0x00003A1E
		public int HandlerOffset
		{
			get
			{
				return this.handlerOffset;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005826 File Offset: 0x00003A26
		public int TryLength
		{
			get
			{
				return this.tryLength;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000582E File Offset: 0x00003A2E
		public int TryOffset
		{
			get
			{
				return this.tryOffset;
			}
		}

		// Token: 0x0400011A RID: 282
		private readonly int flags;

		// Token: 0x0400011B RID: 283
		private readonly int tryOffset;

		// Token: 0x0400011C RID: 284
		private readonly int tryLength;

		// Token: 0x0400011D RID: 285
		private readonly int handlerOffset;

		// Token: 0x0400011E RID: 286
		private readonly int handlerLength;

		// Token: 0x0400011F RID: 287
		private readonly Type catchType;

		// Token: 0x04000120 RID: 288
		private readonly int filterOffset;
	}
}
