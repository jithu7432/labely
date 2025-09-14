using Labely.Core;
namespace Labely.Konsole;


internal class Program {
    private static void Main(string[] args) {
        var lc = LabelConfig.FromFile("payload.json");
        Drawer.DrawLabel(lc);
    }
}
