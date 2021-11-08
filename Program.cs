using System;
using System.Linq;

namespace curso
{
    static class Program
    {
        static void Main(string[] args)
        {
            PrinterService ps = new("PS1");
            PrinterService ps2 = new("PS2");

            var pss = new PaperSupplierService(ps);
            var tss = new TonerSupplierService(ps);
            var cs = new CleanerService();
            cs.Handle(ps);
            cs.Handle(ps2);
            //ps.AfterPrinting.Add( (p) => Console.WriteLine("Isaac Ingrato!!!!") ); 

            ps.Print("abc");
            ps.Print("def");
            ps.Print("ghi");

            ps2.Print("xyz");
        }

        static void MostrarUFsEMunicipios()
        {
            var gMunicipios = new GestorMunicipios();
            var ufs = gMunicipios.ObterUFs().Result.OrderBy(u => u.nome);
            foreach (GestorMunicipios.UF uf in ufs)
            {
                if (uf.sigla != "RS")
                {
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