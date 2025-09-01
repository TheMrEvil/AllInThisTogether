using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007FE RID: 2046
	internal class HeaderCollection : NameValueCollection
	{
		// Token: 0x0600414C RID: 16716 RVA: 0x000E1030 File Offset: 0x000DF230
		internal HeaderCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000E1040 File Offset: 0x000DF240
		public override void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType = null;
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition = null;
			}
			base.Remove(name);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000E10CC File Offset: 0x000DF2CC
		public override string Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.Get(name);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000E1164 File Offset: 0x000DF364
		public override string[] GetValues(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.GetValues(name);
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000E11FC File Offset: 0x000DF3FC
		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000E1205 File Offset: 0x000DF405
		internal void InternalSet(string name, string value)
		{
			base.Set(name, value);
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000E120F File Offset: 0x000DF40F
		internal void InternalAdd(string name, string value)
		{
			if (MailHeaderInfo.IsSingleton(name))
			{
				base.Set(name, value);
				return;
			}
			base.Add(name, value);
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000E122C File Offset: 0x000DF42C
		public override void Set(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "value"), "value");
			}
			if (!MimeBasePart.IsAscii(name, false))
			{
				throw new FormatException(SR.Format("An invalid character was found in header name.", Array.Empty<object>()));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			base.Set(name, value);
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000E1340 File Offset: 0x000DF540
		public override void Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "value"), "value");
			}
			MailBnfHelper.ValidateHeaderName(name);
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			this.InternalAdd(name, value);
		}

		// Token: 0x0400279C RID: 10140
		private MimeBasePart _part;
	}
}
