using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionCsharp.Service
{
    public interface ICambioService
    {
        decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor);
    }
}
