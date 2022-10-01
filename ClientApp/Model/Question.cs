using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model
{
    public class Question
    {
        public int Level { get; set; }
        public string Content { get; set; }
        public List<Variant > Variants { get; set; }

        public Question(int level, string content, List<Variant> variants)
        {
            Level = level;
            Content = content;
            Variants = variants;
        }
    }
}
