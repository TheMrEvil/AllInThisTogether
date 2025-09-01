using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000105 RID: 261
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	internal sealed class WordStorageDebugView
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x0001DA0F File Offset: 0x0001BC0F
		public WordStorageDebugView(WordStorage wordStorage)
		{
			this.m_wordStorage = wordStorage;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0001DA20 File Offset: 0x0001BC20
		public FixedString128Bytes[] Table
		{
			get
			{
				FixedString128Bytes[] array = new FixedString128Bytes[this.m_wordStorage.Entries];
				for (int i = 0; i < this.m_wordStorage.Entries; i++)
				{
					this.m_wordStorage.GetFixedString<FixedString128Bytes>(i, ref array[i]);
				}
				return array;
			}
		}

		// Token: 0x0400033B RID: 827
		private WordStorage m_wordStorage;
	}
}
