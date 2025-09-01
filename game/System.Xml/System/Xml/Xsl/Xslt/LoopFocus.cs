using System;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E3 RID: 995
	internal struct LoopFocus : IFocus
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x000EB5F4 File Offset: 0x000E97F4
		public LoopFocus(XPathQilFactory f)
		{
			this.f = f;
			this.current = (this.cached = (this.last = null));
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000EB624 File Offset: 0x000E9824
		public void SetFocus(QilIterator current)
		{
			this.current = current;
			this.cached = (this.last = null);
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x000EB648 File Offset: 0x000E9848
		public bool IsFocusSet
		{
			get
			{
				return this.current != null;
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000EB653 File Offset: 0x000E9853
		public QilNode GetCurrent()
		{
			return this.current;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000EB65B File Offset: 0x000E985B
		public QilNode GetPosition()
		{
			return this.f.XsltConvert(this.f.PositionOf(this.current), XmlQueryTypeFactory.DoubleX);
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000EB67E File Offset: 0x000E987E
		public QilNode GetLast()
		{
			if (this.last == null)
			{
				this.last = this.f.Let(this.f.Double(0.0));
			}
			return this.last;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000EB6B3 File Offset: 0x000E98B3
		public void EnsureCache()
		{
			if (this.cached == null)
			{
				this.cached = this.f.Let(this.current.Binding);
				this.current.Binding = this.cached;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000EB6EA File Offset: 0x000E98EA
		public void Sort(QilNode sortKeys)
		{
			if (sortKeys != null)
			{
				this.EnsureCache();
				this.current = this.f.For(this.f.Sort(this.current, sortKeys));
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000EB718 File Offset: 0x000E9918
		public QilLoop ConstructLoop(QilNode body)
		{
			if (this.last != null)
			{
				this.EnsureCache();
				this.last.Binding = this.f.XsltConvert(this.f.Length(this.cached), XmlQueryTypeFactory.DoubleX);
			}
			QilLoop qilLoop = this.f.BaseFactory.Loop(this.current, body);
			if (this.last != null)
			{
				qilLoop = this.f.BaseFactory.Loop(this.last, qilLoop);
			}
			if (this.cached != null)
			{
				qilLoop = this.f.BaseFactory.Loop(this.cached, qilLoop);
			}
			return qilLoop;
		}

		// Token: 0x04001F0B RID: 7947
		private XPathQilFactory f;

		// Token: 0x04001F0C RID: 7948
		private QilIterator current;

		// Token: 0x04001F0D RID: 7949
		private QilIterator cached;

		// Token: 0x04001F0E RID: 7950
		private QilIterator last;
	}
}
