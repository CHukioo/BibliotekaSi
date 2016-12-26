using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    class UcenikRepository
    {
        public IList<Ucenik> FindAll()
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually fetches all the Products from a database and creates a list
            throw new System.NotImplementedException();
        }
        public Ucenik  SelektPoId(int ucenikid)
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually fetches a Product from a database, using the supplied parameter
            throw new System.NotImplementedException();
        }
        public Ucenik SelektPoEmail(string email)
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually fetches a Product from a database, using the supplied parameter
            throw new System.NotImplementedException();
        }
        public bool VnesiUcenik(Ucenik target)
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually saves a Product to a database (insert or update), using the supplied parameter
            throw new System.NotImplementedException();
        }
    }
}
