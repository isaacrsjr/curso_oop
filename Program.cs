using System;
using System.Linq;

namespace curso
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var gMunicipios = new GestorMunicipios();
            var ufs = gMunicipios.ObterUFs().Result.OrderBy(u => u.nome);
            foreach (GestorMunicipios.UF uf in ufs)
            {
                if (uf.sigla != "RS") {
                    Console.WriteLine($"Pulando {uf}...");
                    continue;
                }

                Console.WriteLine($"########## MUNICÍPIOS DE {uf.ToString().ToUpper()} ##########");
                uf.Municipios = gMunicipios.ObterMunicipios(uf).Result.OrderBy(m => m.nome);

                foreach (GestorMunicipios.Municipio m in uf.Municipios)
                    Console.WriteLine(m);
                
                Console.WriteLine($"####################");
            }
        }
    }
}
