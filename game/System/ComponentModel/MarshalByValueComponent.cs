using System;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	/// <summary>Implements <see cref="T:System.ComponentModel.IComponent" /> and provides the base implementation for remotable components that are marshaled by value (a copy of the serialized object is passed).</summary>
	// Token: 0x020003D4 RID: 980
	[TypeConverter(typeof(ComponentConverter))]
	[DesignerCategory("Component")]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	public class MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> class.</summary>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x0000219B File Offset: 0x0000039B
		public MarshalByValueComponent()
		{
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06001FA2 RID: 8098 RVA: 0x0006D7A0 File Offset: 0x0006B9A0
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		/// <summary>Adds an event handler to listen to the <see cref="E:System.ComponentModel.MarshalByValueComponent.Disposed" /> event on the component.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001FA3 RID: 8099 RVA: 0x0006D7D0 File Offset: 0x0006B9D0
		// (remove) Token: 0x06001FA4 RID: 8100 RVA: 0x0006D7E3 File Offset: 0x0006B9E3
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(MarshalByValueComponent.s_eventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(MarshalByValueComponent.s_eventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0006D7F8 File Offset: 0x0006B9F8
		protected EventHandlerList Events
		{
			get
			{
				EventHandlerList result;
				if ((result = this._events) == null)
				{
					result = (this._events = new EventHandlerList());
				}
				return result;
			}
		}

		/// <summary>Gets or sets the site of the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.ISite" /> interface that represents the site of the component.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x0006D81D File Offset: 0x0006BA1D
		// (set) Token: 0x06001FA7 RID: 8103 RVA: 0x0006D825 File Offset: 0x0006BA25
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" />.</summary>
		// Token: 0x06001FA8 RID: 8104 RVA: 0x0006D82E File Offset: 0x0006BA2E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001FA9 RID: 8105 RVA: 0x0006D840 File Offset: 0x0006BA40
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					ISite site = this._site;
					if (site != null)
					{
						IContainer container = site.Container;
						if (container != null)
						{
							container.Remove(this);
						}
					}
					EventHandlerList events = this._events;
					EventHandler eventHandler = (EventHandler)((events != null) ? events[MarshalByValueComponent.s_eventDisposed] : null);
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets the container for the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.IContainer" /> interface that represents the component's container, or <see langword="null" /> if the component does not have a site.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x0006D8C4 File Offset: 0x0006BAC4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IContainer Container
		{
			get
			{
				ISite site = this._site;
				if (site == null)
				{
					return null;
				}
				return site.Container;
			}
		}

		/// <summary>Gets the implementer of the <see cref="T:System.IServiceProvider" />.</summary>
		/// <param name="service">A <see cref="T:System.Type" /> that represents the type of service you want.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the implementer of the <see cref="T:System.IServiceProvider" />.</returns>
		// Token: 0x06001FAB RID: 8107 RVA: 0x0006D8D7 File Offset: 0x0006BAD7
		public virtual object GetService(Type service)
		{
			ISite site = this._site;
			if (site == null)
			{
				return null;
			}
			return site.GetService(service);
		}

		/// <summary>Gets a value indicating whether the component is currently in design mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the component is in design mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x0006D8EB File Offset: 0x0006BAEB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool DesignMode
		{
			get
			{
				ISite site = this._site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any.  
		///  <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06001FAD RID: 8109 RVA: 0x0006D900 File Offset: 0x0006BB00
		public override string ToString()
		{
			ISite site = this._site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0006D943 File Offset: 0x0006BB43
		// Note: this type is marked as 'beforefieldinit'.
		static MarshalByValueComponent()
		{
		}

		// Token: 0x04000F76 RID: 3958
		private static readonly object s_eventDisposed = new object();

		// Token: 0x04000F77 RID: 3959
		private ISite _site;

		// Token: 0x04000F78 RID: 3960
		private EventHandlerList _events;
	}
}
