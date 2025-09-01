using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000476 RID: 1142
	internal struct XmlNavigatorStack
	{
		// Token: 0x06002C2B RID: 11307 RVA: 0x001060B0 File Offset: 0x001042B0
		public void Push(XPathNavigator nav)
		{
			if (this.stkNav == null)
			{
				this.stkNav = new XPathNavigator[8];
			}
			else if (this.sp >= this.stkNav.Length)
			{
				Array sourceArray = this.stkNav;
				this.stkNav = new XPathNavigator[2 * this.sp];
				Array.Copy(sourceArray, this.stkNav, this.sp);
			}
			XPathNavigator[] array = this.stkNav;
			int num = this.sp;
			this.sp = num + 1;
			array[num] = nav;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x00106128 File Offset: 0x00104328
		public XPathNavigator Pop()
		{
			XPathNavigator[] array = this.stkNav;
			int num = this.sp - 1;
			this.sp = num;
			return array[num];
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0010614D File Offset: 0x0010434D
		public XPathNavigator Peek()
		{
			return this.stkNav[this.sp - 1];
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0010615E File Offset: 0x0010435E
		public void Reset()
		{
			this.sp = 0;
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x00106167 File Offset: 0x00104367
		public bool IsEmpty
		{
			get
			{
				return this.sp == 0;
			}
		}

		// Token: 0x040022D7 RID: 8919
		private XPathNavigator[] stkNav;

		// Token: 0x040022D8 RID: 8920
		private int sp;

		// Token: 0x040022D9 RID: 8921
		private const int InitialStackSize = 8;
	}
}
