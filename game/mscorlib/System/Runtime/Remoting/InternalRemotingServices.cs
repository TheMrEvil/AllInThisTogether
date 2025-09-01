using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	/// <summary>Defines utility methods for use by the .NET Framework remoting infrastructure.</summary>
	// Token: 0x02000562 RID: 1378
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.InternalRemotingServices" />.</summary>
		// Token: 0x060035FE RID: 13822 RVA: 0x0000259F File Offset: 0x0000079F
		public InternalRemotingServices()
		{
		}

		/// <summary>Sends a message concerning a remoting channel to an unmanaged debugger.</summary>
		/// <param name="s">A string to place in the message.</param>
		// Token: 0x060035FF RID: 13823 RVA: 0x000472C8 File Offset: 0x000454C8
		[Conditional("_LOGGING")]
		public static void DebugOutChnl(string s)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets an appropriate SOAP-related attribute for the specified class member or method parameter.</summary>
		/// <param name="reflectionObject">A class member or method parameter.</param>
		/// <returns>The SOAP-related attribute for the specified class member or method parameter.</returns>
		// Token: 0x06003600 RID: 13824 RVA: 0x000C2534 File Offset: 0x000C0734
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			object syncRoot = InternalRemotingServices._soapAttributes.SyncRoot;
			SoapAttribute result;
			lock (syncRoot)
			{
				SoapAttribute soapAttribute = InternalRemotingServices._soapAttributes[reflectionObject] as SoapAttribute;
				if (soapAttribute != null)
				{
					result = soapAttribute;
				}
				else
				{
					object[] customAttributes = ((ICustomAttributeProvider)reflectionObject).GetCustomAttributes(typeof(SoapAttribute), true);
					if (customAttributes.Length != 0)
					{
						soapAttribute = (SoapAttribute)customAttributes[0];
					}
					else if (reflectionObject is Type)
					{
						soapAttribute = new SoapTypeAttribute();
					}
					else if (reflectionObject is FieldInfo)
					{
						soapAttribute = new SoapFieldAttribute();
					}
					else if (reflectionObject is MethodBase)
					{
						soapAttribute = new SoapMethodAttribute();
					}
					else if (reflectionObject is ParameterInfo)
					{
						soapAttribute = new SoapParameterAttribute();
					}
					soapAttribute.SetReflectionObject(reflectionObject);
					InternalRemotingServices._soapAttributes[reflectionObject] = soapAttribute;
					result = soapAttribute;
				}
			}
			return result;
		}

		/// <summary>Instructs an internal debugger to check for a condition and display a message if the condition is <see langword="false" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to prevent a message from being displayed; otherwise, <see langword="false" />.</param>
		/// <param name="message">The message to display if <paramref name="condition" /> is <see langword="false" />.</param>
		// Token: 0x06003601 RID: 13825 RVA: 0x000472C8 File Offset: 0x000454C8
		[Conditional("_DEBUG")]
		public static void RemotingAssert(bool condition, string message)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sends any number of messages concerning remoting channels to an internal debugger.</summary>
		/// <param name="messages">An array of type <see cref="T:System.Object" /> that contains any number of messages.</param>
		// Token: 0x06003602 RID: 13826 RVA: 0x000472C8 File Offset: 0x000454C8
		[Conditional("_LOGGING")]
		public static void RemotingTrace(params object[] messages)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets internal identifying information for a remoted server object for each method call from client to server.</summary>
		/// <param name="m">A <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> that represents a method call on a remote object.</param>
		/// <param name="srvID">Internal identifying information for a remoted server object.</param>
		// Token: 0x06003603 RID: 13827 RVA: 0x000C260C File Offset: 0x000C080C
		[CLSCompliant(false)]
		public static void SetServerIdentity(MethodCall m, object srvID)
		{
			Identity identity = srvID as Identity;
			if (identity == null)
			{
				throw new ArgumentException("srvID");
			}
			RemotingServices.SetMessageTargetIdentity(m, identity);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000C2635 File Offset: 0x000C0835
		// Note: this type is marked as 'beforefieldinit'.
		static InternalRemotingServices()
		{
		}

		// Token: 0x04002523 RID: 9507
		private static Hashtable _soapAttributes = new Hashtable();
	}
}
