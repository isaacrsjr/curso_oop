using System;
using System.Linq;

namespace curso
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var gMunicipios = new  GestorMunicipios();
            var ufs = gMunicipios.ObterUFs().Result;
            foreach(GestorMunicipios.UF uf in ufs)
                Console.WriteLine(uf);

            Console.WriteLine("########## MUNICÍPIOS ##########");
            var ms = gMunicipios.ObterMunicipios(ufs.Where(ufs => ufs.sigla == "RS").First());
            foreach(GestorMunicipios.Municipio m in ms.Result)
                Console.WriteLine(m);

        }
    }
}
