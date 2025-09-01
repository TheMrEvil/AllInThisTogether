using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000570 RID: 1392
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x060036BC RID: 14012 RVA: 0x000C5B0E File Offset: 0x000C3D0E
		public ClientActivatedIdentity(string objectUri, Type objectType) : base(objectUri, null, objectType)
		{
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000C5B19 File Offset: 0x000C3D19
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000C5B21 File Offset: 0x000C3D21
		public MarshalByRefObject GetClientProxy()
		{
			return this._targetThis;
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000C5B29 File Offset: 0x000C3D29
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000C5B32 File Offset: 0x000C3D32
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000C5B40 File Offset: 0x000C3D40
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000C5B90 File Offset: 0x000C3D90
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x0400255E RID: 9566
		private MarshalByRefObject _targetThis;
	}
}
