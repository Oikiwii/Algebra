using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp20
{

    public class Subject
    {
        public string title;
        public string content;
        public string img;
        public int id;

        public Subject(string title, string content, int id, string img)
        {
            this.title = title;
            this.content = content;
            this.id = id;
            this.img = img;
        }
    }
}