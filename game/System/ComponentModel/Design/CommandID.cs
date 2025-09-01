using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x0200043F RID: 1087
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to.</param>
		/// <param name="commandID">The numeric identifier of this menu command.</param>
		// Token: 0x06002387 RID: 9095 RVA: 0x00081028 File Offset: 0x0007F228
		public CommandID(Guid menuGroup, int commandID)
		{
			this.Guid = menuGroup;
			this.ID = commandID;
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0008103E File Offset: 0x0007F23E
		public virtual int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equivalent to this one; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002389 RID: 9097 RVA: 0x00081048 File Offset: 0x0007F248
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.Guid.Equals(this.Guid) && commandID.ID == this.ID;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600238A RID: 9098 RVA: 0x0008108C File Offset: 0x0007F28C
		public override int GetHashCode()
		{
			return this.Guid.GetHashCode() << 2 | this.ID;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000810B6 File Offset: 0x0007F2B6
		public virtual Guid Guid
		{
			[CompilerGenerated]
			get
			{
				return this.<Guid>k__BackingField;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier.</returns>
		// Token: 0x0600238C RID: 9100 RVA: 0x000810C0 File Offset: 0x0007F2C0
		public override string ToString()
		{
			return this.Guid.ToString() + " : " + this.ID.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x040010AE RID: 4270
		[CompilerGenerated]
		private readonly int <ID>k__BackingField;

		// Token: 0x040010AF RID: 4271
		[CompilerGenerated]
		private readonly Guid <Guid>k__BackingField;
	}
}
