using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x0200046F RID: 1135
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button.</param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu.</param>
		// Token: 0x06002487 RID: 9351 RVA: 0x000818B6 File Offset: 0x0007FAB6
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this._execHandler = handler;
			this.CommandID = command;
			this._status = 3;
		}

		/// <summary>Gets or sets a value indicating whether this menu item is checked.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is checked; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000818D3 File Offset: 0x0007FAD3
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x000818E0 File Offset: 0x0007FAE0
		public virtual bool Checked
		{
			get
			{
				return (this._status & 4) != 0;
			}
			set
			{
				this.SetStatus(4, value);
			}
		}

		/// <summary>Gets a value indicating whether this menu item is available.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000818EA File Offset: 0x0007FAEA
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x000818F7 File Offset: 0x0007FAF7
		public virtual bool Enabled
		{
			get
			{
				return (this._status & 2) != 0;
			}
			set
			{
				this.SetStatus(2, value);
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00081904 File Offset: 0x0007FB04
		private void SetStatus(int mask, bool value)
		{
			int num = this._status;
			if (value)
			{
				num |= mask;
			}
			else
			{
				num &= ~mask;
			}
			if (num != this._status)
			{
				this._status = num;
				this.OnCommandChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</returns>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00081944 File Offset: 0x0007FB44
		public virtual IDictionary Properties
		{
			get
			{
				IDictionary result;
				if ((result = this._properties) == null)
				{
					result = (this._properties = new HybridDictionary());
				}
				return result;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is supported, which is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00081969 File Offset: 0x0007FB69
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x00081976 File Offset: 0x0007FB76
		public virtual bool Supported
		{
			get
			{
				return (this._status & 1) != 0;
			}
			set
			{
				this.SetStatus(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00081980 File Offset: 0x0007FB80
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0008198E File Offset: 0x0007FB8E
		public virtual bool Visible
		{
			get
			{
				return (this._status & 16) == 0;
			}
			set
			{
				this.SetStatus(16, !value);
			}
		}

		/// <summary>Occurs when the menu command changes.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06002492 RID: 9362 RVA: 0x0008199C File Offset: 0x0007FB9C
		// (remove) Token: 0x06002493 RID: 9363 RVA: 0x000819D4 File Offset: 0x0007FBD4
		public event EventHandler CommandChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.CommandChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.CommandChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.CommandChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.CommandChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x00081A09 File Offset: 0x0007FC09
		public virtual CommandID CommandID
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandID>k__BackingField;
			}
		}

		/// <summary>Invokes the command.</summary>
		// Token: 0x06002495 RID: 9365 RVA: 0x00081A14 File Offset: 0x0007FC14
		public virtual void Invoke()
		{
			if (this._execHandler != null)
			{
				try
				{
					this._execHandler(this, EventArgs.Empty);
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Invokes the command with the given parameter.</summary>
		/// <param name="arg">An optional argument for use by the command.</param>
		// Token: 0x06002496 RID: 9366 RVA: 0x00081A58 File Offset: 0x0007FC58
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		/// <summary>Gets the OLE command status code for this menu item.</summary>
		/// <returns>An integer containing a mixture of status flags that reflect the state of this menu item.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00081A60 File Offset: 0x0007FC60
		public virtual int OleStatus
		{
			get
			{
				return this._status;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommand.CommandChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002498 RID: 9368 RVA: 0x00081A68 File Offset: 0x0007FC68
		protected virtual void OnCommandChanged(EventArgs e)
		{
			EventHandler commandChanged = this.CommandChanged;
			if (commandChanged == null)
			{
				return;
			}
			commandChanged(this, e);
		}

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x06002499 RID: 9369 RVA: 0x00081A7C File Offset: 0x0007FC7C
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this._status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this._status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this._status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this._status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x040010D1 RID: 4305
		private EventHandler _execHandler;

		// Token: 0x040010D2 RID: 4306
		private int _status;

		// Token: 0x040010D3 RID: 4307
		private IDictionary _properties;

		// Token: 0x040010D4 RID: 4308
		private const int ENABLED = 2;

		// Token: 0x040010D5 RID: 4309
		private const int INVISIBLE = 16;

		// Token: 0x040010D6 RID: 4310
		private const int CHECKED = 4;

		// Token: 0x040010D7 RID: 4311
		private const int SUPPORTED = 1;

		// Token: 0x040010D8 RID: 4312
		[CompilerGenerated]
		private EventHandler CommandChanged;

		// Token: 0x040010D9 RID: 4313
		[CompilerGenerated]
		private readonly CommandID <CommandID>k__BackingField;
	}
}
