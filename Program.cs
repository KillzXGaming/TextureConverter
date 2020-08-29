using System;
using Toolbox.Core;
using Toolbox.Core.IO;

namespace TextureConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: TextureConverter file [ext] [output]" );
                return;
            }

            OpenTK.GameWindow window = new OpenTK.GameWindow();
            Runtime.OpenTKInitialized = true;

            var file = STFileLoader.OpenFileFormat(args[0]);
            if (file == null)
            {
                Console.WriteLine("No files specified");
                return;
            }

            string ext = ".png";
            string directory = System.IO.Path.GetDirectoryName(args[0]).Replace("\\", "/") + "/";
            Console.WriteLine("Directory: " + directory);

            if (args.Length > 1) {
                ext = args[1];
            }

            Console.WriteLine($"file {file.FileInfo.FileName} {file.GetType()}");

            if (file is STGenericTexture) {
                ExportTexture((STGenericTexture)file, $"{directory}{((STGenericTexture)file).Name.Split('.')[0]}{ext}");
            }
            if (file is ITextureContainer)
            {
                foreach (var tex in ((ITextureContainer)file).TextureList) {
                    ExportTexture(tex, $"{directory}{tex.Name.Split('.')[0]}{ext}");
                }
            }

            Console.WriteLine("\nConverted the file at " + args[0] + " to " + ext + " format");
        }

        static void ExportTexture(STGenericTexture texture, string fileName) {
            texture.Export(fileName, new TextureExportSettings());
        }
    }
}
