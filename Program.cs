using System;
using Toolbox.Core;
using Toolbox.Core.IO;

namespace TextureConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string ext = ".png";
            string directory = "";

            OpenTK.GameWindow window = new OpenTK.GameWindow();
            Runtime.OpenTKInitialized = true;

            var file = STFileLoader.OpenFileFormat(args[0]);
            if (file == null) return;

            Console.WriteLine($"file {file.FileInfo.FileName} {file.GetType()}");

            if (file is STGenericTexture) {
                ExportTexture((STGenericTexture)file, $"{directory}{((STGenericTexture)file).Name}{ext}");
            }
            if (file is ITextureContainer)
            {
                foreach (var tex in ((ITextureContainer)file).TextureList) {
                    ExportTexture(tex, $"{directory}{tex.Name}{ext}");
                }
            }
        }

        static void ExportTexture(STGenericTexture texture, string fileName) {
            texture.Export(fileName, new TextureExportSettings());
        }
    }
}
