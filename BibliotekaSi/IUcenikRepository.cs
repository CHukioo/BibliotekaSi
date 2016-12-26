using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    interface IUcenikRepository
    {
        IList<Ucenik> Site();
        Ucenik SelektPoId(int ucenikid);
        Ucenik SelektPoEmail(string email);
        bool VnesiUcenik(Ucenik target);
    }
}
