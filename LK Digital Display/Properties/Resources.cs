using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace LK_Digital_Display.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
      private static ResourceManager resourceMan;
      private static CultureInfo resourceCulture;

      internal Resources()
      {
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (LK_Digital_Display.Properties.Resources.resourceMan == null)
            LK_Digital_Display.Properties.Resources.resourceMan = new ResourceManager("LK_Digital_Display.Properties.Resources", typeof (LK_Digital_Display.Properties.Resources).Assembly);
          return LK_Digital_Display.Properties.Resources.resourceMan;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => LK_Digital_Display.Properties.Resources.resourceCulture;
        set => LK_Digital_Display.Properties.Resources.resourceCulture = value;
      }

      internal static Icon LK
      {
        get => (Icon) LK_Digital_Display.Properties.Resources.ResourceManager.GetObject(nameof (LK), LK_Digital_Display.Properties.Resources.resourceCulture);
      }
    }
}
