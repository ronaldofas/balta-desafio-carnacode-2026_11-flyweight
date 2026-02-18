using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    // Flyweight Factory: Gerencia e garante o compartilhamento dos objetos Flyweight
    public class FlyweightFactory
    {
        private Dictionary<string, CharacterFlyweight> _flyweights = new Dictionary<string, CharacterFlyweight>();

        public CharacterFlyweight GetFlyweight(char symbol, string fontFamily, int fontSize, string color, 
                                             bool isBold, bool isItalic, bool isUnderline)
        {
            // Gera uma chave única baseada em todas as propriedades intrínsecas
            string key = $"{symbol}-{fontFamily}-{fontSize}-{color}-{isBold}-{isItalic}-{isUnderline}";

            if (!_flyweights.ContainsKey(key))
            {
                var flyweight = new CharacterFlyweight(symbol, fontFamily, fontSize, color, isBold, isItalic, isUnderline);
                _flyweights[key] = flyweight;
            }

            return _flyweights[key];
        }

        public int GetTotalFlyweights()
        {
            return _flyweights.Count;
        }

        public void PrintFactoryStats()
        {
            Console.WriteLine($"\n=== Estatísticas da Factory ===");
            Console.WriteLine($"Total de Flyweights criados: {_flyweights.Count}");
            
            long totalMemory = 0;
            foreach (var fw in _flyweights.Values)
            {
                totalMemory += fw.GetMemorySize();
            }
             // Adiciona estimativa do Dictionary overhead
            long dictionaryOverhead = _flyweights.Count * (80 + 32); // Entry + Key String overhead aprox
            totalMemory += dictionaryOverhead;

            Console.WriteLine($"Memória da Factory: ~{totalMemory:N0} bytes");
        }
    }
}
