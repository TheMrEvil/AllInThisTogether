using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000F2 RID: 242
	internal class EmptyEnumerator<T> : QueryOperatorEnumerator<T, int>, IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool MoveNext(ref T currentElement, ref int currentKey)
		{
			return false;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001D07C File Offset: 0x0001B27C
		public T Current
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0000392D File Offset: 0x00001B2D
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000023D1 File Offset: 0x000005D1
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00003A59 File Offset: 0x00001C59
		void IEnumerator.Reset()
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001D092 File Offset: 0x0001B292
		public EmptyEnumerator()
		{
		}
	}
}
