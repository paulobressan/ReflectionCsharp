using System;
using System.Linq;

namespace ReflectionCsharp.Infraestrutura
{
    public static class Utilidades
    {
        //Converter urls do navegador para buscar arquivos dentro do assembly dinamicamente
        public static string ConverterPathParaNomeAssembly(string path)
        {
            //Variavel que armazena o prefixo do assembly, ou seja o nome da solução
            var prefixoAssembly = "ReflectionCsharp";
            //padrões de busca no assembly
            var pathComPontos = path.Replace("/", ".");
            //Caminho do assembly completo, nome da solução mais caminho dos diretórios(namespace)
            var nomeCompleto = $"{prefixoAssembly}{pathComPontos}";

            return nomeCompleto;
        }

        //Identificar a extensão do arquivo solicitado
        //Definir o tipo do conteudo que sera retornado
        public static string ObterTipoDeConteudo(string path)
        {
            //Se o final do arquivo terminar com .css
            if (path.EndsWith(".css"))
            {
                return "text/css charset=utf-8";
            }
            //Se não se o final do arquivo terminar com .js
            else if (path.EndsWith(".js"))
            {
                return "application/js charset=utf-8";
            }
            //Se não se o final do arquivo terminar com .html
            else if (path.EndsWith(".html"))
            {
                return "text/html charset=utf-8";
            }
            throw new NotImplementedException("Tipo de conteudo não previsto!");
        }

        //Se a url conter uma extenção, estamos requisitando um arquivo, se não é uma pagina
        public static Boolean EhArquivo(string path)
        {
            //quebrando a url em partes pela / e usando o recurso de Options para remover os espaços vazios
            var partesPath = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
            //A ultima parte pode conter uma extenção, exemplo NOME.css
            var ultimaParte = partesPath.Last();
            //Retonar true se a string contem ponto ou false se não
            return ultimaParte.Contains(".");
        }
    }
}