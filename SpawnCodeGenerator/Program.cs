using System;
using System.IO;


namespace ArkSpawnCodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isArkModFolder()
            {
                var files = Directory.GetFiles(Directory.GetCurrentDirectory());
                for (int i = 0; i < files.Length; i++)
                {
                    string filename = Path.GetFileNameWithoutExtension(files[i]);
                    if (!filename.StartsWith("PrimalGameData"))
                    {
                        if (i == files.Length - 1) // Last file
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            void MakeSpawnCodes()
            {
                var author = "\nCode generated with ARKMod.net's ARK Code Generator. For latest version, visit https://arkmod.net/.\nHappy ARKing!";
                var engramsHeader = "\n---------------------------------------------------------------------------------Engram Names---------------------------------------------------------------------------------\n";
                var itemsHeader = "\n---------------------------------------------------------------------------------Item Spawncodes--------------------------------------------------------------------------------\n";
                var dinoHeader = "\n---------------------------------------------------------------------------------Creature Spawncodes-----------------------------------------------------------------------------\n";
                var dinoTHeader = "\n---------------------------------------------------------------------------------Tamed Creature Spawncodes-----------------------------------------------------------------------\n";
                
                var allItems = Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories);
                
                File.Delete("Output.txt"); //this will wipe the text file so a clean set of codes can be re generated
                File.AppendAllText("Output.txt", author + Environment.NewLine);

                File.AppendAllText("Output.txt", engramsHeader + Environment.NewLine);
                for (int i = 0; i < allItems.Length; i++)
                {                
                    var filename = Path.GetFileNameWithoutExtension(allItems[i]);
                    if (filename.StartsWith("EngramEntry"))
                    {

                        var s = filename + "_C";

                        File.AppendAllText("Output.txt", s + Environment.NewLine);

                        Console.WriteLine(s);
                    }
                }

                File.AppendAllText("Output.txt", itemsHeader + Environment.NewLine);// this add the item header to show that everythign below is an item spawn code
                for (int i = 0; i < allItems.Length; i++)
                {
                    var filename = Path.GetFileNameWithoutExtension(allItems[i]);

                    if (filename.StartsWith("PrimalItem"))
                    {

                        var s = @"admincheat GiveItem " + ((char)34) + "Blueprint'" + allItems[i].Substring(allItems[i].IndexOf("Content")).Replace(@"Content\", @"\Game\").Replace(".uasset", "." + filename).Replace(@"\", "/") + "'" + ((char)34) + " 1 1 0";

                        File.AppendAllText("Output.txt", s + Environment.NewLine);

                        Console.WriteLine(s);
                    }
                }
                File.AppendAllText("Output.txt", dinoHeader + Environment.NewLine);
                for (int i = 0; i < allItems.Length; i++)
                {
                    var filename = Path.GetFileNameWithoutExtension(allItems[i]);

                    if (filename.Contains("Character_BP"))
                    {

                        var s = @"admincheat SpawnDino " + ((char)34) + "Blueprint'" + allItems[i].Substring(allItems[i].IndexOf("Content")).Replace(@"Content\", @"\Game\").Replace(".uasset", "." + filename).Replace(@"\", "/") + "'" + ((char)34) + " 500 0 0 120";

                        File.AppendAllText("Output.txt", s + Environment.NewLine);

                        Console.WriteLine(s);

                    }
                }
                File.AppendAllText("Output.txt", dinoTHeader + Environment.NewLine);
                for (int i = 0; i < allItems.Length; i++)
                {
                    var filename = Path.GetFileNameWithoutExtension(allItems[i]) + "_C";

                    if (filename.Contains("Character_BP"))
                    {

                        var s = @"admincheat GMSummon " + ((char)34) + filename + ((char)34) + " 120";

                        File.AppendAllText("Output.txt", s + Environment.NewLine);

                        Console.WriteLine(s);

                    }
                }
                File.AppendAllText("Output.txt", author + Environment.NewLine);
            }
            if (isArkModFolder())
            {
                MakeSpawnCodes();
            }
            else
            {
                Console.WriteLine("This folder dont have a PrimalGameData.");
                Console.ReadKey();
            }
        }
    }
}
