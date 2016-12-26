using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    class UcenikRepository : IUcenikRepository
    {
        public IList<Ucenik> Site()
        {
            throw new NotImplementedException();
        }

        public Ucenik SelektPoEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Ucenik SelektPoId(int ucenikid)
        {
            throw new NotImplementedException();
        }

        public bool VnesiUcenik(Ucenik target)
        {
            throw new NotImplementedException();
        }
    }
}
