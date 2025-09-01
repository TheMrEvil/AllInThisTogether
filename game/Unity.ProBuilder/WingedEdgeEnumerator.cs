using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000067 RID: 103
	public sealed class WingedEdgeEnumerator : IEnumerator<WingedEdge>, IEnumerator, IDisposable
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00024978 File Offset: 0x00022B78
		public WingedEdgeEnumerator(WingedEdge start)
		{
			this.m_Start = start;
			this.m_Current = null;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00024990 File Offset: 0x00022B90
		public bool MoveNext()
		{
			if (this.m_Current == null)
			{
				this.m_Current = this.m_Start;
				return this.m_Current != null;
			}
			this.m_Current = this.m_Current.next;
			return this.m_Current != null && this.m_Current != this.m_Start;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000249E7 File Offset: 0x00022BE7
		public void Reset()
		{
			this.m_Current = null;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000249F0 File Offset: 0x00022BF0
		public WingedEdge Current
		{
			get
			{
				WingedEdge current;
				try
				{
					current = this.m_Current;
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
				return current;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00024A20 File Offset: 0x00022C20
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00024A28 File Offset: 0x00022C28
		public void Dispose()
		{
		}

		// Token: 0x0400022C RID: 556
		private WingedEdge m_Start;

		// Token: 0x0400022D RID: 557
		private WingedEdge m_Current;
	}
}
