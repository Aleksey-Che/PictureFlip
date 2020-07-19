using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PictureFlip
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())))) + "\\testPics";

            var files = Directory.GetFiles(dirName, "*.*").Where(str => str.EndsWith(".jpg") || str.EndsWith(".png") || str.EndsWith(".bmp"));

            var sw = new Stopwatch();

            Parallel.ForEach(files, (currentFile) =>
            {
                sw.Start();

                string filename = Path.GetFileName(currentFile);
                var bitmap = new Bitmap(currentFile);
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                bitmap.Save(Path.Combine(dirName, filename));

                sw.Stop();

                Console.WriteLine($"Файл {filename} принят в обработку потоком №{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Время выполнения операции: {sw.ElapsedMilliseconds} мс");
            });

            Console.WriteLine("Выполнение завершено успешно.");
            Console.ReadKey();
        }
    }
}
