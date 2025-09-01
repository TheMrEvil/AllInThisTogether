using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Stores mappings between delegates and event tokens, to support the implementation of a Windows Runtime event in managed code.</summary>
	/// <typeparam name="T">The type of the event handler delegate for a particular event.</typeparam>
	// Token: 0x02000789 RID: 1929
	public sealed class EventRegistrationTokenTable<T> where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="T" /> is not a delegate type.</exception>
		// Token: 0x0600448E RID: 17550 RVA: 0x000E3BB4 File Offset: 0x000E1DB4
		public EventRegistrationTokenTable()
		{
			if (!typeof(Delegate).IsAssignableFrom(typeof(T)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Type '{0}' is not a delegate type.  EventTokenTable may only be used with delegate types.", new object[]
				{
					typeof(T)
				}));
			}
		}

		/// <summary>Gets or sets a delegate of type <paramref name="T" /> whose invocation list includes all the event handler delegates that have been added, and that have not yet been removed. Invoking this delegate invokes all the event handlers.</summary>
		/// <returns>A delegate of type <paramref name="T" /> that represents all the event handler delegates that are currently registered for an event.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x000E3C10 File Offset: 0x000E1E10
		// (set) Token: 0x06004490 RID: 17552 RVA: 0x000E3C1C File Offset: 0x000E1E1C
		public T InvocationList
		{
			get
			{
				return this.m_invokeList;
			}
			set
			{
				Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
				lock (tokens)
				{
					this.m_tokens.Clear();
					this.m_invokeList = default(T);
					if (value != null)
					{
						this.AddEventHandlerNoLock(value);
					}
				}
			}
		}

		/// <summary>Adds the specified event handler to the table and to the invocation list, and returns a token that can be used to remove the event handler.</summary>
		/// <param name="handler">The event handler to add.</param>
		/// <returns>A token that can be used to remove the event handler from the table and the invocation list.</returns>
		// Token: 0x06004491 RID: 17553 RVA: 0x000E3C84 File Offset: 0x000E1E84
		public EventRegistrationToken AddEventHandler(T handler)
		{
			if (handler == null)
			{
				return new EventRegistrationToken(0UL);
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			EventRegistrationToken result;
			lock (tokens)
			{
				result = this.AddEventHandlerNoLock(handler);
			}
			return result;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000E3CD8 File Offset: 0x000E1ED8
		private EventRegistrationToken AddEventHandlerNoLock(T handler)
		{
			EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
			while (this.m_tokens.ContainsKey(preferredToken))
			{
				preferredToken = new EventRegistrationToken(preferredToken.Value + 1UL);
			}
			this.m_tokens[preferredToken] = handler;
			Delegate @delegate = (Delegate)((object)this.m_invokeList);
			@delegate = Delegate.Combine(@delegate, (Delegate)((object)handler));
			this.m_invokeList = (T)((object)@delegate);
			return preferredToken;
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x000E3D50 File Offset: 0x000E1F50
		[FriendAccessAllowed]
		internal T ExtractHandler(EventRegistrationToken token)
		{
			T result = default(T);
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				if (this.m_tokens.TryGetValue(token, out result))
				{
					this.RemoveEventHandlerNoLock(token);
				}
			}
			return result;
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x000E3DAC File Offset: 0x000E1FAC
		private static EventRegistrationToken GetPreferredToken(T handler)
		{
			Delegate[] invocationList = ((Delegate)((object)handler)).GetInvocationList();
			uint hashCode;
			if (invocationList.Length == 1)
			{
				hashCode = (uint)invocationList[0].Method.GetHashCode();
			}
			else
			{
				hashCode = (uint)handler.GetHashCode();
			}
			return new EventRegistrationToken((ulong)typeof(T).MetadataToken << 32 | (ulong)hashCode);
		}

		/// <summary>Removes the event handler that is associated with the specified token from the table and the invocation list.</summary>
		/// <param name="token">The token that was returned when the event handler was added.</param>
		// Token: 0x06004495 RID: 17557 RVA: 0x000E3E0C File Offset: 0x000E200C
		public void RemoveEventHandler(EventRegistrationToken token)
		{
			if (token.Value == 0UL)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				this.RemoveEventHandlerNoLock(token);
			}
		}

		/// <summary>Removes the specified event handler delegate from the table and the invocation list.</summary>
		/// <param name="handler">The event handler to remove.</param>
		// Token: 0x06004496 RID: 17558 RVA: 0x000E3E58 File Offset: 0x000E2058
		public void RemoveEventHandler(T handler)
		{
			if (handler == null)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
				T t;
				if (this.m_tokens.TryGetValue(preferredToken, out t) && t == handler)
				{
					this.RemoveEventHandlerNoLock(preferredToken);
				}
				else
				{
					foreach (KeyValuePair<EventRegistrationToken, T> keyValuePair in this.m_tokens)
					{
						if (keyValuePair.Value == (T)((object)handler))
						{
							this.RemoveEventHandlerNoLock(keyValuePair.Key);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x000E3F34 File Offset: 0x000E2134
		private void RemoveEventHandlerNoLock(EventRegistrationToken token)
		{
			T t;
			if (this.m_tokens.TryGetValue(token, out t))
			{
				this.m_tokens.Remove(token);
				Delegate @delegate = (Delegate)((object)this.m_invokeList);
				@delegate = Delegate.Remove(@delegate, (Delegate)((object)t));
				this.m_invokeList = (T)((object)@delegate);
			}
		}

		/// <summary>Returns the specified event registration token table, if it is not <see langword="null" />; otherwise, returns a new event registration token table.</summary>
		/// <param name="refEventTable">An event registration token table, passed by reference.</param>
		/// <returns>The event registration token table that is specified by <paramref name="refEventTable" />, if it is not <see langword="null" />; otherwise, a new event registration token table.</returns>
		// Token: 0x06004498 RID: 17560 RVA: 0x000E3F91 File Offset: 0x000E2191
		public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable)
		{
			if (refEventTable == null)
			{
				Interlocked.CompareExchange<EventRegistrationTokenTable<T>>(ref refEventTable, new EventRegistrationTokenTable<T>(), null);
			}
			return refEventTable;
		}

		// Token: 0x04002C25 RID: 11301
		private Dictionary<EventRegistrationToken, T> m_tokens = new Dictionary<EventRegistrationToken, T>();

		// Token: 0x04002C26 RID: 11302
		private volatile T m_invokeList;
	}
}
