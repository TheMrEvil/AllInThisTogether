using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E2 RID: 994
	internal struct FunctionFocus : IFocus
	{
		// Token: 0x06002795 RID: 10133 RVA: 0x000EB530 File Offset: 0x000E9730
		public void StartFocus(IList<QilNode> args, XslFlags flags)
		{
			int num = 0;
			if ((flags & XslFlags.Current) != XslFlags.None)
			{
				this.current = (QilParameter)args[num++];
			}
			if ((flags & XslFlags.Position) != XslFlags.None)
			{
				this.position = (QilParameter)args[num++];
			}
			if ((flags & XslFlags.Last) != XslFlags.None)
			{
				this.last = (QilParameter)args[num++];
			}
			this.isSet = true;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000EB5A4 File Offset: 0x000E97A4
		public void StopFocus()
		{
			this.isSet = false;
			this.current = (this.position = (this.last = null));
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000EB5D1 File Offset: 0x000E97D1
		public bool IsFocusSet
		{
			get
			{
				return this.isSet;
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000EB5D9 File Offset: 0x000E97D9
		public QilNode GetCurrent()
		{
			return this.current;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000EB5E1 File Offset: 0x000E97E1
		public QilNode GetPosition()
		{
			return this.position;
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000EB5E9 File Offset: 0x000E97E9
		public QilNode GetLast()
		{
			return this.last;
		}

		// Token: 0x04001F07 RID: 7943
		private bool isSet;

		// Token: 0x04001F08 RID: 7944
		private QilParameter current;

		// Token: 0x04001F09 RID: 7945
		private QilParameter position;

		// Token: 0x04001F0A RID: 7946
		private QilParameter last;
	}
}
