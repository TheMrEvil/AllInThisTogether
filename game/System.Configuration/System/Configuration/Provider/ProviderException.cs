using System;
using System.Runtime.Serialization;

namespace System.Configuration.Provider
{
	/// <summary>The exception that is thrown when a configuration provider error has occurred. This exception class is also used by providers to throw exceptions when internal errors occur within the provider that do not map to other pre-existing exception classes.</summary>
	// Token: 0x02000079 RID: 121
	[Serializable]
	public class ProviderException : Exception
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B15D File Offset: 0x0000935D
		public ProviderException()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B165 File Offset: 0x00009365
		protected ProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.Provider.ProviderException" /> was thrown.</param>
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B16F File Offset: 0x0000936F
		public ProviderException(string message) : base(message)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.Provider.ProviderException" /> was thrown.</param>
		/// <param name="innerException">The exception that caused this <see cref="T:System.Configuration.Provider.ProviderException" /> to be thrown.</param>
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B178 File Offset: 0x00009378
		public ProviderException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
