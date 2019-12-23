using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Features
{

    public class BaseMember : IMember
    {

    }

    public interface IMember
    {
        string Print(string mgs) { return mgs; }

    }
}
