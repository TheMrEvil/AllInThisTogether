using System;
using System.Text.RegularExpressions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a verb that can be invoked from a designer.</summary>
	// Token: 0x0200044B RID: 1099
	public class DesignerVerb : MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user.</param>
		/// <param name="handler">The event handler that performs the actions of the verb.</param>
		// Token: 0x060023C1 RID: 9153 RVA: 0x000812D3 File Offset: 0x0007F4D3
		public DesignerVerb(string text, EventHandler handler) : base(handler, StandardCommands.VerbFirst)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user.</param>
		/// <param name="handler">The event handler that performs the actions of the verb.</param>
		/// <param name="startCommandID">The starting command ID for this verb. By default, the designer architecture sets aside a range of command IDs for verbs. You can override this by providing a custom command ID.</param>
		// Token: 0x060023C2 RID: 9154 RVA: 0x00081307 File Offset: 0x0007F507
		public DesignerVerb(string text, EventHandler handler, CommandID startCommandID) : base(handler, startCommandID)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Gets or sets the description of the menu item for the verb.</summary>
		/// <returns>A string describing the menu item.</returns>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x00081338 File Offset: 0x0007F538
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x00081365 File Offset: 0x0007F565
		public string Description
		{
			get
			{
				object obj = this.Properties["Description"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.Properties["Description"] = value;
			}
		}

		/// <summary>Gets the text description for the verb command on the menu.</summary>
		/// <returns>A description for the verb command.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00081378 File Offset: 0x0007F578
		public string Text
		{
			get
			{
				object obj = this.Properties["Text"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
		}

		/// <summary>Overrides <see cref="M:System.Object.ToString" />.</summary>
		/// <returns>The verb's text, or an empty string ("") if the text field is empty.</returns>
		// Token: 0x060023C6 RID: 9158 RVA: 0x000813A5 File Offset: 0x0007F5A5
		public override string ToString()
		{
			return this.Text + " : " + base.ToString();
		}
	}
}
