using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    public class SmartDocument
    {
        private List<SmartCharacter> _characters;
        private FlyweightFactory _factory;

        public SmartDocument(FlyweightFactory factory)
        {
            _characters = new List<SmartCharacter>();
            _factory = factory;
        }

        public void AddCharacter(char symbol, string fontFamily, int fontSize, string color,
                                bool isBold, bool isItalic, bool isUnderline, int row, int column)
        {
            // 1. Obtém (ou cria) o Flyweight correspondente ao estado intrínseco
            var flyweight = _factory.GetFlyweight(symbol, fontFamily, fontSize, color, isBold, isItalic, isUnderline);

            // 2. Cria o caractere inteligente com o estado extrínseco
            var character = new SmartCharacter(flyweight, row, column);
            
            _characters.Add(character);
        }

        public void Render()
        {
            foreach (var character in _characters)
            {
                character.Render();
            }
        }

        public void PrintMemoryUsage()
        {
            long totalMemory = 0;
            
            // Soma memória das instâncias leves (SmartCharacter)
            foreach (var character in _characters)
            {
                totalMemory += character.GetMemorySize();
            }

            Console.WriteLine($"\n=== Uso de Memória (Otimizado) ===");
            Console.WriteLine($"Total de caracteres: {_characters.Count}");
            Console.WriteLine($"Memória dos contextos (SmartCharacters): {totalMemory:N0} bytes");

            // Imprime e soma memória da Factory
            _factory.PrintFactoryStats();
        }
    }
}
