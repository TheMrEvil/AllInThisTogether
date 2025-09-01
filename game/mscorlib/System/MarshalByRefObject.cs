using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;

namespace System
{
	/// <summary>Enables access to objects across application domain boundaries in applications that support remoting.</summary>
	// Token: 0x0200023B RID: 571
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MarshalByRefObject" /> class.</summary>
		// Token: 0x06001A16 RID: 6678 RVA: 0x0000259F File Offset: 0x0000079F
		protected MarshalByRefObject()
		{
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0005FEBC File Offset: 0x0005E0BC
		internal Identity GetObjectIdentity(MarshalByRefObject obj, out bool IsClient)
		{
			IsClient = false;
			Identity objectIdentity;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				objectIdentity = RemotingServices.GetRealProxy(obj).ObjectIdentity;
				IsClient = true;
			}
			else
			{
				objectIdentity = obj.ObjectIdentity;
			}
			return objectIdentity;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0005FEEF File Offset: 0x0005E0EF
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0005FEF7 File Offset: 0x0005E0F7
		internal ServerIdentity ObjectIdentity
		{
			get
			{
				return this._identity;
			}
			set
			{
				this._identity = value;
			}
		}

		/// <summary>Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.</summary>
		/// <param name="requestedType">The <see cref="T:System.Type" /> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> will reference.</param>
		/// <returns>Information required to generate a proxy.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06001A1A RID: 6682 RVA: 0x0005FF00 File Offset: 0x0005E100
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this._identity == null)
			{
				throw new RemotingException(Locale.GetText("No remoting information was found for the object."));
			}
			return this._identity.CreateObjRef(requestedType);
		}

		/// <summary>Retrieves the current lifetime service object that controls the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06001A1B RID: 6683 RVA: 0x0005FF26 File Offset: 0x0005E126
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public object GetLifetimeService()
		{
			if (this._identity == null)
			{
				return null;
			}
			return this._identity.Lease;
		}

		/// <summary>Obtains a lifetime service object to control the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06001A1C RID: 6684 RVA: 0x0005FF3D File Offset: 0x0005E13D
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual object InitializeLifetimeService()
		{
			if (this._identity != null && this._identity.Lease != null)
			{
				return this._identity.Lease;
			}
			return new Lease();
		}

		/// <summary>Creates a shallow copy of the current <see cref="T:System.MarshalByRefObject" /> object.</summary>
		/// <param name="cloneIdentity">
		///   <see langword="false" /> to delete the current <see cref="T:System.MarshalByRefObject" /> object's identity, which will cause the object to be assigned a new identity when it is marshaled across a remoting boundary. A value of <see langword="false" /> is usually appropriate. <see langword="true" /> to copy the current <see cref="T:System.MarshalByRefObject" /> object's identity to its clone, which will cause remoting client calls to be routed to the remote server object.</param>
		/// <returns>A shallow copy of the current <see cref="T:System.MarshalByRefObject" /> object.</returns>
		// Token: 0x06001A1D RID: 6685 RVA: 0x0005FF68 File Offset: 0x0005E168
		protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)base.MemberwiseClone();
			if (!cloneIdentity)
			{
				marshalByRefObject._identity = null;
			}
			return marshalByRefObject;
		}

		// Token: 0x0400171E RID: 5918
		[NonSerialized]
		private ServerIdentity _identity;
	}
}
