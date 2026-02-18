using System;

namespace DesignPatternChallenge
{
    // Flyweight: Armazena o estado intrínseco (compartilhado)
    public class CharacterFlyweight
    {
        public char Symbol { get; }
        public string FontFamily { get; }
        public int FontSize { get; }
        public string Color { get; }
        public bool IsBold { get; }
        public bool IsItalic { get; }
        public bool IsUnderline { get; }

        public CharacterFlyweight(char symbol, string fontFamily, int fontSize, string color, 
                                bool isBold, bool isItalic, bool isUnderline)
        {
            Symbol = symbol;
            FontFamily = fontFamily;
            FontSize = fontSize;
            Color = color;
            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderline = isUnderline;
        }

        // Método utilitário para calcular o tamanho aproximado em memória desta instância
        public int GetMemorySize()
        {
            return sizeof(char) +         // Symbol: 2 bytes
                   32 +                   // FontFamily: ~32 bytes (estimativa string)
                   sizeof(int) +          // FontSize: 4 bytes
                   32 +                   // Color: ~32 bytes (estimativa string)
                   3 * sizeof(bool);      // Booleans: 3 bytes
        }
    }
}
