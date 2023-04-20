using System.IO;
using UnityEngine;
using UnityEditor;

public static class SvaePNGContext
{
	[MenuItem("CONTEXT/RenderTexture/Save PNG")]
	public static void RenderTextureContext (MenuCommand menuCommand)
	{
		RenderTexture renderTexture = menuCommand.context as RenderTexture;
		RenderTexture.active = renderTexture;

		Vector2Int size = new Vector2Int(renderTexture.width, renderTexture.height);
		Texture2D texture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);
		texture.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);

		RenderTexture.active = null;
		SaveDialog(texture);
	}

	[MenuItem("CONTEXT/Texture2D/Save PNG")]
	public static void Texture2DContext (MenuCommand menuCommand)
	{
		Texture2D texture = menuCommand.context as Texture2D;
		SaveDialog(texture);
	}

	[MenuItem("CONTEXT/Sprite/Save PNG")]
	public static void SpriteContext (MenuCommand menuCommand)
	{
		Sprite sprite = menuCommand.context as Sprite;
		Texture2D texture = sprite.texture;
		SaveDialog(texture);
	}

	private static void SaveDialog (Texture2D texture)
    {
		byte[] png = texture.EncodeToPNG();
		SaveDialog(png);
	}

	private static void SaveDialog (byte[] png)
	{
		string title = "Save PNG";
		string path = EditorUtility.SaveFilePanel(title, "Assets/", "Image", "png");
		if (string.IsNullOrEmpty(path))
			return;
		path = FileUtil.GetProjectRelativePath(path);
		Save(png, path);
	}

	private static void Save (byte[] png, string path)
	{
		File.WriteAllBytes(path, png);
	}
}
