using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    public interface IKnigaRepository
    {
        IList<Kniga> Site();
        Kniga SelektPoId(int knigaid);
        bool VnesiKniga(Kniga target);
    }
}
