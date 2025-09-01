using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000407 RID: 1031
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class ArraySubsetEnumerator : IEnumerator
	{
		// Token: 0x06002155 RID: 8533 RVA: 0x000721B6 File Offset: 0x000703B6
		public ArraySubsetEnumerator(Array array, int count)
		{
			this.array = array;
			this.total = count;
			this.current = -1;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000721D3 File Offset: 0x000703D3
		public bool MoveNext()
		{
			if (this.current < this.total - 1)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000721F6 File Offset: 0x000703F6
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000721FF File Offset: 0x000703FF
		public object Current
		{
			get
			{
				if (this.current == -1)
				{
					throw new InvalidOperationException();
				}
				return this.array.GetValue(this.current);
			}
		}

		// Token: 0x04000FFE RID: 4094
		private Array array;

		// Token: 0x04000FFF RID: 4095
		private int total;

		// Token: 0x04001000 RID: 4096
		private int current;
	}
}
