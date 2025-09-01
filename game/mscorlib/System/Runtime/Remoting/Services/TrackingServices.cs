using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Services
{
	/// <summary>Provides a way to register, unregister, and obtain a list of tracking handlers.</summary>
	// Token: 0x0200057D RID: 1405
	[ComVisible(true)]
	public class TrackingServices
	{
		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		// Token: 0x0600370E RID: 14094 RVA: 0x0000259F File Offset: 0x0000079F
		public TrackingServices()
		{
		}

		/// <summary>Registers a new tracking handler with the <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		/// <param name="handler">The tracking handler to register.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handler" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The handler that is indicated in the <paramref name="handler" /> parameter is already registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</exception>
		// Token: 0x0600370F RID: 14095 RVA: 0x000C6A60 File Offset: 0x000C4C60
		public static void RegisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object syncRoot = TrackingServices._handlers.SyncRoot;
			lock (syncRoot)
			{
				if (-1 != TrackingServices._handlers.IndexOf(handler))
				{
					throw new RemotingException("handler already registered");
				}
				TrackingServices._handlers.Add(handler);
			}
		}

		/// <summary>Unregisters the specified tracking handler from <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		/// <param name="handler">The handler to unregister.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handler" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The handler that is indicated in the <paramref name="handler" /> parameter is not registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</exception>
		// Token: 0x06003710 RID: 14096 RVA: 0x000C6AD4 File Offset: 0x000C4CD4
		public static void UnregisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object syncRoot = TrackingServices._handlers.SyncRoot;
			lock (syncRoot)
			{
				int num = TrackingServices._handlers.IndexOf(handler);
				if (num == -1)
				{
					throw new RemotingException("handler is not registered");
				}
				TrackingServices._handlers.RemoveAt(num);
			}
		}

		/// <summary>Gets an array of the tracking handlers that are currently registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> in the current <see cref="T:System.AppDomain" />.</summary>
		/// <returns>An array of the tracking handlers that are currently registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> in the current <see cref="T:System.AppDomain" />.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000C6B48 File Offset: 0x000C4D48
		public static ITrackingHandler[] RegisteredHandlers
		{
			get
			{
				object syncRoot = TrackingServices._handlers.SyncRoot;
				ITrackingHandler[] result;
				lock (syncRoot)
				{
					if (TrackingServices._handlers.Count == 0)
					{
						result = new ITrackingHandler[0];
					}
					else
					{
						result = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
					}
				}
				return result;
			}
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000C6BB8 File Offset: 0x000C4DB8
		internal static void NotifyMarshaledObject(object obj, ObjRef or)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].MarshaledObject(obj, or);
			}
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000C6C38 File Offset: 0x000C4E38
		internal static void NotifyUnmarshaledObject(object obj, ObjRef or)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UnmarshaledObject(obj, or);
			}
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000C6CB8 File Offset: 0x000C4EB8
		internal static void NotifyDisconnectedObject(object obj)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].DisconnectedObject(obj);
			}
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000C6D38 File Offset: 0x000C4F38
		// Note: this type is marked as 'beforefieldinit'.
		static TrackingServices()
		{
		}

		// Token: 0x04002576 RID: 9590
		private static ArrayList _handlers = new ArrayList();
	}
}
