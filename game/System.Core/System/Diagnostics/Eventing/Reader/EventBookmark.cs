using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents a placeholder (bookmark) within an event stream. You can use the placeholder to mark a position and return to this position in a stream of events. An instance of this object can be obtained from an <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> object, in which case it corresponds to the position of that event record.</summary>
	// Token: 0x02000397 RID: 919
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventBookmark : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventBookmark" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Diagnostics.Eventing.Reader.EventBookmark" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source of the serialized stream that is associated with the new <see cref="T:System.Diagnostics.Eventing.Reader.EventBookmark" />.</param>
		// Token: 0x06001B6F RID: 7023 RVA: 0x0000235B File Offset: 0x0000055B
		protected EventBookmark(SerializationInfo info, StreamingContext context)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data required to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06001B70 RID: 7024 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06001B71 RID: 7025 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
