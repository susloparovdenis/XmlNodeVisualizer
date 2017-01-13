using System;
using System.Diagnostics;
using System.IO;
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
            using (var xw = new XmlTextWriter(outgoingData,Encoding.UTF8))
            {
                xw.Formatting = Formatting.Indented;
                xw.Indentation = 2; //default is 1. I used 2 to make the indents larger.

                xmlNode.WriteTo(xw);
            }
        }
    }


    public class XmlVisualizer : DialogDebuggerVisualizer
    {



        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
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
                MessageBox.Show(e.ToString(), "XML node Visualizer error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost myHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DialogDebuggerVisualizer));
            myHost.ShowVisualizer();
        }

        public static void OpenInEditor(string filePath)
        {
            var editors = new[]
            {
                @"Sublime Text 3\sublime_text.exe",
                @"Notepad++\notepad++.exe",
            };
            foreach (var editor in editors)
            {
                var path = Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), editor);
                if(!File.Exists(path))
                    path = Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), editor);
                if (!File.Exists(path))
                    continue;

                var proc = new Process();
                proc.StartInfo.FileName = path;
                proc.StartInfo.Arguments = filePath;
                proc.Start();
                return;
            }
            MessageBox.Show("Editor not found", "XML node Visualizer error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string ToIndentedString(XmlNode xmlNode)
        {
            
            var doc = new XmlDocument();
            using (var sw = new StringWriter())
            {
                using (var xw = new XmlTextWriter(sw))
                {
                    xw.Formatting = Formatting.Indented;
                    xw.Indentation = 2; //default is 1. I used 2 to make the indents larger.

                    xmlNode.WriteTo(xw);
                }
                return sw.ToString(); //The node, as a string, with indents!
            }
        }
    }
}
