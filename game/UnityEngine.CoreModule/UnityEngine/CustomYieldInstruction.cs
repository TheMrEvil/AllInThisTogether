using System;
using System.Collections;

namespace UnityEngine
{
	// Token: 0x02000201 RID: 513
	public abstract class CustomYieldInstruction : IEnumerator
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060016B1 RID: 5809
		public abstract bool keepWaiting { get; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000244EC File Offset: 0x000226EC
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00024500 File Offset: 0x00022700
		public bool MoveNext()
		{
			return this.keepWaiting;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void Reset()
		{
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00002072 File Offset: 0x00000272
		protected CustomYieldInstruction()
		{
		}
	}
}
