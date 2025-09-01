using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Thrown when SQL Server or the ADO.NET <see cref="N:System.Data.SqlClient" /> provider detects an invalid user-defined type (UDT).</summary>
	// Token: 0x02000062 RID: 98
	[Serializable]
	public sealed class InvalidUdtException : SystemException
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		internal InvalidUdtException()
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00010BE7 File Offset: 0x0000EDE7
		internal InvalidUdtException(string message) : base(message)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00010BFB File Offset: 0x0000EDFB
		internal InvalidUdtException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00010C10 File Offset: 0x0000EE10
		private InvalidUdtException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		/// <summary>Streams all the <see cref="T:Microsoft.SqlServer.Server.InvalidUdtException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060004A1 RID: 1185 RVA: 0x00010C1A File Offset: 0x0000EE1A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00010C24 File Offset: 0x0000EE24
		internal static InvalidUdtException Create(Type udtType, string resourceReason)
		{
			string @string = Res.GetString(resourceReason);
			InvalidUdtException ex = new InvalidUdtException(Res.GetString("'{0}' is an invalid user defined type, reason: {1}.", new object[]
			{
				udtType.FullName,
				@string
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x02000063 RID: 99
		private class HResults
		{
			// Token: 0x060004A3 RID: 1187 RVA: 0x00003D93 File Offset: 0x00001F93
			public HResults()
			{
			}

			// Token: 0x04000614 RID: 1556
			internal const int InvalidUdt = -2146232009;
		}
	}
}
