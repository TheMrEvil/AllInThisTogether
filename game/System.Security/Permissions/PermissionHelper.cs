using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x0200000C RID: 12
	internal sealed class PermissionHelper
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002480 File Offset: 0x00000680
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D8 File Offset: 0x000006D8
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), state), "state");
				}
				if (!allowUnrestricted)
				{
					throw new ArgumentException(Locale.GetText("Unrestricted isn't not allowed for identity permissions."), "state");
				}
			}
			return state;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002528 File Offset: 0x00000728
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Attribute("class") == null)
			{
				throw new ArgumentException(Locale.GetText("Missing 'class' attribute."), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D4 File Offset: 0x000007D4
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002606 File Offset: 0x00000806
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002145 File Offset: 0x00000345
		public PermissionHelper()
		{
		}
	}
}
