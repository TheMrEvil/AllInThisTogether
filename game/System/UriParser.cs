using System;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	/// <summary>Parses a new URI scheme. This is an abstract class.</summary>
	// Token: 0x02000166 RID: 358
	public abstract class UriParser
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0002A33D File Offset: 0x0002853D
		internal string SchemeName
		{
			get
			{
				return this.m_Scheme;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002A345 File Offset: 0x00028545
		internal int DefaultPort
		{
			get
			{
				return this.m_Port;
			}
		}

		/// <summary>Constructs a default URI parser.</summary>
		// Token: 0x06000995 RID: 2453 RVA: 0x0002A34D File Offset: 0x0002854D
		protected UriParser() : this(UriSyntaxFlags.MayHavePath)
		{
		}

		/// <summary>Invoked by a <see cref="T:System.Uri" /> constructor to get a <see cref="T:System.UriParser" /> instance</summary>
		/// <returns>A <see cref="T:System.UriParser" /> for the constructed <see cref="T:System.Uri" />.</returns>
		// Token: 0x06000996 RID: 2454 RVA: 0x000075E1 File Offset: 0x000057E1
		protected virtual UriParser OnNewUri()
		{
			return this;
		}

		/// <summary>Invoked by the Framework when a <see cref="T:System.UriParser" /> method is registered.</summary>
		/// <param name="schemeName">The scheme that is associated with this <see cref="T:System.UriParser" />.</param>
		/// <param name="defaultPort">The port number of the scheme.</param>
		// Token: 0x06000997 RID: 2455 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		/// <summary>Initialize the state of the parser and validate the URI.</summary>
		/// <param name="uri">The T:System.Uri to validate.</param>
		/// <param name="parsingError">Validation errors, if any.</param>
		// Token: 0x06000998 RID: 2456 RVA: 0x0002A357 File Offset: 0x00028557
		protected virtual void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
		{
			parsingError = uri.ParseMinimal();
		}

		/// <summary>Called by <see cref="T:System.Uri" /> constructors and <see cref="Overload:System.Uri.TryCreate" /> to resolve a relative URI.</summary>
		/// <param name="baseUri">A base URI.</param>
		/// <param name="relativeUri">A relative URI.</param>
		/// <param name="parsingError">Errors during the resolve process, if any.</param>
		/// <returns>The string of the resolved relative <see cref="T:System.Uri" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="baseUri" /> parameter is not an absolute <see cref="T:System.Uri" />  
		/// -or-
		///  <paramref name="baseUri" /> parameter requires user-driven parsing.</exception>
		// Token: 0x06000999 RID: 2457 RVA: 0x0002A364 File Offset: 0x00028564
		protected virtual string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError)
		{
			if (baseUri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("This operation is not supported for a relative URI."));
			}
			string result = null;
			bool flag = false;
			Uri uri = Uri.ResolveHelper(baseUri, relativeUri, ref result, ref flag, out parsingError);
			if (parsingError != null)
			{
				return null;
			}
			if (uri != null)
			{
				return uri.OriginalString;
			}
			return result;
		}

		/// <summary>Determines whether <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />.</summary>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="relativeUri">The URI to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600099A RID: 2458 RVA: 0x0002A3DD File Offset: 0x000285DD
		protected virtual bool IsBaseOf(Uri baseUri, Uri relativeUri)
		{
			return baseUri.IsBaseOfHelper(relativeUri);
		}

		/// <summary>Gets the components from a URI.</summary>
		/// <param name="uri">The URI to parse.</param>
		/// <param name="components">The <see cref="T:System.UriComponents" /> to retrieve from <paramref name="uri" />.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped.</param>
		/// <returns>A string that contains the components.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriFormat" /> is invalid.  
		/// -or-
		///  <paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="uri" /> requires user-driven parsing  
		/// -or-
		///  <paramref name="uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		// Token: 0x0600099B RID: 2459 RVA: 0x0002A3E8 File Offset: 0x000285E8
		protected virtual string GetComponents(Uri uri, UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, SR.GetString("UriComponents.SerializationInfoString must not be combined with other UriComponents."));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (uri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("This operation is not supported for a relative URI."));
			}
			return uri.GetComponentsHelper(components, format);
		}

		/// <summary>Indicates whether a URI is well-formed.</summary>
		/// <param name="uri">The URI to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="uri" /> is well-formed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600099C RID: 2460 RVA: 0x0002A47E File Offset: 0x0002867E
		protected virtual bool IsWellFormedOriginalString(Uri uri)
		{
			return uri.InternalIsWellFormedOriginalString();
		}

		/// <summary>Associates a scheme and port number with a <see cref="T:System.UriParser" />.</summary>
		/// <param name="uriParser">The URI parser to register.</param>
		/// <param name="schemeName">The name of the scheme that is associated with this parser.</param>
		/// <param name="defaultPort">The default port number for the specified scheme.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriParser" /> parameter is null  
		/// -or-
		///  <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="schemeName" /> parameter is not valid  
		/// -or-
		///  <paramref name="defaultPort" /> parameter is not valid. The <paramref name="defaultPort" /> parameter is less than -1 or greater than 65,534.</exception>
		// Token: 0x0600099D RID: 2461 RVA: 0x0002A488 File Offset: 0x00028688
		public static void Register(UriParser uriParser, string schemeName, int defaultPort)
		{
			if (uriParser == null)
			{
				throw new ArgumentNullException("uriParser");
			}
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (schemeName.Length == 1)
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if ((defaultPort >= 65535 || defaultPort < 0) && defaultPort != -1)
			{
				throw new ArgumentOutOfRangeException("defaultPort");
			}
			schemeName = schemeName.ToLower(CultureInfo.InvariantCulture);
			UriParser.FetchSyntax(uriParser, schemeName, defaultPort);
		}

		/// <summary>Indicates whether the parser for a scheme is registered.</summary>
		/// <param name="schemeName">The scheme name to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="schemeName" /> has been registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="schemeName" /> parameter is not valid.</exception>
		// Token: 0x0600099E RID: 2462 RVA: 0x0002A508 File Offset: 0x00028708
		public static bool IsKnownScheme(string schemeName)
		{
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			UriParser syntax = UriParser.GetSyntax(schemeName.ToLower(CultureInfo.InvariantCulture));
			return syntax != null && syntax.NotAny(UriSyntaxFlags.V1_UnknownUri);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002A557 File Offset: 0x00028757
		internal static bool ShouldUseLegacyV2Quirks
		{
			get
			{
				return UriParser.s_QuirksVersion <= UriParser.UriQuirksVersion.V2;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002A564 File Offset: 0x00028764
		static UriParser()
		{
			UriParser.m_Table = new Dictionary<string, UriParser>(25);
			UriParser.m_TempTable = new Dictionary<string, UriParser>(25);
			UriParser.HttpUri = new UriParser.BuiltInUriParser("http", 80, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.HttpUri.SchemeName] = UriParser.HttpUri;
			UriParser.HttpsUri = new UriParser.BuiltInUriParser("https", 443, UriParser.HttpUri.m_Flags);
			UriParser.m_Table[UriParser.HttpsUri.SchemeName] = UriParser.HttpsUri;
			UriParser.WsUri = new UriParser.BuiltInUriParser("ws", 80, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WsUri.SchemeName] = UriParser.WsUri;
			UriParser.WssUri = new UriParser.BuiltInUriParser("wss", 443, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WssUri.SchemeName] = UriParser.WssUri;
			UriParser.FtpUri = new UriParser.BuiltInUriParser("ftp", 21, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.FtpUri.SchemeName] = UriParser.FtpUri;
			UriParser.FileUri = new UriParser.BuiltInUriParser("file", -1, UriParser.FileSyntaxFlags);
			UriParser.m_Table[UriParser.FileUri.SchemeName] = UriParser.FileUri;
			UriParser.GopherUri = new UriParser.BuiltInUriParser("gopher", 70, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.GopherUri.SchemeName] = UriParser.GopherUri;
			UriParser.NntpUri = new UriParser.BuiltInUriParser("nntp", 119, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NntpUri.SchemeName] = UriParser.NntpUri;
			UriParser.NewsUri = new UriParser.BuiltInUriParser("news", -1, UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NewsUri.SchemeName] = UriParser.NewsUri;
			UriParser.MailToUri = new UriParser.BuiltInUriParser("mailto", 25, UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.MailToUri.SchemeName] = UriParser.MailToUri;
			UriParser.UuidUri = new UriParser.BuiltInUriParser("uuid", -1, UriParser.NewsUri.m_Flags);
			UriParser.m_Table[UriParser.UuidUri.SchemeName] = UriParser.UuidUri;
			UriParser.TelnetUri = new UriParser.BuiltInUriParser("telnet", 23, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.TelnetUri.SchemeName] = UriParser.TelnetUri;
			UriParser.LdapUri = new UriParser.BuiltInUriParser("ldap", 389, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.LdapUri.SchemeName] = UriParser.LdapUri;
			UriParser.NetTcpUri = new UriParser.BuiltInUriParser("net.tcp", 808, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetTcpUri.SchemeName] = UriParser.NetTcpUri;
			UriParser.NetPipeUri = new UriParser.BuiltInUriParser("net.pipe", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetPipeUri.SchemeName] = UriParser.NetPipeUri;
			UriParser.VsMacrosUri = new UriParser.BuiltInUriParser("vsmacros", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.VsMacrosUri.SchemeName] = UriParser.VsMacrosUri;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002A903 File Offset: 0x00028B03
		internal UriSyntaxFlags Flags
		{
			get
			{
				return this.m_Flags;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002A90B File Offset: 0x00028B0B
		internal bool NotAny(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002A915 File Offset: 0x00028B15
		internal bool InFact(UriSyntaxFlags flags)
		{
			return !this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002A922 File Offset: 0x00028B22
		internal bool IsAllSet(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, flags);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002A92C File Offset: 0x00028B2C
		private bool IsFullMatch(UriSyntaxFlags flags, UriSyntaxFlags expected)
		{
			UriSyntaxFlags uriSyntaxFlags;
			if ((flags & UriSyntaxFlags.UnEscapeDotsAndSlashes) == UriSyntaxFlags.None || !this.m_UpdatableFlagsUsed)
			{
				uriSyntaxFlags = this.m_Flags;
			}
			else
			{
				uriSyntaxFlags = ((this.m_Flags & ~UriSyntaxFlags.UnEscapeDotsAndSlashes) | this.m_UpdatableFlags);
			}
			return (uriSyntaxFlags & flags) == expected;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002A971 File Offset: 0x00028B71
		internal UriParser(UriSyntaxFlags flags)
		{
			this.m_Flags = flags;
			this.m_Scheme = string.Empty;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002A98C File Offset: 0x00028B8C
		private static void FetchSyntax(UriParser syntax, string lwrCaseSchemeName, int defaultPort)
		{
			if (syntax.SchemeName.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("The URI parser instance passed into 'uriParser' parameter is already registered with the scheme name '{0}'.", new object[]
				{
					syntax.SchemeName
				}));
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			lock (table)
			{
				syntax.m_Flags &= ~UriSyntaxFlags.V1_UnknownUri;
				UriParser uriParser = null;
				UriParser.m_Table.TryGetValue(lwrCaseSchemeName, out uriParser);
				if (uriParser != null)
				{
					throw new InvalidOperationException(SR.GetString("A URI scheme name '{0}' already has a registered custom parser.", new object[]
					{
						uriParser.SchemeName
					}));
				}
				UriParser.m_TempTable.TryGetValue(syntax.SchemeName, out uriParser);
				if (uriParser != null)
				{
					lwrCaseSchemeName = uriParser.m_Scheme;
					UriParser.m_TempTable.Remove(lwrCaseSchemeName);
				}
				syntax.OnRegister(lwrCaseSchemeName, defaultPort);
				syntax.m_Scheme = lwrCaseSchemeName;
				syntax.CheckSetIsSimpleFlag();
				syntax.m_Port = defaultPort;
				UriParser.m_Table[syntax.SchemeName] = syntax;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002AA8C File Offset: 0x00028C8C
		internal static UriParser FindOrFetchAsUnknownV1Syntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			UriParser result;
			lock (table)
			{
				if (UriParser.m_TempTable.Count >= 512)
				{
					UriParser.m_TempTable = new Dictionary<string, UriParser>(25);
				}
				uriParser = new UriParser.BuiltInUriParser(lwrCaseScheme, -1, UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
				UriParser.m_TempTable[lwrCaseScheme] = uriParser;
				result = uriParser;
			}
			return result;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002AB28 File Offset: 0x00028D28
		internal static UriParser GetSyntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser == null)
			{
				UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			}
			return uriParser;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002AB57 File Offset: 0x00028D57
		internal bool IsSimple
		{
			get
			{
				return this.InFact(UriSyntaxFlags.SimpleUserSyntax);
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002AB64 File Offset: 0x00028D64
		internal void CheckSetIsSimpleFlag()
		{
			Type type = base.GetType();
			if (type == typeof(GenericUriParser) || type == typeof(HttpStyleUriParser) || type == typeof(FtpStyleUriParser) || type == typeof(FileStyleUriParser) || type == typeof(NewsStyleUriParser) || type == typeof(GopherStyleUriParser) || type == typeof(NetPipeStyleUriParser) || type == typeof(NetTcpStyleUriParser) || type == typeof(LdapStyleUriParser))
			{
				this.m_Flags |= UriSyntaxFlags.SimpleUserSyntax;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002AC2F File Offset: 0x00028E2F
		internal void SetUpdatableFlags(UriSyntaxFlags flags)
		{
			this.m_UpdatableFlags = flags;
			this.m_UpdatableFlagsUsed = true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002AC44 File Offset: 0x00028E44
		internal UriParser InternalOnNewUri()
		{
			UriParser uriParser = this.OnNewUri();
			if (this != uriParser)
			{
				uriParser.m_Scheme = this.m_Scheme;
				uriParser.m_Port = this.m_Port;
				uriParser.m_Flags = this.m_Flags;
			}
			return uriParser;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002AC81 File Offset: 0x00028E81
		internal void InternalValidate(Uri thisUri, out UriFormatException parsingError)
		{
			this.InitializeAndValidate(thisUri, out parsingError);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002AC8B File Offset: 0x00028E8B
		internal string InternalResolve(Uri thisBaseUri, Uri uriLink, out UriFormatException parsingError)
		{
			return this.Resolve(thisBaseUri, uriLink, out parsingError);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002AC96 File Offset: 0x00028E96
		internal bool InternalIsBaseOf(Uri thisBaseUri, Uri uriLink)
		{
			return this.IsBaseOf(thisBaseUri, uriLink);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002ACA0 File Offset: 0x00028EA0
		internal string InternalGetComponents(Uri thisUri, UriComponents uriComponents, UriFormat uriFormat)
		{
			return this.GetComponents(thisUri, uriComponents, uriFormat);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002ACAB File Offset: 0x00028EAB
		internal bool InternalIsWellFormedOriginalString(Uri thisUri)
		{
			return this.IsWellFormedOriginalString(thisUri);
		}

		// Token: 0x04000651 RID: 1617
		private const UriSyntaxFlags SchemeOnlyFlags = UriSyntaxFlags.MayHavePath;

		// Token: 0x04000652 RID: 1618
		private static readonly Dictionary<string, UriParser> m_Table;

		// Token: 0x04000653 RID: 1619
		private static Dictionary<string, UriParser> m_TempTable;

		// Token: 0x04000654 RID: 1620
		private UriSyntaxFlags m_Flags;

		// Token: 0x04000655 RID: 1621
		private volatile UriSyntaxFlags m_UpdatableFlags;

		// Token: 0x04000656 RID: 1622
		private volatile bool m_UpdatableFlagsUsed;

		// Token: 0x04000657 RID: 1623
		private const UriSyntaxFlags c_UpdatableFlags = UriSyntaxFlags.UnEscapeDotsAndSlashes;

		// Token: 0x04000658 RID: 1624
		private int m_Port;

		// Token: 0x04000659 RID: 1625
		private string m_Scheme;

		// Token: 0x0400065A RID: 1626
		internal const int NoDefaultPort = -1;

		// Token: 0x0400065B RID: 1627
		private const int c_InitialTableSize = 25;

		// Token: 0x0400065C RID: 1628
		internal static UriParser HttpUri;

		// Token: 0x0400065D RID: 1629
		internal static UriParser HttpsUri;

		// Token: 0x0400065E RID: 1630
		internal static UriParser WsUri;

		// Token: 0x0400065F RID: 1631
		internal static UriParser WssUri;

		// Token: 0x04000660 RID: 1632
		internal static UriParser FtpUri;

		// Token: 0x04000661 RID: 1633
		internal static UriParser FileUri;

		// Token: 0x04000662 RID: 1634
		internal static UriParser GopherUri;

		// Token: 0x04000663 RID: 1635
		internal static UriParser NntpUri;

		// Token: 0x04000664 RID: 1636
		internal static UriParser NewsUri;

		// Token: 0x04000665 RID: 1637
		internal static UriParser MailToUri;

		// Token: 0x04000666 RID: 1638
		internal static UriParser UuidUri;

		// Token: 0x04000667 RID: 1639
		internal static UriParser TelnetUri;

		// Token: 0x04000668 RID: 1640
		internal static UriParser LdapUri;

		// Token: 0x04000669 RID: 1641
		internal static UriParser NetTcpUri;

		// Token: 0x0400066A RID: 1642
		internal static UriParser NetPipeUri;

		// Token: 0x0400066B RID: 1643
		internal static UriParser VsMacrosUri;

		// Token: 0x0400066C RID: 1644
		private static readonly UriParser.UriQuirksVersion s_QuirksVersion = UriParser.UriQuirksVersion.V3;

		// Token: 0x0400066D RID: 1645
		private const int c_MaxCapacity = 512;

		// Token: 0x0400066E RID: 1646
		private const UriSyntaxFlags UnknownV1SyntaxFlags = UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400066F RID: 1647
		private static readonly UriSyntaxFlags HttpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.UnEscapeDotsAndSlashes : UriSyntaxFlags.None) | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000670 RID: 1648
		private const UriSyntaxFlags FtpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000671 RID: 1649
		private static readonly UriSyntaxFlags FileSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.None : UriSyntaxFlags.MayHaveQuery) | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000672 RID: 1650
		private const UriSyntaxFlags VsmacrosSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000673 RID: 1651
		private const UriSyntaxFlags GopherSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000674 RID: 1652
		private const UriSyntaxFlags NewsSyntaxFlags = UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000675 RID: 1653
		private const UriSyntaxFlags NntpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000676 RID: 1654
		private const UriSyntaxFlags TelnetSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000677 RID: 1655
		private const UriSyntaxFlags LdapSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000678 RID: 1656
		private const UriSyntaxFlags MailtoSyntaxFlags = UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000679 RID: 1657
		private const UriSyntaxFlags NetPipeSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400067A RID: 1658
		private const UriSyntaxFlags NetTcpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x02000167 RID: 359
		private enum UriQuirksVersion
		{
			// Token: 0x0400067C RID: 1660
			V2 = 2,
			// Token: 0x0400067D RID: 1661
			V3
		}

		// Token: 0x02000168 RID: 360
		private class BuiltInUriParser : UriParser
		{
			// Token: 0x060009B3 RID: 2483 RVA: 0x0002ACB4 File Offset: 0x00028EB4
			internal BuiltInUriParser(string lwrCaseScheme, int defaultPort, UriSyntaxFlags syntaxFlags) : base(syntaxFlags | UriSyntaxFlags.SimpleUserSyntax | UriSyntaxFlags.BuiltInSyntax)
			{
				this.m_Scheme = lwrCaseScheme;
				this.m_Port = defaultPort;
			}
		}
	}
}
