using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.XPath
{
	/// <summary>Provides the exception thrown when an error occurs while processing an XPath expression. </summary>
	// Token: 0x02000251 RID: 593
	[Serializable]
	public class XPathException : SystemException
	{
		/// <summary>Uses the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects to initialize a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains all the properties of an <see cref="T:System.Xml.XPath.XPathException" />. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x060015E0 RID: 5600 RVA: 0x000853D0 File Offset: 0x000835D0
		protected XPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "version")
				{
					text = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XPathException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the <see cref="T:System.Xml.XPath.XPathException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the specified <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060015E1 RID: 5601 RVA: 0x00085481 File Offset: 0x00083681
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		// Token: 0x060015E2 RID: 5602 RVA: 0x000854BD File Offset: 0x000836BD
		public XPathException() : this(string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class with the specified exception message.</summary>
		/// <param name="message">The description of the error condition.</param>
		// Token: 0x060015E3 RID: 5603 RVA: 0x000854CB File Offset: 0x000836CB
		public XPathException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class using the specified exception message and <see cref="T:System.Exception" /> object.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the <see cref="T:System.Xml.XPath.XPathException" />, if any. This value can be <see langword="null" />. </param>
		// Token: 0x060015E4 RID: 5604 RVA: 0x000854D5 File Offset: 0x000836D5
		public XPathException(string message, Exception innerException) : this("{0}", new string[]
		{
			message
		}, innerException)
		{
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000854ED File Offset: 0x000836ED
		internal static XPathException Create(string res)
		{
			return new XPathException(res, null);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000854F6 File Offset: 0x000836F6
		internal static XPathException Create(string res, string arg)
		{
			return new XPathException(res, new string[]
			{
				arg
			});
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00085508 File Offset: 0x00083708
		internal static XPathException Create(string res, string arg, string arg2)
		{
			return new XPathException(res, new string[]
			{
				arg,
				arg2
			});
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0008551E File Offset: 0x0008371E
		internal static XPathException Create(string res, string arg, Exception innerException)
		{
			return new XPathException(res, new string[]
			{
				arg
			}, innerException);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00085531 File Offset: 0x00083731
		private XPathException(string res, string[] args) : this(res, args, null)
		{
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0008553C File Offset: 0x0008373C
		private XPathException(string res, string[] args, Exception inner) : base(XPathException.CreateMessage(res, args), inner)
		{
			base.HResult = -2146231997;
			this.res = res;
			this.args = args;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00085568 File Offset: 0x00083768
		private static string CreateMessage(string res, string[] args)
		{
			string result;
			try
			{
				string text = Res.GetString(res, args);
				if (text == null)
				{
					text = "UNKNOWN(" + res + ")";
				}
				result = text;
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		/// <summary>Gets the description of the error condition for this exception.</summary>
		/// <returns>The <see langword="string" /> description of the error condition for this exception.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x000855BC File Offset: 0x000837BC
		public override string Message
		{
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x040017E8 RID: 6120
		private string res;

		// Token: 0x040017E9 RID: 6121
		private string[] args;

		// Token: 0x040017EA RID: 6122
		private string message;
	}
}
