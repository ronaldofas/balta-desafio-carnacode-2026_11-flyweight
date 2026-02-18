using System;

namespace DesignPatternChallenge
{
    // Contexto: Contém o estado extrínseco e referência ao Flyweight
    public struct SmartCharacter
    {
        private readonly CharacterFlyweight _flyweight;
        
        // Estado extrínseco (único para cada posição)
        public int Row { get; }
        public int Column { get; }

        public SmartCharacter(CharacterFlyweight flyweight, int row, int column)
        {
            _flyweight = flyweight;
            Row = row;
            Column = column;
        }

        public void Render()
        {
            // Combina estado extrínseco (Row, Col) com intrínseco (_flyweight)
            var style = "";
            if (_flyweight.IsBold) style += "B";
            if (_flyweight.IsItalic) style += "I";
            if (_flyweight.IsUnderline) style += "U";
            
            Console.WriteLine($"[{Row},{Column}] '{_flyweight.Symbol}' {_flyweight.FontFamily} {_flyweight.FontSize}pt {_flyweight.Color} {style}");
        }

        // Calcula memória usada por ESTA instância (referência + ints)
        // Não conta o Flyweight pois ele é compartilhado
        public int GetMemorySize()
        {
            return 8 +            // Referência para classe (em 64-bit)
                   2 * sizeof(int); // Row, Column
        }
    }
}
