using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    interface IBazaPod
    {
        string VnesPodatoci(string sqlKomanda);
        DataSet Kveri(string sqlKomanda);
    }
}
