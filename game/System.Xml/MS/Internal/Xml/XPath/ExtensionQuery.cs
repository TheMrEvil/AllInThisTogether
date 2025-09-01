using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200062B RID: 1579
	internal abstract class ExtensionQuery : Query
	{
		// Token: 0x06004086 RID: 16518 RVA: 0x001648EE File Offset: 0x00162AEE
		public ExtensionQuery(string prefix, string name)
		{
			this.prefix = prefix;
			this.name = name;
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x00164904 File Offset: 0x00162B04
		protected ExtensionQuery(ExtensionQuery other) : base(other)
		{
			this.prefix = other.prefix;
			this.name = other.name;
			this.xsltContext = other.xsltContext;
			this._queryIterator = (ResetableIterator)Query.Clone(other._queryIterator);
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x00164952 File Offset: 0x00162B52
		public override void Reset()
		{
			if (this._queryIterator != null)
			{
				this._queryIterator.Reset();
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x00164967 File Offset: 0x00162B67
		public override XPathNavigator Current
		{
			get
			{
				if (this._queryIterator == null)
				{
					throw XPathException.Create("Expression must evaluate to a node-set.");
				}
				if (this._queryIterator.CurrentPosition == 0)
				{
					this.Advance();
				}
				return this._queryIterator.Current;
			}
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0016499B File Offset: 0x00162B9B
		public override XPathNavigator Advance()
		{
			if (this._queryIterator == null)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
			if (this._queryIterator.MoveNext())
			{
				return this._queryIterator.Current;
			}
			return null;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x001649CA File Offset: 0x00162BCA
		public override int CurrentPosition
		{
			get
			{
				if (this._queryIterator != null)
				{
					return this._queryIterator.CurrentPosition;
				}
				return 0;
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x001649E4 File Offset: 0x00162BE4
		protected object ProcessResult(object value)
		{
			if (value is string)
			{
				return value;
			}
			if (value is double)
			{
				return value;
			}
			if (value is bool)
			{
				return value;
			}
			if (value is XPathNavigator)
			{
				return value;
			}
			if (value is int)
			{
				return (double)((int)value);
			}
			if (value == null)
			{
				this._queryIterator = XPathEmptyIterator.Instance;
				return this;
			}
			ResetableIterator resetableIterator = value as ResetableIterator;
			if (resetableIterator != null)
			{
				this._queryIterator = (ResetableIterator)resetableIterator.Clone();
				return this;
			}
			XPathNodeIterator xpathNodeIterator = value as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				this._queryIterator = new XPathArrayIterator(xpathNodeIterator);
				return this;
			}
			IXPathNavigable ixpathNavigable = value as IXPathNavigable;
			if (ixpathNavigable != null)
			{
				return ixpathNavigable.CreateNavigator();
			}
			if (value is short)
			{
				return (double)((short)value);
			}
			if (value is long)
			{
				return (double)((long)value);
			}
			if (value is uint)
			{
				return (uint)value;
			}
			if (value is ushort)
			{
				return (double)((ushort)value);
			}
			if (value is ulong)
			{
				return (ulong)value;
			}
			if (value is float)
			{
				return (double)((float)value);
			}
			if (value is decimal)
			{
				return (double)((decimal)value);
			}
			return value.ToString();
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x00164B24 File Offset: 0x00162D24
		protected string QName
		{
			get
			{
				if (this.prefix.Length == 0)
				{
					return this.name;
				}
				return this.prefix + ":" + this.name;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x00164B50 File Offset: 0x00162D50
		public override int Count
		{
			get
			{
				if (this._queryIterator != null)
				{
					return this._queryIterator.Count;
				}
				return 1;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x0006AB76 File Offset: 0x00068D76
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x04002E10 RID: 11792
		protected string prefix;

		// Token: 0x04002E11 RID: 11793
		protected string name;

		// Token: 0x04002E12 RID: 11794
		protected XsltContext xsltContext;

		// Token: 0x04002E13 RID: 11795
		private ResetableIterator _queryIterator;
	}
}
