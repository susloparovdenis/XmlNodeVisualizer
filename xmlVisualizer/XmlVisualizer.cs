using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace XmlNodeVisualizer
{
    public class XmlVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            var xmlNode = (XmlNode) target;
            var xw = new XmlTextWriter(outgoingData, Encoding.UTF8)
            {
                Formatting = Formatting.Indented,
                Indentation = 2
            };
            xmlNode.WriteTo(xw);
            xw.Flush();
        }
    }


    public class XmlVisualizer : DialogDebuggerVisualizer
    {
        private static readonly string[] Editors =
        {
            @"Sublime Text 3\sublime_text.exe",
            @"Notepad++\notepad++.exe"
        };

        protected override void Show(IDialogVisualizerService windowService,
                                     IVisualizerObjectProvider objectProvider)
        {
            var text = new StreamReader(objectProvider.GetData()).ReadToEnd();

            try
            {
                var tmpFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xml");
                File.WriteAllText(tmpFilePath, text);
                OpenInEditor(tmpFilePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(),
                    "XML node Visualizer error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            var myHost = new VisualizerDevelopmentHost(objectToVisualize,
                typeof(XmlVisualizer),
                typeof(XmlVisualizerObjectSource));
            myHost.ShowVisualizer();
        }

        private static IEnumerable<string> AppToPaths(string relativePath)
        {
            var programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            yield return Path.Combine(programFiles, relativePath);
            var programFilesX86 = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");
            yield return Path.Combine(programFilesX86, relativePath);
        }

        private static void OpenInEditor(string filePath)
        {
            var path = Editors.SelectMany(AppToPaths).FirstOrDefault(File.Exists);
            if (path == null)
            {
                MessageBox.Show("Editor not found",
                    "XML node Visualizer error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var proc = new Process();
            proc.StartInfo.FileName = path;
            proc.StartInfo.Arguments = filePath;
            proc.Start();
        }
    }
}