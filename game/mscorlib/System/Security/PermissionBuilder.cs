using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020003E2 RID: 994
	internal static class PermissionBuilder
	{
		// Token: 0x060028C5 RID: 10437 RVA: 0x00093580 File Offset: 0x00091780
		public static IPermission Create(string fullname, PermissionState state)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", fullname);
			securityElement.AddAttribute("version", "1");
			if (state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return PermissionBuilder.CreatePermission(fullname, securityElement);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000935E0 File Offset: 0x000917E0
		public static IPermission Create(SecurityElement se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			string text = se.Attribute("class");
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException("class");
			}
			return PermissionBuilder.CreatePermission(text, se);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x00093624 File Offset: 0x00091824
		public static IPermission Create(string fullname, SecurityElement se)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			return PermissionBuilder.CreatePermission(fullname, se);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x00093649 File Offset: 0x00091849
		public static IPermission Create(Type type)
		{
			return (IPermission)Activator.CreateInstance(type, PermissionBuilder.psNone);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0009365B File Offset: 0x0009185B
		internal static IPermission CreatePermission(string fullname, SecurityElement se)
		{
			Type type = Type.GetType(fullname);
			if (type == null)
			{
				throw new TypeLoadException(string.Format(Locale.GetText("Can't create an instance of permission class {0}."), fullname));
			}
			IPermission permission = PermissionBuilder.Create(type);
			permission.FromXml(se);
			return permission;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0009368E File Offset: 0x0009188E
		// Note: this type is marked as 'beforefieldinit'.
		static PermissionBuilder()
		{
		}

		// Token: 0x04001EC1 RID: 7873
		private static object[] psNone = new object[]
		{
			PermissionState.None
		};
	}
}
