using System.Collections.Generic;

namespace TownRally
{
    public struct Rally
    {
        public enum DescriptionType
        {
            None = 0,
            Text = 1,
            Image = 2,
        }

        public struct Description
        {
            public DescriptionType Type;
            public string Data;
        }

        public string Name { get; set; }
        public string Caption { get; set; }
        public Description[] Descr { get; set; }
        public KeyValuePair<float, float> Pos { get; set; }
    }
}
