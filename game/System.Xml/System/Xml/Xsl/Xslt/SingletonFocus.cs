using System;
using System.Diagnostics;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E1 RID: 993
	internal struct SingletonFocus : IFocus
	{
		// Token: 0x0600278E RID: 10126 RVA: 0x000EB48B File Offset: 0x000E968B
		public SingletonFocus(XPathQilFactory f)
		{
			this.f = f;
			this.focusType = SingletonFocusType.None;
			this.current = null;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000EB4A2 File Offset: 0x000E96A2
		public void SetFocus(SingletonFocusType focusType)
		{
			this.focusType = focusType;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000EB4AB File Offset: 0x000E96AB
		public void SetFocus(QilIterator current)
		{
			if (current != null)
			{
				this.focusType = SingletonFocusType.Iterator;
				this.current = current;
				return;
			}
			this.focusType = SingletonFocusType.None;
			this.current = null;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckFocus()
		{
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000EB4D0 File Offset: 0x000E96D0
		public QilNode GetCurrent()
		{
			SingletonFocusType singletonFocusType = this.focusType;
			if (singletonFocusType == SingletonFocusType.InitialDocumentNode)
			{
				return this.f.Root(this.f.XmlContext());
			}
			if (singletonFocusType != SingletonFocusType.InitialContextNode)
			{
				return this.current;
			}
			return this.f.XmlContext();
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000EB517 File Offset: 0x000E9717
		public QilNode GetPosition()
		{
			return this.f.Double(1.0);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000EB517 File Offset: 0x000E9717
		public QilNode GetLast()
		{
			return this.f.Double(1.0);
		}

		// Token: 0x04001F04 RID: 7940
		private XPathQilFactory f;

		// Token: 0x04001F05 RID: 7941
		private SingletonFocusType focusType;

		// Token: 0x04001F06 RID: 7942
		private QilIterator current;
	}
}
