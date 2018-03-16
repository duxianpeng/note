using YuewenNote.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace YuewenNote.Tool
{
    public class DocumentUpdator
    {
        private Timer timer = new Timer(200);

        MainViewModel model;
        string content;
        string sourcePath;
        string destPath;
        string markdownType;
        string cssFile;

        public DocumentUpdator()
        {
            timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(sourcePath);
            sw.Write(content);
            sw.Close();

            DocumentExporter.Export("Html Local Mathjax",
                markdownType,
                cssFile,
                sourcePath,
                destPath);

            //Smodel.ShouldReload = !model.ShouldReload;
            timer.Stop();
        }

        public void Update(MainViewModel model, string content, string sourcePath, string destPath, string markdownType, string cssFile)
        {
            timer.Stop();
            this.model = model;
            this.content = content;
            this.sourcePath = sourcePath;
            this.destPath = destPath;
            this.cssFile = cssFile;
            this.markdownType = markdownType;
            timer.Start();
        }
    }
}
