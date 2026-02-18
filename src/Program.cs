using System;
using System.Reflection;

namespace DesignPatternChallenge
{
    class Runner
    {
        static void Main(string[] args) // Entry point
        {
            Console.WriteLine("========================================");
            Console.WriteLine("EXECUTANDO VERSÃO LEGADA (SEM PADRÃO)");
            Console.WriteLine("========================================");

            // Executa o Main da classe Program original via Reflection
            // Isso permite manter o Challenge.cs intocado, acessando o método private/internal
            var legacyProgramType = typeof(Program); // Classe 'Program' do Challenge.cs
            var legacyMain = legacyProgramType.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Static);
            
            if (legacyMain != null)
            {
                legacyMain.Invoke(null, new object[] { args });
            }
            else
            {
                 Console.WriteLine("Erro: Não foi possível encontrar o método Main original.");
            }

            Console.WriteLine("\n\n========================================");
            Console.WriteLine("EXECUTANDO VERSÃO REFATORADA (FLYWEIGHT)");
            Console.WriteLine("========================================");
            RunSmartVersion();

            // Comparação teórica
            PrintComparison();
            
            // Wait for user input to close
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        static void RunSmartVersion()
        {
            var factory = new FlyweightFactory();
            var document = new SmartDocument(factory);

            // Reproduzindo o mesmo cenário do Challenge.cs
            
            // 1. "Hello World"
            string text1 = "Hello World";
            for (int i = 0; i < text1.Length; i++)
            {
                document.AddCharacter(text1[i], "Arial", 12, "Black", false, false, false, 1, i + 1);
            }

            // 2. "IMPORTANT"
            string text2 = "IMPORTANT";
            for (int i = 0; i < text2.Length; i++)
            {
                document.AddCharacter(text2[i], "Arial", 12, "Red", true, false, false, 2, i + 1);
            }

            // 3. "This is a sample text"
            string text3 = "This is a sample text";
            for (int i = 0; i < text3.Length; i++)
            {
                document.AddCharacter(text3[i], "Arial", 12, "Black", false, false, false, 3, i + 1);
            }

            Console.WriteLine("Renderizando primeiros 5 caracteres (Smart):\n");
            // No SmartDocument o render imprime tudo, então vamos apenas chamar o Render geral 
            // ou criar um método específico? O original renderiza "Hello" manualmente.
            // Vamos renderizar tudo para mostrar que funciona igual.
            document.Render();

            document.PrintMemoryUsage();
        }

        static void PrintComparison()
        {
            Console.WriteLine("\n=== COMPARAÇÃO DE MEMÓRIA (Estimada para 1 Milhão de Caracteres) ===");
            
            long count = 1_000_000;
            
            // Legado: ~80 bytes por char
            long legacySize = count * 80;
            
            // Flyweight: 
            // 1. Contexto (SmartCharacter): ~16 bytes por char (ref + 2 ints)
            // 2. Flyweights: Desprezível (algumas dezenas de instâncias únicas)
            long smartSize = (count * 16) + (100 * 80); // 100 variações únicas * 80 bytes

            Console.WriteLine($"Legado:    ~{legacySize / (1024.0 * 1024.0):N2} MB");
            Console.WriteLine($"Flyweight: ~{smartSize / (1024.0 * 1024.0):N2} MB");
            Console.WriteLine($"Economia:  {((1 - (double)smartSize / legacySize) * 100):N2}%");
        }
    }
}
