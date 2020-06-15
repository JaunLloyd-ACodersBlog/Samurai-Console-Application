using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace SamuraiConsoleApplication
{
  public class Options
  {
    [Option('c', "create", Required = false, HelpText = "Creates entities on the selected type ex. \"Samurai\", \"Horses\"")]
    public bool Create { get; set; }

    [Option('s', "samurai", Required = false, HelpText = "Entity type to perform operation on")]
    public bool Samurai { get; set; }
  }
}
