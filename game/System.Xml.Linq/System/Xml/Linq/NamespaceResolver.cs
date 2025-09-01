using System;

namespace System.Xml.Linq
{
	// Token: 0x02000046 RID: 70
	internal struct NamespaceResolver
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000C64E File Offset: 0x0000A84E
		public void PushScope()
		{
			this._scope++;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C660 File Offset: 0x0000A860
		public void PopScope()
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = this._declaration;
			if (namespaceDeclaration != null)
			{
				do
				{
					namespaceDeclaration = namespaceDeclaration.prev;
					if (namespaceDeclaration.scope != this._scope)
					{
						break;
					}
					if (namespaceDeclaration == this._declaration)
					{
						this._declaration = null;
					}
					else
					{
						this._declaration.prev = namespaceDeclaration.prev;
					}
					this._rover = null;
				}
				while (namespaceDeclaration != this._declaration && this._declaration != null);
			}
			this._scope--;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public void Add(string prefix, XNamespace ns)
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = new NamespaceResolver.NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = this._scope;
			if (this._declaration == null)
			{
				this._declaration = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = this._declaration.prev;
			}
			this._declaration.prev = namespaceDeclaration;
			this._rover = null;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C73C File Offset: 0x0000A93C
		public void AddFirst(string prefix, XNamespace ns)
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = new NamespaceResolver.NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = this._scope;
			if (this._declaration == null)
			{
				namespaceDeclaration.prev = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = this._declaration.prev;
				this._declaration.prev = namespaceDeclaration;
			}
			this._declaration = namespaceDeclaration;
			this._rover = null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			if (this._rover != null && this._rover.ns == ns && (allowDefaultNamespace || this._rover.prefix.Length > 0))
			{
				return this._rover.prefix;
			}
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = this._declaration;
			if (namespaceDeclaration != null)
			{
				for (;;)
				{
					namespaceDeclaration = namespaceDeclaration.prev;
					if (namespaceDeclaration.ns == ns)
					{
						NamespaceResolver.NamespaceDeclaration prev = this._declaration.prev;
						while (prev != namespaceDeclaration && prev.prefix != namespaceDeclaration.prefix)
						{
							prev = prev.prev;
						}
						if (prev == namespaceDeclaration)
						{
							if (allowDefaultNamespace)
							{
								break;
							}
							if (namespaceDeclaration.prefix.Length > 0)
							{
								goto Block_8;
							}
						}
					}
					if (namespaceDeclaration == this._declaration)
					{
						goto IL_BB;
					}
				}
				this._rover = namespaceDeclaration;
				return namespaceDeclaration.prefix;
				Block_8:
				return namespaceDeclaration.prefix;
			}
			IL_BB:
			return null;
		}

		// Token: 0x04000166 RID: 358
		private int _scope;

		// Token: 0x04000167 RID: 359
		private NamespaceResolver.NamespaceDeclaration _declaration;

		// Token: 0x04000168 RID: 360
		private NamespaceResolver.NamespaceDeclaration _rover;

		// Token: 0x02000047 RID: 71
		private class NamespaceDeclaration
		{
			// Token: 0x06000271 RID: 625 RVA: 0x00003E36 File Offset: 0x00002036
			public NamespaceDeclaration()
			{
			}

			// Token: 0x04000169 RID: 361
			public string prefix;

			// Token: 0x0400016A RID: 362
			public XNamespace ns;

			// Token: 0x0400016B RID: 363
			public int scope;

			// Token: 0x0400016C RID: 364
			public NamespaceResolver.NamespaceDeclaration prev;
		}
	}
}
