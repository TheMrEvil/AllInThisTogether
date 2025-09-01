using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace System.Runtime.Remoting
{
	// Token: 0x0200056F RID: 1391
	internal abstract class ServerIdentity : Identity
	{
		// Token: 0x060036B0 RID: 14000 RVA: 0x000C596A File Offset: 0x000C3B6A
		public ServerIdentity(string objectUri, Context context, Type objectType) : base(objectUri)
		{
			this._objectType = objectType;
			this._context = context;
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000C5981 File Offset: 0x000C3B81
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000C5989 File Offset: 0x000C3B89
		public void StartTrackingLifetime(ILease lease)
		{
			if (lease != null && lease.CurrentState == LeaseState.Null)
			{
				lease = null;
			}
			if (lease != null)
			{
				if (!(lease is Lease))
				{
					lease = new Lease();
				}
				this._lease = (Lease)lease;
				LifetimeServices.TrackLifetime(this);
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000C59BD File Offset: 0x000C3BBD
		public virtual void OnLifetimeExpired()
		{
			this.DisposeServerObject();
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000C59C8 File Offset: 0x000C3BC8
		public override ObjRef CreateObjRef(Type requestedType)
		{
			if (this._objRef != null)
			{
				this._objRef.UpdateChannelInfo();
				return this._objRef;
			}
			if (requestedType == null)
			{
				requestedType = this._objectType;
			}
			this._objRef = new ObjRef();
			this._objRef.TypeInfo = new TypeInfo(requestedType);
			this._objRef.URI = this._objectUri;
			if (this._envoySink != null && !(this._envoySink is EnvoyTerminatorSink))
			{
				this._objRef.EnvoyInfo = new EnvoyInfo(this._envoySink);
			}
			return this._objRef;
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000C5A60 File Offset: 0x000C3C60
		public void AttachServerObject(MarshalByRefObject serverObject, Context context)
		{
			this.DisposeServerObject();
			this._context = context;
			this._serverObject = serverObject;
			if (RemotingServices.IsTransparentProxy(serverObject))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(serverObject);
				if (realProxy.ObjectIdentity == null)
				{
					realProxy.ObjectIdentity = this;
					return;
				}
			}
			else
			{
				if (this._objectType.IsContextful)
				{
					this._envoySink = context.CreateEnvoySink(serverObject);
				}
				this._serverObject.ObjectIdentity = this;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000C5AC6 File Offset: 0x000C3CC6
		public Lease Lease
		{
			get
			{
				return this._lease;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000C5ACE File Offset: 0x000C3CCE
		// (set) Token: 0x060036B8 RID: 14008 RVA: 0x000C5AD6 File Offset: 0x000C3CD6
		public Context Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x060036B9 RID: 14009
		public abstract IMessage SyncObjectProcessMessage(IMessage msg);

		// Token: 0x060036BA RID: 14010
		public abstract IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x060036BB RID: 14011 RVA: 0x000C5ADF File Offset: 0x000C3CDF
		protected void DisposeServerObject()
		{
			if (this._serverObject != null)
			{
				object serverObject = this._serverObject;
				this._serverObject.ObjectIdentity = null;
				this._serverObject = null;
				this._serverSink = null;
				TrackingServices.NotifyDisconnectedObject(serverObject);
			}
		}

		// Token: 0x04002559 RID: 9561
		protected Type _objectType;

		// Token: 0x0400255A RID: 9562
		protected MarshalByRefObject _serverObject;

		// Token: 0x0400255B RID: 9563
		protected IMessageSink _serverSink;

		// Token: 0x0400255C RID: 9564
		protected Context _context;

		// Token: 0x0400255D RID: 9565
		protected Lease _lease;
	}
}
