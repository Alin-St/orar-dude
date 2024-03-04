using HtmlAgilityPack;

namespace OrarDude.Utils
{
    static class Extensions
    {
        public static IEnumerable<HtmlNode> Elements(this HtmlNode node)
            => node.ChildNodes.Where(n => n.NodeType == HtmlNodeType.Element);
    }
}
