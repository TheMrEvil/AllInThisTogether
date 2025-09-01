using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	/// <summary>Represents a text node.</summary>
	// Token: 0x02000061 RID: 97
	public class XText : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class.</summary>
		/// <param name="value">The <see cref="T:System.String" /> that contains the value of the <see cref="T:System.Xml.Linq.XText" /> node.</param>
		// Token: 0x060003AC RID: 940 RVA: 0x00010614 File Offset: 0x0000E814
		public XText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.text = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class from another <see cref="T:System.Xml.Linq.XText" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XText" /> node to copy from.</param>
		// Token: 0x060003AD RID: 941 RVA: 0x00010631 File Offset: 0x0000E831
		public XText(XText other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.text = other.text;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00010653 File Offset: 0x0000E853
		internal XText(XmlReader r)
		{
			this.text = r.Value;
			r.Read();
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XText" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Text" />.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001066E File Offset: 0x0000E86E
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		/// <summary>Gets or sets the value of this node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of this node.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00010671 File Offset: 0x0000E871
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00010679 File Offset: 0x0000E879
		public string Value
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.text = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		// Token: 0x060003B2 RID: 946 RVA: 0x000106AB File Offset: 0x0000E8AB
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this.parent is XDocument)
			{
				writer.WriteWhitespace(this.text);
				return;
			}
			writer.WriteString(this.text);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (!(this.parent is XDocument))
			{
				return writer.WriteStringAsync(this.text);
			}
			return writer.WriteWhitespaceAsync(this.text);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010735 File Offset: 0x0000E935
		internal override void AppendText(StringBuilder sb)
		{
			sb.Append(this.text);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010744 File Offset: 0x0000E944
		internal override XNode CloneNode()
		{
			return new XText(this);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001074C File Offset: 0x0000E94C
		internal override bool DeepEquals(XNode node)
		{
			return node != null && this.NodeType == node.NodeType && this.text == ((XText)node).text;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00010777 File Offset: 0x0000E977
		internal override int GetDeepHashCode()
		{
			return this.text.GetHashCode();
		}

		// Token: 0x040001E2 RID: 482
		internal string text;
	}
}
