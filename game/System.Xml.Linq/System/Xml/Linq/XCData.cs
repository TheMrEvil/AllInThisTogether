using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	/// <summary>Represents a text node that contains CDATA.</summary>
	// Token: 0x0200001B RID: 27
	public class XCData : XText
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class.</summary>
		/// <param name="value">A string that contains the value of the <see cref="T:System.Xml.Linq.XCData" /> node.</param>
		// Token: 0x060000F6 RID: 246 RVA: 0x0000586F File Offset: 0x00003A6F
		public XCData(string value) : base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XCData" /> node to copy from.</param>
		// Token: 0x060000F7 RID: 247 RVA: 0x00005878 File Offset: 0x00003A78
		public XCData(XCData other) : base(other)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005881 File Offset: 0x00003A81
		internal XCData(XmlReader r) : base(r)
		{
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XCData" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.CDATA" />.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000588A File Offset: 0x00003A8A
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		/// <summary>Writes this CDATA object to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		// Token: 0x060000FA RID: 250 RVA: 0x0000588D File Offset: 0x00003A8D
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteCData(this.text);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000058A9 File Offset: 0x00003AA9
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
			return writer.WriteCDataAsync(this.text);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000058D5 File Offset: 0x00003AD5
		internal override XNode CloneNode()
		{
			return new XCData(this);
		}
	}
}
