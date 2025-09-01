using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing
{
	/// <summary>Use this class to write events.</summary>
	// Token: 0x02000394 RID: 916
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventProvider : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.EventProvider" /> class.</summary>
		/// <param name="providerGuid">Guid that uniquely identifies the provider.</param>
		/// <exception cref="T:System.InsufficientMemoryException">There is not enough memory to complete the operation.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The classes in the <see cref="N:System.Diagnostics.Eventing" /> namespace work only on Windows Vista.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="providerGuid" /> parameter cannot be null.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error returned by the ETW subsystem. </exception>
		// Token: 0x06001B58 RID: 7000 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public EventProvider(Guid providerGuid)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the provider's registration from the ETW subsystem and releases all unmanaged resources.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error returned by the ETW subsystem. </exception>
		// Token: 0x06001B59 RID: 7001 RVA: 0x0000235B File Offset: 0x0000055B
		public virtual void Close()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a unique activity identifier for the provider.</summary>
		/// <returns>A unique Guid that you use when calling the <see cref="M:System.Diagnostics.Eventing.EventProvider.SetActivityId(System.Guid@)" /> method to set the activity identifier for the provider.</returns>
		// Token: 0x06001B5A RID: 7002 RVA: 0x0005A400 File Offset: 0x00058600
		[SecurityCritical]
		public static Guid CreateActivityId()
		{
			ThrowStub.ThrowNotSupportedException();
			return default(Guid);
		}

		/// <summary>Releases the resources used by this <see cref="T:System.Diagnostics.Eventing.EventProvider" /> object.</summary>
		// Token: 0x06001B5B RID: 7003 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the resources used by this <see cref="T:System.Diagnostics.Eventing.EventProvider" /> object.</summary>
		/// <param name="disposing">This parameter is ignored by this method since there are no unmanaged resources.</param>
		// Token: 0x06001B5C RID: 7004 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the last error associated with an event write failure.</summary>
		/// <returns>Use the value to determine the cause of an event write failure.</returns>
		// Token: 0x06001B5D RID: 7005 RVA: 0x0005A41C File Offset: 0x0005861C
		public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
		{
			ThrowStub.ThrowNotSupportedException();
			return EventProvider.WriteEventErrorCode.NoError;
		}

		/// <summary>Determines whether any session enabled the provider, regardless of the level and keyword values used to enable the provider.</summary>
		/// <returns>Is <see langword="true" /> if the provider is enabled to any session; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B5E RID: 7006 RVA: 0x0005A438 File Offset: 0x00058638
		public bool IsEnabled()
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Determines whether any session is requesting the specified event from the provider.</summary>
		/// <param name="level">Level of detail included in the event.</param>
		/// <param name="keywords">Bit mask that specifies the event category. This mask should be the same keyword mask that is defined in the manifest for the event.</param>
		/// <returns>Is <see langword="true" /> if any session is requesting the specified event; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B5F RID: 7007 RVA: 0x0005A454 File Offset: 0x00058654
		public bool IsEnabled(byte level, long keywords)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Sets the current activity identifier used by the <see cref="Overload:System.Diagnostics.Eventing.EventProvider.WriteEvent" /> methods.</summary>
		/// <param name="id">A unique activity identifier that the <see cref="M:System.Diagnostics.Eventing.EventProvider.CreateActivityId" /> method returns.</param>
		// Token: 0x06001B60 RID: 7008 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public static void SetActivityId(ref Guid id)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Writes an event. The event data is specified as a block of memory.</summary>
		/// <param name="eventDescriptor">An instance of <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> that identifies the event to write.</param>
		/// <param name="dataCount">Size of the event data to which the <paramref name="data" /> parameter points. The maximum event data size is limited to 64 KB minus the size of the event headers. The event size is less if the session's buffer size is less and the session includes extended data items with the event.</param>
		/// <param name="data">Pointer to the event data to write.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		// Token: 0x06001B61 RID: 7009 RVA: 0x0005A470 File Offset: 0x00058670
		[SecurityCritical]
		protected bool WriteEvent(ref EventDescriptor eventDescriptor, int dataCount, IntPtr data)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Writes an event. The event data is specified as an array of objects.</summary>
		/// <param name="eventDescriptor">An instance of <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> that identifies the event to write.</param>
		/// <param name="eventPayload">An array of objects that contain the event data to write. The object must be in the order specified in the manifest. The array is limited to 32 objects, of which only eight may be strings. The maximum data size for the event is limited to 64 KB minus the size of the event headers. The event size is less if the session's buffer size is less and the session includes extended data items with the event.This parameter can be null.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="eventPayload" /> parameter contains too many objects or strings.</exception>
		// Token: 0x06001B62 RID: 7010 RVA: 0x0005A48C File Offset: 0x0005868C
		public bool WriteEvent(ref EventDescriptor eventDescriptor, object[] eventPayload)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Writes an event. The event data is specified as a string.</summary>
		/// <param name="eventDescriptor">An instance of <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> that identifies the event to write.</param>
		/// <param name="data">The string to write as the event data.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		/// <exception cref="T:System.ArgumentException">If <paramref name="data" /> is <see langword="null" />.</exception>
		// Token: 0x06001B63 RID: 7011 RVA: 0x0005A4A8 File Offset: 0x000586A8
		[SecurityCritical]
		public bool WriteEvent(ref EventDescriptor eventDescriptor, string data)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Writes an event that contains a string as its data.</summary>
		/// <param name="eventMessage">String to write as the event data.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		/// <exception cref="T:System.ArgumentException">If <paramref name="eventMessage" /> is <see langword="null" />.</exception>
		// Token: 0x06001B64 RID: 7012 RVA: 0x0005A4C4 File Offset: 0x000586C4
		public bool WriteMessageEvent(string eventMessage)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Writes an event that contains a string as its data if the level and keyword value match the events requested by the session.</summary>
		/// <param name="eventMessage">String to write as the event data.</param>
		/// <param name="eventLevel">Level of detail included in the event. If the provider uses a manifest to define the event, set this value to the same level defined in the manifest.</param>
		/// <param name="eventKeywords">Bit mask that specifies the event category. If the provider uses a manifest to define the event, set this value to the same keyword mask defined in the manifest.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		/// <exception cref="T:System.ArgumentException">If <paramref name="eventMessage" /> is <see langword="null" />.</exception>
		// Token: 0x06001B65 RID: 7013 RVA: 0x0005A4E0 File Offset: 0x000586E0
		[SecurityCritical]
		public bool WriteMessageEvent(string eventMessage, byte eventLevel, long eventKeywords)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Links events together when tracing events in an end-to-end scenario. The event data is specified as a block of memory.</summary>
		/// <param name="eventDescriptor">An instance of <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> that identifies the event to write.</param>
		/// <param name="relatedActivityId">Activity identifier from the previous component. Use this parameter to link your component's events to the previous component's events.</param>
		/// <param name="dataCount">Size of the event data to which the <paramref name="data" /> parameter points. The maximum event data size is limited to 64 KB minus the size of the event headers. The event size is less if the session's buffer size is less and the session includes extended data items with the event.</param>
		/// <param name="data">Pointer to the event data to write.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		// Token: 0x06001B66 RID: 7014 RVA: 0x0005A4FC File Offset: 0x000586FC
		[SecurityCritical]
		protected bool WriteTransferEvent(ref EventDescriptor eventDescriptor, Guid relatedActivityId, int dataCount, IntPtr data)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Links events together when tracing events in an end-to-end scenario. The event data is specified as an array of objects.</summary>
		/// <param name="eventDescriptor">An instance of <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> that identifies the event to write.</param>
		/// <param name="relatedActivityId">Activity identifier from the previous component. Use this parameter to link your component's events to the previous component's events.</param>
		/// <param name="eventPayload">An array of objects that contain the event data to write. The data must be in the order specified in the manifest. The array is limited to 32 objects, of which only eight may be strings. The maximum data size for the event is limited to 64 KB minus the size of the event headers. The event size is less if the session's buffer size is less and the session includes extended data items with the event.</param>
		/// <returns>Is <see langword="true" /> if the event is written; otherwise, <see langword="false" />. If false, call the <see cref="M:System.Diagnostics.Eventing.EventProvider.GetLastWriteEventError" /> method to determine the cause of the failure.</returns>
		/// <exception cref="T:System.ArgumentException">If <paramref name="eventPayload" /> contains too many objects or strings.</exception>
		// Token: 0x06001B67 RID: 7015 RVA: 0x0005A518 File Offset: 0x00058718
		[SecurityCritical]
		public bool WriteTransferEvent(ref EventDescriptor eventDescriptor, Guid relatedActivityId, object[] eventPayload)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Defines the possible states of the last write operation.</summary>
		// Token: 0x02000395 RID: 917
		public enum WriteEventErrorCode
		{
			/// <summary>The event is larger than the session buffer size; events cannot span buffers.</summary>
			// Token: 0x04000D3F RID: 3391
			EventTooBig = 2,
			/// <summary>The write was successful.</summary>
			// Token: 0x04000D40 RID: 3392
			NoError = 0,
			/// <summary>The session ran out of free buffers to write to. This can occur during high event rates because the disk subsystem is overloaded or the number of buffers is too small. Rather than blocking until more buffers become available, the event is dropped. Consider increasing the number and size of the buffers for the session, or reducing the number of events written or the size of the events.</summary>
			// Token: 0x04000D41 RID: 3393
			NoFreeBuffers
		}
	}
}
