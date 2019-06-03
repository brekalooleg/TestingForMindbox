using AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Path
    {
        List<EdgeInWork> path;

        public Path(List<EdgeInWork> path)
        {
            this.path = path;
        }

        public EdgeInWork this[int id]
        {
            get
            {
                return path[id];
            }
        }

        public List<EdgeInWork> Value
        {
            get
            {
                return path;
            }
        }

        public int Count
        {
            get
            {
                return path.Count();
            }
        }
    }
}
