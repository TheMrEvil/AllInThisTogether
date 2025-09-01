using System;

namespace System.Xml
{
	/// <summary>Provides data for the <see cref="E:System.Xml.XmlDocument.NodeChanged" />, <see cref="E:System.Xml.XmlDocument.NodeChanging" />, <see cref="E:System.Xml.XmlDocument.NodeInserted" />, <see cref="E:System.Xml.XmlDocument.NodeInserting" />, <see cref="E:System.Xml.XmlDocument.NodeRemoved" /> and <see cref="E:System.Xml.XmlDocument.NodeRemoving" /> events.</summary>
	// Token: 0x020001D2 RID: 466
	public class XmlNodeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNodeChangedEventArgs" /> class.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldParent">The old parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newParent">The new parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldValue">The old value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newValue">The new value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="action">The <see cref="T:System.Xml.XmlNodeChangedAction" />.</param>
		// Token: 0x06001238 RID: 4664 RVA: 0x0006E4AA File Offset: 0x0006C6AA
		public XmlNodeChangedEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.node = node;
			this.oldParent = oldParent;
			this.newParent = newParent;
			this.action = action;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		/// <summary>Gets a value indicating what type of node change event is occurring.</summary>
		/// <returns>An <see langword="XmlNodeChangedAction" /> value describing the node change event.XmlNodeChangedAction Value Description Insert A node has been or will be inserted. Remove A node has been or will be removed. Change A node has been or will be changed. The <see langword="Action" /> value does not differentiate between when the event occurred (before or after). You can create separate event handlers to handle both instances.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0006E4DF File Offset: 0x0006C6DF
		public XmlNodeChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> that is being added, removed or changed.</summary>
		/// <returns>The <see langword="XmlNode" /> that is being added, removed or changed; this property never returns <see langword="null" />.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0006E4E7 File Offset: 0x0006C6E7
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Xml.XmlNode.ParentNode" /> before the operation began.</summary>
		/// <returns>The value of the <see langword="ParentNode" /> before the operation began. This property returns <see langword="null" /> if the node did not have a parent.For attribute nodes this property returns the <see cref="P:System.Xml.XmlAttribute.OwnerElement" />.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0006E4EF File Offset: 0x0006C6EF
		public XmlNode OldParent
		{
			get
			{
				return this.oldParent;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Xml.XmlNode.ParentNode" /> after the operation completes.</summary>
		/// <returns>The value of the <see langword="ParentNode" /> after the operation completes. This property returns <see langword="null" /> if the node is being removed.For attribute nodes this property returns the <see cref="P:System.Xml.XmlAttribute.OwnerElement" />.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0006E4F7 File Offset: 0x0006C6F7
		public XmlNode NewParent
		{
			get
			{
				return this.newParent;
			}
		}

		/// <summary>Gets the original value of the node.</summary>
		/// <returns>The original value of the node. This property returns <see langword="null" /> if the node is neither an attribute nor a text node, or if the node is being inserted.If called in a <see cref="E:System.Xml.XmlDocument.NodeChanging" /> event, <see langword="OldValue" /> returns the current value of the node that will be replaced if the change is successful. If called in a <see cref="E:System.Xml.XmlDocument.NodeChanged" /> event, <see langword="OldValue" /> returns the value of node prior to the change.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0006E4FF File Offset: 0x0006C6FF
		public string OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		/// <summary>Gets the new value of the node.</summary>
		/// <returns>The new value of the node. This property returns <see langword="null" /> if the node is neither an attribute nor a text node, or if the node is being removed.If called in a <see cref="E:System.Xml.XmlDocument.NodeChanging" /> event, <see langword="NewValue" /> returns the value of the node if the change is successful. If called in a <see cref="E:System.Xml.XmlDocument.NodeChanged" /> event, <see langword="NewValue" /> returns the current value of the node.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0006E507 File Offset: 0x0006C707
		public string NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x040010B9 RID: 4281
		private XmlNodeChangedAction action;

		// Token: 0x040010BA RID: 4282
		private XmlNode node;

		// Token: 0x040010BB RID: 4283
		private XmlNode oldParent;

		// Token: 0x040010BC RID: 4284
		private XmlNode newParent;

		// Token: 0x040010BD RID: 4285
		private string oldValue;

		// Token: 0x040010BE RID: 4286
		private string newValue;
	}
}
