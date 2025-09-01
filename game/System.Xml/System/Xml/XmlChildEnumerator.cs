using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001B8 RID: 440
	internal sealed class XmlChildEnumerator : IEnumerator
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0006776D File Offset: 0x0006596D
		internal XmlChildEnumerator(XmlNode container)
		{
			this.container = container;
			this.child = container.FirstChild;
			this.isFirst = true;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0006778F File Offset: 0x0006598F
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00067798 File Offset: 0x00065998
		internal bool MoveNext()
		{
			if (this.isFirst)
			{
				this.child = this.container.FirstChild;
				this.isFirst = false;
			}
			else if (this.child != null)
			{
				this.child = this.child.NextSibling;
			}
			return this.child != null;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000677E9 File Offset: 0x000659E9
		void IEnumerator.Reset()
		{
			this.isFirst = true;
			this.child = this.container.FirstChild;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00067803 File Offset: 0x00065A03
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0006780B File Offset: 0x00065A0B
		internal XmlNode Current
		{
			get
			{
				if (this.isFirst || this.child == null)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				return this.child;
			}
		}

		// Token: 0x0400103D RID: 4157
		internal XmlNode container;

		// Token: 0x0400103E RID: 4158
		internal XmlNode child;

		// Token: 0x0400103F RID: 4159
		internal bool isFirst;
	}
}
