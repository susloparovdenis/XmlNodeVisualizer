using System.Diagnostics;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.VisualStudio.DebuggerVisualizers;
using XmlNodeVisualizer;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("XmlVisualizer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("suit")]
[assembly: AssemblyProduct("XmlVisualizer")]
[assembly: AssemblyCopyright("Copyright © suit 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("60d4a8e3-8e68-4cc6-9b39-247431772947")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: DebuggerVisualizer(typeof(XmlNodeVisualizer.XmlVisualizer), typeof(XmlVisualizerObjectSource), Target = typeof(XmlNode), Description = "XML node visualizer")]
