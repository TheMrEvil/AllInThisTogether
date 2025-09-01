using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Xsl
{
	// Token: 0x02000328 RID: 808
	internal struct IListEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06002142 RID: 8514 RVA: 0x000D2958 File Offset: 0x000D0B58
		public IListEnumerator(IList<T> sequence)
		{
			this.sequence = sequence;
			this.index = 0;
			this.current = default(T);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0000B528 File Offset: 0x00009728
		public void Dispose()
		{
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x000D2974 File Offset: 0x000D0B74
		public T Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x000D297C File Offset: 0x000D0B7C
		object IEnumerator.Current
		{
			get
			{
				if (this.index == 0)
				{
					throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[]
					{
						string.Empty
					}));
				}
				if (this.index > this.sequence.Count)
				{
					throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[]
					{
						string.Empty
					}));
				}
				return this.current;
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000D29EC File Offset: 0x000D0BEC
		public bool MoveNext()
		{
			if (this.index < this.sequence.Count)
			{
				this.current = this.sequence[this.index];
				this.index++;
				return true;
			}
			this.current = default(T);
			return false;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000D2A40 File Offset: 0x000D0C40
		void IEnumerator.Reset()
		{
			this.index = 0;
			this.current = default(T);
		}

		// Token: 0x04001B88 RID: 7048
		private IList<T> sequence;

		// Token: 0x04001B89 RID: 7049
		private int index;

		// Token: 0x04001B8A RID: 7050
		private T current;
	}
}
