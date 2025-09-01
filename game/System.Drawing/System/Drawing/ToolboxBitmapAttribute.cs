using System;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
	/// <summary>Allows you to specify an icon to represent a control in a container, such as the Microsoft Visual Studio Form Designer.</summary>
	// Token: 0x0200008E RID: 142
	[AttributeUsage(AttributeTargets.Class)]
	public class ToolboxBitmapAttribute : Attribute
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x00002064 File Offset: 0x00000264
		private ToolboxBitmapAttribute()
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object with an image from a specified file.</summary>
		/// <param name="imageFile">The name of a file that contains a 16 by 16 bitmap.</param>
		// Token: 0x060007AB RID: 1963 RVA: 0x00002064 File Offset: 0x00000264
		public ToolboxBitmapAttribute(string imageFile)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 x 16 bitmap that is embedded as a resource in a specified assembly.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource.</param>
		// Token: 0x060007AC RID: 1964 RVA: 0x00016760 File Offset: 0x00014960
		public ToolboxBitmapAttribute(Type t)
		{
			this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(t, null, false);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 by 16 bitmap that is embedded as a resource in a specified assembly.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource.</param>
		/// <param name="name">The name of the embedded bitmap resource.</param>
		// Token: 0x060007AD RID: 1965 RVA: 0x00016776 File Offset: 0x00014976
		public ToolboxBitmapAttribute(Type t, string name)
		{
			this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(t, name, false);
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if <paramref name="value" /> is both a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007AE RID: 1966 RVA: 0x0001678C File Offset: 0x0001498C
		public override bool Equals(object value)
		{
			return value is ToolboxBitmapAttribute && (value == this || ((ToolboxBitmapAttribute)value).smallImage == this.smallImage);
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007AF RID: 1967 RVA: 0x000167B1 File Offset: 0x000149B1
		public override int GetHashCode()
		{
			return this.smallImage.GetHashCode() ^ this.bigImage.GetHashCode();
		}

		/// <summary>Gets the small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <param name="component">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type of the object specified by the component parameter. For example, if you pass an object of type ControlA to the component parameter, then this method searches the assembly that defines ControlA.</param>
		/// <returns>The small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007B0 RID: 1968 RVA: 0x000167CA File Offset: 0x000149CA
		public Image GetImage(object component)
		{
			return this.GetImage(component.GetType(), null, false);
		}

		/// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <param name="component">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type of the object specified by the component parameter. For example, if you pass an object of type ControlA to the component parameter, then this method searches the assembly that defines ControlA.</param>
		/// <param name="large">Specifies whether this method returns a large image (<see langword="true" />) or a small image (<see langword="false" />). The small image is 16 by 16, and the large image is 32 by 32.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> object associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007B1 RID: 1969 RVA: 0x000167DA File Offset: 0x000149DA
		public Image GetImage(object component, bool large)
		{
			return this.GetImage(component.GetType(), null, large);
		}

		/// <summary>Gets the small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type specified by the type parameter. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA.</param>
		/// <returns>The small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007B2 RID: 1970 RVA: 0x000167EA File Offset: 0x000149EA
		public Image GetImage(Type type)
		{
			return this.GetImage(type, null, false);
		}

		/// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type specified by the component type. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA.</param>
		/// <param name="large">Specifies whether this method returns a large image (<see langword="true" />) or a small image (<see langword="false" />). The small image is 16 by 16, and the large image is 32 by 32.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007B3 RID: 1971 RVA: 0x000167F5 File Offset: 0x000149F5
		public Image GetImage(Type type, bool large)
		{
			return this.GetImage(type, null, large);
		}

		/// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
		/// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for an embedded bitmap resource in the assembly that defines the type specified by the component type. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA.</param>
		/// <param name="imgName">The name of the embedded bitmap resource.</param>
		/// <param name="large">Specifies whether this method returns a large image (<see langword="true" />) or a small image (<see langword="false" />). The small image is 16 by 16, and the large image is 32 by 32.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
		// Token: 0x060007B4 RID: 1972 RVA: 0x00016800 File Offset: 0x00014A00
		public Image GetImage(Type type, string imgName, bool large)
		{
			if (this.smallImage == null)
			{
				this.smallImage = ToolboxBitmapAttribute.GetImageFromResource(type, imgName, false);
			}
			if (large)
			{
				if (this.bigImage == null)
				{
					this.bigImage = new Bitmap(this.smallImage, 32, 32);
				}
				return this.bigImage;
			}
			return this.smallImage;
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> object based on a bitmap resource that is embedded in an assembly.</summary>
		/// <param name="t">This method searches for an embedded bitmap resource in the assembly that defines the type specified by the t parameter. For example, if you pass typeof(ControlA) to the t parameter, then this method searches the assembly that defines ControlA.</param>
		/// <param name="imageName">The name of the embedded bitmap resource.</param>
		/// <param name="large">Specifies whether this method returns a large image (true) or a small image (false). The small image is 16 by 16, and the large image is 32 x 32.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> object based on the retrieved bitmap.</returns>
		// Token: 0x060007B5 RID: 1973 RVA: 0x00016850 File Offset: 0x00014A50
		public static Image GetImageFromResource(Type t, string imageName, bool large)
		{
			if (imageName == null)
			{
				imageName = t.Name + ".bmp";
			}
			Image result;
			try
			{
				Bitmap bitmap;
				using (Stream manifestResourceStream = t.GetTypeInfo().Assembly.GetManifestResourceStream(t.Namespace + "." + imageName))
				{
					if (manifestResourceStream == null)
					{
						return null;
					}
					bitmap = new Bitmap(manifestResourceStream, false);
				}
				if (large)
				{
					result = new Bitmap(bitmap, 32, 32);
				}
				else
				{
					result = bitmap;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000168E8 File Offset: 0x00014AE8
		// Note: this type is marked as 'beforefieldinit'.
		static ToolboxBitmapAttribute()
		{
		}

		// Token: 0x04000592 RID: 1426
		private Image smallImage;

		// Token: 0x04000593 RID: 1427
		private Image bigImage;

		/// <summary>A <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object that has its small image and its large image set to <see langword="null" />.</summary>
		// Token: 0x04000594 RID: 1428
		public static readonly ToolboxBitmapAttribute Default = new ToolboxBitmapAttribute();
	}
}
