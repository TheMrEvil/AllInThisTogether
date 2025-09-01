using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x0200036C RID: 876
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x06001D07 RID: 7431 RVA: 0x0006833E File Offset: 0x0006653E
		internal EventHandlerList(Component parent)
		{
			this._parent = parent;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventHandlerList" /> class.</summary>
		// Token: 0x06001D08 RID: 7432 RVA: 0x0000219B File Offset: 0x0000039B
		public EventHandlerList()
		{
		}

		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <param name="key">An object to find in the list.</param>
		/// <returns>The delegate for the specified key, or <see langword="null" /> if a delegate does not exist.</returns>
		// Token: 0x170005DA RID: 1498
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this._parent == null || this._parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry == null)
				{
					return null;
				}
				return listEntry._handler;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry._handler = value;
					return;
				}
				this._head = new EventHandlerList.ListEntry(key, value, this._head);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event.</param>
		/// <param name="value">The delegate to add to the list.</param>
		// Token: 0x06001D0B RID: 7435 RVA: 0x000683BC File Offset: 0x000665BC
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Combine(listEntry._handler, value);
				return;
			}
			this._head = new EventHandlerList.ListEntry(key, value, this._head);
		}

		/// <summary>Adds a list of delegates to the current list.</summary>
		/// <param name="listToAddFrom">The list to add.</param>
		// Token: 0x06001D0C RID: 7436 RVA: 0x000683FC File Offset: 0x000665FC
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry listEntry = listToAddFrom._head; listEntry != null; listEntry = listEntry._next)
			{
				this.AddHandler(listEntry._key, listEntry._handler);
			}
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x06001D0D RID: 7437 RVA: 0x0006842E File Offset: 0x0006662E
		public void Dispose()
		{
			this._head = null;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00068438 File Offset: 0x00066638
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry listEntry = this._head;
			while (listEntry != null && listEntry._key != key)
			{
				listEntry = listEntry._next;
			}
			return listEntry;
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event.</param>
		/// <param name="value">The delegate to remove from the list.</param>
		// Token: 0x06001D0F RID: 7439 RVA: 0x00068464 File Offset: 0x00066664
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Remove(listEntry._handler, value);
			}
		}

		// Token: 0x04000EB8 RID: 3768
		private EventHandlerList.ListEntry _head;

		// Token: 0x04000EB9 RID: 3769
		private Component _parent;

		// Token: 0x0200036D RID: 877
		private sealed class ListEntry
		{
			// Token: 0x06001D10 RID: 7440 RVA: 0x0006848E File Offset: 0x0006668E
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this._next = next;
				this._key = key;
				this._handler = handler;
			}

			// Token: 0x04000EBA RID: 3770
			internal EventHandlerList.ListEntry _next;

			// Token: 0x04000EBB RID: 3771
			internal object _key;

			// Token: 0x04000EBC RID: 3772
			internal Delegate _handler;
		}
	}
}
