using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Interfaces
{
    public interface IConfig
    {
        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }
    }

}
