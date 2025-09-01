using System;
using System.Globalization;
using System.Reflection;

namespace System.Net
{
	// Token: 0x020005EF RID: 1519
	internal class WebRequestPrefixElement
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000A6B54 File Offset: 0x000A4D54
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x000A6BD4 File Offset: 0x000A4DD4
		public IWebRequestCreate Creator
		{
			get
			{
				if (this.creator == null && this.creatorType != null)
				{
					lock (this)
					{
						if (this.creator == null)
						{
							this.creator = (IWebRequestCreate)Activator.CreateInstance(this.creatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
						}
					}
				}
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000A6BE0 File Offset: 0x000A4DE0
		public WebRequestPrefixElement(string P, Type creatorType)
		{
			if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
			{
				throw new InvalidCastException(SR.GetString("Invalid cast from {0} to {1}.", new object[]
				{
					creatorType.AssemblyQualifiedName,
					"IWebRequestCreate"
				}));
			}
			this.Prefix = P;
			this.creatorType = creatorType;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000A6C3A File Offset: 0x000A4E3A
		public WebRequestPrefixElement(string P, IWebRequestCreate C)
		{
			this.Prefix = P;
			this.Creator = C;
		}

		// Token: 0x04001BB6 RID: 7094
		public string Prefix;

		// Token: 0x04001BB7 RID: 7095
		internal IWebRequestCreate creator;

		// Token: 0x04001BB8 RID: 7096
		internal Type creatorType;
	}
}
