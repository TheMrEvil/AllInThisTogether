using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Enforces a synchronization domain for the current context and all contexts that share the same instance.</summary>
	// Token: 0x0200059E RID: 1438
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with default values.</summary>
		// Token: 0x060037E1 RID: 14305 RVA: 0x000C8D28 File Offset: 0x000C6F28
		public SynchronizationAttribute() : this(8, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a Boolean value indicating whether reentry is required.</summary>
		/// <param name="reEntrant">A Boolean value indicating whether reentry is required.</param>
		// Token: 0x060037E2 RID: 14306 RVA: 0x000C8D32 File Offset: 0x000C6F32
		public SynchronizationAttribute(bool reEntrant) : this(8, reEntrant)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags.</exception>
		// Token: 0x060037E3 RID: 14307 RVA: 0x000C8D3C File Offset: 0x000C6F3C
		public SynchronizationAttribute(int flag) : this(flag, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied, and a Boolean value indicating whether reentry is required.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied.</param>
		/// <param name="reEntrant">
		///   <see langword="true" /> if reentry is required, and callouts must be intercepted and serialized; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags.</exception>
		// Token: 0x060037E4 RID: 14308 RVA: 0x000C8D48 File Offset: 0x000C6F48
		public SynchronizationAttribute(int flag, bool reEntrant) : base("Synchronization")
		{
			if (flag != 1 && flag != 4 && flag != 8 && flag != 2)
			{
				throw new ArgumentException("flag");
			}
			this._bReEntrant = reEntrant;
			this._flavor = flag;
		}

		/// <summary>Gets or sets a Boolean value indicating whether reentry is required.</summary>
		/// <returns>A Boolean value indicating whether reentry is required.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000C8D95 File Offset: 0x000C6F95
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</summary>
		/// <returns>A Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000C8D9D File Offset: 0x000C6F9D
		// (set) Token: 0x060037E7 RID: 14311 RVA: 0x000C8DA8 File Offset: 0x000C6FA8
		public virtual bool Locked
		{
			get
			{
				return this._lockCount > 0;
			}
			set
			{
				SynchronizationAttribute obj;
				if (value)
				{
					this.AcquireLock();
					obj = this;
					lock (obj)
					{
						if (this._lockCount > 1)
						{
							this.ReleaseLock();
						}
						return;
					}
				}
				obj = this;
				lock (obj)
				{
					while (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
					{
						this.ReleaseLock();
					}
				}
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000C8E38 File Offset: 0x000C7038
		internal void AcquireLock()
		{
			this._mutex.WaitOne();
			lock (this)
			{
				this._ownerThread = Thread.CurrentThread;
				this._lockCount++;
			}
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000C8E94 File Offset: 0x000C7094
		internal void ReleaseLock()
		{
			lock (this)
			{
				if (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
				{
					this._lockCount--;
					this._mutex.ReleaseMutex();
					if (this._lockCount == 0)
					{
						this._ownerThread = null;
					}
				}
			}
		}

		/// <summary>Adds the <see langword="Synchronized" /> context property to the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the property.</param>
		// Token: 0x060037EA RID: 14314 RVA: 0x000C8F08 File Offset: 0x000C7108
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor != 1)
			{
				ctorMsg.ContextProperties.Add(this);
			}
		}

		/// <summary>Creates a CallOut sink and chains it in front of the provided chain of sinks at the context boundary on the client end of a remoting call.</summary>
		/// <param name="nextSink">The chain of sinks composed so far.</param>
		/// <returns>The composite sink chain with the new CallOut sink.</returns>
		// Token: 0x060037EB RID: 14315 RVA: 0x000C8F20 File Offset: 0x000C7120
		[SecurityCritical]
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			return new SynchronizedClientContextSink(nextSink, this);
		}

		/// <summary>Creates a synchronized dispatch sink and chains it in front of the provided chain of sinks at the context boundary on the server end of a remoting call.</summary>
		/// <param name="nextSink">The chain of sinks composed so far.</param>
		/// <returns>The composite sink chain with the new synchronized dispatch sink.</returns>
		// Token: 0x060037EC RID: 14316 RVA: 0x000C8F29 File Offset: 0x000C7129
		[SecurityCritical]
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			return new SynchronizedServerContextSink(nextSink, this);
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <param name="ctx">The context to check.</param>
		/// <param name="msg">Information gathered at construction time of the context bound object marked by this attribute. The <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> can inspect, add to, and remove properties from the context while determining if the context is acceptable to it.</param>
		/// <returns>
		///   <see langword="true" /> if the passed in context is OK; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctx" /> or <paramref name="msg" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060037ED RID: 14317 RVA: 0x000C8F34 File Offset: 0x000C7134
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			SynchronizationAttribute synchronizationAttribute = ctx.GetProperty("Synchronization") as SynchronizationAttribute;
			int flavor = this._flavor;
			switch (flavor)
			{
			case 1:
				return synchronizationAttribute == null;
			case 2:
				return true;
			case 3:
				break;
			case 4:
				return synchronizationAttribute != null;
			default:
				if (flavor == 8)
				{
					return false;
				}
				break;
			}
			return false;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000C8F88 File Offset: 0x000C7188
		internal static void ExitContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = false;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000C8FC4 File Offset: 0x000C71C4
		internal static void EnterContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = true;
		}

		/// <summary>Indicates that the class to which this attribute is applied cannot be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x040025B7 RID: 9655
		public const int NOT_SUPPORTED = 1;

		/// <summary>Indicates that the class to which this attribute is applied is not dependent on whether the context has synchronization. This field is constant.</summary>
		// Token: 0x040025B8 RID: 9656
		public const int SUPPORTED = 2;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x040025B9 RID: 9657
		public const int REQUIRED = 4;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context with a new instance of the synchronization property each time. This field is constant.</summary>
		// Token: 0x040025BA RID: 9658
		public const int REQUIRES_NEW = 8;

		// Token: 0x040025BB RID: 9659
		private bool _bReEntrant;

		// Token: 0x040025BC RID: 9660
		private int _flavor;

		// Token: 0x040025BD RID: 9661
		[NonSerialized]
		private int _lockCount;

		// Token: 0x040025BE RID: 9662
		[NonSerialized]
		private Mutex _mutex = new Mutex(false);

		// Token: 0x040025BF RID: 9663
		[NonSerialized]
		private Thread _ownerThread;
	}
}
