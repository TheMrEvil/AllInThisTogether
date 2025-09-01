using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a processing instruction, which XML defines to keep processor-specific information in the text of the document.</summary>
	// Token: 0x020001D9 RID: 473
	public class XmlProcessingInstruction : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlProcessingInstruction" /> class.</summary>
		/// <param name="target">The target of the processing instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Target" /> property.</param>
		/// <param name="data">The content of the instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Data" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060012C6 RID: 4806 RVA: 0x000706E6 File Offset: 0x0006E8E6
		protected internal XmlProcessingInstruction(string target, string data, XmlDocument doc) : base(doc)
		{
			this.target = target;
			this.data = data;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000706FD File Offset: 0x0006E8FD
		public override string Name
		{
			get
			{
				if (this.target != null)
				{
					return this.target;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00067B4B File Offset: 0x00065D4B
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The entire content of the processing instruction, excluding the target.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00070713 File Offset: 0x0006E913
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x0007071B File Offset: 0x0006E91B
		public override string Value
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets the target of the processing instruction.</summary>
		/// <returns>The target of the processing instruction.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00070724 File Offset: 0x0006E924
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Gets or sets the content of the processing instruction, excluding the target.</summary>
		/// <returns>The content of the processing instruction, excluding the target.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00070713 File Offset: 0x0006E913
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x0007072C File Offset: 0x0006E92C
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				XmlNode parentNode = this.ParentNode;
				XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, value, XmlNodeChangedAction.Change);
				if (eventArgs != null)
				{
					this.BeforeEvent(eventArgs);
				}
				this.data = value;
				if (eventArgs != null)
				{
					this.AfterEvent(eventArgs);
				}
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00070713 File Offset: 0x0006E913
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x0007071B File Offset: 0x0006E91B
		public override string InnerText
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For <see langword="XmlProcessingInstruction" /> nodes, this value is XmlNodeType.ProcessingInstruction.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0007076D File Offset: 0x0006E96D
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. </param>
		/// <returns>The duplicate node.</returns>
		// Token: 0x060012D1 RID: 4817 RVA: 0x00070770 File Offset: 0x0006E970
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateProcessingInstruction(this.target, this.data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060012D2 RID: 4818 RVA: 0x00070789 File Offset: 0x0006E989
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.target, this.data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because ProcessingInstruction nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060012D3 RID: 4819 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00067B4B File Offset: 0x00065D4B
		internal override string XPLocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0007076D File Offset: 0x0006E96D
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x040010E3 RID: 4323
		private string target;

		// Token: 0x040010E4 RID: 4324
		private string data;
	}
}
