using System;

namespace curso
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var gMunicipios = new  GestorMunicipios();
            var ufs = gMunicipios.ObterUFs();
            foreach(GestorMunicipios.UF uf in ufs.Result)
                Console.WriteLine(uf);
        }
    }
}
