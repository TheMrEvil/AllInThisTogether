using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200092A RID: 2346
	internal struct ILExceptionInfo
	{
		// Token: 0x06005092 RID: 20626 RVA: 0x000FB14D File Offset: 0x000F934D
		internal int NumHandlers()
		{
			return this.handlers.Length;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x000FB158 File Offset: 0x000F9358
		internal void AddCatch(Type extype, int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 0;
			this.handlers[num].start = offset;
			this.handlers[num].extype = extype;
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x000FB1B4 File Offset: 0x000F93B4
		internal void AddFinally(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 2;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x000FB210 File Offset: 0x000F9410
		internal void AddFault(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 4;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x000FB26C File Offset: 0x000F946C
		internal void AddFilter(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = -1;
			this.handlers[num].extype = null;
			this.handlers[num].filter_offset = offset;
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x000FB2C8 File Offset: 0x000F94C8
		internal void End(int offset)
		{
			if (this.handlers == null)
			{
				return;
			}
			int num = this.handlers.Length - 1;
			if (num >= 0)
			{
				this.handlers[num].len = offset - this.handlers[num].start;
			}
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x000FB311 File Offset: 0x000F9511
		internal int LastClauseType()
		{
			if (this.handlers != null)
			{
				return this.handlers[this.handlers.Length - 1].type;
			}
			return 0;
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x000FB338 File Offset: 0x000F9538
		internal void PatchFilterClause(int start)
		{
			if (this.handlers != null && this.handlers.Length != 0)
			{
				this.handlers[this.handlers.Length - 1].start = start;
				this.handlers[this.handlers.Length - 1].type = 1;
			}
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void Debug(int b)
		{
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x000FB38C File Offset: 0x000F958C
		private void add_block(int offset)
		{
			if (this.handlers != null)
			{
				int num = this.handlers.Length;
				ILExceptionBlock[] destinationArray = new ILExceptionBlock[num + 1];
				Array.Copy(this.handlers, destinationArray, num);
				this.handlers = destinationArray;
				this.handlers[num].len = offset - this.handlers[num].start;
				return;
			}
			this.handlers = new ILExceptionBlock[1];
			this.len = offset - this.start;
		}

		// Token: 0x04003189 RID: 12681
		internal ILExceptionBlock[] handlers;

		// Token: 0x0400318A RID: 12682
		internal int start;

		// Token: 0x0400318B RID: 12683
		internal int len;

		// Token: 0x0400318C RID: 12684
		internal Label end;
	}
}
