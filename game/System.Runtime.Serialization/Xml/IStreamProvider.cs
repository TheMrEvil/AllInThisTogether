using System;
using System.IO;

namespace System.Xml
{
	/// <summary>Represents an interface that can be implemented by classes providing streams.</summary>
	// Token: 0x0200001D RID: 29
	public interface IStreamProvider
	{
		/// <summary>Gets a stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x0600008F RID: 143
		Stream GetStream();

		/// <summary>Releases a stream to output.</summary>
		/// <param name="stream">The stream being released.</param>
		// Token: 0x06000090 RID: 144
		void ReleaseStream(Stream stream);
	}
}
