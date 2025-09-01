using System;

namespace System.Xml.Linq
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Linq.XObject.Changing" /> and <see cref="E:System.Xml.Linq.XObject.Changed" /> events.</summary>
	// Token: 0x0200005E RID: 94
	public class XObjectChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XObjectChangeEventArgs" /> class.</summary>
		/// <param name="objectChange">An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the event arguments for LINQ to XML events.</param>
		// Token: 0x06000389 RID: 905 RVA: 0x0001015F File Offset: 0x0000E35F
		public XObjectChangeEventArgs(XObjectChange objectChange)
		{
			this._objectChange = objectChange;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the type of change.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0001016E File Offset: 0x0000E36E
		public XObjectChange ObjectChange
		{
			get
			{
				return this._objectChange;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00010176 File Offset: 0x0000E376
		// Note: this type is marked as 'beforefieldinit'.
		static XObjectChangeEventArgs()
		{
		}

		// Token: 0x040001D9 RID: 473
		private XObjectChange _objectChange;

		/// <summary>Event argument for an <see cref="F:System.Xml.Linq.XObjectChange.Add" /> change event.</summary>
		// Token: 0x040001DA RID: 474
		public static readonly XObjectChangeEventArgs Add = new XObjectChangeEventArgs(XObjectChange.Add);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Remove" /> change event.</summary>
		// Token: 0x040001DB RID: 475
		public static readonly XObjectChangeEventArgs Remove = new XObjectChangeEventArgs(XObjectChange.Remove);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Name" /> change event.</summary>
		// Token: 0x040001DC RID: 476
		public static readonly XObjectChangeEventArgs Name = new XObjectChangeEventArgs(XObjectChange.Name);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Value" /> change event.</summary>
		// Token: 0x040001DD RID: 477
		public static readonly XObjectChangeEventArgs Value = new XObjectChangeEventArgs(XObjectChange.Value);
	}
}
