using System.Text.Json;
using Labely.Core;
namespace Labely.Konsole;


internal class Program {
    private static void Main(string[] args) {
        using var fs = File.OpenRead("payload.json");
        var lc = JsonSerializer.Deserialize<LabelConfig>(fs);
        Drawer.DrawLabel(lc);
    }
}
