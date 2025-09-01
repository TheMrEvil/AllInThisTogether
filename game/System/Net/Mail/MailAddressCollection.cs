using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Store email addresses that are associated with an email message.</summary>
	// Token: 0x0200081E RID: 2078
	public class MailAddressCollection : Collection<MailAddress>
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailAddressCollection" /> class.</summary>
		// Token: 0x06004228 RID: 16936 RVA: 0x000E4A20 File Offset: 0x000E2C20
		public MailAddressCollection()
		{
		}

		/// <summary>Add a list of email addresses to the collection.</summary>
		/// <param name="addresses">The email addresses to add to the <see cref="T:System.Net.Mail.MailAddressCollection" />. Multiple email addresses must be separated with a comma character (",").</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="addresses" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="addresses" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="addresses" /> parameter contains an email address that is invalid or not supported.</exception>
		// Token: 0x06004229 RID: 16937 RVA: 0x000E4A28 File Offset: 0x000E2C28
		public void Add(string addresses)
		{
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "addresses"), "addresses");
			}
			this.ParseValue(addresses);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The index of the email address element to be replaced.</param>
		/// <param name="item">An email address that will replace the element in the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
		// Token: 0x0600422A RID: 16938 RVA: 0x000E4A66 File Offset: 0x000E2C66
		protected override void SetItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		/// <summary>Inserts an email address into the <see cref="T:System.Net.Mail.MailAddressCollection" />, at the specified location.</summary>
		/// <param name="index">The location at which to insert the email address that is specified by <paramref name="item" />.</param>
		/// <param name="item">The email address to be inserted into the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
		// Token: 0x0600422B RID: 16939 RVA: 0x000E4A7E File Offset: 0x000E2C7E
		protected override void InsertItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000E4A98 File Offset: 0x000E2C98
		internal void ParseValue(string addresses)
		{
			IList<MailAddress> list = MailAddressParser.ParseMultipleAddresses(addresses);
			for (int i = 0; i < list.Count; i++)
			{
				base.Add(list[i]);
			}
		}

		/// <summary>Returns a string representation of the email addresses in this <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the email addresses in this collection.</returns>
		// Token: 0x0600422D RID: 16941 RVA: 0x000E4ACC File Offset: 0x000E2CCC
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailAddress mailAddress in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mailAddress.ToString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000E4B3C File Offset: 0x000E2D3C
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			foreach (MailAddress mailAddress in this)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = mailAddress.Encode(charsConsumed, allowUnicode);
				}
				else
				{
					text = text + ", " + mailAddress.Encode(1, allowUnicode);
				}
			}
			return text;
		}
	}
}
