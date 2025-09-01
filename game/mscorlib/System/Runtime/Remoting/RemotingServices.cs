using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects and proxies. This class cannot be inherited.</summary>
	// Token: 0x0200056B RID: 1387
	[ComVisible(true)]
	public static class RemotingServices
	{
		// Token: 0x0600366A RID: 13930 RVA: 0x000C480C File Offset: 0x000C2A0C
		static RemotingServices()
		{
			ISurrogateSelector selector = new RemotingSurrogateSelector();
			StreamingContext context = new StreamingContext(StreamingContextStates.Remoting, null);
			RemotingServices._serializationFormatter = new BinaryFormatter(selector, context);
			RemotingServices._deserializationFormatter = new BinaryFormatter(null, context);
			RemotingServices._serializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			RemotingServices._deserializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			RemotingServices.RegisterInternalChannels();
			RemotingServices.CreateWellKnownServerIdentity(typeof(RemoteActivator), "RemoteActivationService.rem", WellKnownObjectMode.Singleton);
			RemotingServices.FieldSetterMethod = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.NonPublic);
			RemotingServices.FieldGetterMethod = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x0600366B RID: 13931
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalExecute(MethodBase method, object obj, object[] parameters, out object[] out_args);

		// Token: 0x0600366C RID: 13932
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBase GetVirtualMethod(Type type, MethodBase method);

		/// <summary>Returns a Boolean value that indicates whether the given object is a transparent proxy or a real object.</summary>
		/// <param name="proxy">The reference to the object to check.</param>
		/// <returns>A Boolean value that indicates whether the object specified in the <paramref name="proxy" /> parameter is a transparent proxy or a real object.</returns>
		// Token: 0x0600366D RID: 13933
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTransparentProxy(object proxy);

		// Token: 0x0600366E RID: 13934 RVA: 0x000040F7 File Offset: 0x000022F7
		internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
		{
			return true;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000C48C0 File Offset: 0x000C2AC0
		internal static IMethodReturnMessage InternalExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			Type type = target.GetType();
			MethodBase methodBase;
			if (reqMsg.MethodBase.DeclaringType == type || reqMsg.MethodBase == RemotingServices.FieldSetterMethod || reqMsg.MethodBase == RemotingServices.FieldGetterMethod)
			{
				methodBase = reqMsg.MethodBase;
			}
			else
			{
				methodBase = RemotingServices.GetVirtualMethod(type, reqMsg.MethodBase);
				if (methodBase == null)
				{
					throw new RemotingException(string.Format("Cannot resolve method {0}:{1}", type, reqMsg.MethodName));
				}
			}
			if (reqMsg.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = reqMsg.MethodBase.GetGenericArguments();
				methodBase = ((MethodInfo)methodBase).GetGenericMethodDefinition().MakeGenericMethod(genericArguments);
			}
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(reqMsg.LogicalCallContext);
			ReturnMessage result;
			try
			{
				object[] array;
				object ret = RemotingServices.InternalExecute(methodBase, target, reqMsg.Args, out array);
				ParameterInfo[] parameters = methodBase.GetParameters();
				object[] array2 = new object[parameters.Length];
				int outArgsCount = 0;
				int num = 0;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					if (parameterInfo.IsOut && !parameterInfo.ParameterType.IsByRef)
					{
						array2[outArgsCount++] = reqMsg.GetArg(parameterInfo.Position);
					}
					else if (parameterInfo.ParameterType.IsByRef)
					{
						array2[outArgsCount++] = array[num++];
					}
					else
					{
						array2[outArgsCount++] = null;
					}
				}
				LogicalCallContext logicalCallContext2 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
				result = new ReturnMessage(ret, array2, outArgsCount, logicalCallContext2, reqMsg);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, reqMsg);
			}
			CallContext.SetLogicalCallContext(logicalCallContext);
			return result;
		}

		/// <summary>Connects to the specified remote object, and executes the provided <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> on it.</summary>
		/// <param name="target">The remote object whose method you want to call.</param>
		/// <param name="reqMsg">A method call message to the specified remote object's method.</param>
		/// <returns>The response of the remote method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The method was called from a context other than the native context of the object.</exception>
		// Token: 0x06003670 RID: 13936 RVA: 0x000C4A60 File Offset: 0x000C2C60
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (RemotingServices.IsTransparentProxy(target))
			{
				return (IMethodReturnMessage)RemotingServices.GetRealProxy(target).Invoke(reqMsg);
			}
			return RemotingServices.InternalExecuteMessage(target, reqMsg);
		}

		/// <summary>Creates a proxy for a well-known object, given the <see cref="T:System.Type" /> and URL.</summary>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of a well-known object on the server end to which you want to connect.</param>
		/// <param name="url">The URL of the server class.</param>
		/// <returns>A proxy to the remote object that points to an endpoint served by the specified well-known object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003671 RID: 13937 RVA: 0x000C4A83 File Offset: 0x000C2C83
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url)
		{
			return RemotingServices.GetRemoteObject(new ObjRef(classToProxy, url, null), classToProxy);
		}

		/// <summary>Creates a proxy for a well-known object, given the <see cref="T:System.Type" />, URL, and channel-specific data.</summary>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of the well-known object to which you want to connect.</param>
		/// <param name="url">The URL of the well-known object.</param>
		/// <param name="data">Channel specific data. Can be <see langword="null" />.</param>
		/// <returns>A proxy that points to an endpoint that is served by the requested well-known object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003672 RID: 13938 RVA: 0x000C4A93 File Offset: 0x000C2C93
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			return RemotingServices.GetRemoteObject(new ObjRef(classToProxy, url, data), classToProxy);
		}

		/// <summary>Stops an object from receiving any further messages through the registered remoting channels.</summary>
		/// <param name="obj">Object to disconnect from its channel.</param>
		/// <returns>
		///   <see langword="true" /> if the object was disconnected from the registered remoting channels successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a proxy.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003673 RID: 13939 RVA: 0x000C4AA4 File Offset: 0x000C2CA4
		public static bool Disconnect(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			ServerIdentity serverIdentity;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				if (!realProxy.GetProxiedType().IsContextful || !(realProxy.ObjectIdentity is ServerIdentity))
				{
					throw new ArgumentException("The obj parameter is a proxy.");
				}
				serverIdentity = (realProxy.ObjectIdentity as ServerIdentity);
			}
			else
			{
				serverIdentity = obj.ObjectIdentity;
				obj.ObjectIdentity = null;
			}
			if (serverIdentity == null || !serverIdentity.IsConnected)
			{
				return false;
			}
			LifetimeServices.StopTrackingLifetime(serverIdentity);
			RemotingServices.DisposeIdentity(serverIdentity);
			TrackingServices.NotifyDisconnectedObject(obj);
			return true;
		}

		/// <summary>Returns the <see cref="T:System.Type" /> of the object with the specified URI.</summary>
		/// <param name="URI">The URI of the object whose <see cref="T:System.Type" /> is requested.</param>
		/// <returns>The <see cref="T:System.Type" /> of the object with the specified URI.</returns>
		/// <exception cref="T:System.Security.SecurityException">Either the immediate caller does not have infrastructure permission, or at least one of the callers higher in the callstack does not have permission to retrieve the type information of non-public members.</exception>
		// Token: 0x06003674 RID: 13940 RVA: 0x000C4B30 File Offset: 0x000C2D30
		public static Type GetServerTypeForUri(string URI)
		{
			ServerIdentity serverIdentity = RemotingServices.GetIdentityForUri(URI) as ServerIdentity;
			if (serverIdentity == null)
			{
				return null;
			}
			return serverIdentity.ObjectType;
		}

		/// <summary>Retrieves the URI for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.MarshalByRefObject" /> for which a URI is requested.</param>
		/// <returns>The URI of the specified object if it has one, or <see langword="null" /> if the object has not yet been marshaled.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003675 RID: 13941 RVA: 0x000C4B54 File Offset: 0x000C2D54
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			Identity objectIdentity = RemotingServices.GetObjectIdentity(obj);
			if (objectIdentity is ClientIdentity)
			{
				return ((ClientIdentity)objectIdentity).TargetUri;
			}
			if (objectIdentity != null)
			{
				return objectIdentity.ObjectUri;
			}
			return null;
		}

		/// <summary>Takes a <see cref="T:System.Runtime.Remoting.ObjRef" /> and creates a proxy object out of it.</summary>
		/// <param name="objectRef">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object for which the proxy is being created.</param>
		/// <returns>A proxy to the object that the given <see cref="T:System.Runtime.Remoting.ObjRef" /> represents.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Runtime.Remoting.ObjRef" /> instance specified in the <paramref name="objectRef" /> parameter is not well-formed.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003676 RID: 13942 RVA: 0x000C4B87 File Offset: 0x000C2D87
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.Unmarshal(objectRef, true);
		}

		/// <summary>Takes a <see cref="T:System.Runtime.Remoting.ObjRef" /> and creates a proxy object out of it, refining it to the type on the server.</summary>
		/// <param name="objectRef">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object for which the proxy is being created.</param>
		/// <param name="fRefine">
		///   <see langword="true" /> to refine the proxy to the type on the server; otherwise, <see langword="false" />.</param>
		/// <returns>A proxy to the object that the given <see cref="T:System.Runtime.Remoting.ObjRef" /> represents.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Runtime.Remoting.ObjRef" /> instance specified in the <paramref name="objectRef" /> parameter is not well-formed.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003677 RID: 13943 RVA: 0x000C4B90 File Offset: 0x000C2D90
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			Type type = fRefine ? objectRef.ServerType : typeof(MarshalByRefObject);
			if (type == null)
			{
				type = typeof(MarshalByRefObject);
			}
			if (objectRef.IsReferenceToWellKnow)
			{
				object remoteObject = RemotingServices.GetRemoteObject(objectRef, type);
				TrackingServices.NotifyUnmarshaledObject(remoteObject, objectRef);
				return remoteObject;
			}
			if (type.IsContextful)
			{
				ProxyAttribute proxyAttribute = (ProxyAttribute)Attribute.GetCustomAttribute(type, typeof(ProxyAttribute), true);
				if (proxyAttribute != null)
				{
					object transparentProxy = proxyAttribute.CreateProxy(objectRef, type, null, null).GetTransparentProxy();
					TrackingServices.NotifyUnmarshaledObject(transparentProxy, objectRef);
					return transparentProxy;
				}
			}
			object proxyForRemoteObject = RemotingServices.GetProxyForRemoteObject(objectRef, type);
			TrackingServices.NotifyUnmarshaledObject(proxyForRemoteObject, objectRef);
			return proxyForRemoteObject;
		}

		/// <summary>Takes a <see cref="T:System.MarshalByRefObject" />, registers it with the remoting infrastructure, and converts it into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class.</summary>
		/// <param name="Obj">The object to convert.</param>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The <paramref name="Obj" /> parameter is an object proxy.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003678 RID: 13944 RVA: 0x000C4C25 File Offset: 0x000C2E25
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.Marshal(Obj, null, null);
		}

		/// <summary>Converts the given <see cref="T:System.MarshalByRefObject" /> into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with the specified URI.</summary>
		/// <param name="Obj">The object to convert.</param>
		/// <param name="URI">The specified URI with which to initialize the new <see cref="T:System.Runtime.Remoting.ObjRef" />. Can be <see langword="null" />.</param>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="Obj" /> is an object proxy, and the <paramref name="URI" /> parameter is not <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003679 RID: 13945 RVA: 0x000C4C2F File Offset: 0x000C2E2F
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.Marshal(Obj, URI, null);
		}

		/// <summary>Takes a <see cref="T:System.MarshalByRefObject" /> and converts it into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with the specified URI, and the provided <see cref="T:System.Type" />.</summary>
		/// <param name="Obj">The object to convert into a <see cref="T:System.Runtime.Remoting.ObjRef" />.</param>
		/// <param name="ObjURI">The URI the object specified in the <paramref name="Obj" /> parameter is marshaled with. Can be <see langword="null" />.</param>
		/// <param name="RequestedType">The <see cref="T:System.Type" /><paramref name="Obj" /> is marshaled as. Can be <see langword="null" />.</param>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="Obj" /> is a proxy of a remote object, and the <paramref name="ObjUri" /> parameter is not <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600367A RID: 13946 RVA: 0x000C4C3C File Offset: 0x000C2E3C
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				Identity objectIdentity = realProxy.ObjectIdentity;
				if (objectIdentity != null)
				{
					if (realProxy.GetProxiedType().IsContextful && !objectIdentity.IsConnected)
					{
						ClientActivatedIdentity clientActivatedIdentity = (ClientActivatedIdentity)objectIdentity;
						if (ObjURI == null)
						{
							ObjURI = RemotingServices.NewUri();
						}
						clientActivatedIdentity.ObjectUri = ObjURI;
						RemotingServices.RegisterServerIdentity(clientActivatedIdentity);
						clientActivatedIdentity.StartTrackingLifetime((ILease)Obj.InitializeLifetimeService());
						return clientActivatedIdentity.CreateObjRef(RequestedType);
					}
					if (ObjURI != null)
					{
						throw new RemotingException("It is not possible marshal a proxy of a remote object.");
					}
					ObjRef objRef = realProxy.ObjectIdentity.CreateObjRef(RequestedType);
					TrackingServices.NotifyMarshaledObject(Obj, objRef);
					return objRef;
				}
			}
			if (RequestedType == null)
			{
				RequestedType = Obj.GetType();
			}
			if (ObjURI == null)
			{
				if (Obj.ObjectIdentity == null)
				{
					ObjURI = RemotingServices.NewUri();
					RemotingServices.CreateClientActivatedServerIdentity(Obj, RequestedType, ObjURI);
				}
			}
			else
			{
				ClientActivatedIdentity clientActivatedIdentity2 = RemotingServices.GetIdentityForUri("/" + ObjURI) as ClientActivatedIdentity;
				if (clientActivatedIdentity2 == null || Obj != clientActivatedIdentity2.GetServerObject())
				{
					RemotingServices.CreateClientActivatedServerIdentity(Obj, RequestedType, ObjURI);
				}
			}
			ObjRef objRef2;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				objRef2 = RemotingServices.GetRealProxy(Obj).ObjectIdentity.CreateObjRef(RequestedType);
			}
			else
			{
				objRef2 = Obj.CreateObjRef(RequestedType);
			}
			TrackingServices.NotifyMarshaledObject(Obj, objRef2);
			return objRef2;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000C4D58 File Offset: 0x000C2F58
		private static string NewUri()
		{
			if (RemotingServices.app_id == null)
			{
				object obj = RemotingServices.app_id_lock;
				lock (obj)
				{
					if (RemotingServices.app_id == null)
					{
						RemotingServices.app_id = Guid.NewGuid().ToString().Replace('-', '_') + "/";
					}
				}
			}
			int num = Interlocked.Increment(ref RemotingServices.next_id);
			return string.Concat(new string[]
			{
				RemotingServices.app_id,
				Environment.TickCount.ToString("x"),
				"_",
				num.ToString(),
				".rem"
			});
		}

		/// <summary>Returns the real proxy backing the specified transparent proxy.</summary>
		/// <param name="proxy">A transparent proxy.</param>
		/// <returns>The real proxy instance backing the transparent proxy.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600367C RID: 13948 RVA: 0x000C4E18 File Offset: 0x000C3018
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static RealProxy GetRealProxy(object proxy)
		{
			if (!RemotingServices.IsTransparentProxy(proxy))
			{
				throw new RemotingException("Cannot get the real proxy from an object that is not a transparent proxy.");
			}
			return ((TransparentProxy)proxy)._rp;
		}

		/// <summary>Returns the method base from the given <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" />.</summary>
		/// <param name="msg">The method message to extract the method base from.</param>
		/// <returns>The method base extracted from the <paramref name="msg" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">Either the immediate caller does not have infrastructure permission, or at least one of the callers higher in the callstack does not have permission to retrieve the type information of non-public members.</exception>
		// Token: 0x0600367D RID: 13949 RVA: 0x000C4E38 File Offset: 0x000C3038
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			Type type = Type.GetType(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException("Type '" + msg.TypeName + "' not found.");
			}
			return RemotingServices.GetMethodBaseFromName(type, msg.MethodName, (Type[])msg.MethodSignature);
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000C4E8C File Offset: 0x000C308C
		internal static MethodBase GetMethodBaseFromName(Type type, string methodName, Type[] signature)
		{
			if (type.IsInterface)
			{
				return RemotingServices.FindInterfaceMethod(type, methodName, signature);
			}
			MethodBase method;
			if (signature == null)
			{
				method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
			}
			if (method != null)
			{
				return method;
			}
			if (methodName == "FieldSetter")
			{
				return RemotingServices.FieldSetterMethod;
			}
			if (methodName == "FieldGetter")
			{
				return RemotingServices.FieldGetterMethod;
			}
			if (signature == null)
			{
				return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			}
			return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000C4F18 File Offset: 0x000C3118
		private static MethodBase FindInterfaceMethod(Type type, string methodName, Type[] signature)
		{
			MethodBase methodBase;
			if (signature == null)
			{
				methodBase = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				methodBase = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
			}
			if (methodBase != null)
			{
				return methodBase;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				methodBase = RemotingServices.FindInterfaceMethod(interfaces[i], methodName, signature);
				if (methodBase != null)
				{
					return methodBase;
				}
			}
			return null;
		}

		/// <summary>Serializes the specified marshal by reference object into the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="obj">The object to serialize.</param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> into which the object is serialized.</param>
		/// <param name="context">The source and destination of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> or <paramref name="info" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003680 RID: 13952 RVA: 0x000C4F7A File Offset: 0x000C317A
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			RemotingServices.Marshal((MarshalByRefObject)obj).GetObjectData(info, context);
		}

		/// <summary>Returns the <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object from the specified proxy.</summary>
		/// <param name="obj">A proxy connected to the object you want to create a <see cref="T:System.Runtime.Remoting.ObjRef" /> for.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object the specified proxy is connected to, or <see langword="null" /> if the object or proxy have not been marshaled.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003681 RID: 13953 RVA: 0x000C4F9C File Offset: 0x000C319C
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			Identity objectIdentity = RemotingServices.GetObjectIdentity(obj);
			if (objectIdentity == null)
			{
				return null;
			}
			return objectIdentity.CreateObjRef(null);
		}

		/// <summary>Returns a lifetime service object that controls the lifetime policy of the specified object.</summary>
		/// <param name="obj">The object to obtain lifetime service for.</param>
		/// <returns>The object that controls the lifetime of <paramref name="obj" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003682 RID: 13954 RVA: 0x000C4FBC File Offset: 0x000C31BC
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.GetLifetimeService();
		}

		/// <summary>Returns a chain of envoy sinks that should be used when sending messages to the remote object represented by the specified proxy.</summary>
		/// <param name="obj">The proxy of the remote object that requested envoy sinks are associated with.</param>
		/// <returns>A chain of envoy sinks associated with the specified proxy.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003683 RID: 13955 RVA: 0x000C4FC9 File Offset: 0x000C31C9
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				return ((ClientIdentity)RemotingServices.GetRealProxy(obj).ObjectIdentity).EnvoySink;
			}
			throw new ArgumentException("obj must be a proxy.", "obj");
		}

		/// <summary>Logs the stage in a remoting exchange to an external debugger.</summary>
		/// <param name="stage">An internally defined constant that identifies the stage in a remoting exchange.</param>
		// Token: 0x06003684 RID: 13956 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete("It existed for only internal use in .NET and unimplemented in mono")]
		[Conditional("REMOTING_PERF")]
		[MonoTODO]
		public static void LogRemotingStage(int stage)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a session ID for a message.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> for which a session ID is requested.</param>
		/// <returns>A session ID string that uniquely identifies the current session.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003685 RID: 13957 RVA: 0x000C4FF8 File Offset: 0x000C31F8
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		/// <summary>Returns a Boolean value that indicates whether the method in the given message is overloaded.</summary>
		/// <param name="msg">The message that contains a call to the method in question.</param>
		/// <returns>
		///   <see langword="true" /> if the method called in <paramref name="msg" /> is overloaded; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003686 RID: 13958 RVA: 0x000C5000 File Offset: 0x000C3200
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			RuntimeType runtimeType = (RuntimeType)msg.MethodBase.DeclaringType;
			return runtimeType.GetMethodsByName(msg.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, RuntimeType.MemberListType.CaseSensitive, runtimeType).Length > 1;
		}

		/// <summary>Returns a Boolean value that indicates whether the object specified by the given transparent proxy is contained in a different application domain than the object that called the current method.</summary>
		/// <param name="tp">The object to check.</param>
		/// <returns>
		///   <see langword="true" /> if the object is out of the current application domain; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003687 RID: 13959 RVA: 0x000C5034 File Offset: 0x000C3234
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			MarshalByRefObject marshalByRefObject = tp as MarshalByRefObject;
			return marshalByRefObject != null && RemotingServices.GetObjectIdentity(marshalByRefObject) is ClientIdentity;
		}

		/// <summary>Returns a Boolean value that indicates whether the object represented by the given proxy is contained in a different context than the object that called the current method.</summary>
		/// <param name="tp">The object to check.</param>
		/// <returns>
		///   <see langword="true" /> if the object is out of the current context; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003688 RID: 13960 RVA: 0x000C505C File Offset: 0x000C325C
		public static bool IsObjectOutOfContext(object tp)
		{
			MarshalByRefObject marshalByRefObject = tp as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			Identity objectIdentity = RemotingServices.GetObjectIdentity(marshalByRefObject);
			if (objectIdentity == null)
			{
				return false;
			}
			ServerIdentity serverIdentity = objectIdentity as ServerIdentity;
			return serverIdentity == null || serverIdentity.Context != Thread.CurrentContext;
		}

		/// <summary>Returns a Boolean value that indicates whether the client that called the method specified in the given message is waiting for the server to finish processing the method before continuing execution.</summary>
		/// <param name="method">The method in question.</param>
		/// <returns>
		///   <see langword="true" /> if the method is one way; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003689 RID: 13961 RVA: 0x000C509D File Offset: 0x000C329D
		public static bool IsOneWay(MethodBase method)
		{
			return method.IsDefined(typeof(OneWayAttribute), false);
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000C50B0 File Offset: 0x000C32B0
		internal static bool IsAsyncMessage(IMessage msg)
		{
			return msg is MonoMethodMessage && (((MonoMethodMessage)msg).IsAsync || RemotingServices.IsOneWay(((MonoMethodMessage)msg).MethodBase));
		}

		/// <summary>Sets the URI for the subsequent call to the <see cref="M:System.Runtime.Remoting.RemotingServices.Marshal(System.MarshalByRefObject)" /> method.</summary>
		/// <param name="obj">The object to set a URI for.</param>
		/// <param name="uri">The URI to assign to the specified object.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="obj" /> is not a local object, has already been marshaled, or the current method has already been called on.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600368B RID: 13963 RVA: 0x000C50E0 File Offset: 0x000C32E0
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity objectIdentity = realProxy.ObjectIdentity;
				if (objectIdentity != null && !(objectIdentity is ServerIdentity) && !realProxy.GetProxiedType().IsContextful)
				{
					throw new RemotingException("SetObjectUriForMarshal method should only be called for MarshalByRefObjects that exist in the current AppDomain.");
				}
			}
			RemotingServices.Marshal(obj, uri);
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000C5130 File Offset: 0x000C3330
		internal static object CreateClientProxy(ActivatedClientTypeEntry entry, object[] activationAttributes)
		{
			if (entry.ContextAttributes != null || activationAttributes != null)
			{
				ArrayList arrayList = new ArrayList();
				if (entry.ContextAttributes != null)
				{
					arrayList.AddRange(entry.ContextAttributes);
				}
				if (activationAttributes != null)
				{
					arrayList.AddRange(activationAttributes);
				}
				return RemotingServices.CreateClientProxy(entry.ObjectType, entry.ApplicationUrl, arrayList.ToArray());
			}
			return RemotingServices.CreateClientProxy(entry.ObjectType, entry.ApplicationUrl, null);
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000C5198 File Offset: 0x000C3398
		internal static object CreateClientProxy(Type objectType, string url, object[] activationAttributes)
		{
			string text = url;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			text += "RemoteActivationService.rem";
			string text2;
			RemotingServices.GetClientChannelSinkChain(text, null, out text2);
			return new RemotingProxy(objectType, text, activationAttributes).GetTransparentProxy();
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000C51E3 File Offset: 0x000C33E3
		internal static object CreateClientProxy(WellKnownClientTypeEntry entry)
		{
			return RemotingServices.Connect(entry.ObjectType, entry.ObjectUrl, null);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000C51F8 File Offset: 0x000C33F8
		internal static object CreateClientProxyForContextBound(Type type, object[] activationAttributes)
		{
			if (type.IsContextful)
			{
				ProxyAttribute proxyAttribute = (ProxyAttribute)Attribute.GetCustomAttribute(type, typeof(ProxyAttribute), true);
				if (proxyAttribute != null)
				{
					return proxyAttribute.CreateInstance(type);
				}
			}
			return new RemotingProxy(type, ChannelServices.CrossContextUrl, activationAttributes).GetTransparentProxy();
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000C5240 File Offset: 0x000C3440
		internal static object CreateClientProxyForComInterop(Type type)
		{
			return ComInteropProxy.CreateProxy(type).GetTransparentProxy();
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000C5250 File Offset: 0x000C3450
		internal static Identity GetIdentityForUri(string uri)
		{
			string text = RemotingServices.GetNormalizedUri(uri);
			Hashtable obj = RemotingServices.uri_hash;
			Identity result;
			lock (obj)
			{
				Identity identity = (Identity)RemotingServices.uri_hash[text];
				if (identity == null)
				{
					text = RemotingServices.RemoveAppNameFromUri(uri);
					if (text != null)
					{
						identity = (Identity)RemotingServices.uri_hash[text];
					}
				}
				result = identity;
			}
			return result;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000C52C4 File Offset: 0x000C34C4
		private static string RemoveAppNameFromUri(string uri)
		{
			string text = RemotingConfiguration.ApplicationName;
			if (text == null)
			{
				return null;
			}
			text = "/" + text + "/";
			if (uri.StartsWith(text))
			{
				return uri.Substring(text.Length);
			}
			return null;
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000C5304 File Offset: 0x000C3504
		internal static Identity GetObjectIdentity(MarshalByRefObject obj)
		{
			if (RemotingServices.IsTransparentProxy(obj))
			{
				return RemotingServices.GetRealProxy(obj).ObjectIdentity;
			}
			return obj.ObjectIdentity;
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000C5320 File Offset: 0x000C3520
		internal static ClientIdentity GetOrCreateClientIdentity(ObjRef objRef, Type proxyType, out object clientProxy)
		{
			object channelData = (objRef.ChannelInfo != null) ? objRef.ChannelInfo.ChannelData : null;
			string uri;
			IMessageSink clientChannelSinkChain = RemotingServices.GetClientChannelSinkChain(objRef.URI, channelData, out uri);
			if (uri == null)
			{
				uri = objRef.URI;
			}
			Hashtable obj = RemotingServices.uri_hash;
			ClientIdentity result;
			lock (obj)
			{
				clientProxy = null;
				string normalizedUri = RemotingServices.GetNormalizedUri(objRef.URI);
				ClientIdentity clientIdentity = RemotingServices.uri_hash[normalizedUri] as ClientIdentity;
				if (clientIdentity != null)
				{
					clientProxy = clientIdentity.ClientProxy;
					if (clientProxy != null)
					{
						return clientIdentity;
					}
					RemotingServices.DisposeIdentity(clientIdentity);
				}
				clientIdentity = new ClientIdentity(uri, objRef);
				clientIdentity.ChannelSink = clientChannelSinkChain;
				RemotingServices.uri_hash[normalizedUri] = clientIdentity;
				if (proxyType != null)
				{
					RemotingProxy remotingProxy = new RemotingProxy(proxyType, clientIdentity);
					CrossAppDomainSink crossAppDomainSink = clientChannelSinkChain as CrossAppDomainSink;
					if (crossAppDomainSink != null)
					{
						remotingProxy.SetTargetDomain(crossAppDomainSink.TargetDomainId);
					}
					clientProxy = remotingProxy.GetTransparentProxy();
					clientIdentity.ClientProxy = (MarshalByRefObject)clientProxy;
				}
				result = clientIdentity;
			}
			return result;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000C5438 File Offset: 0x000C3638
		private static IMessageSink GetClientChannelSinkChain(string url, object channelData, out string objectUri)
		{
			IMessageSink messageSink = ChannelServices.CreateClientChannelSinkChain(url, channelData, out objectUri);
			if (messageSink != null)
			{
				return messageSink;
			}
			if (url != null)
			{
				throw new RemotingException(string.Format("Cannot create channel sink to connect to URL {0}. An appropriate channel has probably not been registered.", url));
			}
			throw new RemotingException(string.Format("Cannot create channel sink to connect to the remote object. An appropriate channel has probably not been registered.", url));
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000C546A File Offset: 0x000C366A
		internal static ClientActivatedIdentity CreateContextBoundObjectIdentity(Type objectType)
		{
			return new ClientActivatedIdentity(null, objectType)
			{
				ChannelSink = ChannelServices.CrossContextChannel
			};
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000C547E File Offset: 0x000C367E
		internal static ClientActivatedIdentity CreateClientActivatedServerIdentity(MarshalByRefObject realObject, Type objectType, string objectUri)
		{
			ClientActivatedIdentity clientActivatedIdentity = new ClientActivatedIdentity(objectUri, objectType);
			clientActivatedIdentity.AttachServerObject(realObject, Context.DefaultContext);
			RemotingServices.RegisterServerIdentity(clientActivatedIdentity);
			clientActivatedIdentity.StartTrackingLifetime((ILease)realObject.InitializeLifetimeService());
			return clientActivatedIdentity;
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000C54AC File Offset: 0x000C36AC
		internal static ServerIdentity CreateWellKnownServerIdentity(Type objectType, string objectUri, WellKnownObjectMode mode)
		{
			ServerIdentity serverIdentity;
			if (mode == WellKnownObjectMode.SingleCall)
			{
				serverIdentity = new SingleCallIdentity(objectUri, Context.DefaultContext, objectType);
			}
			else
			{
				serverIdentity = new SingletonIdentity(objectUri, Context.DefaultContext, objectType);
			}
			RemotingServices.RegisterServerIdentity(serverIdentity);
			return serverIdentity;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000C54E0 File Offset: 0x000C36E0
		private static void RegisterServerIdentity(ServerIdentity identity)
		{
			Hashtable obj = RemotingServices.uri_hash;
			lock (obj)
			{
				if (RemotingServices.uri_hash.ContainsKey(identity.ObjectUri))
				{
					throw new RemotingException("Uri already in use: " + identity.ObjectUri + ".");
				}
				RemotingServices.uri_hash[identity.ObjectUri] = identity;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000C5558 File Offset: 0x000C3758
		internal static object GetProxyForRemoteObject(ObjRef objref, Type classToProxy)
		{
			ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(objref.URI) as ClientActivatedIdentity;
			if (clientActivatedIdentity != null)
			{
				return clientActivatedIdentity.GetServerObject();
			}
			return RemotingServices.GetRemoteObject(objref, classToProxy);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000C5588 File Offset: 0x000C3788
		internal static object GetRemoteObject(ObjRef objRef, Type proxyType)
		{
			object result;
			RemotingServices.GetOrCreateClientIdentity(objRef, proxyType, out result);
			return result;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000C55A0 File Offset: 0x000C37A0
		internal static object GetServerObject(string uri)
		{
			ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(uri) as ClientActivatedIdentity;
			if (clientActivatedIdentity == null)
			{
				throw new RemotingException("Server for uri '" + uri + "' not found");
			}
			return clientActivatedIdentity.GetServerObject();
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000C55CC File Offset: 0x000C37CC
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static byte[] SerializeCallData(object obj)
		{
			LogicalCallContext.Reader logicalCallContext = Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext;
			if (!logicalCallContext.IsNull)
			{
				obj = new RemotingServices.CACD
				{
					d = obj,
					c = logicalCallContext.Clone()
				};
			}
			if (obj == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter serializationFormatter = RemotingServices._serializationFormatter;
			lock (serializationFormatter)
			{
				RemotingServices._serializationFormatter.Serialize(memoryStream, obj);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000C565C File Offset: 0x000C385C
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static object DeserializeCallData(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			MemoryStream serializationStream = new MemoryStream(array);
			BinaryFormatter deserializationFormatter = RemotingServices._deserializationFormatter;
			object obj;
			lock (deserializationFormatter)
			{
				obj = RemotingServices._deserializationFormatter.Deserialize(serializationStream);
			}
			if (obj is RemotingServices.CACD)
			{
				RemotingServices.CACD cacd = (RemotingServices.CACD)obj;
				obj = cacd.d;
				LogicalCallContext logicalCallContext = (LogicalCallContext)cacd.c;
				if (logicalCallContext.HasInfo)
				{
					Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Merge(logicalCallContext);
				}
			}
			return obj;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000C56F0 File Offset: 0x000C38F0
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static byte[] SerializeExceptionData(Exception ex)
		{
			byte[] result = null;
			try
			{
			}
			finally
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter serializationFormatter = RemotingServices._serializationFormatter;
				lock (serializationFormatter)
				{
					RemotingServices._serializationFormatter.Serialize(memoryStream, ex);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000C5754 File Offset: 0x000C3954
		internal static object GetDomainProxy(AppDomain domain)
		{
			byte[] array = null;
			Context currentContext = Thread.CurrentContext;
			try
			{
				array = (byte[])AppDomain.InvokeInDomain(domain, typeof(AppDomain).GetMethod("GetMarshalledDomainObjRef", BindingFlags.Instance | BindingFlags.NonPublic), domain, null);
			}
			finally
			{
				AppDomain.InternalSetContext(currentContext);
			}
			byte[] array2 = new byte[array.Length];
			array.CopyTo(array2, 0);
			return (AppDomain)RemotingServices.Unmarshal((ObjRef)CADSerializer.DeserializeObject(new MemoryStream(array2)));
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000C57D4 File Offset: 0x000C39D4
		private static void RegisterInternalChannels()
		{
			CrossAppDomainChannel.RegisterCrossAppDomainChannel();
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000C57DC File Offset: 0x000C39DC
		internal static void DisposeIdentity(Identity ident)
		{
			Hashtable obj = RemotingServices.uri_hash;
			lock (obj)
			{
				if (!ident.Disposed)
				{
					ClientIdentity clientIdentity = ident as ClientIdentity;
					if (clientIdentity != null)
					{
						RemotingServices.uri_hash.Remove(RemotingServices.GetNormalizedUri(clientIdentity.TargetUri));
					}
					else
					{
						RemotingServices.uri_hash.Remove(ident.ObjectUri);
					}
					ident.Disposed = true;
				}
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000C5858 File Offset: 0x000C3A58
		internal static Identity GetMessageTargetIdentity(IMessage msg)
		{
			if (msg is IInternalMessage)
			{
				return ((IInternalMessage)msg).TargetIdentity;
			}
			Hashtable obj = RemotingServices.uri_hash;
			Identity result;
			lock (obj)
			{
				string normalizedUri = RemotingServices.GetNormalizedUri(((IMethodMessage)msg).Uri);
				result = (RemotingServices.uri_hash[normalizedUri] as ServerIdentity);
			}
			return result;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000C58C8 File Offset: 0x000C3AC8
		internal static void SetMessageTargetIdentity(IMessage msg, Identity ident)
		{
			if (msg is IInternalMessage)
			{
				((IInternalMessage)msg).TargetIdentity = ident;
			}
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000C58E0 File Offset: 0x000C3AE0
		internal static bool UpdateOutArgObject(ParameterInfo pi, object local, object remote)
		{
			if (pi.ParameterType.IsArray && ((Array)local).Rank == 1)
			{
				Array array = (Array)local;
				if (array.Rank == 1)
				{
					Array.Copy((Array)remote, array, array.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000C592D File Offset: 0x000C3B2D
		private static string GetNormalizedUri(string uri)
		{
			if (uri.StartsWith("/"))
			{
				return uri.Substring(1);
			}
			return uri;
		}

		// Token: 0x0400254E RID: 9550
		private static Hashtable uri_hash = new Hashtable();

		// Token: 0x0400254F RID: 9551
		private static BinaryFormatter _serializationFormatter;

		// Token: 0x04002550 RID: 9552
		private static BinaryFormatter _deserializationFormatter;

		// Token: 0x04002551 RID: 9553
		private static string app_id;

		// Token: 0x04002552 RID: 9554
		private static readonly object app_id_lock = new object();

		// Token: 0x04002553 RID: 9555
		private static int next_id = 1;

		// Token: 0x04002554 RID: 9556
		private const BindingFlags methodBindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04002555 RID: 9557
		private static readonly MethodInfo FieldSetterMethod;

		// Token: 0x04002556 RID: 9558
		private static readonly MethodInfo FieldGetterMethod;

		// Token: 0x0200056C RID: 1388
		[Serializable]
		private class CACD
		{
			// Token: 0x060036A7 RID: 13991 RVA: 0x0000259F File Offset: 0x0000079F
			public CACD()
			{
			}

			// Token: 0x04002557 RID: 9559
			public object d;

			// Token: 0x04002558 RID: 9560
			public object c;
		}
	}
}
