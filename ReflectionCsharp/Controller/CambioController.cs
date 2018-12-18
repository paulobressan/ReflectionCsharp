using ReflectionCsharp.Service;
using ReflectionCsharp.Service.Cambio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ReflectionCsharp.Controller
{
    //O controlador é responsavel por controlar o retorno dos dados
    public class CambioController
    {
        private readonly ICambioService _cambioService;
        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }

        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);
            //caminho do assembly
            var nomeCompletoResource = "ReflectionCsharp.View.Cambio.MXN.html";
            //Capturando o assembly de execução do contexto da aplicação
            var assembly = Assembly.GetExecutingAssembly();
            //Pegando o Stream(Arquivo em bytes) do assembly 
            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);
            //O pacote System.IO contem o StreamReader que fornece recursos para ler e manipular arquivos
            var streamLeitura = new StreamReader(streamRecurso);
            //Ler o texto da pagina do começo até o final e atribuir a textoPagina
            var textoPagina = streamLeitura.ReadToEnd();
            //Texto editado, trocamos o coringa VALOR_EM_REAIS do texto pelo valor dinamico
            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
            return textoResultado;
        }

        public string USD()
        {
            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
            //caminho do assembly
            var nomeCompletoResource = "ReflectionCsharp.View.Cambio.USD.html";
            //Capturando o assembly de execução do contexto da aplicação
            var assembly = Assembly.GetExecutingAssembly();
            //Pegando o Stream(Arquivo em bytes) do assembly 
            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);
            //O pacote System.IO contem o StreamReader que fornece recursos para ler e manipular arquivos
            var streamLeitura = new StreamReader(streamRecurso);
            //Ler o texto da pagina do começo até o final e atribuir a textoPagina
            var textoPagina = streamLeitura.ReadToEnd();
            //Texto editado, trocamos o coringa VALOR_EM_REAIS do texto pelo valor dinamico
            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
            return textoResultado;
        }
    }
}
