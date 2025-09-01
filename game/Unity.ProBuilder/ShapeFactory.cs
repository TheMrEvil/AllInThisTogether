using System;
using System.Reflection;
using UnityEngine.ProBuilder.Shapes;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000052 RID: 82
	public static class ShapeFactory
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0001CAFD File Offset: 0x0001ACFD
		public static ProBuilderMesh Instantiate<T>(PivotLocation pivotType = PivotLocation.Center) where T : Shape, new()
		{
			return ShapeFactory.Instantiate(typeof(T), PivotLocation.Center);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001CB10 File Offset: 0x0001AD10
		public static ProBuilderMesh Instantiate(Type shapeType, PivotLocation pivotType = PivotLocation.Center)
		{
			if (shapeType == null)
			{
				throw new ArgumentNullException("shapeType", "Cannot instantiate a null shape.");
			}
			if (shapeType.IsAssignableFrom(typeof(Shape)))
			{
				throw new ArgumentException("Type needs to derive from Shape");
			}
			try
			{
				return ShapeFactory.Instantiate(Activator.CreateInstance(shapeType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null, null) as Shape, pivotType);
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Failed creating shape \"{0}\". Shapes must contain an empty constructor.\n{1}", shapeType, arg));
			}
			return null;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001CB98 File Offset: 0x0001AD98
		public static ProBuilderMesh Instantiate(Shape shape, PivotLocation pivotType = PivotLocation.Center)
		{
			if (shape == null)
			{
				throw new ArgumentNullException("shape", "Cannot instantiate a null shape.");
			}
			ProBuilderShape proBuilderShape = new GameObject("Shape").AddComponent<ProBuilderShape>();
			proBuilderShape.SetShape(shape, pivotType);
			ProBuilderMesh mesh = proBuilderShape.mesh;
			mesh.renderer.sharedMaterial = BuiltinMaterials.defaultMaterial;
			ShapeAttribute shapeAttribute = Attribute.GetCustomAttribute(shape.GetType(), typeof(ShapeAttribute)) as ShapeAttribute;
			if (shapeAttribute != null)
			{
				mesh.gameObject.name = shapeAttribute.name;
			}
			return mesh;
		}
	}
}
