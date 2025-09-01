using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005AE RID: 1454
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x0600385E RID: 14430 RVA: 0x000CA3A3 File Offset: 0x000C85A3
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000CA3B4 File Offset: 0x000C85B4
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink result;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					result = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink;
					result = crossAppDomainSink;
				}
			}
			return result;
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000CA438 File Offset: 0x000C8638
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000CA440 File Offset: 0x000C8640
		private static CrossAppDomainSink.ProcessMessageRes ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg)
		{
			CrossAppDomainSink.ProcessMessageRes result = default(CrossAppDomainSink.ProcessMessageRes);
			try
			{
				AppDomain.CurrentDomain.ProcessMessageInDomain(arrRequest, cadMsg, out result.arrResponse, out result.cadMrm);
			}
			catch (Exception e)
			{
				IMessage msg = new MethodResponse(e, new ErrorMessage());
				result.arrResponse = CADSerializer.SerializeMessage(msg).GetBuffer();
			}
			return result;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000CA4A4 File Offset: 0x000C86A4
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage result = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage retmsg = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					array2 = CADSerializer.SerializeMessage(msgRequest).GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[]
					{
						array2,
						cadmethodCallMessage
					});
					array = processMessageRes.arrResponse;
					retmsg = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					result = CADSerializer.DeserializeMessage(new MemoryStream(array), msgRequest as IMethodCallMessage);
				}
				else
				{
					result = new MethodResponse(msgRequest as IMethodCallMessage, retmsg);
				}
			}
			catch (Exception e)
			{
				try
				{
					result = new ReturnMessage(e, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return result;
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000CA57C File Offset: 0x000C877C
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest state = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.SendAsyncMessage(data);
				}
				catch
				{
				}
			}, state);
			return null;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000CA5A8 File Offset: 0x000C87A8
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage msg = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(msg);
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000CA5D6 File Offset: 0x000C87D6
		// Note: this type is marked as 'beforefieldinit'.
		static CrossAppDomainSink()
		{
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000CA600 File Offset: 0x000C8800
		[CompilerGenerated]
		private void <AsyncProcessMessage>b__10_0(object data)
		{
			try
			{
				this.SendAsyncMessage(data);
			}
			catch
			{
			}
		}

		// Token: 0x040025E2 RID: 9698
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x040025E3 RID: 9699
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040025E4 RID: 9700
		private int _domainID;

		// Token: 0x020005AF RID: 1455
		private struct ProcessMessageRes
		{
			// Token: 0x040025E5 RID: 9701
			public byte[] arrResponse;

			// Token: 0x040025E6 RID: 9702
			public CADMethodReturnMessage cadMrm;
		}
	}
}
