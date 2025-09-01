using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Displays user interface dialogs that allow you to select and view X.509 certificates. This class cannot be inherited.</summary>
	// Token: 0x02000017 RID: 23
	public static class X509Certificate2UI
	{
		/// <summary>Displays a dialog box that contains the properties of an X.509 certificate and its associated certificate chain.</summary>
		/// <param name="certificate">The X.509 certificate to display.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> parameter is invalid.</exception>
		// Token: 0x0600005C RID: 92 RVA: 0x00003063 File Offset: 0x00001263
		[MonoTODO]
		public static void DisplayCertificate(X509Certificate2 certificate)
		{
			X509Certificate2UI.DisplayCertificate(certificate, IntPtr.Zero);
		}

		/// <summary>Displays a dialog box that contains the properties of an X.509 certificate and its associated certificate chain using a handle to a parent window.</summary>
		/// <param name="certificate">The X.509 certificate to display.</param>
		/// <param name="hwndParent">A handle to the parent window to use for the display dialog.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> parameter is invalid.</exception>
		// Token: 0x0600005D RID: 93 RVA: 0x00003070 File Offset: 0x00001270
		[MonoTODO]
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeTopLevelWindows)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void DisplayCertificate(X509Certificate2 certificate, IntPtr hwndParent)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			certificate.GetRawCertData();
			throw new NotImplementedException();
		}

		/// <summary>Displays a dialog box for selecting an X.509 certificate from a certificate collection.</summary>
		/// <param name="certificates">A collection of X.509 certificates to select from.</param>
		/// <param name="title">The title of the dialog box.</param>
		/// <param name="message">A descriptive message to guide the user.  The message is displayed in the dialog box.</param>
		/// <param name="selectionFlag">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SelectionFlag" /> values that specifies whether single or multiple selections are allowed.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that contains the selected certificate or certificates.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="selectionFlag" /> parameter is not a valid flag.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificates" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificates" /> parameter is invalid.</exception>
		// Token: 0x0600005E RID: 94 RVA: 0x0000308C File Offset: 0x0000128C
		[MonoTODO]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag)
		{
			return X509Certificate2UI.SelectFromCollection(certificates, title, message, selectionFlag, IntPtr.Zero);
		}

		/// <summary>Displays a dialog box for selecting an X.509 certificate from a certificate collection using a handle to a parent window.</summary>
		/// <param name="certificates">A collection of X.509 certificates to select from.</param>
		/// <param name="title">The title of the dialog box.</param>
		/// <param name="message">A descriptive message to guide the user.  The message is displayed in the dialog box.</param>
		/// <param name="selectionFlag">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SelectionFlag" /> values that specifies whether single or multiple selections are allowed.</param>
		/// <param name="hwndParent">A handle to the parent window to use for the display dialog box.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that contains the selected certificate or certificates.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="selectionFlag" /> parameter is not a valid flag.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificates" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificates" /> parameter is invalid.</exception>
		// Token: 0x0600005F RID: 95 RVA: 0x0000309C File Offset: 0x0000129C
		[MonoTODO]
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeTopLevelWindows)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag, IntPtr hwndParent)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (selectionFlag < X509SelectionFlag.SingleSelection || selectionFlag > X509SelectionFlag.MultiSelection)
			{
				throw new ArgumentException("selectionFlag");
			}
			throw new NotImplementedException();
		}
	}
}
