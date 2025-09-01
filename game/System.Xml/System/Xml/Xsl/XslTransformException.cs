using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Utils;

namespace System.Xml.Xsl
{
	// Token: 0x0200033F RID: 831
	[Serializable]
	internal class XslTransformException : XsltException
	{
		// Token: 0x06002251 RID: 8785 RVA: 0x000D91A7 File Offset: 0x000D73A7
		protected XslTransformException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000D91B1 File Offset: 0x000D73B1
		public XslTransformException(Exception inner, string res, params string[] args) : base(XslTransformException.CreateMessage(res, args), inner)
		{
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000D91C1 File Offset: 0x000D73C1
		public XslTransformException(string message) : base(XslTransformException.CreateMessage(message, null), null)
		{
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000D91D1 File Offset: 0x000D73D1
		internal XslTransformException(string res, params string[] args) : this(null, res, args)
		{
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000D91DC File Offset: 0x000D73DC
		internal static string CreateMessage(string res, params string[] args)
		{
			string text = null;
			try
			{
				text = Res.GetString(res, args);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (text != null)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(res);
			if (args != null && args.Length != 0)
			{
				stringBuilder.Append('(');
				stringBuilder.Append(args[0]);
				for (int i = 1; i < args.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(args[i]);
				}
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000D9264 File Offset: 0x000D7464
		internal virtual string FormatDetailedMessage()
		{
			return this.Message;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000D926C File Offset: 0x000D746C
		public override string ToString()
		{
			string text = base.GetType().FullName;
			string text2 = this.FormatDetailedMessage();
			if (text2 != null && text2.Length > 0)
			{
				text = text + ": " + text2;
			}
			if (base.InnerException != null)
			{
				text = string.Concat(new string[]
				{
					text,
					" ---> ",
					base.InnerException.ToString(),
					Environment.NewLine,
					"   ",
					XslTransformException.CreateMessage("--- End of inner exception stack trace ---", Array.Empty<string>())
				});
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
