using System;
using System.ComponentModel;

namespace System.Xml
{
	/// <summary>The XmlXapResolver type is used to resolve resources in the Silverlight application’s XAP package. </summary>
	// Token: 0x0200024A RID: 586
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlXapResolver : XmlResolver
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlXapResolver" /> class.</summary>
		// Token: 0x060015BF RID: 5567 RVA: 0x000021F2 File Offset: 0x000003F2
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlXapResolver()
		{
		}

		/// <summary>Maps a URI to an object containing the actual resource.</summary>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current version does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink: role and used as an implementation specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current version only returns <see cref="T:System.IO.Stream" /> objects. </param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object. If the stream is not found, an exception is thrown.</returns>
		// Token: 0x060015C0 RID: 5568 RVA: 0x00084E66 File Offset: 0x00083066
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new XmlException("Cannot open '{0}'. The Uri parameter must be a relative path pointing to content inside the Silverlight application's XAP package. If you need to load content from an arbitrary Uri, please see the documentation on Loading XML content using WebClient/HttpWebRequest.", absoluteUri.ToString(), null, null);
		}

		/// <summary>Registers a resource stream resolver for the application.</summary>
		/// <param name="appStreamResolver">An application resource resolver to register.</param>
		// Token: 0x060015C1 RID: 5569 RVA: 0x0000B528 File Offset: 0x00009728
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterApplicationResourceStreamResolver(IApplicationResourceStreamResolver appStreamResolver)
		{
		}
	}
}
