using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.IComponent" /> interface and enables object sharing between applications.</summary>
	// Token: 0x0200040D RID: 1037
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DesignerCategory("Component")]
	public class Component : MarshalByRefObject, IComponent, IDisposable
	{
		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.ComponentModel.Component" /> is reclaimed by garbage collection.</summary>
		// Token: 0x0600217E RID: 8574 RVA: 0x00072904 File Offset: 0x00070B04
		~Component()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a value indicating whether the component can raise an event.</summary>
		/// <returns>
		///   <see langword="true" /> if the component can raise events; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0000390E File Offset: 0x00001B0E
		protected virtual bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x00072934 File Offset: 0x00070B34
		internal bool CanRaiseEventsInternal
		{
			get
			{
				return this.CanRaiseEvents;
			}
		}

		/// <summary>Occurs when the component is disposed by a call to the <see cref="M:System.ComponentModel.Component.Dispose" /> method.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002181 RID: 8577 RVA: 0x0007293C File Offset: 0x00070B3C
		// (remove) Token: 0x06002182 RID: 8578 RVA: 0x0007294F File Offset: 0x00070B4F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(Component.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(Component.EventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x00072962 File Offset: 0x00070B62
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList(this);
				}
				return this.events;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0007297E File Offset: 0x00070B7E
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x00072986 File Offset: 0x00070B86
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.</summary>
		// Token: 0x06002186 RID: 8582 RVA: 0x0007298F File Offset: 0x00070B8F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002187 RID: 8583 RVA: 0x000729A0 File Offset: 0x00070BA0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.site != null && this.site.Container != null)
					{
						this.site.Container.Remove(this);
					}
					if (this.events != null)
					{
						EventHandler eventHandler = (EventHandler)this.events[Component.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x00072A2C File Offset: 0x00070C2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IContainer Container
		{
			get
			{
				ISite site = this.site;
				if (site != null)
				{
					return site.Container;
				}
				return null;
			}
		}

		/// <summary>Returns an object that represents a service provided by the <see cref="T:System.ComponentModel.Component" /> or by its <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="service">A service provided by the <see cref="T:System.ComponentModel.Component" />.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
		// Token: 0x06002189 RID: 8585 RVA: 0x00072A4C File Offset: 0x00070C4C
		protected virtual object GetService(Type service)
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.GetService(service);
			}
			return null;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.ComponentModel.Component" /> is currently in design mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Component" /> is in design mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00072A6C File Offset: 0x00070C6C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x0600218B RID: 8587 RVA: 0x00072A8C File Offset: 0x00070C8C
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Component" /> class.</summary>
		// Token: 0x0600218C RID: 8588 RVA: 0x0002D758 File Offset: 0x0002B958
		public Component()
		{
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00072ACF File Offset: 0x00070CCF
		// Note: this type is marked as 'beforefieldinit'.
		static Component()
		{
		}

		// Token: 0x04001016 RID: 4118
		private static readonly object EventDisposed = new object();

		// Token: 0x04001017 RID: 4119
		private ISite site;

		// Token: 0x04001018 RID: 4120
		private EventHandlerList events;
	}
}
