﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    class PrvTest
    {
        public int Soberi (int a, int b)
        {
            return a + b;
        }

        public bool VnesUcenikProverka(string ime, string prezime, string klas, string br, string email, string profesor)
        {
            if (profesor == "0") { 
                if (ime != "" && prezime!="" && klas != "" && br != "" && email != "" && profesor != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (ime != "" && prezime != "" && email != "" && profesor != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
