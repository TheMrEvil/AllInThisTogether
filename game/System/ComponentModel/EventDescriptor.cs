using System;

namespace System.ComponentModel
{
	/// <summary>Provides information about an event.</summary>
	// Token: 0x020003A6 RID: 934
	public abstract class EventDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the specified name and attribute array.</summary>
		/// <param name="name">The name of the event.</param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the event attributes.</param>
		// Token: 0x06001E83 RID: 7811 RVA: 0x0006C2CB File Offset: 0x0006A4CB
		protected EventDescriptor(string name, Attribute[] attrs) : base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the event and its attributes.</param>
		// Token: 0x06001E84 RID: 7812 RVA: 0x0006C2D5 File Offset: 0x0006A4D5
		protected EventDescriptor(MemberDescriptor descr) : base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that has the name of the member and its attributes.</param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array with the attributes you want to add to this event description.</param>
		// Token: 0x06001E85 RID: 7813 RVA: 0x0006C2DE File Offset: 0x0006A4DE
		protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs) : base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of component this event is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component the event is bound to.</returns>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001E86 RID: 7814
		public abstract Type ComponentType { get; }

		/// <summary>When overridden in a derived class, gets the type of delegate for the event.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001E87 RID: 7815
		public abstract Type EventType { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the event delegate is a multicast delegate.</summary>
		/// <returns>
		///   <see langword="true" /> if the event delegate is multicast; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001E88 RID: 7816
		public abstract bool IsMulticast { get; }

		/// <summary>When overridden in a derived class, binds the event to the component.</summary>
		/// <param name="component">A component that provides events to the delegate.</param>
		/// <param name="value">A delegate that represents the method that handles the event.</param>
		// Token: 0x06001E89 RID: 7817
		public abstract void AddEventHandler(object component, Delegate value);

		/// <summary>When overridden in a derived class, unbinds the delegate from the component so that the delegate will no longer receive events from the component.</summary>
		/// <param name="component">The component that the delegate is bound to.</param>
		/// <param name="value">The delegate to unbind from the component.</param>
		// Token: 0x06001E8A RID: 7818
		public abstract void RemoveEventHandler(object component, Delegate value);
	}
}
