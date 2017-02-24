using System;

namespace HtmlParser
{
    public class Link : IEquatable<Link>
    {
        public string Url { get; set; }
        public int Depth { get; set; }

        public bool Equals(Link other)
        {
            if (other == null)
                return false;

            return Url != null && Url.Equals(other.Url) || ReferenceEquals(Url, other.Url);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Link);
        }

        public override int GetHashCode()
        {
            return Url.GetHashCode();
        }
    }
}
