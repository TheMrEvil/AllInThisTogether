using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML processing instruction.</summary>
	// Token: 0x0200005F RID: 95
	public class XProcessingInstruction : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class.</summary>
		/// <param name="target">A <see cref="T:System.String" /> containing the target application for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <param name="data">The string data for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="target" /> or <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		// Token: 0x0600038C RID: 908 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public XProcessingInstruction(string target, string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			XProcessingInstruction.ValidateName(target);
			this.target = target;
			this.data = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XProcessingInstruction" /> node to copy from.</param>
		// Token: 0x0600038D RID: 909 RVA: 0x000101CE File Offset: 0x0000E3CE
		public XProcessingInstruction(XProcessingInstruction other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.target = other.target;
			this.data = other.data;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000101FC File Offset: 0x0000E3FC
		internal XProcessingInstruction(XmlReader r)
		{
			this.target = r.Name;
			this.data = r.Value;
			r.Read();
		}

		/// <summary>Gets or sets the string value of this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00010223 File Offset: 0x0000E423
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0001022B File Offset: 0x0000E42B
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.data = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XProcessingInstruction" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.ProcessingInstruction" />.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001025D File Offset: 0x0000E45D
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Gets or sets a string containing the target application for this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the target application for this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00010260 File Offset: 0x0000E460
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00010268 File Offset: 0x0000E468
		public string Target
		{
			get
			{
				return this.target;
			}
			set
			{
				XProcessingInstruction.ValidateName(value);
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Name);
				this.target = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		/// <summary>Writes this processing instruction to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write this processing instruction to.</param>
		// Token: 0x06000394 RID: 916 RVA: 0x00010292 File Offset: 0x0000E492
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteProcessingInstruction(this.target, this.data);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000102B4 File Offset: 0x0000E4B4
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
			return writer.WriteProcessingInstructionAsync(this.target, this.data);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000102E6 File Offset: 0x0000E4E6
		internal override XNode CloneNode()
		{
			return new XProcessingInstruction(this);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000102F0 File Offset: 0x0000E4F0
		internal override bool DeepEquals(XNode node)
		{
			XProcessingInstruction xprocessingInstruction = node as XProcessingInstruction;
			return xprocessingInstruction != null && this.target == xprocessingInstruction.target && this.data == xprocessingInstruction.data;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001032D File Offset: 0x0000E52D
		internal override int GetDeepHashCode()
		{
			return this.target.GetHashCode() ^ this.data.GetHashCode();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00010346 File Offset: 0x0000E546
		private static void ValidateName(string name)
		{
			XmlConvert.VerifyNCName(name);
			if (string.Equals(name, "xml", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(SR.Format("'{0}' is an invalid name for a processing instruction.", name));
			}
		}

		// Token: 0x040001DE RID: 478
		internal string target;

		// Token: 0x040001DF RID: 479
		internal string data;
	}
}
