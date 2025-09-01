using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005CB RID: 1483
	internal class ActivationServices
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000CA8C0 File Offset: 0x000C8AC0
		private static IActivator ConstructionActivator
		{
			get
			{
				if (ActivationServices._constructionActivator == null)
				{
					ActivationServices._constructionActivator = new ConstructionLevelActivator();
				}
				return ActivationServices._constructionActivator;
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000CA8D8 File Offset: 0x000C8AD8
		public static IMessage Activate(RemotingProxy proxy, ConstructionCall ctorCall)
		{
			ctorCall.SourceProxy = proxy;
			IMessage message;
			if (Thread.CurrentContext.HasExitSinks && !ctorCall.IsContextOk)
			{
				message = Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(ctorCall);
			}
			else
			{
				message = ActivationServices.RemoteActivate(ctorCall);
			}
			if (message is IConstructionReturnMessage && ((IConstructionReturnMessage)message).Exception == null && proxy.ObjectIdentity == null)
			{
				Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(ctorCall);
				proxy.AttachIdentity(messageTargetIdentity);
			}
			return message;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000CA948 File Offset: 0x000C8B48
		public static IMessage RemoteActivate(IConstructionCallMessage ctorCall)
		{
			IMessage result;
			try
			{
				result = ctorCall.Activator.Activate(ctorCall);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, ctorCall);
			}
			return result;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000CA980 File Offset: 0x000C8B80
		public static object CreateProxyFromAttributes(Type type, object[] activationAttributes)
		{
			string text = null;
			foreach (object obj in activationAttributes)
			{
				if (!(obj is IContextAttribute))
				{
					throw new RemotingException("Activation attribute does not implement the IContextAttribute interface");
				}
				if (obj is UrlAttribute)
				{
					text = ((UrlAttribute)obj).UrlValue;
				}
			}
			if (text != null)
			{
				return RemotingServices.CreateClientProxy(type, text, activationAttributes);
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, activationAttributes);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, activationAttributes);
			}
			return null;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000CAA00 File Offset: 0x000C8C00
		public static ConstructionCall CreateConstructionCall(Type type, string activationUrl, object[] activationAttributes)
		{
			ConstructionCall constructionCall = new ConstructionCall(type);
			if (!type.IsContextful)
			{
				constructionCall.Activator = new AppDomainLevelActivator(activationUrl, ActivationServices.ConstructionActivator);
				constructionCall.IsContextOk = false;
				return constructionCall;
			}
			IActivator activator = ActivationServices.ConstructionActivator;
			activator = new ContextLevelActivator(activator);
			ArrayList arrayList = new ArrayList();
			if (activationAttributes != null)
			{
				arrayList.AddRange(activationAttributes);
			}
			bool flag = activationUrl == ChannelServices.CrossContextUrl;
			Context currentContext = Thread.CurrentContext;
			if (flag)
			{
				using (IEnumerator enumerator = arrayList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((IContextAttribute)enumerator.Current).IsContextOK(currentContext, constructionCall))
						{
							flag = false;
							break;
						}
					}
				}
			}
			foreach (object obj in type.GetCustomAttributes(true))
			{
				if (obj is IContextAttribute)
				{
					flag = (flag && ((IContextAttribute)obj).IsContextOK(currentContext, constructionCall));
					arrayList.Add(obj);
				}
			}
			if (!flag)
			{
				constructionCall.SetActivationAttributes(arrayList.ToArray());
				foreach (object obj2 in arrayList)
				{
					((IContextAttribute)obj2).GetPropertiesForNewContext(constructionCall);
				}
			}
			if (activationUrl != ChannelServices.CrossContextUrl)
			{
				activator = new AppDomainLevelActivator(activationUrl, activator);
			}
			constructionCall.Activator = activator;
			constructionCall.IsContextOk = flag;
			return constructionCall;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000CAB84 File Offset: 0x000C8D84
		public static IMessage CreateInstanceFromMessage(IConstructionCallMessage ctorCall)
		{
			object obj = ActivationServices.AllocateUninitializedClassInstance(ctorCall.ActivationType);
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(ctorCall);
			serverIdentity.AttachServerObject((MarshalByRefObject)obj, Thread.CurrentContext);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (ctorCall.ActivationType.IsContextful && constructionCall != null && constructionCall.SourceProxy != null)
			{
				constructionCall.SourceProxy.AttachIdentity(serverIdentity);
				RemotingServices.InternalExecuteMessage((MarshalByRefObject)constructionCall.SourceProxy.GetTransparentProxy(), ctorCall);
			}
			else
			{
				ctorCall.MethodBase.Invoke(obj, ctorCall.Args);
			}
			return new ConstructionResponse(obj, null, ctorCall);
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000CAC1C File Offset: 0x000C8E1C
		public static object CreateProxyForType(Type type)
		{
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, null);
			}
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfiguration.IsWellKnownClientType(type);
			if (wellKnownClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(wellKnownClientTypeEntry);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, null);
			}
			if (type.IsCOMObject)
			{
				return RemotingServices.CreateClientProxyForComInterop(type);
			}
			return null;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void PushActivationAttributes(Type serverType, object[] attributes)
		{
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void PopActivationAttributes(Type serverType)
		{
		}

		// Token: 0x060038C5 RID: 14533
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object AllocateUninitializedClassInstance(Type type);

		// Token: 0x060038C6 RID: 14534
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableProxyActivation(Type type, bool enable);

		// Token: 0x060038C7 RID: 14535 RVA: 0x0000259F File Offset: 0x0000079F
		public ActivationServices()
		{
		}

		// Token: 0x040025F2 RID: 9714
		private static IActivator _constructionActivator;
	}
}
