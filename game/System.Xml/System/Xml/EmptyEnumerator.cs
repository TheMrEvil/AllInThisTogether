using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001DE RID: 478
	internal sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0000B528 File Offset: 0x00009728
		void IEnumerator.Reset()
		{
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0000BB08 File Offset: 0x00009D08
		object IEnumerator.Current
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0000216B File Offset: 0x0000036B
		public EmptyEnumerator()
		{
		}
	}
}
