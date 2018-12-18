using System;
using System.Net;
using System.Reflection;

namespace ReflectionCsharp.Infraestrutura
{
    //Classe responsavel por manipular requisição de arquivos
    public class ManipuladorRequisicaoArquivo
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            //O assembly é o contexto da aplicação com todos os arquivos.
            //O metodo GetExecutingAssembly captura o assembly atual em execução
            //O retorno desse metodo é um objeto que representa o contexto da aplicação 
            var assembly = Assembly.GetExecutingAssembly();
            //Nome do recurso, nome do arquivo presente no contexo da aplicação
            //Para acessar um arquivo temos que usar o caminho de assembly que segue o padrão
            //Solution.Namespace.Arquivo
            var recurso = Utilidades.ConverterPathParaNomeAssembly(path);
            //O Styles.css é um arquivo pequeno então não é necessario preocupar com o seu tamanho.
            /**
            O stream é o fluxo de manipulação de bytes em uma aplicação, 
            quando é necessário trabalhar com arquivos muito grande ou até arquivos pequenos, 
            usamos steam para tornar manipular esse arquivo em pequenos pedaços, isso é feito
            pelo motivo de que podemos trabalhar com arquivo em que não sabemos o seu tamanho
            e para isso é necessário o uso de Stream. Cada variável criada em memória é um 
            balde de bytes onde nele está o valor da variável, quando a variável recebe
            valores pequenos, não vai ter problemas porém se a variável recebe um valor 
            maior isso vai exigir muito recurso tornando a aplicação lenta, para isso o 
            Stream entra para manipular esse arquivos, como se fosse uma mangueira nesse 
            balde pegando somente o fluxo, ou seja pequenos pedaços dos bytes tornando a 
            aplicação rápida.
             */
            var recursoStream = assembly.GetManifestResourceStream(recurso);
            //Se o recurso não for encontrado, vamos retornar o status code 404 para informa que não foi encontrado
            //e fechar a saida sem retornar.
            if (recursoStream == null)
            {
                resposta.StatusCode = 404;
                resposta.OutputStream.Close();
            }
            else
                //A classe Stream implementa a interface IDisposable e por boa pratica temos que usar o using
                //Ao utilizar o using, informamos ao .net a limpeza desse recurso, se não for informado
                //o .net acredita que esse recurso ainda vai ser utilizado
                using (recursoStream)
                {
                    //Criando um balde com o tamanho do recursoStream (styles.css)
                    var bytesRecurso = new Byte[recursoStream.Length];
                    //Como sabemos que o styles.css é pequenos, poremos ler completo, iniciando do 0
                    //O primeiro parametro é o buffer, ou seja o balde onde vai ser armazenado os dados
                    //O segundo é como vai ser canalizado esses bytes, no caso vamos carregar tudo de uma vez
                    //E o ultimo é onde termina o array de bytes
                    recursoStream.Read(bytesRecurso, 0, (int)recursoStream.Length);

                    //RESPONDENDO O STREAM
                    //Definir o tipo do conteudo que sera retornado
                    resposta.ContentType = Utilidades.ObterTipoDeConteudo(path);
                    //definir o código de resposta do HTTP
                    resposta.StatusCode = 200;
                    //tamanho da resposta, como a resposta é gerado um array de bytes, vamos ver o tamnho de posição
                    resposta.ContentLength64 = recursoStream.Length;
                    //O body da requisição só aceita stream e para escrever nele temos que usar alguns recursos
                    //Ele espera 3 parametro, o primeiro é o array de bytes, o segundo é de qual array vai ser iniciado e o terceiro até qual array vai ser escrito
                    //Ou seja, temos o tamanho de array que vai ser escrito
                    resposta.OutputStream.Write(bytesRecurso, 0, bytesRecurso.Length);
                    //Fechar o escritor do stream
                    resposta.OutputStream.Close();
                }
        }
    }
}
