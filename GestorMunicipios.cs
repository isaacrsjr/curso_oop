using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class GestorMunicipios
{
    public class Municipio
    {
        public int id { get; set; }
        public string nome { get; set; }

        public override string ToString() => nome;
    }

    public class UF
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }

        public override string ToString() => nome;

        public IEnumerable<Municipio> Municipios { get; set; }
    }

    const string baseUrl = "https://servicodados.ibge.gov.br";
    readonly string pathMunicipios = "/api/v1/localidades/estados/{UF}/municipios";
    readonly string pathUFs = "/api/v1/localidades/estados";

    public async Task<IEnumerable<UF>> ObterUFs()
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        IEnumerable<UF> ufs = null;
        HttpResponseMessage response = await client.GetAsync(pathUFs);
        if (response.IsSuccessStatusCode)
        {
            string jsonUFs = await response.Content.ReadAsStringAsync();
            ufs = JsonConvert.DeserializeObject<UF[]>(jsonUFs);
        }
        return ufs;
    }

    public async Task<IEnumerable<Municipio>> ObterMunicipios(UF uf)
    {
        if (uf == null)
            throw new ArgumentNullException(nameof(uf));

        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        IEnumerable<Municipio> municipios = null;
        HttpResponseMessage response = await client.GetAsync(pathMunicipios.Replace("{UF}", uf.id.ToString()));
        if (response.IsSuccessStatusCode)
        {
            var jsonMunicipios = await response.Content.ReadAsStringAsync();
            municipios = JsonConvert.DeserializeObject<Municipio[]>(jsonMunicipios);
        }
        return municipios;
    }
}