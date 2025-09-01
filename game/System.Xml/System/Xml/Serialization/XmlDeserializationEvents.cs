using System;

namespace System.Xml.Serialization
{
	/// <summary>Contains fields that can be used to pass event delegates to a thread-safe <see cref="Overload:System.Xml.Serialization.XmlSerializer.Deserialize" /> method of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x02000300 RID: 768
	public struct XmlDeserializationEvents
	{
		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlNodeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x000CF469 File Offset: 0x000CD669
		// (set) Token: 0x06001FDB RID: 8155 RVA: 0x000CF471 File Offset: 0x000CD671
		public XmlNodeEventHandler OnUnknownNode
		{
			get
			{
				return this.onUnknownNode;
			}
			set
			{
				this.onUnknownNode = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x000CF47A File Offset: 0x000CD67A
		// (set) Token: 0x06001FDD RID: 8157 RVA: 0x000CF482 File Offset: 0x000CD682
		public XmlAttributeEventHandler OnUnknownAttribute
		{
			get
			{
				return this.onUnknownAttribute;
			}
			set
			{
				this.onUnknownAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementEventHandler" /> that points to the event handler.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x000CF48B File Offset: 0x000CD68B
		// (set) Token: 0x06001FDF RID: 8159 RVA: 0x000CF493 File Offset: 0x000CD693
		public XmlElementEventHandler OnUnknownElement
		{
			get
			{
				return this.onUnknownElement;
			}
			set
			{
				this.onUnknownElement = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.UnreferencedObjectEventHandler" /> that points to the event handler.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x000CF49C File Offset: 0x000CD69C
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x000CF4A4 File Offset: 0x000CD6A4
		public UnreferencedObjectEventHandler OnUnreferencedObject
		{
			get
			{
				return this.onUnreferencedObject;
			}
			set
			{
				this.onUnreferencedObject = value;
			}
		}

		// Token: 0x04001B0F RID: 6927
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04001B10 RID: 6928
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04001B11 RID: 6929
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x04001B12 RID: 6930
		private UnreferencedObjectEventHandler onUnreferencedObject;

		// Token: 0x04001B13 RID: 6931
		internal object sender;
	}
}
