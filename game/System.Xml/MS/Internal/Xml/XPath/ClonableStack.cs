using System;
using System.Collections.Generic;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000621 RID: 1569
	internal sealed class ClonableStack<T> : List<T>
	{
		// Token: 0x06004042 RID: 16450 RVA: 0x001641F5 File Offset: 0x001623F5
		public ClonableStack()
		{
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x001641FD File Offset: 0x001623FD
		private ClonableStack(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x00164206 File Offset: 0x00162406
		public void Push(T value)
		{
			base.Add(value);
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x00164210 File Offset: 0x00162410
		public T Pop()
		{
			int index = base.Count - 1;
			T result = base[index];
			base.RemoveAt(index);
			return result;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x00164234 File Offset: 0x00162434
		public T Peek()
		{
			return base[base.Count - 1];
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x00164244 File Offset: 0x00162444
		public ClonableStack<T> Clone()
		{
			return new ClonableStack<T>(this);
		}
	}
}
