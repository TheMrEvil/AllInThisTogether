using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Defines an environment for the objects that are resident inside it and for which a policy can be enforced.</summary>
	// Token: 0x0200058B RID: 1419
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class Context
	{
		// Token: 0x0600378A RID: 14218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterContext(Context ctx);

		// Token: 0x0600378B RID: 14219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseContext(Context ctx);

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> class.</summary>
		// Token: 0x0600378C RID: 14220 RVA: 0x000C80F0 File Offset: 0x000C62F0
		public Context()
		{
			this.domain_id = Thread.GetDomainID();
			this.context_id = Interlocked.Increment(ref Context.global_count);
			Context.RegisterContext(this);
		}

		/// <summary>Cleans up the backing objects for the nondefault contexts.</summary>
		// Token: 0x0600378D RID: 14221 RVA: 0x000C811C File Offset: 0x000C631C
		~Context()
		{
			Context.ReleaseContext(this);
		}

		/// <summary>Gets the default context for the current application domain.</summary>
		/// <returns>The default context for the <see cref="T:System.AppDomain" /> namespace.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x000C8148 File Offset: 0x000C6348
		public static Context DefaultContext
		{
			get
			{
				return AppDomain.InternalGetDefaultContext();
			}
		}

		/// <summary>Gets the context ID for the current context.</summary>
		/// <returns>The context ID for the current context.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000C814F File Offset: 0x000C634F
		public virtual int ContextID
		{
			get
			{
				return this.context_id;
			}
		}

		/// <summary>Gets the array of the current context properties.</summary>
		/// <returns>The current context properties array; otherwise, <see langword="null" /> if the context does not have any properties attributed to it.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000C8157 File Offset: 0x000C6357
		public virtual IContextProperty[] ContextProperties
		{
			get
			{
				if (this.context_properties == null)
				{
					return new IContextProperty[0];
				}
				return this.context_properties.ToArray();
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000C8173 File Offset: 0x000C6373
		internal bool IsDefaultContext
		{
			get
			{
				return this.context_id == 0;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000C817E File Offset: 0x000C637E
		internal bool NeedsContextSink
		{
			get
			{
				return this.context_id != 0 || (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties) || (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties);
			}
		}

		/// <summary>Registers a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface with the remoting service.</summary>
		/// <param name="prop">The dynamic property to register.</param>
		/// <param name="obj">The object/proxy for which the property is registered.</param>
		/// <param name="ctx">The context for which the property is registered.</param>
		/// <returns>
		///   <see langword="true" /> if the property was successfully registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="prop" /> or its name is <see langword="null" />, or it is not dynamic (it does not implement <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />).</exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not <see langword="null" />).</exception>
		// Token: 0x06003793 RID: 14227 RVA: 0x000C81B2 File Offset: 0x000C63B2
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).RegisterDynamicProperty(prop);
		}

		/// <summary>Unregisters a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface.</summary>
		/// <param name="name">The name of the dynamic property to unregister.</param>
		/// <param name="obj">The object/proxy for which the property is registered.</param>
		/// <param name="ctx">The context for which the property is registered.</param>
		/// <returns>
		///   <see langword="true" /> if the object was successfully unregistered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not <see langword="null" />).</exception>
		// Token: 0x06003794 RID: 14228 RVA: 0x000C81C1 File Offset: 0x000C63C1
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).UnregisterDynamicProperty(name);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000C81D0 File Offset: 0x000C63D0
		private static DynamicPropertyCollection GetDynamicPropertyCollection(ContextBoundObject obj, Context ctx)
		{
			if (ctx == null && obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					return RemotingServices.GetRealProxy(obj).ObjectIdentity.ClientDynamicProperties;
				}
				return obj.ObjectIdentity.ServerDynamicProperties;
			}
			else
			{
				if (ctx != null && obj == null)
				{
					if (ctx.context_dynamic_properties == null)
					{
						ctx.context_dynamic_properties = new DynamicPropertyCollection();
					}
					return ctx.context_dynamic_properties;
				}
				if (ctx == null && obj == null)
				{
					if (Context.global_dynamic_properties == null)
					{
						Context.global_dynamic_properties = new DynamicPropertyCollection();
					}
					return Context.global_dynamic_properties;
				}
				throw new ArgumentException("Either obj or ctx must be null");
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000C824F File Offset: 0x000C644F
		internal static void NotifyGlobalDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties)
			{
				Context.global_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000C8272 File Offset: 0x000C6472
		internal static bool HasGlobalDynamicSinks
		{
			get
			{
				return Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000C8287 File Offset: 0x000C6487
		internal void NotifyDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties)
			{
				this.context_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000C82AE File Offset: 0x000C64AE
		internal bool HasDynamicSinks
		{
			get
			{
				return this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000C82C5 File Offset: 0x000C64C5
		internal bool HasExitSinks
		{
			get
			{
				return !(this.GetClientContextSinkChain() is ClientContextTerminatorSink) || this.HasDynamicSinks || Context.HasGlobalDynamicSinks;
			}
		}

		/// <summary>Returns a specific context property, specified by name.</summary>
		/// <param name="name">The name of the property.</param>
		/// <returns>The specified context property.</returns>
		// Token: 0x0600379B RID: 14235 RVA: 0x000C82E4 File Offset: 0x000C64E4
		public virtual IContextProperty GetProperty(string name)
		{
			if (this.context_properties == null)
			{
				return null;
			}
			foreach (IContextProperty contextProperty in this.context_properties)
			{
				if (contextProperty.Name == name)
				{
					return contextProperty;
				}
			}
			return null;
		}

		/// <summary>Sets a specific context property by name.</summary>
		/// <param name="prop">The actual context property.</param>
		/// <exception cref="T:System.InvalidOperationException">The context is frozen.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property or the property name is <see langword="null" />.</exception>
		// Token: 0x0600379C RID: 14236 RVA: 0x000C8350 File Offset: 0x000C6550
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("IContextProperty");
			}
			if (this == Context.DefaultContext)
			{
				throw new InvalidOperationException("Can not add properties to default context");
			}
			if (this.context_properties == null)
			{
				this.context_properties = new List<IContextProperty>();
			}
			this.context_properties.Add(prop);
		}

		/// <summary>Freezes the context, making it impossible to add or remove context properties from the current context.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context is already frozen.</exception>
		// Token: 0x0600379D RID: 14237 RVA: 0x000C83A0 File Offset: 0x000C65A0
		public virtual void Freeze()
		{
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					contextProperty.Freeze(this);
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> class representation of the current context.</summary>
		/// <returns>A <see cref="T:System.String" /> class representation of the current context.</returns>
		// Token: 0x0600379E RID: 14238 RVA: 0x000C83FC File Offset: 0x000C65FC
		public override string ToString()
		{
			return "ContextID: " + this.context_id.ToString();
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000C8414 File Offset: 0x000C6614
		internal IMessageSink GetServerContextSinkChain()
		{
			if (this.server_context_sink_chain == null)
			{
				if (Context.default_server_context_sink == null)
				{
					Context.default_server_context_sink = new ServerContextTerminatorSink();
				}
				this.server_context_sink_chain = Context.default_server_context_sink;
				if (this.context_properties != null)
				{
					for (int i = this.context_properties.Count - 1; i >= 0; i--)
					{
						IContributeServerContextSink contributeServerContextSink = this.context_properties[i] as IContributeServerContextSink;
						if (contributeServerContextSink != null)
						{
							this.server_context_sink_chain = contributeServerContextSink.GetServerContextSink(this.server_context_sink_chain);
						}
					}
				}
			}
			return this.server_context_sink_chain;
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000C8494 File Offset: 0x000C6694
		internal IMessageSink GetClientContextSinkChain()
		{
			if (this.client_context_sink_chain == null)
			{
				this.client_context_sink_chain = new ClientContextTerminatorSink(this);
				if (this.context_properties != null)
				{
					foreach (IContextProperty contextProperty in this.context_properties)
					{
						IContributeClientContextSink contributeClientContextSink = contextProperty as IContributeClientContextSink;
						if (contributeClientContextSink != null)
						{
							this.client_context_sink_chain = contributeClientContextSink.GetClientContextSink(this.client_context_sink_chain);
						}
					}
				}
			}
			return this.client_context_sink_chain;
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000C851C File Offset: 0x000C671C
		internal IMessageSink CreateServerObjectSinkChain(MarshalByRefObject obj, bool forceInternalExecute)
		{
			IMessageSink messageSink = new StackBuilderSink(obj, forceInternalExecute);
			messageSink = new ServerObjectTerminatorSink(messageSink);
			messageSink = new LeaseSink(messageSink);
			if (this.context_properties != null)
			{
				for (int i = this.context_properties.Count - 1; i >= 0; i--)
				{
					IContributeObjectSink contributeObjectSink = this.context_properties[i] as IContributeObjectSink;
					if (contributeObjectSink != null)
					{
						messageSink = contributeObjectSink.GetObjectSink(obj, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000C8580 File Offset: 0x000C6780
		internal IMessageSink CreateEnvoySink(MarshalByRefObject serverObject)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.Instance;
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					IContributeEnvoySink contributeEnvoySink = contextProperty as IContributeEnvoySink;
					if (contributeEnvoySink != null)
					{
						messageSink = contributeEnvoySink.GetEnvoySink(serverObject, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000C85EC File Offset: 0x000C67EC
		internal static Context SwitchToContext(Context newContext)
		{
			return AppDomain.InternalSetContext(newContext);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000C85F4 File Offset: 0x000C67F4
		internal static Context CreateNewContext(IConstructionCallMessage msg)
		{
			Context context = new Context();
			foreach (object obj in msg.ContextProperties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (context.GetProperty(contextProperty.Name) == null)
				{
					context.SetProperty(contextProperty);
				}
			}
			context.Freeze();
			using (IEnumerator enumerator = msg.ContextProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IContextProperty)enumerator.Current).IsNewContextOK(context))
					{
						throw new RemotingException("A context property did not approve the candidate context for activating the object");
					}
				}
			}
			return context;
		}

		/// <summary>Executes code in another context.</summary>
		/// <param name="deleg">The delegate used to request the callback.</param>
		// Token: 0x060037A5 RID: 14245 RVA: 0x000C86BC File Offset: 0x000C68BC
		public void DoCallBack(CrossContextDelegate deleg)
		{
			lock (this)
			{
				if (this.callback_object == null)
				{
					Context newContext = Context.SwitchToContext(this);
					this.callback_object = new ContextCallbackObject();
					Context.SwitchToContext(newContext);
				}
			}
			this.callback_object.DoCallBack(deleg);
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000C871C File Offset: 0x000C691C
		private LocalDataStore MyLocalStore
		{
			get
			{
				if (this._localDataStore == null)
				{
					LocalDataStoreMgr localDataStoreMgr = Context._localDataStoreMgr;
					lock (localDataStoreMgr)
					{
						if (this._localDataStore == null)
						{
							this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
						}
					}
				}
				return this._localDataStore.Store;
			}
		}

		/// <summary>Allocates an unnamed data slot.</summary>
		/// <returns>A local data slot.</returns>
		// Token: 0x060037A7 RID: 14247 RVA: 0x000C8788 File Offset: 0x000C6988
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Context._localDataStoreMgr.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot.</summary>
		/// <param name="name">The required name for the data slot.</param>
		/// <returns>A local data slot object.</returns>
		// Token: 0x060037A8 RID: 14248 RVA: 0x000C8794 File Offset: 0x000C6994
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
		}

		/// <summary>Frees a named data slot on all the contexts.</summary>
		/// <param name="name">The name of the data slot to free.</param>
		// Token: 0x060037A9 RID: 14249 RVA: 0x000C87A1 File Offset: 0x000C69A1
		public static void FreeNamedDataSlot(string name)
		{
			Context._localDataStoreMgr.FreeNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot.</summary>
		/// <param name="name">The data slot name.</param>
		/// <returns>A local data slot.</returns>
		// Token: 0x060037AA RID: 14250 RVA: 0x000C87AE File Offset: 0x000C69AE
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.GetNamedDataSlot(name);
		}

		/// <summary>Retrieves the value from the specified slot on the current context.</summary>
		/// <param name="slot">The data slot that contains the data.</param>
		/// <returns>The data associated with <paramref name="slot" />.</returns>
		// Token: 0x060037AB RID: 14251 RVA: 0x000C87BB File Offset: 0x000C69BB
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.CurrentContext.MyLocalStore.GetData(slot);
		}

		/// <summary>Sets the data in the specified slot on the current context.</summary>
		/// <param name="slot">The data slot where the data is to be added.</param>
		/// <param name="data">The data that is to be added.</param>
		// Token: 0x060037AC RID: 14252 RVA: 0x000C87CD File Offset: 0x000C69CD
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Thread.CurrentContext.MyLocalStore.SetData(slot, data);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000C87E0 File Offset: 0x000C69E0
		// Note: this type is marked as 'beforefieldinit'.
		static Context()
		{
		}

		// Token: 0x0400259F RID: 9631
		private int domain_id;

		// Token: 0x040025A0 RID: 9632
		private int context_id;

		// Token: 0x040025A1 RID: 9633
		private UIntPtr static_data;

		// Token: 0x040025A2 RID: 9634
		private UIntPtr data;

		// Token: 0x040025A3 RID: 9635
		[ContextStatic]
		private static object[] local_slots;

		// Token: 0x040025A4 RID: 9636
		private static IMessageSink default_server_context_sink;

		// Token: 0x040025A5 RID: 9637
		private IMessageSink server_context_sink_chain;

		// Token: 0x040025A6 RID: 9638
		private IMessageSink client_context_sink_chain;

		// Token: 0x040025A7 RID: 9639
		private List<IContextProperty> context_properties;

		// Token: 0x040025A8 RID: 9640
		private static int global_count;

		// Token: 0x040025A9 RID: 9641
		private volatile LocalDataStoreHolder _localDataStore;

		// Token: 0x040025AA RID: 9642
		private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();

		// Token: 0x040025AB RID: 9643
		private static DynamicPropertyCollection global_dynamic_properties;

		// Token: 0x040025AC RID: 9644
		private DynamicPropertyCollection context_dynamic_properties;

		// Token: 0x040025AD RID: 9645
		private ContextCallbackObject callback_object;
	}
}
