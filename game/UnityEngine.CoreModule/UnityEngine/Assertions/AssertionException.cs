using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000487 RID: 1159
	public class AssertionException : Exception
	{
		// Token: 0x06002916 RID: 10518 RVA: 0x00043F13 File Offset: 0x00042113
		public AssertionException(string message, string userMessage) : base(message)
		{
			this.m_UserMessage = userMessage;
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x00043F28 File Offset: 0x00042128
		public override string Message
		{
			get
			{
				string text = base.Message;
				bool flag = this.m_UserMessage != null;
				if (flag)
				{
					text = this.m_UserMessage + "\n" + text;
				}
				return text;
			}
		}

		// Token: 0x04000F9D RID: 3997
		private string m_UserMessage;
	}
}
