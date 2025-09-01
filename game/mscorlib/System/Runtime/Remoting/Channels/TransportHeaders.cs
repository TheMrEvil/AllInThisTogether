using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores a collection of headers used in the channel sinks.</summary>
	// Token: 0x020005CA RID: 1482
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" /> class.</summary>
		// Token: 0x060038B8 RID: 14520 RVA: 0x000CA879 File Offset: 0x000C8A79
		public TransportHeaders()
		{
			this.hash_table = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
		}

		/// <summary>Gets or sets a transport header that is associated with the given key.</summary>
		/// <param name="key">The <see cref="T:System.String" /> that the requested header is associated with.</param>
		/// <returns>A transport header that is associated with the given key, or <see langword="null" /> if the key was not found.</returns>
		// Token: 0x17000810 RID: 2064
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				return this.hash_table[key];
			}
			[SecurityCritical]
			set
			{
				this.hash_table[key] = value;
			}
		}

		/// <summary>Returns an enumerator of the stored transport headers.</summary>
		/// <returns>An enumerator of the stored transport headers.</returns>
		// Token: 0x060038BB RID: 14523 RVA: 0x000CA8B3 File Offset: 0x000C8AB3
		[SecurityCritical]
		public IEnumerator GetEnumerator()
		{
			return this.hash_table.GetEnumerator();
		}

		// Token: 0x040025F1 RID: 9713
		private Hashtable hash_table;
	}
}
